///
//  Generated code. Do not modify.
//  source: br_state_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

import 'dart:async' as $async;

import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'br_model_types.pb.dart' as $1;
import 'br_state_types.pb.dart' as $3;
import 'br_state_service.pb.dart' as $4;
import 'br_model_service.pb.dart' as $0;
export 'br_state_service.pb.dart';

class StateServiceClient extends $grpc.Client {
  static final _$getRailwayState =
      $grpc.ClientMethod<$1.Empty, $3.RailwayState>(
          '/binkyrailways.v1.StateService/GetRailwayState',
          ($1.Empty value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.RailwayState.fromBuffer(value));
  static final _$enableRunMode =
      $grpc.ClientMethod<$4.EnableRunModeRequest, $3.RailwayState>(
          '/binkyrailways.v1.StateService/EnableRunMode',
          ($4.EnableRunModeRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.RailwayState.fromBuffer(value));
  static final _$disableRunMode = $grpc.ClientMethod<$1.Empty, $3.RailwayState>(
      '/binkyrailways.v1.StateService/DisableRunMode',
      ($1.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $3.RailwayState.fromBuffer(value));
  static final _$getStateChanges =
      $grpc.ClientMethod<$4.GetStateChangesRequest, $4.StateChange>(
          '/binkyrailways.v1.StateService/GetStateChanges',
          ($4.GetStateChangesRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.StateChange.fromBuffer(value));
  static final _$setPower =
      $grpc.ClientMethod<$4.SetPowerRequest, $3.RailwayState>(
          '/binkyrailways.v1.StateService/SetPower',
          ($4.SetPowerRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.RailwayState.fromBuffer(value));
  static final _$setAutomaticControl =
      $grpc.ClientMethod<$4.SetAutomaticControlRequest, $3.RailwayState>(
          '/binkyrailways.v1.StateService/SetAutomaticControl',
          ($4.SetAutomaticControlRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.RailwayState.fromBuffer(value));
  static final _$setLocSpeedAndDirection =
      $grpc.ClientMethod<$4.SetLocSpeedAndDirectionRequest, $3.LocState>(
          '/binkyrailways.v1.StateService/SetLocSpeedAndDirection',
          ($4.SetLocSpeedAndDirectionRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.LocState.fromBuffer(value));
  static final _$setLocControlledAutomatically =
      $grpc.ClientMethod<$4.SetLocControlledAutomaticallyRequest, $3.LocState>(
          '/binkyrailways.v1.StateService/SetLocControlledAutomatically',
          ($4.SetLocControlledAutomaticallyRequest value) =>
              value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.LocState.fromBuffer(value));
  static final _$setLocFunctions =
      $grpc.ClientMethod<$4.SetLocFunctionsRequest, $3.LocState>(
          '/binkyrailways.v1.StateService/SetLocFunctions',
          ($4.SetLocFunctionsRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.LocState.fromBuffer(value));
  static final _$setSwitchDirection =
      $grpc.ClientMethod<$4.SetSwitchDirectionRequest, $3.JunctionState>(
          '/binkyrailways.v1.StateService/SetSwitchDirection',
          ($4.SetSwitchDirectionRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.JunctionState.fromBuffer(value));
  static final _$setBinaryOutputActive =
      $grpc.ClientMethod<$4.SetBinaryOutputActiveRequest, $3.OutputState>(
          '/binkyrailways.v1.StateService/SetBinaryOutputActive',
          ($4.SetBinaryOutputActiveRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.OutputState.fromBuffer(value));
  static final _$clickVirtualSensor =
      $grpc.ClientMethod<$4.ClickVirtualSensorRequest, $3.RailwayState>(
          '/binkyrailways.v1.StateService/ClickVirtualSensor',
          ($4.ClickVirtualSensorRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.RailwayState.fromBuffer(value));
  static final _$assignLocToBlock =
      $grpc.ClientMethod<$4.AssignLocToBlockRequest, $3.RailwayState>(
          '/binkyrailways.v1.StateService/AssignLocToBlock',
          ($4.AssignLocToBlockRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.RailwayState.fromBuffer(value));
  static final _$takeLocOfTrack =
      $grpc.ClientMethod<$4.TakeLocOfTrackRequest, $3.RailwayState>(
          '/binkyrailways.v1.StateService/TakeLocOfTrack',
          ($4.TakeLocOfTrackRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.RailwayState.fromBuffer(value));
  static final _$setBlockClosed =
      $grpc.ClientMethod<$4.SetBlockClosedRequest, $3.BlockState>(
          '/binkyrailways.v1.StateService/SetBlockClosed',
          ($4.SetBlockClosedRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.BlockState.fromBuffer(value));
  static final _$discoverHardware = $grpc.ClientMethod<
          $4.DiscoverHardwareRequest, $4.DiscoverHardwareResponse>(
      '/binkyrailways.v1.StateService/DiscoverHardware',
      ($4.DiscoverHardwareRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) =>
          $4.DiscoverHardwareResponse.fromBuffer(value));
  static final _$resetHardwareModule =
      $grpc.ClientMethod<$0.IDRequest, $1.Empty>(
          '/binkyrailways.v1.StateService/ResetHardwareModule',
          ($0.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $1.Empty.fromBuffer(value));

  StateServiceClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options, interceptors: interceptors);

  $grpc.ResponseFuture<$3.RailwayState> getRailwayState($1.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getRailwayState, request, options: options);
  }

  $grpc.ResponseFuture<$3.RailwayState> enableRunMode(
      $4.EnableRunModeRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$enableRunMode, request, options: options);
  }

  $grpc.ResponseFuture<$3.RailwayState> disableRunMode($1.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$disableRunMode, request, options: options);
  }

  $grpc.ResponseStream<$4.StateChange> getStateChanges(
      $4.GetStateChangesRequest request,
      {$grpc.CallOptions? options}) {
    return $createStreamingCall(
        _$getStateChanges, $async.Stream.fromIterable([request]),
        options: options);
  }

  $grpc.ResponseFuture<$3.RailwayState> setPower($4.SetPowerRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setPower, request, options: options);
  }

  $grpc.ResponseFuture<$3.RailwayState> setAutomaticControl(
      $4.SetAutomaticControlRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setAutomaticControl, request, options: options);
  }

  $grpc.ResponseFuture<$3.LocState> setLocSpeedAndDirection(
      $4.SetLocSpeedAndDirectionRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setLocSpeedAndDirection, request,
        options: options);
  }

  $grpc.ResponseFuture<$3.LocState> setLocControlledAutomatically(
      $4.SetLocControlledAutomaticallyRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setLocControlledAutomatically, request,
        options: options);
  }

  $grpc.ResponseFuture<$3.LocState> setLocFunctions(
      $4.SetLocFunctionsRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setLocFunctions, request, options: options);
  }

  $grpc.ResponseFuture<$3.JunctionState> setSwitchDirection(
      $4.SetSwitchDirectionRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setSwitchDirection, request, options: options);
  }

  $grpc.ResponseFuture<$3.OutputState> setBinaryOutputActive(
      $4.SetBinaryOutputActiveRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setBinaryOutputActive, request, options: options);
  }

  $grpc.ResponseFuture<$3.RailwayState> clickVirtualSensor(
      $4.ClickVirtualSensorRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$clickVirtualSensor, request, options: options);
  }

  $grpc.ResponseFuture<$3.RailwayState> assignLocToBlock(
      $4.AssignLocToBlockRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$assignLocToBlock, request, options: options);
  }

  $grpc.ResponseFuture<$3.RailwayState> takeLocOfTrack(
      $4.TakeLocOfTrackRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$takeLocOfTrack, request, options: options);
  }

  $grpc.ResponseFuture<$3.BlockState> setBlockClosed(
      $4.SetBlockClosedRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setBlockClosed, request, options: options);
  }

  $grpc.ResponseFuture<$4.DiscoverHardwareResponse> discoverHardware(
      $4.DiscoverHardwareRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$discoverHardware, request, options: options);
  }

  $grpc.ResponseFuture<$1.Empty> resetHardwareModule($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$resetHardwareModule, request, options: options);
  }
}

abstract class StateServiceBase extends $grpc.Service {
  $core.String get $name => 'binkyrailways.v1.StateService';

  StateServiceBase() {
    $addMethod($grpc.ServiceMethod<$1.Empty, $3.RailwayState>(
        'GetRailwayState',
        getRailwayState_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Empty.fromBuffer(value),
        ($3.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$4.EnableRunModeRequest, $3.RailwayState>(
        'EnableRunMode',
        enableRunMode_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $4.EnableRunModeRequest.fromBuffer(value),
        ($3.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Empty, $3.RailwayState>(
        'DisableRunMode',
        disableRunMode_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Empty.fromBuffer(value),
        ($3.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$4.GetStateChangesRequest, $4.StateChange>(
        'GetStateChanges',
        getStateChanges_Pre,
        false,
        true,
        ($core.List<$core.int> value) =>
            $4.GetStateChangesRequest.fromBuffer(value),
        ($4.StateChange value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$4.SetPowerRequest, $3.RailwayState>(
        'SetPower',
        setPower_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $4.SetPowerRequest.fromBuffer(value),
        ($3.RailwayState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$4.SetAutomaticControlRequest, $3.RailwayState>(
            'SetAutomaticControl',
            setAutomaticControl_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $4.SetAutomaticControlRequest.fromBuffer(value),
            ($3.RailwayState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$4.SetLocSpeedAndDirectionRequest, $3.LocState>(
            'SetLocSpeedAndDirection',
            setLocSpeedAndDirection_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $4.SetLocSpeedAndDirectionRequest.fromBuffer(value),
            ($3.LocState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$4.SetLocControlledAutomaticallyRequest,
            $3.LocState>(
        'SetLocControlledAutomatically',
        setLocControlledAutomatically_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $4.SetLocControlledAutomaticallyRequest.fromBuffer(value),
        ($3.LocState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$4.SetLocFunctionsRequest, $3.LocState>(
        'SetLocFunctions',
        setLocFunctions_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $4.SetLocFunctionsRequest.fromBuffer(value),
        ($3.LocState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$4.SetSwitchDirectionRequest, $3.JunctionState>(
            'SetSwitchDirection',
            setSwitchDirection_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $4.SetSwitchDirectionRequest.fromBuffer(value),
            ($3.JunctionState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$4.SetBinaryOutputActiveRequest, $3.OutputState>(
            'SetBinaryOutputActive',
            setBinaryOutputActive_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $4.SetBinaryOutputActiveRequest.fromBuffer(value),
            ($3.OutputState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$4.ClickVirtualSensorRequest, $3.RailwayState>(
            'ClickVirtualSensor',
            clickVirtualSensor_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $4.ClickVirtualSensorRequest.fromBuffer(value),
            ($3.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$4.AssignLocToBlockRequest, $3.RailwayState>(
        'AssignLocToBlock',
        assignLocToBlock_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $4.AssignLocToBlockRequest.fromBuffer(value),
        ($3.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$4.TakeLocOfTrackRequest, $3.RailwayState>(
        'TakeLocOfTrack',
        takeLocOfTrack_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $4.TakeLocOfTrackRequest.fromBuffer(value),
        ($3.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$4.SetBlockClosedRequest, $3.BlockState>(
        'SetBlockClosed',
        setBlockClosed_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $4.SetBlockClosedRequest.fromBuffer(value),
        ($3.BlockState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$4.DiscoverHardwareRequest,
            $4.DiscoverHardwareResponse>(
        'DiscoverHardware',
        discoverHardware_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $4.DiscoverHardwareRequest.fromBuffer(value),
        ($4.DiscoverHardwareResponse value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $1.Empty>(
        'ResetHardwareModule',
        resetHardwareModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($1.Empty value) => value.writeToBuffer()));
  }

  $async.Future<$3.RailwayState> getRailwayState_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Empty> request) async {
    return getRailwayState(call, await request);
  }

  $async.Future<$3.RailwayState> enableRunMode_Pre($grpc.ServiceCall call,
      $async.Future<$4.EnableRunModeRequest> request) async {
    return enableRunMode(call, await request);
  }

  $async.Future<$3.RailwayState> disableRunMode_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Empty> request) async {
    return disableRunMode(call, await request);
  }

  $async.Stream<$4.StateChange> getStateChanges_Pre($grpc.ServiceCall call,
      $async.Future<$4.GetStateChangesRequest> request) async* {
    yield* getStateChanges(call, await request);
  }

  $async.Future<$3.RailwayState> setPower_Pre(
      $grpc.ServiceCall call, $async.Future<$4.SetPowerRequest> request) async {
    return setPower(call, await request);
  }

  $async.Future<$3.RailwayState> setAutomaticControl_Pre($grpc.ServiceCall call,
      $async.Future<$4.SetAutomaticControlRequest> request) async {
    return setAutomaticControl(call, await request);
  }

  $async.Future<$3.LocState> setLocSpeedAndDirection_Pre($grpc.ServiceCall call,
      $async.Future<$4.SetLocSpeedAndDirectionRequest> request) async {
    return setLocSpeedAndDirection(call, await request);
  }

  $async.Future<$3.LocState> setLocControlledAutomatically_Pre(
      $grpc.ServiceCall call,
      $async.Future<$4.SetLocControlledAutomaticallyRequest> request) async {
    return setLocControlledAutomatically(call, await request);
  }

  $async.Future<$3.LocState> setLocFunctions_Pre($grpc.ServiceCall call,
      $async.Future<$4.SetLocFunctionsRequest> request) async {
    return setLocFunctions(call, await request);
  }

  $async.Future<$3.JunctionState> setSwitchDirection_Pre($grpc.ServiceCall call,
      $async.Future<$4.SetSwitchDirectionRequest> request) async {
    return setSwitchDirection(call, await request);
  }

  $async.Future<$3.OutputState> setBinaryOutputActive_Pre(
      $grpc.ServiceCall call,
      $async.Future<$4.SetBinaryOutputActiveRequest> request) async {
    return setBinaryOutputActive(call, await request);
  }

  $async.Future<$3.RailwayState> clickVirtualSensor_Pre($grpc.ServiceCall call,
      $async.Future<$4.ClickVirtualSensorRequest> request) async {
    return clickVirtualSensor(call, await request);
  }

  $async.Future<$3.RailwayState> assignLocToBlock_Pre($grpc.ServiceCall call,
      $async.Future<$4.AssignLocToBlockRequest> request) async {
    return assignLocToBlock(call, await request);
  }

  $async.Future<$3.RailwayState> takeLocOfTrack_Pre($grpc.ServiceCall call,
      $async.Future<$4.TakeLocOfTrackRequest> request) async {
    return takeLocOfTrack(call, await request);
  }

  $async.Future<$3.BlockState> setBlockClosed_Pre($grpc.ServiceCall call,
      $async.Future<$4.SetBlockClosedRequest> request) async {
    return setBlockClosed(call, await request);
  }

  $async.Future<$4.DiscoverHardwareResponse> discoverHardware_Pre(
      $grpc.ServiceCall call,
      $async.Future<$4.DiscoverHardwareRequest> request) async {
    return discoverHardware(call, await request);
  }

  $async.Future<$1.Empty> resetHardwareModule_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return resetHardwareModule(call, await request);
  }

  $async.Future<$3.RailwayState> getRailwayState(
      $grpc.ServiceCall call, $1.Empty request);
  $async.Future<$3.RailwayState> enableRunMode(
      $grpc.ServiceCall call, $4.EnableRunModeRequest request);
  $async.Future<$3.RailwayState> disableRunMode(
      $grpc.ServiceCall call, $1.Empty request);
  $async.Stream<$4.StateChange> getStateChanges(
      $grpc.ServiceCall call, $4.GetStateChangesRequest request);
  $async.Future<$3.RailwayState> setPower(
      $grpc.ServiceCall call, $4.SetPowerRequest request);
  $async.Future<$3.RailwayState> setAutomaticControl(
      $grpc.ServiceCall call, $4.SetAutomaticControlRequest request);
  $async.Future<$3.LocState> setLocSpeedAndDirection(
      $grpc.ServiceCall call, $4.SetLocSpeedAndDirectionRequest request);
  $async.Future<$3.LocState> setLocControlledAutomatically(
      $grpc.ServiceCall call, $4.SetLocControlledAutomaticallyRequest request);
  $async.Future<$3.LocState> setLocFunctions(
      $grpc.ServiceCall call, $4.SetLocFunctionsRequest request);
  $async.Future<$3.JunctionState> setSwitchDirection(
      $grpc.ServiceCall call, $4.SetSwitchDirectionRequest request);
  $async.Future<$3.OutputState> setBinaryOutputActive(
      $grpc.ServiceCall call, $4.SetBinaryOutputActiveRequest request);
  $async.Future<$3.RailwayState> clickVirtualSensor(
      $grpc.ServiceCall call, $4.ClickVirtualSensorRequest request);
  $async.Future<$3.RailwayState> assignLocToBlock(
      $grpc.ServiceCall call, $4.AssignLocToBlockRequest request);
  $async.Future<$3.RailwayState> takeLocOfTrack(
      $grpc.ServiceCall call, $4.TakeLocOfTrackRequest request);
  $async.Future<$3.BlockState> setBlockClosed(
      $grpc.ServiceCall call, $4.SetBlockClosedRequest request);
  $async.Future<$4.DiscoverHardwareResponse> discoverHardware(
      $grpc.ServiceCall call, $4.DiscoverHardwareRequest request);
  $async.Future<$1.Empty> resetHardwareModule(
      $grpc.ServiceCall call, $0.IDRequest request);
}
