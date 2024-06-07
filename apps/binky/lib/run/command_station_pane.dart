// Copyright 2024 Ewout Prangsma
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

import '../api.dart';

class CommandStationPane extends StatelessWidget {
  final CommandStationState commandStation;
  const CommandStationPane({Key? key, required this.commandStation})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    final color = commandStation.powerRequested
        ? (commandStation.powerActual ? Colors.green : Colors.purple)
        : (commandStation.powerActual ? Colors.orange : Colors.red);
    return Container(
      padding: const EdgeInsets.fromLTRB(8, 4, 0, 4),
      child: TextButton(
        style: ButtonStyle(
            backgroundColor: WidgetStateProperty.all(color),
            foregroundColor: WidgetStateProperty.all(Colors.black)),
        child: Text(commandStation.model.description),
        onPressed: () {},
      ),
    );
  }
}
