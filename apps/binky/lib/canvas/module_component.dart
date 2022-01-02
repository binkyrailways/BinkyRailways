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

import 'dart:ui' as ui;
import 'dart:math';

import 'package:flame/components.dart' as fc;
import 'package:flutter/material.dart';
import 'package:flame/extensions.dart';

import 'entity_component.dart';
import '../models.dart';
import '../api.dart' as mapi;

class ModuleComponent extends EntityComponent {
  final mapi.Module model;
  ui.Image? _bgImage;

  ModuleComponent({required this.model}) {
    size.x = model.hasWidth() ? max(1, model.width.toDouble()) : 1;
    size.y = model.hasHeight() ? max(1, model.height.toDouble()) : 1;
  }

  Future<void> loadBackgroundImage(ModelModel modelModel) async {
    // Load background image (if any)
    if (model.hasBackgroundImage) {
      _bgImage = await modelModel.getModuleBackgroundImage(model.id);
    }
  }

  @override
  void render(Canvas canvas) {
    canvas.drawRect(size.toRect(), Paint()..color = Colors.yellow.shade50);
    if (_bgImage != null) {
      final img = _bgImage!;
      final dst = Rect.fromLTRB(0, 0, width, height);
      final src =
          Rect.fromLTRB(0, 0, img.width.toDouble(), img.height.toDouble());
      canvas.drawImageRect(img, src, dst, Paint());
    }
  }
}
