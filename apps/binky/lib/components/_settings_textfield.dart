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

class SettingsDivider extends StatelessWidget {
  const SettingsDivider({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return const Divider(thickness: 3);
  }
}

class SettingsTextField extends StatelessWidget {
  final TextEditingController controller;
  final String label;
  final SettingsTextFieldLostFocus onLostFocus;
  final bool firstChild;
  final TextInputType keyboardType;
  final bool autocorrect;
  final String? Function(String?)? validator;
  final Widget? prefix;
  final Widget? suffix;
  final String? helperText;
  final bool disabled;

  const SettingsTextField({
    Key? key,
    required this.controller,
    required this.label,
    required this.onLostFocus,
    this.firstChild = false,
    this.keyboardType = TextInputType.text,
    this.autocorrect = true,
    this.validator,
    this.prefix,
    this.suffix,
    this.helperText,
    this.disabled = false,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: firstChild
          ? const EdgeInsets.all(8)
          : const EdgeInsets.fromLTRB(8, 0, 8, 8),
      child: Focus(
        child: TextFormField(
          controller: controller,
          keyboardType: keyboardType,
          autocorrect: autocorrect,
          validator: validator,
          autovalidateMode: AutovalidateMode.always,
          readOnly: disabled,
          decoration: InputDecoration(
            border: const UnderlineInputBorder(),
            label: Text(label),
            prefix: prefix,
            suffix: suffix,
            helperText: helperText,
            filled: disabled,
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
