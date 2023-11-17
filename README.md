# BinkyRailways

BinkyRailways is a program to automate running model trains.

It is being used by [Team Zwitserleven](https://www.teamzwitserleven.nl).

## Building

```bash
make binaries-gui
make
```

## Running

```bash
./bin/<os>/<architecture>/binky-server
```

Next, install the TLS certificate as trusted certificate:

```bash
# For local
curl -k https://localhost:18033/tls/ca.pem > binky-ca.pem
# For remote
curl -k https://192.168.77.1:18033/tls/ca.pem > binky-ca.pem
# Install on MacOS
sudo /usr/bin/security add-trusted-cert -d -r trustRoot -k /Library/Keychains/System.keychain binky-ca.pem
```

Then connect to https://localhost:18033
(accept the self-signed certificate).