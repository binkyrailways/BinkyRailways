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
import 'package:file_picker/file_picker.dart';

import '../components.dart';
import '../models.dart';
import '../api.dart' hide Image;
import 'package:binky/editor/editor_context.dart';

class ModuleSettings extends StatelessWidget {
  const ModuleSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(builder: (context, model, child) {
      final moduleId = selector.idOf(EntityType.module) ?? "";
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

  _initControllers() {
    _descriptionController.text = widget.module.description;
  }

  @override
  void initState() {
    super.initState();
    _initControllers();
  }

  @override
  void didUpdateWidget(covariant _ModuleSettings oldWidget) {
    _initControllers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    final hasImage = widget.module.backgroundImageUrl.isNotEmpty;
    return Column(
      children: <Widget>[
        SettingsTextField(
            controller: _descriptionController,
            label: "Description",
            firstChild: true,
            onLostFocus: (value) async {
              final module = await widget.model.getModule(widget.module.id);
              var update = module.deepCopy()..description = value;
              widget.model.updateModule(update);
            }),
        Row(
          children: [
            const SettingsHeader(title: "Image"),
            Expanded(child: Container()),
            IconButton(
              onPressed: () async {
                final result = await FilePicker.platform
                    .pickFiles(type: FileType.image, withData: true);
                if (result != null) {
                  final data = result.files.single.bytes;
                  if (data != null) {
                    await widget.model.updateModuleBackgroundImage(
                        widget.module, data.toList());
                  }
                }
              },
              icon: const Icon(Icons.file_upload),
            ),
          ],
        ),
        hasImage
            ? Image.network(widget.module.backgroundImageUrl)
            : Container(),
      ],
    );
  }
}
