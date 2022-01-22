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

class OutputsTree extends StatelessWidget {
  const OutputsTree({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        final moduleId = selector.idOf(EntityType.module) ?? "";
        return FutureBuilder<List<Output>>(
            future: getOutputs(model, moduleId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Text("Loading...");
              }
              var outputs = snapshot.data!;
              return ListView.builder(
                  itemCount: outputs.length + 2,
                  itemBuilder: (context, index) {
                    if (index == 0) {
                      return ListTile(
                        leading: BinkyIcons.railway,
                        title: const Text("Railway"),
                        onTap: () => editorCtx.select(EntitySelector.railway()),
                      );
                    } else if (index == 1) {
                      return ListTile(
                        leading: BinkyIcons.module,
                        title: const Text("Module"),
                        onTap: () => editorCtx
                            .select(EntitySelector.module(null, moduleId)),
                      );
                    }
                    final output = outputs[index - 2];
                    final id = output.id;
                    return ListTile(
                      leading: BinkyIcons.output,
                      title: Text(output.description),
                      onTap: () =>
                          editorCtx.select(EntitySelector.output(output)),
                      selected: selector.idOf(EntityType.output) == id,
                      trailing: GestureDetector(
                        child: const Icon(Icons.more_vert),
                        onTapDown: (TapDownDetails details) {
                          showMenu(
                            context: context,
                            useRootNavigator: true,
                            position: RelativeRect.fromLTRB(
                                details.globalPosition.dx,
                                details.globalPosition.dy,
                                details.globalPosition.dx,
                                details.globalPosition.dy),
                            items: [
                              PopupMenuItem<String>(
                                  child: const Text('Remove'),
                                  onTap: () async {
                                    await model.deleteOutput(output);
                                  }),
                            ],
                            elevation: 8.0,
                          );
                        },
                      ),
                    );
                  });
            });
      },
    );
  }

  Future<List<Output>> getOutputs(ModelModel model, String moduleId) async {
    var rw = await model.getModule(moduleId);
    return await Future.wait([
      for (var x in rw.outputs) model.getOutput(x.id),
    ]);
  }
}
