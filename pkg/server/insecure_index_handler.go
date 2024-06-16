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
	"net/http"
	"strconv"
	"strings"

	"github.com/labstack/echo/v4"
)

const (
	indexHTML = `<html>
<head>
<style>
body {
	max-width: 200em;
}
.button {
  background-image: linear-gradient(#42A1EC, #0070C9);
  border: 1px solid #0077CC;
  border-radius: 4px;
  box-sizing: border-box;
  color: #FFFFFF;
  cursor: pointer;
  direction: ltr;
  display: inline-block;
  font-family: "SF Pro Text","SF Pro Icons","AOS Icons","Helvetica Neue",Helvetica,Arial,sans-serif;
  font-size: 17px;
  font-weight: 400;
  letter-spacing: -.022em;
  line-height: 1.47059;
  overflow: visible;
  padding: 4px 15px;
  text-align: center;
  vertical-align: baseline;
  user-select: none;
  -webkit-user-select: none;
  touch-action: manipulation;
  white-space: nowrap;
}

.button:disabled {
  cursor: default;
  opacity: .3;
}

.button:hover {
  background-image: linear-gradient(#51A9EE, #147BCD);
  border-color: #1482D0;
  text-decoration: none;
}

.button:active {
  background-image: linear-gradient(#3D94D9, #0067B9);
  border-color: #006DBC;
  outline: none;
}

.button:focus {
  box-shadow: rgba(131, 192, 253, 0.5) 0 0 0 3px;
  outline: none;
}

.button-25 {
  background-color: #36A9AE;
  background-image: linear-gradient(#37ADB2, #329CA0);
  border: 1px solid #2A8387;
  border-radius: 4px;
  box-shadow: rgba(0, 0, 0, 0.12) 0 1px 1px;
  color: #FFFFFF;
  cursor: pointer;
  display: inline-block;
  font-family: -apple-system,".SFNSDisplay-Regular","Helvetica Neue",Helvetica,Arial,sans-serif;
  font-size: 17px;
  line-height: 100%;
  margin: 0;
  outline: 0;
  padding: 11px 15px 12px;
  text-align: center;
  transition: box-shadow .05s ease-in-out,opacity .05s ease-in-out;
  user-select: none;
  -webkit-user-select: none;
  touch-action: manipulation;
}

.button-25:hover {
  box-shadow: rgba(255, 255, 255, 0.3) 0 0 2px inset, rgba(0, 0, 0, 0.4) 0 1px 2px;
  text-decoration: none;
  transition-duration: .15s, .15s;
}

.button-25:active {
  box-shadow: rgba(0, 0, 0, 0.15) 0 2px 4px inset, rgba(0, 0, 0, 0.4) 0 1px 1px;
}

.button-25:disabled {
  cursor: not-allowed;
  opacity: .6;
}

.button-25:disabled:active {
  pointer-events: none;
}

.button-25:disabled:hover {
  box-shadow: none;
}

.code {
	background-color: rgba(51, 51, 51, 0.05);
    border-radius: 8px;
	display: inline-block;
	padding: 15px;
	font-size: 150%;
	margin: 0;
}
</style>
</head>
<body>
<h1>Welcome to Binky Railways</h1>

<h2>Browser Application</h2>
<p>
The easiest way to use Binky Railways is through your browser.
</p>
<p>
First install the TLS certificate in your system.
</p>

<p>
<b>On MacOS:</b>
Copy the commands in the box below and execute them in a Terminal on your Mac.
</p>

<pre class="code">
curl -k http://%HOST%:%HTTPPORT%/tls/ca.pem > binky-ca.pem
sudo /usr/bin/security add-trusted-cert -d -r trustRoot \
    -k /Library/Keychains/System.keychain binky-ca.pem
</pre>

<p>
<b>On Linux:</b>
Copy the commands in the box below and execute them in a Terminal on your Linux machine.
</p>

<pre class="code">
curl -k http://%HOST%:%HTTPPORT%/tls/ca.pem > binky-ca.pem
sudo cp binky-ca.pem /usr/local/share/ca-certificates/
sudo update-ca-certificates
</pre>

<p>
<b>On Windows:</b>
Copy the commands in the box below and execute them in a Powershell on your Windows machine.
</p>

<pre class="code">
curl -k http://%HOST%:%HTTPPORT%/tls/ca.pem > binky-ca.pem
certutil -addstore root binky-ca.pem
</pre>

<p>
<a class="button-25" href="https://%HOST%:%HTTPSPORT%">Open BinkyRailways in Browser</a>
</p>

<p>
<a class="button-25" href="https://%HOST%:%HTTPSPORT%/debug/pprof/">Open Debug Profiles</a>
</p>

<p>
<a class="button" href="http://%HOST%:%HTTPPORT%/tls/ca.pem" download>Download Certificate</a>
</p>

<h2>MacOS Application</h2>

<p>
If you want the best possible performance on your Mac, use the MacOS application.
</p>

<p>
<a class="button" href="binkyrailways://%HOST%">Open BinkyRailways in MacOS Application</a>
</p>

</body>
</html>`
)

func (s *Server) handleInsecureGetIndex(c echo.Context) error {
	replacer := strings.NewReplacer(
		"%HOST%", s.PublishedHostDNSName,
		"%HTTPPORT%", strconv.Itoa(s.HTTPPort),
		"%HTTPSPORT%", strconv.Itoa(s.HTTPSPort),
	)
	return c.HTML(http.StatusOK, replacer.Replace(indexHTML))
}
