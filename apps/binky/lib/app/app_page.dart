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

import '../api.dart';

import '../components.dart';
import '../models.dart';
import '../editor/editor_page.dart';
import '../run/run_page.dart';

class AppPage extends StatefulWidget {
  const AppPage({Key? key}) : super(key: key);

  @override
  State<AppPage> createState() => _AppPageState();
}

class _AppPageState extends State<AppPage> {
  @override
  Widget build(BuildContext context) {
    return Consumer<StateModel>(builder: (context, state, child) {
      return FutureBuilder<RailwayState>(
          future: state.getRailwayState(),
          builder: (context, snapshot) {
            if (!snapshot.hasData) {
              final List<Widget> children = snapshot.hasError
                  ? [
                      ErrorMessage(
                          title: "Failed to load railway state",
                          error: snapshot.error)
                    ]
                  : [
                      const Text('Loading railway state...'),
                      const CircularProgressIndicator(value: null)
                    ];
              return Scaffold(
                appBar: AppBar(
                  // Here we take the value from the MyHomePage object that was created by
                  // the App.build method, and use it to set our appbar title.
                  title: const Text("Binky Railways"),
                ),
                body: Center(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: children,
                  ),
                ),
              );
            }
            var rwState = snapshot.data!;
            if (rwState.isRunModeEnabled) {
              return const RunPage();
            } else {
              return const EditorPage();
            }
          });
    });
  }
}
