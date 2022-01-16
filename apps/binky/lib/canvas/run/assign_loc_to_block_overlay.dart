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

class AssignLocToBlockOverlay extends StatefulWidget {
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
  State<AssignLocToBlockOverlay> createState() =>
      _AssignLocToBlockOverlayState();
}

class _AssignLocToBlockOverlayState extends State<AssignLocToBlockOverlay> {
  BlockSide _side = BlockSide.FRONT;

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<Holder<LocState>>>(
        future: getLocs(widget.stateModel),
        builder: (context, snapshot) {
          if (!snapshot.hasData) {
            return const Text("Loading...");
          }
          final locs = snapshot.data!;
          return Column(
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Text("Assign to ${widget.block.model.description}"),
              ToggleButtons(
                renderBorder: false,
                borderRadius: const BorderRadius.all(Radius.circular(5)),
                selectedColor: Colors.green,
                color: Colors.grey,
                children: [
                  RichText(
                      text: const TextSpan(children: [
                    WidgetSpan(child: Icon(Icons.arrow_left)),
                    TextSpan(text: "Facing back"),
                  ])),
                  RichText(
                      text: const TextSpan(children: [
                    TextSpan(text: "Facing front"),
                    WidgetSpan(child: Icon(Icons.arrow_right)),
                  ])),
                ],
                isSelected: [
                  _side == BlockSide.BACK,
                  _side == BlockSide.FRONT,
                ],
                onPressed: (index) {
                  setState(() {
                    _side = (index == 0) ? BlockSide.BACK : BlockSide.FRONT;
                  });
                },
              ),
              Expanded(
                child: ListView.builder(
                  itemCount: locs.length,
                  itemBuilder: (context, index) {
                    final loc = locs[index].last;
                    return ListTile(
                      title: Text(loc.model.description),
                      onTap: () async {
                        await widget.stateModel.assignLocToBlock(
                            loc.model.id, widget.block.model.id, _side);
                        widget.onClose();
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
    final locs = await Future.wait([
      for (var x in rw.locs) model.getLocState(x.id),
    ]);
    return locs.where((x) => !x.last.canBeControlledAutomatically).toList();
  }
}
