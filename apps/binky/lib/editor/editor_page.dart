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

import 'package:binky/components/split_view.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'package:binky/models/model_model.dart';
import '../api/generated/br_model_types.pb.dart';

import './editor_context.dart';
import './blocks_tree.dart';
import './loc_settings.dart';
import './locs_tree.dart';
import './module_settings.dart';
import './module_tree.dart';
import './modules_tree.dart';
import './railway_settings.dart';
import './railway_tree.dart';

class EditorPage extends StatefulWidget {
  const EditorPage({Key? key}) : super(key: key);

  @override
  State<EditorPage> createState() => _EditorPageState();
}

class _EditorPageState extends State<EditorPage> {
  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider<EditorContext>(
        create: (context) => EditorContext(),
        child: Consumer<ModelModel>(builder: (context, model, child) {
          return FutureBuilder<Railway>(
              future: model.getRailway(),
              builder: (context, snapshot) {
                return Consumer<EditorContext>(
                    builder: (context, editorCtx, child) {
                  if (!snapshot.hasData) {
                    return Scaffold(
                      appBar: AppBar(
                        // Here we take the value from the MyHomePage object that was created by
                        // the App.build method, and use it to set our appbar title.
                        title: const Text("Binky Railways"),
                      ),
                      body: Center(
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: const <Widget>[
                            Text('Loading railway...'),
                            CircularProgressIndicator(value: null),
                          ],
                        ),
                      ),
                    );
                  }
                  var rw = snapshot.data!;
                  return Scaffold(
                    appBar: AppBar(
                      // Here we take the value from the MyHomePage object that was created by
                      // the App.build method, and use it to set our appbar title.
                      title: Text(model.title()),
                      leading: _buildLeading(context, editorCtx, model),
                      actions: _buildActions(context, editorCtx, model),
                    ),
                    body: _buildContent(context, editorCtx, model, rw),
                    floatingActionButton: FloatingActionButton(
                      onPressed: () => {},
                      tooltip: 'Increment',
                      child: const Icon(Icons.add),
                    ), // This trailing comma makes auto-formatting nicer for build methods.
                  );
                });
              });
        }));
  }

  Widget _buildContent(BuildContext context, EditorContext editorCtx,
      ModelModel model, Railway railway) {
    if ((editorCtx.selector.entityType == EntityType.unknown) &&
        model.isRailwayLoaded()) {
      editorCtx.select(EntitySelector.railway(EntityType.railway),
          notify: false);
    }
    switch (editorCtx.selector.entityType) {
      case EntityType.railway:
        return SplitView(
          menu: const RailwayTree(),
          content: RailwaySettings(model: model, railway: railway),
        );
      case EntityType.modules:
        return const SplitView(
          menu: RailwayTree(),
          content: ModulesTree(),
        );
      case EntityType.module:
        return const SplitView(
          menu: ModuleTree(),
          content: ModuleSettings(),
        );
      case EntityType.locs:
        return const SplitView(
          menu: RailwayTree(),
          content: LocsTree(),
        );
      case EntityType.loc:
        return const SplitView(
          menu: LocsTree(),
          content: LocSettings(),
        );
      case EntityType.blocks:
        return const SplitView(
          menu: BlocksTree(),
          content: Text("TODO"),
        );
      default:
        return const Center(child: Text("No selection"));
    }
  }

  Widget? _buildLeading(
      BuildContext context, EditorContext editorCtx, ModelModel model) {
    final selector = editorCtx.selector;
    final prev = selector.back();
    if (prev.entityType == selector.entityType) {
      // No reason for back button
      return null;
    }
    return IconButton(
      onPressed: () => editorCtx.select(prev),
      icon: const Icon(Icons.arrow_back),
      tooltip: 'Back',
    );
  }

  List<Widget>? _buildActions(
      BuildContext context, EditorContext editorCtx, ModelModel model) {
    switch (editorCtx.selector.entityType) {
      default:
        return [
          IconButton(
            icon: const Icon(Icons.save),
            onPressed: () {
              model.save();
            },
          ),
        ];
    }
  }
}
