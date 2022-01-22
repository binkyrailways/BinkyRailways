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
import '../api.dart';
import '../icons.dart';

class CommandStationTree extends StatelessWidget {
  const CommandStationTree({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        final csId = selector.idOf(EntityType.commandstation) ?? "";
        return FutureBuilder<CommandStation>(
            future: model.getCommandStation(csId),
            initialData: model.getCachedCommandStation(csId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var cs = snapshot.data!;
              final List<Widget> children = [
                ListTile(
                  leading: BinkyIcons.railway,
                  minLeadingWidth: 20,
                  title: const Text("Railway"),
                  onTap: () => editorCtx.select(EntitySelector.railway()),
                ),
                ListTile(
                  leading: BinkyIcons.commandstation,
                  minLeadingWidth: 20,
                  title: const Text(
                    "Command station",
                    overflow: TextOverflow.ellipsis,
                  ),
                  selected: selector.entityType == EntityType.commandstation,
                  onTap: () =>
                      editorCtx.select(EntitySelector.commandStation(cs, null)),
                ),
              ];
              if (cs.hasBinkynetCommandStation()) {
                final bnCs = cs.binkynetCommandStation;
                children.add(
                  ListTile(
                    leading: BinkyIcons.binkynetlocalworker,
                    minLeadingWidth: 20,
                    title: Text(
                      "Local workers (${bnCs.localWorkers.length})",
                      overflow: TextOverflow.ellipsis,
                    ),
                    selected:
                        selector.entityType == EntityType.binkynetlocalworkers,
                    onTap: () => editorCtx
                        .select(EntitySelector.binkynetLocalWorkers(cs, null)),
                  ),
                );
              }
              return ListView(
                children: children,
              );
            });
      },
    );
  }
}
