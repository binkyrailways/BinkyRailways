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

class OutputComponent extends EntityComponent {
  final mapi.Output model;

  OutputComponent({required this.model}) {
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
    // Draw description
    textPaint.render(canvas, model.description, Vector2(size.x / 2, size.y / 2),
        anchor: fc.Anchor.center);
    canvas.restore();
  }

  Color backgroundColor() => isActive() ? Colors.yellow : Colors.blueGrey;
  Color textColor() => Colors.black;

  bool isActive() => true;
}
