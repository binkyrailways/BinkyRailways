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
import 'package:flutter/material.dart';

import '../../models/model_model.dart';
import '../../editor/editor_context.dart';
import 'module_component.dart';

class ModuleGame extends FlameGame {
  final ModelModel modelModel;
  final String moduleId;
  final EditorContext editorCtx;

  ModuleGame(
      {required this.editorCtx,
      required this.modelModel,
      required this.moduleId});

  @override
  Color backgroundColor() => Colors.white;

  // Load the game components
  @override
  Future<void> onLoad() async {
    await super.onLoad();

    final module = await modelModel.getModule(moduleId);
    final size = Vector2(module.width.toDouble(), module.height.toDouble());
    final modComp = ModuleComponent(model: module);
    await modComp.loadChildren(editorCtx, modelModel);
    add(modComp);

    camera.viewport = FixedResolutionViewport(size);
  }
}
