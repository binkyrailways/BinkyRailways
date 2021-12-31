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
	"google.golang.org/grpc/codes"
	"google.golang.org/grpc/status"
)

// CauseFunc specifies the prototype of a function that must return the cause
// of the given error.
// If there is not underlying cause, the given error itself must be retured.
// If nil is passed, nil must be returned.
type CauseFunc = func(error) error

// Cause is the cause function used by the error helpers in this module.
var Cause = func(err error) error { return err }

// IsCanceled returns true if the given error signals a request that was canceled. Typically by the caller.
func IsCanceled(err error) bool {
	return status.Code(Cause(err)) == codes.Canceled
}

// Canceled creates a new error that signals a request that was canceled. Typically by the caller.
func Canceled(msg string, args ...interface{}) error {
	if len(args) > 0 {
		return status.Errorf(codes.Canceled, msg, args...)
	}
	return status.Error(codes.Canceled, msg)
}

// IsDeadlineExceeded returns true if the given error signals a request that timed out.
func IsDeadlineExceeded(err error) bool {
	return status.Code(Cause(err)) == codes.DeadlineExceeded
}

// DeadlineExceeded creates a new error that signals a request that timed out.
func DeadlineExceeded(msg string, args ...interface{}) error {
	if len(args) > 0 {
		return status.Errorf(codes.DeadlineExceeded, msg, args...)
	}
	return status.Error(codes.DeadlineExceeded, msg)
}

// IsInvalidArgument returns true if the given error signals a request with invalid arguments.
func IsInvalidArgument(err error) bool {
	return status.Code(Cause(err)) == codes.InvalidArgument
}

// InvalidArgument creates a new error that signals a request with invalid arguments.
func InvalidArgument(msg string, args ...interface{}) error {
	if len(args) > 0 {
		return status.Errorf(codes.InvalidArgument, msg, args...)
	}
	return status.Error(codes.InvalidArgument, msg)
}

// IsNotFound returns true if the given error signals a request to an object that is not found.
func IsNotFound(err error) bool {
	return status.Code(Cause(err)) == codes.NotFound
}

// NotFound creates a new error that signals a request to an object that is not found.
func NotFound(msg string, args ...interface{}) error {
	if len(args) > 0 {
		return status.Errorf(codes.NotFound, msg, args...)
	}
	return status.Error(codes.NotFound, msg)
}

// IsAlreadyExists returns true if the given error signals a request to create an object that already exists.
func IsAlreadyExists(err error) bool {
	return status.Code(Cause(err)) == codes.AlreadyExists
}

// AlreadyExists creates a new error that signals a request to create an object that already exists.
func AlreadyExists(msg string, args ...interface{}) error {
	if len(args) > 0 {
		return status.Errorf(codes.AlreadyExists, msg, args...)
	}
	return status.Error(codes.AlreadyExists, msg)
}

// IsPermissionDenied returns true if the given error signals a request that the caller has not enough permissions for.
func IsPermissionDenied(err error) bool {
	return status.Code(Cause(err)) == codes.PermissionDenied
}

// PermissionDenied creates a new error that signals a request that the caller has not enough permissions for.
func PermissionDenied(msg string, args ...interface{}) error {
	if len(args) > 0 {
		return status.Errorf(codes.PermissionDenied, msg, args...)
	}
	return status.Error(codes.PermissionDenied, msg)
}

// IsPreconditionFailed returns true if the given error signals a precondition of the request has failed.
func IsPreconditionFailed(err error) bool {
	return status.Code(Cause(err)) == codes.FailedPrecondition
}

// PreconditionFailed creates a new error that signals a request that a precondition of the call has failed.
func PreconditionFailed(msg string, args ...interface{}) error {
	if len(args) > 0 {
		return status.Errorf(codes.FailedPrecondition, msg, args...)
	}
	return status.Error(codes.FailedPrecondition, msg)
}

// IsUnauthenticated returns true if the given error signals an unauthenticated request.
func IsUnauthenticated(err error) bool {
	return status.Code(Cause(err)) == codes.Unauthenticated
}

// Unauthenticated creates a new error that signals an unauthenticated request.
func Unauthenticated(msg string, args ...interface{}) error {
	if len(args) > 0 {
		return status.Errorf(codes.Unauthenticated, msg, args...)
	}
	return status.Error(codes.Unauthenticated, msg)
}

// IsUnknown returns true if the given error signals an unknown error.
func IsUnknown(err error) bool {
	return status.Code(Cause(err)) == codes.Unknown
}

// Unknown creates a new error that signals an unknown error.
func Unknown(msg string, args ...interface{}) error {
	if len(args) > 0 {
		return status.Errorf(codes.Unknown, msg, args...)
	}
	return status.Error(codes.Unknown, msg)
}
