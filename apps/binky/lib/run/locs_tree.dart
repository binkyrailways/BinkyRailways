// Copyright 2021-2022 Ewout Prangsma
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

import 'package:binky/api.dart' hide Image;
import 'package:binky/editor/editor_context.dart';
import 'package:binky/run/run_context.dart';
import 'package:flame/extensions.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models.dart';
import './locs_tree_card.dart';

class LocsTree extends StatelessWidget {
  const LocsTree({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final runCtx = Provider.of<RunContext>(context);
    final selectedLocId = runCtx.selectedLocId;
    return Consumer<StateModel>(
      builder: (context, state, child) {
        final allLocs = state.locs().map((x) => x.last).toList()
          ..sort((a, b) => a.model.description.compareTo(b.model.description));
        final assignedLocs =
            allLocs.where((x) => x.canBeControlledAutomatically).toList();
        final unassignedLocs =
            allLocs.where((x) => !x.canBeControlledAutomatically).toList();
        return ListView.builder(
            itemCount: allLocs.length + 2,
            itemBuilder: (context, index) {
              if (index == 0) {
                return ListTile(
                    dense: true,
                    minLeadingWidth: 30,
                    trailing: const Text(""),
                    title: Text(
                      "Assigned locs (${assignedLocs.length})",
                      style: const TextStyle(fontStyle: FontStyle.italic),
                    ));
              } else if (index == 1 + assignedLocs.length) {
                return ListTile(
                    dense: true,
                    minLeadingWidth: 30,
                    trailing: const Text(""),
                    title: Text(
                      "Unassigned locs (${unassignedLocs.length})",
                      style: const TextStyle(fontStyle: FontStyle.italic),
                    ));
              } else {
                index = index - 1;
                final isAssigned = (index < assignedLocs.length);
                final loc = isAssigned
                    ? assignedLocs[index]
                    : unassignedLocs[index - (assignedLocs.length + 1)];
                final isSelected = selectedLocId == loc.model.id;

                return LocsTreeCard(
                    loc: loc,
                    state: state,
                    isAssigned: isAssigned,
                    isSelected: isSelected,
                    runCtx: runCtx);
              }
            });
      },
    );
  }
}
