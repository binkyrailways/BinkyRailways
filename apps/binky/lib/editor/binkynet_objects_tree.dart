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

class BinkyNetObjectsTree extends StatelessWidget {
  const BinkyNetObjectsTree({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    final lwId = selector.idOf(EntityType.binkynetlocalworker) ?? "";
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        return FutureBuilder<List<BinkyNetObject>>(
            future: getBinkyNetObjects(model, lwId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Text("Loading...");
              }
              var objects = snapshot.data!
                ..sort((a, b) => a.objectId.compareTo(b.objectId));
              return ListView.builder(
                  itemCount: objects.length,
                  itemBuilder: (context, index) {
                    final id = objects[index].id;
                    return ListTile(
                      leading: BinkyIcons.binkynetobject,
                      title: Text(objects[index].objectId),
                      onTap: () =>
                          editorCtx.select(EntityType.binkynetobject, id),
                      selected: selector.idOf(EntityType.binkynetobject) == id,
                    );
                  });
            });
      },
    );
  }

  Future<List<BinkyNetObject>> getBinkyNetObjects(
      ModelModel model, String lwId) async {
    final lw = await model.getBinkyNetLocalWorker(lwId);
    return lw.objects.toList();
  }
}
