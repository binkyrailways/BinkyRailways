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

import 'package:flame/components.dart';
import 'package:flame/game.dart';
import 'package:flame/input.dart';

class BaseGame extends FlameGame
    with
        HasDraggables,
        HasHoverables,
        HasTappables,
        MultiTouchDragDetector,
        ScrollDetector {
  void clampZoom() {
    camera.zoom = camera.zoom.clamp(1, 3.0);
  }

  static const zoomPerScrollUnit = 0.02;

  @override
  void onScroll(PointerScrollInfo info) {
    camera.zoom += -info.scrollDelta.global.y.sign * zoomPerScrollUnit;
    camera.position.x += 5;
    clampZoom();
  }

  late bool _draggingCamera = false;

  @override
  // ignore: must_call_super
  void onDragStart(int pointerId, DragStartInfo info) {
    final shouldContinue = propagateToChildren<Draggable>(
        (c) => c.handleDragStart(pointerId, info));
    _draggingCamera = shouldContinue;
  }

  @override
  // ignore: must_call_super
  void onDragUpdate(int pointerId, DragUpdateInfo info) {
    if (_draggingCamera) {
      camera.translateBy(-info.delta.viewport);
      camera.snap();
    } else {
      propagateToChildren<Draggable>(
        (c) => c.handleDragUpdated(pointerId, info),
      );
    }
  }

  @override
  // ignore: must_call_super
  void onDragEnd(int pointerId, DragEndInfo info) {
    if (!_draggingCamera) {
      propagateToChildren<Draggable>(
        (c) => c.handleDragEnded(pointerId, info),
      );
    }
  }

  @override
  // ignore: must_call_super
  void onDragCancel(int pointerId) {
    if (!_draggingCamera) {
      propagateToChildren<Draggable>((c) => c.handleDragCanceled(pointerId));
    }
    _draggingCamera = false;
  }

  void resetCamera() {
    camera.zoom = 1;
    camera.snapTo(Vector2.all(0));
  }
}
