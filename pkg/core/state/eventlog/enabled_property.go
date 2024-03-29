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

package eventlog

import (
	"context"
	"fmt"
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
)

// enabledProperty implements Property for enabling the Event Logger
type enabledProperty struct {
	eventLogger *eventLogger

	actual         bool
	requested      bool
	requestChanges util.SliceWithIdEntries[func(context.Context, bool)]
}

const (
	updateEnabledPropertyTimeout = time.Millisecond
)

// GetName returns the name of the property
func (sp *enabledProperty) GetName() string {
	return "enabled"
}

// Is the request value equal to the actual value?
func (p *enabledProperty) IsConsistent(ctx context.Context) bool {
	return p.GetActual(ctx) == p.GetRequested(ctx)
}

// Gets / sets the actual value
func (p *enabledProperty) GetActual(ctx context.Context) bool {
	var result bool
	p.eventLogger.railway.Exclusive(ctx, updateEnabledPropertyTimeout, "enabledProperty.GetActual", func(ctx context.Context) error {
		result = p.actual
		return nil
	})
	return result
}
func (p *enabledProperty) SetActual(context.Context, bool) (bool, error) {
	return false, fmt.Errorf("Cannot change actual")
}

// Subscribe to actual changes
func (p *enabledProperty) SubscribeActualChanges(cb func(context.Context, bool)) context.CancelFunc {
	panic("Not support")
}

// Gets / sets the requested value
func (p *enabledProperty) GetRequested(ctx context.Context) bool {
	var result bool
	p.eventLogger.railway.Exclusive(ctx, updateEnabledPropertyTimeout, "enabledProperty.GetRequested", func(ctx context.Context) error {
		result = p.requested
		return nil
	})
	return result
}
func (p *enabledProperty) SetRequested(ctx context.Context, value bool) error {
	return p.eventLogger.railway.Exclusive(ctx, updateEnabledPropertyTimeout, "enabledProperty.SetRequested", func(ctx context.Context) error {
		if p.requested == value {
			// Nothing to do
			return nil
		}
		p.requested = value
		p.actual = value
		p.eventLogger.startWritingEvents()
		return nil
	})
}

// Subscribe to requested changes
func (p *enabledProperty) SubscribeRequestChanges(cb func(context.Context, bool)) context.CancelFunc {
	var cancel context.CancelFunc
	p.eventLogger.railway.Exclusive(context.Background(), updateEnabledPropertyTimeout, "enabledProperty.SubscribeRequestChanges", func(c context.Context) error {
		cancel = p.requestChanges.Append(cb)
		return nil
	})
	return cancel
}
