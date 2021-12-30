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

import '../components.dart';
import 'package:flutter/material.dart';
import 'package:protobuf/protobuf.dart';
import 'package:provider/provider.dart';

import '../models.dart';
import '../api.dart';
import 'package:binky/editor/editor_context.dart';

class LocSettings extends StatelessWidget {
  const LocSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(builder: (context, model, child) {
      final locId = selector.locId ?? "";
      return FutureBuilder<Loc>(
          future: model.getLoc(locId),
          initialData: model.getCachedLoc(locId),
          builder: (context, snapshot) {
            if (snapshot.hasError) {
              return ErrorMessage(
                  title: "Failed to load Loc", error: snapshot.error);
            } else if (!snapshot.hasData) {
              return const Center(child: CircularProgressIndicator());
            }
            var loc = snapshot.data!;
            return _LocSettings(model: model, loc: loc);
          });
    });
  }
}

class _LocSettings extends StatefulWidget {
  final ModelModel model;
  final Loc loc;
  const _LocSettings({Key? key, required this.model, required this.loc})
      : super(key: key);

  @override
  State<_LocSettings> createState() => _LocSettingsState();
}

class _LocSettingsState extends State<_LocSettings> {
  final ScrollController _scrollController = ScrollController();
  final TextEditingController _descriptionController = TextEditingController();
  final TextEditingController _ownerController = TextEditingController();
  final TextEditingController _addressController = TextEditingController();
  final TextEditingController _slowSpeedController = TextEditingController();
  final TextEditingController _mediumSpeedController = TextEditingController();
  final TextEditingController _maximumSpeedController = TextEditingController();
  final TextEditingController _speedStepsController = TextEditingController();
  final NumericValidator _speedValidator =
      NumericValidator(minimum: 0, maximum: 100);
  final NumericValidator _speedStepsValidator =
      NumericValidator(values: [14, 28, 128]);
  final AddressValidator _addressValidator = AddressValidator();

  _initControllers() {
    _addressValidator.setState = () {
      setState(() {});
    };
    _descriptionController.text = widget.loc.description;
    _ownerController.text = widget.loc.owner;
    _addressController.text = widget.loc.address;
    _slowSpeedController.text = widget.loc.slowSpeed.toString();
    _mediumSpeedController.text = widget.loc.mediumSpeed.toString();
    _maximumSpeedController.text = widget.loc.maximumSpeed.toString();
    _speedStepsController.text = widget.loc.speedSteps.toString();
  }

  @override
  void initState() {
    super.initState();
    _initControllers();
  }

  @override
  void didUpdateWidget(covariant _LocSettings oldWidget) {
    _initControllers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return ScrollableForm(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          const SettingsHeader(title: "General"),
          SettingsTextField(
              controller: _descriptionController,
              label: "Description",
              firstChild: true,
              onLostFocus: (value) async {
                final loc = await widget.model.getLoc(widget.loc.id);
                var update = loc.deepCopy()..description = value;
                widget.model.updateLoc(update);
              }),
          SettingsTextField(
              controller: _ownerController,
              label: "Owner",
              onLostFocus: (value) async {
                final loc = await widget.model.getLoc(widget.loc.id);
                var update = loc.deepCopy()..owner = value;
                widget.model.updateLoc(update);
              }),
          const SettingsHeader(title: "Controller"),
          SettingsTextField(
              controller: _addressController,
              label: "Address",
              validator: _addressValidator.validate,
              onLostFocus: (value) async {
                final loc = await widget.model.getLoc(widget.loc.id);
                var update = loc.deepCopy()..address = value;
                widget.model.updateLoc(update);
              }),
          SettingsTextField(
              controller: _speedStepsController,
              label: "Speed steps",
              keyboardType: TextInputType.number,
              validator: _speedStepsValidator.validate,
              onLostFocus: (value) async {
                final loc = await widget.model.getLoc(widget.loc.id);
                var update = loc.deepCopy()..speedSteps = int.parse(value);
                widget.model.updateLoc(update);
              }),
          const SettingsHeader(title: "Behavior"),
          SettingsTextField(
              controller: _slowSpeedController,
              label: "Slow speed",
              keyboardType: TextInputType.number,
              validator: _speedValidator.validate,
              onLostFocus: (value) async {
                final loc = await widget.model.getLoc(widget.loc.id);
                var update = loc.deepCopy()..slowSpeed = int.parse(value);
                widget.model.updateLoc(update);
              }),
          SettingsTextField(
              controller: _mediumSpeedController,
              label: "Medium speed",
              keyboardType: TextInputType.number,
              validator: _speedValidator.validate,
              onLostFocus: (value) async {
                final loc = await widget.model.getLoc(widget.loc.id);
                var update = loc.deepCopy()..mediumSpeed = int.parse(value);
                widget.model.updateLoc(update);
              }),
          SettingsTextField(
              controller: _maximumSpeedController,
              label: "Maximum speed",
              keyboardType: TextInputType.number,
              validator: _speedValidator.validate,
              onLostFocus: (value) async {
                final loc = await widget.model.getLoc(widget.loc.id);
                var update = loc.deepCopy()..maximumSpeed = int.parse(value);
                widget.model.updateLoc(update);
              }),
        ],
      ),
    );
  }
}
