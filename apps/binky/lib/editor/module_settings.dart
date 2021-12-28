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

import 'package:binky/models/model_model.dart';
import 'package:binky/api/generated/br_model_types.pb.dart';
import 'package:binky/editor/editor_context.dart';

class ModuleSettings extends StatelessWidget {
  const ModuleSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(builder: (context, model, child) {
      final moduleId = selector.moduleId ?? "";
      return FutureBuilder<Module>(
          future: model.getModule(moduleId),
          initialData: model.getCachedModule(moduleId),
          builder: (context, snapshot) {
            if (!snapshot.hasData) {
              return const Center(child: CircularProgressIndicator());
            }
            var module = snapshot.data!;
            return _ModuleSettings(model: model, module: module);
          });
    });
  }
}

class _ModuleSettings extends StatefulWidget {
  final ModelModel model;
  final Module module;
  const _ModuleSettings({Key? key, required this.model, required this.module})
      : super(key: key);

  @override
  State<_ModuleSettings> createState() => _ModuleSettingsState();
}

class _ModuleSettingsState extends State<_ModuleSettings> {
  final TextEditingController _descriptionController = TextEditingController();

  @override
  void initState() {
    super.initState();
    _descriptionController.text = widget.module.description;
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        Container(
          padding: const EdgeInsets.all(8),
          child: Focus(
            child: TextField(
              controller: _descriptionController,
              decoration: const InputDecoration(
                border: UnderlineInputBorder(),
                label: Text("Description"),
              ),
            ),
            onFocusChange: (bool hasFocus) async {
              if (!hasFocus) {
                final module = await widget.model.getModule(widget.module.id);
                var update = module.deepCopy()
                  ..description = _descriptionController.text;
                widget.model.updateModule(update);
              }
            },
          ),
        ),
      ],
    );
  }
}
