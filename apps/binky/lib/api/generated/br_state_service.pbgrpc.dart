///
//  Generated code. Do not modify.
//  source: br_state_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields

import 'dart:async' as $async;

import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'br_model_types.pb.dart' as $1;
import 'br_state_types.pb.dart' as $2;
import 'br_state_service.pb.dart' as $3;
export 'br_state_service.pb.dart';

class StateServiceClient extends $grpc.Client {
  static final _$getRailwayState =
      $grpc.ClientMethod<$1.Empty, $2.RailwayState>(
          '/binkyrailways.v1.StateService/GetRailwayState',
          ($1.Empty value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.RailwayState.fromBuffer(value));
  static final _$enableRunMode =
      $grpc.ClientMethod<$3.EnableRunModeRequest, $2.RailwayState>(
          '/binkyrailways.v1.StateService/EnableRunMode',
          ($3.EnableRunModeRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.RailwayState.fromBuffer(value));
  static final _$disableRunMode = $grpc.ClientMethod<$1.Empty, $2.RailwayState>(
      '/binkyrailways.v1.StateService/DisableRunMode',
      ($1.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.RailwayState.fromBuffer(value));
  static final _$getStateChanges =
      $grpc.ClientMethod<$3.GetStateChangesRequest, $3.StateChange>(
          '/binkyrailways.v1.StateService/GetStateChanges',
          ($3.GetStateChangesRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.StateChange.fromBuffer(value));
  static final _$setPower =
      $grpc.ClientMethod<$3.SetPowerRequest, $2.RailwayState>(
          '/binkyrailways.v1.StateService/SetPower',
          ($3.SetPowerRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.RailwayState.fromBuffer(value));

  StateServiceClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options, interceptors: interceptors);

  $grpc.ResponseFuture<$2.RailwayState> getRailwayState($1.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getRailwayState, request, options: options);
  }

  $grpc.ResponseFuture<$2.RailwayState> enableRunMode(
      $3.EnableRunModeRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$enableRunMode, request, options: options);
  }

  $grpc.ResponseFuture<$2.RailwayState> disableRunMode($1.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$disableRunMode, request, options: options);
  }

  $grpc.ResponseStream<$3.StateChange> getStateChanges(
      $3.GetStateChangesRequest request,
      {$grpc.CallOptions? options}) {
    return $createStreamingCall(
        _$getStateChanges, $async.Stream.fromIterable([request]),
        options: options);
  }

  $grpc.ResponseFuture<$2.RailwayState> setPower($3.SetPowerRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setPower, request, options: options);
  }
}

abstract class StateServiceBase extends $grpc.Service {
  $core.String get $name => 'binkyrailways.v1.StateService';

  StateServiceBase() {
    $addMethod($grpc.ServiceMethod<$1.Empty, $2.RailwayState>(
        'GetRailwayState',
        getRailwayState_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Empty.fromBuffer(value),
        ($2.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$3.EnableRunModeRequest, $2.RailwayState>(
        'EnableRunMode',
        enableRunMode_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $3.EnableRunModeRequest.fromBuffer(value),
        ($2.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Empty, $2.RailwayState>(
        'DisableRunMode',
        disableRunMode_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Empty.fromBuffer(value),
        ($2.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$3.GetStateChangesRequest, $3.StateChange>(
        'GetStateChanges',
        getStateChanges_Pre,
        false,
        true,
        ($core.List<$core.int> value) =>
            $3.GetStateChangesRequest.fromBuffer(value),
        ($3.StateChange value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$3.SetPowerRequest, $2.RailwayState>(
        'SetPower',
        setPower_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $3.SetPowerRequest.fromBuffer(value),
        ($2.RailwayState value) => value.writeToBuffer()));
  }

  $async.Future<$2.RailwayState> getRailwayState_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Empty> request) async {
    return getRailwayState(call, await request);
  }

  $async.Future<$2.RailwayState> enableRunMode_Pre($grpc.ServiceCall call,
      $async.Future<$3.EnableRunModeRequest> request) async {
    return enableRunMode(call, await request);
  }

  $async.Future<$2.RailwayState> disableRunMode_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Empty> request) async {
    return disableRunMode(call, await request);
  }

  $async.Stream<$3.StateChange> getStateChanges_Pre($grpc.ServiceCall call,
      $async.Future<$3.GetStateChangesRequest> request) async* {
    yield* getStateChanges(call, await request);
  }

  $async.Future<$2.RailwayState> setPower_Pre(
      $grpc.ServiceCall call, $async.Future<$3.SetPowerRequest> request) async {
    return setPower(call, await request);
  }

  $async.Future<$2.RailwayState> getRailwayState(
      $grpc.ServiceCall call, $1.Empty request);
  $async.Future<$2.RailwayState> enableRunMode(
      $grpc.ServiceCall call, $3.EnableRunModeRequest request);
  $async.Future<$2.RailwayState> disableRunMode(
      $grpc.ServiceCall call, $1.Empty request);
  $async.Stream<$3.StateChange> getStateChanges(
      $grpc.ServiceCall call, $3.GetStateChangesRequest request);
  $async.Future<$2.RailwayState> setPower(
      $grpc.ServiceCall call, $3.SetPowerRequest request);
}
