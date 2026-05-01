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
import 'package:flame/input.dart';
import 'package:flutter/material.dart' hide Draggable;
import 'package:protobuf/protobuf.dart';

import '../rail_point_component.dart' as common;
import '../view_settings.dart';
import '../../api.dart' as mapi;
import '../../models.dart';
import '../../editor/editor_context.dart';
import './position_draggable.dart';

class RailPointComponent extends common.RailPointComponent
    with Tappable, Draggable, PositionDraggable<mapi.RailPoint> {
  final EditorContext editorCtx;
  final ModelModel modelModel;

  RailPointComponent(ViewSettings viewSettings,
      {required this.editorCtx,
      required mapi.RailPoint model,
      required this.modelModel})
      : super(viewSettings, model: model) {}

  @override
  Future<void> savePosition(void Function(mapi.Position) editor) async {
    final current = await modelModel.getRailPoint(model.id);
    var update = current.deepCopy();
    editor(update.position);
    await modelModel.updateRailPoint(update);
    editorCtx.select(EntitySelector.railPoint(model));
  }

  @override
  bool onTapUp(TapUpInfo event) {
    if (onVisibleLayer()) {
      editorCtx.select(EntitySelector.railPoint(model));
      return false;
    }
    return true;
  }

  @override
  Color backgroundColor() {
    return _isSelected() ? Colors.orange : super.backgroundColor();
  }

  _isSelected() => editorCtx.selector.idOf(EntityType.railpoint) == model.id;
}
