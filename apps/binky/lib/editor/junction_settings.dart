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

import 'package:binky/icons.dart';
import 'package:flutter/material.dart' hide Route;
import 'package:protobuf/protobuf.dart';
import 'package:provider/provider.dart';

import '../components.dart';
import '../models.dart';
import '../api.dart';
import './editor_context.dart';
import './position_settings.dart';

class JunctionSettings extends StatelessWidget {
  const JunctionSettings({Key? key}) : super(key: key);

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
                        editorCtx: editorCtx,
                        model: model,
                        junction: junction,
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

class _JunctionSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final Junction junction;
  final List<Route> routes;

  const _JunctionSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.junction,
      required this.routes})
      : super(key: key);

  @override
  State<_JunctionSettings> createState() => _JunctionSettingsState();
}

class _JunctionSettingsState extends State<_JunctionSettings> {
  final TextEditingController _descriptionController = TextEditingController();
  final TextEditingController _switchDurationController =
      TextEditingController();
  final NumericValidator _switchDurationValidator =
      NumericValidator(minimum: 0, maximum: 10000);
  final ScrollController _usedByScrollController = ScrollController();

  void _initConrollers() {
    _descriptionController.text = widget.junction.description;
    if (widget.junction.hasSwitch_6()) {
      _switchDurationController.text =
          widget.junction.switch_6.switchDuration.toString();
    }
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
    final List<Widget> children = [
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
    if (widget.junction.hasSwitch_6()) {
      children.add(SettingsCheckBoxField(
        label: "Is left (otherwise right)",
        value: widget.junction.switch_6.isLeft,
        onChanged: (value) async {
          await _update((update) {
            update.switch_6.isLeft = value;
          });
        },
      ));
      children.add(SettingsAddressField(
          key: Key("${widget.junction.id}/junction/address"),
          label: "Address",
          address: widget.junction.switch_6.address,
          onLostFocus: (value) async {
            await _update((update) {
              update.switch_6.address = value;
            });
          }));
      children.add(SettingsCheckBoxField(
        label: "Invert direction",
        value: widget.junction.switch_6.invert,
        onChanged: (value) async {
          await _update((update) {
            update.switch_6.invert = value;
          });
        },
      ));
      children.add(SettingsCheckBoxField(
        label: "Has feedback",
        value: widget.junction.switch_6.hasFeedback,
        onChanged: (value) async {
          await _update((update) {
            update.switch_6.hasFeedback = value;
          });
        },
      ));
      if (widget.junction.switch_6.hasFeedback) {
        children.add(SettingsAddressField(
            key: Key("${widget.junction.id}/junction/feedbackAddress"),
            address: widget.junction.switch_6.feedbackAddress,
            label: "Feedback address",
            onLostFocus: (value) async {
              await _update((update) {
                update.switch_6.feedbackAddress = value;
              });
            }));
        children.add(SettingsCheckBoxField(
          label: "Invert feedback direction",
          value: widget.junction.switch_6.invertFeedback,
          onChanged: (value) async {
            await _update((update) {
              update.switch_6.invertFeedback = value;
            });
          },
        ));
        children.add(SettingsTextField(
            controller: _switchDurationController,
            label: "Switch duration (ms)",
            validator: _switchDurationValidator.validate,
            onLostFocus: (value) async {
              await _update((update) {
                update.switch_6.switchDuration = int.parse(value);
              });
            }));
        children.add(SettingsDropdownField<SwitchDirection>(
          label: "Initial direction",
          value: widget.junction.switch_6.initialDirection,
          onChanged: (value) {
            _update((x) {
              if (value != null) {
                x.switch_6.initialDirection = value;
              }
            });
          },
          items: _switchDirectionItems,
        ));
      }
    }
    children.add(const SettingsHeader(title: "Position"));
    children.add(
      PositionSettings(
          editorCtx: widget.editorCtx,
          model: widget.model,
          position: widget.junction.position,
          moduleId: widget.junction.moduleId,
          update: (editor) async {
            await _update((update) {
              editor(update.position);
            });
          }),
    );
    final usedBy = _buildUsedBy();
    children.add(const SettingsHeader(title: "Used by"));
    children.add(Expanded(
      child: ListView.builder(
          controller: _usedByScrollController,
          itemCount: usedBy.length,
          itemBuilder: (context, index) {
            return usedBy[index];
          }),
    ));
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: children,
    );
  }

  Future<void> _update(void Function(Junction) editor) async {
    final current = await widget.model.getJunction(widget.junction.id);
    var update = current.deepCopy();
    editor(update);
    await widget.model.updateJunction(update);
  }

  static final List<DropdownMenuItem<SwitchDirection>> _switchDirectionItems =
      SwitchDirection.values
          .map((e) => DropdownMenuItem<SwitchDirection>(
                child: Text(e.name),
                value: e,
              ))
          .toList();

  List<ListTile> _buildUsedBy() {
    final junctionId = widget.junction.id;
    final routes = widget.routes.where(
        (r) => r.crossingJunctions.any((x) => x.junction.id == junctionId));
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
