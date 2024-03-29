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

class ModulesTree extends StatelessWidget {
  final bool withParents;
  const ModulesTree({Key? key, required this.withParents}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        return FutureBuilder<List<Module>>(
            future: getModules(model),
            builder: (context, snapshot) {
              if (snapshot.hasError) {
                return ErrorMessage(
                    title: "Failed to load module", error: snapshot.error);
              } else if (!snapshot.hasData) {
                return const Text("Loading module ...");
              }
              var modules = snapshot.data!
                ..sort((a, b) => a.description.compareTo(b.description));
              final extra = withParents ? 1 : 0;
              return ListView.builder(
                  itemCount: modules.length + extra,
                  itemBuilder: (context, index) {
                    if ((index == 0) && withParents) {
                      return ListTile(
                        leading: BinkyIcons.railway,
                        title: const Text("Railway"),
                        onTap: () => editorCtx.select(EntitySelector.railway()),
                      );
                    }
                    final module = modules[index - extra];
                    final id = module.id;
                    return ListTile(
                      leading: BinkyIcons.module,
                      title: Text(module.description),
                      onTap: () =>
                          editorCtx.select(EntitySelector.module(module, null)),
                      selected: selector.idOf(EntityType.module) == id,
                      trailing: MorePopupMenu<String>(
                        items: [
                          PopupMenuItem<String>(
                              child: const Text('Remove'),
                              onTap: () async {
                                await model.deleteModule(module);
                              }),
                        ],
                      ),
                    );
                  });
            });
      },
    );
  }

  Future<List<Module>> getModules(ModelModel model) async {
    var rw = await model.getRailway();
    return await Future.wait([
      for (var x in rw.modules) model.getModule(x.id),
    ]);
  }
}
