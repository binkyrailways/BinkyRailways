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

import 'package:binky/models/_state_model.dart';
import 'package:flame/components.dart';
import 'package:flame/input.dart';
import 'package:flutter/material.dart';

import '../junction_component.dart' as common;
import '../../api.dart' as api;

class JunctionComponent extends common.JunctionComponent with Tappable {
  api.JunctionState state;
  final StateModel stateModel;

  JunctionComponent({required this.state, required this.stateModel})
      : super(model: state.model);

  @override
  bool onTapUp(TapUpInfo event) {
    if (state.hasSwitch_2()) {
      stateModel
          .setSwitchDirection(
              model.id, state.switch_2.directionRequested.invert())
          .then((x) {
        state = x;
      });
    }
    return true;
  }

  @override
  Color switchColor() {
    if (state.switch_2.directionActual == state.switch_2.directionRequested) {
      return super.switchColor();
    }
    return Colors.orangeAccent;
  }

  @override
  api.SwitchDirection switchDirection() => state.switch_2.directionActual;
}
