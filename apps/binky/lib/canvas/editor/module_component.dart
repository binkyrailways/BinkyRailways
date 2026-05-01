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

import 'package:flame/components.dart';
import 'package:flame/input.dart';
import '../module_component.dart' as common;
import '../view_settings.dart';
import '../../api.dart' as mapi;
import '../../models.dart';
import '../../editor/editor_context.dart';
import './block_component.dart';
import './junction_component.dart';
import './output_component.dart';
import './route_component.dart';
import './sensor_component.dart';
import './signal_component.dart';
import './rail_point_component.dart';
import './module_game.dart';

class ModuleComponent extends common.ModuleComponent with Tappable {
  final ModuleGame game;
  ModuleComponent(ViewSettings viewSettings,
      {required mapi.Module model, required this.game})
      : super(viewSettings, model: model);

  @override
  bool onTapUp(TapUpInfo event) {
    if (game.shiftPressed()) {
      final selector = game.editorCtx.selector;
      if (selector.entityType == EntityType.railpoints) {
        final localPos = event.eventPosition.game;
        game.modelModel.addRailPoint(game.moduleId).then((rp) async {
          var update = rp.clone();
          update.position.x = localPos.x.toInt();
          update.position.y = localPos.y.toInt();
          await game.modelModel.updateRailPoint(update);
          await game.reload();
        });
        return false;
      }
    }
    return super.onTapUp(event);
  }

  Future<void> loadChildren(
      EditorContext editorCtx, ModelModel modelModel) async {
    // Load background image (if any)
    await loadBackgroundImage(modelModel);

    final List<mapi.Block> blocks = [];
    final List<mapi.Edge> edges = [];
    final List<mapi.Junction> junctions = [];
    final List<mapi.Sensor> sensors = [];
    final List<mapi.Signal> signals = [];
    final List<mapi.RailPoint> railPoints = [];

    // Load blocks
    for (var blockRef in model.blocks) {
      final block = await modelModel.getBlock(blockRef.id);
      blocks.add(block);
      add(BlockComponent(viewSettings,
          editorCtx: editorCtx, model: block, modelModel: modelModel));
    }
    // Load edges
    for (var edgeRef in model.edges) {
      final edge = await modelModel.getEdge(edgeRef.id);
      edges.add(edge);
    }
    // Load junctions
    for (var junctionRef in model.junctions) {
      final junction = await modelModel.getJunction(junctionRef.id);
      junctions.add(junction);
      add(JunctionComponent(viewSettings,
          editorCtx: editorCtx, model: junction, modelModel: modelModel));
    }
    // Load outputs
    for (var outputRef in model.outputs) {
      final output = await modelModel.getOutput(outputRef.id);
      add(OutputComponent(viewSettings,
          editorCtx: editorCtx, model: output, modelModel: modelModel));
    }
    // Load sensors
    for (var sensorRef in model.sensors) {
      final sensor = await modelModel.getSensor(sensorRef.id);
      sensors.add(sensor);
      add(SensorComponent(viewSettings,
          editorCtx: editorCtx,
          model: sensor,
          modelModel: modelModel,
          game: game));
    }
    // Load signals
    for (var signalRef in model.signals) {
      final signal = await modelModel.getSignal(signalRef.id);
      signals.add(signal);
      add(SignalComponent(viewSettings,
          editorCtx: editorCtx,
          model: signal,
          modelModel: modelModel,
          game: game));
    }
    // Load rail points
    for (var railPointRef in model.railPoints) {
      final railPoint = await modelModel.getRailPoint(railPointRef.id);
      railPoints.add(railPoint);
      add(RailPointComponent(viewSettings,
          editorCtx: editorCtx,
          model: railPoint,
          modelModel: modelModel,
          game: game));
    }

    // Load routes
    for (var routeRef in model.routes) {
      final route = await modelModel.getRoute(routeRef.id);
      add(RouteComponent(viewSettings,
          editorCtx: editorCtx,
          module: model,
          blocks: blocks,
          edges: edges,
          junctions: junctions,
          sensors: sensors,
          railPoints: railPoints,
          modelModel: modelModel,
          game: game,
          route: route));
    }
  }
}
