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
import 'package:binky/api/generated/br_model_service.pbgrpc.dart';
import 'package:flutter/material.dart';

import '../api/generated/br_model_types.pb.dart';

class ModelModel extends ChangeNotifier {
  Railway? _railway;
  final Map<String, Module> _modules = {};
  final Map<String, Loc> _locs = {};
  final Map<String, Block> _blocks = {};

  ModelModel();

  // Is a railway already loaded?
  bool isRailwayLoaded() => _railway != null;

  void requireRailwayLoaded() {
    if (!isRailwayLoaded()) {
      throw Exception("Railway is not loaded");
    }
  }

  // Gets the title of the currently loaded railway
  String title() {
    return "${_railway?.description ?? '<no railway loaded>'}${_railway?.dirty ?? false ? "*" : ""}";
  }

  // Save changes made in the model
  Future<void> save() async {
    requireRailwayLoaded();
    var modelClient = APIClient().modelClient();
    await modelClient.save(Empty());
    _railway = await modelClient.getRailway(Empty());
    notifyListeners();
  }

  Future<Railway> getRailway() async {
    if (_railway == null) {
      var modelClient = APIClient().modelClient();
      _railway = await modelClient.getRailway(Empty());
      notifyListeners();
    }
    return _railway!;
  }

  // Update the given railway
  Future<void> updateRailway(Railway value) async {
    requireRailwayLoaded();
    var modelClient = APIClient().modelClient();
    _railway = await modelClient.updateRailway(value);
    notifyListeners();
  }

  // Gets a module by ID from cache
  Module? getCachedModule(String id) => _modules[id];

  // Gets a module by ID
  Future<Module> getModule(String id) async {
    var result = _modules[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = APIClient().modelClient();
    result = await modelClient.getModule(IDRequest(id: id));
    _modules[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given module
  Future<void> updateModule(Module value) async {
    var modelClient = APIClient().modelClient();
    var updated = await modelClient.updateModule(value);
    _modules[updated.id] = updated;
    notifyListeners();
  }

  // Gets a loc by ID from cache
  Loc? getCachedLoc(String id) => _locs[id];

  // Gets a loc by ID
  Future<Loc> getLoc(String id) async {
    var result = _locs[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = APIClient().modelClient();
    result = await modelClient.getLoc(IDRequest(id: id));
    _locs[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given loc
  Future<void> updateLoc(Loc value) async {
    var modelClient = APIClient().modelClient();
    var updated = await modelClient.updateLoc(value);
    _locs[updated.id] = updated;
    notifyListeners();
  }

  // Gets a block by ID from cache
  Block? getCachedBlock(String id) => _blocks[id];

  // Gets a block by ID
  Future<Block> getBlock(String id) async {
    var result = _blocks[id];
    if (result != null) {
      return result;
    }
    // Load from API
    var modelClient = APIClient().modelClient();
    result = await modelClient.getBlock(IDRequest(id: id));
    _blocks[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given block
  Future<void> updateBlock(Block value) async {
    var modelClient = APIClient().modelClient();
    var updated = await modelClient.updateBlock(value);
    _blocks[updated.id] = updated;
    notifyListeners();
  }
}
