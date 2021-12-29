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

import 'package:binky/api/api_client.dart';
import 'package:binky/api/generated/br_model_service.pbgrpc.dart';
import 'package:flutter/material.dart';

import '../api/generated/br_model_types.pb.dart' as mapi;

class ModelModel extends ChangeNotifier {
  mapi.Railway? _railway;
  final Map<String, mapi.Module> _modules = {};
  final Map<int, ui.Image> _images = {};
  final Map<String, ui.Image> _moduleBackgroundImages = {};
  final Map<String, mapi.Loc> _locs = {};
  final Map<String, mapi.Block> _blocks = {};

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
    await modelClient.save(mapi.Empty());
    _railway = await modelClient.getRailway(mapi.Empty());
    notifyListeners();
  }

  Future<mapi.Railway> getRailway() async {
    if (_railway == null) {
      var modelClient = APIClient().modelClient();
      _railway = await modelClient.getRailway(mapi.Empty());
      notifyListeners();
    }
    return _railway!;
  }

  // Update the given railway
  Future<void> updateRailway(mapi.Railway value) async {
    requireRailwayLoaded();
    var modelClient = APIClient().modelClient();
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
    var modelClient = APIClient().modelClient();
    result = await modelClient.getModule(IDRequest(id: id));
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
    var modelClient = APIClient().modelClient();
    final image = await modelClient.getModuleBackgroundImage(IDRequest(id: id));
    final decoded = await _getImage(image.contentBase64);
    _moduleBackgroundImages[id] = decoded;
    return decoded;
  }

  // Update the given module
  Future<void> updateModule(mapi.Module value) async {
    var modelClient = APIClient().modelClient();
    var updated = await modelClient.updateModule(value);
    _modules[updated.id] = updated;
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
    var modelClient = APIClient().modelClient();
    result = await modelClient.getLoc(IDRequest(id: id));
    _locs[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given loc
  Future<void> updateLoc(mapi.Loc value) async {
    var modelClient = APIClient().modelClient();
    var updated = await modelClient.updateLoc(value);
    _locs[updated.id] = updated;
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
    var modelClient = APIClient().modelClient();
    result = await modelClient.getBlock(IDRequest(id: id));
    _blocks[id] = result;
    notifyListeners();
    return result;
  }

  // Update the given block
  Future<void> updateBlock(mapi.Block value) async {
    var modelClient = APIClient().modelClient();
    var updated = await modelClient.updateBlock(value);
    _blocks[updated.id] = updated;
    notifyListeners();
  }
}
