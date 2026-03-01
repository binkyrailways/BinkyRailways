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

import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:url_launcher/url_launcher.dart';

import '../models.dart';
import '../api.dart';

class HardwareModulePane extends StatefulWidget {
  final HardwareModule hardwareModule;
  const HardwareModulePane({Key? key, required this.hardwareModule})
      : super(key: key);

  @override
  State<HardwareModulePane> createState() => _HardwareModulePaneState();
}

class _HardwareModulePaneState extends State<HardwareModulePane> {
  bool _isHovered = false;

  @override
  Widget build(BuildContext context) {
    final state = Provider.of<StateModel>(context);
    final allHws = state
        .commandStations()
        .map((bnCs) => bnCs.last.hardwareModules)
        .expand((x) => x)
        .toList();
    final childModules =
        allHws.where((hw) => hw.parentId == widget.hardwareModule.id).toList();
    childModules.sort((a, b) => a.id.compareTo(b.id));

    final self = _buildBox(context, widget.hardwareModule);

    if (childModules.isEmpty) {
      return Container(
        padding: const EdgeInsets.fromLTRB(8, 4, 0, 4),
        child: self,
      );
    }

    return MouseRegion(
      onEnter: (_) => setState(() => _isHovered = true),
      onExit: (_) => setState(() => _isHovered = false),
      child: Container(
        padding: const EdgeInsets.fromLTRB(8, 4, 0, 4),
        child: _isHovered
            ? Row(
                mainAxisSize: MainAxisSize.min,
                children: [
                  self,
                  ...childModules
                      .map((hw) => HardwareModulePane(hardwareModule: hw)),
                ],
              )
            : Stack(
                clipBehavior: Clip.none,
                children: [
                  ...childModules.asMap().entries.map((entry) {
                    final idx = entry.key;
                    final hw = entry.value;
                    return Positioned(
                      left: (idx + 1) * 6.0,
                      top: (idx + 1) * 6.0,
                      child: Opacity(
                        opacity: 0.5,
                        child: _buildBox(context, hw),
                      ),
                    );
                  }),
                  self,
                ],
              ),
      ),
    );
  }

  Widget _buildBox(BuildContext context, HardwareModule hardwareModule) {
    final secondsSinceLastUpdate = hardwareModule.secondsSinceLastUpdated;
    final hasErrors = hardwareModule.errorMessages.isNotEmpty;
    final color = hasErrors
        ? Colors.redAccent
        : (secondsSinceLastUpdate < 30)
            ? ((hardwareModule.uptime > 0) ? Colors.green : Colors.purple)
            : ((hardwareModule.uptime > 0) ? Colors.orange : Colors.purple);
    final popupItems = [
      PopupMenuItem<String>(
          child: const Text('Reset'),
          onTap: () async {
            final stateModel = Provider.of<StateModel>(context, listen: false);
            await stateModel.resetHardwareModule(hardwareModule.id);
          }),
      PopupMenuItem<String>(
          child: const Text('Discover hardware'),
          onTap: () async {
            final stateModel = Provider.of<StateModel>(context, listen: false);
            await stateModel.discoverHardware(hardwareModule.id);
          }),
    ];
    if (hardwareModule.metricsUrl.isNotEmpty) {
      popupItems.add(PopupMenuItem<String>(
          child: const Text('Show metrics'),
          onTap: () async {
            await launchUrl(Uri.parse(hardwareModule.metricsUrl),
                webOnlyWindowName: "_blank");
          }));
    }
    if (hardwareModule.dccGeneratorUrl.isNotEmpty) {
      popupItems.add(PopupMenuItem<String>(
          child: const Text('Show DCC generator'),
          onTap: () async {
            await launchUrl(Uri.parse(hardwareModule.dccGeneratorUrl),
                webOnlyWindowName: "_blank");
          }));
    }
    if (hardwareModule.sshUrl.isNotEmpty) {
      popupItems.add(PopupMenuItem<String>(
          child: const Text('Open SSH'),
          onTap: () async {
            await launchUrl(Uri.parse(hardwareModule.sshUrl),
                webOnlyWindowName: "_blank");
          }));
    }
    final tooltipErrors =
        hasErrors ? hardwareModule.errorMessages.join(".\n") : "No errors";
    return SizedBox(
      width: 150,
      height: 60,
      child: TextButton(
        style: ButtonStyle(
            backgroundColor: WidgetStateProperty.all(color),
            foregroundColor: WidgetStateProperty.all(Colors.black)),
        child: GestureDetector(
          child: Tooltip(
            message: "ip:${hardwareModule.address}\n\n$tooltipErrors",
            preferBelow: false,
            child: Text(
              "${hardwareModule.id}\nup:${hardwareModule.uptime}/$secondsSinceLastUpdate",
              textAlign: TextAlign.center,
              overflow: TextOverflow.ellipsis,
            ),
          ),
          onTapDown: (TapDownDetails details) {
            showMenu(
              context: context,
              useRootNavigator: true,
              position: RelativeRect.fromLTRB(
                  details.globalPosition.dx,
                  details.globalPosition.dy,
                  details.globalPosition.dx,
                  details.globalPosition.dy),
              items: popupItems,
              elevation: 8.0,
            );
          },
        ),
        onPressed: () {},
      ),
    );
  }
}
