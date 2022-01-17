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
  static final _$setAutomaticControl =
      $grpc.ClientMethod<$3.SetAutomaticControlRequest, $2.RailwayState>(
          '/binkyrailways.v1.StateService/SetAutomaticControl',
          ($3.SetAutomaticControlRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.RailwayState.fromBuffer(value));
  static final _$setLocSpeedAndDirection =
      $grpc.ClientMethod<$3.SetLocSpeedAndDirectionRequest, $2.LocState>(
          '/binkyrailways.v1.StateService/SetLocSpeedAndDirection',
          ($3.SetLocSpeedAndDirectionRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.LocState.fromBuffer(value));
  static final _$setLocControlledAutomatically =
      $grpc.ClientMethod<$3.SetLocControlledAutomaticallyRequest, $2.LocState>(
          '/binkyrailways.v1.StateService/SetLocControlledAutomatically',
          ($3.SetLocControlledAutomaticallyRequest value) =>
              value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.LocState.fromBuffer(value));
  static final _$setSwitchDirection =
      $grpc.ClientMethod<$3.SetSwitchDirectionRequest, $2.JunctionState>(
          '/binkyrailways.v1.StateService/SetSwitchDirection',
          ($3.SetSwitchDirectionRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.JunctionState.fromBuffer(value));
  static final _$setBinaryOutputActive =
      $grpc.ClientMethod<$3.SetBinaryOutputActiveRequest, $2.OutputState>(
          '/binkyrailways.v1.StateService/SetBinaryOutputActive',
          ($3.SetBinaryOutputActiveRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.OutputState.fromBuffer(value));
  static final _$clickVirtualSensor =
      $grpc.ClientMethod<$3.ClickVirtualSensorRequest, $2.RailwayState>(
          '/binkyrailways.v1.StateService/ClickVirtualSensor',
          ($3.ClickVirtualSensorRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.RailwayState.fromBuffer(value));
  static final _$assignLocToBlock =
      $grpc.ClientMethod<$3.AssignLocToBlockRequest, $2.RailwayState>(
          '/binkyrailways.v1.StateService/AssignLocToBlock',
          ($3.AssignLocToBlockRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.RailwayState.fromBuffer(value));
  static final _$setBlockClosed =
      $grpc.ClientMethod<$3.SetBlockClosedRequest, $2.BlockState>(
          '/binkyrailways.v1.StateService/SetBlockClosed',
          ($3.SetBlockClosedRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.BlockState.fromBuffer(value));

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

  $grpc.ResponseFuture<$2.RailwayState> setAutomaticControl(
      $3.SetAutomaticControlRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setAutomaticControl, request, options: options);
  }

  $grpc.ResponseFuture<$2.LocState> setLocSpeedAndDirection(
      $3.SetLocSpeedAndDirectionRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setLocSpeedAndDirection, request,
        options: options);
  }

  $grpc.ResponseFuture<$2.LocState> setLocControlledAutomatically(
      $3.SetLocControlledAutomaticallyRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setLocControlledAutomatically, request,
        options: options);
  }

  $grpc.ResponseFuture<$2.JunctionState> setSwitchDirection(
      $3.SetSwitchDirectionRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setSwitchDirection, request, options: options);
  }

  $grpc.ResponseFuture<$2.OutputState> setBinaryOutputActive(
      $3.SetBinaryOutputActiveRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setBinaryOutputActive, request, options: options);
  }

  $grpc.ResponseFuture<$2.RailwayState> clickVirtualSensor(
      $3.ClickVirtualSensorRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$clickVirtualSensor, request, options: options);
  }

  $grpc.ResponseFuture<$2.RailwayState> assignLocToBlock(
      $3.AssignLocToBlockRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$assignLocToBlock, request, options: options);
  }

  $grpc.ResponseFuture<$2.BlockState> setBlockClosed(
      $3.SetBlockClosedRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setBlockClosed, request, options: options);
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
    $addMethod(
        $grpc.ServiceMethod<$3.SetAutomaticControlRequest, $2.RailwayState>(
            'SetAutomaticControl',
            setAutomaticControl_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $3.SetAutomaticControlRequest.fromBuffer(value),
            ($2.RailwayState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$3.SetLocSpeedAndDirectionRequest, $2.LocState>(
            'SetLocSpeedAndDirection',
            setLocSpeedAndDirection_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $3.SetLocSpeedAndDirectionRequest.fromBuffer(value),
            ($2.LocState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$3.SetLocControlledAutomaticallyRequest,
            $2.LocState>(
        'SetLocControlledAutomatically',
        setLocControlledAutomatically_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $3.SetLocControlledAutomaticallyRequest.fromBuffer(value),
        ($2.LocState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$3.SetSwitchDirectionRequest, $2.JunctionState>(
            'SetSwitchDirection',
            setSwitchDirection_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $3.SetSwitchDirectionRequest.fromBuffer(value),
            ($2.JunctionState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$3.SetBinaryOutputActiveRequest, $2.OutputState>(
            'SetBinaryOutputActive',
            setBinaryOutputActive_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $3.SetBinaryOutputActiveRequest.fromBuffer(value),
            ($2.OutputState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$3.ClickVirtualSensorRequest, $2.RailwayState>(
            'ClickVirtualSensor',
            clickVirtualSensor_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $3.ClickVirtualSensorRequest.fromBuffer(value),
            ($2.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$3.AssignLocToBlockRequest, $2.RailwayState>(
        'AssignLocToBlock',
        assignLocToBlock_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $3.AssignLocToBlockRequest.fromBuffer(value),
        ($2.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$3.SetBlockClosedRequest, $2.BlockState>(
        'SetBlockClosed',
        setBlockClosed_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $3.SetBlockClosedRequest.fromBuffer(value),
        ($2.BlockState value) => value.writeToBuffer()));
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

  $async.Future<$2.RailwayState> setAutomaticControl_Pre($grpc.ServiceCall call,
      $async.Future<$3.SetAutomaticControlRequest> request) async {
    return setAutomaticControl(call, await request);
  }

  $async.Future<$2.LocState> setLocSpeedAndDirection_Pre($grpc.ServiceCall call,
      $async.Future<$3.SetLocSpeedAndDirectionRequest> request) async {
    return setLocSpeedAndDirection(call, await request);
  }

  $async.Future<$2.LocState> setLocControlledAutomatically_Pre(
      $grpc.ServiceCall call,
      $async.Future<$3.SetLocControlledAutomaticallyRequest> request) async {
    return setLocControlledAutomatically(call, await request);
  }

  $async.Future<$2.JunctionState> setSwitchDirection_Pre($grpc.ServiceCall call,
      $async.Future<$3.SetSwitchDirectionRequest> request) async {
    return setSwitchDirection(call, await request);
  }

  $async.Future<$2.OutputState> setBinaryOutputActive_Pre(
      $grpc.ServiceCall call,
      $async.Future<$3.SetBinaryOutputActiveRequest> request) async {
    return setBinaryOutputActive(call, await request);
  }

  $async.Future<$2.RailwayState> clickVirtualSensor_Pre($grpc.ServiceCall call,
      $async.Future<$3.ClickVirtualSensorRequest> request) async {
    return clickVirtualSensor(call, await request);
  }

  $async.Future<$2.RailwayState> assignLocToBlock_Pre($grpc.ServiceCall call,
      $async.Future<$3.AssignLocToBlockRequest> request) async {
    return assignLocToBlock(call, await request);
  }

  $async.Future<$2.BlockState> setBlockClosed_Pre($grpc.ServiceCall call,
      $async.Future<$3.SetBlockClosedRequest> request) async {
    return setBlockClosed(call, await request);
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
  $async.Future<$2.RailwayState> setAutomaticControl(
      $grpc.ServiceCall call, $3.SetAutomaticControlRequest request);
  $async.Future<$2.LocState> setLocSpeedAndDirection(
      $grpc.ServiceCall call, $3.SetLocSpeedAndDirectionRequest request);
  $async.Future<$2.LocState> setLocControlledAutomatically(
      $grpc.ServiceCall call, $3.SetLocControlledAutomaticallyRequest request);
  $async.Future<$2.JunctionState> setSwitchDirection(
      $grpc.ServiceCall call, $3.SetSwitchDirectionRequest request);
  $async.Future<$2.OutputState> setBinaryOutputActive(
      $grpc.ServiceCall call, $3.SetBinaryOutputActiveRequest request);
  $async.Future<$2.RailwayState> clickVirtualSensor(
      $grpc.ServiceCall call, $3.ClickVirtualSensorRequest request);
  $async.Future<$2.RailwayState> assignLocToBlock(
      $grpc.ServiceCall call, $3.AssignLocToBlockRequest request);
  $async.Future<$2.BlockState> setBlockClosed(
      $grpc.ServiceCall call, $3.SetBlockClosedRequest request);
}
