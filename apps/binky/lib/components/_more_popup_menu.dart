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

class MorePopupMenu<T> extends StatelessWidget {
  final List<PopupMenuItem<T>> items;

  const MorePopupMenu({Key? key, required this.items}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      child: const Icon(Icons.more_vert),
      onTapDown: (TapDownDetails details) {
        showMenu<T>(
          context: context,
          useRootNavigator: true,
          position: RelativeRect.fromLTRB(
              details.globalPosition.dx,
              details.globalPosition.dy,
              details.globalPosition.dx,
              details.globalPosition.dy),
          items: this.items,
          elevation: 8.0,
        );
      },
    );
  }
}
