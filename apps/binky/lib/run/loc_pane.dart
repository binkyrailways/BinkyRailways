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
import '../api.dart' hide Image;

class LocPane extends StatelessWidget {
  final String id;
  const LocPane({Key? key, required this.id}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<StateModel>(
      builder: (context, state, child) {
        return FutureBuilder<Holder<LocState>>(
            future: state.getLocState(id),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Text("Loading...");
              }
              final locState = snapshot.data!.last;
              final directionStyle = ButtonStyle(
                  backgroundColor: WidgetStateProperty.all(Colors.grey[400]),
                  foregroundColor: WidgetStateProperty.all(Colors.black));

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
                    Row(
                      children: [
                        Expanded(
                          child: Slider(
                            min: 0,
                            max: locState.model.maximumSpeed.toDouble(),
                            value: locState.speedRequested.toDouble(),
                            label: locState.speedText,
                            onChanged: (value) async {
                              await state.setLocSpeedAndDirection(id,
                                  value.toInt(), locState.directionRequested);
                            },
                          ),
                        ),
                        GestureDetector(
                          child: Icon(
                            Icons.light_mode,
                            color:
                                locState.f0Actual ? Colors.yellow : Colors.grey,
                          ),
                          onTap: () async {
                            await state.setLocFunctions(id, [
                              LocFunction(
                                  index: 0, value: !locState.f0Requested)
                            ]);
                          },
                        ),
                      ],
                    ),
                    Row(
                      children: [
                        ElevatedButton(
                          child: const Icon(Icons.keyboard_arrow_left),
                          style: directionStyle,
                          onPressed: () async {
                            await state.setLocSpeedAndDirection(id,
                                locState.speedRequested, LocDirection.REVERSE);
                          },
                        ),
                        Container(width: 4),
                        Expanded(
                          child: ElevatedButton(
                            style: ButtonStyle(
                                backgroundColor:
                                    WidgetStateProperty.all(Colors.orange[400]),
                                foregroundColor:
                                    WidgetStateProperty.all(Colors.black)),
                            onPressed: () async {
                              await state.setLocSpeedAndDirection(
                                  id, 0, locState.directionRequested);
                            },
                            child: const Text("Stop"),
                          ),
                        ),
                        Container(width: 4),
                        ElevatedButton(
                          child: const Icon(Icons.keyboard_arrow_right),
                          style: directionStyle,
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
}
