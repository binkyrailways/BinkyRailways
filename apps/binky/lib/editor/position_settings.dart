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

import 'package:flutter/material.dart';

import '../components.dart';
import '../models.dart';
import '../api.dart';
import 'package:binky/editor/editor_context.dart';

class PositionSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final Position position;
  final String moduleId;
  final Future<void> Function(void Function(Position)) update;

  const PositionSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.position,
      required this.moduleId,
      required this.update})
      : super(key: key);

  @override
  State<PositionSettings> createState() => _PositionSettingsState();
}

class _PositionSettingsState extends State<PositionSettings> {
  final TextEditingController _xController = TextEditingController();
  final TextEditingController _yController = TextEditingController();
  final TextEditingController _wController = TextEditingController();
  final TextEditingController _hController = TextEditingController();
  final TextEditingController _rotationController = TextEditingController();
  final TextEditingController _layerController = TextEditingController();
  final NumericValidator _xywhValidator =
      NumericValidator(minimum: 0, maximum: 100000);
  final NumericValidator _rotationValidator =
      NumericValidator(minimum: -360, maximum: 360);

  void _initConrollers() {
    _xController.text = widget.position.x.toString();
    _yController.text = widget.position.y.toString();
    _wController.text = widget.position.width.toString();
    _hController.text = widget.position.height.toString();
    _rotationController.text = widget.position.rotation.toString();
    _layerController.text = widget.position.layer;
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant PositionSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        Row(children: [
          Expanded(
              child: SettingsTextField(
                  controller: _xController,
                  label: "X",
                  validator: _xywhValidator.validate,
                  onLostFocus: (value) async {
                    await widget.update((update) {
                      update.x = int.parse(value);
                    });
                  })),
          Expanded(
              child: SettingsTextField(
                  controller: _yController,
                  label: "Y",
                  validator: _xywhValidator.validate,
                  onLostFocus: (value) async {
                    await widget.update((update) {
                      update.y = int.parse(value);
                    });
                  })),
        ]),
        Row(children: [
          Expanded(
              child: SettingsTextField(
                  controller: _wController,
                  label: "Width",
                  validator: _xywhValidator.validate,
                  onLostFocus: (value) async {
                    await widget.update((update) {
                      update.width = int.parse(value);
                    });
                  })),
          Expanded(
              child: SettingsTextField(
                  controller: _hController,
                  label: "Height",
                  validator: _xywhValidator.validate,
                  onLostFocus: (value) async {
                    await widget.update((update) {
                      update.height = int.parse(value);
                    });
                  })),
        ]),
        Row(children: [
          Expanded(
              child: SettingsTextField(
                  controller: _rotationController,
                  label: "rotation",
                  validator: _rotationValidator.validate,
                  onLostFocus: (value) async {
                    await widget.update((update) {
                      update.rotation = int.parse(value);
                    });
                  })),
          Expanded(
              child: SettingsTextField(
            controller: _layerController,
            label: "Layer",
            onLostFocus: (value) async {
              await widget.update((update) {
                update.layer = value;
              });
            },
            suffix: GestureDetector(
              child: const Icon(Icons.arrow_drop_down),
              onTapDown: (TapDownDetails details) async {
                final layers = await _layers();
                final items = layers
                    .map(
                      (e) => PopupMenuItem<String>(
                        child: Text(e),
                        onTap: () async {
                          await widget.update((update) {
                            update.layer = e;
                          });
                        },
                      ),
                    )
                    .toList();
                showMenu(
                  context: context,
                  useRootNavigator: true,
                  position: RelativeRect.fromLTRB(
                      details.globalPosition.dx,
                      details.globalPosition.dy,
                      details.globalPosition.dx,
                      details.globalPosition.dy),
                  items: items,
                  elevation: 8.0,
                );
              },
            ),
          )),
        ]),
      ],
    );
  }

  Future<List<String>> _layers() async {
    final mod = await widget.model.getModule(widget.moduleId);
    return mod.layers;
  }
}
