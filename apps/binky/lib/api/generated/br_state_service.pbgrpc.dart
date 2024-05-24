///
//  Generated code. Do not modify.
//  source: br_state_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

import 'dart:async' as $async;

import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'br_model_types.pb.dart' as $0;
import 'br_state_types.pb.dart' as $4;
import 'br_state_service.pb.dart' as $5;
import 'br_model_service.pb.dart' as $2;
export 'br_state_service.pb.dart';

class StateServiceClient extends $grpc.Client {
  static final _$getRailwayState =
      $grpc.ClientMethod<$0.Empty, $4.RailwayState>(
          '/binkyrailways.v1.StateService/GetRailwayState',
          ($0.Empty value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.RailwayState.fromBuffer(value));
  static final _$enableRunMode =
      $grpc.ClientMethod<$5.EnableRunModeRequest, $4.RailwayState>(
          '/binkyrailways.v1.StateService/EnableRunMode',
          ($5.EnableRunModeRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.RailwayState.fromBuffer(value));
  static final _$disableRunMode = $grpc.ClientMethod<$0.Empty, $4.RailwayState>(
      '/binkyrailways.v1.StateService/DisableRunMode',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $4.RailwayState.fromBuffer(value));
  static final _$getStateChanges =
      $grpc.ClientMethod<$5.GetStateChangesRequest, $5.StateChange>(
          '/binkyrailways.v1.StateService/GetStateChanges',
          ($5.GetStateChangesRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $5.StateChange.fromBuffer(value));
  static final _$setPower =
      $grpc.ClientMethod<$5.SetPowerRequest, $4.RailwayState>(
          '/binkyrailways.v1.StateService/SetPower',
          ($5.SetPowerRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.RailwayState.fromBuffer(value));
  static final _$setAutomaticControl =
      $grpc.ClientMethod<$5.SetAutomaticControlRequest, $4.RailwayState>(
          '/binkyrailways.v1.StateService/SetAutomaticControl',
          ($5.SetAutomaticControlRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.RailwayState.fromBuffer(value));
  static final _$setLocSpeedAndDirection =
      $grpc.ClientMethod<$5.SetLocSpeedAndDirectionRequest, $4.LocState>(
          '/binkyrailways.v1.StateService/SetLocSpeedAndDirection',
          ($5.SetLocSpeedAndDirectionRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.LocState.fromBuffer(value));
  static final _$setLocControlledAutomatically =
      $grpc.ClientMethod<$5.SetLocControlledAutomaticallyRequest, $4.LocState>(
          '/binkyrailways.v1.StateService/SetLocControlledAutomatically',
          ($5.SetLocControlledAutomaticallyRequest value) =>
              value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.LocState.fromBuffer(value));
  static final _$setLocFunctions =
      $grpc.ClientMethod<$5.SetLocFunctionsRequest, $4.LocState>(
          '/binkyrailways.v1.StateService/SetLocFunctions',
          ($5.SetLocFunctionsRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.LocState.fromBuffer(value));
  static final _$setSwitchDirection =
      $grpc.ClientMethod<$5.SetSwitchDirectionRequest, $4.JunctionState>(
          '/binkyrailways.v1.StateService/SetSwitchDirection',
          ($5.SetSwitchDirectionRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.JunctionState.fromBuffer(value));
  static final _$setBinaryOutputActive =
      $grpc.ClientMethod<$5.SetBinaryOutputActiveRequest, $4.OutputState>(
          '/binkyrailways.v1.StateService/SetBinaryOutputActive',
          ($5.SetBinaryOutputActiveRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.OutputState.fromBuffer(value));
  static final _$clickVirtualSensor =
      $grpc.ClientMethod<$5.ClickVirtualSensorRequest, $4.RailwayState>(
          '/binkyrailways.v1.StateService/ClickVirtualSensor',
          ($5.ClickVirtualSensorRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.RailwayState.fromBuffer(value));
  static final _$assignLocToBlock =
      $grpc.ClientMethod<$5.AssignLocToBlockRequest, $4.RailwayState>(
          '/binkyrailways.v1.StateService/AssignLocToBlock',
          ($5.AssignLocToBlockRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.RailwayState.fromBuffer(value));
  static final _$putLocOnTrack =
      $grpc.ClientMethod<$5.PutLocOnTrackRequest, $4.RailwayState>(
          '/binkyrailways.v1.StateService/PutLocOnTrack',
          ($5.PutLocOnTrackRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.RailwayState.fromBuffer(value));
  static final _$takeLocOfTrack =
      $grpc.ClientMethod<$5.TakeLocOfTrackRequest, $4.RailwayState>(
          '/binkyrailways.v1.StateService/TakeLocOfTrack',
          ($5.TakeLocOfTrackRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.RailwayState.fromBuffer(value));
  static final _$setBlockClosed =
      $grpc.ClientMethod<$5.SetBlockClosedRequest, $4.BlockState>(
          '/binkyrailways.v1.StateService/SetBlockClosed',
          ($5.SetBlockClosedRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $4.BlockState.fromBuffer(value));
  static final _$discoverHardware = $grpc.ClientMethod<
          $5.DiscoverHardwareRequest, $5.DiscoverHardwareResponse>(
      '/binkyrailways.v1.StateService/DiscoverHardware',
      ($5.DiscoverHardwareRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) =>
          $5.DiscoverHardwareResponse.fromBuffer(value));
  static final _$resetHardwareModule =
      $grpc.ClientMethod<$2.IDRequest, $0.Empty>(
          '/binkyrailways.v1.StateService/ResetHardwareModule',
          ($2.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Empty.fromBuffer(value));

  StateServiceClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options, interceptors: interceptors);

  $grpc.ResponseFuture<$4.RailwayState> getRailwayState($0.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getRailwayState, request, options: options);
  }

  $grpc.ResponseFuture<$4.RailwayState> enableRunMode(
      $5.EnableRunModeRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$enableRunMode, request, options: options);
  }

  $grpc.ResponseFuture<$4.RailwayState> disableRunMode($0.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$disableRunMode, request, options: options);
  }

  $grpc.ResponseStream<$5.StateChange> getStateChanges(
      $5.GetStateChangesRequest request,
      {$grpc.CallOptions? options}) {
    return $createStreamingCall(
        _$getStateChanges, $async.Stream.fromIterable([request]),
        options: options);
  }

  $grpc.ResponseFuture<$4.RailwayState> setPower($5.SetPowerRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setPower, request, options: options);
  }

  $grpc.ResponseFuture<$4.RailwayState> setAutomaticControl(
      $5.SetAutomaticControlRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setAutomaticControl, request, options: options);
  }

  $grpc.ResponseFuture<$4.LocState> setLocSpeedAndDirection(
      $5.SetLocSpeedAndDirectionRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setLocSpeedAndDirection, request,
        options: options);
  }

  $grpc.ResponseFuture<$4.LocState> setLocControlledAutomatically(
      $5.SetLocControlledAutomaticallyRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setLocControlledAutomatically, request,
        options: options);
  }

  $grpc.ResponseFuture<$4.LocState> setLocFunctions(
      $5.SetLocFunctionsRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setLocFunctions, request, options: options);
  }

  $grpc.ResponseFuture<$4.JunctionState> setSwitchDirection(
      $5.SetSwitchDirectionRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setSwitchDirection, request, options: options);
  }

  $grpc.ResponseFuture<$4.OutputState> setBinaryOutputActive(
      $5.SetBinaryOutputActiveRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setBinaryOutputActive, request, options: options);
  }

  $grpc.ResponseFuture<$4.RailwayState> clickVirtualSensor(
      $5.ClickVirtualSensorRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$clickVirtualSensor, request, options: options);
  }

  $grpc.ResponseFuture<$4.RailwayState> assignLocToBlock(
      $5.AssignLocToBlockRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$assignLocToBlock, request, options: options);
  }

  $grpc.ResponseFuture<$4.RailwayState> putLocOnTrack(
      $5.PutLocOnTrackRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$putLocOnTrack, request, options: options);
  }

  $grpc.ResponseFuture<$4.RailwayState> takeLocOfTrack(
      $5.TakeLocOfTrackRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$takeLocOfTrack, request, options: options);
  }

  $grpc.ResponseFuture<$4.BlockState> setBlockClosed(
      $5.SetBlockClosedRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$setBlockClosed, request, options: options);
  }

  $grpc.ResponseFuture<$5.DiscoverHardwareResponse> discoverHardware(
      $5.DiscoverHardwareRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$discoverHardware, request, options: options);
  }

  $grpc.ResponseFuture<$0.Empty> resetHardwareModule($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$resetHardwareModule, request, options: options);
  }
}

abstract class StateServiceBase extends $grpc.Service {
  $core.String get $name => 'binkyrailways.v1.StateService';

  StateServiceBase() {
    $addMethod($grpc.ServiceMethod<$0.Empty, $4.RailwayState>(
        'GetRailwayState',
        getRailwayState_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($4.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$5.EnableRunModeRequest, $4.RailwayState>(
        'EnableRunMode',
        enableRunMode_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $5.EnableRunModeRequest.fromBuffer(value),
        ($4.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $4.RailwayState>(
        'DisableRunMode',
        disableRunMode_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($4.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$5.GetStateChangesRequest, $5.StateChange>(
        'GetStateChanges',
        getStateChanges_Pre,
        false,
        true,
        ($core.List<$core.int> value) =>
            $5.GetStateChangesRequest.fromBuffer(value),
        ($5.StateChange value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$5.SetPowerRequest, $4.RailwayState>(
        'SetPower',
        setPower_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $5.SetPowerRequest.fromBuffer(value),
        ($4.RailwayState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$5.SetAutomaticControlRequest, $4.RailwayState>(
            'SetAutomaticControl',
            setAutomaticControl_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $5.SetAutomaticControlRequest.fromBuffer(value),
            ($4.RailwayState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$5.SetLocSpeedAndDirectionRequest, $4.LocState>(
            'SetLocSpeedAndDirection',
            setLocSpeedAndDirection_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $5.SetLocSpeedAndDirectionRequest.fromBuffer(value),
            ($4.LocState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$5.SetLocControlledAutomaticallyRequest,
            $4.LocState>(
        'SetLocControlledAutomatically',
        setLocControlledAutomatically_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $5.SetLocControlledAutomaticallyRequest.fromBuffer(value),
        ($4.LocState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$5.SetLocFunctionsRequest, $4.LocState>(
        'SetLocFunctions',
        setLocFunctions_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $5.SetLocFunctionsRequest.fromBuffer(value),
        ($4.LocState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$5.SetSwitchDirectionRequest, $4.JunctionState>(
            'SetSwitchDirection',
            setSwitchDirection_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $5.SetSwitchDirectionRequest.fromBuffer(value),
            ($4.JunctionState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$5.SetBinaryOutputActiveRequest, $4.OutputState>(
            'SetBinaryOutputActive',
            setBinaryOutputActive_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $5.SetBinaryOutputActiveRequest.fromBuffer(value),
            ($4.OutputState value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$5.ClickVirtualSensorRequest, $4.RailwayState>(
            'ClickVirtualSensor',
            clickVirtualSensor_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $5.ClickVirtualSensorRequest.fromBuffer(value),
            ($4.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$5.AssignLocToBlockRequest, $4.RailwayState>(
        'AssignLocToBlock',
        assignLocToBlock_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $5.AssignLocToBlockRequest.fromBuffer(value),
        ($4.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$5.PutLocOnTrackRequest, $4.RailwayState>(
        'PutLocOnTrack',
        putLocOnTrack_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $5.PutLocOnTrackRequest.fromBuffer(value),
        ($4.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$5.TakeLocOfTrackRequest, $4.RailwayState>(
        'TakeLocOfTrack',
        takeLocOfTrack_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $5.TakeLocOfTrackRequest.fromBuffer(value),
        ($4.RailwayState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$5.SetBlockClosedRequest, $4.BlockState>(
        'SetBlockClosed',
        setBlockClosed_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $5.SetBlockClosedRequest.fromBuffer(value),
        ($4.BlockState value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$5.DiscoverHardwareRequest,
            $5.DiscoverHardwareResponse>(
        'DiscoverHardware',
        discoverHardware_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $5.DiscoverHardwareRequest.fromBuffer(value),
        ($5.DiscoverHardwareResponse value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Empty>(
        'ResetHardwareModule',
        resetHardwareModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Empty value) => value.writeToBuffer()));
  }

  $async.Future<$4.RailwayState> getRailwayState_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return getRailwayState(call, await request);
  }

  $async.Future<$4.RailwayState> enableRunMode_Pre($grpc.ServiceCall call,
      $async.Future<$5.EnableRunModeRequest> request) async {
    return enableRunMode(call, await request);
  }

  $async.Future<$4.RailwayState> disableRunMode_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return disableRunMode(call, await request);
  }

  $async.Stream<$5.StateChange> getStateChanges_Pre($grpc.ServiceCall call,
      $async.Future<$5.GetStateChangesRequest> request) async* {
    yield* getStateChanges(call, await request);
  }

  $async.Future<$4.RailwayState> setPower_Pre(
      $grpc.ServiceCall call, $async.Future<$5.SetPowerRequest> request) async {
    return setPower(call, await request);
  }

  $async.Future<$4.RailwayState> setAutomaticControl_Pre($grpc.ServiceCall call,
      $async.Future<$5.SetAutomaticControlRequest> request) async {
    return setAutomaticControl(call, await request);
  }

  $async.Future<$4.LocState> setLocSpeedAndDirection_Pre($grpc.ServiceCall call,
      $async.Future<$5.SetLocSpeedAndDirectionRequest> request) async {
    return setLocSpeedAndDirection(call, await request);
  }

  $async.Future<$4.LocState> setLocControlledAutomatically_Pre(
      $grpc.ServiceCall call,
      $async.Future<$5.SetLocControlledAutomaticallyRequest> request) async {
    return setLocControlledAutomatically(call, await request);
  }

  $async.Future<$4.LocState> setLocFunctions_Pre($grpc.ServiceCall call,
      $async.Future<$5.SetLocFunctionsRequest> request) async {
    return setLocFunctions(call, await request);
  }

  $async.Future<$4.JunctionState> setSwitchDirection_Pre($grpc.ServiceCall call,
      $async.Future<$5.SetSwitchDirectionRequest> request) async {
    return setSwitchDirection(call, await request);
  }

  $async.Future<$4.OutputState> setBinaryOutputActive_Pre(
      $grpc.ServiceCall call,
      $async.Future<$5.SetBinaryOutputActiveRequest> request) async {
    return setBinaryOutputActive(call, await request);
  }

  $async.Future<$4.RailwayState> clickVirtualSensor_Pre($grpc.ServiceCall call,
      $async.Future<$5.ClickVirtualSensorRequest> request) async {
    return clickVirtualSensor(call, await request);
  }

  $async.Future<$4.RailwayState> assignLocToBlock_Pre($grpc.ServiceCall call,
      $async.Future<$5.AssignLocToBlockRequest> request) async {
    return assignLocToBlock(call, await request);
  }

  $async.Future<$4.RailwayState> putLocOnTrack_Pre($grpc.ServiceCall call,
      $async.Future<$5.PutLocOnTrackRequest> request) async {
    return putLocOnTrack(call, await request);
  }

  $async.Future<$4.RailwayState> takeLocOfTrack_Pre($grpc.ServiceCall call,
      $async.Future<$5.TakeLocOfTrackRequest> request) async {
    return takeLocOfTrack(call, await request);
  }

  $async.Future<$4.BlockState> setBlockClosed_Pre($grpc.ServiceCall call,
      $async.Future<$5.SetBlockClosedRequest> request) async {
    return setBlockClosed(call, await request);
  }

  $async.Future<$5.DiscoverHardwareResponse> discoverHardware_Pre(
      $grpc.ServiceCall call,
      $async.Future<$5.DiscoverHardwareRequest> request) async {
    return discoverHardware(call, await request);
  }

  $async.Future<$0.Empty> resetHardwareModule_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return resetHardwareModule(call, await request);
  }

  $async.Future<$4.RailwayState> getRailwayState(
      $grpc.ServiceCall call, $0.Empty request);
  $async.Future<$4.RailwayState> enableRunMode(
      $grpc.ServiceCall call, $5.EnableRunModeRequest request);
  $async.Future<$4.RailwayState> disableRunMode(
      $grpc.ServiceCall call, $0.Empty request);
  $async.Stream<$5.StateChange> getStateChanges(
      $grpc.ServiceCall call, $5.GetStateChangesRequest request);
  $async.Future<$4.RailwayState> setPower(
      $grpc.ServiceCall call, $5.SetPowerRequest request);
  $async.Future<$4.RailwayState> setAutomaticControl(
      $grpc.ServiceCall call, $5.SetAutomaticControlRequest request);
  $async.Future<$4.LocState> setLocSpeedAndDirection(
      $grpc.ServiceCall call, $5.SetLocSpeedAndDirectionRequest request);
  $async.Future<$4.LocState> setLocControlledAutomatically(
      $grpc.ServiceCall call, $5.SetLocControlledAutomaticallyRequest request);
  $async.Future<$4.LocState> setLocFunctions(
      $grpc.ServiceCall call, $5.SetLocFunctionsRequest request);
  $async.Future<$4.JunctionState> setSwitchDirection(
      $grpc.ServiceCall call, $5.SetSwitchDirectionRequest request);
  $async.Future<$4.OutputState> setBinaryOutputActive(
      $grpc.ServiceCall call, $5.SetBinaryOutputActiveRequest request);
  $async.Future<$4.RailwayState> clickVirtualSensor(
      $grpc.ServiceCall call, $5.ClickVirtualSensorRequest request);
  $async.Future<$4.RailwayState> assignLocToBlock(
      $grpc.ServiceCall call, $5.AssignLocToBlockRequest request);
  $async.Future<$4.RailwayState> putLocOnTrack(
      $grpc.ServiceCall call, $5.PutLocOnTrackRequest request);
  $async.Future<$4.RailwayState> takeLocOfTrack(
      $grpc.ServiceCall call, $5.TakeLocOfTrackRequest request);
  $async.Future<$4.BlockState> setBlockClosed(
      $grpc.ServiceCall call, $5.SetBlockClosedRequest request);
  $async.Future<$5.DiscoverHardwareResponse> discoverHardware(
      $grpc.ServiceCall call, $5.DiscoverHardwareRequest request);
  $async.Future<$0.Empty> resetHardwareModule(
      $grpc.ServiceCall call, $2.IDRequest request);
}
