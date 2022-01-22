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
import 'dart:math';

import '../api.dart' as mapi;

class EntityComponent extends PositionComponent with Hoverable {
  // Load the given position into this component
  void loadPosition(mapi.Position position) {
    anchor = Anchor.center;
    width = max(1, position.width.toDouble());
    height = max(1, position.height.toDouble());
    x = (position.hasX() ? position.x.toDouble() : 0) + width / 2;
    y = (position.hasY() ? position.y.toDouble() : 0) + height / 2;
    angle = radians(position.rotation.toDouble());
  }

  // Gets the angle (in radians) to draw the description of this element.
  // Ensures that text is always with the correct side up.
  double getTextRotation(mapi.Position position) {
    final modelRotation = position.rotation % 360;
    final rotation = (modelRotation > 90 && modelRotation < 270) ? 180 : 0;
    return radians(rotation.toDouble());
  }
}
