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

import 'dart:ffi';

import 'package:binky/editor/editor_context.dart';
import 'package:binky/icons.dart';
import 'package:flutter/material.dart' hide Route;
import 'package:provider/provider.dart';

import '../models.dart';
import '../api.dart';

class RoutesTree extends StatefulWidget {
  const RoutesTree({Key? key}) : super(key: key);
  @override
  State<RoutesTree> createState() => _RoutesTreeState();
}

class _RoutesTreeState extends State<RoutesTree> {
  String? _selectedBlockId;

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        final moduleId = selector.idOf(EntityType.module) ?? "";
        return FutureBuilder<List<Block>>(
            future: getBlocks(model, moduleId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Text("Loading...");
              }
              final blocks = snapshot.data!;
              final blockFilterItems = blocks
                  .map((x) => DropdownMenuItem<String>(
                        child: Text(x.description),
                        value: x.id,
                      ))
                  .toList();
              blockFilterItems.insert(
                  0,
                  const DropdownMenuItem<String>(
                      child: Text("All blocks"), value: ""));
              return FutureBuilder<List<Route>>(
                  future: getRoutes(model, moduleId),
                  builder: (context, snapshot) {
                    if (!snapshot.hasData) {
                      return const Text("Loading...");
                    }
                    var routes = snapshot.data!;
                    return Column(
                      children: [
                        Expanded(
                          child: ListView.builder(
                              itemCount: routes.length + 2,
                              itemBuilder: (context, index) {
                                if (index == 0) {
                                  return ListTile(
                                    leading: BinkyIcons.railway,
                                    title: const Text("Railway"),
                                    onTap: () => editorCtx
                                        .select(EntitySelector.railway()),
                                  );
                                } else if (index == 1) {
                                  return ListTile(
                                    leading: BinkyIcons.module,
                                    title: const Text("Module"),
                                    onTap: () => editorCtx.select(
                                        EntitySelector.module(null, moduleId)),
                                  );
                                }
                                final route = routes[index - 2];
                                final id = route.id;
                                return ListTile(
                                  leading: BinkyIcons.route,
                                  title: Text(route.description),
                                  onTap: () => editorCtx
                                      .select(EntitySelector.route(route)),
                                  selected:
                                      selector.idOf(EntityType.route) == id,
                                );
                              }),
                        ),
                        DropdownButton<String>(
                          items: blockFilterItems,
                          isExpanded: true,
                          onChanged: (key) {
                            setState(() {
                              _selectedBlockId = key;
                            });
                          },
                          value: _selectedBlockId,
                        ),
                      ],
                    );
                  });
            });
      },
    );
  }

  Future<List<Block>> getBlocks(ModelModel model, String moduleId) async {
    var rw = await model.getModule(moduleId);
    return await Future.wait([
      for (var x in rw.blocks) model.getBlock(x.id),
    ]);
  }

  Future<List<Route>> getRoutes(ModelModel model, String moduleId) async {
    var rw = await model.getModule(moduleId);
    final allRoutes = await Future.wait([
      for (var x in rw.routes) model.getRoute(x.id),
    ]);
    final blockId = _selectedBlockId;
    if (blockId != null && blockId.isNotEmpty) {
      return allRoutes
          .where(
              (r) => (r.to.block.id == blockId) || (r.from.block.id == blockId))
          .toList();
    }
    return allRoutes;
  }
}
