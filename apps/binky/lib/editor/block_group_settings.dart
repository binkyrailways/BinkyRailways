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
        final blockGroupId = selector.idOf(EntityType.blockgroup) ?? "";
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
  final TextEditingController _minimumLocsInGroupController =
      TextEditingController();
  final TextEditingController _minimumLocsOnTrackController =
      TextEditingController();
  final NumericValidator _minimumLocsInGroupValidator =
      NumericValidator(minimum: 0, maximum: 128);
  final NumericValidator _minimumLocsOnTrackValidator =
      NumericValidator(minimum: 0, maximum: 128);

  void _initControllers() {
    _descriptionController.text = widget.blockGroup.description;
    _minimumLocsInGroupController.text =
        widget.blockGroup.minimumLocsInGroup.toString();
    _minimumLocsOnTrackController.text =
        widget.blockGroup.minimumLocsOnTrack.toString();
  }

  @override
  void initState() {
    super.initState();
    _initControllers();
  }

  @override
  void didUpdateWidget(covariant _BlockGroupSettings oldWidget) {
    _initControllers();
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
        SettingsTextField(
            controller: _minimumLocsInGroupController,
            validator: _minimumLocsInGroupValidator.validate,
            label: "Min #locs that must be present in this group.",
            onLostFocus: (value) async {
              await _update((update) {
                update.minimumLocsInGroup = int.parse(value);
              });
            }),
        SettingsTextField(
            controller: _minimumLocsOnTrackController,
            validator: _minimumLocsOnTrackValidator.validate,
            label: "Min #locs on the track to enforce group effect",
            onLostFocus: (value) async {
              await _update((update) {
                update.minimumLocsOnTrack = int.parse(value);
              });
            }),
      ],
    );
  }

  Future<void> _update(void Function(BlockGroup) editor) async {
    final current = await widget.model.getBlockGroup(widget.blockGroup.id);
    var update = current.deepCopy();
    editor(update);
    await widget.model.updateBlockGroup(update);
  }
}
