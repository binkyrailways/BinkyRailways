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

import '../api.dart';
import 'package:flutter/material.dart';
import 'package:retry/retry.dart';

class StateModel extends ChangeNotifier {
  RailwayState? _railwayState;
  final Map<String, CommandStationState> _commandStations = {};
  final Map<String, LocState> _locs = {};
  final Map<String, BlockState> _blocks = {};
  final Map<String, BlockGroupState> _blockGroups = {};
  final Map<String, JunctionState> _junctions = {};
  final Map<String, OutputState> _outputs = {};
  final Map<String, RouteState> _routes = {};
  final Map<String, SensorState> _sensors = {};
  final Map<String, SignalState> _signals = {};

  StateModel();

  // Is a railway already loaded?
  bool isRailwayStateLoaded() => _railwayState != null;

  // Gets the railway state we have in cache.
  RailwayState? getCachedRailwayState() => _railwayState;

  // Get the last known railway state.
  // Fetch if needed.
  Future<RailwayState> getRailwayState() async {
    if (_railwayState == null) {
      var stateClient = APIClient().stateClient();
      _railwayState = await stateClient.getRailwayState(Empty());
      notifyListeners();
    }
    return _railwayState!;
  }

  // Enable run mode
  Future<RailwayState> enableRunMode({bool virtual = false}) async {
    final current = await getRailwayState();
    if (current.isRunModeEnabled) {
      // We're done
      return current;
    }
    // Enable run mode
    var stateClient = APIClient().stateClient();
    final result =
        await stateClient.enableRunMode(EnableRunModeRequest(virtual: virtual));
    _railwayState = result;
    // Start fetching state changes
    _getStateChanges(true);
    // Notify listeners
    notifyListeners();
    return result;
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
    final result = await stateClient.disableRunMode(Empty());
    _railwayState = result;
    notifyListeners();
    return result;
  }

  Future<RailwayState> setPower(bool enabled) async {
    var stateClient = APIClient().stateClient();
    final result =
        await stateClient.setPower(SetPowerRequest(enabled: enabled));
    _railwayState = result;
    notifyListeners();
    return result;
  }

  Future<LocState> setLocSpeedAndDirection(
      String id, int speed, LocDirection direction) async {
    var stateClient = APIClient().stateClient();
    final result = await stateClient
        .setLocSpeedAndDirection(SetLocSpeedAndDirectionRequest(
      id: id,
      speed: speed,
      direction: direction,
    ));
    _locs[id] = result;
    notifyListeners();
    return result;
  }

  Future<T> _getState<T>(String id, Map<String, T> state) async {
    return retry(() {
      final result = state[id];
      if (result == null) {
        throw Exception("${T.toString()} not found");
      }
      return result;
    });
  }

  // Get all known command stations
  Iterable<CommandStationState> commandStations() => _commandStations.values;
  Future<CommandStationState> getCommandStationState(String id) async =>
      _getState(id, _commandStations);

  // Get all known locs
  Iterable<LocState> locs() => _locs.values;
  Future<LocState> getLocState(String id) async => _getState(id, _locs);

  // Get all known blocks
  Iterable<BlockState> blocks() => _blocks.values;
  Future<BlockState> getBlockState(String id) async => _getState(id, _blocks);

  // Get all known blocks groups
  Iterable<BlockGroupState> blockGroups() => _blockGroups.values;
  Future<BlockGroupState> getBlockGroupState(String id) async =>
      _getState(id, _blockGroups);

  // Get all known junctions
  Iterable<JunctionState> junctions() => _junctions.values;
  Future<JunctionState> getJunctionState(String id) async =>
      _getState(id, _junctions);

  // Get all known outputs
  Iterable<OutputState> outputs() => _outputs.values;
  Future<OutputState> getOutputState(String id) async =>
      _getState(id, _outputs);

  // Get all known routes
  Iterable<RouteState> routes() => _routes.values;
  Future<RouteState> getRouteState(String id) async => _getState(id, _routes);

  // Get all known sensors
  Iterable<SensorState> sensors() => _sensors.values;
  Future<SensorState> getSensorState(String id) async =>
      _getState(id, _sensors);

  // Get all known signals
  Iterable<SignalState> signals() => _signals.values;
  Future<SignalState> getSignalState(String id) async =>
      _getState(id, _signals);

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
          if (change.hasCommandStation()) {
            _commandStations[change.commandStation.model.id] =
                change.commandStation;
          }
          if (change.hasLoc()) {
            _locs[change.loc.model.id] = change.loc;
          }
          if (change.hasBlock()) {
            _blocks[change.block.model.id] = change.block;
          }
          if (change.hasBlockGroup()) {
            _blockGroups[change.blockGroup.model.id] = change.blockGroup;
          }
          if (change.hasJunction()) {
            _junctions[change.junction.model.id] = change.junction;
          }
          if (change.hasOutput()) {
            _outputs[change.output.model.id] = change.output;
          }
          if (change.hasRoute()) {
            _routes[change.route.model.id] = change.route;
          }
          if (change.hasSensor()) {
            _sensors[change.sensor.model.id] = change.sensor;
          }
          if (change.hasSignal()) {
            _signals[change.signal.model.id] = change.signal;
          }
          notifyListeners();
        }
      } catch (err) {
        print(err);
      }
    }
  }
}
