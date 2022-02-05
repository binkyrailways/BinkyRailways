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

class SensorSettings extends StatelessWidget {
  const SensorSettings({Key? key}) : super(key: key);

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
              return FutureBuilder<List<Route>>(
                  future: _getRoutes(model, moduleId),
                  builder: (context, snapshot) {
                    if (!snapshot.hasData) {
                      return const Center(child: CircularProgressIndicator());
                    }
                    var routes = snapshot.data!;
                    final sensorId = selector.idOf(EntityType.sensor) ?? "";
                    return FutureBuilder<Sensor>(
                        future: model.getSensor(sensorId),
                        initialData: model.getCachedSensor(sensorId),
                        builder: (context, snapshot) {
                          if (!snapshot.hasData) {
                            return const Center(
                                child: CircularProgressIndicator());
                          }
                          var sensor = snapshot.data!;
                          return _SensorSettings(
                              editorCtx: editorCtx,
                              model: model,
                              blocks: blocks,
                              routes: routes,
                              sensor: sensor);
                        });
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

  Future<List<Route>> _getRoutes(ModelModel model, String moduleId) async {
    final mod = await model.getModule(moduleId);
    final routeList = mod.routes.map((e) => model.getRoute(e.id));
    final result = await Future.wait(routeList);
    result.sort((a, b) => a.description.compareTo(b.description));
    return result;
  }
}

class _SensorSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final List<Block> blocks;
  final List<Route> routes;
  final Sensor sensor;
  const _SensorSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.blocks,
      required this.routes,
      required this.sensor})
      : super(key: key);

  @override
  State<_SensorSettings> createState() => _SensorSettingsState();
}

class _SensorSettingsState extends State<_SensorSettings> {
  final TextEditingController _descriptionController = TextEditingController();
  final ScrollController _usedByScrollController = ScrollController();

  void _initConrollers() {
    _descriptionController.text = widget.sensor.description;
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _SensorSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    final usedBy = _buildUsedBy();
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
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
        SettingsAddressField(
            key: Key("${widget.sensor.id}/sensor/address"),
            address: widget.sensor.address,
            label: "Address",
            onLostFocus: (value) async {
              await _update((update) {
                update.address = value;
              });
            }),
        SettingsDropdownField<String>(
          label: "Block",
          value: widget.sensor.block.id,
          onChanged: (value) {
            _update((x) {
              if (value != null) {
                x.block = BlockRef(id: value);
              }
            });
          },
          items: _blockIds(),
        ),
        const SettingsHeader(title: "Position"),
        PositionSettings(
            editorCtx: widget.editorCtx,
            model: widget.model,
            position: widget.sensor.position,
            update: (editor) async {
              await _update((update) {
                editor(update.position);
              });
            }),
        SettingsDropdownField<Shape>(
          label: "Shape",
          value: widget.sensor.shape,
          onChanged: (value) {
            _update((x) {
              if (value != null) {
                x.shape = value;
              }
            });
          },
          items: _shapeItems,
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
      ],
    );
  }

  Future<void> _update(void Function(Sensor) editor) async {
    final current = await widget.model.getSensor(widget.sensor.id);
    var update = current.deepCopy();
    editor(update);
    await widget.model.updateSensor(update);
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

  static final List<DropdownMenuItem<Shape>> _shapeItems = Shape.values
      .map((e) => DropdownMenuItem<Shape>(
            child: Text(e.name),
            value: e,
          ))
      .toList();

  List<ListTile> _buildUsedBy() {
    final routes = widget.routes
        .where((r) => r.events.any((evt) => evt.sensor.id == widget.sensor.id));
    final items = routes
        .map((r) => ListTile(
              leading: BinkyIcons.route,
              title: Text(r.description),
              onTap: () {
                widget.editorCtx.select(EntitySelector.route(r));
              },
            ))
        .toList();
    final blocks = widget.blocks.where((b) => b.id == widget.sensor.block.id);
    blocks.forEach((b) {
      items.add(ListTile(
        leading: BinkyIcons.block,
        title: Text(b.description),
        onTap: () {
          widget.editorCtx.select(EntitySelector.block(b));
        },
      ));
    });
    return items;
  }
}
