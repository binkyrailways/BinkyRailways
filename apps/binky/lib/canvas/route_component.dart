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

import 'package:binky/canvas/geometry.dart';
import 'package:flame/components.dart' as fc;
import 'package:flutter/material.dart';
import 'package:flame/extensions.dart';
import 'dart:math';

import 'entity_component.dart';
import 'view_settings.dart';
import '../api.dart' as mapi;
import '../models.dart';

class RouteComponent extends EntityComponent {
  final String routeId;
  final mapi.Module module;
  final ModelModel modelModel;
  final List<mapi.Block> blocks;
  final List<mapi.Edge> edges;
  final List<mapi.Junction> junctions;
  final List<mapi.Sensor> sensors;

  RouteComponent(ViewSettings viewSettings,
      {required this.routeId,
      required this.module,
      required this.modelModel,
      required this.blocks,
      required this.edges,
      required this.junctions,
      required this.sensors})
      : super(viewSettings) {
    size.x = module.hasWidth() ? max(1, module.width.toDouble()) : 1;
    size.y = module.hasHeight() ? max(1, module.height.toDouble()) : 1;
  }

  @override
  void render(Canvas canvas) {
    if (isVisible()) {
      final route = modelModel.getCachedRoute(routeId);
      if (route != null) {
        final linePaint = Paint()..color = Colors.black;
        linePaint.style = PaintingStyle.stroke;

        canvas.save();
        final start = _getFrom(route);
        final end = _getTo(route);
        final intermediates = _getIntermediates(route, start, end);
        var path = Path();
        path.moveTo(start[0].x, start[0].y);
        for (var p in intermediates) {
          path.lineTo(p.x, p.y);
        }
        path.lineTo(end[0].x, end[0].y);
        canvas.drawPath(path, linePaint);
        canvas.restore();
      }
    }
  }

  bool isVisible() => false;

  List<Vector2> _getFrom(mapi.Route route) => _getEndpoint(route.from);
  List<Vector2> _getTo(mapi.Route route) => _getEndpoint(route.to);

  List<Vector2> _getIntermediates(
      mapi.Route route, List<Vector2> start, List<Vector2> end) {
    final List<Vector2> list = [];
    list.add(end[0]);
    final fromBB = _getEndpointBoundingBox(route.from);
    final toBB = _getEndpointBoundingBox(route.to);
    for (var jws in route.crossingJunctions) {
      final junction = junctions.where((b) => b.id == jws.junction.id).toList();
      if (junction.isNotEmpty) {
        final p = Geometry.getCenter(junction.first.position);
        list.add(p);
      }
    }
    for (var evt in route.events) {
      final sensor = sensors.where((b) => b.id == evt.sensor.id).toList();
      if (sensor.isEmpty) {
        continue;
      }
      final r = Geometry.getBoundingBox(sensor.first.position);
      if (r.overlaps(fromBB) || r.overlaps(toBB)) {
        continue;
      }
      final p = Geometry.getCenter(sensor.first.position);
      list.add(p);
    }
    final List<Vector2> ordered = [];
    var p = start[0];
    while (list.isNotEmpty) {
      // Find the element with the shortest distance from p.
      var shortestDistanceIndex = 0;
      var shortestDistance = _distance(p, list[0]);
      for (var i = 1; i < list.length; i++) {
        final d = _distance(p, list[i]);
        if (d < shortestDistance) {
          shortestDistance = d;
          shortestDistanceIndex = i;
        }
      }
      // Take the element with shortest distance and remove from list
      p = list[shortestDistanceIndex];
      ordered.add(p);
      list.removeAt(shortestDistanceIndex);
    }
    return ordered;
  }

  double _distance(Vector2 a, Vector2 b) {
    return a.distanceTo(b);
  }

  List<Vector2> _getEndpoint(mapi.Endpoint ep) {
    if (ep.hasBlock()) {
      final block = blocks.where((b) => b.id == ep.block.id).toList();
      if (block.isEmpty) {
        return [fc.Vector2.zero()];
      }
      return [
        _getBlockPosition(block.first, ep.blockSide),
        _getBlockPosition(block.first, ep.blockSide.invert())
      ];
    }
    if (ep.hasEdge()) {
      final edge = edges.where((b) => b.id == ep.edge.id).toList();
      if (edge.isEmpty) {
        return [fc.Vector2.zero()];
      }
      return [Geometry.getCenter(edge.first.position)];
    }
    return [fc.Vector2.zero()];
  }

  Rect _getEndpointBoundingBox(mapi.Endpoint ep) {
    if (ep.hasBlock()) {
      final block = blocks.where((b) => b.id == ep.block.id).toList();
      if (block.isEmpty) {
        return Rect.zero;
      }
      return Geometry.getBoundingBox(block.first.position);
    }
    if (ep.hasEdge()) {
      final edge = edges.where((b) => b.id == ep.edge.id).toList();
      if (edge.isEmpty) {
        return Rect.zero;
      }
      return Geometry.getBoundingBox(edge.first.position);
    }
    return Rect.zero;
  }

  Vector2 _getBlockPosition(mapi.Block block, mapi.BlockSide side) {
    final position = block.position;
    final center = Geometry.getCenter(position);
    final angle = radians(position.rotation.toDouble());
    var radius = max(1, position.width.toDouble()) / 2;
    if (side == mapi.BlockSide.BACK) {
      radius *= -1;
    }
    if (block.reverseSides) {
      radius *= -1;
    }
    final dx = cos(angle) * radius;
    final dy = sin(angle) * radius;
    return Vector2(center.x + dx, center.y + dy);
  }
}
