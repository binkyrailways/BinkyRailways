// Copyright 2021-2022 Ewout Prangsma
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
import 'package:flutter/services.dart';
import 'package:protobuf/protobuf.dart';
import 'package:provider/provider.dart';

import '../components.dart';
import '../models.dart';
import '../api.dart';
import '../icons.dart';
import 'package:binky/editor/editor_context.dart';
import './route_event_settings.dart';

class RouteSettings extends StatelessWidget {
  const RouteSettings({Key? key}) : super(key: key);

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
              return FutureBuilder<List<Edge>>(
                  future: _getEges(model, moduleId),
                  builder: (context, snapshot) {
                    if (!snapshot.hasData) {
                      return const Center(child: CircularProgressIndicator());
                    }
                    var edges = snapshot.data!;
                    final routeId = selector.idOf(EntityType.route) ?? "";
                    return FutureBuilder<Route>(
                        future: model.getRoute(routeId),
                        initialData: model.getCachedRoute(routeId),
                        builder: (context, snapshot) {
                          if (!snapshot.hasData) {
                            return const Center(
                                child: CircularProgressIndicator());
                          }
                          var route = snapshot.data!;
                          return _RouteSettings(
                            editorCtx: editorCtx,
                            model: model,
                            route: route,
                            blocks: blocks,
                            edges: edges,
                          );
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

  Future<List<Edge>> _getEges(ModelModel model, String moduleId) async {
    final mod = await model.getModule(moduleId);
    final edgeList = mod.edges.map((e) => model.getEdge(e.id));
    final result = await Future.wait(edgeList);
    result.sort((a, b) => a.description.compareTo(b.description));
    return result;
  }
}

class _RouteSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final Route route;
  final List<Block> blocks;
  final List<Edge> edges;
  const _RouteSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.route,
      required this.blocks,
      required this.edges})
      : super(key: key);

  @override
  State<_RouteSettings> createState() => _RouteSettingsState();
}

class _RouteSettingsState extends State<_RouteSettings> {
  final TextEditingController _speedController = TextEditingController();
  final NumericValidator _speedValidator =
      NumericValidator(minimum: 1, maximum: 100);
  final TextEditingController _chooseProbabilityController =
      TextEditingController();
  final TextEditingController _permissionsController = TextEditingController();
  final NumericValidator _chooseProbabilityValidator =
      NumericValidator(minimum: 0, maximum: 100);
  final TextEditingController _maxDurationController = TextEditingController();
  final NumericValidator _maxDurationValidator =
      NumericValidator(minimum: 0, maximum: 600);
  final PermissionValidator _permissionValidator = PermissionValidator();

  void _initConrollers() {
    _speedController.text = widget.route.speed.toString();
    _chooseProbabilityController.text =
        widget.route.chooseProbability.toString();
    _maxDurationController.text = widget.route.maxDuration.toString();
    _permissionsController.text = widget.route.permissions.toString();
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _RouteSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    final List<Widget> children = [
      const SettingsHeader(title: "General"),
      SettingsDropdownField<String>(
        key: Key("${widget.route.id}/route/from"),
        firstChild: true,
        label: "From",
        value: _endpointId(widget.route.from),
        onChanged: (value) {
          _update((x) {
            if (value != null) {
              x.from = _endpointFromId(value);
            }
          });
        },
        items: _endpointIds(),
      ),
      SettingsDropdownField<String>(
        key: Key("${widget.route.id}/route/to"),
        label: "To",
        value: _endpointId(widget.route.to),
        onChanged: (value) {
          _update((x) {
            if (value != null) {
              x.to = _endpointFromId(value);
            }
          });
        },
        items: _endpointIds(),
      ),
      SettingsCheckBoxField(
        label: "Closed",
        value: widget.route.closed,
        onChanged: (value) async {
          await _update((update) {
            update.closed = value;
          });
        },
      ),
      SettingsTextField(
          key: Key("${widget.route.id}/route/speed"),
          controller: _speedController,
          label: "Speed",
          validator: _speedValidator.validate,
          onLostFocus: (value) async {
            await _update((update) {
              update.speed = int.parse(value);
            });
          }),
      SettingsTextField(
          key: Key("${widget.route.id}/route/permissions"),
          controller: _permissionsController,
          label: "Permissions",
          validator: _permissionValidator.validate,
          onLostFocus: (value) async {
            await _update((update) {
              update.permissions = value;
            });
          }),
      SettingsTextField(
          key: Key("${widget.route.id}/route/chooseProbability"),
          controller: _chooseProbabilityController,
          label: "Choose probability",
          validator: _chooseProbabilityValidator.validate,
          onLostFocus: (value) async {
            await _update((update) {
              update.chooseProbability = int.parse(value);
            });
          }),
      SettingsTextField(
          key: Key("${widget.route.id}/route/maxDuration"),
          controller: _maxDurationController,
          label: "Max duration",
          validator: _maxDurationValidator.validate,
          onLostFocus: (value) async {
            await _update((update) {
              update.maxDuration = int.parse(value);
            });
          }),
    ];
    final List<Widget> crossingJunctionChildren = [];
    for (var cj in widget.route.crossingJunctions) {
      crossingJunctionChildren.add(FutureBuilder<String>(
          future: _formatJunctionWithState(widget.model, cj),
          builder: (context, snapshot) {
            if (!snapshot.hasData) {
              return const ListTile(
                leading: BinkyIcons.junction,
                title: Text("Loading..."),
              );
            }
            final description = snapshot.data!;
            return ListTile(
              title: Text(description),
              trailing: GestureDetector(
                child: const Icon(Icons.more_vert),
                onTapDown: (TapDownDetails details) {
                  showMenu(
                    context: context,
                    position: RelativeRect.fromLTRB(details.globalPosition.dx,
                        details.globalPosition.dy, 0, 0),
                    items: [
                      PopupMenuItem<String>(
                          child: const Text('Remove'),
                          onTap: () async {
                            await widget.model.removeRouteCrossingJunction(
                                widget.route.id, cj.junction.id);
                          }),
                    ],
                    elevation: 8.0,
                  );
                },
              ),
            );
          }));
    }
    crossingJunctionChildren.add(FutureBuilder<List<DropdownMenuItem<String>>>(
      future: _addJunctionWithStateList(widget.model),
      builder: (context, snapshot) {
        if (!snapshot.hasData) {
          return const Text("Loading...");
        }
        return Container(
            padding: const EdgeInsets.all(8),
            child: DropdownButton<String>(
              items: snapshot.data!,
              onChanged: (value) {},
              isDense: true,
              hint: const Text("Add..."),
            ));
      },
    ));
    children.add(SettingsHeader(
      title: "Crossing junctions",
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: crossingJunctionChildren,
      ),
    ));

    final List<Widget> outputChildren = [];
    for (var ows in widget.route.outputs) {
      outputChildren.add(FutureBuilder<String>(
          future: _formatOutputWithState(widget.model, ows),
          builder: (context, snapshot) {
            if (!snapshot.hasData) {
              return const ListTile(
                leading: BinkyIcons.output,
                title: Text("Loading..."),
              );
            }
            final description = snapshot.data!;
            return ListTile(
              title: Text(description),
              trailing: GestureDetector(
                child: const Icon(Icons.more_vert),
                onTapDown: (TapDownDetails details) {
                  showMenu(
                    context: context,
                    position: RelativeRect.fromLTRB(details.globalPosition.dx,
                        details.globalPosition.dy, 0, 0),
                    items: [
                      PopupMenuItem<String>(
                          child: const Text('Remove'),
                          onTap: () async {
                            await widget.model.removeRouteOutput(
                                widget.route.id, ows.output.id);
                          }),
                    ],
                    elevation: 8.0,
                  );
                },
              ),
            );
          }));
    }
    outputChildren.add(FutureBuilder<List<DropdownMenuItem<String>>>(
      future: _addOutputWithStateList(widget.model),
      builder: (context, snapshot) {
        if (!snapshot.hasData) {
          return const Text("Loading...");
        }
        return Container(
            padding: const EdgeInsets.all(8),
            child: DropdownButton<String>(
              items: snapshot.data!,
              onChanged: (value) {},
              isDense: true,
              hint: const Text("Add..."),
            ));
      },
    ));
    children.add(SettingsHeader(
      title: "Output",
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: outputChildren,
      ),
    ));

    final List<Widget> eventsChildren = [];
    widget.route.events.asMap().forEach((index, evt) {
      eventsChildren.add(FutureBuilder<String>(
          future: _formatRouteEvent(widget.model, evt),
          builder: (context, snapshot) {
            if (!snapshot.hasData) {
              return const ListTile(
                leading: BinkyIcons.junction,
                title: Text("Loading..."),
              );
            }
            final description = snapshot.data!;
            return ListTile(
              title: Text(description),
              subtitle: Text("${evt.behaviors.length} behaviors"),
              trailing: GestureDetector(
                child: const Icon(Icons.more_vert),
                onTapDown: (TapDownDetails details) {
                  showMenu(
                    context: context,
                    position: RelativeRect.fromLTRB(details.globalPosition.dx,
                        details.globalPosition.dy, 0, 0),
                    items: [
                      PopupMenuItem<String>(
                          child: const Text('Remove'),
                          onTap: () async {
                            await widget.model.removeRouteEvent(
                                widget.route.id, evt.sensor.id);
                          }),
                    ],
                    elevation: 8.0,
                  );
                },
              ),
              onTap: () {
                showDialog(
                    context: context,
                    builder: (context) {
                      return StatefulBuilder(builder: ((context, setState) {
                        return SimpleDialog(
                          title: Text(description),
                          children: [
                            RouteEventSettings(
                              model: widget.model,
                              routeId: widget.route.id,
                              eventIndex: index,
                              update: (editor) async {
                                await _update((update) {
                                  editor(update.events[index]);
                                });
                                // Rebuild dialog content
                                setState(() {});
                              },
                              removeBehavior: (index) async {
                                await widget.model.removeRouteEventBehavior(
                                    widget.route.id, evt.sensor.id, index);
                                // Rebuild dialog content
                                setState(() {});
                              },
                            ),
                            Row(children: [
                              TextButton(
                                child: const Text("Add..."),
                                onPressed: () async {
                                  await widget.model.addRouteEventBehavior(
                                      widget.route.id, evt.sensor.id);
                                  // Rebuild dialog content
                                  setState(() {});
                                },
                              ),
                              TextButton(
                                child: const Text("Add Enter"),
                                onPressed: () async {
                                  final r = await widget.model
                                      .addRouteEventBehavior(
                                          widget.route.id, evt.sensor.id);
                                  final updatedEvt = r.events.firstWhere(
                                      (x) => x.sensor.id == evt.sensor.id);
                                  updatedEvt.behaviors.last.stateBehavior =
                                      RouteStateBehavior.RSB_ENTER;
                                  await widget.model.updateRoute(r);
                                  // Rebuild dialog content
                                  setState(() {});
                                },
                              ),
                              TextButton(
                                child: const Text("Add Reached"),
                                onPressed: () async {
                                  final r = await widget.model
                                      .addRouteEventBehavior(
                                          widget.route.id, evt.sensor.id);
                                  final updatedEvt = r.events.firstWhere(
                                      (x) => x.sensor.id == evt.sensor.id);
                                  updatedEvt.behaviors.last.stateBehavior =
                                      RouteStateBehavior.RSB_REACHED;
                                  await widget.model.updateRoute(r);
                                  // Rebuild dialog content
                                  setState(() {});
                                },
                              ),
                            ]),
                          ],
                        );
                      }));
                    });
              },
            );
          }));
    });
    eventsChildren.add(FutureBuilder<List<DropdownMenuItem<String>>>(
      future: _addRouteEventList(widget.model),
      builder: (context, snapshot) {
        if (!snapshot.hasData) {
          return const Text("Loading...");
        }
        return Container(
            padding: const EdgeInsets.all(8),
            child: DropdownButton<String>(
              items: snapshot.data!,
              onChanged: (value) {},
              isDense: true,
              hint: const Text("Add..."),
            ));
      },
    ));
    children.add(SettingsHeader(
      title: "Events",
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: eventsChildren,
      ),
    ));

    return ScrollableForm(
        child: Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: children,
    ));
  }

  Future<void> _update(void Function(Route) editor) async {
    final current = await widget.model.getRoute(widget.route.id);
    var update = current.deepCopy();
    editor(update);
    await widget.model.updateRoute(update);
  }

  List<DropdownMenuItem<String>> _endpointIds() {
    final List<DropdownMenuItem<String>> list = [];
    for (var e in widget.blocks) {
      list.add(DropdownMenuItem<String>(
        child: Text("Front of ${e.description}"),
        value: "block/front/${e.id}",
      ));
      list.add(DropdownMenuItem<String>(
        child: Text("Back of ${e.description}"),
        value: "block/back/${e.id}",
      ));
    }
    for (var e in widget.edges) {
      list.add(DropdownMenuItem<String>(
        child: Text("Edge ${e.description}"),
        value: "edge/${e.id}",
      ));
    }
    list.add(const DropdownMenuItem(child: Text("<None>"), value: ""));
    return list;
  }

  String _endpointId(Endpoint ep) {
    if (ep.hasBlock()) {
      if (ep.blockSide == BlockSide.FRONT) {
        return "block/front/${ep.block.id}";
      }
      return "block/back/${ep.block.id}";
    }
    if (ep.hasEdge()) {
      return "edge/${ep.edge.id}";
    }
    return "";
  }

  Endpoint _endpointFromId(String id) {
    if (id.startsWith("block/front/")) {
      return Endpoint(
        block: BlockRef(id: id.substring("block/front/".length)),
        blockSide: BlockSide.FRONT,
      );
    }
    if (id.startsWith("block/back/")) {
      return Endpoint(
        block: BlockRef(id: id.substring("block/back/".length)),
        blockSide: BlockSide.BACK,
      );
    }
    if (id.startsWith("edge/")) {
      return Endpoint(
        edge: EdgeRef(id: id.substring("edge/".length)),
        blockSide: BlockSide.FRONT,
      );
    }
    return Endpoint();
  }

  Future<String> _formatJunctionWithState(
      ModelModel modelModel, JunctionWithState jws) async {
    final junction = await modelModel.getJunction(jws.junction.id);
    if (jws.hasSwitchState()) {
      return "${junction.description} -> ${jws.switchState.direction.humanize()}";
    }
    return junction.description;
  }

  Future<String> _formatOutputWithState(
      ModelModel modelModel, OutputWithState jws) async {
    final output = await modelModel.getOutput(jws.output.id);
    if (jws.hasBinaryOutputState()) {
      return "${output.description} -> ${jws.binaryOutputState.active ? _formatActiveText(output.binaryOutput) : _formatInactiveText(output.binaryOutput)}";
    }
    return output.description;
  }

  Future<List<DropdownMenuItem<String>>> _addJunctionWithStateList(
      ModelModel modelModel) async {
    final module = await modelModel.getModule(widget.route.moduleId);
    final crossingJunctionIds =
        widget.route.crossingJunctions.map((cj) => cj.junction.id).toList();
    final allJunctionIds = module.junctions.map((e) => e.id);
    final allUnusedJunctionIds =
        allJunctionIds.where((id) => !crossingJunctionIds.contains(id));
    final allUnusedJunctions = await Future.wait(
        allUnusedJunctionIds.map((id) => modelModel.getJunction(id)));
    allUnusedJunctions.sort((a, b) => a.description.compareTo(b.description));

    final List<DropdownMenuItem<String>> result = [];
    for (var junction in allUnusedJunctions) {
      if (junction.hasSwitch_6()) {
        result.add(DropdownMenuItem<String>(
          value: junction.id,
          child: Text("${junction.description} -> Straight"),
          onTap: () async {
            await modelModel.addRouteCrossingJunctionSwitch(
                widget.route.id, junction.id, SwitchDirection.STRAIGHT);
          },
        ));
        result.add(DropdownMenuItem<String>(
          value: junction.id,
          child: Text("${junction.description} -> Off"),
          onTap: () async {
            await modelModel.addRouteCrossingJunctionSwitch(
                widget.route.id, junction.id, SwitchDirection.OFF);
          },
        ));
      }
    }
    return result;
  }

  Future<List<DropdownMenuItem<String>>> _addOutputWithStateList(
      ModelModel modelModel) async {
    final module = await modelModel.getModule(widget.route.moduleId);
    final outputIds = widget.route.outputs.map((cj) => cj.output.id).toList();
    final allOutputIds = module.outputs.map((e) => e.id);
    final allUnusedOutputIds =
        allOutputIds.where((id) => !outputIds.contains(id));
    final allUnusedOutputs = await Future.wait(
        allUnusedOutputIds.map((id) => modelModel.getOutput(id)));
    allUnusedOutputs.sort((a, b) => a.description.compareTo(b.description));

    final List<DropdownMenuItem<String>> result = [];
    for (var output in allUnusedOutputs) {
      if (output.hasBinaryOutput()) {
        result.add(DropdownMenuItem<String>(
          value: output.id,
          child: Text(
              "${output.description} -> ${_formatActiveText(output.binaryOutput)}"),
          onTap: () async {
            await modelModel.addRouteBinaryOutput(
                widget.route.id, output.id, true);
          },
        ));
        result.add(DropdownMenuItem<String>(
          value: output.id,
          child: Text(
              "${output.description} -> ${_formatInactiveText(output.binaryOutput)}"),
          onTap: () async {
            await modelModel.addRouteBinaryOutput(
                widget.route.id, output.id, false);
          },
        ));
      }
    }
    return result;
  }

  String _formatActiveText(BinaryOutput bo) {
    final result = bo.activeText;
    return result.isNotEmpty ? result : "Active";
  }

  String _formatInactiveText(BinaryOutput bo) {
    final result = bo.inactiveText;
    return result.isNotEmpty ? result : "Inactive";
  }

  Future<String> _formatRouteEvent(
      ModelModel modelModel, RouteEvent evt) async {
    final sensor = await modelModel.getSensor(evt.sensor.id);
    return sensor.description;
  }

  Future<List<DropdownMenuItem<String>>> _addRouteEventList(
      ModelModel modelModel) async {
    final module = await modelModel.getModule(widget.route.moduleId);
    final routeSensorIds =
        widget.route.events.map((evt) => evt.sensor.id).toList();
    final allSensorIds = module.sensors.map((e) => e.id);
    final allUnusedSensorIds =
        allSensorIds.where((id) => !routeSensorIds.contains(id));
    final allUnusedSensors = await Future.wait(
        allUnusedSensorIds.map((id) => modelModel.getSensor(id)));
    allUnusedSensors.sort((a, b) => a.description.compareTo(b.description));

    final List<DropdownMenuItem<String>> result = [];
    for (var sensor in allUnusedSensors) {
      result.add(DropdownMenuItem<String>(
        value: sensor.id,
        child: Text(sensor.description),
        onTap: () async {
          await modelModel.addRouteEvent(widget.route.id, sensor.id);
        },
      ));
    }
    return result;
  }
}
