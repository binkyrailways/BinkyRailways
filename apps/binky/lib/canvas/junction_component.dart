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

import 'package:flame/components.dart' as fc;
import 'package:flutter/material.dart';
import 'package:flame/extensions.dart';
import 'dart:math';

import 'entity_component.dart';
import '../api.dart' as mapi;

class JunctionComponent extends EntityComponent {
  final mapi.Junction model;

  JunctionComponent({required this.model}) {
    loadPosition(model.position);
  }

  @override
  void render(Canvas canvas) {
    final minDim = min(size.x, size.y);
    final fc.TextPaint textPaint = fc.TextPaint(
      style: TextStyle(
        fontSize: minDim * 0.8,
        color: textColor(),
        fontWeight: isHovered ? FontWeight.bold : FontWeight.normal,
      ),
    );
    canvas.save();

    // Clip rrect
    canvas.clipRRect(
        RRect.fromRectAndRadius(size.toRect(), Radius.circular(minDim / 3)));
    // Draw background
    canvas.drawPaint(Paint()..color = backgroundColor());
    // Draw switch direction (if switch)
    if (model.hasSwitch_6()) {
      final strokeWith = minDim / 4;
      final linePaint = Paint()
        ..strokeWidth = strokeWith
        ..color = switchColor();
      if (switchDirection() == mapi.SwitchDirection.STRAIGHT) {
        final y = (size.y - strokeWith) / 2;
        canvas.drawLine(Offset(0, y), Offset(size.x, y), linePaint);
      } else {
        canvas.drawLine(Offset.zero, size.toOffset(), linePaint);
      }
    }
    canvas.restore();
    // Draw description
    if (showDescription()) {
      textPaint.render(
          canvas, model.description, Vector2(size.x / 2, size.y / 2 + height),
          anchor: fc.Anchor.center);
    }
  }

  Color backgroundColor() => Colors.white;
  Color textColor() => Colors.black;
  Color switchColor() => Colors.red.shade200;

  mapi.SwitchDirection switchDirection() => mapi.SwitchDirection.STRAIGHT;
  bool showDescription() => isHovered;
}
