// Copyright 2023 Ewout Prangsma
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

intf.ClientChannel createChannel(String host, int port) =>
    throw UnsupportedError("not implemented");

String defaultChannelHost() => throw UnsupportedError("not implemented");

int defaultChannelPort() => throw UnsupportedError("not implemented");
