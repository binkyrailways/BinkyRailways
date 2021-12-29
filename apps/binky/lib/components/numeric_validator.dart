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

class NumericValidator {
  final int? minimum;
  final int? maximum;

  NumericValidator({this.minimum, this.maximum});

  String? validate(String? input) {
    final s = input ?? "";
    if (s.isEmpty) {
      return "Enter a number";
    }
    final x = int.tryParse(s);
    if (x == null) {
      return "Not a valid number";
    }
    if ((minimum != null) && (x < minimum!)) {
      return "Number must be greater or equal then $minimum";
    }
    if ((maximum != null) && (x > maximum!)) {
      return "Number must be less or equal then $maximum";
    }
    return null;
  }
}
