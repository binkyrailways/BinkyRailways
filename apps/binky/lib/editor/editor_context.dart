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

import 'package:grpc/grpc.dart';

import '../api/generated/br_types.pb.dart';

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
}

class EditorContext {
  final EntityType entityType;
  final String? moduleId;

  EditorContext.initial()
      : entityType = EntityType.unknown,
        moduleId = null;

  EditorContext.railway(this.entityType) : moduleId = null;

  EditorContext.module(this.entityType, this.moduleId);

  EditorContext back() {
    switch (entityType) {
      case EntityType.modules:
      case EntityType.locs:
      case EntityType.locgroups:
      case EntityType.commandstations:
        return EditorContext.railway(EntityType.railway);
      default:
        return this;
    }
  }
}

typedef ContextSetter = void Function(EditorContext newContext);
