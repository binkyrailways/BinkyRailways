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
import 'package:binky/editor/railway_settings.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'package:binky/models/editor_model.dart';

import './editor_context.dart';
import './locs_tree.dart';
import './modules_tree.dart';
import './railway_tree.dart';

class EditorPage extends StatefulWidget {
  const EditorPage({Key? key}) : super(key: key);

  @override
  State<EditorPage> createState() => _EditorPageState();
}

class _EditorPageState extends State<EditorPage> {
  EditorContext _context = EditorContext.initial();

  void _setContext(EditorContext newContext) {
    setState(() {
      _context = newContext;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorModel>(builder: (context, editor, child) {
      if (!editor.isRailwayLoaded()) {
        return Scaffold(
          appBar: AppBar(
            // Here we take the value from the MyHomePage object that was created by
            // the App.build method, and use it to set our appbar title.
            title: const Text("Loading..."),
          ),
          body: Center(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: const <Widget>[
                Text('Loading railway...'),
              ],
            ),
          ),
        );
      }
      return Scaffold(
        appBar: AppBar(
          // Here we take the value from the MyHomePage object that was created by
          // the App.build method, and use it to set our appbar title.
          title: Text(editor.title()),
          leading: _buildLeading(context, editor),
          actions: _buildActions(context, editor),
        ),
        body: _buildContent(context, editor),
        floatingActionButton: FloatingActionButton(
          onPressed: () => {},
          tooltip: 'Increment',
          child: const Icon(Icons.add),
        ), // This trailing comma makes auto-formatting nicer for build methods.
      );
    });
  }

  Widget _buildContent(BuildContext context, EditorModel editor) {
    if ((_context.entityType == EntityType.unknown) &&
        editor.isRailwayLoaded()) {
      _context = EditorContext.railway(EntityType.railway);
    }
    switch (_context.entityType) {
      case EntityType.railway:
        return SplitView(
          menu: RailwayTree(context: _context, contextSetter: _setContext),
          content: const RailwaySettings(),
        );
      case EntityType.modules:
        return SplitView(
          menu: RailwayTree(context: _context, contextSetter: _setContext),
          content: ModulesTree(contextSetter: _setContext),
        );
      case EntityType.locs:
        return SplitView(
          menu: RailwayTree(context: _context, contextSetter: _setContext),
          content: LocsTree(contextSetter: _setContext),
        );
      default:
        return const Center(child: Text("No selection"));
    }
  }

  Widget? _buildLeading(BuildContext context, EditorModel editor) {
    switch (_context.entityType) {
      case EntityType.locgroups:
      case EntityType.commandstations:
        return IconButton(
          onPressed: () => _setContext(_context.back()),
          icon: const Icon(Icons.arrow_back),
          tooltip: 'Back to railway',
        );
      case EntityType.module:
        return IconButton(
          onPressed: () => _setContext(_context.back()),
          icon: const Icon(Icons.arrow_back),
          tooltip: 'Back to modules',
        );
      case EntityType.loc:
        return IconButton(
          onPressed: () => _setContext(_context.back()),
          icon: const Icon(Icons.arrow_back),
          tooltip: 'Back to locs',
        );
      case EntityType.locgroup:
        return IconButton(
          onPressed: () => _setContext(_context.back()),
          icon: const Icon(Icons.arrow_back),
          tooltip: 'Back to loc groups',
        );
      case EntityType.commandstation:
        return IconButton(
          onPressed: () => _setContext(_context.back()),
          icon: const Icon(Icons.arrow_back),
          tooltip: 'Back to command stations',
        );
      default:
        return null;
    }
  }

  List<Widget>? _buildActions(BuildContext context, EditorModel editor) {
    switch (_context.entityType) {
      default:
        return null;
    }
  }
}
