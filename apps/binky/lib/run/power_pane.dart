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
import '../api/generated/br_state_types.pb.dart';

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
              final onShade = (rwState?.powerActual ?? false) ? 200 : 400;
              final offShade = (rwState?.powerActual ?? false) ? 400 : 200;
              return Container(
                  padding: const EdgeInsets.all(8),
                  child: Column(children: [
                    Container(
                      child: Text(_getPowerText(rwState)),
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
                          await state.setPower(true);
                        },
                        child: const Text("On"),
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
                          await state.setPower(false);
                        },
                        child: const Text("Off"),
                      )),
                    ]),
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
