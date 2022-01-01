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

class BinkyNetObjectSettings extends StatelessWidget {
  const BinkyNetObjectSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorContext>(builder: (context, editorCtx, child) {
      final selector = editorCtx.selector;
      return Consumer<ModelModel>(builder: (context, model, child) {
        final lwId = selector.idOf(EntityType.binkynetlocalworker) ?? "";
        final objId = selector.idOf(EntityType.binkynetobject) ?? "";
        return FutureBuilder<BinkyNetLocalWorker>(
            future: model.getBinkyNetLocalWorker(lwId),
            initialData: model.getCachedBinkyNetLocalWorker(lwId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var binkynetlocalworker = snapshot.data!;
              final binkynetobject =
                  binkynetlocalworker.objects.singleWhere((x) => x.id == objId);
              return _BinkyNetObjectSettings(
                  editorCtx: editorCtx,
                  model: model,
                  binkynetlocalworker: binkynetlocalworker,
                  binkynetobject: binkynetobject);
            });
      });
    });
  }
}

class _BinkyNetObjectSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final BinkyNetLocalWorker binkynetlocalworker;
  final BinkyNetObject binkynetobject;
  const _BinkyNetObjectSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.binkynetlocalworker,
      required this.binkynetobject})
      : super(key: key);

  @override
  State<_BinkyNetObjectSettings> createState() =>
      _BinkyNetObjectSettingsState();
}

class _BinkyNetObjectSettingsState extends State<_BinkyNetObjectSettings> {
  final TextEditingController _objectIdController = TextEditingController();

  void _initConrollers() {
    _objectIdController.text = widget.binkynetobject.objectId;
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _BinkyNetObjectSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        Text(widget.binkynetobject.id),
        SettingsTextField(
            controller: _objectIdController,
            label: "Object ID",
            onLostFocus: (value) async {
              await _update((update) => {update.objectId = value});
            }),
      ],
    );
  }

  Future<void> _update(Function(BinkyNetObject) editor) async {
    final lw = await widget.model
        .getBinkyNetLocalWorker(widget.binkynetlocalworker.id);
    var update = lw.deepCopy();
    editor(update.objects.singleWhere((x) => x.id == widget.binkynetobject.id));
    widget.model.updateBinkyNetLocalWorker(update);
  }
}
