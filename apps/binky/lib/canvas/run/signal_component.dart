// Copyright 2024 Ewout Prangsma
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

import '../signal_component.dart' as common;
import '../view_settings.dart';
import '../../api.dart' as api;
import '../../models.dart';

class SignalComponent extends common.SignalComponent with Tappable {
  Holder<api.SignalState> state;
  final StateModel stateModel;

  SignalComponent(ViewSettings viewSettings,
      {required this.state, required this.stateModel})
      : super(viewSettings, model: state.last.model);

  @override
  bool onTapUp(TapUpInfo event) {
    if (onVisibleLayer()) {
      final rw = stateModel.getCachedRailwayState();
      final isVirtualModeEnabled = rw?.isVirtualModeEnabled ?? false;
      if (isVirtualModeEnabled) {
        final sState = state.last;
        stateModel.clickVirtualSensor(sState.model.id);
      }
      return false;
    }
    return true;
  }
}
