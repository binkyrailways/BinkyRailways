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

import 'package:binky/api/generated/br_state_types.pb.dart';
import 'package:binky/canvas/layers_overlay.dart';
import 'package:flame/components.dart';
import 'package:flame/extensions.dart';
import 'package:flame/game.dart';
import 'dart:math';
import 'package:flutter/material.dart';

import '../../models.dart';

import './module_component.dart';
import '../view_settings.dart';
import 'block_overlay.dart';

class RailwayGame extends FlameGame with HasHoverables, HasTappables {
  final ModelModel modelModel;
  final StateModel stateModel;
  final ViewSettings viewSettings;
  BlockState? _block;
  Vector2? _overlayPosition;

  static const blockOverlay = "assignBlockToLoc";
  static const layersOverlay = "layers";

  RailwayGame(
      {required this.modelModel,
      required this.stateModel,
      required this.viewSettings});

  @override
  Color backgroundColor() => const Color(0xFFFFFFFF);

  void showBlock(Vector2 position, BlockState block) {
    _overlayPosition = position;
    _block = block;
    overlays.add(blockOverlay);
  }

  Widget blockOverlayBuilder(BuildContext buildContext, RailwayGame game) {
    return Stack(
      children: [
        GestureDetector(
          child: Container(
            color: Colors.grey.withAlpha(128),
          ),
          onTap: () {
            game.overlays.remove(blockOverlay);
          },
        ),
        Positioned(
          left: _overlayPosition?.x,
          top: _overlayPosition?.y,
          width: 400,
          height: 300,
          child: Container(
            padding: const EdgeInsets.all(8),
            color: Colors.white,
            child: BlockOverlay(
              stateModel: stateModel,
              block: _block!,
              onClose: () {
                game.overlays.remove(blockOverlay);
              },
            ),
          ),
        ),
      ],
    );
  }

  void showLayers(Vector2 position) {
    _overlayPosition = position;
    overlays.add(layersOverlay);
  }

  Widget layersOverlayBuilder(BuildContext buildContext, RailwayGame game) {
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
                final rw = await modelModel.getRailway();
                final modules = await Future.wait(
                    rw.modules.map((e) => modelModel.getModule(e.id)));
                final Set<String> layers = {};
                for (var m in modules) {
                  layers.addAll(m.layers);
                }
                final result = layers.toList();
                result.sort(((a, b) => a.compareTo(b)));
                return result;
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

  // Load the game components
  @override
  Future<void> onLoad() async {
    await super.onLoad();

    var size = Vector2.all(20);
    try {
      final rwModel = await modelModel.getRailway();
      for (var modRef in rwModel.modules) {
        final p = modRef.position;
        final module = await modelModel.getModule(modRef.id);
        final zoomFactor = modRef.zoomFactor.toDouble() / 100.0;
        final x = p.hasX() ? p.x : 0;
        final y = p.hasY() ? p.y : 0;
        final maxX = x.toDouble() + (module.width.toDouble() * zoomFactor);
        final maxY = y.toDouble() + (module.height.toDouble() * zoomFactor);
        size.x = max(size.x, maxX);
        size.y = max(size.y, maxY);
        final modComp = ModuleComponent(viewSettings,
            model: module, moduleRef: modRef, game: this);
        modComp.scale = Vector2.all(zoomFactor);
        await modComp.loadChildren(modelModel, stateModel);
        add(modComp);
      }
    } catch (err) {
      print(err);
    }
    camera.viewport = FixedResolutionViewport(size, noClip: true);
  }
}
