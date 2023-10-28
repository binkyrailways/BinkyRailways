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

import 'package:binky/dropdownmenuitems.dart';
import 'package:binky/editor/helper_texts.dart';
import 'package:binky/icons.dart';
import 'package:flutter/material.dart' hide Route;
import 'package:protobuf/protobuf.dart';
import 'package:provider/provider.dart';
import 'package:recase/recase.dart';

import '../components.dart';
import '../models.dart';
import '../api.dart';
import './editor_context.dart';
import './position_settings.dart';

class BlockSettings extends StatelessWidget {
  const BlockSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorContext>(builder: (context, editorCtx, child) {
      final selector = editorCtx.selector;
      return Consumer<ModelModel>(builder: (context, model, child) {
        final moduleId = selector.idOf(EntityType.module) ?? "";
        return FutureBuilder<List<Route>>(
            future: _getRoutes(model, moduleId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              final routes = snapshot.data!;
              final blockId = selector.idOf(EntityType.block) ?? "";
              return FutureBuilder<Block>(
                  future: model.getBlock(blockId),
                  initialData: model.getCachedBlock(blockId),
                  builder: (context, snapshot) {
                    if (!snapshot.hasData) {
                      return const Center(child: CircularProgressIndicator());
                    }
                    var block = snapshot.data!;
                    return _BlockSettings(
                        editorCtx: editorCtx,
                        model: model,
                        block: block,
                        routes: routes);
                  });
            });
      });
    });
  }

  Future<List<Route>> _getRoutes(ModelModel model, String moduleId) async {
    final mod = await model.getModule(moduleId);
    final routeList = mod.routes.map((e) => model.getRoute(e.id));
    final result = await Future.wait(routeList);
    result.sort((a, b) => a.description.compareTo(b.description));
    return result;
  }
}

class _BlockSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final Block block;
  final List<Route> routes;

  const _BlockSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.block,
      required this.routes})
      : super(key: key);

  @override
  State<_BlockSettings> createState() => _BlockSettingsState();
}

class _BlockSettingsState extends State<_BlockSettings> {
  final TextEditingController _descriptionController = TextEditingController();
  final TextEditingController _waitProbabilityController =
      TextEditingController();
  final TextEditingController _minWaitTimeController = TextEditingController();
  final TextEditingController _maxWaitTimeController = TextEditingController();
  final TextEditingController _waitPermissionsController =
      TextEditingController();
  final ScrollController _usedByScrollController = ScrollController();
  final NumericValidator _waitProbabilityValidator =
      NumericValidator(minimum: 0, maximum: 100);
  final NumericValidator _waitTimeValidator =
      NumericValidator(minimum: 0, maximum: 360);
  final PermissionValidator _permissionValidator = PermissionValidator();

  void _initConrollers() {
    _descriptionController.text = widget.block.description;
    _waitProbabilityController.text = widget.block.waitProbability.toString();
    _minWaitTimeController.text = widget.block.minimumWaitTime.toString();
    _maxWaitTimeController.text = widget.block.maximumWaitTime.toString();
    _waitPermissionsController.text = widget.block.waitPermissions.toString();
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _BlockSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    final usedBy = _buildUsedBy();
    final canWait = widget.block.waitProbability > 0;
    final children = [
      const SettingsHeader(title: "General"),
      SettingsTextField(
          controller: _descriptionController,
          label: "Description",
          onLostFocus: (value) async {
            await _update((update) {
              update.description = value;
            });
          }),
      SettingsTextField(
          controller: _waitProbabilityController,
          validator: _waitProbabilityValidator.validate,
          label: "Wait probability",
          onLostFocus: (value) async {
            await _update((update) {
              update.waitProbability = int.parse(value);
            });
          }),
      canWait
          ? SettingsTextField(
              controller: _waitPermissionsController,
              validator: _permissionValidator.validate,
              label: "Wait permissions",
              helperText: HelperTexts.permission,
              onLostFocus: (value) async {
                await _update((update) {
                  update.waitPermissions = value;
                });
              })
          : Container(),
      canWait
          ? Row(children: [
              Expanded(
                child: SettingsTextField(
                    controller: _minWaitTimeController,
                    validator: _waitTimeValidator.validate,
                    label: "Min wait time (sec)",
                    onLostFocus: (value) async {
                      await _update((update) {
                        update.minimumWaitTime = int.parse(value);
                      });
                    }),
              ),
              Expanded(
                child: SettingsTextField(
                    controller: _maxWaitTimeController,
                    validator: _waitTimeValidator.validate,
                    label: "Max wait time (sec)",
                    onLostFocus: (value) async {
                      await _update((update) {
                        update.maximumWaitTime = int.parse(value);
                      });
                    }),
              ),
            ])
          : Container(),
      SettingsDropdownField<ChangeDirection>(
        label: "Change direction",
        value: widget.block.changeDirection,
        onChanged: (value) async {
          if (value != null) {
            await _update((update) {
              update.changeDirection = value;
            });
          }
        },
        items: BinkyDropdownMenuItems.ChangeDirectionItems,
      ),
      const SettingsHeader(title: "Position"),
      PositionSettings(
          editorCtx: widget.editorCtx,
          model: widget.model,
          position: widget.block.position,
          moduleId: widget.block.moduleId,
          update: (editor) async {
            await _update((update) {
              editor(update.position);
            });
          }),
      SettingsCheckBoxField(
        label: "Reverse sides",
        value: widget.block.reverseSides,
        onChanged: (bool value) async {
          await _update((update) {
            update.reverseSides = value;
          });
        },
      ),
      const SettingsHeader(title: "Used by"),
      Expanded(
        child: ListView.builder(
            controller: _usedByScrollController,
            itemCount: usedBy.length,
            itemBuilder: (context, index) {
              return usedBy[index];
            }),
      ),
    ];
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: children,
    );
  }

  Future<void> _update(void Function(Block) editor) async {
    final current = await widget.model.getBlock(widget.block.id);
    var update = current.deepCopy();
    editor(update);
    await widget.model.updateBlock(update);
  }

  List<ListTile> _buildUsedBy() {
    final blockId = widget.block.id;
    final routes = widget.routes.where(
        (r) => (r.from.block.id == blockId) || (r.to.block.id == blockId));
    final items = routes
        .map((r) => ListTile(
              leading: BinkyIcons.route,
              title: Text(r.description),
              onTap: () {
                widget.editorCtx.select(EntitySelector.route(r));
              },
            ))
        .toList();
    return items;
  }
}
