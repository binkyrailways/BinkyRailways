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

import 'dart:io';

import '../api.dart';
import 'package:flutter/material.dart';
import 'package:retry/retry.dart';

class StateModel extends ChangeNotifier {
  RailwayState? _railwayState;
  bool _fetchingChanges = false;
  final HolderMap<CommandStationState> _commandStations = HolderMap();
  final HolderMap<LocState> _locs = HolderMap();
  final HolderMap<BlockState> _blocks = HolderMap();
  final HolderMap<BlockGroupState> _blockGroups = HolderMap();
  final HolderMap<JunctionState> _junctions = HolderMap();
  final HolderMap<OutputState> _outputs = HolderMap();
  final HolderMap<RouteState> _routes = HolderMap();
  final HolderMap<SensorState> _sensors = HolderMap();
  final HolderMap<SignalState> _signals = HolderMap();

  StateModel();

  void reset() {
    _railwayState = null;
    _commandStations.clear();
    _locs.clear();
    _blocks.clear();
    _blockGroups.clear();
    _junctions.clear();
    _outputs.clear();
    _routes.clear();
    _sensors.clear();
    _signals.clear();
  }

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
      final rwState = _railwayState;
      if (rwState != null) {
        if (rwState.isRunModeEnabled) {
          await _ensureFetchingChanges();
        } else {
          _fetchingChanges = false;
        }
      }
      notifyListeners();
    }
    return _railwayState!;
  }

  Future<DiscoverHardwareResponse> discoverHardware(
      String hardwareModuleID) async {
    var stateClient = APIClient().stateClient();
    return await stateClient.discoverHardware(DiscoverHardwareRequest(
      hardwareModuleId: hardwareModuleID,
    ));
  }

  Future<Empty> resetHardwareModule(String hardwareModuleID) async {
    var stateClient = APIClient().stateClient();
    return await stateClient.resetHardwareModule(IDRequest(
      id: hardwareModuleID,
    ));
  }

  // Enable run mode
  Future<RailwayState> enableRunMode(
      {bool virtual = false, bool autoRun = false}) async {
    final current = await getRailwayState();
    if (current.isRunModeEnabled &&
        (current.isVirtualAutorunEnabled == autoRun)) {
      // We're done
      return current;
    }
    // Enable run mode
    var stateClient = APIClient().stateClient();
    final result = await stateClient.enableRunMode(
        EnableRunModeRequest(virtual: virtual, autoRun: autoRun));
    _railwayState = result;
    await _ensureFetchingChanges();
    // Notify listeners
    notifyListeners();
    return result;
  }

  // Enable entity tester
  Future<RailwayState> enableEntityTester() async {
    var stateClient = APIClient().stateClient();
    final result = await stateClient.enableEntityTester(Empty());
    _railwayState = result;
    // Notify listeners
    notifyListeners();
    return result;
  }

  // Disable entity tester
  Future<RailwayState> disableEntityTester() async {
    var stateClient = APIClient().stateClient();
    final result = await stateClient.disableEntityTester(Empty());
    _railwayState = result;
    // Notify listeners
    notifyListeners();
    return result;
  }

  Future<void> _ensureFetchingChanges() async {
    if (!_fetchingChanges) {
      _fetchingChanges = true;
      // Now wait for all changes
      _getStateChanges();
    }
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
    _fetchingChanges = false;
    notifyListeners();
    return result;
  }

  // Set power on/off
  Future<RailwayState> setPower(bool enabled) async {
    var stateClient = APIClient().stateClient();
    final result =
        await stateClient.setPower(SetPowerRequest(enabled: enabled));
    _railwayState = result;
    notifyListeners();
    return result;
  }

  // Set automatic control on/off
  Future<RailwayState> setAutomaticControl(bool enabled) async {
    var stateClient = APIClient().stateClient();
    final result = await stateClient
        .setAutomaticControl(SetAutomaticControlRequest(enabled: enabled));
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
    if (_locs.set(id, "", "", result)) {
      notifyListeners();
    }
    return result;
  }

  Future<LocState> setLocFunctions(
      String id, List<LocFunction> functions) async {
    var stateClient = APIClient().stateClient();
    final result = await stateClient.setLocFunctions(SetLocFunctionsRequest(
      id: id,
      functions: functions,
    ));
    if (_locs.set(id, "", "", result)) {
      notifyListeners();
    }
    return result;
  }

  Future<LocState> setLocControlledAutomatically(
      String id, bool enabled) async {
    var stateClient = APIClient().stateClient();
    final result = await stateClient
        .setLocControlledAutomatically(SetLocControlledAutomaticallyRequest(
      id: id,
      enabled: enabled,
    ));
    if (_locs.set(id, "", "", result)) {
      notifyListeners();
    }
    return result;
  }

  Future<JunctionState> setSwitchDirection(
      String id, SwitchDirection direction) async {
    var stateClient = APIClient().stateClient();
    final result =
        await stateClient.setSwitchDirection(SetSwitchDirectionRequest(
      id: id,
      direction: direction,
    ));
    if (_junctions.set(id, "", "", result)) {
      notifyListeners();
    }
    return result;
  }

  Future<OutputState> setBinaryOutputActive(String id, bool active) async {
    var stateClient = APIClient().stateClient();
    final result =
        await stateClient.setBinaryOutputActive(SetBinaryOutputActiveRequest(
      id: id,
      active: active,
    ));
    if (_outputs.set(id, "", "", result)) {
      notifyListeners();
    }
    return result;
  }

  Future<RailwayState> clickVirtualSensor(String id) async {
    var stateClient = APIClient().stateClient();
    final result =
        await stateClient.clickVirtualSensor(ClickVirtualSensorRequest(
      id: id,
    ));
    _railwayState = result;
    notifyListeners();
    return result;
  }

  Future<RailwayState> assignLocToBlock(
      String locId, String blockId, BlockSide blockSide) async {
    var stateClient = APIClient().stateClient();
    final result = await stateClient.assignLocToBlock(AssignLocToBlockRequest(
      locId: locId,
      blockId: blockId,
      blockSide: blockSide,
    ));
    _railwayState = result;
    notifyListeners();
    return result;
  }

  Future<RailwayState> putLocOnTrack(String locId) async {
    var stateClient = APIClient().stateClient();
    final result = await stateClient.putLocOnTrack(PutLocOnTrackRequest(
      locId: locId,
    ));
    _railwayState = result;
    notifyListeners();
    return result;
  }

  Future<RailwayState> takeLocOfTrack(String locId) async {
    var stateClient = APIClient().stateClient();
    final result = await stateClient.takeLocOfTrack(TakeLocOfTrackRequest(
      locId: locId,
    ));
    _railwayState = result;
    notifyListeners();
    return result;
  }

  Future<BlockState> setBlockClosed(String id, bool closed) async {
    var stateClient = APIClient().stateClient();
    final result = await stateClient.setBlockClosed(SetBlockClosedRequest(
      id: id,
      closed: closed,
    ));
    if (_blocks.set(id, "", "", result)) {
      notifyListeners();
    }
    return result;
  }

  Future<Holder<T>> _getState<T>(String id, HolderMap<T> state) async {
    return retry(() {
      final result = state.get(id);
      if (result == null) {
        throw Exception("${T.toString()} not found");
      }
      return result;
    });
  }

  Holder<T>? _getCachedState<T>(String id, HolderMap<T> state) {
    return state.get(id);
  }

  // Get all known command stations
  Iterable<Holder<CommandStationState>> commandStations() =>
      _commandStations.values;
  Future<Holder<CommandStationState>> getCommandStationState(String id) async =>
      _getState(id, _commandStations);

  // Get all known locs
  Iterable<Holder<LocState>> locs() => _locs.values;
  Holder<LocState>? getCachedLocState(String id) => _getCachedState(id, _locs);
  Future<Holder<LocState>> getLocState(String id) async => _getState(id, _locs);

  // Get all known blocks
  Iterable<Holder<BlockState>> blocks() => _blocks.values;
  Future<Holder<BlockState>> getBlockState(String id) async =>
      _getState(id, _blocks);

  // Get all known blocks groups
  Iterable<Holder<BlockGroupState>> blockGroups() => _blockGroups.values;
  Future<Holder<BlockGroupState>> getBlockGroupState(String id) async =>
      _getState(id, _blockGroups);

  // Get all known junctions
  Iterable<Holder<JunctionState>> junctions() => _junctions.values;
  Future<Holder<JunctionState>> getJunctionState(String id) async =>
      _getState(id, _junctions);

  // Get all known outputs
  Iterable<Holder<OutputState>> outputs() => _outputs.values;
  Future<Holder<OutputState>> getOutputState(String id) async =>
      _getState(id, _outputs);

  // Get all known routes
  Iterable<Holder<RouteState>> routes() => _routes.values;
  Future<Holder<RouteState>> getRouteState(String id) async =>
      _getState(id, _routes);

  // Get all known sensors
  Iterable<Holder<SensorState>> sensors() => _sensors.values;
  Future<Holder<SensorState>> getSensorState(String id) async =>
      _getState(id, _sensors);

  // Get all known signals
  Iterable<Holder<SignalState>> signals() => _signals.values;
  Future<Holder<SignalState>> getSignalState(String id) async =>
      _getState(id, _signals);

  // Collect state changes from the server, until the
  Future<void> _getStateChanges() async {
    // Clear current state
    _commandStations.clear();
    _locs.clear();
    _blocks.clear();
    _blockGroups.clear();
    _junctions.clear();
    _outputs.clear();
    _routes.clear();
    _sensors.clear();
    _signals.clear();
    // Keep fetching changes until run mode is disabled
    final stateClient = APIClient().stateClient();
    var railwayHashKey = "";
    var railwayHashValue = "";
    while (_railwayState?.isRunModeEnabled ?? false) {
      // Request state changes
      try {
        // Collect all known hashes
        final Map<String, String> hashes = {
          railwayHashKey: railwayHashValue,
        };
        _commandStations.copyHashesTo(hashes);
        _locs.copyHashesTo(hashes);
        _blocks.copyHashesTo(hashes);
        _blockGroups.copyHashesTo(hashes);
        _junctions.copyHashesTo(hashes);
        _outputs.copyHashesTo(hashes);
        _routes.copyHashesTo(hashes);
        _sensors.copyHashesTo(hashes);
        _signals.copyHashesTo(hashes);

        // Make request
        final req = GetStateChangesRequest(hashes: hashes);
        await for (var change in stateClient.getStateChanges(req)) {
          var changed = false;
          if (change.hasRailway()) {
            _railwayState = change.railway;
            railwayHashKey = change.id;
            railwayHashValue = change.hash;
            changed = true;
          }
          if (change.hasCommandStation()) {
            changed |= _commandStations.set(change.commandStation.model.id,
                change.id, change.hash, change.commandStation);
          }
          if (change.hasLoc()) {
            changed |= _locs.set(
                change.loc.model.id, change.id, change.hash, change.loc);
          }
          if (change.hasBlock()) {
            changed |= _blocks.set(
                change.block.model.id, change.id, change.hash, change.block);
          }
          if (change.hasBlockGroup()) {
            changed |= _blockGroups.set(change.blockGroup.model.id, change.id,
                change.hash, change.blockGroup);
          }
          if (change.hasJunction()) {
            changed |= _junctions.set(change.junction.model.id, change.id,
                change.hash, change.junction);
          }
          if (change.hasOutput()) {
            changed |= _outputs.set(
                change.output.model.id, change.id, change.hash, change.output);
          }
          if (change.hasRoute()) {
            changed |= _routes.set(
                change.route.model.id, change.id, change.hash, change.route);
          }
          if (change.hasSensor()) {
            changed |= _sensors.set(
                change.sensor.model.id, change.id, change.hash, change.sensor);
          }
          if (change.hasSignal()) {
            changed |= _signals.set(
                change.signal.model.id, change.id, change.hash, change.signal);
          }
          if (changed) {
            notifyListeners();
          }
        }
      } catch (err) {
        print(err);
      }
      //sleep(const Duration(milliseconds: 10));
    }
  }
}

// Holder allows for changing content in a map.
class Holder<T> {
  T last;

  Holder(this.last);

  // Update the last value.
  // Returns true if changes, false otherwise.
  bool update(T value) {
    if (last.hashCode == value.hashCode) {
      return false;
    }
    last = value;
    return true;
  }
}

class HolderMap<T> {
  final Map<String, Holder<T>> _map = {};
  final Map<String, String> _hashes = {};

  Iterable<Holder<T>> get values => _map.values;

  Holder<T>? get(String id) {
    return _map[id];
  }

  bool set(String id, String hashKey, String hashValue, T value) {
    final holder = _map[id];
    if (hashKey.isNotEmpty) {
      _hashes[hashKey] = hashValue;
    } else {
      _hashes.clear();
    }
    if (holder != null) {
      return holder.update(value);
    }
    _map[id] = Holder(value);
    return true;
  }

  void clear() {
    _map.clear();
    _hashes.clear();
  }

  void copyHashesTo(Map<String, String> target) {
    _hashes.forEach((k, v) => target[k] = v);
  }
}
