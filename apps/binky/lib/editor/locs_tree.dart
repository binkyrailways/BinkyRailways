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

import 'package:binky/models/editor_model.dart';
import '../api/generated/br_types.pb.dart';

class LocsTree extends StatelessWidget {
  final ContextSetter _contextSetter;
  const LocsTree({Key? key, required ContextSetter contextSetter})
      : _contextSetter = contextSetter,
        super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorModel>(
      builder: (context, editor, child) {
        var locs = editor.railway().locs;
        return ListView.builder(
            itemCount: locs.length,
            itemBuilder: (context, index) {
              return FutureBuilder<Loc>(
                  future: editor.getLoc(locs[index].id),
                  builder: (context, snapshot) {
                    if (snapshot.hasData) {
                      return ListTile(
                        title: Text(snapshot.data?.description ?? "?"),
                        onTap: () => _contextSetter(EditorContext.module(
                            EntityType.loc, locs[index].id)),
                      );
                    } else if (snapshot.hasError) {
                      return ListTile(
                        leading: const Icon(Icons.error),
                        title: Text("Error: ${snapshot.error}"),
                      );
                    } else {
                      return const ListTile(
                        leading: CircularProgressIndicator(),
                      );
                    }
                  });
            });
      },
    );
  }
}
