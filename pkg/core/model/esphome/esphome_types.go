// Copyright 2025 Ewout Prangsma
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

package esphome

import (
	"fmt"
	"os"
	"path/filepath"
	"strings"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"gopkg.in/yaml.v3"
)

// ESPHOME File structure
type DeviceFile struct {
	Substitutions map[string]string `yaml:"substitutions,omitempty"`
	Packages      map[string]any    `yaml:"packages,omitempty"`
	Esphome       *Esphome          `yaml:"esphome,omitempty"`
	Logger        *Logger           `yaml:"logger,omitempty"`
	MQTT          *MQTT             `yaml:"mqtt,omitempty"`
	OTA           *OTA              `yaml:"ota,omitempty"`
	WebServer     *WebServer        `yaml:"web_server,omitempty"`
	Wifi          *Wifi             `yaml:"wifi,omitempty"`
	BinarySensors []BinarySensor    `yaml:"binary_sensor,omitempty"`
	Buttons       []Button          `yaml:"button,omitempty"`
	Numbers       []Number          `yaml:"number,omitempty"`
	Outputs       []Output          `yaml:"output,omitempty"`
	Servos        []Servo           `yaml:"servo,omitempty"`
	Switches      []Switch          `yaml:"switch,omitempty"`
	I2C           *I2C              `yaml:"i2c,omitempty"`
	PCA9685s      []PCA9685Hub      `yaml:"pca9685,omitempty"`
	MCP23008s     []MCP23xxxHub     `yaml:"mcp23008,omitempty"`
	MCP23017s     []MCP23xxxHub     `yaml:"mcp23017,omitempty"`

	platforms map[api.DeviceID]devicePlatform
}

type Esphome struct {
	OnBoot *Trigger `yaml:"on_boot,omitempty"`
}

type Trigger struct {
	Then []Action `yaml:"then,omitempty"`
}

type Logger struct {
	Level string `yaml:"level,omitempty"`
}

type MQTT struct {
	OnConnect    []Action `yaml:"on_connect,omitempty"`
	OnDisconnect []Action `yaml:"on_disconnect,omitempty"`
}

type OTA struct {
	Platform string `yaml:"platform,omitempty"`
}

type WebServer struct {
	Port  int  `yaml:"port,omitempty"`
	Local bool `yaml:"local,omitempty"`
}

type Wifi struct {
	FastConnect  bool     `yaml:"fast_connect,omitempty"`
	Domain       string   `yaml:"domain,omitempty"`
	OnConnect    []Action `yaml:"on_connect,omitempty"`
	OnDisconnect []Action `yaml:"on_disconnect,omitempty"`
}

type I2C struct {
}

type PCA9685Hub struct {
	Id        string  `yaml:"id"`
	Address   string  `yaml:"address,omitempty"`
	Frequency float32 `yaml:"frequency,omitempty"`
}

type MCP23xxxHub struct {
	Id      string `yaml:"id"`
	Address string `yaml:"address,omitempty"`
}

type MQTTComponent struct {
	StateTopic   string `yaml:"state_topic,omitempty"`
	CommandTopic string `yaml:"command_topic,omitempty"`
}

type Component struct {
	Platform      string `yaml:"platform,omitempty"`
	Id            string `yaml:"id,omitempty"`
	Name          string `yaml:"name,omitempty"`
	MQTTComponent `yaml:",inline"`
}

type BinarySensor struct {
	Component `yaml:",inline"`
	Pin       *Pin     `yaml:"pin,omitempty"`
	OnState   *Trigger `yaml:"on_state,omitempty"`
}

type Button struct {
	Component `yaml:",inline"`
	OnPress   *Trigger `yaml:"on_press,omitempty"`
}

type Switch struct {
	Component `yaml:",inline"`
	Pin       *Pin `yaml:"pin,omitempty"`
}

type Output struct {
	Component `yaml:",inline"`
	Channel   string `yaml:"channel,omitempty"`
	PCA9685Id string `yaml:"pca9685_id,omitempty"`
}

type Servo struct {
	Component        `yaml:",inline"`
	Output           string `yaml:"output,omitempty"`
	AutoDetachTime   string `yaml:"auto_detach_time,omitempty"`
	TransitionLength string `yaml:"transition_length,omitempty"`
	MinLevel         string `yaml:"min_level,omitempty"`
	MaxLevel         string `yaml:"max_level,omitempty"`
	IdleLevel        string `yaml:"idle_level,omitempty"`
}

type Number struct {
	Component `yaml:",inline"`
	MinValue  *int     `yaml:"min_value,omitempty"`
	MaxValue  *int     `yaml:"max_value,omitempty"`
	Step      *int     `yaml:"step,omitempty"`
	SetAction []Action `yaml:"set_action,omitempty"`
}

type Action map[string]any

type Pin struct {
	Number   string   `yaml:"number,omitempty"`
	MCP23XXX string   `yaml:"mcp23xxx,omitempty"`
	Mode     *PinMode `yaml:"mode,omitempty"`
	Inverted bool     `yaml:"inverted,omitempty"`
}

type PinMode struct {
	Input  bool `yaml:"input,omitempty"`
	Output bool `yaml:"output,omitempty"`
}

type yamlInclude string

func (i yamlInclude) MarshalYAML() (interface{}, error) {
	return &yaml.Node{
		Kind:  yaml.ScalarNode,
		Value: string(i),
		Tag:   "!include",
	}, nil
}

type yamlLambda string

func (i yamlLambda) MarshalYAML() (interface{}, error) {
	return &yaml.Node{
		Kind:  yaml.ScalarNode,
		Value: string(i),
		Tag:   "!lambda",
	}, nil
}

// Turn the given input into a valid esphome name
func name(input string) string {
	return strings.ToLower(input)
}

// Save file to disk
func (f *DeviceFile) Save(dir string, name string) error {
	if err := os.MkdirAll(dir, 0755); err != nil {
		return fmt.Errorf("failed to mkdir '%s': %w", dir, err)
	}
	encoded, err := yaml.Marshal(f)
	if err != nil {
		return fmt.Errorf("failed encode esphome file '%s': %w", name, err)
	}
	fullName := filepath.Join(dir, name+".yaml")
	if err := os.WriteFile(fullName, encoded, 0644); err != nil {
		return fmt.Errorf("failed to write '%s': %w", fullName, err)
	}
	return nil
}
