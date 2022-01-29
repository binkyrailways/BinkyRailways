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
import './binkynet_localworker_pane.dart';

class BinkyNetLocalWorkersPane extends StatelessWidget {
  const BinkyNetLocalWorkersPane({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<StateModel>(builder: (context, state, child) {
      final lws = _getLocalWorkers(state);
      if (lws.isEmpty) {
        return const Text("No local workers found");
      }
      lws.sort((a, b) => a.model.alias.compareTo(b.model.alias));
      final List<Widget> children = [];
      lws.forEach((e) {
        children.add(BinkyNetLocalWorkerPane(lwState: e));
      });
      return Container(
        padding: const EdgeInsets.all(8),
        child: Row(children: children),
      );
    });
  }

  List<BinkyNetLocalWorkerState> _getLocalWorkers(StateModel stateModel) {
    final css = stateModel.commandStations();
    final bnCss = css
        .where((cs) => cs.last.hasBinkynetCommandStation())
        .map((bnCs) => bnCs.last.binkynetCommandStation.localWorkers)
        .toList();
    if (bnCss.isEmpty) {
      return [];
    }
    return bnCss.reduce((list, x) {
      final List<BinkyNetLocalWorkerState> combined = [];
      combined.addAll(x);
      combined.addAll(list);
      return combined;
    });
  }
}
