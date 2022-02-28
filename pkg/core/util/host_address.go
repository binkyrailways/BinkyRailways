// Copyright 2022 Ewout Prangsma
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

package util

import (
	"fmt"
	"net"
)

// FindServerHostAddress tries to find the IP address of a network interface that
// matches the given host address as best as possible.
// E.g. If host == "1.2.3.0" and an interface with address "1.2.3.4" exists, that will be returned.
func FindServerHostAddress(host string) (string, error) {
	// Parse host address
	hostIP := net.ParseIP(host)
	if hostIP == nil {
		return "", fmt.Errorf("failed to parse host address: '%s'", host)
	}

	// Collect suitable interface addresses
	intfs, err := net.Interfaces()
	if err != nil {
		return "", fmt.Errorf("failed to fetch network interfaces: %w", err)
	}
	var ipList []net.IP
	for _, intf := range intfs {
		if intf.Flags&net.FlagUp == 0 {
			continue
		}
		if intf.Flags&net.FlagLoopback != 0 {
			continue
		}
		if addrs, err := intf.Addrs(); err == nil {
			for _, addr := range addrs {
				if ip, _, err := net.ParseCIDR(addr.String()); err == nil && ip != nil && ip.To4() != nil {
					ipList = append(ipList, ip)
				}
			}
		}
	}
	// Find best address
	for maskBits := 32; maskBits >= 0; maskBits-- {
		mask := net.CIDRMask(maskBits, 32) // Assume IPv4
		for _, ip := range ipList {
			masked := ip.Mask(mask)
			if masked.Equal(hostIP) {
				// Found a good match
				return ip.String(), nil
			}
		}
	}

	return "", fmt.Errorf("did not find a network interface matching address '%s'", host)
}
