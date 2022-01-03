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
  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider<EditorContext>(
        create: (context) => EditorContext(),
        child: Consumer<ModelModel>(builder: (context, model, child) {
          return FutureBuilder<Railway>(
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
              });
        }));
  }

  Widget _buildContent(BuildContext context, EditorContext editorCtx,
      ModelModel model, Railway railway) {
    if ((editorCtx.selector.entityType == EntityType.unknown) &&
        model.isRailwayLoaded()) {
      editorCtx.select(EntityType.railway, "", notify: false);
    }
    switch (editorCtx.selector.entityType) {
      case EntityType.railway:
        return SplitView(
          menu: const RailwayTree(),
          content: RailwaySettings(model: model, railway: railway),
        );
      case EntityType.modules:
        return const SplitView(
          menu: RailwayTree(),
          content: ModulesTree(),
        );
      case EntityType.module:
        return const SplitView(
          menu: ModuleTree(),
          content: ModuleCanvas(),
          endMenu: ModuleSettings(),
        );
      case EntityType.locs:
        return const SplitView(
          menu: RailwayTree(),
          content: LocsTree(),
        );
      case EntityType.loc:
        return const SplitView(
          menu: LocsTree(),
          content: LocSettings(),
        );
      case EntityType.locgroups:
        return const SplitView(
          menu: RailwayTree(),
          content: LocGroupsTree(),
        );
      case EntityType.locgroup:
        return const SplitView(
          menu: LocGroupsTree(),
          content: LocGroupSettings(),
        );
      case EntityType.commandstations:
        return const SplitView(
          menu: RailwayTree(),
          content: CommandStationsTree(),
        );
      case EntityType.commandstation:
        return const SplitView(
          menu: CommandStationTree(),
          content: CommandStationSettings(),
        );
      case EntityType.blocks:
        return const SplitView(
          menu: BlocksTree(),
          content: ModuleCanvas(),
        );
      case EntityType.block:
        return const SplitView(
          menu: BlocksTree(),
          content: ModuleCanvas(),
          endMenu: BlockSettings(),
        );
      case EntityType.blockgroups:
        return const SplitView(
          menu: BlockGroupsTree(),
          content: ModuleCanvas(),
        );
      case EntityType.blockgroup:
        return const SplitView(
          menu: BlockGroupsTree(),
          content: ModuleCanvas(),
          endMenu: BlockGroupSettings(),
        );
      case EntityType.edges:
        return const SplitView(
          menu: EdgesTree(),
          content: ModuleCanvas(),
        );
      case EntityType.edge:
        return const SplitView(
          menu: EdgesTree(),
          content: ModuleCanvas(),
          endMenu: EdgeSettings(),
        );
      case EntityType.junctions:
        return const SplitView(
          menu: JunctionsTree(),
          content: ModuleCanvas(),
        );
      case EntityType.junction:
        return const SplitView(
          menu: JunctionsTree(),
          content: ModuleCanvas(),
          endMenu: JunctionSettings(),
        );
      case EntityType.outputs:
        return const SplitView(
          menu: OutputsTree(),
          content: ModuleCanvas(),
        );
      case EntityType.output:
        return const SplitView(
          menu: OutputsTree(),
          content: ModuleCanvas(),
          endMenu: OutputSettings(),
        );
      case EntityType.routes:
        return const SplitView(
          menu: RoutesTree(),
          content: ModuleCanvas(),
        );
      case EntityType.route:
        return const SplitView(
          menu: RoutesTree(),
          content: ModuleCanvas(),
          endMenu: RouteSettings(),
        );
      case EntityType.sensors:
        return const SplitView(
          menu: SensorsTree(),
          content: ModuleCanvas(),
        );
      case EntityType.sensor:
        return const SplitView(
          menu: SensorsTree(),
          content: ModuleCanvas(),
          endMenu: SensorSettings(),
        );
      case EntityType.signals:
        return const SplitView(
          menu: SignalsTree(),
          content: ModuleCanvas(),
        );
      case EntityType.signal:
        return const SplitView(
          menu: SignalsTree(),
          content: ModuleCanvas(),
          endMenu: SignalSettings(),
        );
      case EntityType.binkynetlocalworkers:
        return const SplitView(
          menu: CommandStationTree(),
          content: BinkyNetLocalWorkersTree(),
        );
      case EntityType.binkynetlocalworker:
        return const SplitView(
          menu: BinkyNetLocalWorkerTree(),
          content: BinkyNetLocalWorkerSettings(),
        );
      case EntityType.binkynetdevices:
        return const SplitView(
          menu: BinkyNetLocalWorkerTree(),
          content: BinkyNetDevicesTree(),
        );
      case EntityType.binkynetdevice:
        return const SplitView(
          menu: BinkyNetDevicesTree(),
          content: BinkyNetDeviceSettings(),
        );
      case EntityType.binkynetobjects:
        return const SplitView(
          menu: BinkyNetLocalWorkerTree(),
          content: BinkyNetObjectsTree(),
        );
      case EntityType.binkynetobject:
        return const SplitView(
          menu: BinkyNetObjectsTree(),
          content: BinkyNetObjectSettings(),
        );
      default:
        return const Center(child: Text("No selection"));
    }
  }

  Widget? _buildLeading(
      BuildContext context, EditorContext editorCtx, ModelModel model) {
    final selector = editorCtx.selector;
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
      case EntityType.modules:
        return [
          SpeedDialChild(
            child: BinkyIcons.module,
            label: "Add module",
            onTap: () async {
              final added = await model.addModule();
              editorCtx.select(EntityType.module, added.id);
            },
          ),
        ];
      case EntityType.locs:
        return [
          SpeedDialChild(
            child: BinkyIcons.loc,
            label: "Add loc",
            onTap: () async {
              final added = await model.addLoc();
              editorCtx.select(EntityType.loc, added.id);
            },
          ),
        ];
      case EntityType.blocks:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.block,
              label: "Add block",
              onTap: () async {
                final added = await model.addBlock(moduleId);
                editorCtx.select(EntityType.block, added.id);
              },
            ),
          ];
        }
        return [];
      case EntityType.blockgroups:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.blockGroup,
              label: "Add block group",
              onTap: () async {
                final added = await model.addBlockGroup(moduleId);
                editorCtx.select(EntityType.blockgroup, added.id);
              },
            ),
          ];
        }
        return [];
      case EntityType.edges:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.edge,
              label: "Add edge",
              onTap: () async {
                final added = await model.addEdge(moduleId);
                editorCtx.select(EntityType.edge, added.id);
              },
            ),
          ];
        }
        return [];
      case EntityType.junctions:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.junction,
              label: "Add switch",
              onTap: () async {
                final added = await model.addSwitch(moduleId);
                editorCtx.select(EntityType.junction, added.id);
              },
            ),
          ];
        }
        return [];
      case EntityType.outputs:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.output,
              label: "Add binary output",
              onTap: () async {
                final added = await model.addBinaryOutput(moduleId);
                editorCtx.select(EntityType.output, added.id);
              },
            ),
          ];
        }
        return [];
      case EntityType.routes:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.route,
              label: "Add route",
              onTap: () async {
                final added = await model.addRoute(moduleId);
                editorCtx.select(EntityType.route, added.id);
              },
            ),
          ];
        }
        return [];
      case EntityType.sensors:
        final moduleId = selector.idOf(EntityType.module);
        if (moduleId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.sensor,
              label: "Add binary sensor",
              onTap: () async {
                final added = await model.addBinarySensor(moduleId);
                editorCtx.select(EntityType.sensor, added.id);
              },
            ),
          ];
        }
        return [];
      case EntityType.binkynetlocalworkers:
        final csId = selector.idOf(EntityType.commandstation);
        if (csId != null) {
          return [
            SpeedDialChild(
              child: BinkyIcons.binkynetlocalworker,
              label: "Add local worker",
              onTap: () async {
                final added = await model.addBinkyNetLocalWorker(csId);
                editorCtx.select(EntityType.binkynetlocalworker, added.id);
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
