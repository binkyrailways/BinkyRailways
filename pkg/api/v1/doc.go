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

package v1

//go:generate protoc -I .:../../../:../../../proto_vendor/:../../../proto_vendor/github.com/gogo/protobuf/protobuf/  --gofast_out=Mgithub.com/golang/protobuf/ptypes/timestamp/timestamp.proto=github.com/gogo/protobuf/types,plugins=grpc,paths=source_relative:. ./br_types.proto ./br_editor.proto
//go:generate protoc -I .:../../../:../../../proto_vendor/:../../../proto_vendor/github.com/gogo/protobuf/protobuf/ --dart_out=grpc:../../../dart/lib/ ./br_editor.proto ./br_types.proto
