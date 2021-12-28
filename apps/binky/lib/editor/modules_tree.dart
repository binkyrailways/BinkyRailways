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

class ModulesTree extends StatelessWidget {
  final ContextSetter _contextSetter;
  final Railway _railway;
  const ModulesTree(
      {Key? key,
      required ContextSetter contextSetter,
      required Railway railway})
      : _contextSetter = contextSetter,
        _railway = railway,
        super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        var modules = _railway.modules;
        return ListView.builder(
            itemCount: modules.length,
            itemBuilder: (context, index) {
              return FutureBuilder<Module>(
                  future: model.getModule(modules[index].id),
                  initialData: model.getCachedModule(modules[index].id),
                  builder: (context, snapshot) {
                    if (snapshot.hasData) {
                      return ListTile(
                        title: Text(snapshot.data?.description ?? "?"),
                        onTap: () => _contextSetter(EditorContext.module(
                            EntityType.module, modules[index].id)),
                      );
                    } else if (snapshot.hasError) {
                      return ListTile(
                        leading: const Icon(Icons.error),
                        title: Text("Error: ${snapshot.error}"),
                      );
                    } else {
                      return const ListTile(
                        title: Text("Loading ..."),
                      );
                    }
                  });
            });
      },
    );
  }
}