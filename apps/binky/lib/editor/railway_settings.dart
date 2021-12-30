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
import 'package:protobuf/protobuf.dart';

import '../components.dart';
import '../models.dart';
import 'package:binky/api/generated/br_model_types.pb.dart';

class RailwaySettings extends StatefulWidget {
  final ModelModel model;
  final Railway railway;
  const RailwaySettings({Key? key, required this.model, required this.railway})
      : super(key: key);

  @override
  State<RailwaySettings> createState() => _RailwaySettingsState();
}

class _RailwaySettingsState extends State<RailwaySettings> {
  final TextEditingController _descriptionController = TextEditingController();

  @override
  void initState() {
    super.initState();
    _descriptionController.text = widget.railway.description;
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        SettingsTextField(
            controller: _descriptionController,
            label: "Description",
            firstChild: true,
            onLostFocus: (value) async {
              var rw = await widget.model.getRailway();
              var update = rw.deepCopy()..description = value;
              widget.model.updateRailway(update);
            }),
      ],
    );
  }
}
