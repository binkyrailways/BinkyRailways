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

import 'package:flame/game.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../models/model_model.dart';
import '../../models/state_model.dart';

import 'railway_game.dart';

class RailwayCanvas extends StatelessWidget {
  const RailwayCanvas({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<ModelModel>(builder: (context, model, child) {
      return Consumer<StateModel>(builder: (context, state, child) {
        return GameWidget(
            game: RailwayGame(modelModel: model, stateModel: state));
      });
    });
  }
}
