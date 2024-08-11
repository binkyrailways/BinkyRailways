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

import 'package:binky/colors.dart';
import 'package:flame/components.dart' as fc;
import 'package:flutter/material.dart';
import 'package:flame/extensions.dart';
import 'dart:math';

import 'entity_component.dart';
import 'view_settings.dart';
import '../api.dart' as mapi;

class SignalComponent extends EntityComponent {
  final mapi.Signal model;

  SignalComponent(ViewSettings viewSettings, {required this.model})
      : super(viewSettings) {
    loadPosition(model.position);
  }

  @override
  void render(Canvas canvas) {
    if (onVisibleLayer()) {
      final minDim = min(size.x, size.y);
      final fc.TextPaint textPaint = fc.TextPaint(
        style: TextStyle(
          fontSize: minDim * 0.8,
          color: textColor(),
          fontWeight: isHovered ? FontWeight.bold : FontWeight.normal,
        ),
      );
      canvas.save();

      // Count number of colors
      final colors = <Color>[];
      if (model.hasBlockSignal()) {
        final bs = model.blockSignal;
        if (bs.hasRedPattern()) {
          colors.add(redColor());
        }
        if (bs.hasGreenPattern()) {
          colors.add(greenColor());
        }
        if (bs.hasYellowPattern()) {
          colors.add(yellowColor());
        }
        if (bs.hasWhitePattern()) {
          colors.add(whiteColor());
        }
      }
      if (colors.isEmpty) {
        colors.add(redColor());
        colors.add(greenColor());
      }
      final colorHeight = size.y / (colors.length + 1);
      final colorRadius = (min(colorHeight, (size.x * 0.6)) / 2) * 0.9;

      // Clip rrect
      final backgroundPaint = Paint()..color = backgroundColor();
      final borderPaint = Paint()..color = Colors.black.withAlpha(64);
      borderPaint.style = PaintingStyle.stroke;
      canvas.clipRRect(
          RRect.fromRectAndRadius(size.toRect(), Radius.circular(minDim / 2)));
      // Draw background
      canvas.drawPaint(backgroundPaint);
      // Draw colors
      var y = colorHeight;
      for (var c in colors) {
        final colorPaint = Paint()..color = c;
        canvas.drawCircle(Offset(size.x / 2, y), colorRadius, colorPaint);
        canvas.drawCircle(Offset(size.x / 2, y), colorRadius, borderPaint);
        y += colorHeight;
      }

      // Draw border
      canvas.drawPaint(borderPaint);
      canvas.restore();

      // Show description (if hovered)
      if (showDescription()) {
        canvas.renderRotated(
            getTextRotation(model.position), Vector2(size.x / 2, size.y / 2),
            (canvas) {
          textPaint.render(canvas, model.description,
              Vector2(size.x / 2, size.y / 2 + height),
              anchor: fc.Anchor.center);
        });
      }
    }
  }

  Color backgroundColor() => Colors.grey;
  Color textColor() => Colors.black;
  Color redColor() => Colors.red;
  Color greenColor() => Colors.green;
  Color yellowColor() => Colors.yellow;
  Color whiteColor() => Colors.white;

  bool showDescription() => isHovered;
}
