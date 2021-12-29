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

import 'dart:ui';

import 'package:flame/components.dart' as fc;
import 'package:flutter/material.dart';
import 'package:flame/extensions.dart';
import 'dart:math';
import 'package:flutter/painting.dart';

import 'entity_component.dart';
import '../api/generated/br_model_types.pb.dart' as mapi;

class BlockComponent extends EntityComponent {
  final mapi.Block model;

  BlockComponent({required this.model}) {
    loadPosition(model.position);
  }

  @override
  void render(Canvas canvas) {
    final minDim = min(size.x, size.y);
    final fc.TextPaint textPaint = fc.TextPaint(
      style: TextStyle(
        fontSize: minDim * 0.8,
        color: textColor(),
      ),
    );
    canvas.drawRRect(
        RRect.fromRectAndRadius(size.toRect(), Radius.circular(minDim / 3)),
        Paint()..color = backgroundColor());
    textPaint.render(canvas, model.description, Vector2(size.x / 2, size.y / 2),
        anchor: fc.Anchor.center);
//    canvas.drawParagraph(p, Offset(size.x / 2, size.y / 2));
  }

  Color backgroundColor() => Colors.grey;
  Color textColor() => Colors.black;
}
