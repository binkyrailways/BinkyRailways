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

class SignalPatternValidator {
  String? validate(String? input) {
    final s = input ?? "";
    if ((s.isEmpty) || (s == _disabledText)) {
      // Valid disabled pattern
      return null;
    }
    if (int.tryParse(s, radix: 2) == null) {
      return "Invalid (bit) pattern";
    }
    return null;
  }

  static const _disabledPattern = 2147483647; // max int32
  static const _disabledText = "-";

  static String patternToString(int value) {
    if (value == _disabledPattern) {
      return _disabledText;
    }
    final str = value.toRadixString(2);
    return str.padLeft(4, '0');
  }

  static int stringToPattern(String value) {
    if (value.isEmpty || (value == _disabledText)) {
      return _disabledPattern;
    }
    return int.parse(value, radix: 2);
  }
}
