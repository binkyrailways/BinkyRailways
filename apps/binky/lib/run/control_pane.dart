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

import 'package:binky/run/run_context.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../components.dart';
import './power_pane.dart';
import './locs_tree.dart';
import './loc_pane.dart';

class ControlPane extends StatelessWidget {
  const ControlPane({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<RunContext>(builder: (context, runCtx, child) {
      final List<Widget> children = [
        const PowerPane(),
        const SettingsDivider(),
        const Text("Locs"),
        const Expanded(child: LocsTree()),
      ];
      final selectedLocId = runCtx.selectedLocId;
      if (selectedLocId != null) {
        children.add(const SettingsDivider());
        children.add(LocPane(id: selectedLocId));
      }
      return Column(children: children);
    });
  }
}
