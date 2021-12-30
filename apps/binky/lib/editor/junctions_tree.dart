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
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models.dart';
import '../api.dart';

class JunctionsTree extends StatelessWidget {
  const JunctionsTree({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        final moduleId = selector.moduleId ?? "";
        return FutureBuilder<List<Junction>>(
            future: getJunctions(model, moduleId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Text("Loading...");
              }
              var junctions = snapshot.data!;
              return ListView.builder(
                  itemCount: junctions.length,
                  itemBuilder: (context, index) {
                    final id = junctions[index].id;
                    return ListTile(
                      title: Text(junctions[index].description),
                      onTap: () => editorCtx.select(EntitySelector.junction(
                          EntityType.junction, moduleId, id)),
                      selected: selector.junctionId == id,
                    );
                  });
            });
      },
    );
  }

  Future<List<Junction>> getJunctions(ModelModel model, String moduleId) async {
    var rw = await model.getModule(moduleId);
    return await Future.wait([
      for (var x in rw.junctions) model.getJunction(x.id),
    ]);
  }
}
