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

package automatic

import (
	"context"
	"fmt"
)

// enabledProperty implements Property for enabling the Automatic Loc Controller
type enabledProperty struct {
	alc *automaticLocController

	actual         bool
	requested      bool
	requestChanges []func(context.Context, bool)
}

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
	p.alc.railway.Exclusive(ctx, func(ctx context.Context) error {
		result = p.actual
		return nil
	})
	return result
}
func (p *enabledProperty) SetActual(context.Context, bool) (bool, error) {
	return false, fmt.Errorf("Cannot change actual")
}

// Subscribe to actual changes
func (p *enabledProperty) SubscribeActualChanges(cb func(context.Context, bool)) {
	panic("Not support")
}

// Gets / sets the requested value
func (p *enabledProperty) GetRequested(ctx context.Context) bool {
	var result bool
	p.alc.railway.Exclusive(ctx, func(ctx context.Context) error {
		result = p.requested
		return nil
	})
	return result
}
func (p *enabledProperty) SetRequested(ctx context.Context, value bool) error {
	return p.alc.railway.Exclusive(ctx, func(ctx context.Context) error {
		if p.requested == value {
			// Nothing to do
			return nil
		}
		p.requested = value
		p.actual = value
		p.alc.Startup(ctx)
		return nil
	})
}

// Subscribe to requested changes
func (p *enabledProperty) SubscribeRequestChanges(cb func(context.Context, bool)) {
	p.alc.railway.Exclusive(context.Background(), func(c context.Context) error {
		p.requestChanges = append(p.requestChanges, cb)
		return nil
	})
}
