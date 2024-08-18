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
import './editor_context.dart';
import './position_settings.dart';

class SignalSettings extends StatelessWidget {
  const SignalSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorContext>(builder: (context, editorCtx, child) {
      final selector = editorCtx.selector;
      return Consumer<ModelModel>(builder: (context, model, child) {
        final moduleId = selector.idOf(EntityType.module) ?? "";
        return FutureBuilder<List<Block>>(
            future: _getBlocks(model, moduleId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var blocks = snapshot.data!;
              final signalId = selector.idOf(EntityType.signal) ?? "";
              return FutureBuilder<Signal>(
                  future: model.getSignal(signalId),
                  initialData: model.getCachedSignal(signalId),
                  builder: (context, snapshot) {
                    if (!snapshot.hasData) {
                      return const Center(child: CircularProgressIndicator());
                    }
                    var signal = snapshot.data!;
                    return _SignalSettings(
                        editorCtx: editorCtx,
                        model: model,
                        blocks: blocks,
                        signal: signal);
                  });
            });
      });
    });
  }

  Future<List<Block>> _getBlocks(ModelModel model, String moduleId) async {
    final mod = await model.getModule(moduleId);
    final blockList = mod.blocks.map((e) => model.getBlock(e.id));
    final result = await Future.wait(blockList);
    result.sort((a, b) => a.description.compareTo(b.description));
    return result;
  }
}

class _SignalSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final Signal signal;
  final List<Block> blocks;

  const _SignalSettings({
    Key? key,
    required this.editorCtx,
    required this.model,
    required this.blocks,
    required this.signal,
  }) : super(key: key);

  @override
  State<_SignalSettings> createState() => _SignalSettingsState();
}

class _SignalSettingsState extends State<_SignalSettings> {
  final SignalPatternValidator _patternValidator = SignalPatternValidator();
  final TextEditingController _descriptionController = TextEditingController();
  final TextEditingController _redPatternController = TextEditingController();
  final TextEditingController _greenPatternController = TextEditingController();
  final TextEditingController _yellowPatternController =
      TextEditingController();
  final TextEditingController _whitePatternController = TextEditingController();

  void _initControllers() {
    _descriptionController.text = widget.signal.description;
    if (widget.signal.hasBlockSignal()) {
      _redPatternController.text = SignalPatternValidator.patternToString(
          widget.signal.blockSignal.redPattern);
      _greenPatternController.text = SignalPatternValidator.patternToString(
          widget.signal.blockSignal.greenPattern);
      _yellowPatternController.text = SignalPatternValidator.patternToString(
          widget.signal.blockSignal.yellowPattern);
      _whitePatternController.text = SignalPatternValidator.patternToString(
          widget.signal.blockSignal.whitePattern);
    }
  }

  @override
  void initState() {
    super.initState();
    _initControllers();
  }

  @override
  void didUpdateWidget(covariant _SignalSettings oldWidget) {
    _initControllers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    final widgets = <Widget>[
      const SettingsHeader(title: "General"),
      SettingsTextField(
          controller: _descriptionController,
          label: "Description",
          firstChild: true,
          onLostFocus: (value) async {
            await _update((update) {
              update.description = value;
            });
          }),
    ];
    if (widget.signal.hasBlockSignal()) {
      widgets.add(const SettingsHeader(title: "Connections"));
      widgets.add(Row(children: [
        Expanded(
            child: SettingsAddressField(
                key: Key("${widget.signal.id}/signal/address1"),
                label: "Address 1",
                address: widget.signal.blockSignal.address1,
                onLostFocus: (value) async {
                  await _update((update) {
                    update.blockSignal.address1 = value;
                  });
                })),
        Expanded(
            child: SettingsAddressField(
                key: Key("${widget.signal.id}/signal/address2"),
                label: "Address 2",
                address: widget.signal.blockSignal.address2,
                onLostFocus: (value) async {
                  await _update((update) {
                    update.blockSignal.address2 = value;
                  });
                })),
      ]));
      widgets.add(Row(children: [
        Expanded(
            child: SettingsAddressField(
                key: Key("${widget.signal.id}/signal/address3"),
                label: "Address 3",
                address: widget.signal.blockSignal.address3,
                onLostFocus: (value) async {
                  await _update((update) {
                    update.blockSignal.address3 = value;
                  });
                })),
        Expanded(
            child: SettingsAddressField(
                key: Key("${widget.signal.id}/signal/address4"),
                label: "Address 4",
                address: widget.signal.blockSignal.address4,
                onLostFocus: (value) async {
                  await _update((update) {
                    update.blockSignal.address4 = value;
                  });
                })),
      ]));

      widgets.add(Row(children: [
        Expanded(
            child: SettingsTextField(
                key: Key("${widget.signal.id}/signal/red_pattern"),
                controller: _redPatternController,
                label: "Red pattern",
                validator: _patternValidator.validate,
                onLostFocus: (value) async {
                  await _update((update) {
                    update.blockSignal.redPattern =
                        SignalPatternValidator.stringToPattern(value);
                  });
                })),
        Expanded(
            child: SettingsTextField(
                key: Key("${widget.signal.id}/signal/green_pattern"),
                controller: _greenPatternController,
                label: "Green pattern",
                validator: _patternValidator.validate,
                onLostFocus: (value) async {
                  await _update((update) {
                    update.blockSignal.greenPattern =
                        SignalPatternValidator.stringToPattern(value);
                  });
                })),
      ]));
      widgets.add(Row(children: [
        Expanded(
            child: SettingsTextField(
                key: Key("${widget.signal.id}/signal/yellow_pattern"),
                controller: _yellowPatternController,
                label: "Yellow pattern",
                validator: _patternValidator.validate,
                onLostFocus: (value) async {
                  await _update((update) {
                    update.blockSignal.yellowPattern =
                        SignalPatternValidator.stringToPattern(value);
                  });
                })),
        Expanded(
            child: SettingsTextField(
                key: Key("${widget.signal.id}/signal/white_pattern"),
                controller: _whitePatternController,
                label: "White pattern",
                validator: _patternValidator.validate,
                onLostFocus: (value) async {
                  await _update((update) {
                    update.blockSignal.whitePattern =
                        SignalPatternValidator.stringToPattern(value);
                  });
                })),
      ]));

      widgets.add(SettingsDropdownField<String>(
        key: Key("${widget.signal.id}/signal/block"),
        label: "Block",
        value: widget.signal.blockSignal.block.id,
        onChanged: (value) {
          _update((x) {
            if (value != null) {
              x.blockSignal.block = BlockRef(id: value);
            }
          });
        },
        items: _blockIds(),
      ));
      print(
          "blockID=${widget.signal.blockSignal.block.id}, #blocks=${_blockIds()}");

      widgets.add(SettingsDropdownField<BlockSide>(
        key: Key("${widget.signal.id}/signal/block_side"),
        label: "Block side",
        value: widget.signal.blockSignal.blockSide,
        onChanged: (value) {
          _update((x) {
            if (value != null) {
              x.blockSignal.blockSide = value;
            }
          });
        },
        items: _blockSideItems,
      ));
      widgets.add(SettingsDropdownField<BlockSignalType>(
        key: Key("${widget.signal.id}/signal/type"),
        label: "Block signal type",
        value: widget.signal.blockSignal.type,
        onChanged: (value) {
          _update((x) {
            if (value != null) {
              x.blockSignal.type = value;
            }
          });
        },
        items: _blockSignalTypeItems,
      ));
    }

    widgets.add(const SettingsHeader(title: "Position"));
    widgets.add(
      PositionSettings(
          editorCtx: widget.editorCtx,
          model: widget.model,
          position: widget.signal.position,
          moduleId: widget.signal.moduleId,
          update: (editor) async {
            await _update((update) {
              editor(update.position);
            });
          }),
    );
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: widgets,
    );
  }

  Future<void> _update(void Function(Signal) editor) async {
    final current = await widget.model.getSignal(widget.signal.id);
    var update = current.deepCopy();
    editor(update);
    await widget.model.updateSignal(update);
  }

  List<DropdownMenuItem<String>> _blockIds() {
    final list = widget.blocks
        .map((e) => DropdownMenuItem<String>(
              child: Text(e.description),
              value: e.id,
            ))
        .toList();
    list.add(
      const DropdownMenuItem(child: Text("<None>"), value: ""),
    );
    return list;
  }

  static final List<DropdownMenuItem<BlockSide>> _blockSideItems =
      BlockSide.values
          .map((e) => DropdownMenuItem<BlockSide>(
                child: Text(e.name),
                value: e,
              ))
          .toList();

  static final List<DropdownMenuItem<BlockSignalType>> _blockSignalTypeItems =
      BlockSignalType.values
          .map((e) => DropdownMenuItem<BlockSignalType>(
                child: Text(e.name),
                value: e,
              ))
          .toList();
}
