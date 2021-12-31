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
import 'package:flutter/material.dart';
import 'package:flame/extensions.dart';
import 'dart:math';

import 'entity_component.dart';
import '../api.dart' as mapi;

class BlockComponent extends EntityComponent with Tappable {
  final mapi.Block model;

  BlockComponent({required this.model}) {
    loadPosition(model.position);
  }

  @override
  void render(Canvas canvas) {
    final minDim = min(size.x, size.y);
    final TextPaint textPaint = TextPaint(
      style: TextStyle(
        fontSize: minDim * 0.8,
        color: textColor(),
        fontWeight: isHovered ? FontWeight.bold : FontWeight.normal,
      ),
    );

    canvas.save();

    // Draw background
    canvas.clipRRect(
        RRect.fromRectAndRadius(size.toRect(), Radius.circular(minDim / 3)));
    canvas.drawPaint(Paint()..color = backgroundColor());
    // Draw front
    final frontRect = model.reverseSides
        ? Rect.fromLTRB(0, 0, minDim, minDim)
        : Rect.fromLTRB(size.x - minDim, 0, size.x, minDim);
    canvas.drawRect(frontRect, Paint()..color = frontColor());
    // Draw description
    textPaint.render(canvas, model.description, Vector2(size.x / 2, size.y / 2),
        anchor: Anchor.center);

    canvas.restore();
  }

  Color backgroundColor() => Colors.grey;
  Color frontColor() => Colors.green;
  Color textColor() => Colors.black;
}
