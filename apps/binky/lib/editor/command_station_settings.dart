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

import 'package:binky/editor/binkynet_local_workers_tree.dart';
import 'package:flutter/material.dart';
import 'package:protobuf/protobuf.dart';
import 'package:provider/provider.dart';

import '../components.dart';
import '../models.dart';
import '../api.dart';
import 'package:binky/editor/editor_context.dart';

class CommandStationSettings extends StatelessWidget {
  const CommandStationSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(builder: (context, model, child) {
      final csId = selector.idOf(EntityType.commandstation) ?? "";
      return FutureBuilder<CommandStation>(
          future: model.getCommandStation(csId),
          initialData: model.getCachedCommandStation(csId),
          builder: (context, snapshot) {
            if (snapshot.hasError) {
              return ErrorMessage(
                  title: "Failed to load CommandStation",
                  error: snapshot.error);
            } else if (!snapshot.hasData) {
              return const Center(child: CircularProgressIndicator());
            }
            var cs = snapshot.data!;
            return _CommandStationSettings(model: model, commandStation: cs);
          });
    });
  }
}

class _CommandStationSettings extends StatefulWidget {
  final ModelModel model;
  final CommandStation commandStation;
  const _CommandStationSettings(
      {Key? key, required this.model, required this.commandStation})
      : super(key: key);

  @override
  State<_CommandStationSettings> createState() =>
      _CommandStationSettingsState();
}

class _CommandStationSettingsState extends State<_CommandStationSettings> {
  final TextEditingController _addressSpacesController =
      TextEditingController();
  final TextEditingController _descriptionController = TextEditingController();
  final TextEditingController _serverHostController = TextEditingController();
  final TextEditingController _domainController = TextEditingController();
  final TextEditingController _grpcPortController = TextEditingController();
  final TextEditingController _serialPortController = TextEditingController();
  final TextEditingController _requiredVersionController =
      TextEditingController();
  final NumericValidator _grpcPortValidator =
      NumericValidator(minimum: 1, maximum: 32000);

  _initControllers() {
    final cs = widget.commandStation;
    _descriptionController.text = cs.description;
    _addressSpacesController.text = cs.addressSpaces.join(",");
    if (cs.hasBinkynetCommandStation()) {
      final bnCs = cs.binkynetCommandStation;
      _serverHostController.text = bnCs.serverHost;
      _domainController.text = bnCs.domain;
      _grpcPortController.text = bnCs.grpcPort.toString();
      _requiredVersionController.text = bnCs.requiredWorkerVersion;
    }
    if (cs.hasBidibCommandStation()) {
      final bdCs = cs.bidibCommandStation;
      _serialPortController.text = bdCs.serialPortName;
    }
  }

  @override
  void initState() {
    super.initState();
    _initControllers();
  }

  @override
  void dispose() {
    _addressSpacesController.dispose();
    _descriptionController.dispose();
    _serverHostController.dispose();
    _domainController.dispose();
    _grpcPortController.dispose();
    _serialPortController.dispose();
    _requiredVersionController.dispose();
    super.dispose();
  }

  @override
  void didUpdateWidget(covariant _CommandStationSettings oldWidget) {
    _initControllers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    final cs = widget.commandStation;
    final List<Widget> children = [
      const SettingsHeader(title: "General"),
      SettingsTextField(
          controller: _descriptionController,
          label: "Description",
          firstChild: true,
          onLostFocus: (value) async {
            await _update((update) => {update.description = value});
          }),
    ];
    if (cs.hasBinkynetCommandStation()) {
      children.add(const SettingsHeader(title: "BinkyNet"));
      children.add(SettingsTextField(
          controller: _serverHostController,
          label: "Server host",
          onLostFocus: (value) async {
            await _update(
                (update) => {update.binkynetCommandStation.serverHost = value});
          }));
      children.add(SettingsTextField(
          controller: _grpcPortController,
          label: "GRPC port",
          validator: _grpcPortValidator.validate,
          onLostFocus: (value) async {
            await _update((update) =>
                {update.binkynetCommandStation.grpcPort = int.parse(value)});
          }));
      children.add(SettingsTextField(
          controller: _domainController,
          label: "Domain",
          onLostFocus: (value) async {
            await _update(
                (update) => {update.binkynetCommandStation.domain = value});
          }));
      children.add(SettingsTextField(
          controller: _requiredVersionController,
          label: "Required version",
          onLostFocus: (value) async {
            await _update((update) =>
                {update.binkynetCommandStation.requiredWorkerVersion = value});
          }));
      children.add(SettingsCheckBoxField(
        label: "Exclude unused objects",
        value: cs.binkynetCommandStation.excludeUnusedObjects,
        onChanged: (value) async {
          await _update((update) {
            update.binkynetCommandStation.excludeUnusedObjects = value;
          });
        },
      ));
    }
    if (cs.hasBidibCommandStation()) {
      children.add(const SettingsHeader(title: "Bidib"));
      children.add(SettingsTextField(
          controller: _serialPortController,
          label: "Serial port name",
          onLostFocus: (value) async {
            await _update((update) =>
                {update.bidibCommandStation.serialPortName = value});
          }));
    }
    children.add(const SettingsHeader(title: "Advanced"));
    children.add(SettingsTextField(
        controller: _addressSpacesController,
        label: "Address spaces (',' seperated)",
        onLostFocus: (value) async {
          await _update((update) {
            update.addressSpaces.clear();
            update.addressSpaces.addAll(value.split(','));
          });
        }));

    if (cs.hasBinkynetCommandStation()) {
      children.add(const SettingsHeader(title: "Local Workers"));
      children
          .add(Expanded(child: BinkyNetLocalWorkersTree(withParents: false)));
    }

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: children,
    );
  }

  Future<void> _update(Function(CommandStation) editor) async {
    final cs = await widget.model.getCommandStation(widget.commandStation.id);
    var update = cs.deepCopy();
    editor(update);
    widget.model.updateCommandStation(update);
  }
}
