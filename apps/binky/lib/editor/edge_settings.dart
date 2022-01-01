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

class EdgeSettings extends StatelessWidget {
  const EdgeSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorContext>(builder: (context, editorCtx, child) {
      final selector = editorCtx.selector;
      return Consumer<ModelModel>(builder: (context, model, child) {
        final edgeId = selector.idOf(EntityType.edge) ?? "";
        return FutureBuilder<Edge>(
            future: model.getEdge(edgeId),
            initialData: model.getCachedEdge(edgeId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var edge = snapshot.data!;
              return _EdgeSettings(
                  editorCtx: editorCtx, model: model, edge: edge);
            });
      });
    });
  }
}

class _EdgeSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final Edge edge;
  const _EdgeSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.edge})
      : super(key: key);

  @override
  State<_EdgeSettings> createState() => _EdgeSettingsState();
}

class _EdgeSettingsState extends State<_EdgeSettings> {
  final TextEditingController _descriptionController = TextEditingController();

  void _initConrollers() {
    _descriptionController.text = widget.edge.description;
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _EdgeSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        Text(widget.edge.id),
        SettingsTextField(
            controller: _descriptionController,
            label: "Description",
            firstChild: true,
            onLostFocus: (value) async {
              final edge = await widget.model.getEdge(widget.edge.id);
              var update = edge.deepCopy()..description = value;
              widget.model.updateEdge(update);
            }),
      ],
    );
  }
}
