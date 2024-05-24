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

import 'package:binky/errors.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:app_links/app_links.dart';
import 'package:universal_html/html.dart' as html;

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
  String _lastFrontendBuild = "";
  Timer? _updateTimer;

  @override
  void initState() {
    super.initState();

    initDeepLinks();
  }

  @override
  void dispose() {
    _linkSubscription?.cancel();
    _updateTimer?.cancel();

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

  // Ensure we have an update timer running
  void _ensureUpdateTimer(AppModel appModel, ModelModel modelModel) {
    _updateTimer ??= Timer.periodic(const Duration(seconds: 2),
        (timer) => _updateServerState(appModel, modelModel));
  }

  // Trigger an update of app info & railway entry.
  void _updateServerState(AppModel appModel, ModelModel modelModel) async {
    // Load app info
    final info = await appModel.updateAppInfo();
    if (info != null) {
      final frontendBuild = info.frontendBuild;
      if (frontendBuild != _lastFrontendBuild) {
        if (_lastFrontendBuild.isEmpty) {
          // First time we see a frontend version. Save it.
          setState(() {
            _lastFrontendBuild = frontendBuild;
          });
        } else {
          // Frontend version changed, reload window
          html.window.location.reload();
          return;
        }
      }
      // Load railway entry
      modelModel.updateRailwayEntry();
    }
  }

  @override
  Widget build(BuildContext context) {
    return Consumer<AppModel>(builder: (context, appModel, child) {
      return Consumer<ModelModel>(builder: (context, modelModel, child) {
        _ensureUpdateTimer(appModel, modelModel);
        return FutureBuilder<AppInfo>(
            future: appModel.getAppInfo(),
            builder: (context, snapshot) {
              if (!snapshot.hasData) {
                final List<Widget> children = snapshot.hasError
                    ? [
                        ErrorMessage(
                          title: "Failed to load application information.",
                          error: Errors.format(snapshot.error),
                        ),
                      ]
                    : [
                        const Text('Loading application info...'),
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
              } else {
                return _buildRailwayEntryLayer(context);
              }
            });
      });
    });
  }

  Widget _buildRailwayEntryLayer(BuildContext context) {
    return Consumer<ModelModel>(builder: (context, modelModel, child) {
      return FutureBuilder<RailwayEntry>(
          future: modelModel.getRailwayEntry(),
          builder: (context, snapshot) {
            if (!snapshot.hasData) {
              final List<Widget> children = snapshot.hasError
                  ? [
                      ErrorMessage(
                        title: "Failed to load railway entry",
                        error: Errors.format(snapshot.error),
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
            } else {
              var rwEntry = snapshot.data!;
              if (rwEntry.name.isNotEmpty) {
                return _buildForLoadedEntry(context);
              } else {
                return const StoragePage();
              }
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
              final List<Widget> children = snapshot.hasError
                  ? [
                      ErrorMessage(
                        title: "Failed to load railway state",
                        error: Errors.format(snapshot.error),
                      ),
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
