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

import 'package:binky/api/generated/br_model_types.pbenum.dart';

extension SwitchDirectionExt on SwitchDirection {
  SwitchDirection invert() {
    if (this == SwitchDirection.STRAIGHT) {
      return SwitchDirection.OFF;
    } else {
      return SwitchDirection.STRAIGHT;
    }
  }
}
