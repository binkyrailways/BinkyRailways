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

import 'package:binky/colors.dart';
import 'package:flame/components.dart' as fc;
import 'package:flutter/material.dart';
import 'package:flame/extensions.dart';
import 'dart:math';

import 'entity_component.dart';
import '../api.dart' as mapi;

class RouteComponent extends EntityComponent {
  final mapi.Route model;
  final mapi.Module module;
  final List<mapi.Block> blocks;
  final List<mapi.Edge> edges;

  RouteComponent(
      {required this.model,
      required this.module,
      required this.blocks,
      required this.edges}) {
    size.x = module.hasWidth() ? max(1, module.width.toDouble()) : 1;
    size.y = module.hasHeight() ? max(1, module.height.toDouble()) : 1;
  }

  @override
  void render(Canvas canvas) {
    if (isVisible()) {
      final linePaint = Paint()..color = Colors.black;
      linePaint.style = PaintingStyle.stroke;

      canvas.save();
      final start = _getFrom();
      final end = _getTo();
      var path = Path();
      path.moveTo(start.x, start.y);
      path.lineTo(end.x, end.y);
      canvas.drawPath(path, linePaint);
      canvas.restore();
    }
  }

  bool isVisible() => false;

  Vector2 _getFrom() => _getEndpoint(model.from);
  Vector2 _getTo() => _getEndpoint(model.to);

  Vector2 _getEndpoint(mapi.Endpoint ep) {
    if (ep.hasBlock()) {
      final block = blocks.where((b) => b.id == ep.block.id).toList();
      if (block.isEmpty) {
        return fc.Vector2.zero();
      }
      return _getBlockPosition(block.first, ep.blockSide);
    }
    if (ep.hasEdge()) {
      final edge = edges.where((b) => b.id == ep.edge.id).toList();
      if (edge.isEmpty) {
        return fc.Vector2.zero();
      }
      return _getCenter(edge.first.position);
    }
    return fc.Vector2.zero();
  }

  Vector2 _getBlockPosition(mapi.Block block, mapi.BlockSide side) {
    final position = block.position;
    final center = _getCenter(position);
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

  Vector2 _getCenter(mapi.Position position) {
    final width = max(1, position.width.toDouble());
    final height = max(1, position.height.toDouble());
    final x = (position.hasX() ? position.x.toDouble() : 0) + width / 2;
    final y = (position.hasY() ? position.y.toDouble() : 0) + height / 2;
    return Vector2(x, y);
  }
}