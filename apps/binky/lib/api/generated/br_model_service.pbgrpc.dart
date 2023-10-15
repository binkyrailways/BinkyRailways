///
//  Generated code. Do not modify.
//  source: br_model_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

import 'dart:async' as $async;

import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'br_model_service.pb.dart' as $0;
import 'br_storage_types.pb.dart' as $1;
import 'br_model_types.pb.dart' as $2;
export 'br_model_service.pb.dart';

class ModelServiceClient extends $grpc.Client {
  static final _$parseAddress =
      $grpc.ClientMethod<$0.ParseAddressRequest, $0.ParseAddressResult>(
          '/binkyrailways.v1.ModelService/ParseAddress',
          ($0.ParseAddressRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.ParseAddressResult.fromBuffer(value));
  static final _$parsePermission =
      $grpc.ClientMethod<$0.ParsePermissionRequest, $0.ParsePermissionResult>(
          '/binkyrailways.v1.ModelService/ParsePermission',
          ($0.ParsePermissionRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.ParsePermissionResult.fromBuffer(value));
  static final _$loadRailway = $grpc.ClientMethod<$1.RailwayEntry, $2.Railway>(
      '/binkyrailways.v1.ModelService/LoadRailway',
      ($1.RailwayEntry value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Railway.fromBuffer(value));
  static final _$closeRailway = $grpc.ClientMethod<$2.Empty, $2.Empty>(
      '/binkyrailways.v1.ModelService/CloseRailway',
      ($2.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Empty.fromBuffer(value));
  static final _$getRailwayEntry =
      $grpc.ClientMethod<$2.Empty, $1.RailwayEntry>(
          '/binkyrailways.v1.ModelService/GetRailwayEntry',
          ($2.Empty value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $1.RailwayEntry.fromBuffer(value));
  static final _$getRailway = $grpc.ClientMethod<$2.Empty, $2.Railway>(
      '/binkyrailways.v1.ModelService/GetRailway',
      ($2.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Railway.fromBuffer(value));
  static final _$updateRailway = $grpc.ClientMethod<$2.Railway, $2.Railway>(
      '/binkyrailways.v1.ModelService/UpdateRailway',
      ($2.Railway value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Railway.fromBuffer(value));
  static final _$save = $grpc.ClientMethod<$2.Empty, $2.Empty>(
      '/binkyrailways.v1.ModelService/Save',
      ($2.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Empty.fromBuffer(value));
  static final _$getModule = $grpc.ClientMethod<$0.IDRequest, $2.Module>(
      '/binkyrailways.v1.ModelService/GetModule',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Module.fromBuffer(value));
  static final _$getModuleBackgroundImage =
      $grpc.ClientMethod<$0.IDRequest, $2.Image>(
          '/binkyrailways.v1.ModelService/GetModuleBackgroundImage',
          ($0.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.Image.fromBuffer(value));
  static final _$updateModule = $grpc.ClientMethod<$2.Module, $2.Module>(
      '/binkyrailways.v1.ModelService/UpdateModule',
      ($2.Module value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Module.fromBuffer(value));
  static final _$updateModuleBackgroundImage =
      $grpc.ClientMethod<$0.ImageIDRequest, $2.Module>(
          '/binkyrailways.v1.ModelService/UpdateModuleBackgroundImage',
          ($0.ImageIDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.Module.fromBuffer(value));
  static final _$addModule = $grpc.ClientMethod<$2.Empty, $2.Module>(
      '/binkyrailways.v1.ModelService/AddModule',
      ($2.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Module.fromBuffer(value));
  static final _$deleteModule = $grpc.ClientMethod<$0.IDRequest, $2.Empty>(
      '/binkyrailways.v1.ModelService/DeleteModule',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Empty.fromBuffer(value));
  static final _$getLoc = $grpc.ClientMethod<$0.IDRequest, $2.Loc>(
      '/binkyrailways.v1.ModelService/GetLoc',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Loc.fromBuffer(value));
  static final _$updateLoc = $grpc.ClientMethod<$2.Loc, $2.Loc>(
      '/binkyrailways.v1.ModelService/UpdateLoc',
      ($2.Loc value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Loc.fromBuffer(value));
  static final _$updateLocImage = $grpc.ClientMethod<$0.ImageIDRequest, $2.Loc>(
      '/binkyrailways.v1.ModelService/UpdateLocImage',
      ($0.ImageIDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Loc.fromBuffer(value));
  static final _$addLoc = $grpc.ClientMethod<$2.Empty, $2.Loc>(
      '/binkyrailways.v1.ModelService/AddLoc',
      ($2.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Loc.fromBuffer(value));
  static final _$deleteLoc = $grpc.ClientMethod<$0.IDRequest, $2.Empty>(
      '/binkyrailways.v1.ModelService/DeleteLoc',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Empty.fromBuffer(value));
  static final _$getLocGroup = $grpc.ClientMethod<$0.IDRequest, $2.LocGroup>(
      '/binkyrailways.v1.ModelService/GetLocGroup',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.LocGroup.fromBuffer(value));
  static final _$updateLocGroup = $grpc.ClientMethod<$2.LocGroup, $2.LocGroup>(
      '/binkyrailways.v1.ModelService/UpdateLocGroup',
      ($2.LocGroup value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.LocGroup.fromBuffer(value));
  static final _$addLocGroup = $grpc.ClientMethod<$2.Empty, $2.LocGroup>(
      '/binkyrailways.v1.ModelService/AddLocGroup',
      ($2.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.LocGroup.fromBuffer(value));
  static final _$deleteLocGroup = $grpc.ClientMethod<$0.IDRequest, $2.Empty>(
      '/binkyrailways.v1.ModelService/DeleteLocGroup',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Empty.fromBuffer(value));
  static final _$getCommandStation =
      $grpc.ClientMethod<$0.IDRequest, $2.CommandStation>(
          '/binkyrailways.v1.ModelService/GetCommandStation',
          ($0.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.CommandStation.fromBuffer(value));
  static final _$updateCommandStation =
      $grpc.ClientMethod<$2.CommandStation, $2.CommandStation>(
          '/binkyrailways.v1.ModelService/UpdateCommandStation',
          ($2.CommandStation value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.CommandStation.fromBuffer(value));
  static final _$addBidibCommandStation =
      $grpc.ClientMethod<$2.Empty, $2.CommandStation>(
          '/binkyrailways.v1.ModelService/AddBidibCommandStation',
          ($2.Empty value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.CommandStation.fromBuffer(value));
  static final _$addBinkyNetCommandStation =
      $grpc.ClientMethod<$2.Empty, $2.CommandStation>(
          '/binkyrailways.v1.ModelService/AddBinkyNetCommandStation',
          ($2.Empty value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.CommandStation.fromBuffer(value));
  static final _$getBlock = $grpc.ClientMethod<$0.IDRequest, $2.Block>(
      '/binkyrailways.v1.ModelService/GetBlock',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Block.fromBuffer(value));
  static final _$updateBlock = $grpc.ClientMethod<$2.Block, $2.Block>(
      '/binkyrailways.v1.ModelService/UpdateBlock',
      ($2.Block value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Block.fromBuffer(value));
  static final _$addBlock = $grpc.ClientMethod<$0.IDRequest, $2.Block>(
      '/binkyrailways.v1.ModelService/AddBlock',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Block.fromBuffer(value));
  static final _$deleteBlock = $grpc.ClientMethod<$0.IDRequest, $2.Module>(
      '/binkyrailways.v1.ModelService/DeleteBlock',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Module.fromBuffer(value));
  static final _$getBlockGroup =
      $grpc.ClientMethod<$0.IDRequest, $2.BlockGroup>(
          '/binkyrailways.v1.ModelService/GetBlockGroup',
          ($0.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.BlockGroup.fromBuffer(value));
  static final _$updateBlockGroup =
      $grpc.ClientMethod<$2.BlockGroup, $2.BlockGroup>(
          '/binkyrailways.v1.ModelService/UpdateBlockGroup',
          ($2.BlockGroup value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.BlockGroup.fromBuffer(value));
  static final _$addBlockGroup =
      $grpc.ClientMethod<$0.IDRequest, $2.BlockGroup>(
          '/binkyrailways.v1.ModelService/AddBlockGroup',
          ($0.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.BlockGroup.fromBuffer(value));
  static final _$deleteBlockGroup = $grpc.ClientMethod<$0.IDRequest, $2.Module>(
      '/binkyrailways.v1.ModelService/DeleteBlockGroup',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Module.fromBuffer(value));
  static final _$getEdge = $grpc.ClientMethod<$0.IDRequest, $2.Edge>(
      '/binkyrailways.v1.ModelService/GetEdge',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Edge.fromBuffer(value));
  static final _$updateEdge = $grpc.ClientMethod<$2.Edge, $2.Edge>(
      '/binkyrailways.v1.ModelService/UpdateEdge',
      ($2.Edge value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Edge.fromBuffer(value));
  static final _$addEdge = $grpc.ClientMethod<$0.IDRequest, $2.Edge>(
      '/binkyrailways.v1.ModelService/AddEdge',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Edge.fromBuffer(value));
  static final _$deleteEdge = $grpc.ClientMethod<$0.IDRequest, $2.Module>(
      '/binkyrailways.v1.ModelService/DeleteEdge',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Module.fromBuffer(value));
  static final _$getJunction = $grpc.ClientMethod<$0.IDRequest, $2.Junction>(
      '/binkyrailways.v1.ModelService/GetJunction',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Junction.fromBuffer(value));
  static final _$updateJunction = $grpc.ClientMethod<$2.Junction, $2.Junction>(
      '/binkyrailways.v1.ModelService/UpdateJunction',
      ($2.Junction value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Junction.fromBuffer(value));
  static final _$addSwitch = $grpc.ClientMethod<$0.IDRequest, $2.Junction>(
      '/binkyrailways.v1.ModelService/AddSwitch',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Junction.fromBuffer(value));
  static final _$deleteJunction = $grpc.ClientMethod<$0.IDRequest, $2.Module>(
      '/binkyrailways.v1.ModelService/DeleteJunction',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Module.fromBuffer(value));
  static final _$getOutput = $grpc.ClientMethod<$0.IDRequest, $2.Output>(
      '/binkyrailways.v1.ModelService/GetOutput',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Output.fromBuffer(value));
  static final _$updateOutput = $grpc.ClientMethod<$2.Output, $2.Output>(
      '/binkyrailways.v1.ModelService/UpdateOutput',
      ($2.Output value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Output.fromBuffer(value));
  static final _$addBinaryOutput = $grpc.ClientMethod<$0.IDRequest, $2.Output>(
      '/binkyrailways.v1.ModelService/AddBinaryOutput',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Output.fromBuffer(value));
  static final _$deleteOutput = $grpc.ClientMethod<$0.IDRequest, $2.Module>(
      '/binkyrailways.v1.ModelService/DeleteOutput',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Module.fromBuffer(value));
  static final _$getRoute = $grpc.ClientMethod<$0.IDRequest, $2.Route>(
      '/binkyrailways.v1.ModelService/GetRoute',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Route.fromBuffer(value));
  static final _$updateRoute = $grpc.ClientMethod<$2.Route, $2.Route>(
      '/binkyrailways.v1.ModelService/UpdateRoute',
      ($2.Route value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Route.fromBuffer(value));
  static final _$addRoute = $grpc.ClientMethod<$0.IDRequest, $2.Route>(
      '/binkyrailways.v1.ModelService/AddRoute',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Route.fromBuffer(value));
  static final _$deleteRoute = $grpc.ClientMethod<$0.IDRequest, $2.Module>(
      '/binkyrailways.v1.ModelService/DeleteRoute',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Module.fromBuffer(value));
  static final _$addRouteCrossingJunctionSwitch =
      $grpc.ClientMethod<$0.AddRouteCrossingJunctionSwitchRequest, $2.Route>(
          '/binkyrailways.v1.ModelService/AddRouteCrossingJunctionSwitch',
          ($0.AddRouteCrossingJunctionSwitchRequest value) =>
              value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.Route.fromBuffer(value));
  static final _$removeRouteCrossingJunction =
      $grpc.ClientMethod<$0.RemoveRouteCrossingJunctionRequest, $2.Route>(
          '/binkyrailways.v1.ModelService/RemoveRouteCrossingJunction',
          ($0.RemoveRouteCrossingJunctionRequest value) =>
              value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.Route.fromBuffer(value));
  static final _$addRouteBinaryOutput =
      $grpc.ClientMethod<$0.AddRouteBinaryOutputRequest, $2.Route>(
          '/binkyrailways.v1.ModelService/AddRouteBinaryOutput',
          ($0.AddRouteBinaryOutputRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.Route.fromBuffer(value));
  static final _$removeRouteOutput =
      $grpc.ClientMethod<$0.RemoveRouteOutputRequest, $2.Route>(
          '/binkyrailways.v1.ModelService/RemoveRouteOutput',
          ($0.RemoveRouteOutputRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.Route.fromBuffer(value));
  static final _$addRouteEvent =
      $grpc.ClientMethod<$0.AddRouteEventRequest, $2.Route>(
          '/binkyrailways.v1.ModelService/AddRouteEvent',
          ($0.AddRouteEventRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.Route.fromBuffer(value));
  static final _$removeRouteEvent =
      $grpc.ClientMethod<$0.RemoveRouteEventRequest, $2.Route>(
          '/binkyrailways.v1.ModelService/RemoveRouteEvent',
          ($0.RemoveRouteEventRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.Route.fromBuffer(value));
  static final _$addRouteEventBehavior =
      $grpc.ClientMethod<$0.AddRouteEventBehaviorRequest, $2.Route>(
          '/binkyrailways.v1.ModelService/AddRouteEventBehavior',
          ($0.AddRouteEventBehaviorRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.Route.fromBuffer(value));
  static final _$removeRouteEventBehavior =
      $grpc.ClientMethod<$0.RemoveRouteEventBehaviorRequest, $2.Route>(
          '/binkyrailways.v1.ModelService/RemoveRouteEventBehavior',
          ($0.RemoveRouteEventBehaviorRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.Route.fromBuffer(value));
  static final _$getSensor = $grpc.ClientMethod<$0.IDRequest, $2.Sensor>(
      '/binkyrailways.v1.ModelService/GetSensor',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Sensor.fromBuffer(value));
  static final _$updateSensor = $grpc.ClientMethod<$2.Sensor, $2.Sensor>(
      '/binkyrailways.v1.ModelService/UpdateSensor',
      ($2.Sensor value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Sensor.fromBuffer(value));
  static final _$addBinarySensor = $grpc.ClientMethod<$0.IDRequest, $2.Sensor>(
      '/binkyrailways.v1.ModelService/AddBinarySensor',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Sensor.fromBuffer(value));
  static final _$deleteSensor = $grpc.ClientMethod<$0.IDRequest, $2.Module>(
      '/binkyrailways.v1.ModelService/DeleteSensor',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Module.fromBuffer(value));
  static final _$getSignal = $grpc.ClientMethod<$0.IDRequest, $2.Signal>(
      '/binkyrailways.v1.ModelService/GetSignal',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Signal.fromBuffer(value));
  static final _$updateSignal = $grpc.ClientMethod<$2.Signal, $2.Signal>(
      '/binkyrailways.v1.ModelService/UpdateSignal',
      ($2.Signal value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Signal.fromBuffer(value));
  static final _$deleteSignal = $grpc.ClientMethod<$0.IDRequest, $2.Module>(
      '/binkyrailways.v1.ModelService/DeleteSignal',
      ($0.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $2.Module.fromBuffer(value));
  static final _$getBinkyNetLocalWorker =
      $grpc.ClientMethod<$0.IDRequest, $2.BinkyNetLocalWorker>(
          '/binkyrailways.v1.ModelService/GetBinkyNetLocalWorker',
          ($0.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $2.BinkyNetLocalWorker.fromBuffer(value));
  static final _$updateBinkyNetLocalWorker =
      $grpc.ClientMethod<$2.BinkyNetLocalWorker, $2.BinkyNetLocalWorker>(
          '/binkyrailways.v1.ModelService/UpdateBinkyNetLocalWorker',
          ($2.BinkyNetLocalWorker value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $2.BinkyNetLocalWorker.fromBuffer(value));
  static final _$deleteBinkyNetLocalWorker =
      $grpc.ClientMethod<$0.IDRequest, $2.CommandStation>(
          '/binkyrailways.v1.ModelService/DeleteBinkyNetLocalWorker',
          ($0.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.CommandStation.fromBuffer(value));
  static final _$addBinkyNetLocalWorker =
      $grpc.ClientMethod<$0.IDRequest, $2.BinkyNetLocalWorker>(
          '/binkyrailways.v1.ModelService/AddBinkyNetLocalWorker',
          ($0.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $2.BinkyNetLocalWorker.fromBuffer(value));
  static final _$addBinkyNetDevice =
      $grpc.ClientMethod<$0.IDRequest, $2.BinkyNetDevice>(
          '/binkyrailways.v1.ModelService/AddBinkyNetDevice',
          ($0.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.BinkyNetDevice.fromBuffer(value));
  static final _$deleteBinkyNetDevice =
      $grpc.ClientMethod<$0.SubIDRequest, $2.BinkyNetLocalWorker>(
          '/binkyrailways.v1.ModelService/DeleteBinkyNetDevice',
          ($0.SubIDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $2.BinkyNetLocalWorker.fromBuffer(value));
  static final _$addBinkyNetObject =
      $grpc.ClientMethod<$0.IDRequest, $2.BinkyNetObject>(
          '/binkyrailways.v1.ModelService/AddBinkyNetObject',
          ($0.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.BinkyNetObject.fromBuffer(value));
  static final _$deleteBinkyNetObject =
      $grpc.ClientMethod<$0.SubIDRequest, $2.BinkyNetLocalWorker>(
          '/binkyrailways.v1.ModelService/DeleteBinkyNetObject',
          ($0.SubIDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $2.BinkyNetLocalWorker.fromBuffer(value));
  static final _$addBinkyNetObjectsGroup = $grpc.ClientMethod<
          $0.AddBinkyNetObjectsGroupRequest, $2.BinkyNetLocalWorker>(
      '/binkyrailways.v1.ModelService/AddBinkyNetObjectsGroup',
      ($0.AddBinkyNetObjectsGroupRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) =>
          $2.BinkyNetLocalWorker.fromBuffer(value));

  ModelServiceClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options, interceptors: interceptors);

  $grpc.ResponseFuture<$0.ParseAddressResult> parseAddress(
      $0.ParseAddressRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$parseAddress, request, options: options);
  }

  $grpc.ResponseFuture<$0.ParsePermissionResult> parsePermission(
      $0.ParsePermissionRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$parsePermission, request, options: options);
  }

  $grpc.ResponseFuture<$2.Railway> loadRailway($1.RailwayEntry request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$loadRailway, request, options: options);
  }

  $grpc.ResponseFuture<$2.Empty> closeRailway($2.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$closeRailway, request, options: options);
  }

  $grpc.ResponseFuture<$1.RailwayEntry> getRailwayEntry($2.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getRailwayEntry, request, options: options);
  }

  $grpc.ResponseFuture<$2.Railway> getRailway($2.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getRailway, request, options: options);
  }

  $grpc.ResponseFuture<$2.Railway> updateRailway($2.Railway request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateRailway, request, options: options);
  }

  $grpc.ResponseFuture<$2.Empty> save($2.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$save, request, options: options);
  }

  $grpc.ResponseFuture<$2.Module> getModule($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getModule, request, options: options);
  }

  $grpc.ResponseFuture<$2.Image> getModuleBackgroundImage($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getModuleBackgroundImage, request,
        options: options);
  }

  $grpc.ResponseFuture<$2.Module> updateModule($2.Module request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateModule, request, options: options);
  }

  $grpc.ResponseFuture<$2.Module> updateModuleBackgroundImage(
      $0.ImageIDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateModuleBackgroundImage, request,
        options: options);
  }

  $grpc.ResponseFuture<$2.Module> addModule($2.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addModule, request, options: options);
  }

  $grpc.ResponseFuture<$2.Empty> deleteModule($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteModule, request, options: options);
  }

  $grpc.ResponseFuture<$2.Loc> getLoc($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getLoc, request, options: options);
  }

  $grpc.ResponseFuture<$2.Loc> updateLoc($2.Loc request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateLoc, request, options: options);
  }

  $grpc.ResponseFuture<$2.Loc> updateLocImage($0.ImageIDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateLocImage, request, options: options);
  }

  $grpc.ResponseFuture<$2.Loc> addLoc($2.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addLoc, request, options: options);
  }

  $grpc.ResponseFuture<$2.Empty> deleteLoc($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteLoc, request, options: options);
  }

  $grpc.ResponseFuture<$2.LocGroup> getLocGroup($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getLocGroup, request, options: options);
  }

  $grpc.ResponseFuture<$2.LocGroup> updateLocGroup($2.LocGroup request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateLocGroup, request, options: options);
  }

  $grpc.ResponseFuture<$2.LocGroup> addLocGroup($2.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addLocGroup, request, options: options);
  }

  $grpc.ResponseFuture<$2.Empty> deleteLocGroup($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteLocGroup, request, options: options);
  }

  $grpc.ResponseFuture<$2.CommandStation> getCommandStation(
      $0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getCommandStation, request, options: options);
  }

  $grpc.ResponseFuture<$2.CommandStation> updateCommandStation(
      $2.CommandStation request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateCommandStation, request, options: options);
  }

  $grpc.ResponseFuture<$2.CommandStation> addBidibCommandStation(
      $2.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBidibCommandStation, request,
        options: options);
  }

  $grpc.ResponseFuture<$2.CommandStation> addBinkyNetCommandStation(
      $2.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinkyNetCommandStation, request,
        options: options);
  }

  $grpc.ResponseFuture<$2.Block> getBlock($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getBlock, request, options: options);
  }

  $grpc.ResponseFuture<$2.Block> updateBlock($2.Block request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateBlock, request, options: options);
  }

  $grpc.ResponseFuture<$2.Block> addBlock($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBlock, request, options: options);
  }

  $grpc.ResponseFuture<$2.Module> deleteBlock($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteBlock, request, options: options);
  }

  $grpc.ResponseFuture<$2.BlockGroup> getBlockGroup($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getBlockGroup, request, options: options);
  }

  $grpc.ResponseFuture<$2.BlockGroup> updateBlockGroup($2.BlockGroup request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateBlockGroup, request, options: options);
  }

  $grpc.ResponseFuture<$2.BlockGroup> addBlockGroup($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBlockGroup, request, options: options);
  }

  $grpc.ResponseFuture<$2.Module> deleteBlockGroup($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteBlockGroup, request, options: options);
  }

  $grpc.ResponseFuture<$2.Edge> getEdge($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getEdge, request, options: options);
  }

  $grpc.ResponseFuture<$2.Edge> updateEdge($2.Edge request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateEdge, request, options: options);
  }

  $grpc.ResponseFuture<$2.Edge> addEdge($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addEdge, request, options: options);
  }

  $grpc.ResponseFuture<$2.Module> deleteEdge($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteEdge, request, options: options);
  }

  $grpc.ResponseFuture<$2.Junction> getJunction($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getJunction, request, options: options);
  }

  $grpc.ResponseFuture<$2.Junction> updateJunction($2.Junction request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateJunction, request, options: options);
  }

  $grpc.ResponseFuture<$2.Junction> addSwitch($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addSwitch, request, options: options);
  }

  $grpc.ResponseFuture<$2.Module> deleteJunction($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteJunction, request, options: options);
  }

  $grpc.ResponseFuture<$2.Output> getOutput($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getOutput, request, options: options);
  }

  $grpc.ResponseFuture<$2.Output> updateOutput($2.Output request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateOutput, request, options: options);
  }

  $grpc.ResponseFuture<$2.Output> addBinaryOutput($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinaryOutput, request, options: options);
  }

  $grpc.ResponseFuture<$2.Module> deleteOutput($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteOutput, request, options: options);
  }

  $grpc.ResponseFuture<$2.Route> getRoute($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getRoute, request, options: options);
  }

  $grpc.ResponseFuture<$2.Route> updateRoute($2.Route request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateRoute, request, options: options);
  }

  $grpc.ResponseFuture<$2.Route> addRoute($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addRoute, request, options: options);
  }

  $grpc.ResponseFuture<$2.Module> deleteRoute($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteRoute, request, options: options);
  }

  $grpc.ResponseFuture<$2.Route> addRouteCrossingJunctionSwitch(
      $0.AddRouteCrossingJunctionSwitchRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addRouteCrossingJunctionSwitch, request,
        options: options);
  }

  $grpc.ResponseFuture<$2.Route> removeRouteCrossingJunction(
      $0.RemoveRouteCrossingJunctionRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$removeRouteCrossingJunction, request,
        options: options);
  }

  $grpc.ResponseFuture<$2.Route> addRouteBinaryOutput(
      $0.AddRouteBinaryOutputRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addRouteBinaryOutput, request, options: options);
  }

  $grpc.ResponseFuture<$2.Route> removeRouteOutput(
      $0.RemoveRouteOutputRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$removeRouteOutput, request, options: options);
  }

  $grpc.ResponseFuture<$2.Route> addRouteEvent($0.AddRouteEventRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addRouteEvent, request, options: options);
  }

  $grpc.ResponseFuture<$2.Route> removeRouteEvent(
      $0.RemoveRouteEventRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$removeRouteEvent, request, options: options);
  }

  $grpc.ResponseFuture<$2.Route> addRouteEventBehavior(
      $0.AddRouteEventBehaviorRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addRouteEventBehavior, request, options: options);
  }

  $grpc.ResponseFuture<$2.Route> removeRouteEventBehavior(
      $0.RemoveRouteEventBehaviorRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$removeRouteEventBehavior, request,
        options: options);
  }

  $grpc.ResponseFuture<$2.Sensor> getSensor($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getSensor, request, options: options);
  }

  $grpc.ResponseFuture<$2.Sensor> updateSensor($2.Sensor request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateSensor, request, options: options);
  }

  $grpc.ResponseFuture<$2.Sensor> addBinarySensor($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinarySensor, request, options: options);
  }

  $grpc.ResponseFuture<$2.Module> deleteSensor($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteSensor, request, options: options);
  }

  $grpc.ResponseFuture<$2.Signal> getSignal($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getSignal, request, options: options);
  }

  $grpc.ResponseFuture<$2.Signal> updateSignal($2.Signal request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateSignal, request, options: options);
  }

  $grpc.ResponseFuture<$2.Module> deleteSignal($0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteSignal, request, options: options);
  }

  $grpc.ResponseFuture<$2.BinkyNetLocalWorker> getBinkyNetLocalWorker(
      $0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getBinkyNetLocalWorker, request,
        options: options);
  }

  $grpc.ResponseFuture<$2.BinkyNetLocalWorker> updateBinkyNetLocalWorker(
      $2.BinkyNetLocalWorker request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateBinkyNetLocalWorker, request,
        options: options);
  }

  $grpc.ResponseFuture<$2.CommandStation> deleteBinkyNetLocalWorker(
      $0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteBinkyNetLocalWorker, request,
        options: options);
  }

  $grpc.ResponseFuture<$2.BinkyNetLocalWorker> addBinkyNetLocalWorker(
      $0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinkyNetLocalWorker, request,
        options: options);
  }

  $grpc.ResponseFuture<$2.BinkyNetDevice> addBinkyNetDevice(
      $0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinkyNetDevice, request, options: options);
  }

  $grpc.ResponseFuture<$2.BinkyNetLocalWorker> deleteBinkyNetDevice(
      $0.SubIDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteBinkyNetDevice, request, options: options);
  }

  $grpc.ResponseFuture<$2.BinkyNetObject> addBinkyNetObject(
      $0.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinkyNetObject, request, options: options);
  }

  $grpc.ResponseFuture<$2.BinkyNetLocalWorker> deleteBinkyNetObject(
      $0.SubIDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteBinkyNetObject, request, options: options);
  }

  $grpc.ResponseFuture<$2.BinkyNetLocalWorker> addBinkyNetObjectsGroup(
      $0.AddBinkyNetObjectsGroupRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinkyNetObjectsGroup, request,
        options: options);
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
    $addMethod($grpc.ServiceMethod<$0.ParsePermissionRequest,
            $0.ParsePermissionResult>(
        'ParsePermission',
        parsePermission_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $0.ParsePermissionRequest.fromBuffer(value),
        ($0.ParsePermissionResult value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.RailwayEntry, $2.Railway>(
        'LoadRailway',
        loadRailway_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.RailwayEntry.fromBuffer(value),
        ($2.Railway value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Empty, $2.Empty>(
        'CloseRailway',
        closeRailway_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Empty.fromBuffer(value),
        ($2.Empty value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Empty, $1.RailwayEntry>(
        'GetRailwayEntry',
        getRailwayEntry_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Empty.fromBuffer(value),
        ($1.RailwayEntry value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Empty, $2.Railway>(
        'GetRailway',
        getRailway_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Empty.fromBuffer(value),
        ($2.Railway value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Railway, $2.Railway>(
        'UpdateRailway',
        updateRailway_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Railway.fromBuffer(value),
        ($2.Railway value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Empty, $2.Empty>(
        'Save',
        save_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Empty.fromBuffer(value),
        ($2.Empty value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Module>(
        'GetModule',
        getModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Image>(
        'GetModuleBackgroundImage',
        getModuleBackgroundImage_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Image value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Module, $2.Module>(
        'UpdateModule',
        updateModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Module.fromBuffer(value),
        ($2.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.ImageIDRequest, $2.Module>(
        'UpdateModuleBackgroundImage',
        updateModuleBackgroundImage_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.ImageIDRequest.fromBuffer(value),
        ($2.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Empty, $2.Module>(
        'AddModule',
        addModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Empty.fromBuffer(value),
        ($2.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Empty>(
        'DeleteModule',
        deleteModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Empty value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Loc>(
        'GetLoc',
        getLoc_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Loc value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Loc, $2.Loc>(
        'UpdateLoc',
        updateLoc_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Loc.fromBuffer(value),
        ($2.Loc value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.ImageIDRequest, $2.Loc>(
        'UpdateLocImage',
        updateLocImage_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.ImageIDRequest.fromBuffer(value),
        ($2.Loc value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Empty, $2.Loc>(
        'AddLoc',
        addLoc_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Empty.fromBuffer(value),
        ($2.Loc value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Empty>(
        'DeleteLoc',
        deleteLoc_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Empty value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.LocGroup>(
        'GetLocGroup',
        getLocGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.LocGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.LocGroup, $2.LocGroup>(
        'UpdateLocGroup',
        updateLocGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.LocGroup.fromBuffer(value),
        ($2.LocGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Empty, $2.LocGroup>(
        'AddLocGroup',
        addLocGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Empty.fromBuffer(value),
        ($2.LocGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Empty>(
        'DeleteLocGroup',
        deleteLocGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Empty value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.CommandStation>(
        'GetCommandStation',
        getCommandStation_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.CommandStation value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.CommandStation, $2.CommandStation>(
        'UpdateCommandStation',
        updateCommandStation_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.CommandStation.fromBuffer(value),
        ($2.CommandStation value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Empty, $2.CommandStation>(
        'AddBidibCommandStation',
        addBidibCommandStation_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Empty.fromBuffer(value),
        ($2.CommandStation value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Empty, $2.CommandStation>(
        'AddBinkyNetCommandStation',
        addBinkyNetCommandStation_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Empty.fromBuffer(value),
        ($2.CommandStation value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Block>(
        'GetBlock',
        getBlock_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Block value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Block, $2.Block>(
        'UpdateBlock',
        updateBlock_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Block.fromBuffer(value),
        ($2.Block value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Block>(
        'AddBlock',
        addBlock_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Block value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Module>(
        'DeleteBlock',
        deleteBlock_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.BlockGroup>(
        'GetBlockGroup',
        getBlockGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.BlockGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.BlockGroup, $2.BlockGroup>(
        'UpdateBlockGroup',
        updateBlockGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.BlockGroup.fromBuffer(value),
        ($2.BlockGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.BlockGroup>(
        'AddBlockGroup',
        addBlockGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.BlockGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Module>(
        'DeleteBlockGroup',
        deleteBlockGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Edge>(
        'GetEdge',
        getEdge_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Edge value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Edge, $2.Edge>(
        'UpdateEdge',
        updateEdge_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Edge.fromBuffer(value),
        ($2.Edge value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Edge>(
        'AddEdge',
        addEdge_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Edge value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Module>(
        'DeleteEdge',
        deleteEdge_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Junction>(
        'GetJunction',
        getJunction_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Junction value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Junction, $2.Junction>(
        'UpdateJunction',
        updateJunction_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Junction.fromBuffer(value),
        ($2.Junction value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Junction>(
        'AddSwitch',
        addSwitch_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Junction value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Module>(
        'DeleteJunction',
        deleteJunction_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Output>(
        'GetOutput',
        getOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Output value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Output, $2.Output>(
        'UpdateOutput',
        updateOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Output.fromBuffer(value),
        ($2.Output value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Output>(
        'AddBinaryOutput',
        addBinaryOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Output value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Module>(
        'DeleteOutput',
        deleteOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Route>(
        'GetRoute',
        getRoute_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Route, $2.Route>(
        'UpdateRoute',
        updateRoute_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Route.fromBuffer(value),
        ($2.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Route>(
        'AddRoute',
        addRoute_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Module>(
        'DeleteRoute',
        deleteRoute_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Module value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$0.AddRouteCrossingJunctionSwitchRequest, $2.Route>(
            'AddRouteCrossingJunctionSwitch',
            addRouteCrossingJunctionSwitch_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $0.AddRouteCrossingJunctionSwitchRequest.fromBuffer(value),
            ($2.Route value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$0.RemoveRouteCrossingJunctionRequest, $2.Route>(
            'RemoveRouteCrossingJunction',
            removeRouteCrossingJunction_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $0.RemoveRouteCrossingJunctionRequest.fromBuffer(value),
            ($2.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.AddRouteBinaryOutputRequest, $2.Route>(
        'AddRouteBinaryOutput',
        addRouteBinaryOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $0.AddRouteBinaryOutputRequest.fromBuffer(value),
        ($2.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.RemoveRouteOutputRequest, $2.Route>(
        'RemoveRouteOutput',
        removeRouteOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $0.RemoveRouteOutputRequest.fromBuffer(value),
        ($2.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.AddRouteEventRequest, $2.Route>(
        'AddRouteEvent',
        addRouteEvent_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $0.AddRouteEventRequest.fromBuffer(value),
        ($2.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.RemoveRouteEventRequest, $2.Route>(
        'RemoveRouteEvent',
        removeRouteEvent_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $0.RemoveRouteEventRequest.fromBuffer(value),
        ($2.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.AddRouteEventBehaviorRequest, $2.Route>(
        'AddRouteEventBehavior',
        addRouteEventBehavior_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $0.AddRouteEventBehaviorRequest.fromBuffer(value),
        ($2.Route value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$0.RemoveRouteEventBehaviorRequest, $2.Route>(
            'RemoveRouteEventBehavior',
            removeRouteEventBehavior_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $0.RemoveRouteEventBehaviorRequest.fromBuffer(value),
            ($2.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Sensor>(
        'GetSensor',
        getSensor_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Sensor value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Sensor, $2.Sensor>(
        'UpdateSensor',
        updateSensor_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Sensor.fromBuffer(value),
        ($2.Sensor value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Sensor>(
        'AddBinarySensor',
        addBinarySensor_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Sensor value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Module>(
        'DeleteSensor',
        deleteSensor_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Signal>(
        'GetSignal',
        getSignal_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Signal value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.Signal, $2.Signal>(
        'UpdateSignal',
        updateSignal_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.Signal.fromBuffer(value),
        ($2.Signal value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.Module>(
        'DeleteSignal',
        deleteSignal_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.BinkyNetLocalWorker>(
        'GetBinkyNetLocalWorker',
        getBinkyNetLocalWorker_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.BinkyNetLocalWorker value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$2.BinkyNetLocalWorker, $2.BinkyNetLocalWorker>(
            'UpdateBinkyNetLocalWorker',
            updateBinkyNetLocalWorker_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $2.BinkyNetLocalWorker.fromBuffer(value),
            ($2.BinkyNetLocalWorker value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.CommandStation>(
        'DeleteBinkyNetLocalWorker',
        deleteBinkyNetLocalWorker_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.CommandStation value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.BinkyNetLocalWorker>(
        'AddBinkyNetLocalWorker',
        addBinkyNetLocalWorker_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.BinkyNetLocalWorker value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.BinkyNetDevice>(
        'AddBinkyNetDevice',
        addBinkyNetDevice_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.BinkyNetDevice value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.SubIDRequest, $2.BinkyNetLocalWorker>(
        'DeleteBinkyNetDevice',
        deleteBinkyNetDevice_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.SubIDRequest.fromBuffer(value),
        ($2.BinkyNetLocalWorker value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.IDRequest, $2.BinkyNetObject>(
        'AddBinkyNetObject',
        addBinkyNetObject_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.IDRequest.fromBuffer(value),
        ($2.BinkyNetObject value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.SubIDRequest, $2.BinkyNetLocalWorker>(
        'DeleteBinkyNetObject',
        deleteBinkyNetObject_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.SubIDRequest.fromBuffer(value),
        ($2.BinkyNetLocalWorker value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.AddBinkyNetObjectsGroupRequest,
            $2.BinkyNetLocalWorker>(
        'AddBinkyNetObjectsGroup',
        addBinkyNetObjectsGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $0.AddBinkyNetObjectsGroupRequest.fromBuffer(value),
        ($2.BinkyNetLocalWorker value) => value.writeToBuffer()));
  }

  $async.Future<$0.ParseAddressResult> parseAddress_Pre($grpc.ServiceCall call,
      $async.Future<$0.ParseAddressRequest> request) async {
    return parseAddress(call, await request);
  }

  $async.Future<$0.ParsePermissionResult> parsePermission_Pre(
      $grpc.ServiceCall call,
      $async.Future<$0.ParsePermissionRequest> request) async {
    return parsePermission(call, await request);
  }

  $async.Future<$2.Railway> loadRailway_Pre(
      $grpc.ServiceCall call, $async.Future<$1.RailwayEntry> request) async {
    return loadRailway(call, await request);
  }

  $async.Future<$2.Empty> closeRailway_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Empty> request) async {
    return closeRailway(call, await request);
  }

  $async.Future<$1.RailwayEntry> getRailwayEntry_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Empty> request) async {
    return getRailwayEntry(call, await request);
  }

  $async.Future<$2.Railway> getRailway_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Empty> request) async {
    return getRailway(call, await request);
  }

  $async.Future<$2.Railway> updateRailway_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Railway> request) async {
    return updateRailway(call, await request);
  }

  $async.Future<$2.Empty> save_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Empty> request) async {
    return save(call, await request);
  }

  $async.Future<$2.Module> getModule_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getModule(call, await request);
  }

  $async.Future<$2.Image> getModuleBackgroundImage_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getModuleBackgroundImage(call, await request);
  }

  $async.Future<$2.Module> updateModule_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Module> request) async {
    return updateModule(call, await request);
  }

  $async.Future<$2.Module> updateModuleBackgroundImage_Pre(
      $grpc.ServiceCall call, $async.Future<$0.ImageIDRequest> request) async {
    return updateModuleBackgroundImage(call, await request);
  }

  $async.Future<$2.Module> addModule_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Empty> request) async {
    return addModule(call, await request);
  }

  $async.Future<$2.Empty> deleteModule_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return deleteModule(call, await request);
  }

  $async.Future<$2.Loc> getLoc_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getLoc(call, await request);
  }

  $async.Future<$2.Loc> updateLoc_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Loc> request) async {
    return updateLoc(call, await request);
  }

  $async.Future<$2.Loc> updateLocImage_Pre(
      $grpc.ServiceCall call, $async.Future<$0.ImageIDRequest> request) async {
    return updateLocImage(call, await request);
  }

  $async.Future<$2.Loc> addLoc_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Empty> request) async {
    return addLoc(call, await request);
  }

  $async.Future<$2.Empty> deleteLoc_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return deleteLoc(call, await request);
  }

  $async.Future<$2.LocGroup> getLocGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getLocGroup(call, await request);
  }

  $async.Future<$2.LocGroup> updateLocGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$2.LocGroup> request) async {
    return updateLocGroup(call, await request);
  }

  $async.Future<$2.LocGroup> addLocGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Empty> request) async {
    return addLocGroup(call, await request);
  }

  $async.Future<$2.Empty> deleteLocGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return deleteLocGroup(call, await request);
  }

  $async.Future<$2.CommandStation> getCommandStation_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getCommandStation(call, await request);
  }

  $async.Future<$2.CommandStation> updateCommandStation_Pre(
      $grpc.ServiceCall call, $async.Future<$2.CommandStation> request) async {
    return updateCommandStation(call, await request);
  }

  $async.Future<$2.CommandStation> addBidibCommandStation_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Empty> request) async {
    return addBidibCommandStation(call, await request);
  }

  $async.Future<$2.CommandStation> addBinkyNetCommandStation_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Empty> request) async {
    return addBinkyNetCommandStation(call, await request);
  }

  $async.Future<$2.Block> getBlock_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getBlock(call, await request);
  }

  $async.Future<$2.Block> updateBlock_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Block> request) async {
    return updateBlock(call, await request);
  }

  $async.Future<$2.Block> addBlock_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return addBlock(call, await request);
  }

  $async.Future<$2.Module> deleteBlock_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return deleteBlock(call, await request);
  }

  $async.Future<$2.BlockGroup> getBlockGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getBlockGroup(call, await request);
  }

  $async.Future<$2.BlockGroup> updateBlockGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$2.BlockGroup> request) async {
    return updateBlockGroup(call, await request);
  }

  $async.Future<$2.BlockGroup> addBlockGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return addBlockGroup(call, await request);
  }

  $async.Future<$2.Module> deleteBlockGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return deleteBlockGroup(call, await request);
  }

  $async.Future<$2.Edge> getEdge_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getEdge(call, await request);
  }

  $async.Future<$2.Edge> updateEdge_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Edge> request) async {
    return updateEdge(call, await request);
  }

  $async.Future<$2.Edge> addEdge_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return addEdge(call, await request);
  }

  $async.Future<$2.Module> deleteEdge_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return deleteEdge(call, await request);
  }

  $async.Future<$2.Junction> getJunction_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getJunction(call, await request);
  }

  $async.Future<$2.Junction> updateJunction_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Junction> request) async {
    return updateJunction(call, await request);
  }

  $async.Future<$2.Junction> addSwitch_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return addSwitch(call, await request);
  }

  $async.Future<$2.Module> deleteJunction_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return deleteJunction(call, await request);
  }

  $async.Future<$2.Output> getOutput_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getOutput(call, await request);
  }

  $async.Future<$2.Output> updateOutput_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Output> request) async {
    return updateOutput(call, await request);
  }

  $async.Future<$2.Output> addBinaryOutput_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return addBinaryOutput(call, await request);
  }

  $async.Future<$2.Module> deleteOutput_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return deleteOutput(call, await request);
  }

  $async.Future<$2.Route> getRoute_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getRoute(call, await request);
  }

  $async.Future<$2.Route> updateRoute_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Route> request) async {
    return updateRoute(call, await request);
  }

  $async.Future<$2.Route> addRoute_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return addRoute(call, await request);
  }

  $async.Future<$2.Module> deleteRoute_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return deleteRoute(call, await request);
  }

  $async.Future<$2.Route> addRouteCrossingJunctionSwitch_Pre(
      $grpc.ServiceCall call,
      $async.Future<$0.AddRouteCrossingJunctionSwitchRequest> request) async {
    return addRouteCrossingJunctionSwitch(call, await request);
  }

  $async.Future<$2.Route> removeRouteCrossingJunction_Pre(
      $grpc.ServiceCall call,
      $async.Future<$0.RemoveRouteCrossingJunctionRequest> request) async {
    return removeRouteCrossingJunction(call, await request);
  }

  $async.Future<$2.Route> addRouteBinaryOutput_Pre($grpc.ServiceCall call,
      $async.Future<$0.AddRouteBinaryOutputRequest> request) async {
    return addRouteBinaryOutput(call, await request);
  }

  $async.Future<$2.Route> removeRouteOutput_Pre($grpc.ServiceCall call,
      $async.Future<$0.RemoveRouteOutputRequest> request) async {
    return removeRouteOutput(call, await request);
  }

  $async.Future<$2.Route> addRouteEvent_Pre($grpc.ServiceCall call,
      $async.Future<$0.AddRouteEventRequest> request) async {
    return addRouteEvent(call, await request);
  }

  $async.Future<$2.Route> removeRouteEvent_Pre($grpc.ServiceCall call,
      $async.Future<$0.RemoveRouteEventRequest> request) async {
    return removeRouteEvent(call, await request);
  }

  $async.Future<$2.Route> addRouteEventBehavior_Pre($grpc.ServiceCall call,
      $async.Future<$0.AddRouteEventBehaviorRequest> request) async {
    return addRouteEventBehavior(call, await request);
  }

  $async.Future<$2.Route> removeRouteEventBehavior_Pre($grpc.ServiceCall call,
      $async.Future<$0.RemoveRouteEventBehaviorRequest> request) async {
    return removeRouteEventBehavior(call, await request);
  }

  $async.Future<$2.Sensor> getSensor_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getSensor(call, await request);
  }

  $async.Future<$2.Sensor> updateSensor_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Sensor> request) async {
    return updateSensor(call, await request);
  }

  $async.Future<$2.Sensor> addBinarySensor_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return addBinarySensor(call, await request);
  }

  $async.Future<$2.Module> deleteSensor_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return deleteSensor(call, await request);
  }

  $async.Future<$2.Signal> getSignal_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getSignal(call, await request);
  }

  $async.Future<$2.Signal> updateSignal_Pre(
      $grpc.ServiceCall call, $async.Future<$2.Signal> request) async {
    return updateSignal(call, await request);
  }

  $async.Future<$2.Module> deleteSignal_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return deleteSignal(call, await request);
  }

  $async.Future<$2.BinkyNetLocalWorker> getBinkyNetLocalWorker_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return getBinkyNetLocalWorker(call, await request);
  }

  $async.Future<$2.BinkyNetLocalWorker> updateBinkyNetLocalWorker_Pre(
      $grpc.ServiceCall call,
      $async.Future<$2.BinkyNetLocalWorker> request) async {
    return updateBinkyNetLocalWorker(call, await request);
  }

  $async.Future<$2.CommandStation> deleteBinkyNetLocalWorker_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return deleteBinkyNetLocalWorker(call, await request);
  }

  $async.Future<$2.BinkyNetLocalWorker> addBinkyNetLocalWorker_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return addBinkyNetLocalWorker(call, await request);
  }

  $async.Future<$2.BinkyNetDevice> addBinkyNetDevice_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return addBinkyNetDevice(call, await request);
  }

  $async.Future<$2.BinkyNetLocalWorker> deleteBinkyNetDevice_Pre(
      $grpc.ServiceCall call, $async.Future<$0.SubIDRequest> request) async {
    return deleteBinkyNetDevice(call, await request);
  }

  $async.Future<$2.BinkyNetObject> addBinkyNetObject_Pre(
      $grpc.ServiceCall call, $async.Future<$0.IDRequest> request) async {
    return addBinkyNetObject(call, await request);
  }

  $async.Future<$2.BinkyNetLocalWorker> deleteBinkyNetObject_Pre(
      $grpc.ServiceCall call, $async.Future<$0.SubIDRequest> request) async {
    return deleteBinkyNetObject(call, await request);
  }

  $async.Future<$2.BinkyNetLocalWorker> addBinkyNetObjectsGroup_Pre(
      $grpc.ServiceCall call,
      $async.Future<$0.AddBinkyNetObjectsGroupRequest> request) async {
    return addBinkyNetObjectsGroup(call, await request);
  }

  $async.Future<$0.ParseAddressResult> parseAddress(
      $grpc.ServiceCall call, $0.ParseAddressRequest request);
  $async.Future<$0.ParsePermissionResult> parsePermission(
      $grpc.ServiceCall call, $0.ParsePermissionRequest request);
  $async.Future<$2.Railway> loadRailway(
      $grpc.ServiceCall call, $1.RailwayEntry request);
  $async.Future<$2.Empty> closeRailway(
      $grpc.ServiceCall call, $2.Empty request);
  $async.Future<$1.RailwayEntry> getRailwayEntry(
      $grpc.ServiceCall call, $2.Empty request);
  $async.Future<$2.Railway> getRailway(
      $grpc.ServiceCall call, $2.Empty request);
  $async.Future<$2.Railway> updateRailway(
      $grpc.ServiceCall call, $2.Railway request);
  $async.Future<$2.Empty> save($grpc.ServiceCall call, $2.Empty request);
  $async.Future<$2.Module> getModule(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Image> getModuleBackgroundImage(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Module> updateModule(
      $grpc.ServiceCall call, $2.Module request);
  $async.Future<$2.Module> updateModuleBackgroundImage(
      $grpc.ServiceCall call, $0.ImageIDRequest request);
  $async.Future<$2.Module> addModule($grpc.ServiceCall call, $2.Empty request);
  $async.Future<$2.Empty> deleteModule(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Loc> getLoc($grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Loc> updateLoc($grpc.ServiceCall call, $2.Loc request);
  $async.Future<$2.Loc> updateLocImage(
      $grpc.ServiceCall call, $0.ImageIDRequest request);
  $async.Future<$2.Loc> addLoc($grpc.ServiceCall call, $2.Empty request);
  $async.Future<$2.Empty> deleteLoc(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.LocGroup> getLocGroup(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.LocGroup> updateLocGroup(
      $grpc.ServiceCall call, $2.LocGroup request);
  $async.Future<$2.LocGroup> addLocGroup(
      $grpc.ServiceCall call, $2.Empty request);
  $async.Future<$2.Empty> deleteLocGroup(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.CommandStation> getCommandStation(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.CommandStation> updateCommandStation(
      $grpc.ServiceCall call, $2.CommandStation request);
  $async.Future<$2.CommandStation> addBidibCommandStation(
      $grpc.ServiceCall call, $2.Empty request);
  $async.Future<$2.CommandStation> addBinkyNetCommandStation(
      $grpc.ServiceCall call, $2.Empty request);
  $async.Future<$2.Block> getBlock(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Block> updateBlock($grpc.ServiceCall call, $2.Block request);
  $async.Future<$2.Block> addBlock(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Module> deleteBlock(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.BlockGroup> getBlockGroup(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.BlockGroup> updateBlockGroup(
      $grpc.ServiceCall call, $2.BlockGroup request);
  $async.Future<$2.BlockGroup> addBlockGroup(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Module> deleteBlockGroup(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Edge> getEdge($grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Edge> updateEdge($grpc.ServiceCall call, $2.Edge request);
  $async.Future<$2.Edge> addEdge($grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Module> deleteEdge(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Junction> getJunction(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Junction> updateJunction(
      $grpc.ServiceCall call, $2.Junction request);
  $async.Future<$2.Junction> addSwitch(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Module> deleteJunction(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Output> getOutput(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Output> updateOutput(
      $grpc.ServiceCall call, $2.Output request);
  $async.Future<$2.Output> addBinaryOutput(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Module> deleteOutput(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Route> getRoute(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Route> updateRoute($grpc.ServiceCall call, $2.Route request);
  $async.Future<$2.Route> addRoute(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Module> deleteRoute(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Route> addRouteCrossingJunctionSwitch(
      $grpc.ServiceCall call, $0.AddRouteCrossingJunctionSwitchRequest request);
  $async.Future<$2.Route> removeRouteCrossingJunction(
      $grpc.ServiceCall call, $0.RemoveRouteCrossingJunctionRequest request);
  $async.Future<$2.Route> addRouteBinaryOutput(
      $grpc.ServiceCall call, $0.AddRouteBinaryOutputRequest request);
  $async.Future<$2.Route> removeRouteOutput(
      $grpc.ServiceCall call, $0.RemoveRouteOutputRequest request);
  $async.Future<$2.Route> addRouteEvent(
      $grpc.ServiceCall call, $0.AddRouteEventRequest request);
  $async.Future<$2.Route> removeRouteEvent(
      $grpc.ServiceCall call, $0.RemoveRouteEventRequest request);
  $async.Future<$2.Route> addRouteEventBehavior(
      $grpc.ServiceCall call, $0.AddRouteEventBehaviorRequest request);
  $async.Future<$2.Route> removeRouteEventBehavior(
      $grpc.ServiceCall call, $0.RemoveRouteEventBehaviorRequest request);
  $async.Future<$2.Sensor> getSensor(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Sensor> updateSensor(
      $grpc.ServiceCall call, $2.Sensor request);
  $async.Future<$2.Sensor> addBinarySensor(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Module> deleteSensor(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Signal> getSignal(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.Signal> updateSignal(
      $grpc.ServiceCall call, $2.Signal request);
  $async.Future<$2.Module> deleteSignal(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.BinkyNetLocalWorker> getBinkyNetLocalWorker(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.BinkyNetLocalWorker> updateBinkyNetLocalWorker(
      $grpc.ServiceCall call, $2.BinkyNetLocalWorker request);
  $async.Future<$2.CommandStation> deleteBinkyNetLocalWorker(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.BinkyNetLocalWorker> addBinkyNetLocalWorker(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.BinkyNetDevice> addBinkyNetDevice(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.BinkyNetLocalWorker> deleteBinkyNetDevice(
      $grpc.ServiceCall call, $0.SubIDRequest request);
  $async.Future<$2.BinkyNetObject> addBinkyNetObject(
      $grpc.ServiceCall call, $0.IDRequest request);
  $async.Future<$2.BinkyNetLocalWorker> deleteBinkyNetObject(
      $grpc.ServiceCall call, $0.SubIDRequest request);
  $async.Future<$2.BinkyNetLocalWorker> addBinkyNetObjectsGroup(
      $grpc.ServiceCall call, $0.AddBinkyNetObjectsGroupRequest request);
}
