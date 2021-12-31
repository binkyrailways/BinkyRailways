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

import 'package:binky/run/run_context.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models.dart';
import '../api.dart';

class LocsTree extends StatelessWidget {
  const LocsTree({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final runCtx = Provider.of<RunContext>(context);
    final selectedLocId = runCtx.selectedLocId;
    return Consumer<StateModel>(
      builder: (context, state, child) {
        final allLocs = state.locs().toList()
          ..sort((a, b) => a.model.description.compareTo(b.model.description));
        final assignedLocs =
            allLocs.where((x) => x.canBeControlledAutomatically).toList();
        final unassignedLocs =
            allLocs.where((x) => !x.canBeControlledAutomatically).toList();
        return ListView.builder(
            itemCount: allLocs.length + 2,
            itemBuilder: (context, index) {
              if (index == 0) {
                return Expanded(
                    child: Text(
                  "Assigned locs (${assignedLocs.length})",
                  textAlign: TextAlign.center,
                ));
              } else if (index == 1 + assignedLocs.length) {
                return Expanded(
                    child: Text(
                  "Unassigned locs (${unassignedLocs.length})",
                  textAlign: TextAlign.center,
                ));
              } else {
                index = index - 1;
                final loc = (index < assignedLocs.length)
                    ? assignedLocs[index]
                    : unassignedLocs[index - (assignedLocs.length + 1)];
                final stateText = loc.stateText;
                final canBeControlledAutomatically =
                    loc.canBeControlledAutomatically;
                return ListTile(
                  leading: canBeControlledAutomatically
                      ? Checkbox(
                          onChanged: (bool? value) {},
                          value: loc.controlledAutomaticallyActual,
                        )
                      : null,
                  title: Text(loc.model.description),
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
}
