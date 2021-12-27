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
import 'package:flutter/material.dart';

import '../api/generated/br_types.pb.dart';

class EditorModel extends ChangeNotifier {
  Railway? _railway;

  EditorModel() {
    _loadRailway();
  }

  _loadRailway() async {
    var editorClient = APIClient().editor();
    _railway = await editorClient.getRailway(Empty());
    notifyListeners();
  }

  // Is a railway already loaded?
  bool isRailwayLoaded() => _railway != null;

  Railway railway() => _railway ?? Railway();

  // Gets the title of the currently loaded railway
  String title() => _railway?.description ?? '<no railway loaded>';
}
