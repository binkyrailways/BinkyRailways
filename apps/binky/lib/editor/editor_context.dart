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
  binkynetdevice,
  binkynetdevices,
}

class EntitySelector {
  final EntityType entityType;
  final String? id;
  final EntitySelector? parent;

  EntitySelector.initial()
      : entityType = EntityType.unknown,
        id = null,
        parent = null;

  EntitySelector.railway(this.entityType)
      : id = null,
        parent = null;

  EntitySelector.child(this.entityType, this.id) : parent = null;

  EntitySelector.parentChild(this.entityType, this.parent, this.id);

  String? get parentId => parent?.id;

  String? idOf(EntityType entityType) {
    if (entityType == this.entityType) {
      return id;
    }
    return parent?.idOf(entityType);
  }
}

class EditorContext extends ChangeNotifier {
  EntitySelector selector = EntitySelector.initial();

  bool get canGoBack => selector.parent != null;

  void goBack() {
    selector = selector.parent ?? selector;
    notifyListeners();
  }

  void select(EntityType entityType, String id, {bool notify = true}) {
    if (id.isEmpty) {
      selector = EntitySelector.child(entityType, id);
    } else if (selector.entityType == entityType) {
      selector = EntitySelector.parentChild(entityType, selector.parent, id);
    } else {
      selector = EntitySelector.parentChild(entityType, selector, id);
    }
    if (notify) {
      notifyListeners();
    }
  }
}
