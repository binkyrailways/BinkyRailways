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

import 'package:grpc/src/client/channel.dart' as intf show ClientChannel;

import 'api_channel.dart'
    if (dart.library.io) 'api_channel_grpc.dart'
    if (dart.library.html) 'api_channel_web.dart';

import "./generated/br_app_service.pbgrpc.dart";
import "./generated/br_model_service.pbgrpc.dart";
import './generated/br_state_service.pbgrpc.dart';
import './generated/br_storage_service.pbgrpc.dart';

class APIClient {
  final intf.ClientChannel _channel;

  final AppServiceClient _appClient;
  final ModelServiceClient _modelClient;
  final StateServiceClient _stateClient;
  final StorageServiceClient _storageClient;

  APIClient._initialize(intf.ClientChannel channel)
      : _channel = channel,
        _appClient = AppServiceClient(channel),
        _modelClient = ModelServiceClient(channel),
        _stateClient = StateServiceClient(channel),
        _storageClient = StorageServiceClient(channel);

  static var _instance = APIClient._initialize(
      createChannel(defaultChannelHost(), defaultChannelPort()));

  factory APIClient() => _instance;

  AppServiceClient appClient() => _appClient;

  ModelServiceClient modelClient() => _modelClient;

  StateServiceClient stateClient() => _stateClient;

  StorageServiceClient storageClient() => _storageClient;

  static void reload(Uri uri) {
    final newInstance =
        APIClient._initialize(createChannel(uri.host, uri.port));
    final oldInstance = _instance;
    _instance = newInstance;
    oldInstance.shutdown();
  }

  void shutdown() {
    _channel.shutdown();
  }
}
