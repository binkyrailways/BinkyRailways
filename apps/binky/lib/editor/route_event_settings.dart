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

import 'package:binky/components.dart';
import 'package:flutter/material.dart' hide Route;

import '../models.dart';
import '../api.dart';

class RouteEventSettings extends StatefulWidget {
  final ModelModel model;
  final String routeId;
  final int eventIndex;
  final Future<void> Function(void Function(RouteEvent)) update;
  final Future<void> Function(int index) removeBehavior;
  final Future<void> Function(int index) moveBehaviorUp;
  final Future<void> Function(int index) moveBehaviorDown;

  const RouteEventSettings({
    Key? key,
    required this.model,
    required this.routeId,
    required this.eventIndex,
    required this.update,
    required this.removeBehavior,
    required this.moveBehaviorUp,
    required this.moveBehaviorDown,
  }) : super(key: key);

  @override
  State<RouteEventSettings> createState() => _RouteEventSettingsState();
}

class _RouteEventSettingsState extends State<RouteEventSettings> {
  @override
  Widget build(BuildContext context) {
    final screenWidth = MediaQuery.of(context).size.width;
    return FutureBuilder<Route>(
        future: widget.model.getRoute(widget.routeId),
        builder: (context, snapshot) {
          if (!snapshot.hasData) {
            return SizedBox(
              width: screenWidth * 0.6,
              child: const Text("Loading..."),
            );
          }
          final route = snapshot.data!;
          final event = route.events[widget.eventIndex];
          return SizedBox(
            width: screenWidth / 2,
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                ReorderableListView.builder(
                  shrinkWrap: true,
                  itemCount: event.behaviors.length,
                  onReorder: (int oldIndex, int newIndex) async {
                    if (newIndex > oldIndex) {
                      newIndex -= 1;
                    }
                    if (oldIndex == newIndex) return;
                    // Move behavior using multiple API calls since we only have moveUp/moveDown
                    int current = oldIndex;
                    while (current < newIndex) {
                      await widget.moveBehaviorDown(current);
                      current++;
                    }
                    while (current > newIndex) {
                      await widget.moveBehaviorUp(current);
                      current--;
                    }
                  },
                  itemBuilder: (context, index) {
                    final bhv = event.behaviors[index];
                    return ListTile(
                      key: Key("behavior-$index"),
                      title: Row(
                        children: [
                          Expanded(
                            child: TextFormField(
                              initialValue: bhv.appliesTo,
                              onChanged: (value) async {
                                await widget.update((update) {
                                  update.behaviors[index].appliesTo = value;
                                });
                              },
                              decoration: const InputDecoration(labelText: "Locs"),
                            ),
                          ),
                          const SizedBox(width: 8),
                          DropdownButton<RouteStateBehavior>(
                            items: _routeStateBehaviorOptions(),
                            value: bhv.stateBehavior,
                            onChanged: (value) async {
                              await widget.update((update) {
                                if (value != null) {
                                  update.behaviors[index].stateBehavior = value;
                                }
                              });
                            },
                          ),
                          const SizedBox(width: 8),
                          DropdownButton<LocSpeedBehavior>(
                            items: _locSpeedBehaviorOptions(),
                            value: bhv.speedBehavior,
                            onChanged: (value) async {
                              await widget.update((update) {
                                if (value != null) {
                                  update.behaviors[index].speedBehavior = value;
                                }
                              });
                            },
                          ),
                          IconButton(
                            icon: const Icon(Icons.delete_outline),
                            onPressed: () async {
                              await widget.removeBehavior(index);
                            },
                          ),
                        ],
                      ),
                    );
                  },
                ),
              ],
            ),
          );
        });
  }

  List<DropdownMenuItem<RouteStateBehavior>> _routeStateBehaviorOptions() {
    return RouteStateBehavior.values
        .map((x) => DropdownMenuItem<RouteStateBehavior>(
              child: Text(x.humanize()),
              value: x,
            ))
        .toList();
  }

  List<DropdownMenuItem<LocSpeedBehavior>> _locSpeedBehaviorOptions() {
    return LocSpeedBehavior.values
        .map((x) => DropdownMenuItem<LocSpeedBehavior>(
              child: Text(x.humanize()),
              value: x,
            ))
        .toList();
  }
}
