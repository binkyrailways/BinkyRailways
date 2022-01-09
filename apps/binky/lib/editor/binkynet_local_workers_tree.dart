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

import 'package:binky/editor/editor_context.dart';
import 'package:binky/icons.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models.dart';
import '../api.dart';

class BinkyNetLocalWorkersTree extends StatelessWidget {
  const BinkyNetLocalWorkersTree({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    final csId =
        selector.parentId ?? selector.idOf(EntityType.commandstation) ?? "";
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        return FutureBuilder<List<BinkyNetLocalWorker>>(
            future: getBinkyNetLocalWorkers(model, csId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Text("Loading...");
              }
              var localworkers = snapshot.data!
                ..sort((a, b) => a.description.compareTo(b.description));
              return ListView.builder(
                  itemCount: localworkers.length,
                  itemBuilder: (context, index) {
                    final id = localworkers[index].id;
                    return ListTile(
                      leading: BinkyIcons.binkynetlocalworker,
                      title: Text(localworkers[index].description),
                      onTap: () => editorCtx.select(
                          EntitySelector.binkynetLocalWorker(
                              localworkers[index])),
                      selected:
                          selector.idOf(EntityType.binkynetlocalworker) == id,
                    );
                  });
            });
      },
    );
  }

  Future<List<BinkyNetLocalWorker>> getBinkyNetLocalWorkers(
      ModelModel model, String csId) async {
    final cs = await model.getCommandStation(csId);
    if (cs.hasBinkynetCommandStation()) {
      final bnCs = cs.binkynetCommandStation;
      return await Future.wait([
        for (var x in bnCs.localWorkers) model.getBinkyNetLocalWorker(x.id),
      ]);
    }
    return [];
  }
}
