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

import 'package:flutter/widgets.dart';

import '../api.dart';

class AddressValidator {
  VoidCallback? setState;
  String? _lastResult;

  String? validate(String? input) {
    _validate(input).then((x) {
      _lastResult = x;
      final cb = setState;
      if (cb != null) {
        cb();
      }
    });
    return _lastResult;
  }

  Future<String?> _validate(String? input) async {
    final modelClient = APIClient().modelClient();
    final result =
        await modelClient.parseAddress(ParseAddressRequest(value: input ?? ""));
    if (result.valid) {
      return null;
    }
    return result.message;
  }
}
