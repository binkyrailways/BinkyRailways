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
//

import 'package:binky/api/api_client.dart';
import 'package:binky/api/generated/br_state_service.pb.dart';
import 'package:flutter/material.dart';

import '../api/generated/br_model_types.pb.dart';
import '../api/generated/br_state_types.pb.dart';

class StateModel extends ChangeNotifier {
  RailwayState? _railwayState;
  final Map<String, BlockState> _blocks = {};
  final Map<String, LocState> _locs = {};

  StateModel();

  // Is a railway already loaded?
  bool isRailwayStateLoaded() => _railwayState != null;

  void requireRailwayLoaded() {
    if (!isRailwayStateLoaded()) {
      throw Exception("RailwayState is not loaded");
    }
  }

  Future<RailwayState> getRailwayState() async {
    if (_railwayState == null) {
      var stateClient = APIClient().stateClient();
      _railwayState = await stateClient.getRailwayState(Empty());
      notifyListeners();
    }
    return _railwayState!;
  }

  Future<RailwayState> enableRunMode({bool virtual = false}) async {
    final current = await getRailwayState();
    if (current.isRunModeEnabled) {
      // We're done
      return current;
    }
    // Enable run mode
    var stateClient = APIClient().stateClient();
    _railwayState =
        await stateClient.enableRunMode(EnableRunModeRequest(virtual: virtual));
    // Start fetching state changes
    _getStateChanges(true);
    // Notify listeners
    notifyListeners();
    return _railwayState!;
  }

  // Disable run mode
  Future<RailwayState> disableRunMode() async {
    final current = await getRailwayState();
    if (!current.isRunModeEnabled) {
      // We're done
      return current;
    }
    // Disable run mode
    var stateClient = APIClient().stateClient();
    _railwayState = await stateClient.disableRunMode(Empty());
    notifyListeners();
    return _railwayState!;
  }

  // Collect state changes from the server, until the
  Future<void> _getStateChanges(bool bootstrap) async {
    // Clear current state
    _blocks.clear();
    // Keep fetching changes until run mode is disabled
    final stateClient = APIClient().stateClient();
    while (_railwayState?.isRunModeEnabled ?? false) {
      // Request state changes
      final req = GetStateChangesRequest(bootstrap: bootstrap);
      try {
        await for (var change in stateClient.getStateChanges(req)) {
          if (change.hasRailway()) {
            _railwayState = change.railway;
          }
          if (change.hasBlock()) {
            _blocks[change.block.model.id] = change.block;
          }
          if (change.hasLoc()) {
            _locs[change.loc.model.id] = change.loc;
          }
          notifyListeners();
        }
      } catch (err) {
        print(err);
      }
    }
  }
}
