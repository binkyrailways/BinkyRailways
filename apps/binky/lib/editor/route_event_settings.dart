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

  const RouteEventSettings({
    Key? key,
    required this.model,
    required this.routeId,
    required this.eventIndex,
    required this.update,
    required this.removeBehavior,
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
                PaginatedDataTable(
                  sortAscending: false,
                  rowsPerPage: 5,
                  columns: const [
                    DataColumn(label: Text("Locs")),
                    DataColumn(label: Text("State behavior")),
                    DataColumn(label: Text("Speed behavior")),
                    DataColumn(label: Text("")),
                  ],
                  source: _BehaviorsDataSource(
                    model: widget.model,
                    route: route,
                    event: event,
                    items: event.behaviors,
                    update: widget.update,
                    removeBehavior: widget.removeBehavior,
                  ),
                ),
              ],
            ),
          );
        });
  }
}

class _BehaviorsDataSource extends DataTableSource {
  final ModelModel model;
  final Route route;
  final RouteEvent event;
  final List<RouteEventBehavior> items;
  final Future<void> Function(void Function(RouteEvent)) update;
  final Future<void> Function(int index) removeBehavior;
  final PermissionValidator _permissionValidator = PermissionValidator();

  _BehaviorsDataSource({
    required this.model,
    required this.route,
    required this.event,
    required this.items,
    required this.update,
    required this.removeBehavior,
  });

  @override
  bool get isRowCountApproximate => false;

  @override
  int get rowCount => items.length;

  @override
  int get selectedRowCount => 0;

  @override
  DataRow getRow(int index) {
    final bhv = items[index];
    return DataRow(cells: [
      DataCell(TextFormField(
        initialValue: bhv.appliesTo,
        validator: _permissionValidator.validate,
        onChanged: (value) async {
          await update((update) {
            update.behaviors[index].appliesTo = value;
          });
        },
      )),
      DataCell(DropdownButton<RouteStateBehavior>(
        items: _routeStateBehaviorOptions(),
        value: bhv.stateBehavior,
        onChanged: (value) async {
          await update((update) {
            if (value != null) {
              update.behaviors[index].stateBehavior = value;
            }
          });
        },
      )),
      DataCell(DropdownButton<LocSpeedBehavior>(
        items: _locSpeedBehaviorOptions(),
        value: bhv.speedBehavior,
        onChanged: (value) async {
          await update((update) {
            if (value != null) {
              update.behaviors[index].speedBehavior = value;
            }
          });
        },
      )),
      DataCell(GestureDetector(
        child: const Icon(Icons.delete_outline),
        onTapDown: (details) async {
          await removeBehavior(index);
        },
      )),
    ]);
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
