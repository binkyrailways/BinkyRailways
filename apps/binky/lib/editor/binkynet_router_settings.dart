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

import 'package:flutter/material.dart' hide Route;
import 'package:protobuf/protobuf.dart';
import 'package:provider/provider.dart';

import '../components.dart';
import '../models.dart';
import '../api.dart';
import 'package:binky/editor/editor_context.dart';

class BinkyNetRouterSettings extends StatelessWidget {
  const BinkyNetRouterSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorContext>(builder: (context, editorCtx, child) {
      final selector = editorCtx.selector;
      return Consumer<ModelModel>(builder: (context, model, child) {
        final lwId = selector.idOf(EntityType.binkynetlocalworker) ?? "";
        final routerId = selector.idOf(EntityType.binkynetrouter) ?? "";
        return FutureBuilder<BinkyNetLocalWorker>(
            future: model.getBinkyNetLocalWorker(lwId),
            initialData: model.getCachedBinkyNetLocalWorker(lwId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var binkynetlocalworker = snapshot.data!;
              final binkynetrouter = binkynetlocalworker.routers
                  .singleWhere((x) => x.id == routerId);
              return _BinkyNetRouterSettings(
                  editorCtx: editorCtx,
                  model: model,
                  binkynetlocalworker: binkynetlocalworker,
                  binkynetrouter: binkynetrouter);
            });
      });
    });
  }
}

class _BinkyNetRouterSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final BinkyNetLocalWorker binkynetlocalworker;
  final BinkyNetRouter binkynetrouter;
  const _BinkyNetRouterSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.binkynetlocalworker,
      required this.binkynetrouter})
      : super(key: key);

  @override
  State<_BinkyNetRouterSettings> createState() =>
      _BinkyNetRouterSettingsState();
}

class _BinkyNetRouterSettingsState extends State<_BinkyNetRouterSettings> {
  final TextEditingController _descriptionController = TextEditingController();

  void _initControllers() {
    _descriptionController.text = widget.binkynetrouter.description;
  }

  @override
  void initState() {
    super.initState();
    _initControllers();
  }

  @override
  void didUpdateWidget(covariant _BinkyNetRouterSettings oldWidget) {
    _initControllers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        Text(widget.binkynetrouter.id),
        SettingsTextField(
            controller: _descriptionController,
            label: "Name",
            onLostFocus: (value) async {
              await _update((update) => {update.description = value});
            }),
        SettingsHeader(title: "Validation"),
        SettingsFindingsField(value: widget.binkynetrouter.validationFindings),
      ],
    );
  }

  Future<void> _update(Function(BinkyNetRouter) editor) async {
    final lw = await widget.model
        .getBinkyNetLocalWorker(widget.binkynetlocalworker.id);
    var update = lw.deepCopy();
    editor(update.routers.singleWhere((x) => x.id == widget.binkynetrouter.id));
    widget.model.updateBinkyNetLocalWorker(update);
  }
}
