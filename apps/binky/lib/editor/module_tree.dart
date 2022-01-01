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
import 'package:binky/icons.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models.dart';
import '../api.dart';

class ModuleTree extends StatelessWidget {
  const ModuleTree({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        final moduleId = selector.id ?? "";
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
                    leading: BinkyIcons.module,
                    minLeadingWidth: 20,
                    title: const Text(
                      "Module",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected: selector.entityType == EntityType.module,
                    onTap: () => editorCtx.select(
                        EntitySelector.child(EntityType.module, moduleId)),
                  ),
                  ListTile(
                    leading: BinkyIcons.block,
                    minLeadingWidth: 20,
                    title: Text(
                      "Blocks (${rw.blocks.length})",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected: selector.entityType == EntityType.blocks,
                    onTap: () => editorCtx.select(
                        EntitySelector.child(EntityType.blocks, moduleId)),
                  ),
                  ListTile(
                    leading: BinkyIcons.blockGroup,
                    minLeadingWidth: 20,
                    title: Text(
                      "Block groups (${rw.blockGroups.length})",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected: selector.entityType == EntityType.blockgroups,
                    onTap: () => editorCtx.select(
                        EntitySelector.child(EntityType.blockgroups, moduleId)),
                  ),
                  ListTile(
                    leading: BinkyIcons.edge,
                    minLeadingWidth: 20,
                    title: Text(
                      "Edges (${rw.edges.length})",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected: selector.entityType == EntityType.edges,
                    onTap: () => editorCtx.select(
                        EntitySelector.child(EntityType.edges, moduleId)),
                  ),
                  ListTile(
                    leading: BinkyIcons.junction,
                    minLeadingWidth: 20,
                    title: Text(
                      "Junctions (${rw.junctions.length})",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected: selector.entityType == EntityType.junctions,
                    onTap: () => editorCtx.select(
                        EntitySelector.child(EntityType.junctions, moduleId)),
                  ),
                  ListTile(
                    leading: BinkyIcons.output,
                    minLeadingWidth: 20,
                    title: Text(
                      "Outputs (${rw.outputs.length})",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected: selector.entityType == EntityType.outputs,
                    onTap: () => editorCtx.select(
                        EntitySelector.child(EntityType.outputs, moduleId)),
                  ),
                  ListTile(
                    leading: BinkyIcons.route,
                    minLeadingWidth: 20,
                    title: Text(
                      "Routes (${rw.routes.length})",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected: selector.entityType == EntityType.routes,
                    onTap: () => editorCtx.select(
                        EntitySelector.child(EntityType.routes, moduleId)),
                  ),
                  ListTile(
                    leading: BinkyIcons.sensor,
                    minLeadingWidth: 20,
                    title: Text(
                      "Sensors (${rw.sensors.length})",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected: selector.entityType == EntityType.sensors,
                    onTap: () => editorCtx.select(
                        EntitySelector.child(EntityType.sensors, moduleId)),
                  ),
                  ListTile(
                    leading: BinkyIcons.signal,
                    minLeadingWidth: 20,
                    title: Text(
                      "Signals (${rw.signals.length})",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected: selector.entityType == EntityType.signals,
                    onTap: () => editorCtx.select(
                        EntitySelector.child(EntityType.signals, moduleId)),
                  ),
                ],
              );
            });
      },
    );
  }
}
