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
import 'package:binky/components.dart';
import 'package:binky/icons.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models.dart';
import '../api.dart';

class LocsTree extends StatelessWidget {
  final bool withParents;
  const LocsTree({Key? key, required this.withParents}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        return FutureBuilder<List<Loc>>(
            future: getLocs(model),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Text("Loading...");
              }
              var locs = snapshot.data!
                ..sort((a, b) => a.description.compareTo(b.description));
              final extra = withParents ? 1 : 0;
              return ListView.builder(
                  itemCount: locs.length + extra,
                  itemBuilder: (context, index) {
                    if ((index == 0) && withParents) {
                      return ListTile(
                        leading: BinkyIcons.railway,
                        title: const Text("Railway"),
                        onTap: () => editorCtx.select(EntitySelector.railway()),
                      );
                    }
                    final loc = locs[index - extra];
                    final id = loc.id;
                    return ListTile(
                      leading: BinkyIcons.loc,
                      title: Text(loc.description),
                      subtitle: Text("${loc.address}, ${loc.speedSteps}s"),
                      onTap: () => editorCtx.select(EntitySelector.loc(loc)),
                      selected: selector.idOf(EntityType.loc) == id,
                      trailing: editorCtx.isRunningState
                          ? null
                          : MorePopupMenu<String>(
                              items: [
                                PopupMenuItem<String>(
                                    child: const Text('Remove'),
                                    onTap: () async {
                                      await model.deleteLoc(loc);
                                    }),
                              ],
                            ),
                    );
                  });
            });
      },
    );
  }

  Future<List<Loc>> getLocs(ModelModel model) async {
    var rw = await model.getRailway();
    return await Future.wait([
      for (var x in rw.locs) model.getLoc(x.id),
    ]);
  }
}
