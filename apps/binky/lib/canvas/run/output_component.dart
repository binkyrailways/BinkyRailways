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

import 'package:binky/models/_state_model.dart';
import 'package:flame/components.dart';
import 'package:flame/input.dart';
import 'package:flutter/material.dart';

import '../output_component.dart' as common;
import '../view_settings.dart';
import '../../api.dart' as api;
import '../../models.dart';

class OutputComponent extends common.OutputComponent with Tappable {
  Holder<api.OutputState> state;
  final StateModel stateModel;

  OutputComponent(ViewSettings viewSettings,
      {required this.state, required this.stateModel})
      : super(viewSettings, model: state.last.model);

  @override
  bool onTapUp(TapUpInfo event) {
    if (onVisibleLayer()) {
      final sw = state.last;
      if (sw.hasBinaryOutput()) {
        stateModel.setBinaryOutputActive(
            model.id, !sw.binaryOutput.activeActual);
      }
      return false;
    }
    return true;
  }

  @override
  bool isActive() {
    final sw = state.last;
    if (sw.hasBinaryOutput()) {
      return sw.binaryOutput.activeActual;
    }
    return super.isActive();
  }

  @override
  bool showActiveStatusText() => true;
}
