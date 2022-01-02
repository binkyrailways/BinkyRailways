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
import 'package:protobuf/protobuf.dart';
import 'package:provider/provider.dart';

import '../components.dart';
import '../models.dart';
import '../api.dart';
import './editor_context.dart';
import './position_settings.dart';

class OutputSettings extends StatelessWidget {
  const OutputSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorContext>(builder: (context, editorCtx, child) {
      final selector = editorCtx.selector;
      return Consumer<ModelModel>(builder: (context, model, child) {
        final outputId = selector.idOf(EntityType.output) ?? "";
        return FutureBuilder<Output>(
            future: model.getOutput(outputId),
            initialData: model.getCachedOutput(outputId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var output = snapshot.data!;
              return _OutputSettings(
                  editorCtx: editorCtx, model: model, output: output);
            });
      });
    });
  }
}

class _OutputSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final Output output;
  const _OutputSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.output})
      : super(key: key);

  @override
  State<_OutputSettings> createState() => _OutputSettingsState();
}

class _OutputSettingsState extends State<_OutputSettings> {
  final TextEditingController _descriptionController = TextEditingController();

  void _initConrollers() {
    _descriptionController.text = widget.output.description;
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _OutputSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        const SettingsHeader(title: "General"),
        SettingsTextField(
            controller: _descriptionController,
            label: "Description",
            firstChild: true,
            onLostFocus: (value) async {
              await _update((update) {
                update.description = value;
              });
            }),
        const SettingsHeader(title: "Position"),
        PositionSettings(
            editorCtx: widget.editorCtx,
            model: widget.model,
            position: widget.output.position,
            update: (editor) async {
              await _update((update) {
                editor(update.position);
              });
            }),
      ],
    );
  }

  Future<void> _update(void Function(Output) editor) async {
    final block = await widget.model.getOutput(widget.output.id);
    var update = block.deepCopy();
    editor(update);
    await widget.model.updateOutput(update);
  }
}
