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

import 'package:flame/game.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../models.dart';
import '../../editor/editor_context.dart';
import '../view_settings.dart';

import 'module_game.dart';

class ModuleCanvas extends StatelessWidget {
  final ViewSettings viewSettings;

  const ModuleCanvas({Key? key, required this.viewSettings}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context, listen: false);
    final model = Provider.of<ModelModel>(context, listen: false);
    final selector = editorCtx.selector;
    final moduleId = selector.idOf(EntityType.module) ?? "";
    final game = ModuleGame(
        editorCtx: editorCtx,
        modelModel: model,
        moduleId: moduleId,
        viewSettings: viewSettings);
    return Stack(
      children: [
        GameWidget(
          autofocus: true,
          game: game,
          overlayBuilderMap: {
            ModuleGame.layersOverlay: game.layersOverlayBuilder,
          },
        ),
        Positioned(
          right: 8,
          top: 8,
          child: GestureDetector(
              child: const Icon(Icons.layers),
              onTapDown: (TapDownDetails details) {
                game.showLayers(Vector2(
                    details.localPosition.dx, details.localPosition.dy));
              }),
        ),
      ],
    );
  }
}
