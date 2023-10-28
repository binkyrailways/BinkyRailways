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

typedef SettingsDropDownFieldChanges<T> = void Function(T? value);

class SettingsDropdownField<T> extends StatelessWidget {
  final String label;
  final T? value;
  final SettingsDropDownFieldChanges<T> onChanged;
  final bool firstChild;
  final List<DropdownMenuItem<T>> items;

  const SettingsDropdownField({
    Key? key,
    required this.label,
    required this.value,
    required this.onChanged,
    this.firstChild = false,
    required this.items,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: firstChild
          ? const EdgeInsets.all(8)
          : const EdgeInsets.fromLTRB(8, 0, 8, 8),
      child: DropdownButtonFormField<T>(
        decoration: InputDecoration(
          labelText: label,
        ),
        onChanged: onChanged,
        items: items,
        value: value,
      ),
    );
  }
}
