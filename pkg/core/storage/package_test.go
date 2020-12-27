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

package storage

import (
	"os"
	"sort"
	"testing"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/stretchr/testify/assert"
	"github.com/stretchr/testify/require"
)

func TestLoad(t *testing.T) {
	// Open package
	t.Log(os.Getwd())
	p, err := NewPackageFromFile("../../../Fixtures/tzl.brw")
	require.NoError(t, err)
	require.NotNil(t, p)
	p.OnError().Add(func(value interface{}) {
		t.Logf("OnError: %v", value)
	})

	// Check railway
	assert.Equal(t, "Zwitserlandbaan", p.GetRailway().GetDescription())

	// Load Loc
	l := p.GetLoc("79881cff-da67-4422-bf03-8d593984bdac")
	require.NotNil(t, l)
	assert.Equal(t, model.AddressTypeDcc, l.GetAddress().Network.Type)
	assert.Equal(t, "62", l.GetAddress().Value)
	assert.Equal(t, "TM2/2 (S)", l.GetDescription())
	assert.Equal(t, "Sjraar", l.GetOwner())

	// Foreach loc
	var list []string
	p.ForEachLoc(func(l model.Loc) {
		list = append(list, l.GetDescription())
	})
	sort.Strings(list)
	assert.EqualValues(t, []string{"ABe 4/4 34", "ABe 4/46", "ABe 54", "Borgward Sattel Triebwagen", "Car LinksOm", "Car RechtsOm", "GE 46", "Ge 4/4", "Ge 4/4 622", "Ge 4/4 III", "Ge 6/6", "Ge 6/6 II", "Ge 6/6 Krokodil", "RhB 605", "TM2/2 (N)", "TM2/2 (S)", "Te 2/2 #72"}, list)

	// Load module
	m := p.GetModule("cfd620a5-a5e8-40f1-baa4-fd6efb2b8907")
	require.NotNil(t, m)
	assert.Equal(t, "De baan", m.GetDescription())
	assert.Equal(t, 30, m.GetBlocks().GetCount())

	// Test block
	b, ok := m.GetBlocks().Get("e851c545-e408-4ac4-b38f-28a6a4616fee")
	assert.True(t, ok)
	require.NotNil(t, b)
	assert.Equal(t, "sch-ond-1", b.GetDescription())
	assert.Equal(t, 428, b.GetX())
	assert.Equal(t, 5, b.GetY())
	assert.Equal(t, 89, b.GetWidth())
	assert.Equal(t, 16, b.GetHeight())
	assert.True(t, b.GetLocked())
	assert.True(t, b.GetReverseSides())
	assert.Equal(t, model.ChangeDirectionAllow, b.GetChangeDirection())
	require.NotNil(t, b.GetBlockGroup())
	assert.Equal(t, "fb22cdda-5f8c-4dab-af46-71f582a5e06c", b.GetBlockGroup().GetID())

	// Foreach block
	m.GetBlocks().ForEach(func(b model.Block) {
		assert.Equal(t, m, b.GetModule(), b.GetID())
	})

	// Test BlockGroup
	bg, ok := m.GetBlockGroups().Get("0b7af321-79f0-4669-8d3f-d2c764419eda")
	assert.True(t, ok)
	require.NotNil(t, bg)
	assert.Equal(t, "Schaduw station boven", bg.GetDescription())
	assert.Equal(t, 1, bg.GetMinimumLocsInGroup())
	assert.Equal(t, 6, bg.GetMinimumLocsOnTrackForMinimumLocsInGroupStart())

	// Foreach block group
	m.GetBlockGroups().ForEach(func(bg model.BlockGroup) {
		assert.Equal(t, m, bg.GetModule(), bg.GetID())
	})

	// Test Switch
	j, ok := m.GetJunctions().Get("f99677ca-7861-451c-a279-39115dd793af")
	assert.True(t, ok)
	require.NotNil(t, j)
	sw, ok := j.(model.Switch)
	assert.True(t, ok)
	require.NotNil(t, sw)
	assert.Equal(t, "w-322", sw.GetDescription())
	assert.Equal(t, 111, sw.GetX())
	assert.Equal(t, 8, sw.GetY())
	assert.Equal(t, 32, sw.GetWidth())
	assert.Equal(t, 12, sw.GetHeight())
	assert.True(t, sw.GetLocked())
	assert.True(t, sw.GetHasFeedback())
	assert.Equal(t, 1000, sw.GetSwitchDuration())
	assert.True(t, sw.GetInvert())
	assert.False(t, sw.GetInvertFeedback())
	assert.Equal(t, "LocoNet 322", sw.GetAddress().String())
	assert.True(t, sw.GetFeedbackAddress().IsEmpty())

	// Foreach junction
	m.GetJunctions().ForEach(func(j model.Junction) {
		assert.Equal(t, m, j.GetModule(), j.GetID())
	})

	// Test Sensor
	s, ok := m.GetSensors().Get("b9ec78e6-4255-4139-8e02-67d8d9f9e7b4")
	assert.True(t, ok)
	require.NotNil(t, s)
	assert.Equal(t, "S-231", s.GetDescription())
	assert.Equal(t, 559, s.GetX())
	assert.Equal(t, 59, s.GetY())
	assert.Equal(t, 10, s.GetWidth())
	assert.Equal(t, 10, s.GetHeight())
	assert.Equal(t, "LocoNet 231", s.GetAddress().String())

	// Foreach sensor
	m.GetSensors().ForEach(func(j model.Sensor) {
		assert.Equal(t, m, j.GetModule(), j.GetID())
	})

	// Test Route
	r, ok := m.GetRoutes().Get("17fc9047-646a-4fbf-b543-251f6f9c94d6")
	assert.True(t, ok)
	require.NotNil(t, r)
	assert.Equal(t, "sch-ond-1 -> ond-a", r.GetDescription())
	assert.Equal(t, "e851c545-e408-4ac4-b38f-28a6a4616fee", r.GetFrom().GetID())
	assert.Equal(t, "5c181b09-4f7d-4e8b-b543-37bc1ed49898", r.GetTo().GetID())
	assert.Equal(t, model.BlockSideFront, r.GetFromBlockSide())
	assert.Equal(t, model.BlockSideBack, r.GetToBlockSide())
	assert.Equal(t, model.DefaultRouteSpeed, r.GetSpeed())
	assert.Equal(t, model.DefaultRouteChooseProbability, r.GetChooseProbability())
	assert.Equal(t, model.DefaultRouteMaxDuration, r.GetMaxDuration())
	assert.False(t, r.GetClosed())

	// Test Route.CrossingJunctions
	swsCount := 0
	r.GetCrossingJunctions().ForEach(func(jws model.JunctionWithState) {
		assert.Equal(t, m, jws.GetModule(), jws.GetID())
		if sws, ok := jws.(model.SwitchWithState); ok {
			swsCount++
			assert.Equal(t, "f99677ca-7861-451c-a279-39115dd793af", sws.GetJunction().GetID())
			assert.Equal(t, model.SwitchDirectionStraight, sws.GetDirection())
		}
	})
	assert.Equal(t, 1, swsCount)

	// Test Route.Events
	var routeEventList []model.RouteEvent
	r.GetEvents().ForEach(func(rev model.RouteEvent) {
		routeEventList = append(routeEventList, rev)
		assert.Equal(t, m, rev.GetModule(), rev.GetID())
	})
	require.Len(t, routeEventList, 2)
	assert.Equal(t, "e50b7a09-81f9-4a04-8030-feba4c8f8257", routeEventList[0].GetID())
	assert.Equal(t, "S-215", routeEventList[0].GetDescription())
	assert.Equal(t, "b8c93f02-ed0b-42c2-90b1-c397e36d1bc6", routeEventList[0].GetSensor().GetID())
	assert.Equal(t, 1, routeEventList[0].GetBehaviors().GetCount())

	// Foreach route
	m.GetRoutes().ForEach(func(r model.Route) {
		assert.Equal(t, m, r.GetModule(), r.GetID())
	})

	// Foreach edge
	m.GetEdges().ForEach(func(r model.Edge) {
		assert.Equal(t, m, r.GetModule(), r.GetID())
	})

	// Test Output (Clock4Stage)
	op, ok := m.GetOutputs().Get("9548a54d-b252-45d1-a734-4ba0d10cfb1d")
	assert.True(t, ok)
	require.NotNil(t, op)
	cso, ok := op.(model.Clock4StageOutput)
	assert.True(t, ok)
	require.NotNil(t, cso)
	assert.Equal(t, "Clock", cso.GetDescription())
	assert.Equal(t, 394, cso.GetX())
	assert.Equal(t, 214, cso.GetY())
	assert.Equal(t, 32, cso.GetWidth())
	assert.Equal(t, 32, cso.GetHeight())
	assert.Equal(t, "LocoNet 1", cso.GetAddress1().String())
	assert.Equal(t, "LocoNet 2", cso.GetAddress2().String())

	// Foreach output
	m.GetOutputs().ForEach(func(r model.Output) {
		assert.Equal(t, m, r.GetModule(), r.GetID())
	})
}
