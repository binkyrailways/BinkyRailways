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
	"errors"
	"fmt"
	"strings"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Create esphome yaml configuration files for all local workers.
func BuildEsphomeConfigs(baseFolder string, cs model.BinkyNetCommandStation, lwSet model.BinkyNetLocalWorkerSet) error {
	var result error
	lwSet.ForEach(func(lwModel model.BinkyNetLocalWorker) {
		if lwModel.GetLocalWorkerType() != model.BinkynetLocalWorkerTypeEsphome {
			return
		}
		fileSet := &DeviceFileSet{
			platforms:   make(map[api.DeviceID]*devicePlatform),
			deviceFiles: make(map[string]*DeviceFile),
		}
		lwModel.GetRouters().ForEach(func(router model.BinkyNetRouter) {
			file := &DeviceFile{
				Substitutions: map[string]string{
					"name": router.GetModuleID(),
				},
				Packages: map[string]any{
					"networking": yamlInclude("../packages/networking.yaml"),
					"device":     yamlInclude("../packages/device-d1mini.yaml"),
				},
				Esphome: &Esphome{
					OnBoot: &Trigger{
						Then: []Action{
							{"switch.turn_on": "led_yellow"},
							{"switch.turn_on": "led_green"},
							{"switch.turn_on": "led_red"},
							{"delay": "2s"},
							{"switch.turn_off": "led_yellow"},
							{"switch.turn_off": "led_green"},
							{"switch.turn_off": "led_red"},
						},
					},
				},
				Logger: &Logger{
					Level: "DEBUG",
				},
				MQTT: &MQTT{
					OnConnect: []Action{
						{"switch.turn_on": "led_green"},
					},
					OnDisconnect: []Action{
						{"switch.turn_off": "led_green"},
					},
					TopicPrefix: api.GetMqttModuleInfoPrefix(router.GetModuleID()),
				},
				OTA: &OTA{
					Platform: "esphome",
				},
				WebServer: &WebServer{
					Port:  80,
					Local: true,
				},
				Wifi: &Wifi{
					FastConnect: true,
					OnConnect: []Action{
						{"switch.turn_on": "led_yellow"},
					},
					OnDisconnect: []Action{
						{"switch.turn_off": "led_yellow"},
					},
				},
				Buttons: []Button{
					createRebootButton(),
				},
				Switches: []Switch{
					createLedSwitch("led_red", "D5"),
					createLedSwitch("led_yellow", "D6"),
					createLedSwitch("led_green", "D7"),
				},
			}
			// Setup domain
			if domain := cs.GetDomain(); domain != "" {
				file.Wifi.Domain = "." + strings.TrimPrefix(domain, ".")
			}
			fileSet.deviceFiles[router.GetID()] = file
			// Add common sensors
			file.createUptimeSensor(router.GetModuleID())
			file.createWifiSignalSensor(router.GetModuleID())
			file.createWifiInfoSensor(router.GetModuleID())
		})
		// Add devices
		lwModel.GetDevices().ForEach(func(devModel model.BinkyNetDevice) {
			if !devModel.GetIsDisabled() {
				if err := addDevice(fileSet, devModel); err != nil {
					result = errors.Join(result, err)
				}
			}
		})
		// Add objects
		lwModel.GetObjects().ForEach(func(objModel model.BinkyNetObject) {
			if err := addObject(fileSet, objModel, lwModel); err != nil {
				result = errors.Join(result, err)
			}
		})
		// Store config(s)
		lwModel.GetRouters().ForEach(func(router model.BinkyNetRouter) {
			fname := fmt.Sprintf("lw-%s", router.GetModuleID())
			file := fileSet.deviceFiles[router.GetID()]
			if err := file.Save(baseFolder, fname); err != nil {
				result = errors.Join(result, err)
			}
		})
	})
	return result
}

// Create a reboot button
func createRebootButton() Button {
	return Button{
		Component: Component{
			Platform: "restart",
			Id:       "reboot",
			Name:     "reboot",
		},
	}
}

// Create a Switch for leds
func createLedSwitch(id, port string) Switch {
	return Switch{
		Component: Component{
			Platform: "gpio",
			Id:       id,
			Name:     id,
		},
		Pin: &Pin{
			Number:   port,
			Inverted: true,
		},
	}
}

// Adds the given device to the esphome config
func addDevice(fs *DeviceFileSet, devModel model.BinkyNetDevice) error {
	f, ok := fs.deviceFiles[devModel.GetRouter().GetID()]
	if !ok {
		return fmt.Errorf("No DeviceFile found for router on device %s", devModel.GetDeviceID())
	}
	switch devModel.GetDeviceType() {
	case api.DeviceTypeGPIO:
		fs.platforms[devModel.GetDeviceID()] = &devicePlatform{
			Platform:   "gpio",
			deviceFile: f,
			configureOutput: func(o *Output) {
				o.Platform = "gpio"
			},
		}
	case api.DeviceTypePCA9685:
		hub := PCA9685Hub{
			Id:        model.NormalizeName(string(devModel.GetDeviceID())),
			Address:   devModel.GetAddress(),
			Frequency: 54,
		}
		f.I2C = &I2C{}
		f.PCA9685s = append(f.PCA9685s, hub)
		fs.platforms[devModel.GetDeviceID()] = &devicePlatform{
			Platform:   "pca9685",
			deviceFile: f,
			configureOutput: func(o *Output) {
				o.Platform = "pca9685"
				o.PCA9685Id = hub.Id
			},
		}
	case api.DeviceTypeMCP23008:
		hub := MCP23xxxHub{
			Id:      model.NormalizeName(string(devModel.GetDeviceID())),
			Address: devModel.GetAddress(),
		}
		f.I2C = &I2C{}
		f.MCP23008s = append(f.MCP23008s, hub)
		fs.platforms[devModel.GetDeviceID()] = &devicePlatform{
			Platform:   "gpio",
			deviceFile: f,
			configurePin: func(pin *Pin) {
				pin.MCP23XXX = hub.Id
			},
		}
	case api.DeviceTypeMCP23017:
		hub := MCP23xxxHub{
			Id:      model.NormalizeName(string(devModel.GetDeviceID())),
			Address: devModel.GetAddress(),
		}
		f.I2C = &I2C{}
		f.MCP23017s = append(f.MCP23017s, hub)
		fs.platforms[devModel.GetDeviceID()] = &devicePlatform{
			Platform:   "gpio",
			deviceFile: f,
			configurePin: func(pin *Pin) {
				pin.MCP23XXX = hub.Id
			},
		}
	case api.DeviceTypePCF8574:
		hub := PCF8574Hub{
			Id:      model.NormalizeName(string(devModel.GetDeviceID())),
			Address: devModel.GetAddress(),
		}
		f.I2C = &I2C{}
		f.PCF8574s = append(f.PCF8574s, hub)
		fs.platforms[devModel.GetDeviceID()] = &devicePlatform{
			Platform:   "gpio",
			deviceFile: f,
			configurePin: func(pin *Pin) {
				pin.PCF8574 = hub.Id
			},
		}
	}
	return nil
}

// Adds the given object to the esphome config
func addObject(fs *DeviceFileSet, objModel model.BinkyNetObject, lwModel model.BinkyNetLocalWorker) error {
	disabled := false
	for bnc := range objModel.GetConnections().All() {
		if anyPinsHaveDisabledDevice(bnc, lwModel) {
			disabled = true
			break
		}
	}
	if disabled {
		return nil
	}
	switch objModel.GetObjectType() {
	case api.ObjectTypeBinarySensor:
		return addBinarySensor(fs, objModel)
	case api.ObjectTypeBinaryOutput:
		return addBinaryOutput(fs, objModel)
	case api.ObjectTypeMagneticSwitch:
		return addMagneticSwitch(fs, objModel)
	case api.ObjectTypeServoSwitch:
		return addServoSwitch(fs, objModel)
	case api.ObjectTypeRelaySwitch:
		return addRelaySwitch(fs, objModel)
	case api.ObjectTypeTrackInverter:
		return addTrackInverter(fs, objModel)
	}
	/*
			disabled := false
		o := &api.Object{
			Id:            objModel.GetObjectID(),
			Type:          objModel.GetObjectType(),
			Configuration: make(map[api.ObjectConfigKey]string),
		}
		objModel.GetConfiguration().ForEach(func(k, v string) {
			o.Configuration[api.ObjectConfigKey(k)] = v
		})
		objModel.GetConnections().ForEach(func(cm model.BinkyNetConnection) {
			if anyPinsHaveDisabledDevice(cm, lwModel) {
				// Using a disabled device
				disabled = true
			} else if allPinsHaveNoDevice(cm) {
				// No device configured for this connection, ignore it
			} else {
				conn := &api.Connection{
					Key: cm.GetKey(),
				}
				cm.GetPins().ForEach(func(pm model.BinkyNetDevicePin) {
					conn.Pins = append(conn.Pins, &api.DevicePin{
						DeviceId: pm.GetDeviceID(),
						Index:    pm.GetIndex(),
					})
				})
				conn.Configuration = make(map[api.ConfigKey]string)
				cm.GetConfiguration().ForEach(func(k, v string) {
					conn.Configuration[api.ConfigKey(k)] = v
				})
				o.Connections = append(o.Connections, conn)
			}
		})
		if !disabled {
			lw.Objects = append(lw.Objects, o)
		}
	*/
	return nil
}

// anyPinsHaveDisabledDevice returns true if any of the pins in the
// given connection refer to a disabled device.
func anyPinsHaveDisabledDevice(cm model.BinkyNetConnection, lw model.BinkyNetLocalWorker) bool {
	foundDisabledDevice := false
	cm.GetPins().ForEach(func(pin model.BinkyNetDevicePin) {
		if id := pin.GetDeviceID(); id != "" {
			if dev, found := lw.GetDevices().Get(id); found {
				if dev.GetIsDisabled() {
					foundDisabledDevice = true
				}
			}
		}
	})
	return foundDisabledDevice
}
