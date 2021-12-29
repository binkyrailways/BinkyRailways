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

import 'package:flame/extensions.dart';
import 'package:flame/game.dart';

import '../../models/model_model.dart';
import '../../models/state_model.dart';

import 'module_component.dart';

class RailwayGame extends FlameGame {
  final ModelModel modelModel;
  final StateModel stateModel;

  RailwayGame({required this.modelModel, required this.stateModel});

  @override
  Color backgroundColor() => const Color(0xFFFFFFFF);

  // Load the game components
  @override
  Future<void> onLoad() async {
    await super.onLoad();

    var size = Vector2.all(20);
    final rwModel = await modelModel.getRailway();
    for (var modRef in rwModel.modules) {
      final p = modRef.position;
      final zoomFactor = modRef.zoomFactor.toDouble() / 100.0;
      final maxX = p.x.toDouble() + (p.width.toDouble() * zoomFactor);
      final maxY = p.y.toDouble() + (p.height.toDouble() * zoomFactor);
      size.x = size.x > maxX ? size.x : maxX;
      size.y = size.y > maxY ? size.y : maxY;
      final module = await modelModel.getModule(modRef.id);
      final modComp = ModuleComponent(model: module, moduleRef: modRef);
      await modComp.loadChildren(modelModel, stateModel);
      add(modComp);
    }

    camera.viewport = FixedResolutionViewport(size);
  }
}
