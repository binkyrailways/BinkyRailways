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

import 'package:binky/canvas/layers_overlay.dart';
import 'package:flame/extensions.dart';
import 'package:flame/game.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import '../../models.dart';
import '../../editor/editor_context.dart';
import '../base_game.dart';
import '../view_settings.dart';
import 'module_component.dart';

class ModuleGame extends BaseGame {
  final ModelModel modelModel;
  final String moduleId;
  final EditorContext editorCtx;
  final ViewSettings viewSettings;

  Vector2? _overlayPosition;

  static const layersOverlay = "layers";

  ModuleGame(
      {required this.editorCtx,
      required this.modelModel,
      required this.moduleId,
      required this.viewSettings});

  @override
  Color backgroundColor() => Colors.white;

  bool shiftPressed() => HardwareKeyboard.instance.isShiftPressed;

  // Load the game components
  @override
  Future<void> onLoad() async {
    await super.onLoad();

    final module = await modelModel.getModule(moduleId);
    final size = Vector2(module.width.toDouble(), module.height.toDouble());
    final modComp = ModuleComponent(viewSettings, model: module, game: this);
    await modComp.loadChildren(editorCtx, modelModel);
    add(modComp);

    camera.viewport = FixedResolutionViewport(size, noClip: true);
  }

  void showLayers(Vector2 position) {
    _overlayPosition = position;
    overlays.add(layersOverlay);
  }

  Widget layersOverlayBuilder(BuildContext buildContext, ModuleGame game) {
    return Stack(
      children: [
        GestureDetector(
          child: Container(
            color: Colors.grey.withAlpha(128),
          ),
          onTap: () {
            game.overlays.remove(layersOverlay);
          },
        ),
        Positioned(
          right: _overlayPosition?.x,
          top: _overlayPosition?.y,
          width: 250,
          height: 300,
          child: Container(
            padding: const EdgeInsets.all(8),
            color: Colors.white,
            child: LayersOverlay(
              modelModel: modelModel,
              viewSettings: viewSettings,
              buildLayers: () async {
                final module = await modelModel.getModule(moduleId);
                return module.layers;
              },
              onClose: () {
                game.overlays.remove(layersOverlay);
              },
            ),
          ),
        ),
      ],
    );
  }
}
