// Copyright 2024 Ewout Prangsma
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
import 'package:flame/components.dart';
import '../../api.dart' as api;
import '../../models.dart';
import '../route_component.dart' as common;
import '../view_settings.dart';

class RouteComponent extends common.RouteComponent {
  final StateModel stateModel;

  RouteComponent(ViewSettings viewSettings,
      {required String routeId,
      required api.Module module,
      required ModelModel modelModel,
      required this.stateModel,
      required List<api.Block> blocks,
      required List<api.Edge> edges,
      required List<api.Junction> junctions,
      required List<api.Sensor> sensors})
      : super(viewSettings,
            routeId: routeId,
            module: module,
            modelModel: modelModel,
            blocks: blocks,
            edges: edges,
            junctions: junctions,
            sensors: sensors);

  @override
  bool isVisible() {
    final locs = stateModel.locs();
    for (var loc in locs) {
      if (loc.last.hasCurrentRoute() && loc.last.currentRoute.id == routeId) {
        return true;
      }
    }
    return false;
  }

  @override
  void render(Canvas canvas) {
    if (isVisible()) {
      final route = modelModel.getCachedRoute(routeId);
      if (route != null) {
        final linePaint = Paint()
          ..color = Colors.blue.withAlpha(128)
          ..strokeWidth = 4
          ..style = PaintingStyle.stroke;

        final start = getFrom(route);
        final end = getTo(route);
        final intermediates = getIntermediates(route, start, end);

        var path = Path();
        path.moveTo(start[0].x, start[0].y);
        for (var p in intermediates) {
          path.lineTo(p.x, p.y);
        }
        path.lineTo(end[0].x, end[0].y);
        canvas.drawPath(path, linePaint);
      }
    }
  }
}
