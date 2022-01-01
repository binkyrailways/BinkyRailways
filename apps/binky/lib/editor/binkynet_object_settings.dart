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
    final List<Widget> children = [
      const SettingsHeader(title: "General"),
      SettingsTextField(
          controller: _objectIdController,
          label: "Object ID",
          onLostFocus: (value) async {
            await _update((update) => {update.objectId = value});
          }),
      SettingsDropdownField<BinkyNetObjectType>(
        label: "Object type",
        value: widget.binkynetobject.objectType,
        onChanged: (value) {
          _update((x) {
            if (value != null) {
              x.objectType = value;
            }
          });
        },
        items: _objectTypeItems,
      ),
    ];
    final connections = widget.binkynetobject.connections;
    for (var i = 0; i < connections.length; i++) {
      children.add(_BinkyNetConnectionSettings(
          editorCtx: widget.editorCtx,
          model: widget.model,
          binkynetlocalworker: widget.binkynetlocalworker,
          binkynetobject: widget.binkynetobject,
          binkynetconnection: connections[i],
          update: _update));
    }
    return ScrollableForm(
        child: Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: children,
    ));
  }

  Future<void> _update(void Function(BinkyNetObject) editor) async {
    final lw = await widget.model
        .getBinkyNetLocalWorker(widget.binkynetlocalworker.id);
    var update = lw.deepCopy();
    editor(update.objects.singleWhere((x) => x.id == widget.binkynetobject.id));
    widget.model.updateBinkyNetLocalWorker(update);
  }

  static final List<DropdownMenuItem<BinkyNetObjectType>> _objectTypeItems =
      BinkyNetObjectType.values
          .map((e) => DropdownMenuItem<BinkyNetObjectType>(
                child: Text(e.name),
                value: e,
              ))
          .toList();
}

class _BinkyNetConnectionSettings extends StatelessWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final BinkyNetLocalWorker binkynetlocalworker;
  final BinkyNetObject binkynetobject;
  final BinkyNetConnection binkynetconnection;
  final Future<void> Function(void Function(BinkyNetObject)) update;

  const _BinkyNetConnectionSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.binkynetlocalworker,
      required this.binkynetobject,
      required this.binkynetconnection,
      required this.update})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    final List<Widget> children = [
      SettingsHeader(title: "Connection: ${binkynetconnection.key}"),
    ];
    final pins = binkynetconnection.pins;
    for (var i = 0; i < pins.length; i++) {
      children.add(_BinkyNetDevicePinSettings(
          binkynetlocalworker: binkynetlocalworker,
          binkynetconnection: binkynetconnection,
          binkynetdevicepin: pins[i],
          binkynetdevicepinIndex: i,
          update: _update));
    }
    final configuration = binkynetconnection.configuration;
    configuration.forEach((key, value) {
      children.add(_BinkyNetConfigKeyValueSettings(
          binkynetconnection: binkynetconnection,
          configKey: key,
          update: _update));
    });
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: children,
    );
  }

  Future<void> _update(void Function(BinkyNetConnection) editor) async {
    await update((update) {
      editor(update.connections
          .singleWhere((x) => x.key == binkynetconnection.key));
    });
  }
}

class _BinkyNetDevicePinSettings extends StatefulWidget {
  final BinkyNetLocalWorker binkynetlocalworker;
  final BinkyNetConnection binkynetconnection;
  final BinkyNetDevicePin binkynetdevicepin;
  final int binkynetdevicepinIndex;
  final Future<void> Function(void Function(BinkyNetConnection)) update;

  const _BinkyNetDevicePinSettings(
      {Key? key,
      required this.binkynetlocalworker,
      required this.binkynetconnection,
      required this.binkynetdevicepin,
      required this.binkynetdevicepinIndex,
      required this.update})
      : super(key: key);

  @override
  State<_BinkyNetDevicePinSettings> createState() =>
      _BinkyNetDevicePinSettingsState();
}

class _BinkyNetDevicePinSettingsState
    extends State<_BinkyNetDevicePinSettings> {
  final TextEditingController _indexController = TextEditingController();
  final NumericValidator _indexValidator =
      NumericValidator(minimum: 1, maximum: 16);

  void _initConrollers() {
    _indexController.text = widget.binkynetdevicepin.index.toString();
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _BinkyNetDevicePinSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    final List<Widget> children = [
      SettingsTextField(
          controller: _indexController,
          label: "Pin",
          validator: _indexValidator.validate,
          onLostFocus: (value) async {
            await _update((update) => {update.index = int.parse(value)});
          }),
      SettingsDropdownField<String>(
        label: "Device ID",
        value: widget.binkynetdevicepin.deviceId,
        onChanged: (value) {
          _update((x) {
            if (value != null) {
              x.deviceId = value;
            }
          });
        },
        items: _deviceIds(),
      ),
    ];
    return Column(
      children: children,
    );
  }

  Future<void> _update(void Function(BinkyNetDevicePin) editor) async {
    await widget.update((update) {
      editor(update.pins[widget.binkynetdevicepinIndex]);
    });
  }

  List<DropdownMenuItem<String>> _deviceIds() {
    return widget.binkynetlocalworker.devices
        .map((e) => DropdownMenuItem<String>(
              child: Text(e.deviceId),
              value: e.deviceId,
            ))
        .toList();
  }
}

class _BinkyNetConfigKeyValueSettings extends StatefulWidget {
  final BinkyNetConnection binkynetconnection;
  final String configKey;
  final Future<void> Function(void Function(BinkyNetConnection)) update;

  const _BinkyNetConfigKeyValueSettings(
      {Key? key,
      required this.binkynetconnection,
      required this.configKey,
      required this.update})
      : super(key: key);

  @override
  State<_BinkyNetConfigKeyValueSettings> createState() =>
      _BinkyNetConfigKeyValueSettingsState();
}

class _BinkyNetConfigKeyValueSettingsState
    extends State<_BinkyNetConfigKeyValueSettings> {
  final TextEditingController _valueController = TextEditingController();

  void _initConrollers() {
    _valueController.text =
        widget.binkynetconnection.configuration[widget.configKey] ?? "";
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _BinkyNetConfigKeyValueSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        SettingsTextField(
            controller: _valueController,
            label: widget.configKey,
            onLostFocus: (value) async {
              await widget.update(
                  (update) => {update.configuration[widget.configKey] = value});
            }),
      ],
    );
  }
}
