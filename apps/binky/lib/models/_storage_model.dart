// Copyright 2023 Ewout Prangsma
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

import 'package:binky/api/generated/br_storage_service.pbgrpc.dart';
import 'package:binky/api/generated/br_storage_types.pb.dart';

import '../api.dart' as mapi;
import 'package:flutter/material.dart';

class StorageModel extends ChangeNotifier {
  List<RailwayEntry>? _entries;

  StorageModel();

  // Flush all caches and force a reload.
  Future<void> reloadAll() async {
    // Clear all caches
    _entries = null;

    // Reload
    await getRailwayEntries();
  }

  // getRailwayEntries returns the cached railway entries, unless no such cache
  // is available. In those cases, railway entries are actively loaded.
  Future<List<RailwayEntry>> getRailwayEntries() async {
    final cached = _entries;
    if (cached != null) {
      return cached;
    }
    return updateRailwayEntries();
  }

  // Reload railway storage entries
  Future<List<RailwayEntry>> updateRailwayEntries() async {
    var storageClient = mapi.APIClient().storageClient();
    final entries =
        await storageClient.getRailwayEntries(GetRailwayEntriesRequest());
    _entries = entries.items;
    notifyListeners();
    return entries.items;
  }
}
