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

import 'package:flutter/material.dart';
import 'package:flame/input.dart';

import '../block_component.dart' as common;
import '../../api.dart' as mapi;
import '../../editor/editor_context.dart';

class BlockComponent extends common.BlockComponent {
  final EditorContext editorCtx;

  BlockComponent({required this.editorCtx, required mapi.Block model})
      : super(model: model);

  @override
  bool onTapUp(TapUpInfo event) {
    editorCtx.select(
        EntitySelector.parentChild(EntityType.block, model.moduleId, model.id));
    return true;
  }

  _isSelected() =>
      editorCtx.selector.entityType == EntityType.block &&
      editorCtx.selector.id == model.id;

  @override
  backgroundColor() => _isSelected() ? Colors.orange : super.backgroundColor();
}
