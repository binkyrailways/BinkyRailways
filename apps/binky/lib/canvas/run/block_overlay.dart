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

import 'package:binky/api.dart' hide Switch;
import 'package:flutter/material.dart';

import '../../models.dart';

class BlockOverlay extends StatefulWidget {
  final StateModel stateModel;
  final BlockState block;
  final void Function() onClose;

  const BlockOverlay(
      {Key? key,
      required this.stateModel,
      required this.block,
      required this.onClose})
      : super(key: key);

  @override
  State<BlockOverlay> createState() => _BlockOverlayState();
}

class _BlockOverlayState extends State<BlockOverlay> {
  String? _selectedLocId;

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<Holder<LocState>>>(
        future: getLocs(widget.stateModel),
        builder: (context, snapshot) {
          if (!snapshot.hasData) {
            return const Text("Loading...");
          }
          final allLocs = snapshot.data!;
          final locs = allLocs.where((l) => l.last.isEnabled).toList();
          locs.sort((a, b) =>
              a.last.model.description.compareTo(b.last.model.description));
          final List<Widget> children = [
            Text(widget.block.model.description,
                style: const TextStyle(fontWeight: FontWeight.bold)),
            const Divider(),
          ];
          if (locs.isNotEmpty) {
            children.add(
              Expanded(
                child: ListView.builder(
                  itemCount: locs.length,
                  itemBuilder: (context, index) {
                    final loc = locs[index].last;
                    return ListTile(
                      dense: true,
                      title: Text(loc.model.description),
                      subtitle: Text(loc.model.owner),
                      selectedTileColor: Colors.lightBlue,
                      selectedColor: Colors.orange,
                      selected: (loc.model.id == _selectedLocId),
                      onTap: () async {
                        setState(() {
                          _selectedLocId = loc.model.id;
                        });
                      },
                    );
                  },
                ),
              ),
            );
            children.add(
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: [
                  TextButton(
                    child: const Text("Assign facing back"),
                    onPressed: _selectedLocId != null
                        ? () async {
                            final locId = _selectedLocId;
                            if (locId != null) {
                              await widget.stateModel.assignLocToBlock(
                                  locId, widget.block.model.id, BlockSide.BACK);
                              widget.onClose();
                            }
                          }
                        : null,
                  ),
                  TextButton(
                    child: const Text("Assign facing front"),
                    onPressed: _selectedLocId != null
                        ? () async {
                            final locId = _selectedLocId;
                            if (locId != null) {
                              await widget.stateModel.assignLocToBlock(locId,
                                  widget.block.model.id, BlockSide.FRONT);
                              widget.onClose();
                            }
                          }
                        : null,
                  ),
                ],
              ),
            );
            children.add(const Divider());
          }
          children.add(
            TextButton(
              onPressed: () async {
                await widget.stateModel.setBlockClosed(
                    widget.block.model.id, !widget.block.closedActual);
                widget.onClose();
              },
              child: Text(
                  widget.block.closedActual ? "Re-open block" : "Close block"),
            ),
          );
          return Column(
            crossAxisAlignment: CrossAxisAlignment.center,
            children: children,
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
