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

import 'package:binky/canvas/view_settings.dart';
import 'package:binky/run/run_editor.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:wakelock/wakelock.dart';

import '../components.dart';
import '../models.dart';
import '../api.dart';
import './locs_tree.dart';
import './control_pane.dart';
import './run_context.dart';
import '../canvas/run/railway_canvas.dart';
import 'hardware_modules_pane.dart';
import '../editor/editor_context.dart';
import './run_editor_context.dart';

class RunPage extends StatefulWidget {
  const RunPage({Key? key}) : super(key: key);

  @override
  State<RunPage> createState() => _RunPageState();
}

class _RunPageState extends State<RunPage> {
  final ViewSettings _viewSettings = ViewSettings();
  bool _isReloaded = false;

  @override
  void initState() {
    //print("Activate wakelock");
    Wakelock.enable();
    super.initState();
  }

  @override
  void deactivate() {
    //print("Deactivate wakelock");
    Wakelock.disable();
    super.deactivate();
  }

  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider<EditorContext>(
      create: (context) => RunEditorContext(),
      child: ChangeNotifierProvider<RunContext>(
        create: (context) => RunContext(),
        child: Consumer<StateModel>(builder: (context, state, child) {
          if (!_isReloaded) {
            setState(() {
              state.reset();
              _isReloaded = true;
            });
          }
          return Consumer<ModelModel>(builder: (context, model, child) {
            return FutureBuilder<RailwayState>(
                future: state.getRailwayState(),
                initialData: state.getCachedRailwayState(),
                builder: (context, snapshot) {
                  if (!snapshot.hasData) {
                    return Scaffold(
                      appBar: AppBar(
                        // Here we take the value from the MyHomePage object that was created by
                        // the App.build method, and use it to set our appbar title.
                        title: const Text("Binky Railways"),
                      ),
                      body: const Center(
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: <Widget>[
                            Text('Loading railway...'),
                            CircularProgressIndicator(value: null),
                          ],
                        ),
                      ),
                    );
                  }
                  var rwState = snapshot.data!;
                  final rwModel = model.getCachedRailway();
                  final rwDescription =
                      rwModel != null ? rwModel.description : "";
                  final editorCtx = Provider.of<EditorContext>(context);
                  final selector = editorCtx.selector;
                  final hasEditor = selector.entityType != EntityType.unknown;
                  return Scaffold(
                    appBar: AppBar(
                      // Here we take the value from the MyHomePage object that was created by
                      // the App.build method, and use it to set our appbar title.
                      title: Text(
                          "$rwDescription [${rwState.isVirtualModeEnabled ? "virtual" : "live"}]"),
                      actions: _buildActions(context, model, state, editorCtx),
                    ),
                    body: SplitView(
                      menuWidth: 300,
                      menu: const LocsTree(),
                      endMenu: hasEditor ? const RunEditor() : null,
                      endMenuWidth: hasEditor ? 300 : 0,
                      content: Column(children: [
                        const ControlPane(),
                        Expanded(
                            child: RailwayCanvas(
                          viewSettings: _viewSettings,
                        )),
                        const HardwareModulesPane(),
                      ]),
                    ),
                  );
                });
          });
        }),
      ),
    );
  }

  List<Widget>? _buildActions(BuildContext context, ModelModel model,
      StateModel state, EditorContext editorCtx) {
    final List<Widget> list = [];

    list.add(IconButton(
      icon: const Icon(Icons.save),
      onPressed: () async {
        try {
          await model.save();
        } catch (err) {
          showErrorDialog(
              context: context,
              title: "Failed to save changes",
              content: Text("$err"));
        }
      },
    ));
    if (editorCtx.selector.entityType == EntityType.unknown) {
      list.add(IconButton(
        icon: const Icon(Icons.edit_note),
        tooltip: "Show inline editor",
        onPressed: () {
          editorCtx.select(EntitySelector.locs());
        },
      ));
    } else {
      list.add(IconButton(
        icon: const Icon(Icons.edit_off),
        tooltip: "Hide inline editor",
        onPressed: () {
          editorCtx.select(EntitySelector.initial());
        },
      ));
    }

    list.add(const VerticalDivider());

    final rwState = state.getCachedRailwayState();
    if (rwState != null) {
      if (rwState.isVirtualModeEnabled) {
        if (!rwState.isVirtualAutorunEnabled) {
          list.add(IconButton(
            icon: const Icon(Icons.screen_share_outlined),
            tooltip: "Activate auto run",
            onPressed: () async {
              try {
                await state.enableRunMode(virtual: true, autoRun: true);
              } catch (err) {
                showErrorDialog(
                    context: context,
                    title: "Failed to enable autorun mode",
                    content: Text("$err"));
              }
            },
          ));
        } else {
          list.add(IconButton(
            icon: const Icon(Icons.stop_screen_share_outlined),
            tooltip: "Stop auto run",
            onPressed: () async {
              try {
                await state.enableRunMode(virtual: true, autoRun: false);
              } catch (err) {
                showErrorDialog(
                    context: context,
                    title: "Failed to disable autorun mode",
                    content: Text("$err"));
              }
            },
          ));
        }
      }
    }
    list.add(IconButton(
      icon: const Icon(Icons.stop_rounded),
      tooltip: "Edit",
      onPressed: () async {
        try {
          await state.disableRunMode();
        } catch (err) {
          showErrorDialog(
              context: context,
              title: "Failed to close run mode",
              content: Text("$err"));
        }
      },
    ));
    return list;
  }
}
