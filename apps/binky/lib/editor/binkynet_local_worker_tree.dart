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
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models.dart';
import '../api.dart';
import '../icons.dart';

class BinkyNetLocalWorkerTree extends StatelessWidget {
  const BinkyNetLocalWorkerTree({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(
      builder: (context, model, child) {
        final csId = selector.idOf(EntityType.commandstation) ?? "";
        final lwId = selector.idOf(EntityType.binkynetlocalworker) ?? "";
        return FutureBuilder<BinkyNetLocalWorker>(
            future: model.getBinkyNetLocalWorker(lwId),
            initialData: model.getCachedBinkyNetLocalWorker(lwId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var lw = snapshot.data!;
              final List<Widget> children = [
                ListTile(
                  leading: BinkyIcons.railway,
                  minLeadingWidth: 20,
                  title: const Text("Railway"),
                  onTap: () => editorCtx.select(EntitySelector.railway()),
                ),
                ListTile(
                  leading: BinkyIcons.commandstation,
                  minLeadingWidth: 20,
                  title: const Text("Command station"),
                  onTap: () => editorCtx
                      .select(EntitySelector.commandStation(null, csId)),
                ),
                ListTile(
                  leading: BinkyIcons.binkynetlocalworker,
                  minLeadingWidth: 20,
                  title: const Text(
                    "Local worker",
                    overflow: TextOverflow.ellipsis,
                  ),
                  selected:
                      selector.entityType == EntityType.binkynetlocalworker,
                  onTap: () =>
                      editorCtx.select(EntitySelector.binkynetLocalWorker(lw)),
                ),
                ListTile(
                  leading: BinkyIcons.binkynetrouter,
                  minLeadingWidth: 20,
                  title: Text(
                    "Routers (${lw.routers.length})",
                    overflow: TextOverflow.ellipsis,
                  ),
                  selected: selector.entityType == EntityType.binkynetrouters,
                  onTap: () =>
                      editorCtx.select(EntitySelector.binkynetRouters(lw)),
                ),
                ListTile(
                  leading: BinkyIcons.binkynetdevice,
                  minLeadingWidth: 20,
                  title: Text(
                    "Devices (${lw.devices.length})",
                    overflow: TextOverflow.ellipsis,
                  ),
                  selected: selector.entityType == EntityType.binkynetdevices,
                  onTap: () =>
                      editorCtx.select(EntitySelector.binkynetDevices(lw)),
                ),
                ListTile(
                  leading: BinkyIcons.binkynetobject,
                  minLeadingWidth: 20,
                  title: Text(
                    "Objects (${lw.objects.length})",
                    overflow: TextOverflow.ellipsis,
                  ),
                  selected: selector.entityType == EntityType.binkynetobjects,
                  onTap: () =>
                      editorCtx.select(EntitySelector.binkynetObjects(lw)),
                ),
              ];
              return ListView(
                children: children,
              );
            });
      },
    );
  }
}
