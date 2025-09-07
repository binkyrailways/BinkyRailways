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

import 'package:binky/colors.dart';
import 'package:flame/components.dart';
import 'package:flame/input.dart';
import 'package:protobuf/protobuf.dart';

import '../signal_component.dart' as common;
import '../view_settings.dart';
import '../../api.dart' as mapi;
import '../../models.dart';
import '../../editor/editor_context.dart';
import './position_draggable.dart';
import './module_game.dart';

class SignalComponent extends common.SignalComponent
    with Tappable, Draggable, PositionDraggable<mapi.Signal> {
  final EditorContext editorCtx;
  final ModelModel modelModel;
  final ModuleGame game;

  SignalComponent(ViewSettings viewSettings,
      {required this.editorCtx,
      required mapi.Signal model,
      required this.modelModel,
      required this.game})
      : super(viewSettings, model: model);

  @override
  Future<void> savePosition(void Function(mapi.Position) editor) async {
    final current = await modelModel.getSignal(model.id);
    var update = current.deepCopy();
    editor(update.position);
    await modelModel.updateSignal(update);
    editorCtx.select(EntitySelector.signal(model));
  }

  @override
  bool onTapUp(TapUpInfo event) {
    if (onVisibleLayer()) {
      editorCtx.select(EntitySelector.signal(model));
      return false;
    }
    return true;
  }

  _isSelected() => editorCtx.selector.idOf(EntityType.signal) == model.id;

  @override
  backgroundColor() {
    if (_isSelected()) {
      return BinkyColors.selectedBg;
    }
    return BinkyColors.partOfSelectedRouteBg;
  }
}
