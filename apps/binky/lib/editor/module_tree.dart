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

import 'package:binky/models/model_model.dart';
import '../api/generated/br_model_types.pb.dart';

class ModuleTree extends StatelessWidget {
  const ModuleTree({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        final moduleId = selector.moduleId ?? "";
        return FutureBuilder<Module>(
            future: model.getModule(moduleId),
            initialData: model.getCachedModule(moduleId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var rw = snapshot.data!;
              return ListView(
                children: <Widget>[
                  ListTile(
                    leading: const Icon(Icons.book),
                    title: const Text("Module"),
                    selected: selector.entityType == EntityType.module,
                    onTap: () => editorCtx.select(
                        EntitySelector.module(EntityType.module, moduleId)),
                  ),
                  ListTile(
                    leading: const Icon(Icons.stop),
                    title: Text("Blocks (${rw.blocks.length})"),
                    selected: selector.entityType == EntityType.blocks,
                    onTap: () => editorCtx.select(
                        EntitySelector.module(EntityType.blocks, moduleId)),
                  ),
                ],
              );
            });
      },
    );
  }
}