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

import 'package:binky/run/run_context.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models.dart';
import './locs_tree_card.dart';

class LocsTree extends StatefulWidget {
  const LocsTree({Key? key}) : super(key: key);

  @override
  State<LocsTree> createState() => _LocsTreeState();
}

class _LocsTreeState extends State<LocsTree> {
  @override
  Widget build(BuildContext context) {
    final runCtx = Provider.of<RunContext>(context);
    final selectedLocId = runCtx.selectedLocId;
    return Consumer<StateModel>(
      builder: (context, state, child) {
        final allLocs = state.locs().map((x) => x.last).toList()
          ..sort((a, b) => a.model.description.compareTo(b.model.description));
        final enabledLocs = allLocs.where((x) => x.isEnabled).toList();
        final disabledLocs = allLocs.where((x) => !x.isEnabled).toList();
        final assignedLocs =
            enabledLocs.where((x) => x.canBeControlledAutomatically).toList();
        final unassignedLocs =
            enabledLocs.where((x) => !x.canBeControlledAutomatically).toList();

        final assignedHeaderIndex = 0;
        final unassignedHeaderIndex = assignedLocs.length + 1;

        final locListView = ListView.builder(
            itemCount: enabledLocs.length + 2,
            itemBuilder: (context, index) {
              if (index == assignedHeaderIndex) {
                return ListTile(
                    trailing: const Text(""),
                    title: Text(
                      "Assigned locs (${assignedLocs.length})",
                      style: const TextStyle(
                          fontStyle: FontStyle.italic,
                          fontWeight: FontWeight.bold),
                    ));
              } else if (index == unassignedHeaderIndex) {
                return ListTile(
                    trailing: const Text(""),
                    title: Text(
                      "Unassigned locs (${unassignedLocs.length})",
                      style: const TextStyle(
                          fontStyle: FontStyle.italic,
                          fontWeight: FontWeight.bold),
                    ));
              } else {
                final isAssigned = (index < unassignedHeaderIndex);
                final loc = isAssigned
                    ? assignedLocs[index - (assignedHeaderIndex + 1)]
                    : unassignedLocs[index - (unassignedHeaderIndex + 1)];
                final isSelected = selectedLocId == loc.model.id;

                return LocsTreeCard(
                    loc: loc,
                    state: state,
                    isAssigned: isAssigned,
                    isEnabled: true,
                    isSelected: isSelected,
                    runCtx: runCtx);
              }
            });

        final disabledLocsCards = disabledLocs
            .map((loc) => PopupMenuItem<String>(
                  child: GestureDetector(
                    child: SizedBox(
                      child: LocsTreeCard(
                        loc: loc,
                        state: state,
                        isAssigned: false,
                        isEnabled: false,
                        isSelected: false,
                        runCtx: runCtx,
                      ),
                      width: 240,
                    ),
                  ),
                ))
            .toList();

        return Column(children: [
          Expanded(child: locListView),
          GestureDetector(
            child: const Icon(Icons.add, size: 32),
            onTapDown: (TapDownDetails details) {
              showMenu(
                context: context,
                useRootNavigator: true,
                position: const RelativeRect.fromLTRB(
                  0,
                  0,
                  0,
                  0,
                ),
                items: disabledLocsCards,
                elevation: 8.0,
              );
            },
          ),
        ]);
      },
    );
  }
}
