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

import 'package:binky/colors.dart';
import 'package:flame/components.dart';
import 'package:flutter/material.dart' hide Draggable;
import 'package:flame/input.dart';
import 'package:protobuf/protobuf.dart';

import '../block_component.dart' as common;
import '../view_settings.dart';
import '../../api.dart' as mapi;
import '../../models.dart';
import '../../editor/editor_context.dart';
import './position_draggable.dart';

class BlockComponent extends common.BlockComponent
    with Draggable, PositionDraggable<mapi.Block> {
  final EditorContext editorCtx;
  final ModelModel modelModel;

  BlockComponent(ViewSettings viewSettings,
      {required this.editorCtx,
      required mapi.Block model,
      required this.modelModel})
      : super(viewSettings, model: model);

  @override
  Future<void> savePosition(void Function(mapi.Position) editor) async {
    final current = await modelModel.getBlock(model.id);
    var update = current.deepCopy();
    editor(update.position);
    await modelModel.updateBlock(update);
    editorCtx.select(EntitySelector.block(model));
  }

  @override
  bool onTapUp(TapUpInfo event) {
    if (onVisibleLayer()) {
      editorCtx.select(EntitySelector.block(model));
      return false;
    }
    return true;
  }

  _isSelected() => editorCtx.selector.idOf(EntityType.block) == model.id;
  _isSourceOfSelectedRoute() {
    final routeId = editorCtx.selector.idOf(EntityType.route);
    if (routeId == null) {
      return false;
    }
    final route = modelModel.getCachedRoute(routeId);
    return (route != null) && (route.from.block.id == model.id);
  }

  _isDestinationOfSelectedRoute() {
    final routeId = editorCtx.selector.idOf(EntityType.route);
    if (routeId == null) {
      return false;
    }
    final route = modelModel.getCachedRoute(routeId);
    return (route != null) && (route.to.block.id == model.id);
  }

  @override
  backgroundColor() => _isSelected()
      ? BinkyColors.selectedBg
      : _isSourceOfSelectedRoute() || _isDestinationOfSelectedRoute()
          ? BinkyColors.partOfSelectedRouteBg
          : super.backgroundColor();

  @override
  String description() {
    final prefix = _isSourceOfSelectedRoute()
        ? "From "
        : _isDestinationOfSelectedRoute()
            ? "To "
            : "";
    return prefix + super.description();
  }
}
