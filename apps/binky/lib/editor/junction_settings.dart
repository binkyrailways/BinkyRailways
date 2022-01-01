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

class JunctionSettings extends StatelessWidget {
  const JunctionSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorContext>(builder: (context, editorCtx, child) {
      final selector = editorCtx.selector;
      return Consumer<ModelModel>(builder: (context, model, child) {
        final junctionId = selector.idOf(EntityType.junction) ?? "";
        return FutureBuilder<Junction>(
            future: model.getJunction(junctionId),
            initialData: model.getCachedJunction(junctionId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var junction = snapshot.data!;
              return _JunctionSettings(
                  editorCtx: editorCtx, model: model, junction: junction);
            });
      });
    });
  }
}

class _JunctionSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final Junction junction;
  const _JunctionSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.junction})
      : super(key: key);

  @override
  State<_JunctionSettings> createState() => _JunctionSettingsState();
}

class _JunctionSettingsState extends State<_JunctionSettings> {
  final TextEditingController _descriptionController = TextEditingController();

  void _initConrollers() {
    _descriptionController.text = widget.junction.description;
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _JunctionSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        Text(widget.junction.id),
        SettingsTextField(
            controller: _descriptionController,
            label: "Description",
            firstChild: true,
            onLostFocus: (value) async {
              final junction =
                  await widget.model.getJunction(widget.junction.id);
              var update = junction.deepCopy()..description = value;
              widget.model.updateJunction(update);
            }),
      ],
    );
  }
}
