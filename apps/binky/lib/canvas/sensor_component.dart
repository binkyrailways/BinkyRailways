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

class SensorComponent extends EntityComponent {
  final mapi.Sensor model;

  SensorComponent({required this.model}) {
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
    final backgroundPaint = Paint()..color = backgroundColor();
    final borderPaint = Paint()..color = Colors.black;
    borderPaint.style = PaintingStyle.stroke;
    switch (model.shape) {
      case mapi.Shape.CIRCLE:
        canvas.drawCircle(
            Offset(size.x / 2, size.y / 2), minDim / 2, backgroundPaint);
        canvas.drawCircle(
            Offset(size.x / 2, size.y / 2), minDim / 2, borderPaint);
        break;
      case mapi.Shape.DIAMOND:
        var path = Path();
        path.moveTo(size.x / 2, 0); // top-center
        path.lineTo(size.x, size.y / 2); // middle-right
        path.lineTo(size.x / 2, size.y); // bottom-center
        path.lineTo(0, size.y / 2); // middle-left
        path.close();
        canvas.drawPath(path, backgroundPaint);
        canvas.drawPath(path, borderPaint);
        break;
      case mapi.Shape.SQUARE:
        canvas.drawRect(size.toRect(), backgroundPaint);
        canvas.drawRect(size.toRect(), borderPaint);
        break;
      case mapi.Shape.TRIANGLE:
        var path = Path();
        path.moveTo(size.x / 2, 0); // top-center
        path.lineTo(size.x, size.y); // bottom-right
        path.lineTo(0, size.y); // bottom-left
        path.close();
        canvas.drawPath(path, backgroundPaint);
        canvas.drawPath(path, borderPaint);
        break;
      default:
        canvas.clipRRect(RRect.fromRectAndRadius(
            size.toRect(), Radius.circular(minDim / 3)));
        // Draw background
        canvas.drawPaint(backgroundPaint);
        canvas.drawPaint(borderPaint);
    }
    canvas.restore();

    // Show description (if hovered)
    if (showDescription()) {
      canvas.renderRotated(
          getTextRotation(model.position), Vector2(size.x / 2, size.y / 2),
          (canvas) {
        textPaint.render(
            canvas, model.description, Vector2(size.x / 2, size.y / 2 + height),
            anchor: fc.Anchor.center);
      });
    }
  }

  Color backgroundColor() =>
      isActive() ? BinkyColors.activeSensorBg : BinkyColors.inactiveSensorBg;
  Color textColor() => Colors.black;

  bool isActive() => true;
  bool showDescription() => isHovered;
}
