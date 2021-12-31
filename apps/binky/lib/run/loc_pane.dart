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

class LocPane extends StatelessWidget {
  final String id;
  const LocPane({Key? key, required this.id}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<StateModel>(
      builder: (context, state, child) {
        return FutureBuilder<LocState>(
            future: state.getLocState(id),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Text("Loading...");
              }
              final locState = snapshot.data!;
              return Container(
                  padding: const EdgeInsets.all(8),
                  child: Column(children: [
                    Container(
                      child: Row(
                        children: [
                          Text(
                            locState.model.description,
                            textAlign: TextAlign.start,
                          ),
                          Expanded(
                              child: Text(
                            "[${locState.speedText}]",
                            textAlign: TextAlign.end,
                          )),
                        ],
                      ),
                      padding: const EdgeInsets.fromLTRB(8, 0, 8, 8),
                    ),
                    Slider(
                      min: 0,
                      max: locState.model.maximumSpeed.toDouble(),
                      value: locState.speedRequested.toDouble(),
                      label: locState.speedText,
                      onChanged: (value) async {
                        await state.setLocSpeedAndDirection(
                            id, value.toInt(), locState.directionRequested);
                      },
                    ),
                    Row(
                      children: [
                        IconButton(
                          icon: const Icon(Icons.keyboard_arrow_left),
                          tooltip: "Reverse",
                          color:
                              _directionColor(locState, LocDirection.REVERSE),
                          onPressed: () async {
                            await state.setLocSpeedAndDirection(id,
                                locState.speedRequested, LocDirection.REVERSE);
                          },
                        ),
                        Expanded(
                          child: TextButton(
                            style: ButtonStyle(
                                backgroundColor: MaterialStateProperty.all(
                                    Colors.orange[400]),
                                foregroundColor:
                                    MaterialStateProperty.all(Colors.black)),
                            onPressed: () async {
                              await state.setLocSpeedAndDirection(
                                  id, 0, locState.directionRequested);
                            },
                            child: const Text("Stop"),
                          ),
                        ),
                        IconButton(
                          icon: const Icon(Icons.keyboard_arrow_right),
                          tooltip: "Forward",
                          color:
                              _directionColor(locState, LocDirection.FORWARD),
                          onPressed: () async {
                            await state.setLocSpeedAndDirection(id,
                                locState.speedRequested, LocDirection.FORWARD);
                          },
                        ),
                      ],
                    ),
                  ]));
            });
      },
    );
  }

  Color? _directionColor(LocState locState, LocDirection expected) {
    final dirActual = locState.directionActual;
    final dirRequested = locState.directionRequested;
    final dirConsistent = (dirActual == dirRequested);
    if (dirActual == expected) {
      if (!dirConsistent) {
        return Colors.purple[200];
      }
      return Colors.green[400];
    }
    return Colors.grey;
  }
}
