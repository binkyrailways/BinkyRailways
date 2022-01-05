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

import '../module_component.dart' as common;
import '../../api.dart' as mapi;
import '../../models.dart';
import './block_component.dart';
import './junction_component.dart';
import './output_component.dart';
import './sensor_component.dart';

class ModuleComponent extends common.ModuleComponent {
  final mapi.ModuleRef moduleRef;

  ModuleComponent({required mapi.Module model, required this.moduleRef})
      : super(model: model) {
    loadPosition(moduleRef.position);
  }

  Future<void> loadChildren(
      ModelModel modelModel, StateModel stateModel) async {
    await loadBackgroundImage(modelModel);
    for (var blockRef in model.blocks) {
      final blockState = await stateModel.getBlockState(blockRef.id);
      add(BlockComponent(state: blockState));
    }
    for (var junctionRef in model.junctions) {
      final junctionState = await stateModel.getJunctionState(junctionRef.id);
      add(JunctionComponent(state: junctionState, stateModel: stateModel));
    }
    for (var outputRef in model.outputs) {
      final outputState = await stateModel.getOutputState(outputRef.id);
      add(OutputComponent(state: outputState, stateModel: stateModel));
    }
    for (var sensorRef in model.sensors) {
      final sensorState = await stateModel.getSensorState(sensorRef.id);
      add(SensorComponent(state: sensorState, stateModel: stateModel));
    }
  }
}
