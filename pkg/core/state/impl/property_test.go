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

package impl

import (
	"context"
	"testing"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
	"github.com/stretchr/testify/assert"
	"github.com/stretchr/testify/mock"
)

type dispatcherMock struct {
	mock.Mock
}

var _ state.EventDispatcher = &dispatcherMock{}

// Send the given event to all interested receivers.
func (m *dispatcherMock) Send(evt state.Event) {
	m.Called(evt)
}

// Subscribe to events.
// To cancel the subscription, call the given cancel function.
func (m *dispatcherMock) Subscribe(ctx context.Context, cb func(state.Event)) context.CancelFunc {
	return m.Called(ctx, cb).Get(0).(context.CancelFunc)
}

type testEntity struct {
	mock.Mock
}

var _ state.Entity = &testEntity{}

// Unique ID of the underlying entity
func (m *testEntity) GetID() string {
	return m.Called().String(0)
}

// Description of the underlying entity
func (m *testEntity) GetDescription() string {
	return m.Called().String(0)
}

// Gets the railway state this object is a part of.
func (m *testEntity) GetRailway() state.Railway {
	return m.Called().Get(0).(state.Railway)
}

// Is this entity fully resolved such that is can be used in the live railway?
func (m *testEntity) GetIsReadyForUse() bool {
	return m.Called().Bool(0)
}

func TestBoolProperty(t *testing.T) {
	ctx := context.Background()
	subject := &testEntity{}
	dispatch := &dispatcherMock{}
	p := &boolProperty{}
	dispatch.On("Send", mock.MatchedBy(func(evt state.Event) bool {
		x, ok := evt.(state.ActualStateChangedEvent)
		if !ok {
			return false
		}
		assert.Equal(t, subject, x.Subject)
		assert.Equal(t, p, x.Property)
		return true
	})).Return()
	dispatch.On("Send", mock.MatchedBy(func(evt state.Event) bool {
		x, ok := evt.(state.RequestedStateChangedEvent)
		if !ok {
			return false
		}
		assert.Equal(t, subject, x.Subject)
		assert.Equal(t, p, x.Property)
		return true
	})).Return()
	p.Configure(subject, dispatch, util.NewExclusive())

	assert.False(t, p.GetActual(ctx))
	assert.NoError(t, p.SetActual(ctx, true))
	assert.NoError(t, p.SetRequested(ctx, true))
	dispatch.AssertExpectations(t)
	subject.AssertExpectations(t)
}

func TestSwitchDirectionProperty(t *testing.T) {
	ctx := context.Background()
	subject := &testEntity{}
	dispatch := &dispatcherMock{}
	p := &switchDirectionProperty{}
	dispatch.On("Send", mock.MatchedBy(func(evt state.Event) bool {
		x, ok := evt.(state.ActualStateChangedEvent)
		if !ok {
			return false
		}
		assert.Equal(t, subject, x.Subject)
		assert.Equal(t, p, x.Property)
		return true
	})).Return()
	dispatch.On("Send", mock.MatchedBy(func(evt state.Event) bool {
		x, ok := evt.(state.RequestedStateChangedEvent)
		if !ok {
			return false
		}
		assert.Equal(t, subject, x.Subject)
		assert.Equal(t, p, x.Property)
		return true
	})).Return()
	p.Configure(subject, dispatch, util.NewExclusive())

	assert.False(t, p.GetActual(ctx) == model.SwitchDirectionStraight)
	assert.NoError(t, p.SetActual(ctx, model.SwitchDirectionOff))
	assert.NoError(t, p.SetRequested(ctx, model.SwitchDirectionOff))
	dispatch.AssertExpectations(t)
	subject.AssertExpectations(t)
}
