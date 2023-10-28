// Copyright 2023 Ewout Prangsma
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

import 'package:flame/extensions.dart';
import 'dart:math';

import '../api.dart' as mapi;

class Geometry {
  static Vector2 getCenter(mapi.Position position) {
    final width = max(1, position.width.toDouble());
    final height = max(1, position.height.toDouble());
    final x = (position.hasX() ? position.x.toDouble() : 0) + width / 2;
    final y = (position.hasY() ? position.y.toDouble() : 0) + height / 2;
    return Vector2(x, y);
  }

  // Gets the rectangle that contains the given position.
  static Rect getBoundingBox(mapi.Position position) {
    final width = max(1, position.width.toDouble());
    final height = max(1, position.height.toDouble());
    final x = (position.hasX() ? position.x.toDouble() : 0.0);
    final y = (position.hasY() ? position.y.toDouble() : 0.0);
    final angle = radians(position.rotation.toDouble());

    final unrotatedRect = Rect.fromLTRB(x, y, x + width, y + height);
    final center = unrotatedRect.center;
    final topLeft = _rotate(center, unrotatedRect.topLeft, angle);
    final topRight = _rotate(center, unrotatedRect.topRight, angle);
    final bottomLeft = _rotate(center, unrotatedRect.bottomLeft, angle);
    final bottomRight = _rotate(center, unrotatedRect.bottomRight, angle);

    final left =
        min(min(topLeft.x, topRight.x), min(bottomLeft.x, bottomRight.x));
    final top =
        min(min(topLeft.y, topRight.y), min(bottomLeft.y, bottomRight.y));
    final right =
        max(max(topLeft.x, topRight.x), max(bottomLeft.x, bottomRight.x));
    final bottom =
        max(max(topLeft.y, topRight.y), max(bottomLeft.y, bottomRight.y));

    return Rect.fromLTRB(left, top, right, bottom);
  }

  static Vector2 _rotate(Offset center, Offset pt, double angle) {
    final relPt = pt - center;
    final distance = relPt.distance;
    final offset = Offset.fromDirection(relPt.direction + angle, distance);
    return (offset + center).toVector2();
  }
}
