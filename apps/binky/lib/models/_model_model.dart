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

import 'dart:convert';
import 'dart:ui' as ui;

import 'package:binky/api/generated/br_model_service.pb.dart';
import 'package:binky/api/generated/br_storage_service.pbgrpc.dart';
import 'package:binky/api/generated/br_storage_types.pb.dart';

import '../api.dart' as mapi;
import 'package:flutter/material.dart';

class ModelModel extends ChangeNotifier {
  mapi.RailwayEntry? _railwayEntry;
  mapi.Railway? _railway;
  final Map<String, mapi.Module> _modules = {};
  final Map<int, ui.Image> _images = {};
  final Map<String, ui.Image> _moduleBackgroundImages = {};
  final Map<String, mapi.Loc> _locs = {};
  final Map<String, mapi.LocGroup> _locGroups = {};
  final Map<String, mapi.CommandStation> _commandStations = {};
  final Map<String, mapi.Block> _blocks = {};
  final Map<String, mapi.BlockGroup> _blockGroups = {};
  final Map<String, mapi.Edge> _edges = {};
  final Map<String, mapi.Junction> _junctions = {};
  final Map<String, mapi.Output> _outputs = {};
  final Map<String, mapi.Route> _routes = {};
  final Map<String, mapi.Sensor> _sensors = {};
  final Map<String, mapi.Signal> _signals = {};
  final Map<String, mapi.BinkyNetLocalWorker> _binkynetLocalWorkers = {};

  ModelModel();

  // Flush all caches and force a reload.
  Future<void> reloadAll() async {
    // Clear all caches
    _railway = null;
    _railwayEntry = null;
    _modules.clear();
    _images.clear();
    _moduleBackgroundImages.clear();
    _locs.clear();
    _locGroups.clear();
    _commandStations.clear();
    _blocks.clear();
    _blockGroups.clear();
    _edges.clear();
    _junctions.clear();
    _outputs.clear();
    _routes.clear();
    _sensors.clear();
    _signals.clear();
    _binkynetLocalWorkers.clear();

    // Reload
    var modelClient = mapi.APIClient().modelClient();
    _railwayEntry = await modelClient.getRailwayEntry(mapi.Empty());
    if (loadedRailwayEntryName().isNotEmpty) {
      _railway = await modelClient.getRailway(mapi.Empty());
    } else {
      _railway = null;
    }
    notifyListeners();
  }

  // Gets the name of the loaded entry (if any).
  String loadedRailwayEntryName() => _railwayEntry?.name ?? "";

  // Is a railway already loaded?
  bool isRailwayLoaded() =>
      loadedRailwayEntryName().isNotEmpty && _railway != null;

  // Is the currently loaded railway modified since last save?
  bool isRailwayModified() => _railway?.dirty ?? false;

  void requireRailwayLoaded() {
    if (!isRailwayLoaded()) {
      throw Exception("Railway is not loaded");
    }
  }

  // Gets the title of the currently loaded railway
  String title() {
    return "${_railway?.description ?? '<no railway loaded>'}${_railway?.dirty ?? false ? "*" : ""}";
  }

  // Load the given railway entry and make it current.
  Future<void> loadRailway(RailwayEntry entry) async {
    var modelClient = mapi.APIClient().modelClient();
    await modelClient.loadRailway(entry);
    await reloadAll();
  }

  // Create a new railway entry and load it.
  Future<void> createAndLoadRailway(String name) async {
    var storageClient = mapi.APIClient().storageClient();
    final entry =
        await storageClient.createRailwayEntry(CreateRailwayEntryRequest(
      name: name,
    ));
    await loadRailway(entry);
  }

  // Close the current railway.
  Future<void> closeRailway() async {
    var modelClient = mapi.APIClient().modelClient();
    await modelClient.closeRailway(mapi.Empty());
    await reloadAll();
  }

  // Save changes made in the model
  Future<void> save() async {
    requireRailwayLoaded();
    var modelClient = mapi.APIClient().modelClient();
    await modelClient.save(mapi.Empty());
    _railway = await modelClient.getRailway(mapi.Empty());
    notifyListeners();
  }

  mapi.Railway? getCachedRailway() => _railway;

  Future<mapi.RailwayEntry> getRailwayEntry() async {
    var modelClient = mapi.APIClient().modelClient();
    _railwayEntry = await modelClient.getRailwayEntry(mapi.Empty());
    notifyListeners();
    return _railwayEntry!;
  }

  Future<mapi.Railway> getRailway() async {
    if (_railway == null) {
      var modelClient = mapi.APIClient().modelClient();
      _railway = await modelClient.getRailway(mapi.Empty());
      notifyListeners();
    }
    return _railway!;
  }

  // Update the given railway
  Future<void> updateRailway(mapi.Railway value) async {
    requireRailwayLoaded();
    var modelClient = mapi.APIClient().modelClient();
    _railway = await modelClient.updateRailway(value);
    notifyListeners();
  }

  // Decode an image, using the cache if possible.
  Future<ui.Image> _getImage(String base64Encoded) async {
    final key = base64Encoded.hashCode;
    var result = _images[key];
    if (result != null) {
      return result;
    }
    final decoded = await decodeImageFromList(base64Decode(base64Encoded));
    _images[key] = decoded;
    return decoded;
  }

  // Gets a module by ID from cache
  mapi.Module? getCachedModule(String id) => _modules[id];

  // Gets a module by ID
  Future<mapi.Module> getModule(String id) async {
    var result = _modules[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    result = await modelClient.getModule(mapi.IDRequest(id: id));
    _modules[id] = result;
    notifyListeners();
    return result;
  }

  // Gets the background image of a module by ID
  Future<ui.Image?> getModuleBackgroundImage(String id) async {
    final mod = await getModule(id);
    if (!mod.hasBackgroundImage) {
      return null;
    }
    // Load from cache
    final cached = _moduleBackgroundImages[id];
    if (cached != null) {
      return cached;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    final image =
        await modelClient.getModuleBackgroundImage(mapi.IDRequest(id: id));
    final decoded = await _getImage(image.contentBase64);
    _moduleBackgroundImages[id] = decoded;
    return decoded;
  }

  // Update the given module
  Future<void> updateModule(mapi.Module value) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.updateModule(value);
    _modules[updated.id] = updated;
    notifyListeners();
  }

  // Update the background image of the given module
  Future<void> updateModuleBackgroundImage(
      mapi.Module value, List<int> image) async {
    var modelClient = mapi.APIClient().modelClient();
    final req = ImageIDRequest(
      id: value.id,
      image: image,
    );
    var updated = await modelClient.updateModuleBackgroundImage(req);
    _modules[updated.id] = updated;
    notifyListeners();
  }

  // Add a new module
  Future<mapi.Module> addModule() async {
    var modelClient = mapi.APIClient().modelClient();
    var added = await modelClient.addModule(mapi.Empty());
    _modules[added.id] = added;
    _railway = await modelClient.getRailway(mapi.Empty());
    notifyListeners();
    return added;
  }

  // Delete the given module
  Future<void> deleteModule(mapi.Module value) async {
    var modelClient = mapi.APIClient().modelClient();
    final id = value.id;
    await modelClient.deleteModule(mapi.IDRequest(id: id));
    _modules.remove(id);
    _railway = await modelClient.getRailway(mapi.Empty());
    notifyListeners();
  }

  // Gets a loc by ID from cache
  mapi.Loc? getCachedLoc(String id) => _locs[id];

  // Gets a loc by ID
  Future<mapi.Loc> getLoc(String id) async {
    var result = _locs[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    result = await modelClient.getLoc(mapi.IDRequest(id: id));
    _locs[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given loc
  Future<void> updateLoc(mapi.Loc value) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.updateLoc(value);
    _locs[updated.id] = updated;
    notifyListeners();
  }

  // Update the image of the given loc
  Future<void> updateLocImage(mapi.Loc value, List<int> image) async {
    var modelClient = mapi.APIClient().modelClient();
    final req = ImageIDRequest(
      id: value.id,
      image: image,
    );
    var updated = await modelClient.updateLocImage(req);
    _locs[updated.id] = updated;
    notifyListeners();
  }

  // Add a new loc
  Future<mapi.Loc> addLoc() async {
    var modelClient = mapi.APIClient().modelClient();
    var added = await modelClient.addLoc(mapi.Empty());
    _locs[added.id] = added;
    _railway = await modelClient.getRailway(mapi.Empty());
    notifyListeners();
    return added;
  }

  // Delete the given loc
  Future<void> deleteLoc(mapi.Loc value) async {
    var modelClient = mapi.APIClient().modelClient();
    final id = value.id;
    await modelClient.deleteLoc(mapi.IDRequest(id: id));
    _locs.remove(id);
    _railway = await modelClient.getRailway(mapi.Empty());
    notifyListeners();
  }

  // Gets a loc group by ID from cache
  mapi.LocGroup? getCachedLocGroup(String id) => _locGroups[id];

  // Gets a loc group by ID
  Future<mapi.LocGroup> getLocGroup(String id) async {
    var result = _locGroups[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    result = await modelClient.getLocGroup(mapi.IDRequest(id: id));
    _locGroups[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given loc group
  Future<void> updateLocGroup(mapi.LocGroup value) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.updateLocGroup(value);
    _locGroups[updated.id] = updated;
    notifyListeners();
  }

  // Update the given loc group
  Future<mapi.LocGroup> addLocGroup() async {
    var modelClient = mapi.APIClient().modelClient();
    var added = await modelClient.addLocGroup(mapi.Empty());
    _locGroups[added.id] = added;
    _railway = await modelClient.getRailway(mapi.Empty());
    notifyListeners();
    return added;
  }

  // Delete the given loc group
  Future<void> deleteLocGroup(mapi.LocGroup value) async {
    var modelClient = mapi.APIClient().modelClient();
    final id = value.id;
    await modelClient.deleteLocGroup(mapi.IDRequest(id: id));
    _locGroups.remove(id);
    _railway = await modelClient.getRailway(mapi.Empty());
    notifyListeners();
  }

  // Gets a CommandStation by ID from cache
  mapi.CommandStation? getCachedCommandStation(String id) =>
      _commandStations[id];

  // Gets a CommandStation by ID
  Future<mapi.CommandStation> getCommandStation(String id) async {
    var result = _commandStations[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    result = await modelClient.getCommandStation(mapi.IDRequest(id: id));
    _commandStations[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given CommandStation
  Future<void> updateCommandStation(mapi.CommandStation value) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.updateCommandStation(value);
    _commandStations[updated.id] = updated;
    notifyListeners();
  }

  // Gets a block by ID from cache
  mapi.Block? getCachedBlock(String id) => _blocks[id];

  // Gets a block by ID
  Future<mapi.Block> getBlock(String id) async {
    var result = _blocks[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    result = await modelClient.getBlock(mapi.IDRequest(id: id));
    _blocks[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given block
  Future<void> updateBlock(mapi.Block value) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.updateBlock(value);
    _blocks[updated.id] = updated;
    _modules[updated.moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: updated.moduleId));
    notifyListeners();
  }

  // Add a new block
  Future<mapi.Block> addBlock(String moduleId) async {
    var modelClient = mapi.APIClient().modelClient();
    var added = await modelClient.addBlock(mapi.IDRequest(id: moduleId));
    _blocks[added.id] = added;
    _modules[moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: moduleId));
    notifyListeners();
    return added;
  }

  // Delete the given block
  Future<void> deleteBlock(mapi.Block value) async {
    var modelClient = mapi.APIClient().modelClient();
    final updated = await modelClient.deleteBlock(mapi.IDRequest(id: value.id));
    _blocks.remove(value.id);
    _modules[updated.id] = updated;
    notifyListeners();
  }

  // Gets a block group by ID from cache
  mapi.BlockGroup? getCachedBlockGroup(String id) => _blockGroups[id];

  // Gets a block group by ID
  Future<mapi.BlockGroup> getBlockGroup(String id) async {
    var result = _blockGroups[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    result = await modelClient.getBlockGroup(mapi.IDRequest(id: id));
    _blockGroups[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given block group
  Future<void> updateBlockGroup(mapi.BlockGroup value) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.updateBlockGroup(value);
    _blockGroups[updated.id] = updated;
    _modules[updated.moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: updated.moduleId));
    notifyListeners();
  }

  // Add a new block group
  Future<mapi.BlockGroup> addBlockGroup(String moduleId) async {
    var modelClient = mapi.APIClient().modelClient();
    var added = await modelClient.addBlockGroup(mapi.IDRequest(id: moduleId));
    _blockGroups[added.id] = added;
    _modules[moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: moduleId));
    notifyListeners();
    return added;
  }

  // Delete the given block group
  Future<void> deleteBlockGroup(mapi.BlockGroup value) async {
    var modelClient = mapi.APIClient().modelClient();
    final updated =
        await modelClient.deleteBlockGroup(mapi.IDRequest(id: value.id));
    _blockGroups.remove(value.id);
    _modules[updated.id] = updated;
    notifyListeners();
  }

  // Gets an edge by ID from cache
  mapi.Edge? getCachedEdge(String id) => _edges[id];

  // Gets an edge by ID
  Future<mapi.Edge> getEdge(String id) async {
    var result = _edges[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    result = await modelClient.getEdge(mapi.IDRequest(id: id));
    _edges[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given edge
  Future<void> updateEdge(mapi.Edge value) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.updateEdge(value);
    _edges[updated.id] = updated;
    _modules[updated.moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: updated.moduleId));
    notifyListeners();
  }

  // Add a new edge
  Future<mapi.Edge> addEdge(String moduleId) async {
    var modelClient = mapi.APIClient().modelClient();
    var added = await modelClient.addEdge(mapi.IDRequest(id: moduleId));
    _edges[added.id] = added;
    _modules[moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: moduleId));
    notifyListeners();
    return added;
  }

  // Delete the given edge
  Future<void> deleteEdge(mapi.Edge value) async {
    var modelClient = mapi.APIClient().modelClient();
    final updated = await modelClient.deleteEdge(mapi.IDRequest(id: value.id));
    _edges.remove(value.id);
    _modules[updated.id] = updated;
    notifyListeners();
  }

  // Gets a Junction by ID from cache
  mapi.Junction? getCachedJunction(String id) => _junctions[id];

  // Gets a Junction by ID
  Future<mapi.Junction> getJunction(String id) async {
    var result = _junctions[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    result = await modelClient.getJunction(mapi.IDRequest(id: id));
    _junctions[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given Junction
  Future<void> updateJunction(mapi.Junction value) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.updateJunction(value);
    _junctions[updated.id] = updated;
    _modules[updated.moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: updated.moduleId));
    notifyListeners();
  }

  // Add a new junction of type switch
  Future<mapi.Junction> addSwitch(String moduleId) async {
    var modelClient = mapi.APIClient().modelClient();
    var added = await modelClient.addSwitch(mapi.IDRequest(id: moduleId));
    _junctions[added.id] = added;
    _modules[moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: moduleId));
    notifyListeners();
    return added;
  }

  // Delete the given junction
  Future<void> deleteJunction(mapi.Junction value) async {
    var modelClient = mapi.APIClient().modelClient();
    final updated =
        await modelClient.deleteJunction(mapi.IDRequest(id: value.id));
    _junctions.remove(value.id);
    _modules[updated.id] = updated;
    notifyListeners();
  }

  // Gets a Output by ID from cache
  mapi.Output? getCachedOutput(String id) => _outputs[id];

  // Gets a Output by ID
  Future<mapi.Output> getOutput(String id) async {
    var result = _outputs[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    result = await modelClient.getOutput(mapi.IDRequest(id: id));
    _outputs[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given Output
  Future<void> updateOutput(mapi.Output value) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.updateOutput(value);
    _outputs[updated.id] = updated;
    _modules[updated.moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: updated.moduleId));
    notifyListeners();
  }

  // Add a new output of type binary output
  Future<mapi.Output> addBinaryOutput(String moduleId) async {
    var modelClient = mapi.APIClient().modelClient();
    var added = await modelClient.addBinaryOutput(mapi.IDRequest(id: moduleId));
    _outputs[added.id] = added;
    _modules[moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: moduleId));
    notifyListeners();
    return added;
  }

  // Delete the given output
  Future<void> deleteOutput(mapi.Output value) async {
    var modelClient = mapi.APIClient().modelClient();
    final updated =
        await modelClient.deleteOutput(mapi.IDRequest(id: value.id));
    _outputs.remove(value.id);
    _modules[updated.id] = updated;
    notifyListeners();
  }

  // Gets a Route by ID from cache
  mapi.Route? getCachedRoute(String id, {bool load = false}) {
    final result = _routes[id];
    if ((result == null) && load) {
      getRoute(id);
    }
    return result;
  }

  // Gets a Route by ID
  Future<mapi.Route> getRoute(String id) async {
    var result = _routes[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    result = await modelClient.getRoute(mapi.IDRequest(id: id));
    _routes[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given Route
  Future<void> updateRoute(mapi.Route value) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.updateRoute(value);
    _routes[updated.id] = updated;
    _modules[updated.moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: updated.moduleId));
    notifyListeners();
  }

  // Add a new route
  Future<mapi.Route> addRoute(String moduleId) async {
    var modelClient = mapi.APIClient().modelClient();
    var added = await modelClient.addRoute(mapi.IDRequest(id: moduleId));
    _routes[added.id] = added;
    _modules[moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: moduleId));
    notifyListeners();
    return added;
  }

  // Delete the given route
  Future<void> deleteRoute(mapi.Route value) async {
    var modelClient = mapi.APIClient().modelClient();
    final updated = await modelClient.deleteRoute(mapi.IDRequest(id: value.id));
    _routes.remove(value.id);
    _modules[updated.id] = updated;
    notifyListeners();
  }

  // Add a crossing junction (of type switch) to a given route.
  Future<mapi.Route> addRouteCrossingJunctionSwitch(
      String routeId, String junctionId, mapi.SwitchDirection direction) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.addRouteCrossingJunctionSwitch(
        mapi.AddRouteCrossingJunctionSwitchRequest(
      routeId: routeId,
      junctionId: junctionId,
      direction: direction,
    ));
    _routes[updated.id] = updated;
    notifyListeners();
    return updated;
  }

  // Remove a crossing junction from a given route.
  Future<mapi.Route> removeRouteCrossingJunction(
      String routeId, String junctionId) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient
        .removeRouteCrossingJunction(mapi.RemoveRouteCrossingJunctionRequest(
      routeId: routeId,
      junctionId: junctionId,
    ));
    _routes[updated.id] = updated;
    notifyListeners();
    return updated;
  }

  // Add an output (of type binary output) to a given route.
  Future<mapi.Route> addRouteBinaryOutput(
      String routeId, String outputId, bool active) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated =
        await modelClient.addRouteBinaryOutput(mapi.AddRouteBinaryOutputRequest(
      routeId: routeId,
      outputId: outputId,
      active: active,
    ));
    _routes[updated.id] = updated;
    notifyListeners();
    return updated;
  }

  // Remove an output from a given route.
  Future<mapi.Route> removeRouteOutput(String routeId, String outputId) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated =
        await modelClient.removeRouteOutput(mapi.RemoveRouteOutputRequest(
      routeId: routeId,
      outputId: outputId,
    ));
    _routes[updated.id] = updated;
    notifyListeners();
    return updated;
  }

  // Add an event to a route.
  Future<mapi.Route> addRouteEvent(String routeId, String sensorId) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.addRouteEvent(mapi.AddRouteEventRequest(
      routeId: routeId,
      sensorId: sensorId,
    ));
    _routes[updated.id] = updated;
    notifyListeners();
    return updated;
  }

  // Remove an event from a route.
  Future<mapi.Route> removeRouteEvent(String routeId, String sensorId) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated =
        await modelClient.removeRouteEvent(mapi.RemoveRouteEventRequest(
      routeId: routeId,
      sensorId: sensorId,
    ));
    _routes[updated.id] = updated;
    notifyListeners();
    return updated;
  }

  // Add a behavior to an event in a route.
  Future<mapi.Route> addRouteEventBehavior(
      String routeId, String sensorId) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient
        .addRouteEventBehavior(mapi.AddRouteEventBehaviorRequest(
      routeId: routeId,
      sensorId: sensorId,
    ));
    _routes[updated.id] = updated;
    notifyListeners();
    return updated;
  }

  // Remove a behavior from an event in a route.
  Future<mapi.Route> removeRouteEventBehavior(
      String routeId, String sensorId, int index) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient
        .removeRouteEventBehavior(mapi.RemoveRouteEventBehaviorRequest(
      routeId: routeId,
      sensorId: sensorId,
      index: index,
    ));
    _routes[updated.id] = updated;
    notifyListeners();
    return updated;
  }

  // Gets a Sensor by ID from cache
  mapi.Sensor? getCachedSensor(String id) => _sensors[id];

  // Gets a Sensor by ID
  Future<mapi.Sensor> getSensor(String id) async {
    var result = _sensors[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    result = await modelClient.getSensor(mapi.IDRequest(id: id));
    _sensors[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given Sensor
  Future<void> updateSensor(mapi.Sensor value) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.updateSensor(value);
    _sensors[updated.id] = updated;
    _modules[updated.moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: updated.moduleId));
    notifyListeners();
  }

  // Add a new output of type binary sensor
  Future<mapi.Sensor> addBinarySensor(String moduleId) async {
    var modelClient = mapi.APIClient().modelClient();
    var added = await modelClient.addBinarySensor(mapi.IDRequest(id: moduleId));
    _sensors[added.id] = added;
    _modules[moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: moduleId));
    notifyListeners();
    return added;
  }

  // Delete the given sensor
  Future<void> deleteSensor(mapi.Sensor value) async {
    var modelClient = mapi.APIClient().modelClient();
    final updated =
        await modelClient.deleteSensor(mapi.IDRequest(id: value.id));
    _sensors.remove(value.id);
    _modules[updated.id] = updated;
    notifyListeners();
  }

  // Gets a Signal by ID from cache
  mapi.Signal? getCachedSignal(String id) => _signals[id];

  // Gets a Signal by ID
  Future<mapi.Signal> getSignal(String id) async {
    var result = _signals[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    result = await modelClient.getSignal(mapi.IDRequest(id: id));
    _signals[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given Signal
  Future<void> updateSignal(mapi.Signal value) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.updateSignal(value);
    _signals[updated.id] = updated;
    _modules[updated.moduleId] =
        await modelClient.getModule(mapi.IDRequest(id: updated.moduleId));
    notifyListeners();
  }

  // Delete the given signal
  Future<void> deleteSignal(mapi.Signal value) async {
    var modelClient = mapi.APIClient().modelClient();
    final updated =
        await modelClient.deleteSignal(mapi.IDRequest(id: value.id));
    _signals.remove(value.id);
    _modules[updated.id] = updated;
    notifyListeners();
  }

  // Gets a BinkyNetLocalWorker by ID from cache
  mapi.BinkyNetLocalWorker? getCachedBinkyNetLocalWorker(String id) =>
      _binkynetLocalWorkers[id];

  // Gets a BinkyNetLocalWorker by ID
  Future<mapi.BinkyNetLocalWorker> getBinkyNetLocalWorker(String id) async {
    var result = _binkynetLocalWorkers[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = mapi.APIClient().modelClient();
    result = await modelClient.getBinkyNetLocalWorker(mapi.IDRequest(id: id));
    _binkynetLocalWorkers[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given BinkyNetLocalWorker
  Future<void> updateBinkyNetLocalWorker(mapi.BinkyNetLocalWorker value) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient.updateBinkyNetLocalWorker(value);
    _binkynetLocalWorkers[updated.id] = updated;
    notifyListeners();
  }

  // Add a new local worker
  Future<mapi.BinkyNetLocalWorker> addBinkyNetLocalWorker(
      String commandStationId) async {
    var modelClient = mapi.APIClient().modelClient();
    var added = await modelClient
        .addBinkyNetLocalWorker(mapi.IDRequest(id: commandStationId));
    _commandStations[commandStationId] = await modelClient
        .getCommandStation(mapi.IDRequest(id: commandStationId));
    notifyListeners();
    return added;
  }

  // Delete the given local worker
  Future<void> deleteBinkyNetLocalWorker(mapi.BinkyNetLocalWorker value) async {
    var modelClient = mapi.APIClient().modelClient();
    final updated = await modelClient
        .deleteBinkyNetLocalWorker(mapi.IDRequest(id: value.id));
    _binkynetLocalWorkers.remove(value.id);
    _commandStations[updated.id] = updated;
    notifyListeners();
  }

  // Add a new device to a binkynet local worker
  Future<mapi.BinkyNetDevice> addBinkyNetDevice(String localWorkerId) async {
    var modelClient = mapi.APIClient().modelClient();
    var added =
        await modelClient.addBinkyNetDevice(mapi.IDRequest(id: localWorkerId));
    _binkynetLocalWorkers[localWorkerId] = await modelClient
        .getBinkyNetLocalWorker(mapi.IDRequest(id: localWorkerId));
    notifyListeners();
    return added;
  }

  // Delete the given binkynet device from the given local worker
  Future<void> deleteBinkyNetDevice(
      mapi.BinkyNetLocalWorker lw, mapi.BinkyNetDevice value) async {
    var modelClient = mapi.APIClient().modelClient();
    _binkynetLocalWorkers[lw.id] = await modelClient
        .deleteBinkyNetDevice(mapi.SubIDRequest(id: lw.id, subId: value.id));
    notifyListeners();
  }

  // Add a new object to a binkynet local worker
  Future<mapi.BinkyNetObject> addBinkyNetObject(String localWorkerId) async {
    var modelClient = mapi.APIClient().modelClient();
    var added =
        await modelClient.addBinkyNetObject(mapi.IDRequest(id: localWorkerId));
    _binkynetLocalWorkers[localWorkerId] = await modelClient
        .getBinkyNetLocalWorker(mapi.IDRequest(id: localWorkerId));
    notifyListeners();
    return added;
  }

  // Delete the given binkynet object from the given local worker
  Future<void> deleteBinkyNetObject(
      mapi.BinkyNetLocalWorker lw, mapi.BinkyNetObject value) async {
    var modelClient = mapi.APIClient().modelClient();
    _binkynetLocalWorkers[lw.id] = await modelClient
        .deleteBinkyNetObject(mapi.SubIDRequest(id: lw.id, subId: value.id));
    notifyListeners();
  }

  // Add a new object to a binkynet local worker
  Future<mapi.BinkyNetLocalWorker> addBinkyNetObjectsGroup(String localWorkerId,
      String bnDeviceId, mapi.BinkyNetObjectsGroupType type) async {
    var modelClient = mapi.APIClient().modelClient();
    var updated = await modelClient
        .addBinkyNetObjectsGroup(mapi.AddBinkyNetObjectsGroupRequest(
      localWorkerId: localWorkerId,
      deviceId: bnDeviceId,
      type: type,
    ));
    _binkynetLocalWorkers[localWorkerId] = updated;
    notifyListeners();
    return updated;
  }
}
