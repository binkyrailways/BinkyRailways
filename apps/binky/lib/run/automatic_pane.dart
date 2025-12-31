// Copyright 2022 Ewout Prangsma
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

import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models.dart';
import '../api.dart';

class AutomaticPane extends StatelessWidget {
  const AutomaticPane({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<StateModel>(
      builder: (context, state, child) {
        return FutureBuilder<RailwayState>(
            future: state.getRailwayState(),
            initialData: state.getCachedRailwayState(),
            builder: (context, snapshot) {
              final rwState = snapshot.data;
              final acActual = rwState?.automaticControlActual ?? false;
              final onStyle = ButtonStyle(
                  backgroundColor: WidgetStateProperty.all(Colors.green[200]),
                  foregroundColor: WidgetStateProperty.all(Colors.black));
              final offStyle = ButtonStyle(
                  backgroundColor: WidgetStateProperty.all(Colors.red[200]),
                  foregroundColor: WidgetStateProperty.all(Colors.black));
              return Container(
                  padding: const EdgeInsets.all(8),
                  child: Row(children: [
                    Expanded(
                        child: Container(
                      alignment: Alignment.centerLeft,
                      child: Text(
                        _getAutomaticText(rwState),
                        overflow: TextOverflow.ellipsis,
                      ),
                      padding: const EdgeInsets.all(8),
                    )),
                    Expanded(
                        child: acActual
                            ? ElevatedButton(
                                style: offStyle,
                                onPressed: () async {
                                  await state.setAutomaticControl(false);
                                },
                                child: const Text("Manual",
                                    overflow: TextOverflow.ellipsis),
                              )
                            : ElevatedButton.icon(
                                style: offStyle,
                                onPressed: () async {
                                  await state.setAutomaticControl(false);
                                },
                                icon: const Icon(Icons.check_outlined),
                                label: const Text("Manual",
                                    overflow: TextOverflow.ellipsis),
                              )),
                    Container(
                      width: 5,
                    ),
                    Expanded(
                        child: acActual
                            ? ElevatedButton.icon(
                                style: onStyle,
                                onPressed: () async {
                                  await state.setAutomaticControl(true);
                                },
                                icon: const Icon(Icons.check_outlined),
                                label: const Text("Automatic",
                                    overflow: TextOverflow.ellipsis),
                              )
                            : ElevatedButton(
                                style: onStyle,
                                onPressed: () async {
                                  await state.setAutomaticControl(true);
                                },
                                child: const Text("Automatic",
                                    overflow: TextOverflow.ellipsis),
                              )),
                  ]));
            });
      },
    );
  }

  String _getAutomaticText(final RailwayState? rwState) {
    if (rwState == null) {
      return "Loading ...";
    }
    if (rwState.automaticControlActual == rwState.automaticControlRequested) {
      return rwState.automaticControlActual
          ? "Automatic control"
          : "Manual control";
    }
    return rwState.automaticControlRequested
        ? "Automatic control turning On"
        : "Manual control turning On";
  }
}
