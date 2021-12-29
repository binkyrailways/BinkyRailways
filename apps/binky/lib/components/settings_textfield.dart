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

typedef SettingsTextFieldLostFocus = void Function(String value);

class SettingsTextField extends StatelessWidget {
  final TextEditingController controller;
  final String label;
  final SettingsTextFieldLostFocus onLostFocus;
  final bool firstChild;

  const SettingsTextField(
      {Key? key,
      required this.controller,
      required this.label,
      required this.onLostFocus,
      this.firstChild = false})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: firstChild
          ? const EdgeInsets.all(8)
          : const EdgeInsets.fromLTRB(8, 0, 8, 8),
      child: Focus(
        child: TextField(
          controller: controller,
          decoration: InputDecoration(
            border: const UnderlineInputBorder(),
            label: Text(label),
          ),
        ),
        onFocusChange: (bool hasFocus) async {
          if (!hasFocus) {
            onLostFocus(controller.text);
          }
        },
      ),
    );
  }
}
