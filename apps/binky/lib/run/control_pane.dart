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

import 'package:binky/run/run_context.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import './automatic_pane.dart';
import './power_pane.dart';

class ControlPane extends StatelessWidget {
  const ControlPane({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<RunContext>(builder: (context, runCtx, child) {
      return LayoutBuilder(builder: (context, constraints) {
        return Row(children: [
          SizedBox(width: constraints.maxWidth / 2, child: const PowerPane()),
          SizedBox(
              width: constraints.maxWidth / 2, child: const AutomaticPane()),
        ]);
      });
    });
  }
}
