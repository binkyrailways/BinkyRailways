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

import 'package:grpc/grpc.dart';

import "./generated/br_model_service.pbgrpc.dart";
import './generated/br_state_service.pbgrpc.dart';

class APIClient {
  final ClientChannel _channel;

  final ModelServiceClient _modelClient;
  final StateServiceClient _stateClient;

  APIClient._initialize(ClientChannel channel)
      : _channel = channel,
        _modelClient = ModelServiceClient(channel),
        _stateClient = StateServiceClient(channel);

  static final _instance =
      APIClient._initialize(ClientChannel('192.168.140.164', //'127.0.0.1',
          port: 18034,
          options: const ChannelOptions(
            credentials: ChannelCredentials.insecure(),
          )));

  factory APIClient() => _instance;

  ModelServiceClient modelClient() => _modelClient;

  StateServiceClient stateClient() => _stateClient;

  void shutdown() {
    _channel.shutdown();
  }
}
