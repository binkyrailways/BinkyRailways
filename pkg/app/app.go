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

package app

import (
	"os"

	gapp "gioui.org/app"
	"gioui.org/font/gofont"
	"gioui.org/io/system"
	"gioui.org/layout"
	"gioui.org/op"
	"gioui.org/unit"
	"gioui.org/widget/material"
	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/app/views/railway"
	"github.com/binkyrailways/BinkyRailways/pkg/app/views/start"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/storage"
	"github.com/rs/zerolog"
)

// Config for the application
type Config struct {
	ProjectVersion string
	ProjectBuild   string
	RailwayPath    string
}

// Dependencies for the application
type Dependencies struct {
	Logger zerolog.Logger
}

// App implements application
type App struct {
	Config
	Dependencies

	theme         *material.Theme
	mainWindow    *gapp.Window
	startView     views.View
	railwayView   views.View
	currentView   views.View
	railwayChange chan model.Railway
}

// New creates a new, intialized App instance.
func New(cfg Config, deps Dependencies) *App {
	a := &App{
		Config:        cfg,
		Dependencies:  deps,
		theme:         material.NewTheme(gofont.Collection()),
		mainWindow:    gapp.NewWindow(gapp.Title("BinkyRailways"), gapp.Size(unit.Dp(2048), unit.Dp(1600))),
		railwayChange: make(chan model.Railway),
	}
	a.startView = start.New(deps.Logger, a)
	return a
}

// Run the application until it closes.
func (a *App) Run() error {
	log := a.Logger
	go func() {
		a.setCurrentView(a.startView)
		go a.openRailwayFromConfig()
		if err := a.runEventLoop(); err != nil {
			log.Fatal().Err(err).Msg("loop failed")
		}
		os.Exit(0)
	}()
	gapp.Main()
	return nil
}

// runEventLoop runs the event loop of the main window.
func (a *App) runEventLoop() error {
	var ops op.Ops
	for {
		select {
		case r := <-a.railwayChange:
			// Change current railway
			a.railwayView = railway.New(a, r)
			a.setCurrentView(a.railwayView)
		case e := <-a.mainWindow.Events():
			switch e := e.(type) {
			case system.DestroyEvent:
				return e.Err
			case system.FrameEvent:
				gtx := layout.NewContext(&ops, e)
				a.currentView.Layout(gtx)
				e.Frame(gtx.Ops)
			}
		}
	}
}

// GetTheme return the current theme
func (a *App) GetTheme() *material.Theme {
	return a.theme
}

// Invalidate forces a redraw of the window
func (a *App) Invalidate() {
	a.mainWindow.Invalidate()
}

// OpenRailway closes any current railway and opens the given railway.
func (a *App) OpenRailway(r model.Railway) {
	a.railwayChange <- r
}

// setCurrentView switch the current view
func (a *App) setCurrentView(v views.View) {
	a.currentView = v
	/*titleExt := ""
	if v != nil {
		titleExt = v.GetTitleExtension()
	}*/
	a.Invalidate()
}

// openRailwayFromConfig opens a railway from the Config (if any).
func (a *App) openRailwayFromConfig() {
	fName := a.Config.RailwayPath
	if fName == "" {
		return
	}
	// NewPackageFromFile loads a package from file
	log := a.Logger
	pkg, err := storage.NewPackageFromFile(fName)
	if err != nil {
		log.Fatal().Err(err).Msg("Cannot open railway")
	}
	a.OpenRailway(pkg.GetRailway())
}
