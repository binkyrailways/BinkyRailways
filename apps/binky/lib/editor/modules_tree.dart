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

import 'package:binky/models/model_model.dart';
import '../api/generated/br_model_types.pb.dart';
import '../components/error_message.dart';

class ModulesTree extends StatelessWidget {
  const ModulesTree({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
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
              var modules = snapshot.data!;
              return ListView.builder(
                  itemCount: modules.length,
                  itemBuilder: (context, index) {
                    return ListTile(
                      title: Text(modules[index].description),
                      onTap: () => editorCtx.select(EntitySelector.module(
                          EntityType.module, modules[index].id)),
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
