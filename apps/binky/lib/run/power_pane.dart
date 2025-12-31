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

import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models.dart';
import '../api.dart';

class PowerPane extends StatelessWidget {
  const PowerPane({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<StateModel>(
      builder: (context, state, child) {
        return FutureBuilder<RailwayState>(
            future: state.getRailwayState(),
            initialData: state.getCachedRailwayState(),
            builder: (context, snapshot) {
              final rwState = snapshot.data;
              final pwActual = rwState?.powerActual ?? false;
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
                        _getPowerText(rwState),
                        overflow: TextOverflow.ellipsis,
                      ),
                      padding: const EdgeInsets.all(8),
                    )),
                    Expanded(
                        child: pwActual
                            ? ElevatedButton(
                                style: offStyle,
                                onPressed: () async {
                                  await state.setPower(false);
                                },
                                child: const Text("Off",
                                    overflow: TextOverflow.ellipsis),
                              )
                            : ElevatedButton.icon(
                                style: offStyle,
                                onPressed: () async {
                                  await state.setPower(false);
                                },
                                icon: const Icon(Icons.check_outlined),
                                label: const Text("Off",
                                    overflow: TextOverflow.ellipsis),
                              )),
                    Container(
                      width: 5,
                    ),
                    Expanded(
                        child: pwActual
                            ? ElevatedButton.icon(
                                style: onStyle,
                                onPressed: () async {
                                  await state.setPower(true);
                                },
                                icon: const Icon(Icons.check_outlined),
                                label: const Text("On",
                                    overflow: TextOverflow.ellipsis),
                              )
                            : ElevatedButton(
                                style: onStyle,
                                onPressed: () async {
                                  await state.setPower(true);
                                },
                                child: const Text("On",
                                    overflow: TextOverflow.ellipsis),
                              )),
                  ]));
            });
      },
    );
  }

  String _getPowerText(final RailwayState? rwState) {
    if (rwState == null) {
      return "Loading ...";
    }
    if (rwState.powerActual == rwState.powerRequested) {
      return rwState.powerActual ? "Power is On" : "Power is Off";
    }
    return rwState.powerRequested
        ? "Power is turning On"
        : "Power is turning Off";
  }
}
