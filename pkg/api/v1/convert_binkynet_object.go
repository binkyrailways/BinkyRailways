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

import (
	context "context"

	"go.uber.org/multierr"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// FromModel converts a model BinkyNetObject to an API BinkyNetObject
func (dst *BinkyNetObject) FromModel(ctx context.Context, src model.BinkyNetObject) error {
	dst.Id = src.GetID()
	dst.ObjectId = string(src.GetObjectID())
	dst.ObjectType.FromModel(ctx, src.GetObjectType())
	src.GetConnections().ForEach(func(bnc model.BinkyNetConnection) {
		conn := &BinkyNetConnection{}
		conn.FromModel(ctx, bnc)
		dst.Connections = append(dst.Connections, conn)
	})
	return nil
}

// ToModel converts an API BinkyNetObject to a model BinkyNetObject
func (src *BinkyNetObject) ToModel(ctx context.Context, dst model.BinkyNetObject) error {
	if src.GetId() != dst.GetID() {
		return InvalidArgument("Unexpected binkynet object ID: '%s'", src.GetId())
	}
	if len(src.GetConnections()) != dst.GetConnections().GetCount() {
		return InvalidArgument("Unexpected number of connections in object '%s' (got %d, expected %d) %v", src.GetObjectId(), len(src.GetConnections()), dst.GetConnections().GetCount())
	}
	var err error
	multierr.AppendInto(&err, dst.SetObjectID(api.ObjectID(src.GetObjectId())))
	for i, srcConn := range src.GetConnections() {
		dstConn, ok := dst.GetConnections().GetAt(i)
		if !ok {
			return InvalidArgument("Failed to get connection at index %d", i)
		}
		multierr.AppendInto(&err, srcConn.ToModel(ctx, dstConn))
	}
	// Set object type last, since it can change the connections
	if ot, err := src.GetObjectType().ToModel(ctx); err != nil {
		return err
	} else {
		multierr.AppendInto(&err, dst.SetObjectType(ot))
	}
	return err
}
