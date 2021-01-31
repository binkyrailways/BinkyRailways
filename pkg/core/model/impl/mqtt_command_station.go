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

// MqttCommandStation extends model MqttCommandStation with implementation methods
type MqttCommandStation interface {
	model.MqttCommandStation
	PersistentEntity
}

type mqttCommandStation struct {
	commandStation

	HostName    *string `xml:"HostName,omitempty"`
	Port        *int    `xml:"Port,omitempty"`
	TopicPrefix *string `xml:"TopicPrefix,omitempty"`
}

var _ model.MqttCommandStation = &mqttCommandStation{}

// NewMqttCommandStation creates a new MQTT type command station
func NewMqttCommandStation() MqttCommandStation {
	cs := &mqttCommandStation{}
	cs.Initialize()
	return cs
}

// GetEntityType returns the type of this entity
func (cs *mqttCommandStation) GetEntityType() string {
	return TypeMqttCommandStation
}

// Accept a visit by the given visitor
func (cs *mqttCommandStation) Accept(v model.EntityVisitor) interface{} {
	return v.VisitMqttCommandStation(cs)
}

// What types of addresses does this command station support?
// The result may vary depending on the type of the optional given entity.
func (cs *mqttCommandStation) GetSupportedAddressTypes(entity model.AddressEntity) []model.AddressType {
	if l, ok := entity.(model.Loc); ok && l != nil {
		return []model.AddressType{model.AddressTypeMqtt, model.AddressTypeDcc}
	}
	return []model.AddressType{model.AddressTypeMqtt}
}

// Network hostname of the command station
func (cs *mqttCommandStation) GetHostName() string {
	return refs.StringValue(cs.HostName, "mqtt")
}
func (cs *mqttCommandStation) SetHostName(value string) error {
	if cs.GetHostName() != value {
		cs.HostName = refs.NewString(value)
		cs.OnModified()
	}
	return nil
}

// Network port of the command station
func (cs *mqttCommandStation) GetPort() int {
	return refs.IntValue(cs.Port, 1883)
}
func (cs *mqttCommandStation) SetPort(value int) error {
	if cs.GetPort() != value {
		cs.Port = refs.NewInt(value)
		cs.OnModified()
	}
	return nil
}

// Prefix inserted before topics.
func (cs *mqttCommandStation) GetTopicPrefix() string {
	return refs.StringValue(cs.TopicPrefix, "")
}
func (cs *mqttCommandStation) SetTopicPrefix(value string) error {
	if cs.GetTopicPrefix() != value {
		cs.TopicPrefix = refs.NewString(value)
		cs.OnModified()
	}
	return nil
}

func (cs *mqttCommandStation) Upgrade() {
	// Nothing needed
}
