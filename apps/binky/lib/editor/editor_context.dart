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
}

class EntitySelector {
  final EntityType entityType;
  final String? id;
  final String? parentId;

  EntitySelector.initial()
      : entityType = EntityType.unknown,
        id = null,
        parentId = null;

  EntitySelector.railway(this.entityType)
      : id = null,
        parentId = null;

  EntitySelector.child(this.entityType, this.id) : parentId = null;

  EntitySelector.parentChild(this.entityType, this.parentId, this.id);

  EntitySelector back() {
    switch (entityType) {
      case EntityType.module:
        return EntitySelector.railway(EntityType.modules);
      case EntityType.loc:
        return EntitySelector.railway(EntityType.locs);
      case EntityType.locgroup:
        return EntitySelector.railway(EntityType.locgroups);
      case EntityType.commandstation:
        return EntitySelector.railway(EntityType.commandstations);
      case EntityType.modules:
      case EntityType.locs:
      case EntityType.locgroups:
      case EntityType.commandstations:
        return EntitySelector.railway(EntityType.railway);
      case EntityType.blocks:
        return EntitySelector.child(EntityType.module, id);
      case EntityType.block:
        return EntitySelector.child(EntityType.blocks, parentId);
      case EntityType.blockgroups:
        return EntitySelector.child(EntityType.module, id);
      case EntityType.blockgroup:
        return EntitySelector.child(EntityType.blockgroups, parentId);
      case EntityType.edges:
        return EntitySelector.child(EntityType.module, id);
      case EntityType.edge:
        return EntitySelector.child(EntityType.edges, parentId);
      case EntityType.junctions:
        return EntitySelector.child(EntityType.module, id);
      case EntityType.junction:
        return EntitySelector.child(EntityType.junctions, parentId);
      case EntityType.outputs:
        return EntitySelector.child(EntityType.module, id);
      case EntityType.output:
        return EntitySelector.child(EntityType.outputs, parentId);
      case EntityType.routes:
        return EntitySelector.child(EntityType.module, id);
      case EntityType.route:
        return EntitySelector.child(EntityType.routes, parentId);
      case EntityType.sensors:
        return EntitySelector.child(EntityType.module, id);
      case EntityType.sensor:
        return EntitySelector.child(EntityType.sensors, parentId);
      case EntityType.signals:
        return EntitySelector.child(EntityType.module, id);
      case EntityType.signal:
        return EntitySelector.child(EntityType.signals, parentId);
      case EntityType.binkynetlocalworkers:
        return EntitySelector.child(EntityType.commandstation, id);
      case EntityType.binkynetlocalworker:
        return EntitySelector.child(EntityType.commandstation, parentId);
      default:
        return this;
    }
  }
}

class EditorContext extends ChangeNotifier {
  EntitySelector selector = EntitySelector.initial();

  void select(EntitySelector value, {bool notify = true}) {
    selector = value;
    if (notify) {
      notifyListeners();
    }
  }
}
