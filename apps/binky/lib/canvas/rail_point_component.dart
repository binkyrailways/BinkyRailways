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

import 'package:flame/components.dart' as fc;
import 'package:flutter/material.dart';
import 'package:flame/extensions.dart';

import 'entity_component.dart';
import 'view_settings.dart';
import '../api.dart' as mapi;

class RailPointComponent extends EntityComponent {
  final mapi.RailPoint model;

  RailPointComponent(ViewSettings viewSettings, {required this.model})
      : super(viewSettings) {
    loadPosition(model.position);
    // Rail points are tiny, force a small size if not set
    //size.x = 20;
    //size.y = 20;
  }

  @override
  void render(Canvas canvas) {
    if (onVisibleLayer()) {
      final paint = Paint()
        ..color = Colors.black
        ..style = PaintingStyle.stroke
        ..strokeWidth = 4;

      // Draw tiny '=' sign using entire width/height
      // Top line
      canvas.drawLine(
          Offset(0, size.y * 0.3), Offset(size.x, size.y * 0.3), paint);
      // Bottom line
      canvas.drawLine(
          Offset(0, size.y * 0.7), Offset(size.x, size.y * 0.7), paint);
    }
  }
}
