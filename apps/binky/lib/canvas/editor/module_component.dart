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
import '../../editor/editor_context.dart';
import './block_component.dart';
import './junction_component.dart';
import './output_component.dart';
import './sensor_component.dart';
import './module_game.dart';

class ModuleComponent extends common.ModuleComponent {
  final ModuleGame game;
  ModuleComponent({required mapi.Module model, required this.game})
      : super(model: model);

  Future<void> loadChildren(
      EditorContext editorCtx, ModelModel modelModel) async {
    // Load background image (if any)
    await loadBackgroundImage(modelModel);
    // Load blocks
    for (var blockRef in model.blocks) {
      final block = await modelModel.getBlock(blockRef.id);
      add(BlockComponent(
          editorCtx: editorCtx, model: block, modelModel: modelModel));
    }
    // Load junctions
    for (var junctionRef in model.junctions) {
      final junction = await modelModel.getJunction(junctionRef.id);
      add(JunctionComponent(
          editorCtx: editorCtx, model: junction, modelModel: modelModel));
    }
    // Load outputs
    for (var outputRef in model.outputs) {
      final output = await modelModel.getOutput(outputRef.id);
      add(OutputComponent(
          editorCtx: editorCtx, model: output, modelModel: modelModel));
    }
    // Load sensors
    for (var sensorRef in model.sensors) {
      final sensor = await modelModel.getSensor(sensorRef.id);
      add(SensorComponent(
          editorCtx: editorCtx,
          model: sensor,
          modelModel: modelModel,
          game: game));
    }
  }
}
