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
import 'package:binky/models/state_model.dart';
import '../api/generated/br_model_types.pb.dart';
import '../api/generated/br_state_types.pb.dart';

class RunPage extends StatefulWidget {
  const RunPage({Key? key}) : super(key: key);

  @override
  State<RunPage> createState() => _RunPageState();
}

class _RunPageState extends State<RunPage> {
  @override
  Widget build(BuildContext context) {
    return Consumer<StateModel>(builder: (context, state, child) {
      return FutureBuilder<RailwayState>(
          future: state.getRailwayState(),
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
                actions: _buildActions(context),
              ),
              body: Text("TODO"),
              floatingActionButton: FloatingActionButton(
                onPressed: () => {},
                tooltip: 'Increment',
                child: const Icon(Icons.add),
              ), // This trailing comma makes auto-formatting nicer for build methods.
            );
          });
    });
  }

  List<Widget>? _buildActions(BuildContext context) {
    return [
      IconButton(
        icon: const Icon(Icons.stop_rounded),
        tooltip: "Edit",
        onPressed: () async {
          final state = Provider.of<StateModel>(context, listen: false);
          await state.disableRunMode();
        },
      ),
    ];
  }
}
