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

import 'railway_game.dart';
import '../view_settings.dart';

class RailwayCanvas extends StatelessWidget {
  final ViewSettings viewSettings;

  const RailwayCanvas({Key? key, required this.viewSettings}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<ModelModel>(builder: (context, model, child) {
      return Consumer<StateModel>(builder: (context, state, child) {
        return _RailwayCanvas(
          modelModel: model,
          stateModel: state,
          viewSettings: viewSettings,
        );
      });
    });
  }
}

class _RailwayCanvas extends StatefulWidget {
  final ModelModel modelModel;
  final StateModel stateModel;
  final ViewSettings viewSettings;

  const _RailwayCanvas(
      {Key? key,
      required this.modelModel,
      required this.stateModel,
      required this.viewSettings})
      : super(key: key);

  @override
  State<StatefulWidget> createState() => _RailwayCanvasState();
}

class _RailwayCanvasState extends State<_RailwayCanvas> {
  RailwayGame? _game;

  @override
  void initState() {
    super.initState();
    _game = RailwayGame(
        modelModel: widget.modelModel,
        stateModel: widget.stateModel,
        viewSettings: widget.viewSettings);
  }

  @override
  Widget build(BuildContext context) {
    return Stack(
      children: [
        GameWidget(
          game: _game!,
          loadingBuilder: (context) => const Text("Loading canvas..."),
          errorBuilder: (context, err) => Text("Error: $err"),
          overlayBuilderMap: {
            RailwayGame.blockOverlay: _game!.blockOverlayBuilder,
            RailwayGame.layersOverlay: _game!.layersOverlayBuilder,
          },
        ),
        Positioned(
          right: 8,
          top: 8,
          child: GestureDetector(
              child: const Icon(Icons.layers),
              onTapDown: (TapDownDetails details) {
                _game!.showLayers(Vector2(
                    details.localPosition.dx, details.localPosition.dy));
              }),
        ),
      ],
    );

    ;
  }
}
