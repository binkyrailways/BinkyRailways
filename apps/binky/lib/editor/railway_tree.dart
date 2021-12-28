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

class RailwayTree extends StatelessWidget {
  const RailwayTree({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        return FutureBuilder<Railway>(
            future: model.getRailway(),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var rw = snapshot.data!;
              return ListView(
                children: <Widget>[
                  ListTile(
                    leading: const Icon(Icons.book),
                    title: const Text("Railway"),
                    selected: selector.entityType == EntityType.railway,
                    onTap: () => editorCtx
                        .select(EntitySelector.railway(EntityType.railway)),
                  ),
                  ListTile(
                    leading: const Icon(Icons.view_module_sharp),
                    title: Text("Modules (${rw.modules.length})"),
                    selected: selector.entityType == EntityType.modules,
                    onTap: () => editorCtx
                        .select(EntitySelector.railway(EntityType.modules)),
                  ),
                  ListTile(
                    leading: const Icon(Icons.train_sharp),
                    title: Text("Locs (${rw.locs.length})"),
                    selected: selector.entityType == EntityType.locs,
                    onTap: () => editorCtx
                        .select(EntitySelector.railway(EntityType.locs)),
                  ),
                  ListTile(
                    leading: const Icon(Icons.info_outline),
                    title: const Text("Loc groups"),
                    selected: selector.entityType == EntityType.locgroups,
                    onTap: () => editorCtx
                        .select(EntitySelector.railway(EntityType.locgroups)),
                  ),
                  ListTile(
                    leading: const Icon(Icons.computer),
                    title: const Text("Command stations"),
                    selected: selector.entityType == EntityType.commandstations,
                    onTap: () => editorCtx.select(
                        EntitySelector.railway(EntityType.commandstations)),
                  ),
                ],
              );
            });
      },
    );
  }
}
