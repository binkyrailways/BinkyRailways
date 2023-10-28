// Copyright 2023 Ewout Prangsma
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Author Ewout Prangsma
//

package server

import (
	"bytes"
	"crypto"
	"crypto/ecdsa"
	"crypto/elliptic"
	"crypto/rand"
	"crypto/rsa"
	"crypto/tls"
	"crypto/x509"
	"crypto/x509/pkix"
	"encoding/json"
	"encoding/pem"
	"errors"
	"fmt"
	"math/big"
	"os"
	"strings"
	"time"

	"github.com/rs/zerolog"
)

type CA struct {
	Certificate []*x509.Certificate
	PrivateKey  interface{}
}

type certificateStorage struct {
	CA struct {
		Public  string `json:"publicKey"`
		Private string `json:"privateKey"`
	} `json:"ca"`
	Certificate struct {
		Public  string `json:"publicKey"`
		Private string `json:"privateKey"`
	} `json:"certificate"`
}

// loadCertificateFromFile tries to load a certificate info from file
func loadCertificateFromFile(path string) (certificateStorage, error) {
	var result certificateStorage
	content, err := os.ReadFile(path)
	if err != nil {
		return result, err
	}
	if err := json.Unmarshal(content, &result); err != nil {
		return result, err
	}
	return result, nil
}

// saveCertificateToFile saves a certificate info to file
func saveCertificateToFile(info certificateStorage, path string) error {
	content, err := json.Marshal(info)
	if err != nil {
		return fmt.Errorf("failed to marshal certificate info: %w", err)
	}
	if err := os.WriteFile(path, content, 0644); err != nil {
		return fmt.Errorf("failed to write certificate info: %w", err)
	}
	return nil
}

// createSelfSignedCertificate creates a self-signed TLS certificate and
// returns a tls Config with that certificate.
func createSelfSignedCertificate(log zerolog.Logger, publishedHost, storagePath string) (*tls.Config, error) {
	// Try to load from file first
	stg, err := loadCertificateFromFile(storagePath)
	if err == nil {
		if result, err := func() (*tls.Config, error) {
			// Load certificate
			cert, err := tls.X509KeyPair([]byte(stg.Certificate.Public), []byte(stg.Certificate.Private))
			if err != nil {
				return nil, fmt.Errorf("failed to load certificate: %w", err)
			}
			return &tls.Config{
				Certificates: []tls.Certificate{cert},
			}, nil

		}(); err == nil {
			return result, nil
		} else {
			log.Warn().Err(err).Msg("Failed to load certificate from storage, creating new one")
		}
	}

	// Create a CA
	pubCA, privCA, err := createCertificate(publishedHost, nil)
	if err != nil {
		return nil, fmt.Errorf("failed to create CA: %w", err)
	}
	ca, err := loadCAFromPEM(pubCA, privCA)
	if err != nil {
		return nil, fmt.Errorf("failed to load CA: %w", err)
	}
	// Create cert
	pub, priv, err := createCertificate(publishedHost, &ca)
	if err != nil {
		return nil, fmt.Errorf("failed to create certificate: %w", err)
	}
	cert, err := tls.X509KeyPair([]byte(pub), []byte(priv))
	if err != nil {
		return nil, fmt.Errorf("failed to load certificate: %w", err)
	}
	// Save to storage
	stg = certificateStorage{}
	stg.CA.Public = pubCA
	stg.CA.Private = privCA
	stg.Certificate.Public = pub
	stg.Certificate.Private = priv
	if err := saveCertificateToFile(stg, storagePath); err != nil {
		log.Error().Err(err).Msg("Failed to save certificate")
	}
	return &tls.Config{
		Certificates: []tls.Certificate{cert},
	}, nil
}

// loadCAFromPEM parses the given public & private key of a x509 certificate.
func loadCAFromPEM(cert, key string) (CA, error) {
	certs, privKey, err := loadFromPEM(cert, key)
	if err != nil {
		return CA{}, err
	}
	return CA{
		Certificate: certs,
		PrivateKey:  privKey,
	}, nil
}

// loadFromPEM parses the given certificate & key into a certificate slice & private key.
func loadFromPEM(cert, key string) ([]*x509.Certificate, interface{}, error) {
	var certs []*x509.Certificate

	// Parse certificate
	pemCerts := []byte(cert)
	for len(pemCerts) > 0 {
		var block *pem.Block
		block, pemCerts = pem.Decode(pemCerts)
		if block == nil {
			break
		}
		if block.Type != "CERTIFICATE" || len(block.Headers) != 0 {
			continue
		}

		c, err := x509.ParseCertificate(block.Bytes)
		if err != nil {
			return nil, nil, err
		}

		certs = append(certs, c)
	}
	if len(certs) == 0 {
		return nil, nil, fmt.Errorf("No CERTIFICATE's found in '%s'", cert)
	}

	// Parse key
	pemKey := []byte(key)
	var privKey interface{}
	for len(pemKey) > 0 {
		var block *pem.Block
		block, pemKey = pem.Decode(pemKey)
		if block == nil {
			break
		}

		if block.Type == "PRIVATE KEY" || strings.HasSuffix(block.Type, " PRIVATE KEY") {
			if privKey == nil {
				var err error
				privKey, err = parsePrivateKey(block.Bytes)
				if err != nil {
					return nil, nil, err
				}
			}
		}
	}
	if privKey == nil {
		return nil, nil, fmt.Errorf("No PRIVATE KEY found in '%s'", key)
	}

	return certs, privKey, nil
}

// Attempt to parse the given private key DER block. OpenSSL 0.9.8 generates
// PKCS#1 private keys by default, while OpenSSL 1.0.0 generates PKCS#8 keys.
// OpenSSL ecparam generates SEC1 EC private keys for ECDSA. We try all three.
func parsePrivateKey(der []byte) (crypto.PrivateKey, error) {
	if key, err := x509.ParsePKCS1PrivateKey(der); err == nil {
		return key, nil
	}
	if key, err := x509.ParsePKCS8PrivateKey(der); err == nil {
		switch key := key.(type) {
		case *rsa.PrivateKey, *ecdsa.PrivateKey:
			return key, nil
		default:
			return nil, errors.New("tls: found unknown private key type in PKCS#8 wrapping")
		}
	}
	if key, err := x509.ParseECPrivateKey(der); err == nil {
		return key, nil
	}

	return nil, errors.New("tls: failed to parse private key")
}

// Create a TLS x509 certificate.
// Returns: publicKey, privateKey, error
func createCertificate(publishedHost string, ca *CA) (string, string, error) {
	priv, err := ecdsa.GenerateKey(elliptic.P256(), rand.Reader)
	if err != nil {
		return "", "", fmt.Errorf("failed to generated ECDSA key: %w", err)
	}

	notBefore := time.Now()
	notAfter := notBefore.Add(time.Hour * 24 * 30)

	serialNumberLimit := new(big.Int).Lsh(big.NewInt(1), 128)
	serialNumber, err := rand.Int(rand.Reader, serialNumberLimit)
	if err != nil {
		return "", "", fmt.Errorf("failed to generate serial number: %w", err)
	}

	var subject pkix.Name
	subject.CommonName = publishedHost
	subject.Organization = []string{"BinkyRailways"}
	template := x509.Certificate{
		SerialNumber: serialNumber,
		Subject:      subject,
		NotBefore:    notBefore,
		NotAfter:     notAfter,

		KeyUsage:              x509.KeyUsageKeyEncipherment | x509.KeyUsageDigitalSignature,
		ExtKeyUsage:           []x509.ExtKeyUsage{x509.ExtKeyUsageAny | x509.ExtKeyUsageServerAuth},
		BasicConstraintsValid: true,
	}

	if ca == nil {
		template.IsCA = true
		template.KeyUsage |= x509.KeyUsageCertSign
	}

	// Create the certificate
	var derBytes []byte
	if ca != nil {
		derBytes, err = x509.CreateCertificate(rand.Reader, &template, ca.Certificate[0], publicKey(priv), ca.PrivateKey)
		if err != nil {
			return "", "", fmt.Errorf("Failed to create signed certificate: %w", err)
		}
	} else {
		derBytes, err = x509.CreateCertificate(rand.Reader, &template, &template, publicKey(priv), priv)
		if err != nil {
			return "", "", fmt.Errorf("Failed to create self-signed certificate: %w", err)
		}
	}

	// Encode certificate
	// Public key
	buf := &bytes.Buffer{}
	pem.Encode(buf, &pem.Block{Type: "CERTIFICATE", Bytes: derBytes})
	if ca != nil {
		for _, c := range ca.Certificate {
			pem.Encode(buf, &pem.Block{Type: "CERTIFICATE", Bytes: c.Raw})
		}
	}
	certPem := buf.String()

	// Private key
	buf = &bytes.Buffer{}
	privPemBlock, err := pemBlockForKey(priv)
	if err != nil {
		return "", "", err
	}
	pem.Encode(buf, privPemBlock)
	privPem := buf.String()

	return certPem, privPem, nil
}

func publicKey(priv interface{}) interface{} {
	switch k := priv.(type) {
	case *rsa.PrivateKey:
		return &k.PublicKey
	case *ecdsa.PrivateKey:
		return &k.PublicKey
	default:
		return nil
	}
}

func pemBlockForKey(priv interface{}) (*pem.Block, error) {
	switch k := priv.(type) {
	case *rsa.PrivateKey:
		return &pem.Block{Type: "RSA PRIVATE KEY", Bytes: x509.MarshalPKCS1PrivateKey(k)}, nil
	case *ecdsa.PrivateKey:
		b, err := x509.MarshalECPrivateKey(k)
		if err != nil {
			return nil, fmt.Errorf("Unable to marshal ECDSA private key: %w", err)
		}
		return &pem.Block{Type: "EC PRIVATE KEY", Bytes: b}, nil
	default:
		return nil, nil
	}
}
