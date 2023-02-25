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

import 'package:binky/canvas/view_settings.dart';
import 'package:binky/icons.dart';
import 'package:flutter_speed_dial/flutter_speed_dial.dart';

import '../components.dart';
import 'package:flutter/material.dart' hide RouteSettings;
import 'package:provider/provider.dart';

import '../models.dart';
import '../api.dart';

import './editor_context.dart';
import './binkynet_device_settings.dart';
import './binkynet_devices_tree.dart';
import './binkynet_local_worker_settings.dart';
import './binkynet_local_worker_tree.dart';
import './binkynet_local_workers_tree.dart';
import './binkynet_object_settings.dart';
import './binkynet_objects_tree.dart';
import './block_settings.dart';
import './blocks_tree.dart';
import './block_group_settings.dart';
import './block_groups_tree.dart';
import './command_station_settings.dart';
import './command_station_tree.dart';
import './command_stations_tree.dart';
import './edge_settings.dart';
import './edges_tree.dart';
import './junction_settings.dart';
import './junctions_tree.dart';
import './loc_settings.dart';
import './locs_tree.dart';
import './loc_group_settings.dart';
import './loc_groups_tree.dart';
import './module_settings.dart';
import './module_tree.dart';
import './modules_tree.dart';
import './output_settings.dart';
import './outputs_tree.dart';
import './railway_settings.dart';
import './railway_tree.dart';
import './route_settings.dart';
import './routes_tree.dart';
import './sensor_settings.dart';
import './sensors_tree.dart';
import './signal_settings.dart';
import './signals_tree.dart';
import '../canvas/editor/module_canvas.dart';

class EditorPage extends StatefulWidget {
  const EditorPage({Key? key}) : super(key: key);

  @override
  State<EditorPage> createState() => _EditorPageState();
}

class _EditorPageState extends State<EditorPage> {
  final ViewSettings _viewSettings = ViewSettings();

  @override
  Widget build(BuildContext context) {
    final model = Provider.of<ModelModel>(context, listen: false);
    return ChangeNotifierProvider<EditorContext>(
        create: (context) => _EditorPageContext(),
        child: FutureBuilder<Railway>(
            future: model.getRailway(),
            builder: (context, snapshot) {
              return Consumer<EditorContext>(
                  builder: (context, editorCtx, child) {
                if (!snapshot.hasData) {
                  return Scaffold(
                    appBar: AppBar(
                      // Here we take the value from the MyHomePage object that was created by
                      // the App.build method, and use it to set our appbar title.
                      title: const Text("Binky Railways"),
                    ),
                    body: Center(
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: const <Widget>[
                          Text('Loading railway...'),
                          CircularProgressIndicator(value: null),
                        ],
                      ),
                    ),
                  );
                }
                var rw = snapshot.data!;
                final addSpeedDialChildren =
                    _buildAddSpeedDialChildren(context, editorCtx, model);
                return Scaffold(
                  appBar: AppBar(
                    // Here we take the value from the MyHomePage object that was created by
                    // the App.build method, and use it to set our appbar title.
                    title: Text(model.title()),
                    leading: _buildLeading(context, editorCtx, model),
                    actions: _buildActions(context, editorCtx, model),
                  ),
                  body: _buildContent(context, editorCtx, model, rw),
                  floatingActionButton: addSpeedDialChildren.isNotEmpty
                      ? SpeedDial(
                          icon: Icons.add,
                          activeIcon: Icons.close_sharp,
                          children: addSpeedDialChildren,
                        )
                      : null, // This trailing comma makes auto-formatting nicer for build methods.
                );
              });
            }));
  }

  Widget _buildContent(BuildContext context, EditorContext editorCtx,
      ModelModel model, Railway railway) {
    if ((editorCtx.selector.entityType == EntityType.unknown) &&
        model.isRailwayLoaded()) {
      editorCtx.select(EntitySelector.railway(), notify: false);
    }
    final moduleId = editorCtx.selector.idOf(EntityType.module) ?? "";
    switch (editorCtx.selector.entityType) {
      case EntityType.railway:
        return SplitView(
          menu: const RailwayTree(),
          content: RailwaySettings(model: model, railway: railway),
        );
      case EntityType.modules:
        return const SplitView(
          menu: RailwayTree(),
          content: ModulesTree(withParents: false),
        );
      case EntityType.module:
        return SplitView(
          menu: const ModuleTree(),
          content: ModuleCanvas(
            key: Key("edit-canvas-$moduleId"),
            viewSettings: _viewSettings,
          ),
          endMenu: const ModuleSettings(),
          endMenuWidth: 300,
        );
      case EntityType.locs:
        return const SplitView(
          menu: RailwayTree(),
          content: LocsTree(withParents: false),
        );
      case EntityType.loc:
        return const SplitView(
          menu: LocsTree(withParents: true),
          content: LocSettings(),
        );
      case EntityType.locgroups:
        return const SplitView(
          menu: RailwayTree(),
          content: LocGroupsTree(withParents: false),
        );
      case EntityType.locgroup:
        return const SplitView(
          menu: LocGroupsTree(withParents: true),
          content: LocGroupSettings(),
        );
      case EntityType.commandstations:
        return const SplitView(
          menu: RailwayTree(),
          content: CommandStationsTree(
            withParents: false,
          ),
        );
      case EntityType.commandstation:
        return const SplitView(
          menu: CommandStationTree(),
          content: CommandStationSettings(),
        );
      case EntityType.blocks:
        return SplitView(
          menu: const BlocksTree(),
          content: ModuleCanvas(
            key: Key("edit-canvas-$moduleId"),
            viewSettings: _viewSettings,
          ),
        );
      case EntityType.block:
        return SplitView(
          menu: const BlocksTree(),
          content: ModuleCanvas(
            key: Key("edit-canvas-$moduleId"),
            viewSettings: _viewSettings,
          ),
          endMenu: const BlockSettings(),
          endMenuWidth: 300,
        );
      case EntityType.blockgroups:
        return SplitView(
          menu: const BlockGroupsTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
        );
      case EntityType.blockgroup:
        return SplitView(
          menu: const BlockGroupsTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
          endMenu: const BlockGroupSettings(),
          endMenuWidth: 300,
        );
      case EntityType.edges:
        return SplitView(
          menu: const EdgesTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
        );
      case EntityType.edge:
        return SplitView(
          menu: const EdgesTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
          endMenu: const EdgeSettings(),
          endMenuWidth: 300,
        );
      case EntityType.junctions:
        return SplitView(
          menu: const JunctionsTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
        );
      case EntityType.junction:
        return SplitView(
          menu: const JunctionsTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
          endMenu: const JunctionSettings(),
          endMenuWidth: 300,
        );
      case EntityType.outputs:
        return SplitView(
          menu: const OutputsTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
        );
      case EntityType.output:
        return SplitView(
          menu: const OutputsTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
          endMenu: const OutputSettings(),
          endMenuWidth: 300,
        );
      case EntityType.routes:
        return SplitView(
          menu: const RoutesTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
        );
      case EntityType.route:
        return SplitView(
          menu: const RoutesTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
          endMenu: const RouteSettings(),
          endMenuWidth: 300,
        );
      case EntityType.sensors:
        return SplitView(
          menu: const SensorsTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
        );
      case EntityType.sensor:
        return SplitView(
          menu: const SensorsTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
          endMenu: const SensorSettings(),
          endMenuWidth: 300,
        );
      case EntityType.signals:
        return SplitView(
          menu: const SignalsTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
        );
      case EntityType.signal:
        return SplitView(
          menu: const SignalsTree(),
          content: ModuleCanvas(
              key: Key("edit-canvas-$moduleId"), viewSettings: _viewSettings),
          endMenu: const SignalSettings(),
          endMenuWidth: 300,
        );
      case EntityType.binkynetlocalworkers:
        return const SplitView(
          menu: CommandStationTree(),
          content: BinkyNetLocalWorkersTree(withParents: false),
        );
      case EntityType.binkynetlocalworker:
        return const SplitView(
          menu: BinkyNetLocalWorkerTree(),
          content: BinkyNetLocalWorkerSettings(),
        );
      case EntityType.binkynetdevices:
        return const SplitView(
          menu: BinkyNetLocalWorkerTree(),
          content: BinkyNetDevicesTree(withParents: false),
        );
      case EntityType.binkynetdevice:
        return const SplitView(
          menu: BinkyNetDevicesTree(withParents: true),
          content: BinkyNetDeviceSettings(),
        );
      case EntityType.binkynetobjects:
        return const SplitView(
          menu: BinkyNetLocalWorkerTree(),
          content: BinkyNetObjectsTree(withParents: false),
        );
      case EntityType.binkynetobject:
        return const SplitView(
          menu: BinkyNetObjectsTree(withParents: true),
          content: BinkyNetObjectSettings(),
        );
      default:
        return const Center(child: Text("No selection"));
    }
  }

  Widget? _buildLeading(
      BuildContext context, EditorContext editorCtx, ModelModel model) {
    if (!editorCtx.canGoBack) {
      // No reason for back button
      return null;
    }
    return IconButton(
      onPressed: () => editorCtx.goBack(),
      icon: const Icon(Icons.arrow_back),
      tooltip: 'Back',
    );
  }

  List<Widget>? _buildActions(
      BuildContext context, EditorContext editorCtx, ModelModel model) {
    switch (editorCtx.selector.entityType) {
      default:
        return [
          IconButton(
            icon: const Icon(Icons.save),
            onPressed: () async {
              try {
                await model.save();
              } catch (err) {
                showErrorDialog(
                    context: context,
                    title: "Failed to save changes",
                    content: Text("$err"));
              }
            },
          ),
          IconButton(
            icon: const Icon(Icons.refresh),
            onPressed: () async {
              try {
                await model.reloadAll();
              } catch (err) {
                showErrorDialog(
                    context: context,
                    title: "Failed to reload all",
                    content: Text("$err"));
              }
            },
          ),
          IconButton(
            icon: const Icon(Icons.play_arrow_rounded),
            tooltip: "Run",
            onPressed: () async {
              final state = Provider.of<StateModel>(context, listen: false);
              try {
                await state.enableRunMode();
              } catch (err) {
                showErrorDialog(
                    context: context,
                    title: "Failed to enable run mode",
                    content: Text("$err"));
              }
            },
          ),
          IconButton(
            icon: const Icon(Icons.play_circle_outline_rounded),
            tooltip: "Run Virtual",
            onPressed: () async {
              final state = Provider.of<StateModel>(context, listen: false);
              try {
                await state.enableRunMode(virtual: true);
              } catch (err) {
                showErrorDialog(
                    context: context,
                    title: "Failed to enable virtual run mode",
                    content: Text("$err"));
              }
            },
          ),
        ];
    }
  }

  List<SpeedDialChild> _buildAddSpeedDialChildren(
      BuildContext context, EditorContext editorCtx, ModelModel model) {
    final selector = editorCtx.selector;
    switch (selector.entityType) {
      case EntityType.module:
      case EntityType.modules:
        return [
          SpeedDialChild(
            child: BinkyIcons.module,
            label: "Add module",
            onTap: () async {
              final added = await model.addModule();
              editorCtx.select(EntitySelector.module(added, null));
            },
          ),
        ];
      case EntityType.loc:
      case EntityType.locs:
        return [
          SpeedDialChild(
            child: BinkyIcons.loc,
            label: "Add loc",
            onTap: () async {
              final added = await model.addLoc();
              editorCtx.select(EntitySelector.loc(added));
            },
          ),
        ];
      case EntityType.locgroup:
      case EntityType.locgroups:
        return [
          SpeedDialChild(
            child: BinkyIcons.locGroup,
            label: "Add loc group",
            onTap: () async {
              final added = await model.addLocGroup();
              editorCtx.select(EntitySelector.locGroup(added));
            },
          ),
        ];
      case EntityType.block:
      case EntityType.blocks:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.block,
              label: "Add block",
              onTap: () async {
                final added = await model.addBlock(moduleId);
                editorCtx.select(EntitySelector.block(added));
              },
            ),
          ];
        }
        return [];
      case EntityType.blockgroup:
      case EntityType.blockgroups:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.blockGroup,
              label: "Add block group",
              onTap: () async {
                final added = await model.addBlockGroup(moduleId);
                editorCtx.select(EntitySelector.blockGroup(added));
              },
            ),
          ];
        }
        return [];
      case EntityType.edge:
      case EntityType.edges:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.edge,
              label: "Add edge",
              onTap: () async {
                final added = await model.addEdge(moduleId);
                editorCtx.select(EntitySelector.edge(added));
              },
            ),
          ];
        }
        return [];
      case EntityType.junction:
      case EntityType.junctions:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.junction,
              label: "Add switch",
              onTap: () async {
                final added = await model.addSwitch(moduleId);
                editorCtx.select(EntitySelector.junction(added));
              },
            ),
          ];
        }
        return [];
      case EntityType.output:
      case EntityType.outputs:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.output,
              label: "Add binary output",
              onTap: () async {
                final added = await model.addBinaryOutput(moduleId);
                editorCtx.select(EntitySelector.output(added));
              },
            ),
          ];
        }
        return [];
      case EntityType.route:
      case EntityType.routes:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.route,
              label: "Add route",
              onTap: () async {
                final added = await model.addRoute(moduleId);
                editorCtx.select(EntitySelector.route(added));
              },
            ),
          ];
        }
        return [];
      case EntityType.sensor:
      case EntityType.sensors:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.sensor,
              label: "Add binary sensor",
              onTap: () async {
                final added = await model.addBinarySensor(moduleId);
                editorCtx.select(EntitySelector.sensor(added));
              },
            ),
          ];
        }
        return [];
      case EntityType.binkynetlocalworker:
      case EntityType.binkynetlocalworkers:
        final csId = selector.idOf(EntityType.commandstation);
        if (csId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.binkynetlocalworker,
              label: "Add local worker",
              onTap: () async {
                final added = await model.addBinkyNetLocalWorker(csId);
                editorCtx.select(EntitySelector.binkynetLocalWorker(added));
              },
            ),
          ];
        }
        return [];
      case EntityType.binkynetdevice:
        final List<SpeedDialChild> children = [];
        final lwId = selector.idOf(EntityType.binkynetlocalworker);
        final devId = selector.idOf(EntityType.binkynetdevice);
        if (lwId != null) {
          children.add(SpeedDialChild(
            child: BinkyIcons.binkynetdevice,
            label: "Add device",
            onTap: () async {
              final lw = await model.getBinkyNetLocalWorker(lwId);
              final added = await model.addBinkyNetDevice(lwId);
              editorCtx.select(EntitySelector.binkynetDevice(lw, added));
            },
          ));
          if (devId != null) {
            final lw = model.getCachedBinkyNetLocalWorker(lwId);
            if (lw != null) {
              final devList = lw.devices.where((x) => x.id == devId).toList();
              if (devList.length == 1) {
                final dev = devList[0];
                if (dev.canAddMgv93Group) {
                  children.add(SpeedDialChild(
                    child: BinkyIcons.binkynetobject,
                    label: "Add MGV93",
                    onTap: () async {
                      final lw = await model.getBinkyNetLocalWorker(lwId);
                      await model.addBinkyNetObjectsGroup(
                          lwId, dev.deviceId, BinkyNetObjectsGroupType.MGV93);
                      editorCtx.select(EntitySelector.binkynetObjects(lw));
                    },
                  ));
                }
              }
            }
          }
        }
        return children;
      case EntityType.binkynetdevices:
        final lwId = selector.idOf(EntityType.binkynetlocalworker);
        if (lwId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.binkynetdevice,
              label: "Add device",
              onTap: () async {
                final lw = await model.getBinkyNetLocalWorker(lwId);
                final added = await model.addBinkyNetDevice(lwId);
                editorCtx.select(EntitySelector.binkynetDevice(lw, added));
              },
            ),
          ];
        }
        return [];
      case EntityType.binkynetobject:
        final List<SpeedDialChild> children = [];
        final lwId = selector.idOf(EntityType.binkynetlocalworker);
        final objId = selector.idOf(EntityType.binkynetobject);
        if (lwId != null) {
          children.add(SpeedDialChild(
            child: BinkyIcons.binkynetobject,
            label: "Add object",
            onTap: () async {
              final lw = await model.getBinkyNetLocalWorker(lwId);
              final added = await model.addBinkyNetObject(lwId);
              editorCtx.select(EntitySelector.binkynetObject(lw, added));
            },
          ));
          if (objId != null) {
            final lw = model.getCachedBinkyNetLocalWorker(lwId);
            if (lw != null) {
              final objList = lw.objects.where((x) => x.id == objId).toList();
              final rw = model.getCachedRailway();
              if ((objList.length == 1) && (rw != null)) {
                final obj = objList[0];
                final address = "BinkyNet ${lw.alias}/${obj.objectId}";
                if (obj.objectType == BinkyNetObjectType.BINARYSENSOR) {
                  rw.modules.forEach((modRef) {
                    final module = model.getCachedModule(modRef.id);
                    if (module != null) {
                      final sensors = module.sensors
                          .map((sRef) => model.getCachedSensor(sRef.id))
                          .where((x) => x != null);
                      if (!sensors.any((x) => x!.address == address)) {
                        children.add(SpeedDialChild(
                          child: BinkyIcons.sensor,
                          label: "Add Sensor to module ${module.description}",
                          onTap: () async {
                            final sensor =
                                await model.addBinarySensor(module.id);
                            sensor.description = "${lw.alias}/${obj.objectId}";
                            sensor.address = address;
                            await model.updateSensor(sensor);
                            editorCtx.select(EntitySelector.sensor(sensor));
                          },
                        ));
                      }
                    }
                  });
                } else if (obj.objectType == BinkyNetObjectType.SERVOSWITCH) {
                  rw.modules.forEach((modRef) {
                    final module = model.getCachedModule(modRef.id);
                    if (module != null) {
                      final junctions = module.junctions
                          .map((jRef) => model.getCachedJunction(jRef.id))
                          .where((x) => x != null);
                      if (!junctions
                          .any((x) => x!.switch_6.address == address)) {
                        children.add(SpeedDialChild(
                          child: BinkyIcons.junction,
                          label: "Add Switch to module ${module.description}",
                          onTap: () async {
                            final junction = await model.addSwitch(module.id);
                            junction.description =
                                "${lw.alias}/${obj.objectId}";
                            junction.switch_6.address = address;
                            junction.switch_6.feedbackAddress = address;
                            await model.updateJunction(junction);
                            editorCtx.select(EntitySelector.junction(junction));
                          },
                        ));
                      }
                    }
                  });
                }
              }
            }
          }
        }
        return children;
      case EntityType.binkynetobjects:
        final lwId = selector.idOf(EntityType.binkynetlocalworker);
        if (lwId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.binkynetobject,
              label: "Add object",
              onTap: () async {
                final lw = await model.getBinkyNetLocalWorker(lwId);
                final added = await model.addBinkyNetObject(lwId);
                editorCtx.select(EntitySelector.binkynetObject(lw, added));
              },
            ),
          ];
        }
        return [];
      default:
        return [];
    }
  }
}

class _EditorPageContext extends ChangeNotifier implements EditorContext {
  @override
  EntitySelector selector = EntitySelector.initial();

  @override
  bool get canGoBack => selector.parent != null;

  // If set, the program is in running state
  @override
  bool get isRunningState => false;

  @override
  void goBack() {
    selector = selector.parent ?? selector;
    notifyListeners();
  }

  @override
  void select(EntitySelector selector, {bool notify = true}) {
    this.selector = selector;
    if (notify) {
      notifyListeners();
    }
  }
}
