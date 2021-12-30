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

import '../models.dart';
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
                    minLeadingWidth: 20,
                    title: const Text(
                      "Railway",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected: selector.entityType == EntityType.railway,
                    onTap: () => editorCtx
                        .select(EntitySelector.railway(EntityType.railway)),
                  ),
                  ListTile(
                    leading: const Icon(Icons.view_module_sharp),
                    minLeadingWidth: 20,
                    title: Text(
                      "Modules (${rw.modules.length})",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected: selector.entityType == EntityType.modules,
                    onTap: () => editorCtx
                        .select(EntitySelector.railway(EntityType.modules)),
                  ),
                  ListTile(
                    leading: const Icon(Icons.train_sharp),
                    minLeadingWidth: 20,
                    title: Text(
                      "Locs (${rw.locs.length})",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected: selector.entityType == EntityType.locs,
                    onTap: () => editorCtx
                        .select(EntitySelector.railway(EntityType.locs)),
                  ),
                  ListTile(
                    leading: const Icon(Icons.info_outline),
                    minLeadingWidth: 20,
                    title: Text(
                      "Loc groups (${rw.locGroups.length})",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected: selector.entityType == EntityType.locgroups,
                    onTap: () => editorCtx
                        .select(EntitySelector.railway(EntityType.locgroups)),
                  ),
                  ListTile(
                    leading: const Icon(Icons.computer),
                    minLeadingWidth: 20,
                    title: Text(
                      "Command stations (${rw.commandStations.length})",
                      overflow: TextOverflow.ellipsis,
                    ),
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
