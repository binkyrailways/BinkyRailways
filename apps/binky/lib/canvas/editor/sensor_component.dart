// Copyright 2022 Ewout Prangsma
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

import 'package:flutter/services.dart';
import 'package:binky/colors.dart';
import 'package:flame/components.dart';
import 'package:flutter/material.dart' hide Draggable;
import 'package:flame/input.dart';
import 'package:protobuf/protobuf.dart';

import '../sensor_component.dart' as common;
import '../../api.dart' as mapi;
import '../../models.dart';
import '../../editor/editor_context.dart';
import './position_draggable.dart';
import './module_game.dart';

class SensorComponent extends common.SensorComponent
    with Tappable, Draggable, PositionDraggable<mapi.Sensor> {
  final EditorContext editorCtx;
  final ModelModel modelModel;
  final ModuleGame game;

  SensorComponent(
      {required this.editorCtx,
      required mapi.Sensor model,
      required this.modelModel,
      required this.game})
      : super(model: model);

  @override
  Future<void> savePosition(void Function(mapi.Position) editor) async {
    final current = await modelModel.getSensor(model.id);
    var update = current.deepCopy();
    editor(update.position);
    await modelModel.updateSensor(update);
    editorCtx.select(EntitySelector.sensor(model));
  }

  @override
  bool onTapUp(TapUpInfo event) {
    if (game.shiftPressed()) {
      final info = _getSelectedRouteInfo();
      final route = info.route;
      if (route == null) {
        return true;
      }
      if (info.event != null) {
        // Already an event
        return true;
      }
      // Add sensor to given route
      modelModel.addRouteEvent(route.id, model.id);
      return false;
    } else {
      editorCtx.select(EntitySelector.sensor(model));
    }
    return false;
  }

  _isSelected() => editorCtx.selector.idOf(EntityType.sensor) == model.id;

  _RouteInfo _getSelectedRouteInfo() {
    final routeId = editorCtx.selector.idOf(EntityType.route);
    if (routeId == null) {
      return _RouteInfo(null, null);
    }
    final route = modelModel.getCachedRoute(routeId);
    if (route == null) {
      return _RouteInfo(null, null);
    }
    final events = route.events.where((x) => x.sensor.id == model.id).toList();
    if (events.isEmpty) {
      return _RouteInfo(route, null);
    }
    return _RouteInfo(route, events.first);
  }

  @override
  backgroundColor() {
    if (_isSelected()) {
      return BinkyColors.selectedBg;
    }
    final info = _getSelectedRouteInfo();
    if (info.route == null) {
      // There is no selected route
      return super.backgroundColor();
    }
    final event = info.event;
    if (event == null) {
      // This sensor is not yet part of the selected route
      if (isHovered && game.shiftPressed()) {
        return Colors.purple;
      }
      return super.backgroundColor();
    }
    if (event.behaviors
        .any((x) => x.stateBehavior == mapi.RouteStateBehavior.RSB_REACHED)) {
      return BinkyColors.reachedSensorOfSelectedRouteBg;
    }
    if (event.behaviors
        .any((x) => x.stateBehavior == mapi.RouteStateBehavior.RSB_ENTER)) {
      return BinkyColors.enterSensorOfSelectedRouteBg;
    }
    return BinkyColors.partOfSelectedRouteBg;
  }
}

class _RouteInfo {
  final mapi.Route? route;
  final mapi.RouteEvent? event;

  _RouteInfo(this.route, this.event);
}
