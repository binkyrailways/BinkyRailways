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
                    leading: const Text(""),
                    title: Text(
                      "Assigned locs (${assignedLocs.length})",
                      style: const TextStyle(fontStyle: FontStyle.italic),
                    ));
              } else if (index == 1 + assignedLocs.length) {
                return ListTile(
                    dense: true,
                    minLeadingWidth: 30,
                    leading: const Text(""),
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
                final stateText = loc.stateText;
                final canBeControlledAutomatically =
                    loc.canBeControlledAutomatically;
                return ListTile(
                  minLeadingWidth: 30,
                  dense: true,
                  visualDensity: VisualDensity.compact,
                  leading: canBeControlledAutomatically
                      ? GestureDetector(
                          child: Icon(
                            loc.controlledAutomaticallyActual
                                ? Icons.computer
                                : Icons.person,
                            color: loc.controlledAutomaticallyActual ==
                                    loc.controlledAutomaticallyRequested
                                ? Colors.black
                                : Colors.orange,
                            size: 24,
                          ),
                          onTap: () async {
                            await state.setLocControlledAutomatically(
                                loc.model.id,
                                !loc.controlledAutomaticallyRequested);
                          },
                        )
                      : const Icon(
                          Icons.indeterminate_check_box_outlined,
                          size: 24,
                        ),
                  title: Text(loc.model.description),
                  subtitle: (stateText.isNotEmpty) ? Text(loc.stateText) : null,
                  selected: selectedLocId == loc.model.id,
                  onTap: () {
                    runCtx.selectLoc(loc.model.id);
                  },
                  trailing: isAssigned
                      ? GestureDetector(
                          child: const Icon(Icons.more_vert),
                          onTapDown: (TapDownDetails details) {
                            showMenu(
                              context: context,
                              useRootNavigator: true,
                              position: RelativeRect.fromLTRB(
                                  details.globalPosition.dx,
                                  details.globalPosition.dy,
                                  details.globalPosition.dx,
                                  details.globalPosition.dy),
                              items: [
                                PopupMenuItem<String>(
                                    child: const Text('Take of track'),
                                    onTap: () async {
                                      await state.takeLocOfTrack(loc.model.id);
                                    }),
                              ],
                              elevation: 8.0,
                            );
                          },
                        )
                      : null,
                );
              }
            });
      },
    );
  }
}
