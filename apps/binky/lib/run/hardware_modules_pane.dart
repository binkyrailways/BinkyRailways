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
import 'command_station_pane.dart';
import 'hardware_module_pane.dart';

class HardwareModulesPane extends StatelessWidget {
  const HardwareModulesPane({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<StateModel>(builder: (context, state, child) {
      final css = state.commandStations().toList();
      final hws = _getHardwareModules(state);
      if (css.isEmpty && hws.isEmpty) {
        return const Text("No command stations and hardware modules found");
      }
      css.sort((a, b) =>
          a.last.model.description.compareTo(b.last.model.description));
      hws.sort((a, b) => a.id.compareTo(b.id));
      final List<Widget> children = [];
      css.forEach((e) {
        children.add(CommandStationPane(commandStation: e.last));
      });
      children.add(const Divider());
      hws.forEach((e) {
        children.add(HardwareModulePane(hardwareModule: e));
      });
      return Container(
        padding: const EdgeInsets.all(8),
        child: Row(children: children),
      );
    });
  }

  List<HardwareModule> _getHardwareModules(StateModel stateModel) {
    final css = stateModel.commandStations();
    final hmCss = css.map((bnCs) => bnCs.last.hardwareModules).toList();
    if (hmCss.isEmpty) {
      return [];
    }
    return hmCss.reduce((list, x) {
      final List<HardwareModule> combined = [];
      combined.addAll(x);
      combined.addAll(list);
      return combined;
    });
  }
}
