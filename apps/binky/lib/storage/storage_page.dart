// Copyright 2023 Ewout Prangsma
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

import '../models.dart';
import '../api.dart';

class StoragePage extends StatefulWidget {
  const StoragePage({Key? key}) : super(key: key);

  @override
  State<StoragePage> createState() => _StoragePageState();
}

class _StoragePageState extends State<StoragePage> {
  final TextEditingController _textFieldController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Consumer<ModelModel>(builder: (context, model, child) {
      return Consumer<StorageModel>(builder: (context, storage, child) {
        return FutureBuilder<List<RailwayEntry>>(
            future: storage.getRailwayEntries(),
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
                        Text('Loading railways...'),
                        CircularProgressIndicator(value: null),
                      ],
                    ),
                  ),
                );
              }

              var entries = snapshot.data!;

              return Scaffold(
                appBar: AppBar(
                  // Here we take the value from the MyHomePage object that was created by
                  // the App.build method, and use it to set our appbar title.
                  title: const Text("Select a railway"),
                ),
                body: Center(
                  child: ListView.builder(
                      itemCount: entries.length,
                      itemBuilder: (context, index) {
                        return ListTile(
                          trailing: const Text(""),
                          title: Text(
                            entries[index].name,
                          ),
                          onTap: () async =>
                              {await model.loadRailway(entries[index])},
                        );
                      }),
                ),
                floatingActionButton: FloatingActionButton(
                  child: const Icon(Icons.add),
                  onPressed: () => _showAddEntryDialog(context, model),
                ),
              );
            });
      });
    });
  }

  Future<void> _showAddEntryDialog(
      BuildContext context, ModelModel model) async {
    _textFieldController.clear();
    return showDialog(
      context: context,
      builder: (context) {
        final defaultStyle = ButtonStyle(
            backgroundColor: MaterialStateProperty.all(Colors.green[200]),
            foregroundColor: MaterialStateProperty.all(Colors.black));
        final disabledStyle = ButtonStyle(
            backgroundColor: MaterialStateProperty.all(Colors.grey),
            foregroundColor: MaterialStateProperty.all(Colors.black));
        final hasName = _textFieldController.text.isNotEmpty;

        return AlertDialog(
          title: const Text('Name of the railway'),
          content: TextField(
            controller: _textFieldController,
            decoration: const InputDecoration(hintText: "Name"),
            autofocus: true,
          ),
          actions: <Widget>[
            ElevatedButton(
              child: const Text('Cancel'),
              onPressed: () {
                Navigator.pop(context);
              },
            ),
            ElevatedButton(
              child: const Text('Create'),
              style: hasName ? defaultStyle : disabledStyle,
              onPressed: () async {
                final name = _textFieldController.text;
                await model.createAndLoadRailway(name);
                Navigator.pop(context);
              },
            ),
          ],
        );
      },
    );
  }
}
