// Copyright 2020 Ewout Prangsma
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

package impl

import (
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

type route struct {
	moduleEntity

	FromBlock         BlockRef         `xml:"FromBlock"`
	ToBlock           BlockRef         `xml:"ToBlock"`
	FromBlockSide     *model.BlockSide `xml:"FromBlockSide,omitempty"`
	ToBlockSide       *model.BlockSide `xml:"ToBlockSide,omitempty"`
	Speed             *int             `xml:"Speed"`
	ChooseProbability *int             `xml:"ChooseProbability"`
	MaxDuration       *int             `xml:"MaxDuration"`
	Closed            *bool            `xml:"Closed"`
}

var _ model.Route = &route{}

// newRoute initialize a new route
func newRoute() *route {
	r := &route{}
	return r
}

// Starting point of the route
func (r *route) GetFrom() model.EndPoint {
	if result, ok := r.FromBlock.Get(r.module); ok {
		return result
	}
	// TODO Edge
	return nil
}
func (r *route) SetFrom(value model.EndPoint) error {
	if value == nil {
		r.FromBlock.Set(nil, r.module, r.OnModified)
	} else if b, ok := value.(model.Block); ok {
		return r.FromBlock.Set(b, r.module, r.OnModified)
	}
	return nil
}

// Side of the <see cref="From"/> block at which this route will leave that block.
func (r *route) GetFromBlockSide() model.BlockSide {
	return refs.BlockSideValue(r.FromBlockSide, model.BlockSideFront)
}
func (r *route) SetFromBlockSide(value model.BlockSide) error {
	if r.GetFromBlockSide() != value {
		r.FromBlockSide = refs.NewBlockSide(value)
		r.OnModified()
	}
	return nil
}

// End point of the route
func (r *route) GetTo() model.EndPoint {
	if result, ok := r.ToBlock.Get(r.module); ok {
		return result
	}
	// TODO Edge
	return nil
}
func (r *route) SetTo(value model.EndPoint) error {
	if value == nil {
		r.ToBlock.Set(nil, r.module, r.OnModified)
	} else if b, ok := value.(model.Block); ok {
		return r.ToBlock.Set(b, r.module, r.OnModified)
	}
	return nil
}

// Side of the <see cref="To"/> block at which this route will enter that block.
func (r *route) GetToBlockSide() model.BlockSide {
	return refs.BlockSideValue(r.ToBlockSide, model.BlockSideBack)
}
func (r *route) SetToBlockSide(value model.BlockSide) error {
	if r.GetToBlockSide() != value {
		r.ToBlockSide = refs.NewBlockSide(value)
		r.OnModified()
	}
	return nil
}

/// <summary>
/// Set of junctions with their states that are crossed when taking this route.
/// </summary>
//IJunctionWithStateSet CrossingJunctions { get; }

/// <summary>
/// Set of events that change the state of the route and it's running loc.
/// </summary>
//IRouteEventSet Events { get; }

// Speed of locs when going this route.
// This value is a percentage of the maximum / medium speed of the loc.
// <value>0..100</value>
func (r *route) GetSpeed() int {
	return refs.IntValue(r.Speed, model.DefaultRouteSpeed)
}
func (r *route) SetSpeed(value int) error {
	if r.GetSpeed() != value {
		r.Speed = refs.NewInt(value)
		r.OnModified()
	}
	return nil
}

// Probability (in percentage) that a loc will take this route.
// When multiple routes are available to choose from the route with the highest probability will have the highest
// chance or being chosen.
// <value>0..100</value>
func (r *route) GetChooseProbability() int {
	return refs.IntValue(r.ChooseProbability, model.DefaultRouteChooseProbability)
}
func (r *route) SetChooseProbability(value int) error {
	if r.GetChooseProbability() != value {
		r.ChooseProbability = refs.NewInt(value)
		r.OnModified()
	}
	return nil
}

/// <summary>
/// Gets the predicate used to decide which locs are allowed to use this route.
/// </summary>
//ILocStandardPredicate Permissions { get; }

// Is this route open for traffic or not?
// Setting to true, allows for maintance etc. on this route.
func (r *route) GetClosed() bool {
	return refs.BoolValue(r.Closed, false)
}
func (r *route) SetClosed(value bool) error {
	if r.GetClosed() != value {
		r.Closed = refs.NewBool(value)
		r.OnModified()
	}
	return nil
}

// Maximum time in seconds that this route should take.
// If a loc takes this route and exceeds this duration, a warning is given.
func (r *route) GetMaxDuration() int {
	return refs.IntValue(r.MaxDuration, model.DefaultRouteMaxDuration)
}
func (r *route) SetMaxDuration(value int) error {
	if r.GetMaxDuration() != value {
		r.MaxDuration = refs.NewInt(value)
		r.OnModified()
	}
	return nil
}

/// <summary>
/// Trigger fired when a loc has starts entering the destination of this route.
/// </summary>
//IActionTrigger EnteringDestinationTrigger { get; }

/// <summary>
/// Trigger fired when a loc has reached the destination of this route.
/// </summary>
//        IActionTrigger DestinationReachedTrigger { get; }
