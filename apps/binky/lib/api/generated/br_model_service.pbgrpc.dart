///
//  Generated code. Do not modify.
//  source: br_model_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields

import 'dart:async' as $async;

import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'br_model_service.pb.dart' as $0;
import 'br_model_types.pb.dart' as $1;
export 'br_model_service.pb.dart';

class ModelServiceClient extends $grpc.Client {
  static final _$parseAddress =
      $grpc.ClientMethod<$0.ParseAddressRequest, $0.ParseAddressResult>(
          '/binkyrailways.v1.ModelService/ParseAddress',
          ($0.ParseAddressRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.ParseAddressResult.fromBuffer(value));
  static final _$getRailway = $grpc.ClientMethod<$1.Empty, $1.Railway>(
      '/binkyrailways.v1.ModelService/GetRailway',
      ($1.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Railway.fromBuffer(value));
  static final _$updateRailway = $grpc.ClientMethod<$1.Railway, $1.Railway>(
      '/binkyrailways.v1.ModelService/UpdateRailway',
      ($1.Railway value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Railway.fromBuffer(value));
  static final _$save = $grpc.ClientMethod<$1.Empty, $1.Empty>(
      '/binkyrailways.v1.ModelService/Save',
      ($1.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Empty.fromBuffer(value));
  static final _$getModule = $grpc.ClientMethod<$0.IDRequest, $1.Module>(
      '/binkyrailways.v1.ModelService/GetModule',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Module.fromBuffer(value));
  static final _$getModuleBackgroundImage =
      $grpc.ClientMethod<$0.IDRequest, $1.Image>(
          '/binkyrailways.v1.ModelService/GetModuleBackgroundImage',
          ($0.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $1.Image.fromBuffer(value));
  static final _$updateModule = $grpc.ClientMethod<$1.Module, $1.Module>(
      '/binkyrailways.v1.ModelService/UpdateModule',
      ($1.Module value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Module.fromBuffer(value));
  static final _$getLoc = $grpc.ClientMethod<$0.IDRequest, $1.Loc>(
      '/binkyrailways.v1.ModelService/GetLoc',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Loc.fromBuffer(value));
  static final _$updateLoc = $grpc.ClientMethod<$1.Loc, $1.Loc>(
      '/binkyrailways.v1.ModelService/UpdateLoc',
      ($1.Loc value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Loc.fromBuffer(value));
  static final _$getLocGroup = $grpc.ClientMethod<$0.IDRequest, $1.LocGroup>(
      '/binkyrailways.v1.ModelService/GetLocGroup',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.LocGroup.fromBuffer(value));
  static final _$updateLocGroup = $grpc.ClientMethod<$1.LocGroup, $1.LocGroup>(
      '/binkyrailways.v1.ModelService/UpdateLocGroup',
      ($1.LocGroup value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.LocGroup.fromBuffer(value));
  static final _$getCommandStation =
      $grpc.ClientMethod<$0.IDRequest, $1.CommandStation>(
          '/binkyrailways.v1.ModelService/GetCommandStation',
          ($0.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $1.CommandStation.fromBuffer(value));
  static final _$updateCommandStation =
      $grpc.ClientMethod<$1.CommandStation, $1.CommandStation>(
          '/binkyrailways.v1.ModelService/UpdateCommandStation',
          ($1.CommandStation value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $1.CommandStation.fromBuffer(value));
  static final _$getBlock = $grpc.ClientMethod<$0.IDRequest, $1.Block>(
      '/binkyrailways.v1.ModelService/GetBlock',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Block.fromBuffer(value));
  static final _$updateBlock = $grpc.ClientMethod<$1.Block, $1.Block>(
      '/binkyrailways.v1.ModelService/UpdateBlock',
      ($1.Block value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Block.fromBuffer(value));
  static final _$getBlockGroup =
      $grpc.ClientMethod<$0.IDRequest, $1.BlockGroup>(
          '/binkyrailways.v1.ModelService/GetBlockGroup',
          ($0.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $1.BlockGroup.fromBuffer(value));
  static final _$updateBlockGroup =
      $grpc.ClientMethod<$1.BlockGroup, $1.BlockGroup>(
          '/binkyrailways.v1.ModelService/UpdateBlockGroup',
          ($1.BlockGroup value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $1.BlockGroup.fromBuffer(value));
  static final _$getEdge = $grpc.ClientMethod<$0.IDRequest, $1.Edge>(
      '/binkyrailways.v1.ModelService/GetEdge',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Edge.fromBuffer(value));
  static final _$updateEdge = $grpc.ClientMethod<$1.Edge, $1.Edge>(
      '/binkyrailways.v1.ModelService/UpdateEdge',
      ($1.Edge value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Edge.fromBuffer(value));
  static final _$getJunction = $grpc.ClientMethod<$0.IDRequest, $1.Junction>(
      '/binkyrailways.v1.ModelService/GetJunction',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Junction.fromBuffer(value));
  static final _$updateJunction = $grpc.ClientMethod<$1.Junction, $1.Junction>(
      '/binkyrailways.v1.ModelService/UpdateJunction',
      ($1.Junction value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Junction.fromBuffer(value));
  static final _$getOutput = $grpc.ClientMethod<$0.IDRequest, $1.Output>(
      '/binkyrailways.v1.ModelService/GetOutput',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Output.fromBuffer(value));
  static final _$updateOutput = $grpc.ClientMethod<$1.Output, $1.Output>(
      '/binkyrailways.v1.ModelService/UpdateOutput',
      ($1.Output value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Output.fromBuffer(value));
  static final _$getSensor = $grpc.ClientMethod<$0.IDRequest, $1.Sensor>(
      '/binkyrailways.v1.ModelService/GetSensor',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Sensor.fromBuffer(value));
  static final _$updateSensor = $grpc.ClientMethod<$1.Sensor, $1.Sensor>(
      '/binkyrailways.v1.ModelService/UpdateSensor',
      ($1.Sensor value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.Sensor.fromBuffer(value));

  ModelServiceClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options, interceptors: interceptors);

  $grpc.ResponseFuture<$0.ParseAddressResult> parseAddress(
      $0.ParseAddressRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$parseAddress, request, options: options);
  }

  $grpc.ResponseFuture<$1.Railway> getRailway($1.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getRailway, request, options: options);
  }

  $grpc.ResponseFuture<$1.Railway> updateRailway($1.Railway request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateRailway, request, options: options);
  }

  $grpc.ResponseFuture<$1.Empty> save($1.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$save, request, options: options);
  }

  $grpc.ResponseFuture<$1.Module> getModule($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getModule, request, options: options);
  }

  $grpc.ResponseFuture<$1.Image> getModuleBackgroundImage($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getModuleBackgroundImage, request,
        options: options);
  }

  $grpc.ResponseFuture<$1.Module> updateModule($1.Module request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateModule, request, options: options);
  }

  $grpc.ResponseFuture<$1.Loc> getLoc($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getLoc, request, options: options);
  }

  $grpc.ResponseFuture<$1.Loc> updateLoc($1.Loc request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateLoc, request, options: options);
  }

  $grpc.ResponseFuture<$1.LocGroup> getLocGroup($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getLocGroup, request, options: options);
  }

  $grpc.ResponseFuture<$1.LocGroup> updateLocGroup($1.LocGroup request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateLocGroup, request, options: options);
  }

  $grpc.ResponseFuture<$1.CommandStation> getCommandStation(
      $0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getCommandStation, request, options: options);
  }

  $grpc.ResponseFuture<$1.CommandStation> updateCommandStation(
      $1.CommandStation request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateCommandStation, request, options: options);
  }

  $grpc.ResponseFuture<$1.Block> getBlock($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getBlock, request, options: options);
  }

  $grpc.ResponseFuture<$1.Block> updateBlock($1.Block request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateBlock, request, options: options);
  }

  $grpc.ResponseFuture<$1.BlockGroup> getBlockGroup($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getBlockGroup, request, options: options);
  }

  $grpc.ResponseFuture<$1.BlockGroup> updateBlockGroup($1.BlockGroup request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateBlockGroup, request, options: options);
  }

  $grpc.ResponseFuture<$1.Edge> getEdge($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getEdge, request, options: options);
  }

  $grpc.ResponseFuture<$1.Edge> updateEdge($1.Edge request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateEdge, request, options: options);
  }

  $grpc.ResponseFuture<$1.Junction> getJunction($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getJunction, request, options: options);
  }

  $grpc.ResponseFuture<$1.Junction> updateJunction($1.Junction request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateJunction, request, options: options);
  }

  $grpc.ResponseFuture<$1.Output> getOutput($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getOutput, request, options: options);
  }

  $grpc.ResponseFuture<$1.Output> updateOutput($1.Output request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateOutput, request, options: options);
  }

  $grpc.ResponseFuture<$1.Sensor> getSensor($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getSensor, request, options: options);
  }

  $grpc.ResponseFuture<$1.Sensor> updateSensor($1.Sensor request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateSensor, request, options: options);
  }
}

abstract class ModelServiceBase extends $grpc.Service {
  $core.String get $name => 'binkyrailways.v1.ModelService';

  ModelServiceBase() {
    $addMethod(
        $grpc.ServiceMethod<$0.ParseAddressRequest, $0.ParseAddressResult>(
            'ParseAddress',
            parseAddress_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $0.ParseAddressRequest.fromBuffer(value),
            ($0.ParseAddressResult value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Empty, $1.Railway>(
        'GetRailway',
        getRailway_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Empty.fromBuffer(value),
        ($1.Railway value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Railway, $1.Railway>(
        'UpdateRailway',
        updateRailway_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Railway.fromBuffer(value),
        ($1.Railway value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Empty, $1.Empty>(
        'Save',
        save_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Empty.fromBuffer(value),
        ($1.Empty value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $1.Module>(
        'GetModule',
        getModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($1.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $1.Image>(
        'GetModuleBackgroundImage',
        getModuleBackgroundImage_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($1.Image value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Module, $1.Module>(
        'UpdateModule',
        updateModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Module.fromBuffer(value),
        ($1.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $1.Loc>(
        'GetLoc',
        getLoc_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($1.Loc value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Loc, $1.Loc>(
        'UpdateLoc',
        updateLoc_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Loc.fromBuffer(value),
        ($1.Loc value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $1.LocGroup>(
        'GetLocGroup',
        getLocGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($1.LocGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.LocGroup, $1.LocGroup>(
        'UpdateLocGroup',
        updateLocGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.LocGroup.fromBuffer(value),
        ($1.LocGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $1.CommandStation>(
        'GetCommandStation',
        getCommandStation_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($1.CommandStation value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.CommandStation, $1.CommandStation>(
        'UpdateCommandStation',
        updateCommandStation_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.CommandStation.fromBuffer(value),
        ($1.CommandStation value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $1.Block>(
        'GetBlock',
        getBlock_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($1.Block value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Block, $1.Block>(
        'UpdateBlock',
        updateBlock_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Block.fromBuffer(value),
        ($1.Block value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $1.BlockGroup>(
        'GetBlockGroup',
        getBlockGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($1.BlockGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.BlockGroup, $1.BlockGroup>(
        'UpdateBlockGroup',
        updateBlockGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.BlockGroup.fromBuffer(value),
        ($1.BlockGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $1.Edge>(
        'GetEdge',
        getEdge_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($1.Edge value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Edge, $1.Edge>(
        'UpdateEdge',
        updateEdge_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Edge.fromBuffer(value),
        ($1.Edge value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $1.Junction>(
        'GetJunction',
        getJunction_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($1.Junction value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Junction, $1.Junction>(
        'UpdateJunction',
        updateJunction_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Junction.fromBuffer(value),
        ($1.Junction value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $1.Output>(
        'GetOutput',
        getOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($1.Output value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Output, $1.Output>(
        'UpdateOutput',
        updateOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Output.fromBuffer(value),
        ($1.Output value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $1.Sensor>(
        'GetSensor',
        getSensor_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($1.Sensor value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Sensor, $1.Sensor>(
        'UpdateSensor',
        updateSensor_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Sensor.fromBuffer(value),
        ($1.Sensor value) => value.writeToBuffer()));
  }

  $async.Future<$0.ParseAddressResult> parseAddress_Pre($grpc.ServiceCall call,
      $async.Future<$0.ParseAddressRequest> request) async {
    return parseAddress(call, await request);
  }

  $async.Future<$1.Railway> getRailway_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Empty> request) async {
    return getRailway(call, await request);
  }

  $async.Future<$1.Railway> updateRailway_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Railway> request) async {
    return updateRailway(call, await request);
  }

  $async.Future<$1.Empty> save_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Empty> request) async {
    return save(call, await request);
  }

  $async.Future<$1.Module> getModule_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getModule(call, await request);
  }

  $async.Future<$1.Image> getModuleBackgroundImage_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getModuleBackgroundImage(call, await request);
  }

  $async.Future<$1.Module> updateModule_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Module> request) async {
    return updateModule(call, await request);
  }

  $async.Future<$1.Loc> getLoc_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getLoc(call, await request);
  }

  $async.Future<$1.Loc> updateLoc_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Loc> request) async {
    return updateLoc(call, await request);
  }

  $async.Future<$1.LocGroup> getLocGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getLocGroup(call, await request);
  }

  $async.Future<$1.LocGroup> updateLocGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$1.LocGroup> request) async {
    return updateLocGroup(call, await request);
  }

  $async.Future<$1.CommandStation> getCommandStation_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getCommandStation(call, await request);
  }

  $async.Future<$1.CommandStation> updateCommandStation_Pre(
      $grpc.ServiceCall call, $async.Future<$1.CommandStation> request) async {
    return updateCommandStation(call, await request);
  }

  $async.Future<$1.Block> getBlock_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getBlock(call, await request);
  }

  $async.Future<$1.Block> updateBlock_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Block> request) async {
    return updateBlock(call, await request);
  }

  $async.Future<$1.BlockGroup> getBlockGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getBlockGroup(call, await request);
  }

  $async.Future<$1.BlockGroup> updateBlockGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$1.BlockGroup> request) async {
    return updateBlockGroup(call, await request);
  }

  $async.Future<$1.Edge> getEdge_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getEdge(call, await request);
  }

  $async.Future<$1.Edge> updateEdge_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Edge> request) async {
    return updateEdge(call, await request);
  }

  $async.Future<$1.Junction> getJunction_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getJunction(call, await request);
  }

  $async.Future<$1.Junction> updateJunction_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Junction> request) async {
    return updateJunction(call, await request);
  }

  $async.Future<$1.Output> getOutput_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getOutput(call, await request);
  }

  $async.Future<$1.Output> updateOutput_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Output> request) async {
    return updateOutput(call, await request);
  }

  $async.Future<$1.Sensor> getSensor_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getSensor(call, await request);
  }

  $async.Future<$1.Sensor> updateSensor_Pre(
      $grpc.ServiceCall call, $async.Future<$1.Sensor> request) async {
    return updateSensor(call, await request);
  }

  $async.Future<$0.ParseAddressResult> parseAddress(
      $grpc.ServiceCall call, $0.ParseAddressRequest request);
  $async.Future<$1.Railway> getRailway(
      $grpc.ServiceCall call, $1.Empty request);
  $async.Future<$1.Railway> updateRailway(
      $grpc.ServiceCall call, $1.Railway request);
  $async.Future<$1.Empty> save($grpc.ServiceCall call, $1.Empty request);
  $async.Future<$1.Module> getModule(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$1.Image> getModuleBackgroundImage(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$1.Module> updateModule(
      $grpc.ServiceCall call, $1.Module request);
  $async.Future<$1.Loc> getLoc($grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$1.Loc> updateLoc($grpc.ServiceCall call, $1.Loc request);
  $async.Future<$1.LocGroup> getLocGroup(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$1.LocGroup> updateLocGroup(
      $grpc.ServiceCall call, $1.LocGroup request);
  $async.Future<$1.CommandStation> getCommandStation(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$1.CommandStation> updateCommandStation(
      $grpc.ServiceCall call, $1.CommandStation request);
  $async.Future<$1.Block> getBlock(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$1.Block> updateBlock($grpc.ServiceCall call, $1.Block request);
  $async.Future<$1.BlockGroup> getBlockGroup(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$1.BlockGroup> updateBlockGroup(
      $grpc.ServiceCall call, $1.BlockGroup request);
  $async.Future<$1.Edge> getEdge($grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$1.Edge> updateEdge($grpc.ServiceCall call, $1.Edge request);
  $async.Future<$1.Junction> getJunction(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$1.Junction> updateJunction(
      $grpc.ServiceCall call, $1.Junction request);
  $async.Future<$1.Output> getOutput(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$1.Output> updateOutput(
      $grpc.ServiceCall call, $1.Output request);
  $async.Future<$1.Sensor> getSensor(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$1.Sensor> updateSensor(
      $grpc.ServiceCall call, $1.Sensor request);
}
