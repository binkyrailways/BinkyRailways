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

import 'package:binky/api.dart';
import 'package:flutter/material.dart' hide Route;

enum EntityType {
  unknown,
  railway,
  modules,
  module,
  locs,
  loc,
  locgroups,
  locgroup,
  commandstations,
  commandstation,
  block,
  blocks,
  blockgroup,
  blockgroups,
  edge,
  edges,
  junction,
  junctions,
  output,
  outputs,
  route,
  routes,
  sensor,
  sensors,
  signal,
  signals,
  binkynetlocalworker,
  binkynetlocalworkers,
  binkynetdevice,
  binkynetdevices,
  binkynetobject,
  binkynetobjects,
}

class EntitySelector {
  final EntityType entityType;
  final String? _id;
  final EntitySelector? parent;

  EntitySelector.initial()
      : entityType = EntityType.unknown,
        _id = null,
        parent = null;

  EntitySelector.railway()
      : entityType = EntityType.railway,
        _id = null,
        parent = null;

  EntitySelector.modules()
      : entityType = EntityType.modules,
        _id = null,
        parent = EntitySelector.railway();

  EntitySelector.locs()
      : entityType = EntityType.locs,
        _id = null,
        parent = EntitySelector.railway();

  EntitySelector.locGroups()
      : entityType = EntityType.locgroups,
        _id = null,
        parent = EntitySelector.railway();

  EntitySelector.commandStations()
      : entityType = EntityType.commandstations,
        _id = null,
        parent = EntitySelector.railway();

  EntitySelector.module(Module? entity, String? id)
      : entityType = EntityType.module,
        _id = entity?.id ?? id,
        parent = EntitySelector.modules();

  EntitySelector.loc(Loc entity)
      : entityType = EntityType.loc,
        _id = entity.id,
        parent = EntitySelector.locs();

  EntitySelector.locGroup(LocGroup entity)
      : entityType = EntityType.locgroup,
        _id = entity.id,
        parent = EntitySelector.locGroups();

  EntitySelector.commandStation(CommandStation? entity, String? id)
      : entityType = EntityType.commandstation,
        _id = entity?.id ?? id,
        parent = EntitySelector.commandStations();

  EntitySelector.blocks(Module? entity, String? id)
      : entityType = EntityType.blocks,
        _id = entity?.id ?? id,
        parent = EntitySelector.module(entity, id);

  EntitySelector.blockGroups(Module? entity, String? id)
      : entityType = EntityType.blockgroups,
        _id = entity?.id ?? id,
        parent = EntitySelector.module(entity, id);

  EntitySelector.edges(Module? entity, String? id)
      : entityType = EntityType.edges,
        _id = entity?.id ?? id,
        parent = EntitySelector.module(entity, id);

  EntitySelector.junctions(Module? entity, String? id)
      : entityType = EntityType.junctions,
        _id = entity?.id ?? id,
        parent = EntitySelector.module(entity, id);

  EntitySelector.outputs(Module? entity, String? id)
      : entityType = EntityType.outputs,
        _id = entity?.id ?? id,
        parent = EntitySelector.module(entity, id);

  EntitySelector.routes(Module? entity, String? id)
      : entityType = EntityType.routes,
        _id = entity?.id ?? id,
        parent = EntitySelector.module(entity, id);

  EntitySelector.sensors(Module? entity, String? id)
      : entityType = EntityType.sensors,
        _id = entity?.id ?? id,
        parent = EntitySelector.module(entity, id);

  EntitySelector.signals(Module? entity, String? id)
      : entityType = EntityType.signals,
        _id = entity?.id ?? id,
        parent = EntitySelector.module(entity, id);

  EntitySelector.block(Block entity)
      : entityType = EntityType.block,
        _id = entity.id,
        parent = EntitySelector.blocks(null, entity.moduleId);

  EntitySelector.blockGroup(BlockGroup entity)
      : entityType = EntityType.blockgroup,
        _id = entity.id,
        parent = EntitySelector.blockGroups(null, entity.moduleId);

  EntitySelector.edge(Edge entity)
      : entityType = EntityType.edge,
        _id = entity.id,
        parent = EntitySelector.edges(null, entity.moduleId);

  EntitySelector.junction(Junction entity)
      : entityType = EntityType.junction,
        _id = entity.id,
        parent = EntitySelector.junctions(null, entity.moduleId);

  EntitySelector.output(Output entity)
      : entityType = EntityType.output,
        _id = entity.id,
        parent = EntitySelector.outputs(null, entity.moduleId);

  EntitySelector.route(Route entity)
      : entityType = EntityType.route,
        _id = entity.id,
        parent = EntitySelector.routes(null, entity.moduleId);

  EntitySelector.sensor(Sensor entity)
      : entityType = EntityType.sensor,
        _id = entity.id,
        parent = EntitySelector.sensors(null, entity.moduleId);

  EntitySelector.signal(Signal entity)
      : entityType = EntityType.signal,
        _id = entity.id,
        parent = EntitySelector.signals(null, entity.moduleId);

  EntitySelector.binkynetLocalWorkers(CommandStation? entity, String? id)
      : entityType = EntityType.binkynetlocalworkers,
        _id = entity?.id ?? id,
        parent = EntitySelector.commandStation(entity, id);

  EntitySelector.binkynetLocalWorker(BinkyNetLocalWorker entity)
      : entityType = EntityType.binkynetlocalworker,
        _id = entity.id,
        parent =
            EntitySelector.binkynetLocalWorkers(null, entity.commandStationId);

  EntitySelector.binkynetDevices(BinkyNetLocalWorker entity)
      : entityType = EntityType.binkynetdevices,
        _id = entity.id,
        parent = EntitySelector.binkynetLocalWorker(entity);

  EntitySelector.binkynetObjects(BinkyNetLocalWorker entity)
      : entityType = EntityType.binkynetobjects,
        _id = entity.id,
        parent = EntitySelector.binkynetLocalWorker(entity);

  EntitySelector.binkynetDevice(BinkyNetLocalWorker lw, BinkyNetDevice entity)
      : entityType = EntityType.binkynetdevice,
        _id = entity.id,
        parent = EntitySelector.binkynetDevices(lw);

  EntitySelector.binkynetObject(BinkyNetLocalWorker lw, BinkyNetObject entity)
      : entityType = EntityType.binkynetobject,
        _id = entity.id,
        parent = EntitySelector.binkynetObjects(lw);

  String? get parentId => parent?._id;

  String? idOf(EntityType entityType) {
    if (entityType == this.entityType) {
      return _id;
    }
    return parent?.idOf(entityType);
  }
}

abstract class EditorContext extends ChangeNotifier {
  // Gets the current entity selector
  EntitySelector get selector;

  // If set, goBack can be called successfully.
  bool get canGoBack;

  // If set, the program is in running state
  bool get isRunningState;

  void goBack();

  void select(EntitySelector selector, {bool notify = true});
}
