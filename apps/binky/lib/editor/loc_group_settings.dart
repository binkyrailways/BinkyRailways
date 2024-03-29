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
import 'package:protobuf/protobuf.dart';
import 'package:provider/provider.dart';

import '../components.dart';
import '../models.dart';
import '../api.dart' hide Switch;
import 'package:binky/editor/editor_context.dart';

class LocGroupSettings extends StatelessWidget {
  const LocGroupSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    return Consumer<ModelModel>(builder: (context, model, child) {
      final lgId = selector.idOf(EntityType.locgroup) ?? "";
      return FutureBuilder<LocGroup>(
          future: model.getLocGroup(lgId),
          initialData: model.getCachedLocGroup(lgId),
          builder: (context, snapshot) {
            if (snapshot.hasError) {
              return ErrorMessage(
                  title: "Failed to load LocGroup", error: snapshot.error);
            } else if (!snapshot.hasData) {
              return const Center(child: CircularProgressIndicator());
            }
            var cs = snapshot.data!;
            return FutureBuilder<List<Loc>>(
              future: _getAllLocs(model),
              builder: (context, snapshot) {
                if (snapshot.hasError) {
                  return ErrorMessage(
                      title: "Failed to load all Locs", error: snapshot.error);
                } else if (!snapshot.hasData) {
                  return const Center(child: CircularProgressIndicator());
                }
                var allLocs = snapshot.data!;
                return _LocGroupSettings(
                    model: model, locGroup: cs, allLocs: allLocs);
              },
            );
          });
    });
  }

  Future<List<Loc>> _getAllLocs(ModelModel model) async {
    final rw = await model.getRailway();
    final locList = rw.locs.map((e) => model.getLoc(e.id));
    final result = await Future.wait(locList);
    result.sort((a, b) => a.description.compareTo(b.description));
    return result;
  }
}

class _LocGroupSettings extends StatefulWidget {
  final ModelModel model;
  final LocGroup locGroup;
  final List<Loc> allLocs;

  const _LocGroupSettings(
      {Key? key,
      required this.model,
      required this.locGroup,
      required this.allLocs})
      : super(key: key);

  @override
  State<_LocGroupSettings> createState() => _LocGroupSettingsState();
}

class _LocGroupSettingsState extends State<_LocGroupSettings> {
  final TextEditingController _descriptionController = TextEditingController();

  _initControllers() {
    _descriptionController.text = widget.locGroup.description;
  }

  @override
  void initState() {
    super.initState();
    _initControllers();
  }

  @override
  void didUpdateWidget(covariant _LocGroupSettings oldWidget) {
    _initControllers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        const SettingsHeader(title: "General"),
        SettingsTextField(
            controller: _descriptionController,
            label: "Description",
            firstChild: true,
            onLostFocus: (value) async {
              final cs = await widget.model.getLocGroup(widget.locGroup.id);
              var update = cs.deepCopy()..description = value;
              widget.model.updateLocGroup(update);
            }),
        const SettingsHeader(title: "Locs"),
        Expanded(
            child: ListView.builder(
          itemCount: widget.allLocs.length,
          itemBuilder: (context, index) {
            final loc = widget.allLocs[index];
            final isIncluded = widget.locGroup.locs.any((x) => x.id == loc.id);
            return ListTile(
              dense: true,
              minLeadingWidth: 30,
              leading: Switch(
                value: isIncluded,
                onChanged: (value) async {
                  final update = widget.locGroup.deepCopy();
                  if (value) {
                    update.locs.add(LocRef(id: loc.id));
                  } else {
                    update.locs.removeWhere((x) => x.id == loc.id);
                  }
                  await widget.model.updateLocGroup(update);
                },
              ),
              title: Text(loc.description),
              subtitle: Text(
                "${loc.owner} (${loc.address})",
              ),
            );
          },
        )),
      ],
    );
  }
}
