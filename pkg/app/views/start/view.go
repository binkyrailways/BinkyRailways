// Copyright 2021 Ewout Prangsma
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

package start

import (
	"gioui.org/layout"
	"gioui.org/widget"
	"gioui.org/widget/material"
	"github.com/gen2brain/dlgs"
	"github.com/rs/zerolog"
	"github.com/rs/zerolog/log"

	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/storage"
)

type (
	C = layout.Context
	D = layout.Dimensions
)

// New constructs a new start view
func New(log zerolog.Logger, vm views.ViewManager) views.View {
	return &startView{
		log: log,
		vm:  vm,
	}
}

type startView struct {
	log zerolog.Logger
	vm  views.ViewManager

	buttonOpenFile widget.Clickable
	buttonNew      widget.Clickable
}

// Return additional text to add to the window title
func (v *startView) GetTitleExtension() string {
	return ""
}

// Handle events and draw the view
func (v *startView) Layout(gtx layout.Context) layout.Dimensions {
	if v.buttonOpenFile.Clicked() {
		go v.openFile()
	}
	if v.buttonNew.Clicked() {
		go v.createNew()
	}
	th := v.vm.GetTheme()

	hs := widgets.HorizontalSplit(
		func(gtx C) D {
			return material.Button(th, &v.buttonOpenFile, "Open file").Layout(gtx)
		},
		func(gtx C) D {
			return material.Button(th, &v.buttonNew, "New").Layout(gtx)
		},
	)
	return hs.Layout(gtx)
}

// Open a file dialog to select a railway
func (v *startView) openFile() {
	if fName, ok, err := dlgs.File("Open a railway", "*.brw", false); err != nil {
		v.log.Warn().Err(err).Msg("Open file failed")
	} else if ok {
		log.Info().Str("file", fName).Msg("File selected")
		// NewPackageFromFile loads a package from file
		pkg, err := storage.NewPackageFromFile(fName)
		if err != nil {
			dlgs.Error("Cannot open railway", err.Error())
		} else {
			v.vm.OpenRailway(pkg.GetRailway())
		}
	}
}

// Create a new (blank) railway
func (v *startView) createNew() {
	if fName, ok, err := dlgs.Entry("Create a railway", "File name", "newRailway.brw"); err != nil {
		v.log.Warn().Err(err).Msg("Open file failed")
	} else if ok {
		log.Info().Str("file", fName).Msg("File selected")
		// NewPackageFromFile loads a package from file
		pkg := storage.NewPackage(fName)
		v.vm.OpenRailway(pkg.GetRailway())
	}
}
