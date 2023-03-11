// Copyright 2021-2023 Ewout Prangsma
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

import 'package:binky/api.dart' hide Image;
import 'package:binky/editor/editor_context.dart';
import 'package:binky/run/run_context.dart';
import 'package:flame/extensions.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'dart:math' as math;

import '../models.dart';

class LocsTreeCard extends StatefulWidget {
  final RunContext runCtx;
  final StateModel state;
  final LocState loc;
  final bool isAssigned;
  final bool isSelected;

  const LocsTreeCard(
      {Key? key,
      required this.loc,
      required this.state,
      required this.isAssigned,
      required this.isSelected,
      required this.runCtx})
      : super(key: key);

  @override
  State<LocsTreeCard> createState() => _LocsTreeCardState();
}

class _LocsTreeCardState extends State<LocsTreeCard> {
  bool _hasMouse = false;

  @override
  Widget build(BuildContext context) {
    final loc = widget.loc;
    final stateText = loc.stateText;
    final canBeControlledAutomatically = loc.canBeControlledAutomatically;
    final autoControlled = loc.controlledAutomaticallyActual;
    final autoControlledConsistent = loc.controlledAutomaticallyActual ==
        loc.controlledAutomaticallyRequested;
    final actions =
        _buildActions(context, widget.state, loc, widget.isAssigned);
    final icon = canBeControlledAutomatically
        ? autoControlled
            ? Icons.auto_mode
            : Icons.sports_esports
        : Icons.room;
    final hasImage = loc.model.imageUrl.isNotEmpty;
    const statusSize = 32.0;

    return MouseRegion(
      onEnter: (x) {
        setState(() {
          _hasMouse = true;
        });
      },
      onExit: (x) {
        setState(() {
          _hasMouse = false;
        });
      },
      child: Card(
        color: widget.isSelected ? Colors.grey.shade300 : Colors.white,
        elevation: 4,
        child: GestureDetector(
          behavior: HitTestBehavior.opaque,
          child: Container(
            padding: const EdgeInsets.fromLTRB(6, 4, 6, 4),
            child: LayoutBuilder(
              builder: (context, constraints) => Column(children: [
                Row(children: [
                  Column(children: [
                    SizedBox(
                      width: constraints.maxWidth - statusSize,
                      child: Text(
                        "${loc.model.description} (${loc.model.owner})",
                        overflow: TextOverflow.ellipsis,
                        textAlign: TextAlign.start,
                        style: const TextStyle(fontWeight: FontWeight.bold),
                      ),
                    ),
                    SizedBox(
                      width: constraints.maxWidth - statusSize,
                      child: (stateText.isNotEmpty)
                          ? Text(
                              loc.stateText,
                              overflow: TextOverflow.ellipsis,
                              textAlign: TextAlign.start,
                            )
                          : Container(),
                    ),
                  ]),
                  SizedBox(
                    width: statusSize,
                    height: statusSize,
                    child: Stack(alignment: Alignment.center, children: [
                      _buildSpeedIndicator(),
                      Icon(
                        icon,
                        size: 24,
                        color: canBeControlledAutomatically &&
                                !autoControlledConsistent
                            ? Colors.orange.shade300
                            : Colors.blue.shade300,
                      ),
                    ]),
                  ),
                ]),
                Container(
                  width: constraints.maxWidth,
                  padding: const EdgeInsets.only(top: 4, bottom: 4),
                  child: hasImage
                      ? Image.network(
                          loc.model.imageUrl,
                        )
                      : const Icon(Icons.train),
                ),
                widget.isSelected || _hasMouse
                    ? Row(children: actions)
                    : Container(height: 0),
                widget.isSelected ? _buildControlBar() : Container(),
              ]),
            ),
          ),
          onTap: () {
            widget.runCtx.selectLoc(loc.model.id);
          },
        ),
      ),
    );
  }

  Widget _buildAction(
      {IconData? icon,
      String? tooltip,
      Function()? onPressed,
      Color? color,
      double width = 24,
      bool isSelected = false}) {
    return SizedBox(
      width: width,
      height: 24,
      child: IconButton(
        onPressed: onPressed,
        color: color,
        icon: isSelected
            ? Ink(
                decoration: ShapeDecoration(
                  color: Colors.green.shade200,
                  shape: const CircleBorder(),
                ),
                child: Icon(icon),
              )
            : Icon(icon),
        iconSize: 20,
        padding: const EdgeInsets.only(top: 4),
        tooltip: tooltip,
        splashRadius: 16,
      ),
    );
  }

  List<Widget> _buildActions(
      BuildContext context, StateModel state, LocState loc, bool isAssigned) {
    final canBeControlledAutomatically = loc.canBeControlledAutomatically;
    final List<Widget> actions = [];

    if (canBeControlledAutomatically) {
      if (loc.controlledAutomaticallyRequested) {
        actions.add(_buildAction(
            icon: Icons.sports_esports,
            tooltip: "Control manually",
            onPressed: () async {
              await state.setLocControlledAutomatically(loc.model.id, false);
            }));
      } else {
        actions.add(_buildAction(
            icon: Icons.auto_mode,
            tooltip: "Control automatically",
            onPressed: () async {
              await state.setLocControlledAutomatically(loc.model.id, true);
            }));
      }
    }

    actions.add(_buildAction(
        icon: Icons.light_mode,
        tooltip: 'Light',
        color: loc.f0Actual ? Colors.yellow : Colors.grey,
        onPressed: () async {
          await state.setLocFunctions(
              loc.model.id, [LocFunction(index: 0, value: !loc.f0Requested)]);
        }));

    final isStopped = (loc.speedActual == 0) && (loc.speedRequested == 0);
    if (!widget.isSelected && !isStopped) {
      actions.add(_buildAction(
          icon: Icons.stop,
          tooltip: 'Stop',
          width: 20,
          color: isStopped ? Colors.grey : Colors.orange,
          onPressed: () async {
            await state.setLocSpeedAndDirection(
                loc.model.id, 0, loc.directionRequested);
          }));
    }

    if (isAssigned) {
      //actions.add(Container(width: 16));
      actions.add(_buildAction(
          icon: Icons.cancel,
          tooltip: 'Take of track',
          color: Colors.red.shade200,
          onPressed: () async {
            await state.takeLocOfTrack(loc.model.id);
          }));
    }

    actions.add(Expanded(child: Container()));

    actions.add(_buildAction(
        icon: Icons.edit,
        tooltip: 'Edit',
        onPressed: () {
          final editorCtx = Provider.of<EditorContext>(context, listen: false);
          editorCtx.select(EntitySelector.loc(loc.model));
        }));

    return actions;
  }

  Row _buildControlBar() {
    final List<Widget> widgets = [];
    final state = widget.state;
    final loc = widget.loc;

    final isStopped = (loc.speedActual == 0) && (loc.speedRequested == 0);
    widgets.add(_buildAction(
        icon: Icons.stop,
        tooltip: 'Stop',
        width: 20,
        color: isStopped ? Colors.grey : Colors.orange,
        onPressed: () async {
          await state.setLocSpeedAndDirection(
              loc.model.id, 0, loc.directionRequested);
        }));

    widgets.add(Expanded(
      child: Slider(
        min: 0,
        max: loc.model.maximumSpeed.toDouble(),
        value: loc.speedRequested.toDouble(),
        label: loc.speedText,
        onChanged: (value) async {
          await widget.state.setLocSpeedAndDirection(
              loc.model.id, value.toInt(), loc.directionRequested);
        },
      ),
    ));

    final isForward = (loc.directionActual == LocDirection.FORWARD) &&
        (loc.directionRequested == LocDirection.FORWARD);
    final isReverse = (loc.directionActual == LocDirection.REVERSE) &&
        (loc.directionRequested == LocDirection.REVERSE);
    widgets.add(_buildAction(
        icon: Icons.chevron_left,
        tooltip: 'Reverse',
        width: 20,
        isSelected: isReverse,
        onPressed: () async {
          await state.setLocSpeedAndDirection(
              loc.model.id, loc.speedRequested, LocDirection.REVERSE);
        }));

    widgets.add(Container(width: 4));

    widgets.add(_buildAction(
        icon: Icons.chevron_right,
        tooltip: 'Forward',
        width: 20,
        isSelected: isForward,
        onPressed: () async {
          await state.setLocSpeedAndDirection(
              loc.model.id, loc.speedRequested, LocDirection.FORWARD);
        }));

    return Row(children: widgets);
  }

  Widget _buildSpeedIndicator() {
    final loc = widget.loc;
    final isReverse = (loc.directionRequested == LocDirection.REVERSE);

    final indicator = CircularProgressIndicator(
        backgroundColor: Colors.grey.shade200,
        color: _getSpeedColor(loc.speedActual / 100),
        value: loc.speedActual / 100);

    if (isReverse) {
      return Transform(
        alignment: Alignment.center,
        transform: Matrix4.rotationY(math.pi),
        child: indicator,
      );
    }
    return indicator;
  }

  Color _getSpeedColor(double speed) {
    return Colors.green
        .darken(1.0 - speed)
        .withAlpha(96 + (speed * 100).round());
  }
}
