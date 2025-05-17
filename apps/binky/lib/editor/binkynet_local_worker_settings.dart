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

class BinkyNetLocalWorkerSettings extends StatelessWidget {
  const BinkyNetLocalWorkerSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorContext>(builder: (context, editorCtx, child) {
      final selector = editorCtx.selector;
      return Consumer<ModelModel>(builder: (context, model, child) {
        final lwId = selector.idOf(EntityType.binkynetlocalworker) ?? "";
        return FutureBuilder<BinkyNetLocalWorker>(
            future: model.getBinkyNetLocalWorker(lwId),
            initialData: model.getCachedBinkyNetLocalWorker(lwId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var binkynetlocalworker = snapshot.data!;
              return _BinkyNetLocalWorkerSettings(
                  editorCtx: editorCtx,
                  model: model,
                  binkynetlocalworker: binkynetlocalworker);
            });
      });
    });
  }
}

class _BinkyNetLocalWorkerSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final BinkyNetLocalWorker binkynetlocalworker;
  const _BinkyNetLocalWorkerSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.binkynetlocalworker})
      : super(key: key);

  @override
  State<_BinkyNetLocalWorkerSettings> createState() =>
      _BinkyNetLocalWorkerSettingsState();
}

class _BinkyNetLocalWorkerSettingsState
    extends State<_BinkyNetLocalWorkerSettings> {
  final TextEditingController _hardwareIdController = TextEditingController();
  final TextEditingController _aliasController = TextEditingController();

  void _initConrollers() {
    _hardwareIdController.text = widget.binkynetlocalworker.hardwareId;
    _aliasController.text = widget.binkynetlocalworker.alias;
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _BinkyNetLocalWorkerSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        Text(widget.binkynetlocalworker.id),
        SettingsTextField(
            controller: _hardwareIdController,
            label: "Hardware ID",
            onLostFocus: (value) async {
              await _update((update) => {update.hardwareId = value});
            }),
        SettingsTextField(
            controller: _aliasController,
            label: "Alias",
            onLostFocus: (value) async {
              await _update((update) => {update.alias = value});
            }),
        SettingsCheckBoxField(
          label: "Is Virtual",
          value: widget.binkynetlocalworker.isVirtual,
          onChanged: (value) async {
            await _update((update) {
              update.isVirtual = value;
            });
          },
        ),
      ],
    );
  }

  Future<void> _update(Function(BinkyNetLocalWorker) editor) async {
    final lw = await widget.model
        .getBinkyNetLocalWorker(widget.binkynetlocalworker.id);
    var update = lw.deepCopy();
    editor(update);
    widget.model.updateBinkyNetLocalWorker(update);
  }
}
