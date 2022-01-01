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

import 'package:flutter/material.dart' hide Route;
import 'package:protobuf/protobuf.dart';
import 'package:provider/provider.dart';

import '../components.dart';
import '../models.dart';
import '../api.dart';
import 'package:binky/editor/editor_context.dart';

class RouteSettings extends StatelessWidget {
  const RouteSettings({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Consumer<EditorContext>(builder: (context, editorCtx, child) {
      final selector = editorCtx.selector;
      return Consumer<ModelModel>(builder: (context, model, child) {
        final routeId = selector.idOf(EntityType.route) ?? "";
        return FutureBuilder<Route>(
            future: model.getRoute(routeId),
            initialData: model.getCachedRoute(routeId),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                return const Center(child: CircularProgressIndicator());
              }
              var route = snapshot.data!;
              return _RouteSettings(
                  editorCtx: editorCtx, model: model, route: route);
            });
      });
    });
  }
}

class _RouteSettings extends StatefulWidget {
  final EditorContext editorCtx;
  final ModelModel model;
  final Route route;
  const _RouteSettings(
      {Key? key,
      required this.editorCtx,
      required this.model,
      required this.route})
      : super(key: key);

  @override
  State<_RouteSettings> createState() => _RouteSettingsState();
}

class _RouteSettingsState extends State<_RouteSettings> {
  final TextEditingController _descriptionController = TextEditingController();

  void _initConrollers() {
    _descriptionController.text = widget.route.description;
  }

  @override
  void initState() {
    super.initState();
    _initConrollers();
  }

  @override
  void didUpdateWidget(covariant _RouteSettings oldWidget) {
    _initConrollers();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        Text(widget.route.id),
        SettingsTextField(
            controller: _descriptionController,
            label: "Description",
            firstChild: true,
            onLostFocus: (value) async {
              final route = await widget.model.getRoute(widget.route.id);
              var update = route.deepCopy()..description = value;
              widget.model.updateRoute(update);
            }),
      ],
    );
  }
}
