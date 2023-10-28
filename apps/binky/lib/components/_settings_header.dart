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
import 'package:expandable/expandable.dart';

class SettingsHeader extends StatelessWidget {
  final String title;
  final Widget? child;

  const SettingsHeader({Key? key, required this.title, this.child})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    final expanded = this.child;
    final header =
        Text(title, style: const TextStyle(fontWeight: FontWeight.bold));
    final Widget child = (expanded != null)
        ? ExpandablePanel(
            header: header,
            expanded: expanded,
            collapsed: Container(),
            theme: const ExpandableThemeData(
                headerAlignment: ExpandablePanelHeaderAlignment.center),
          )
        : header;
    return Container(
      padding: const EdgeInsets.fromLTRB(8, 8, 8, 0),
      child: child,
    );
  }
}
