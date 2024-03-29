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

class SignalSettings extends StatelessWidget {
  const SignalSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorContext>(builder: (context, editorCtx, child) {
      final selector = editorCtx.selector;
      return Consumer<ModelModel>(builder: (context, model, child) {
        final signalId = selector.idOf(EntityType.signal) ?? "";
        return FutureBuilder<Signal>(
            future: model.getSignal(signalId),
            initialData: model.getCachedSignal(signalId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var signal = snapshot.data!;
              return _SignalSettings(
                  editorCtx: editorCtx, model: model, signal: signal);
            });
      });
    });
  }
}

class _SignalSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final Signal signal;
  const _SignalSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.signal})
      : super(key: key);

  @override
  State<_SignalSettings> createState() => _SignalSettingsState();
}

class _SignalSettingsState extends State<_SignalSettings> {
  final TextEditingController _descriptionController = TextEditingController();

  void _initConrollers() {
    _descriptionController.text = widget.signal.description;
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _SignalSettings oldWidget) {
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
            position: widget.signal.position,
            moduleId: widget.signal.moduleId,
            update: (editor) async {
              await _update((update) {
                editor(update.position);
              });
            }),
      ],
    );
  }

  Future<void> _update(void Function(Signal) editor) async {
    final current = await widget.model.getSignal(widget.signal.id);
    var update = current.deepCopy();
    editor(update);
    await widget.model.updateSignal(update);
  }
}
