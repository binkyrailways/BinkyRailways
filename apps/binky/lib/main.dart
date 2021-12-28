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
import 'package:provider/provider.dart';

import 'models/model_model.dart';
import 'models/state_model.dart';
import "app/app_page.dart";

void main() {
  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (context) => ModelModel()),
        ChangeNotifierProvider(create: (context) => StateModel()),
      ],
      child: const BinkyApp(),
    ),
  );
}

class BinkyApp extends StatelessWidget {
  const BinkyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Binky Railways',
      theme: ThemeData(
        primarySwatch: Colors.indigo,
      ),
      home: const AppPage(),
    );
  }
}
