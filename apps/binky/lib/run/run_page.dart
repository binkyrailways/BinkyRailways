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

import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../components.dart';
import '../models.dart';
import '../api.dart';
import './control_pane.dart';
import './run_context.dart';
import '../canvas/run/railway_canvas.dart';
import 'hardware_modules_pane.dart';

class RunPage extends StatefulWidget {
  const RunPage({Key? key}) : super(key: key);

  @override
  State<RunPage> createState() => _RunPageState();
}

class _RunPageState extends State<RunPage> {
  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider<RunContext>(
      create: (context) => RunContext(),
      child: Consumer<StateModel>(builder: (context, state, child) {
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
              var rwState = snapshot.data!;
              return Scaffold(
                appBar: AppBar(
                  // Here we take the value from the MyHomePage object that was created by
                  // the App.build method, and use it to set our appbar title.
                  title: Text(
                      "${rwState.model.description} [${rwState.isVirtualModeEnabled ? "virtual" : "live"}]"),
                  actions: _buildActions(context, state),
                ),
                body: SplitView(
                  menuWidth: 300,
                  menu: const ControlPane(),
                  content: Column(children: const [
                    Expanded(child: RailwayCanvas()),
                    HardwareModulesPane(),
                  ]),
                ),
              );
            });
      }),
    );
  }

  List<Widget>? _buildActions(BuildContext context, StateModel state) {
    final List<Widget> list = [];
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
