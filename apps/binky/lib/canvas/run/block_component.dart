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

import 'package:binky/api/generated/br_model_types.pb.dart';
import 'package:binky/api/generated/br_state_types.pb.dart';
import 'package:flame/input.dart';
import 'package:flutter/material.dart';

import '../block_component.dart' as common;
import '../view_settings.dart';
import '../../api.dart' as sapi;
import '../../models.dart';
import './railway_game.dart';

class BlockComponent extends common.BlockComponent {
  final Holder<sapi.BlockState> state;
  final RailwayGame game;

  BlockComponent(ViewSettings viewSettings,
      {required this.state, required this.game})
      : super(viewSettings, model: state.last.model);

  @override
  bool onTapUp(TapUpInfo event) {
    if (onVisibleLayer()) {
      game.showBlock(event.eventPosition.widget, state.last);
      return false;
    }
    return true;
  }

  @override
  String description() {
    final bs = state.last;
    final locId = bs.hasLockedBy() ? bs.lockedBy.id : "";
    final loc =
        (locId.isNotEmpty) ? game.stateModel.getCachedLocState(locId) : null;
    final suffix = (loc != null) ? " [${loc.last.model.description}]" : "";
    if (bs.closedActual) {
      return "${bs.model.description}$suffix: Closed";
    }
    if (bs.closedRequested) {
      return "${bs.model.description}$suffix: Closing";
    }
    return "${bs.model.description}$suffix";
  }

  @override
  common.BlockColors backgroundColors() {
    final bs = state.last;
    final blockState = bs.state;
    final locId = bs.hasLockedBy() ? bs.lockedBy.id : "";
    final loc = (locId.isNotEmpty)
        ? game.stateModel.getCachedLocState(locId)?.last
        : null;
    final currentBlockId = (loc != null) ? loc.currentBlock.id : "";
    final currentRouteId = (loc != null) ? loc.currentRoute.id : "";
    final currentRoute = (currentRouteId.isNotEmpty)
        ? game.modelModel.getCachedRoute(currentRouteId, load: true)
        : null;

    switch (blockState) {
      case BlockStateState.FREE:
        return common.BlockColors.single(Colors.white);
      case BlockStateState.OCCUPIEDUNEXPECTED:
        return common.BlockColors.single(Colors.orange);
      case BlockStateState.OCCUPIED:
        if (loc == null) {
          return common.BlockColors.single(Colors.red);
        }
        if ((currentRoute == null) ||
            ((currentBlockId == bs.model.id) &&
                (currentRoute.to.block.id == bs.model.id))) {
          if (loc.currentBlockEnterSide == BlockSide.BACK) {
            return common.BlockColors(Colors.red, Colors.grey);
          } else {
            return common.BlockColors(Colors.grey, Colors.red);
          }
        }
        if (currentRoute.from.blockSide == BlockSide.FRONT) {
          return common.BlockColors(Colors.red, Colors.grey);
        }
        return common.BlockColors(Colors.grey, Colors.red);
      case BlockStateState.DESTINATION:
        if (loc == null) {
          return common.BlockColors.single(Colors.brown);
        }
        if (currentRoute == null) {
          return common.BlockColors.single(Colors.yellow);
        }
        if (currentRoute.to.blockSide == BlockSide.BACK) {
          return common.BlockColors(Colors.yellow, Colors.grey);
        }
        return common.BlockColors(Colors.grey, Colors.yellow);
      case BlockStateState.ENTERING:
        if ((loc == null) || (currentRoute == null)) {
          return common.BlockColors.single(_greenYellow);
        }
        if (currentRoute.to.blockSide == BlockSide.BACK) {
          return common.BlockColors(_greenYellow, Colors.grey);
        }
        return common.BlockColors(Colors.grey, _greenYellow);
      case BlockStateState.LOCKED:
        return common.BlockColors.single(Colors.cyan);
      case BlockStateState.CLOSED:
        return common.BlockColors.single(Colors.grey);
    }
    return super.backgroundColors();
  }

  final Color _greenYellow = const Color.fromARGB(255, 173, 255, 47);
}
