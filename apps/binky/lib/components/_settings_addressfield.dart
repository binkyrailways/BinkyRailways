// Copyright 2022 Ewout Prangsma
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

import 'package:binky/components.dart';
import 'package:flutter/material.dart';

class SettingsAddressField extends StatefulWidget {
  final String address;
  final String label;
  final SettingsTextFieldLostFocus onLostFocus;

  const SettingsAddressField({
    Key? key,
    required this.address,
    required this.label,
    required this.onLostFocus,
  }) : super(key: key);

  @override
  State<SettingsAddressField> createState() => _SettingsAddressFieldState();
}

class _SettingsAddressFieldState extends State<SettingsAddressField> {
  final TextEditingController _controller = TextEditingController();
  final AddressValidator _addressValidator = AddressValidator();

@override 
void initState() {
    super.initState();
    _controller.text = widget.address;
    _addressValidator.setState = () => setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return SettingsTextField(
      controller: _controller,
      label: widget.label,
      onLostFocus: widget.onLostFocus,
      validator: _addressValidator.validate,
      );
  }
}