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
import 'package:binky/editor/editor_context.dart';

class BlockGroupSettings extends StatelessWidget {
  const BlockGroupSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorContext>(builder: (context, editorCtx, child) {
      final selector = editorCtx.selector;
      return Consumer<ModelModel>(builder: (context, model, child) {
        final blockGroupId = selector.id ?? "";
        return FutureBuilder<BlockGroup>(
            future: model.getBlockGroup(blockGroupId),
            initialData: model.getCachedBlockGroup(blockGroupId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var blockGroup = snapshot.data!;
              return _BlockGroupSettings(
                  editorCtx: editorCtx, model: model, blockGroup: blockGroup);
            });
      });
    });
  }
}

class _BlockGroupSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final BlockGroup blockGroup;
  const _BlockGroupSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.blockGroup})
      : super(key: key);

  @override
  State<_BlockGroupSettings> createState() => _BlockGroupSettingsState();
}

class _BlockGroupSettingsState extends State<_BlockGroupSettings> {
  final TextEditingController _descriptionController = TextEditingController();

  void _initConrollers() {
    _descriptionController.text = widget.blockGroup.description;
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _BlockGroupSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        Text(widget.blockGroup.id),
        SettingsTextField(
            controller: _descriptionController,
            label: "Description",
            firstChild: true,
            onLostFocus: (value) async {
              final blockGroup =
                  await widget.model.getBlockGroup(widget.blockGroup.id);
              var update = blockGroup.deepCopy()..description = value;
              widget.model.updateBlockGroup(update);
            }),
      ],
    );
  }
}
