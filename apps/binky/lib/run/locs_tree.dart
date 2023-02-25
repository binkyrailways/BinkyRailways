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

import 'package:binky/api.dart';
import 'package:binky/components.dart';
import 'package:binky/components/_more_popup_menu.dart';
import 'package:binky/editor/editor_context.dart';
import 'package:binky/run/run_context.dart';
import 'package:flame/extensions.dart';
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
                final autoControlled = loc.controlledAutomaticallyActual;
                final autoControlledConsistent =
                    loc.controlledAutomaticallyActual ==
                        loc.controlledAutomaticallyRequested;
                final actions = _buildActions(context, state, loc, isAssigned);
                final icon = canBeControlledAutomatically
                    ? autoControlled
                        ? Icons.auto_mode
                        : Icons.sports_esports
                    : Icons.more_horiz;
                return ListTile(
                  minLeadingWidth: 30,
                  dense: true,
                  visualDensity: VisualDensity.compact,
                  leading: (actions.isNotEmpty)
                      ? MorePopupMenu(
                          icon: icon,
                          iconColor: canBeControlledAutomatically &&
                                  !autoControlledConsistent
                              ? Colors.orange
                              : null,
                          iconSize: 24,
                          items: actions,
                        )
                      : Icon(icon, size: 24),
                  title: Row(children: [
                    Text(loc.model.description),
                    Expanded(child: Container()),
                    SizedBox(
                        width: 24,
                        height: 6,
                        child: LinearProgressIndicator(
                            color: _getSpeedColor(loc.speedActual / 100),
                            value: loc.speedActual / 100)),
                  ]),
                  subtitle: (stateText.isNotEmpty) ? Text(loc.stateText) : null,
                  selected: selectedLocId == loc.model.id,
                  onTap: () {
                    runCtx.selectLoc(loc.model.id);
                  },
                );
              }
            });
      },
    );
  }

  List<PopupMenuItem<String>> _buildActions(
      BuildContext context, StateModel state, LocState loc, bool isAssigned) {
    final canBeControlledAutomatically = loc.canBeControlledAutomatically;
    final List<PopupMenuItem<String>> actions = [];

    if (canBeControlledAutomatically) {
      if (loc.controlledAutomaticallyRequested) {
        actions.add(IconPopupMenuItem(
            icon: const Icon(Icons.sports_esports),
            child: const Text("Control manually"),
            onTap: () async {
              await state.setLocControlledAutomatically(loc.model.id, false);
            }));
      } else {
        actions.add(IconPopupMenuItem(
            icon: const Icon(Icons.auto_mode),
            child: const Text("Control automatically"),
            onTap: () async {
              await state.setLocControlledAutomatically(loc.model.id, true);
            }));
      }
    }

    if (isAssigned) {
      actions.add(IconPopupMenuItem(
          icon: const Icon(Icons.cancel),
          child: const Text('Take of track'),
          onTap: () async {
            await state.takeLocOfTrack(loc.model.id);
          }));
    }

    actions.add(IconPopupMenuItem(
        icon: const Icon(Icons.edit),
        child: const Text('Edit'),
        onTap: () {
          final editorCtx = Provider.of<EditorContext>(context, listen: false);
          editorCtx.select(EntitySelector.loc(loc.model));
        }));

    return actions;
  }

  Color _getSpeedColor(double speed) {
    return Colors.green
        .darken(1.0 - speed)
        .withAlpha(96 + (speed * 100).round());
  }
}
