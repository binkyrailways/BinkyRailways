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
import 'package:flutter/material.dart' hide Draggable;
import 'package:flame/input.dart';
import 'package:protobuf/protobuf.dart';

import '../junction_component.dart' as common;
import '../../api.dart' as mapi;
import '../../models.dart';
import '../../editor/editor_context.dart';
import './position_draggable.dart';

class JunctionComponent extends common.JunctionComponent
    with Tappable, Draggable, PositionDraggable<mapi.Junction> {
  final EditorContext editorCtx;
  final ModelModel modelModel;

  JunctionComponent(
      {required this.editorCtx,
      required mapi.Junction model,
      required this.modelModel})
      : super(model: model);

  @override
  Future<void> savePosition(void Function(mapi.Position) editor) async {
    final current = await modelModel.getJunction(model.id);
    var update = current.deepCopy();
    editor(update.position);
    await modelModel.updateJunction(update);
    editorCtx.select(EntitySelector.junction(model));
  }

  @override
  bool onTapUp(TapUpInfo event) {
    editorCtx.select(EntitySelector.junction(model));
    return false;
  }

  _isSelected() => editorCtx.selector.idOf(EntityType.junction) == model.id;

  _RouteInfo _getSelectedRouteInfo() {
    final routeId = editorCtx.selector.idOf(EntityType.route);
    if (routeId == null) {
      return _RouteInfo(null, null);
    }
    final route = modelModel.getCachedRoute(routeId);
    if (route == null) {
      return _RouteInfo(null, null);
    }
    final junctions = (route.crossingJunctions
        .where((x) => x.junction.id == model.id)).toList();
    if (junctions.isEmpty) {
      return _RouteInfo(null, null);
    }
    final dir = junctions.first.switchState.direction;
    return _RouteInfo(route, dir);
  }

  @override
  backgroundColor() {
    if (_isSelected()) {
      return Colors.orange;
    }
    final info = _getSelectedRouteInfo();
    if (info.route == null) {
      return super.backgroundColor();
    }
    return Colors.cyan;
  }

  @override
  mapi.SwitchDirection switchDirection() {
    final info = _getSelectedRouteInfo();
    if (info.direction == null) {
      return mapi.SwitchDirection.STRAIGHT;
    }
    return info.direction!;
  }
}

class _RouteInfo {
  final mapi.Route? route;
  final mapi.SwitchDirection? direction;

  _RouteInfo(this.route, this.direction);
}
