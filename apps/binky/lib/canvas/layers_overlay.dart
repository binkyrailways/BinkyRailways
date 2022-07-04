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

import 'package:binky/canvas/view_settings.dart';
import 'package:flutter/material.dart';

import '../../models.dart';

class LayersOverlay extends StatefulWidget {
  final ModelModel modelModel;
  final ViewSettings viewSettings;
  final void Function() onClose;
  final Future<List<String>> Function() buildLayers;

  const LayersOverlay(
      {Key? key,
      required this.modelModel,
      required this.viewSettings,
      required this.onClose,
      required this.buildLayers})
      : super(key: key);

  @override
  State<LayersOverlay> createState() => _LayersOverlayState();
}

class _LayersOverlayState extends State<LayersOverlay> {
  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<String>>(
        future: widget.buildLayers(),
        builder: (context, snapshot) {
          if (!snapshot.hasData) {
            return const Text("Loading...");
          }
          final layers = snapshot.data!;
          final lv = ListView.builder(
            itemCount: layers.length,
            itemBuilder: (context, index) {
              final layer = layers[index];
              return ListTile(
                title: Text(layer),
                leading: Icon(widget.viewSettings.isLayerVisible(layer)
                    ? Icons.check_box_outlined
                    : Icons.check_box_outline_blank),
                minLeadingWidth: 16,
                onTap: () {
                  setState(() {
                    if (widget.viewSettings.isLayerVisible(layer)) {
                      widget.viewSettings.hideLayer(layer);
                    } else {
                      widget.viewSettings.showLayer(layer);
                    }
                  });
                },
              );
            },
          );
          return Column(
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Expanded(child: lv),
              Row(
                children: [
                  TextButton(
                      child: const Text("All"),
                      onPressed: () => _selectAll(layers)),
                  TextButton(
                      child: const Text("None"),
                      onPressed: () => _selectNone(layers)),
                  TextButton(
                      child: const Text("Close"),
                      onPressed: () => widget.onClose())
                ],
              ),
            ],
          );
        });
  }

  void _selectAll(List<String> layers) {
    setState(() {
      for (var l in layers) {
        widget.viewSettings.showLayer(l);
      }
    });
  }

  void _selectNone(List<String> layers) {
    setState(() {
      for (var l in layers) {
        widget.viewSettings.hideLayer(l);
      }
    });
  }
}
