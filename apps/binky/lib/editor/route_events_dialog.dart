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

import 'package:flutter/material.dart' hide Route;

import '../models.dart';
import '../api.dart';
import './route_event_settings.dart';

class RouteEventsDialog extends StatefulWidget {
  final ModelModel model;
  final String routeId;

  const RouteEventsDialog({
    Key? key,
    required this.model,
    required this.routeId,
  }) : super(key: key);

  @override
  State<RouteEventsDialog> createState() => _RouteEventsDialogState();
}

class _RouteEventsDialogState extends State<RouteEventsDialog> {
  String? _selectedLocId;

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<Route>(
      future: widget.model.getRoute(widget.routeId),
      builder: (context, snapshot) {
        if (!snapshot.hasData) {
          return const SimpleDialog(
            title: Text("Edit behaviors"),
            children: [Center(child: CircularProgressIndicator())],
          );
        }
        final route = snapshot.data!;
        final eventsWithSensors = route.events.asMap().entries.toList();

        return FutureBuilder<List<Sensor>>(
            future: Future.wait(eventsWithSensors
                .map((entry) => widget.model.getSensor(entry.value.sensor.id))),
            builder: (context, sensorsSnapshot) {
              if (!sensorsSnapshot.hasData) {
                return const SimpleDialog(
                  title: Text("Edit behaviors"),
                  children: [Center(child: CircularProgressIndicator())],
                );
              }
              final sensors = sensorsSnapshot.data!;
              final indexedEvents = eventsWithSensors.asMap().entries.map((e) {
                return _IndexedRouteEvent(
                    e.value.key, e.value.value, sensors[e.key]);
              }).toList();

              return SimpleDialog(
                title: Text("Edit behaviors for ${route.description}"),
                children: [
                  Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 16),
                    child: FutureBuilder<Railway>(
                        future: widget.model.getRailway(),
                        builder: (context, rwSnapshot) {
                          if (!rwSnapshot.hasData) {
                            return const Text("Loading locs...");
                          }
                          final locs = rwSnapshot.data!.locs;
                          return FutureBuilder<List<Loc>>(
                              future: Future.wait(locs.map(
                                  (l) => widget.model.getLoc(l.id))),
                              builder: (context, locsSnapshot) {
                                if (!locsSnapshot.hasData) {
                                  return const Text("Loading locs...");
                                }
                                final locsList = locsSnapshot.data!;
                                locsList.sort((a, b) =>
                                    a.description.compareTo(b.description));
                                return DropdownButton<String>(
                                  hint: const Text("Select a loc to check"),
                                  value: _selectedLocId,
                                  isExpanded: true,
                                  onChanged: (value) {
                                    setState(() {
                                      _selectedLocId = value;
                                    });
                                  },
                                  items: [
                                    const DropdownMenuItem<String>(
                                      value: null,
                                      child: Text("None"),
                                    ),
                                    ...locsList.map((l) =>
                                        DropdownMenuItem<String>(
                                          value: l.id,
                                          child: Text(l.description),
                                        )),
                                  ],
                                );
                              });
                        }),
                  ),
                  SizedBox(
                    width: MediaQuery.of(context).size.width * 0.8,
                    child: Column(
                      children: indexedEvents.map((ie) {
                        final index = ie.index;
                        final evt = ie.event;
                        final description = ie.sensor.description;
                        final behaviorCount = evt.behaviors.length;
                        return ExpansionTile(
                          title: Row(children: [
                            Text("$description ($behaviorCount)"),
                            const Spacer(),
                            if (index > 0)
                              IconButton(
                                icon: const Icon(Icons.arrow_upward),
                                onPressed: () async {
                                  await widget.model.moveRouteEventUp(
                                      widget.routeId, evt.sensor.id);
                                  setState(() {});
                                },
                              ),
                            if (index < route.events.length - 1)
                              IconButton(
                                icon: const Icon(Icons.arrow_downward),
                                onPressed: () async {
                                  await widget.model.moveRouteEventDown(
                                      widget.routeId, evt.sensor.id);
                                  setState(() {});
                                },
                              ),
                          ]),
                          initiallyExpanded: behaviorCount > 0,
                          children: [
                            RouteEventSettings(
                              model: widget.model,
                              routeId: widget.routeId,
                              eventIndex: index,
                              update: (editor) async {
                                final current =
                                    await widget.model.getRoute(widget.routeId);
                                var update = current.clone();
                                editor(update.events[index]);
                                await widget.model.updateRoute(update);
                                setState(() {});
                              },
                              removeBehavior: (bIdx) async {
                                await widget.model.removeRouteEventBehavior(
                                    widget.routeId, evt.sensor.id, bIdx);
                                setState(() {});
                              },
                              moveBehaviorUp: (bIdx) async {
                                await widget.model.moveRouteEventBehaviorUp(
                                    widget.routeId, evt.sensor.id, bIdx);
                                setState(() {});
                              },
                              moveBehaviorDown: (bIdx) async {
                                await widget.model.moveRouteEventBehaviorDown(
                                    widget.routeId, evt.sensor.id, bIdx);
                                setState(() {});
                              },
                              selectedLocId: _selectedLocId,
                            ),
                            Padding(
                              padding:
                                  const EdgeInsets.symmetric(horizontal: 16),
                              child: Row(children: [
                                TextButton(
                                  child: const Text("Add..."),
                                  onPressed: () async {
                                    await widget.model.addRouteEventBehavior(
                                        widget.routeId, evt.sensor.id);
                                    setState(() {});
                                  },
                                ),
                                TextButton(
                                  child: const Text("Add Enter"),
                                  onPressed: () async {
                                    final r = await widget.model
                                        .addRouteEventBehavior(
                                            widget.routeId, evt.sensor.id);
                                    final updatedEvt = r.events.firstWhere(
                                        (x) => x.sensor.id == evt.sensor.id);
                                    updatedEvt.behaviors.last.stateBehavior =
                                        RouteStateBehavior.RSB_ENTER;
                                    await widget.model.updateRoute(r);
                                    setState(() {});
                                  },
                                ),
                                TextButton(
                                  child: const Text("Add Reached"),
                                  onPressed: () async {
                                    final r = await widget.model
                                        .addRouteEventBehavior(
                                            widget.routeId, evt.sensor.id);
                                    final updatedEvt = r.events.firstWhere(
                                        (x) => x.sensor.id == evt.sensor.id);
                                    updatedEvt.behaviors.last.stateBehavior =
                                        RouteStateBehavior.RSB_REACHED;
                                    await widget.model.updateRoute(r);
                                    setState(() {});
                                  },
                                ),
                              ]),
                            ),
                          ],
                        );
                      }).toList(),
                    ),
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.end,
                    children: [
                      TextButton(
                        child: const Text("Close"),
                        onPressed: () => Navigator.of(context).pop(),
                      ),
                    ],
                  ),
                ],
              );
            });
      },
    );
  }
}

class _IndexedRouteEvent {
  final int index;
  final RouteEvent event;
  final Sensor sensor;
  _IndexedRouteEvent(this.index, this.event, this.sensor);
}
