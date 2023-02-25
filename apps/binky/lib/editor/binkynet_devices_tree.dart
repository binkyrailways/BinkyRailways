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
import 'package:binky/components.dart';
import 'package:binky/icons.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models.dart';
import '../api.dart';

class BinkyNetDevicesTree extends StatelessWidget {
  final bool withParents;
  const BinkyNetDevicesTree({Key? key, required this.withParents})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    final csId = selector.idOf(EntityType.commandstation) ?? "";
    final lwId = selector.idOf(EntityType.binkynetlocalworker) ?? "";
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        return FutureBuilder<BinkyNetLocalWorker>(
            future: getBinkyNetLocalWorker(model, lwId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Text("Loading...");
              }
              final lw = snapshot.data!;
              var devices = lw.devices.toList()
                ..sort((a, b) => a.deviceId.compareTo(b.deviceId));
              final extra = withParents ? 3 : 0;
              return ListView.builder(
                  itemCount: devices.length + extra,
                  itemBuilder: (context, index) {
                    if ((index == 0) && withParents) {
                      return ListTile(
                        leading: BinkyIcons.railway,
                        title: const Text("Railway"),
                        onTap: () => editorCtx.select(EntitySelector.railway()),
                      );
                    } else if ((index == 1) && withParents) {
                      return ListTile(
                        leading: BinkyIcons.commandstation,
                        title: const Text("Command station"),
                        onTap: () => editorCtx
                            .select(EntitySelector.commandStation(null, csId)),
                      );
                    } else if ((index == 2) && withParents) {
                      return ListTile(
                        leading: BinkyIcons.binkynetlocalworker,
                        title: const Text("Local worker"),
                        onTap: () => editorCtx
                            .select(EntitySelector.binkynetLocalWorker(lw)),
                      );
                    }
                    final device = devices[index - extra];
                    final id = device.id;
                    return ListTile(
                      leading: BinkyIcons.binkynetdevice,
                      title: Text(device.deviceId),
                      onTap: () => editorCtx
                          .select(EntitySelector.binkynetDevice(lw, device)),
                      selected: selector.idOf(EntityType.binkynetdevice) == id,
                      trailing: MorePopupMenu<String>(
                        items: [
                          PopupMenuItem<String>(
                              child: const Text('Remove'),
                              onTap: () async {
                                await model.deleteBinkyNetDevice(lw, device);
                                editorCtx
                                    .select(EntitySelector.binkynetDevices(lw));
                              }),
                        ],
                      ),
                    );
                  });
            });
      },
    );
  }

  Future<BinkyNetLocalWorker> getBinkyNetLocalWorker(
      ModelModel model, String lwId) async {
    return await model.getBinkyNetLocalWorker(lwId);
  }
}
