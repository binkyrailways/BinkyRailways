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
import 'package:flame/input.dart';
import 'package:flutter/widgets.dart';
import '../../api.dart' as mapi;

mixin PositionDraggable<T> on PositionComponent {
  Vector2? dragDeltaPosition;

  bool onDragStart(int pointerId, DragStartInfo info) {
    dragDeltaPosition = info.eventPosition.game - position;
    return false;
  }

  bool onDragUpdate(int pointerId, DragUpdateInfo info) {
    final dragDeltaPosition = this.dragDeltaPosition;
    if (dragDeltaPosition == null) {
      return false;
    }

    position.setFrom(info.eventPosition.game - dragDeltaPosition);
    return false;
  }

  bool onDragEnd(int pointerId, DragEndInfo info) {
    if (dragDeltaPosition != null) {
      savePosition((update) { 
        update.x = (position.x - width / 2).round();
        update.y =  (position.y - height / 2).round();
      });
    }
    dragDeltaPosition = null;
    return false;
  }

  bool onDragCancel(int pointerId) {
    dragDeltaPosition = null;
    return false;
  }

  @protected
  Future<void> savePosition(void Function(mapi.Position) editor) async {
    // Not implemented
  }
}
