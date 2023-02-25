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

import 'package:flutter/material.dart';
import '../editor/editor_context.dart';

class RunEditorContext extends ChangeNotifier implements EditorContext {
  @override
  EntitySelector selector = EntitySelector.initial();

  @override
  bool get canGoBack => _getParent(selector) != null;

  // If set, the program is in running state
  @override
  bool get isRunningState => true;

  // Derive the parent from the given selector
  EntitySelector? _getParent(EntitySelector current) {
    switch (current.entityType) {
      case EntityType.locgroup:
        return EntitySelector.locGroups();
      default:
        return null;
    }
  }

  @override
  void goBack() {
    selector = _getParent(selector) ?? EntitySelector.initial();
    notifyListeners();
  }

  @override
  void select(EntitySelector selector, {bool notify = true}) {
    this.selector = selector;
    if (notify) {
      notifyListeners();
    }
  }
}
