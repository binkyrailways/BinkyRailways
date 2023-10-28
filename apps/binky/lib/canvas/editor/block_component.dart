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

import 'package:binky/api/generated/br_model_types.pbenum.dart';
import 'package:binky/canvas/block_component.dart';
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
  RouteInfo _isSourceOfSelectedRoute() {
    final routeId = editorCtx.selector.idOf(EntityType.route);
    if (routeId == null) {
      return RouteInfo(false, BlockSide.FRONT);
    }
    final route = modelModel.getCachedRoute(routeId);
    if (route == null) {
      return RouteInfo(false, BlockSide.FRONT);
    }
    return RouteInfo(route.from.block.id == model.id, route.from.blockSide);
  }

  RouteInfo _isDestinationOfSelectedRoute() {
    final routeId = editorCtx.selector.idOf(EntityType.route);
    if (routeId == null) {
      return RouteInfo(false, BlockSide.FRONT);
    }
    final route = modelModel.getCachedRoute(routeId);
    if (route == null) {
      return RouteInfo(false, BlockSide.FRONT);
    }
    return RouteInfo(route.to.block.id == model.id, route.to.blockSide);
  }

  @override
  backgroundColors() {
    if (_isSelected()) {
      return BlockColors(BinkyColors.selectedBg, Colors.grey);
    }
    final srcInfo = _isSourceOfSelectedRoute();
    if (srcInfo.blockOfRoute) {
      if (srcInfo.blockSide == BlockSide.FRONT) {
        return BlockColors(BinkyColors.partOfSelectedRouteBg, Colors.grey);
      } else {
        return BlockColors(Colors.grey, BinkyColors.partOfSelectedRouteBg);
      }
    }
    final dstInfo = _isDestinationOfSelectedRoute();
    if (dstInfo.blockOfRoute) {
      if (dstInfo.blockSide == BlockSide.FRONT) {
        return BlockColors(Colors.grey, BinkyColors.partOfSelectedRouteBg);
      } else {
        return BlockColors(BinkyColors.partOfSelectedRouteBg, Colors.grey);
      }
    }
    return super.backgroundColors();
  }

  @override
  String description() {
    final prefix = _isSourceOfSelectedRoute().blockOfRoute
        ? "From "
        : _isDestinationOfSelectedRoute().blockOfRoute
            ? "To "
            : "";
    return prefix + super.description();
  }
}

class RouteInfo {
  final bool blockOfRoute;
  final BlockSide blockSide;

  RouteInfo(this.blockOfRoute, this.blockSide);
}
