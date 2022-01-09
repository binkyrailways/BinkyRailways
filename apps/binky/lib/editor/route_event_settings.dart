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

import 'package:flutter/material.dart' hide Route;

import '../components.dart';
import '../models.dart';
import '../api.dart';

class RouteEventSettings extends StatelessWidget {
  final ModelModel model;
  final Route route;
  final RouteEvent event;
  const RouteEventSettings(
      {Key? key, required this.model, required this.route, required this.event})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    final behaviors = event.behaviors;
    final List<Widget> children = [];
    for (var bhv in behaviors) {
      children.add(ListTile(
        title: Text("${bhv.stateBehavior}"),
      ));
    }
    return Column(
      children: children,
    );
  }
}
