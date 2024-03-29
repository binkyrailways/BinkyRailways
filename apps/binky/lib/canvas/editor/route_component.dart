// Copyright 2022 Ewout Prangsma
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

import '../route_component.dart' as common;
import '../view_settings.dart';
import '../../api.dart' as mapi;
import '../../models.dart';
import '../../editor/editor_context.dart';
import './module_game.dart';

class RouteComponent extends common.RouteComponent {
  final EditorContext editorCtx;
  final ModuleGame game;
  final mapi.Route route;

  RouteComponent(ViewSettings viewSettings,
      {required this.editorCtx,
      required mapi.Module module,
      required List<mapi.Block> blocks,
      required List<mapi.Edge> edges,
      required List<mapi.Junction> junctions,
      required List<mapi.Sensor> sensors,
      required ModelModel modelModel,
      required this.game,
      required this.route})
      : super(viewSettings,
            routeId: route.id,
            modelModel: modelModel,
            module: module,
            blocks: blocks,
            edges: edges,
            junctions: junctions,
            sensors: sensors);

  @override
  isVisible() {
    if (editorCtx.selector.idOf(EntityType.route) == routeId) {
      return true;
    }
    final selectedBlockId = editorCtx.selector.idOf(EntityType.block);
    if (route.from.hasBlock() && (route.from.block.id == selectedBlockId)) {
      return true;
    }
    if (route.to.hasBlock() && (route.to.block.id == selectedBlockId)) {
      return true;
    }

    return false;
  }
}
