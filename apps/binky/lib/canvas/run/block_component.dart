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

import 'package:flame/input.dart';

import '../block_component.dart' as common;
import '../../api.dart' as sapi;
import '../../models.dart';
import './railway_game.dart';

class BlockComponent extends common.BlockComponent {
  final Holder<sapi.BlockState> state;
  final RailwayGame game;

  BlockComponent({required this.state, required this.game})
      : super(model: state.last.model);

  @override
  bool onTapUp(TapUpInfo event) {
    game.showBlock(event.eventPosition.widget, state.last);
    return true;
  }

  @override
  String description() {
    final bs = state.last;
    if (bs.closedActual) {
      return "${bs.model.description}: Closed";
    }
    if (bs.closedRequested) {
      return "${bs.model.description}: Closing";
    }
    return bs.model.description;
  }
}
