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

class BinkyNetDeviceSettings extends StatelessWidget {
  const BinkyNetDeviceSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorContext>(builder: (context, editorCtx, child) {
      final selector = editorCtx.selector;
      return Consumer<ModelModel>(builder: (context, model, child) {
        final lwId = selector.idOf(EntityType.binkynetlocalworker) ?? "";
        final devId = selector.idOf(EntityType.binkynetdevice) ?? "";
        return FutureBuilder<BinkyNetLocalWorker>(
            future: model.getBinkyNetLocalWorker(lwId),
            initialData: model.getCachedBinkyNetLocalWorker(lwId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var binkynetlocalworker = snapshot.data!;
              final binkynetdevice =
                  binkynetlocalworker.devices.singleWhere((x) => x.id == devId);
              return _BinkyNetDeviceSettings(
                  editorCtx: editorCtx,
                  model: model,
                  binkynetlocalworker: binkynetlocalworker,
                  binkynetdevice: binkynetdevice);
            });
      });
    });
  }
}

class _BinkyNetDeviceSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final BinkyNetLocalWorker binkynetlocalworker;
  final BinkyNetDevice binkynetdevice;
  const _BinkyNetDeviceSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.binkynetlocalworker,
      required this.binkynetdevice})
      : super(key: key);

  @override
  State<_BinkyNetDeviceSettings> createState() =>
      _BinkyNetDeviceSettingsState();
}

class _BinkyNetDeviceSettingsState extends State<_BinkyNetDeviceSettings> {
  final TextEditingController _deviceIdController = TextEditingController();
  final TextEditingController _addressController = TextEditingController();

  void _initConrollers() {
    _deviceIdController.text = widget.binkynetdevice.deviceId;
    _addressController.text = widget.binkynetdevice.address;
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _BinkyNetDeviceSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        Text(widget.binkynetdevice.id),
        SettingsTextField(
            controller: _deviceIdController,
            label: "Device ID",
            onLostFocus: (value) async {
              await _update((update) => {update.deviceId = value});
            }),
        SettingsDropdownField<BinkyNetDeviceType>(
          label: "Device type",
          value: widget.binkynetdevice.deviceType,
          onChanged: (value) {
            _update((x) {
              if (value != null) {
                x.deviceType = value;
              }
            });
          },
          items: _deviceTypeItems,
        ),
        SettingsTextField(
            controller: _addressController,
            label: "Address",
            onLostFocus: (value) async {
              await _update((update) => {update.address = value});
            }),
        SettingsCheckBoxField(
          label: "Disabled",
          value: widget.binkynetdevice.disabled,
          onChanged: (value) async {
            await _update((update) {
              update.disabled = value;
            });
          },
        ),
      ],
    );
  }

  Future<void> _update(Function(BinkyNetDevice) editor) async {
    final lw = await widget.model
        .getBinkyNetLocalWorker(widget.binkynetlocalworker.id);
    var update = lw.deepCopy();
    editor(update.devices.singleWhere((x) => x.id == widget.binkynetdevice.id));
    widget.model.updateBinkyNetLocalWorker(update);
  }

  static final List<DropdownMenuItem<BinkyNetDeviceType>> _deviceTypeItems =
      BinkyNetDeviceType.values
          .map((e) => DropdownMenuItem<BinkyNetDeviceType>(
                child: Text(e.name),
                value: e,
              ))
          .toList();
}
