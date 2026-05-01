// Copyright 2024 Ewout Prangsma
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

class RailPointSettings extends StatelessWidget {
  const RailPointSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorContext>(builder: (context, editorCtx, child) {
      final selector = editorCtx.selector;
      return Consumer<ModelModel>(builder: (context, model, child) {
        final railPointId = selector.idOf(EntityType.railpoint) ?? "";
        return FutureBuilder<RailPoint>(
            future: model.getRailPoint(railPointId),
            initialData: model.getCachedRailPoint(railPointId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var railPoint = snapshot.data!;
              return _RailPointSettings(
                  editorCtx: editorCtx, model: model, railPoint: railPoint);
            });
      });
    });
  }
}

class _RailPointSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final RailPoint railPoint;
  const _RailPointSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.railPoint})
      : super(key: key);

  @override
  State<_RailPointSettings> createState() => _RailPointSettingsState();
}

class _RailPointSettingsState extends State<_RailPointSettings> {
  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        const SettingsHeader(title: "Rail Point"),
        const SettingsHeader(title: "Position"),
        PositionSettings(
            editorCtx: widget.editorCtx,
            model: widget.model,
            position: widget.railPoint.position,
            moduleId: widget.railPoint.moduleId,
            update: (editor) async {
              await _update((update) {
                editor(update.position);
              });
            }),
        const Spacer(),
        Padding(
          padding: const EdgeInsets.all(8.0),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              IconButton(
                icon: const Icon(Icons.delete),
                tooltip: "Remove rail point",
                onPressed: () async {
                  await widget.model.deleteRailPoint(widget.railPoint);
                  widget.editorCtx.select(EntitySelector.module(null, widget.railPoint.moduleId));
                },
              ),
            ],
          ),
        ),
      ],
    );
  }

  Future<void> _update(void Function(RailPoint) editor) async {
    final current = await widget.model.getRailPoint(widget.railPoint.id);
    var update = current.deepCopy();
    editor(update);
    await widget.model.updateRailPoint(update);
  }
}
