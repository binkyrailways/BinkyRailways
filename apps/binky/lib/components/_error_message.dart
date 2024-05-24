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

import 'package:binky/components.dart';
import 'package:flutter/material.dart';

class ErrorMessage extends StatefulWidget {
  final String title;
  final Object? error;

  const ErrorMessage(
      {Key? key, this.title = "An error has occurred", required this.error})
      : super(key: key);

  @override
  State<ErrorMessage> createState() => _ErrorMessageState();
}

class _ErrorMessageState extends State<ErrorMessage> {
  bool _showDetails = false;

  @override
  Widget build(BuildContext context) {
    return DecoratedBox(
      decoration: BoxDecoration(
        border: Border.all(color: Colors.red, width: 2),
        color: Colors.grey.shade100,
        borderRadius: BorderRadius.circular(16),
      ),
      child: Container(
        padding: const EdgeInsets.all(32),
        child: Column(
          children: [
            Text(widget.title,
                style: const TextStyle(fontWeight: FontWeight.bold)),
            Padding(
              padding: const EdgeInsets.only(top: 16),
              child: _showDetails
                  ? Text("${widget.error}")
                  : GestureDetector(
                      child: const Text("show details"),
                      onTap: () {
                        setState(() {
                          _showDetails = !_showDetails;
                        });
                      },
                    ),
            ),
          ],
        ),
      ),
    );
  }
}
