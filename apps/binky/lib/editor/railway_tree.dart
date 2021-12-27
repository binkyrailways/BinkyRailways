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

import 'package:binky/editor/editor_context.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'package:binky/models/editor_model.dart';

class RailwayTree extends StatelessWidget {
  final ContextSetter _contextSetter;
  const RailwayTree({Key? key, required ContextSetter contextSetter})
      : _contextSetter = contextSetter,
        super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorModel>(
      builder: (context, editor, child) {
        var rw = editor.railway();
        return ListView(
          children: <Widget>[
            ListTile(
              leading: const Icon(Icons.book),
              title: const Text("Railway"),
              onTap: () =>
                  _contextSetter(EditorContext.railway(EntityType.railway)),
            ),
            ListTile(
              leading: const Icon(Icons.view_module_sharp),
              title: Text("Modules (${rw.modules.length})"),
              onTap: () =>
                  _contextSetter(EditorContext.railway(EntityType.modules)),
            ),
            ListTile(
              leading: const Icon(Icons.train_sharp),
              title: const Text("Locs"),
              onTap: () =>
                  _contextSetter(EditorContext.railway(EntityType.locs)),
            ),
            ListTile(
              leading: const Icon(Icons.info_outline),
              title: const Text("Loc groups"),
              onTap: () =>
                  _contextSetter(EditorContext.railway(EntityType.locgroups)),
            ),
            ListTile(
              leading: const Icon(Icons.computer),
              title: const Text("Command stations"),
              onTap: () => _contextSetter(
                  EditorContext.railway(EntityType.commandstations)),
            ),
          ],
        );
      },
    );
  }
}
