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

  ModelModel();

  // Is a railway already loaded?
  bool isRailwayLoaded() => _railway != null;

  // Gets the title of the currently loaded railway
  String title() {
    return "${_railway?.description ?? '<no railway loaded>'}${_railway?.dirty ?? false ? "*" : ""}";
  }

  // Save changes made in the model
  Future<void> save() async {
    if (!isRailwayLoaded()) {
      throw Exception("Railwai is not loaded");
    }
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

  Future<void> updateRailway(Railway value) async {
    if (!isRailwayLoaded()) {
      throw Exception("Railwai is not loaded");
    }
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
}