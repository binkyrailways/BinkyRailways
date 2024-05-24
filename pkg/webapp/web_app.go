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

package webapp

import (
	"fmt"
	"log"
	"net/http"
	"os"

	webapp "github.com/binkyrailways/BinkyRailways/apps/binky"
)

// Load the filesystem to serve
func GetWebAppFileSystem(webDevelopment bool) http.FileSystem {
	if webDevelopment {
		log.Print("using live mode")
		return http.FS(os.DirFS("./apps/binky/build/web"))
	}

	log.Print("using embed mode")
	fsys := webapp.GetWebAppFileSystem()
	return fsys
}

var (
	webAppBuild string
)

// Get's a build identifier for the webapp
func GetWebAppBuild(webDevelopment bool) string {
	// Fast path for production build
	if !webDevelopment && webAppBuild != "" {
		return webAppBuild
	}

	// Load build info
	fs := GetWebAppFileSystem(webDevelopment)
	mainDartJs, err := fs.Open("main.dart.js")
	if err != nil {
		return ""
	}
	defer mainDartJs.Close()
	info, err := mainDartJs.Stat()
	if err != nil {
		return ""
	}
	webAppBuild = fmt.Sprintf("%d-%d", info.ModTime().Unix(), info.Size())
	return webAppBuild
}
