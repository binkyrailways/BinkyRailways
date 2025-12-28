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

class SettingsFindingsField extends StatelessWidget {
  final List<String> value;

  const SettingsFindingsField({
    Key? key,
    required this.value,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final content = value.isEmpty
        ? Text("No findings, all good.", textAlign: TextAlign.left)
        : Text(value.join("\n"), textAlign: TextAlign.left);
    return Container(padding: const EdgeInsets.all(8), child: content);
  }
}
