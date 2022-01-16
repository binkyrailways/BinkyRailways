// Copyright 2022 Ewout Prangsma
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

import 'package:binky/api.dart';
import 'package:flutter/material.dart';

import '../../models.dart';

class AssignLocToBlockOverlay extends StatelessWidget {
  final StateModel stateModel;
  final BlockState block;
  final void Function() onClose;

  const AssignLocToBlockOverlay(
      {Key? key,
      required this.stateModel,
      required this.block,
      required this.onClose})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<Holder<LocState>>>(
        future: getLocs(stateModel),
        builder: (context, snapshot) {
          if (!snapshot.hasData) {
            return const Text("Loading...");
          }
          final locs = snapshot.data!;
          return Column(
            children: [
              Text("Assign to ${block.model.description}"),
              Expanded(
                child: ListView.builder(
                  itemCount: locs.length,
                  itemBuilder: (context, index) {
                    final loc = locs[index].last;
                    return ListTile(
                      title: Text(loc.model.description),
                      onTap: () async {
                        await stateModel.assignLocToBlock(
                            loc.model.id, block.model.id, BlockSide.FRONT);
                        onClose();
                      },
                    );
                  },
                ),
              ),
            ],
          );
        });
  }

  Future<List<Holder<LocState>>> getLocs(StateModel model) async {
    var rw = await model.getRailwayState();
    return await Future.wait([
      for (var x in rw.locs) model.getLocState(x.id),
    ]);
  }
}
