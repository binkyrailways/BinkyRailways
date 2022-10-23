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

class SplitView extends StatelessWidget {
  const SplitView({
    Key? key,
    // menu and content are now configurable
    this.menu,
    required this.content,
    this.endMenu,
    // these values are now configurable with sensible default values
    this.breakpoint = 600,
    this.menuWidth = 240,
    this.endMenuWidth = 240,
  }) : super(key: key);
  final Widget? menu;
  final Widget? endMenu;
  final Widget content;
  final double breakpoint;
  final double menuWidth;
  final double endMenuWidth;

  @override
  Widget build(BuildContext context) {
    final screenWidth = MediaQuery.of(context).size.width;
    if (screenWidth >= breakpoint) {
      // widescreen: optional menu on the left, content on the center, optional end menu on the right
      final List<Widget> start = [];
      final List<Widget> end = [];
      var remainingWidth = screenWidth;
      if (menu != null) {
        start.add(SizedBox(
          width: menuWidth,
          child: menu,
        ));
        start.add(Container(width: 0.5, color: Colors.black));
        remainingWidth = remainingWidth - (menuWidth + 0.5);
      }
      if (endMenu != null) {
        end.add(Container(width: 0.5, color: Colors.black));
        end.add(SizedBox(
          width: endMenuWidth,
          child: endMenu,
        ));
        remainingWidth = remainingWidth - (endMenuWidth + 0.5);
      }
      final List<Widget> children = [];
      children.addAll(start);
      children.add(SizedBox(
        width: remainingWidth,
        child: content,
      ));
      children.addAll(end);
      return Row(children: children);
    } else {
      // narrow screen: show content, menu inside drawer
      return Scaffold(
        body: content,
        drawer: menu == null
            ? null
            : SizedBox(
                width: menuWidth,
                child: Drawer(
                  child: menu,
                ),
              ),
        endDrawer: endMenu == null
            ? null
            : SizedBox(
                width: endMenuWidth,
                child: Drawer(
                  child: endMenu,
                ),
              ),
      );
    }
  }
}
