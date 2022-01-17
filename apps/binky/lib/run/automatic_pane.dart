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
              final onShade = (rwState?.powerActual ?? false) ? 200 : 400;
              final offShade = (rwState?.powerActual ?? false) ? 400 : 200;
              return Container(
                  padding: const EdgeInsets.all(8),
                  child: Column(children: [
                    Container(
                      child: Text(_getAutomaticText(rwState)),
                      padding: const EdgeInsets.fromLTRB(8, 0, 8, 8),
                    ),
                    Row(children: [
                      Expanded(
                          child: TextButton(
                        style: ButtonStyle(
                            backgroundColor: MaterialStateProperty.all(
                                Colors.green[onShade]),
                            foregroundColor:
                                MaterialStateProperty.all(Colors.black)),
                        onPressed: () async {
                          await state.setAutomaticControl(true);
                        },
                        child: const Text("Automatic"),
                      )),
                      Container(
                        width: 5,
                      ),
                      Expanded(
                          child: TextButton(
                        style: ButtonStyle(
                            backgroundColor:
                                MaterialStateProperty.all(Colors.red[offShade]),
                            foregroundColor:
                                MaterialStateProperty.all(Colors.black)),
                        onPressed: () async {
                          await state.setAutomaticControl(false);
                        },
                        child: const Text("Manual"),
                      )),
                    ]),
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
