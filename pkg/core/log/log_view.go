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

package log

import (
	"context"
	"sync"
)

// A LogView is a callback that is called when the list of events changes
type LogView func(events []LogEvent)

type logViews struct {
	mutex  sync.RWMutex
	lastID int32
	views  []logViewsEntry
}

type logViewsEntry struct {
	ID   int32
	View LogView
}

// Add a view to the given log views.
// The returned functions must be called to remove the view again.
func (lvs *logViews) InvokeViews(events []LogEvent) {
	lvs.mutex.RLock()
	views := lvs.views
	lvs.mutex.RUnlock()

	for _, x := range views {
		x.View(events)
	}
}

// Add a view to the given log views.
// The returned functions must be called to remove the view again.
func (lvs *logViews) Add(lv LogView) context.CancelFunc {
	lvs.mutex.Lock()
	defer lvs.mutex.Unlock()
	lvs.lastID++
	id := lvs.lastID

	lvs.views = append(lvs.views, logViewsEntry{
		ID:   id,
		View: lv,
	})

	return func() {
		lvs.mutex.Lock()
		defer lvs.mutex.Unlock()

		for i, x := range lvs.views {
			if x.ID == id {
				lvs.views = append(lvs.views[:i], lvs.views[i+1:]...)
				break
			}
		}
	}
}
