// Copyright 2024 Ewout Prangsma
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

import 'package:binky/api/generated/br_app_types.pb.dart';
import 'package:binky/api/generated/br_model_types.pb.dart';

import '../api.dart' as mapi;
import 'package:flutter/material.dart';

class AppModel extends ChangeNotifier {
  mapi.AppInfo? _appInfo;

  AppModel();

  // Update app info
  Future<AppInfo?> updateAppInfo() async {
    try {
      final appClient = mapi.APIClient().appClient();
      _appInfo = await appClient.getAppInfo(Empty());
    } catch (e) {
      _appInfo = null;
    }
    notifyListeners();
    return _appInfo;
  }

  // getAppInfo returns the cached app info, unless no such cached
  // info is available. In those cases, app info is actively loaded.
  Future<mapi.AppInfo> getAppInfo() async {
    final cached = _appInfo;
    if (cached != null) {
      return cached;
    } else {
      final appClient = mapi.APIClient().appClient();
      final result = await appClient.getAppInfo(Empty());
      _appInfo = result;
      notifyListeners();
      return result;
    }
  }
}
