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

import 'package:binky/editor/loc_group_settings.dart';
import 'package:binky/editor/loc_groups_tree.dart';
import 'package:binky/editor/locs_tree.dart';
import 'package:binky/icons.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../editor/editor_context.dart';
import '../editor/loc_settings.dart';

class RunEditor extends StatelessWidget {
  const RunEditor({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final editorCtx = Provider.of<EditorContext>(context);
    final selector = editorCtx.selector;
    final theme = Theme.of(context);

    return Theme(
      data: theme.copyWith(
        colorScheme: theme.colorScheme.copyWith(primary: Colors.lightGreen),
      ),
      child: Column(
        children: [
          AppBar(
            title: const Text("Inline edit"),
            primary: false,
            actions: [
              IconButton(
                tooltip: "Edit locs",
                onPressed: () {
                  editorCtx.select(EntitySelector.locs());
                },
                icon: BinkyIcons.loc,
              ),
              IconButton(
                tooltip: "Edit loc groups",
                onPressed: () {
                  editorCtx.select(EntitySelector.locGroups());
                },
                icon: BinkyIcons.locGroup,
              ),
            ],
          ),
          Expanded(
            child: _buildContent(context, selector),
          ),
        ],
      ),
    );
  }

  Widget _buildContent(BuildContext context, EntitySelector selector) {
    switch (selector.entityType) {
      case EntityType.loc:
        return const LocSettings();
      case EntityType.locs:
        return const LocsTree(withParents: false);
      case EntityType.locgroup:
        return const LocGroupSettings();
      case EntityType.locgroups:
        return const LocGroupsTree(withParents: false);
      default:
        return const Text("editor with something");
    }
  }
}
