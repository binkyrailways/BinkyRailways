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

import 'dart:async';

import 'package:flutter/material.dart';
import 'package:grpc/grpc.dart';
import 'package:provider/provider.dart';
import 'package:app_links/app_links.dart';

import '../api.dart';

import '../components.dart';
import '../models.dart';
import '../editor/editor_page.dart';
import '../run/run_page.dart';
import '../storage/storage_page.dart';

class AppPage extends StatefulWidget {
  const AppPage({Key? key}) : super(key: key);

  @override
  State<AppPage> createState() => _AppPageState();
}

class _AppPageState extends State<AppPage> {
  late AppLinks _appLinks;
  StreamSubscription<Uri>? _linkSubscription;
  String _title = "Testing";

  @override
  void initState() {
    super.initState();

    initDeepLinks();
  }

  @override
  void dispose() {
    _linkSubscription?.cancel();

    super.dispose();
  }

  Future<void> initDeepLinks() async {
    _appLinks = AppLinks();

    // Check initial link if app was in cold state (terminated)
    final appLink = await _appLinks.getInitialAppLink();
    if (appLink != null) {
      print('getInitialAppLink: $appLink');
      openAppLink(appLink);
    }

    // Handle link when app is in warm state (front or background)
    _linkSubscription = _appLinks.uriLinkStream.listen((uri) {
      print('onAppLink: $uri');
      openAppLink(uri);
    });
  }

  void openAppLink(Uri uri) {
    setState(() {
      _title = uri.toString();
      APIClient.reload(uri);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Consumer<ModelModel>(builder: (context, model, child) {
      return FutureBuilder<RailwayEntry>(
          future: model.getRailwayEntry(),
          builder: (context, snapshot) {
            if (!snapshot.hasData) {
              final isUnavailable = snapshot.hasError &&
                  snapshot.error is GrpcError &&
                  (snapshot.error as GrpcError).code == StatusCode.unavailable;
              final List<Widget> children = snapshot.hasError
                  ? [
                      ErrorMessage(
                          title: "Failed to load railway entry",
                          error: isUnavailable
                              ? "Cannot connect to server"
                              : snapshot.error),
                      TextButton(
                        onPressed: () => model.getRailwayEntry(),
                        child: const Text("Retry"),
                      ),
                    ]
                  : [
                      const Text('Loading railway entry...'),
                      const CircularProgressIndicator(value: null)
                    ];
              return Scaffold(
                appBar: AppBar(
                  // Here we take the value from the MyHomePage object that was created by
                  // the App.build method, and use it to set our appbar title.
                  title: Text("Binky Railways '$_title'"),
                ),
                body: Center(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: children,
                  ),
                ),
              );
            }
            var rwEntry = snapshot.data!;
            if (rwEntry.name.isNotEmpty) {
              return _buildForLoadedEntry(context);
            } else {
              return const StoragePage();
            }
          });
    });
  }

  Widget _buildForLoadedEntry(BuildContext context) {
    return Consumer<StateModel>(builder: (context, state, child) {
      return FutureBuilder<RailwayState>(
          future: state.getRailwayState(),
          builder: (context, snapshot) {
            if (!snapshot.hasData) {
              final isUnavailable = snapshot.hasError &&
                  snapshot.error is GrpcError &&
                  (snapshot.error as GrpcError).code == StatusCode.unavailable;
              final List<Widget> children = snapshot.hasError
                  ? [
                      ErrorMessage(
                          title: "Failed to load railway state",
                          error: isUnavailable
                              ? "Cannot connect to server"
                              : snapshot.error),
                      TextButton(
                        onPressed: () => state.getRailwayState(),
                        child: const Text("Retry"),
                      ),
                    ]
                  : [
                      const Text('Loading railway state...'),
                      const CircularProgressIndicator(value: null)
                    ];
              return Scaffold(
                appBar: AppBar(
                  // Here we take the value from the MyHomePage object that was created by
                  // the App.build method, and use it to set our appbar title.
                  title: Text("Binky Railways '$_title'"),
                ),
                body: Center(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: children,
                  ),
                ),
              );
            }
            var rwState = snapshot.data!;
            if (rwState.isRunModeEnabled) {
              return const RunPage();
            } else {
              return const EditorPage();
            }
          });
    });
  }
}
