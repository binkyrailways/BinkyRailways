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
  junction,
  junctions,
  output,
  outputs,
}

class EntitySelector {
  final EntityType entityType;
  final String? moduleId;
  final String? locId;
  final String? locGroupId;
  final String? commandStationId;
  final String? blockId;
  final String? junctionId;
  final String? outputId;

  EntitySelector.initial()
      : entityType = EntityType.unknown,
        moduleId = null,
        locId = null,
        locGroupId = null,
        commandStationId = null,
        blockId = null,
        junctionId = null,
        outputId = null;

  EntitySelector.railway(this.entityType)
      : moduleId = null,
        locId = null,
        locGroupId = null,
        commandStationId = null,
        blockId = null,
        junctionId = null,
        outputId = null;

  EntitySelector.module(this.entityType, this.moduleId)
      : locId = null,
        locGroupId = null,
        commandStationId = null,
        blockId = null,
        junctionId = null,
        outputId = null;

  EntitySelector.loc(this.entityType, this.locId)
      : moduleId = null,
        locGroupId = null,
        commandStationId = null,
        blockId = null,
        junctionId = null,
        outputId = null;

  EntitySelector.locGroup(this.entityType, this.locGroupId)
      : moduleId = null,
        locId = null,
        commandStationId = null,
        blockId = null,
        junctionId = null,
        outputId = null;

  EntitySelector.commandStation(this.entityType, this.commandStationId)
      : moduleId = null,
        locId = null,
        locGroupId = null,
        blockId = null,
        junctionId = null,
        outputId = null;

  EntitySelector.block(this.entityType, this.moduleId, this.blockId)
      : locId = null,
        locGroupId = null,
        commandStationId = null,
        junctionId = null,
        outputId = null;

  EntitySelector.junction(this.entityType, this.moduleId, this.junctionId)
      : locId = null,
        locGroupId = null,
        commandStationId = null,
        blockId = null,
        outputId = null;

  EntitySelector.output(this.entityType, this.moduleId, this.outputId)
      : locId = null,
        locGroupId = null,
        commandStationId = null,
        blockId = null,
        junctionId = null;

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
        return EntitySelector.module(EntityType.module, moduleId);
      case EntityType.block:
        return EntitySelector.module(EntityType.blocks, moduleId);
      case EntityType.junctions:
        return EntitySelector.module(EntityType.module, moduleId);
      case EntityType.junction:
        return EntitySelector.module(EntityType.junctions, moduleId);
      case EntityType.outputs:
        return EntitySelector.module(EntityType.module, moduleId);
      case EntityType.output:
        return EntitySelector.module(EntityType.outputs, moduleId);
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
