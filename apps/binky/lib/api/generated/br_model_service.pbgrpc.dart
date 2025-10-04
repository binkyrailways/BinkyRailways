///
//  Generated code. Do not modify.
//  source: br_model_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

import 'dart:async' as $async;

import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'br_model_service.pb.dart' as $2;
import 'br_model_types.pb.dart' as $0;
import 'br_storage_types.pb.dart' as $3;
export 'br_model_service.pb.dart';

class ModelServiceClient extends $grpc.Client {
  static final _$parseAddress =
      $grpc.ClientMethod<$2.ParseAddressRequest, $2.ParseAddressResult>(
          '/binkyrailways.v1.ModelService/ParseAddress',
          ($2.ParseAddressRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $2.ParseAddressResult.fromBuffer(value));
  static final _$parsePermission =
      $grpc.ClientMethod<$2.ParsePermissionRequest, $2.ParsePermissionResult>(
          '/binkyrailways.v1.ModelService/ParsePermission',
          ($2.ParsePermissionRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $2.ParsePermissionResult.fromBuffer(value));
  static final _$getSerialPorts =
      $grpc.ClientMethod<$0.Empty, $2.SerialPortList>(
          '/binkyrailways.v1.ModelService/GetSerialPorts',
          ($0.Empty value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $2.SerialPortList.fromBuffer(value));
  static final _$loadRailway = $grpc.ClientMethod<$3.RailwayEntry, $0.Railway>(
      '/binkyrailways.v1.ModelService/LoadRailway',
      ($3.RailwayEntry value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Railway.fromBuffer(value));
  static final _$closeRailway = $grpc.ClientMethod<$0.Empty, $0.Empty>(
      '/binkyrailways.v1.ModelService/CloseRailway',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Empty.fromBuffer(value));
  static final _$getRailwayEntry =
      $grpc.ClientMethod<$0.Empty, $3.RailwayEntry>(
          '/binkyrailways.v1.ModelService/GetRailwayEntry',
          ($0.Empty value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.RailwayEntry.fromBuffer(value));
  static final _$getRailway = $grpc.ClientMethod<$0.Empty, $0.Railway>(
      '/binkyrailways.v1.ModelService/GetRailway',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Railway.fromBuffer(value));
  static final _$updateRailway = $grpc.ClientMethod<$0.Railway, $0.Railway>(
      '/binkyrailways.v1.ModelService/UpdateRailway',
      ($0.Railway value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Railway.fromBuffer(value));
  static final _$save = $grpc.ClientMethod<$0.Empty, $0.Empty>(
      '/binkyrailways.v1.ModelService/Save',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Empty.fromBuffer(value));
  static final _$getModule = $grpc.ClientMethod<$2.IDRequest, $0.Module>(
      '/binkyrailways.v1.ModelService/GetModule',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$getModuleBackgroundImage =
      $grpc.ClientMethod<$2.IDRequest, $0.Image>(
          '/binkyrailways.v1.ModelService/GetModuleBackgroundImage',
          ($2.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Image.fromBuffer(value));
  static final _$updateModule = $grpc.ClientMethod<$0.Module, $0.Module>(
      '/binkyrailways.v1.ModelService/UpdateModule',
      ($0.Module value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$updateModuleBackgroundImage =
      $grpc.ClientMethod<$2.ImageIDRequest, $0.Module>(
          '/binkyrailways.v1.ModelService/UpdateModuleBackgroundImage',
          ($2.ImageIDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$addModule = $grpc.ClientMethod<$0.Empty, $0.Module>(
      '/binkyrailways.v1.ModelService/AddModule',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$deleteModule = $grpc.ClientMethod<$2.IDRequest, $0.Empty>(
      '/binkyrailways.v1.ModelService/DeleteModule',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Empty.fromBuffer(value));
  static final _$getLoc = $grpc.ClientMethod<$2.IDRequest, $0.Loc>(
      '/binkyrailways.v1.ModelService/GetLoc',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Loc.fromBuffer(value));
  static final _$updateLoc = $grpc.ClientMethod<$0.Loc, $0.Loc>(
      '/binkyrailways.v1.ModelService/UpdateLoc',
      ($0.Loc value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Loc.fromBuffer(value));
  static final _$updateLocImage = $grpc.ClientMethod<$2.ImageIDRequest, $0.Loc>(
      '/binkyrailways.v1.ModelService/UpdateLocImage',
      ($2.ImageIDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Loc.fromBuffer(value));
  static final _$addLoc = $grpc.ClientMethod<$0.Empty, $0.Loc>(
      '/binkyrailways.v1.ModelService/AddLoc',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Loc.fromBuffer(value));
  static final _$deleteLoc = $grpc.ClientMethod<$2.IDRequest, $0.Empty>(
      '/binkyrailways.v1.ModelService/DeleteLoc',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Empty.fromBuffer(value));
  static final _$getLocGroup = $grpc.ClientMethod<$2.IDRequest, $0.LocGroup>(
      '/binkyrailways.v1.ModelService/GetLocGroup',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.LocGroup.fromBuffer(value));
  static final _$updateLocGroup = $grpc.ClientMethod<$0.LocGroup, $0.LocGroup>(
      '/binkyrailways.v1.ModelService/UpdateLocGroup',
      ($0.LocGroup value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.LocGroup.fromBuffer(value));
  static final _$addLocGroup = $grpc.ClientMethod<$0.Empty, $0.LocGroup>(
      '/binkyrailways.v1.ModelService/AddLocGroup',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.LocGroup.fromBuffer(value));
  static final _$deleteLocGroup = $grpc.ClientMethod<$2.IDRequest, $0.Empty>(
      '/binkyrailways.v1.ModelService/DeleteLocGroup',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Empty.fromBuffer(value));
  static final _$getCommandStation =
      $grpc.ClientMethod<$2.IDRequest, $0.CommandStation>(
          '/binkyrailways.v1.ModelService/GetCommandStation',
          ($2.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.CommandStation.fromBuffer(value));
  static final _$updateCommandStation =
      $grpc.ClientMethod<$0.CommandStation, $0.CommandStation>(
          '/binkyrailways.v1.ModelService/UpdateCommandStation',
          ($0.CommandStation value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.CommandStation.fromBuffer(value));
  static final _$addBidibCommandStation =
      $grpc.ClientMethod<$0.Empty, $0.CommandStation>(
          '/binkyrailways.v1.ModelService/AddBidibCommandStation',
          ($0.Empty value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.CommandStation.fromBuffer(value));
  static final _$addBinkyNetCommandStation =
      $grpc.ClientMethod<$0.Empty, $0.CommandStation>(
          '/binkyrailways.v1.ModelService/AddBinkyNetCommandStation',
          ($0.Empty value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.CommandStation.fromBuffer(value));
  static final _$getBlock = $grpc.ClientMethod<$2.IDRequest, $0.Block>(
      '/binkyrailways.v1.ModelService/GetBlock',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Block.fromBuffer(value));
  static final _$updateBlock = $grpc.ClientMethod<$0.Block, $0.Block>(
      '/binkyrailways.v1.ModelService/UpdateBlock',
      ($0.Block value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Block.fromBuffer(value));
  static final _$addBlock = $grpc.ClientMethod<$2.IDRequest, $0.Block>(
      '/binkyrailways.v1.ModelService/AddBlock',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Block.fromBuffer(value));
  static final _$deleteBlock = $grpc.ClientMethod<$2.IDRequest, $0.Module>(
      '/binkyrailways.v1.ModelService/DeleteBlock',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$getBlockGroup =
      $grpc.ClientMethod<$2.IDRequest, $0.BlockGroup>(
          '/binkyrailways.v1.ModelService/GetBlockGroup',
          ($2.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.BlockGroup.fromBuffer(value));
  static final _$updateBlockGroup =
      $grpc.ClientMethod<$0.BlockGroup, $0.BlockGroup>(
          '/binkyrailways.v1.ModelService/UpdateBlockGroup',
          ($0.BlockGroup value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.BlockGroup.fromBuffer(value));
  static final _$addBlockGroup =
      $grpc.ClientMethod<$2.IDRequest, $0.BlockGroup>(
          '/binkyrailways.v1.ModelService/AddBlockGroup',
          ($2.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.BlockGroup.fromBuffer(value));
  static final _$deleteBlockGroup = $grpc.ClientMethod<$2.IDRequest, $0.Module>(
      '/binkyrailways.v1.ModelService/DeleteBlockGroup',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$getEdge = $grpc.ClientMethod<$2.IDRequest, $0.Edge>(
      '/binkyrailways.v1.ModelService/GetEdge',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Edge.fromBuffer(value));
  static final _$updateEdge = $grpc.ClientMethod<$0.Edge, $0.Edge>(
      '/binkyrailways.v1.ModelService/UpdateEdge',
      ($0.Edge value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Edge.fromBuffer(value));
  static final _$addEdge = $grpc.ClientMethod<$2.IDRequest, $0.Edge>(
      '/binkyrailways.v1.ModelService/AddEdge',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Edge.fromBuffer(value));
  static final _$deleteEdge = $grpc.ClientMethod<$2.IDRequest, $0.Module>(
      '/binkyrailways.v1.ModelService/DeleteEdge',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$getJunction = $grpc.ClientMethod<$2.IDRequest, $0.Junction>(
      '/binkyrailways.v1.ModelService/GetJunction',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Junction.fromBuffer(value));
  static final _$updateJunction = $grpc.ClientMethod<$0.Junction, $0.Junction>(
      '/binkyrailways.v1.ModelService/UpdateJunction',
      ($0.Junction value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Junction.fromBuffer(value));
  static final _$addSwitch = $grpc.ClientMethod<$2.IDRequest, $0.Junction>(
      '/binkyrailways.v1.ModelService/AddSwitch',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Junction.fromBuffer(value));
  static final _$deleteJunction = $grpc.ClientMethod<$2.IDRequest, $0.Module>(
      '/binkyrailways.v1.ModelService/DeleteJunction',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$getOutput = $grpc.ClientMethod<$2.IDRequest, $0.Output>(
      '/binkyrailways.v1.ModelService/GetOutput',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Output.fromBuffer(value));
  static final _$updateOutput = $grpc.ClientMethod<$0.Output, $0.Output>(
      '/binkyrailways.v1.ModelService/UpdateOutput',
      ($0.Output value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Output.fromBuffer(value));
  static final _$addBinaryOutput = $grpc.ClientMethod<$2.IDRequest, $0.Output>(
      '/binkyrailways.v1.ModelService/AddBinaryOutput',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Output.fromBuffer(value));
  static final _$deleteOutput = $grpc.ClientMethod<$2.IDRequest, $0.Module>(
      '/binkyrailways.v1.ModelService/DeleteOutput',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$getRoute = $grpc.ClientMethod<$2.IDRequest, $0.Route>(
      '/binkyrailways.v1.ModelService/GetRoute',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Route.fromBuffer(value));
  static final _$updateRoute = $grpc.ClientMethod<$0.Route, $0.Route>(
      '/binkyrailways.v1.ModelService/UpdateRoute',
      ($0.Route value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Route.fromBuffer(value));
  static final _$addRoute = $grpc.ClientMethod<$2.IDRequest, $0.Route>(
      '/binkyrailways.v1.ModelService/AddRoute',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Route.fromBuffer(value));
  static final _$deleteRoute = $grpc.ClientMethod<$2.IDRequest, $0.Module>(
      '/binkyrailways.v1.ModelService/DeleteRoute',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$addRouteCrossingJunctionSwitch =
      $grpc.ClientMethod<$2.AddRouteCrossingJunctionSwitchRequest, $0.Route>(
          '/binkyrailways.v1.ModelService/AddRouteCrossingJunctionSwitch',
          ($2.AddRouteCrossingJunctionSwitchRequest value) =>
              value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Route.fromBuffer(value));
  static final _$removeRouteCrossingJunction =
      $grpc.ClientMethod<$2.RemoveRouteCrossingJunctionRequest, $0.Route>(
          '/binkyrailways.v1.ModelService/RemoveRouteCrossingJunction',
          ($2.RemoveRouteCrossingJunctionRequest value) =>
              value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Route.fromBuffer(value));
  static final _$addRouteBinaryOutput =
      $grpc.ClientMethod<$2.AddRouteBinaryOutputRequest, $0.Route>(
          '/binkyrailways.v1.ModelService/AddRouteBinaryOutput',
          ($2.AddRouteBinaryOutputRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Route.fromBuffer(value));
  static final _$removeRouteOutput =
      $grpc.ClientMethod<$2.RemoveRouteOutputRequest, $0.Route>(
          '/binkyrailways.v1.ModelService/RemoveRouteOutput',
          ($2.RemoveRouteOutputRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Route.fromBuffer(value));
  static final _$addRouteEvent =
      $grpc.ClientMethod<$2.AddRouteEventRequest, $0.Route>(
          '/binkyrailways.v1.ModelService/AddRouteEvent',
          ($2.AddRouteEventRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Route.fromBuffer(value));
  static final _$moveRouteEventUp =
      $grpc.ClientMethod<$2.MoveRouteEventRequest, $0.Route>(
          '/binkyrailways.v1.ModelService/MoveRouteEventUp',
          ($2.MoveRouteEventRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Route.fromBuffer(value));
  static final _$moveRouteEventDown =
      $grpc.ClientMethod<$2.MoveRouteEventRequest, $0.Route>(
          '/binkyrailways.v1.ModelService/MoveRouteEventDown',
          ($2.MoveRouteEventRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Route.fromBuffer(value));
  static final _$removeRouteEvent =
      $grpc.ClientMethod<$2.RemoveRouteEventRequest, $0.Route>(
          '/binkyrailways.v1.ModelService/RemoveRouteEvent',
          ($2.RemoveRouteEventRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Route.fromBuffer(value));
  static final _$addRouteEventBehavior =
      $grpc.ClientMethod<$2.AddRouteEventBehaviorRequest, $0.Route>(
          '/binkyrailways.v1.ModelService/AddRouteEventBehavior',
          ($2.AddRouteEventBehaviorRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Route.fromBuffer(value));
  static final _$removeRouteEventBehavior =
      $grpc.ClientMethod<$2.RemoveRouteEventBehaviorRequest, $0.Route>(
          '/binkyrailways.v1.ModelService/RemoveRouteEventBehavior',
          ($2.RemoveRouteEventBehaviorRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Route.fromBuffer(value));
  static final _$getSensor = $grpc.ClientMethod<$2.IDRequest, $0.Sensor>(
      '/binkyrailways.v1.ModelService/GetSensor',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Sensor.fromBuffer(value));
  static final _$updateSensor = $grpc.ClientMethod<$0.Sensor, $0.Sensor>(
      '/binkyrailways.v1.ModelService/UpdateSensor',
      ($0.Sensor value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Sensor.fromBuffer(value));
  static final _$addBinarySensor = $grpc.ClientMethod<$2.IDRequest, $0.Sensor>(
      '/binkyrailways.v1.ModelService/AddBinarySensor',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Sensor.fromBuffer(value));
  static final _$deleteSensor = $grpc.ClientMethod<$2.IDRequest, $0.Module>(
      '/binkyrailways.v1.ModelService/DeleteSensor',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$getSignal = $grpc.ClientMethod<$2.IDRequest, $0.Signal>(
      '/binkyrailways.v1.ModelService/GetSignal',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Signal.fromBuffer(value));
  static final _$addBlockSignal = $grpc.ClientMethod<$2.IDRequest, $0.Signal>(
      '/binkyrailways.v1.ModelService/AddBlockSignal',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Signal.fromBuffer(value));
  static final _$updateSignal = $grpc.ClientMethod<$0.Signal, $0.Signal>(
      '/binkyrailways.v1.ModelService/UpdateSignal',
      ($0.Signal value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Signal.fromBuffer(value));
  static final _$deleteSignal = $grpc.ClientMethod<$2.IDRequest, $0.Module>(
      '/binkyrailways.v1.ModelService/DeleteSignal',
      ($2.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$getBinkyNetLocalWorker =
      $grpc.ClientMethod<$2.IDRequest, $0.BinkyNetLocalWorker>(
          '/binkyrailways.v1.ModelService/GetBinkyNetLocalWorker',
          ($2.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.BinkyNetLocalWorker.fromBuffer(value));
  static final _$updateBinkyNetLocalWorker =
      $grpc.ClientMethod<$0.BinkyNetLocalWorker, $0.BinkyNetLocalWorker>(
          '/binkyrailways.v1.ModelService/UpdateBinkyNetLocalWorker',
          ($0.BinkyNetLocalWorker value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.BinkyNetLocalWorker.fromBuffer(value));
  static final _$deleteBinkyNetLocalWorker =
      $grpc.ClientMethod<$2.IDRequest, $0.CommandStation>(
          '/binkyrailways.v1.ModelService/DeleteBinkyNetLocalWorker',
          ($2.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.CommandStation.fromBuffer(value));
  static final _$addBinkyNetLocalWorker =
      $grpc.ClientMethod<$2.IDRequest, $0.BinkyNetLocalWorker>(
          '/binkyrailways.v1.ModelService/AddBinkyNetLocalWorker',
          ($2.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.BinkyNetLocalWorker.fromBuffer(value));
  static final _$addBinkyNetRouter =
      $grpc.ClientMethod<$2.IDRequest, $0.BinkyNetRouter>(
          '/binkyrailways.v1.ModelService/AddBinkyNetRouter',
          ($2.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.BinkyNetRouter.fromBuffer(value));
  static final _$deleteBinkyNetRouter =
      $grpc.ClientMethod<$2.SubIDRequest, $0.BinkyNetLocalWorker>(
          '/binkyrailways.v1.ModelService/DeleteBinkyNetRouter',
          ($2.SubIDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.BinkyNetLocalWorker.fromBuffer(value));
  static final _$addBinkyNetDevice =
      $grpc.ClientMethod<$2.IDRequest, $0.BinkyNetDevice>(
          '/binkyrailways.v1.ModelService/AddBinkyNetDevice',
          ($2.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.BinkyNetDevice.fromBuffer(value));
  static final _$deleteBinkyNetDevice =
      $grpc.ClientMethod<$2.SubIDRequest, $0.BinkyNetLocalWorker>(
          '/binkyrailways.v1.ModelService/DeleteBinkyNetDevice',
          ($2.SubIDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.BinkyNetLocalWorker.fromBuffer(value));
  static final _$addBinkyNetObject =
      $grpc.ClientMethod<$2.IDRequest, $0.BinkyNetObject>(
          '/binkyrailways.v1.ModelService/AddBinkyNetObject',
          ($2.IDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.BinkyNetObject.fromBuffer(value));
  static final _$deleteBinkyNetObject =
      $grpc.ClientMethod<$2.SubIDRequest, $0.BinkyNetLocalWorker>(
          '/binkyrailways.v1.ModelService/DeleteBinkyNetObject',
          ($2.SubIDRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.BinkyNetLocalWorker.fromBuffer(value));
  static final _$addBinkyNetObjectsGroup = $grpc.ClientMethod<
          $2.AddBinkyNetObjectsGroupRequest, $0.BinkyNetLocalWorker>(
      '/binkyrailways.v1.ModelService/AddBinkyNetObjectsGroup',
      ($2.AddBinkyNetObjectsGroupRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) =>
          $0.BinkyNetLocalWorker.fromBuffer(value));

  ModelServiceClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options, interceptors: interceptors);

  $grpc.ResponseFuture<$2.ParseAddressResult> parseAddress(
      $2.ParseAddressRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$parseAddress, request, options: options);
  }

  $grpc.ResponseFuture<$2.ParsePermissionResult> parsePermission(
      $2.ParsePermissionRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$parsePermission, request, options: options);
  }

  $grpc.ResponseFuture<$2.SerialPortList> getSerialPorts($0.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getSerialPorts, request, options: options);
  }

  $grpc.ResponseFuture<$0.Railway> loadRailway($3.RailwayEntry request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$loadRailway, request, options: options);
  }

  $grpc.ResponseFuture<$0.Empty> closeRailway($0.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$closeRailway, request, options: options);
  }

  $grpc.ResponseFuture<$3.RailwayEntry> getRailwayEntry($0.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getRailwayEntry, request, options: options);
  }

  $grpc.ResponseFuture<$0.Railway> getRailway($0.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getRailway, request, options: options);
  }

  $grpc.ResponseFuture<$0.Railway> updateRailway($0.Railway request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateRailway, request, options: options);
  }

  $grpc.ResponseFuture<$0.Empty> save($0.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$save, request, options: options);
  }

  $grpc.ResponseFuture<$0.Module> getModule($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getModule, request, options: options);
  }

  $grpc.ResponseFuture<$0.Image> getModuleBackgroundImage($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getModuleBackgroundImage, request,
        options: options);
  }

  $grpc.ResponseFuture<$0.Module> updateModule($0.Module request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateModule, request, options: options);
  }

  $grpc.ResponseFuture<$0.Module> updateModuleBackgroundImage(
      $2.ImageIDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateModuleBackgroundImage, request,
        options: options);
  }

  $grpc.ResponseFuture<$0.Module> addModule($0.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addModule, request, options: options);
  }

  $grpc.ResponseFuture<$0.Empty> deleteModule($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteModule, request, options: options);
  }

  $grpc.ResponseFuture<$0.Loc> getLoc($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getLoc, request, options: options);
  }

  $grpc.ResponseFuture<$0.Loc> updateLoc($0.Loc request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateLoc, request, options: options);
  }

  $grpc.ResponseFuture<$0.Loc> updateLocImage($2.ImageIDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateLocImage, request, options: options);
  }

  $grpc.ResponseFuture<$0.Loc> addLoc($0.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addLoc, request, options: options);
  }

  $grpc.ResponseFuture<$0.Empty> deleteLoc($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteLoc, request, options: options);
  }

  $grpc.ResponseFuture<$0.LocGroup> getLocGroup($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getLocGroup, request, options: options);
  }

  $grpc.ResponseFuture<$0.LocGroup> updateLocGroup($0.LocGroup request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateLocGroup, request, options: options);
  }

  $grpc.ResponseFuture<$0.LocGroup> addLocGroup($0.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addLocGroup, request, options: options);
  }

  $grpc.ResponseFuture<$0.Empty> deleteLocGroup($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteLocGroup, request, options: options);
  }

  $grpc.ResponseFuture<$0.CommandStation> getCommandStation(
      $2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getCommandStation, request, options: options);
  }

  $grpc.ResponseFuture<$0.CommandStation> updateCommandStation(
      $0.CommandStation request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateCommandStation, request, options: options);
  }

  $grpc.ResponseFuture<$0.CommandStation> addBidibCommandStation(
      $0.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBidibCommandStation, request,
        options: options);
  }

  $grpc.ResponseFuture<$0.CommandStation> addBinkyNetCommandStation(
      $0.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinkyNetCommandStation, request,
        options: options);
  }

  $grpc.ResponseFuture<$0.Block> getBlock($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getBlock, request, options: options);
  }

  $grpc.ResponseFuture<$0.Block> updateBlock($0.Block request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateBlock, request, options: options);
  }

  $grpc.ResponseFuture<$0.Block> addBlock($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBlock, request, options: options);
  }

  $grpc.ResponseFuture<$0.Module> deleteBlock($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteBlock, request, options: options);
  }

  $grpc.ResponseFuture<$0.BlockGroup> getBlockGroup($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getBlockGroup, request, options: options);
  }

  $grpc.ResponseFuture<$0.BlockGroup> updateBlockGroup($0.BlockGroup request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateBlockGroup, request, options: options);
  }

  $grpc.ResponseFuture<$0.BlockGroup> addBlockGroup($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBlockGroup, request, options: options);
  }

  $grpc.ResponseFuture<$0.Module> deleteBlockGroup($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteBlockGroup, request, options: options);
  }

  $grpc.ResponseFuture<$0.Edge> getEdge($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getEdge, request, options: options);
  }

  $grpc.ResponseFuture<$0.Edge> updateEdge($0.Edge request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateEdge, request, options: options);
  }

  $grpc.ResponseFuture<$0.Edge> addEdge($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addEdge, request, options: options);
  }

  $grpc.ResponseFuture<$0.Module> deleteEdge($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteEdge, request, options: options);
  }

  $grpc.ResponseFuture<$0.Junction> getJunction($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getJunction, request, options: options);
  }

  $grpc.ResponseFuture<$0.Junction> updateJunction($0.Junction request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateJunction, request, options: options);
  }

  $grpc.ResponseFuture<$0.Junction> addSwitch($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addSwitch, request, options: options);
  }

  $grpc.ResponseFuture<$0.Module> deleteJunction($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteJunction, request, options: options);
  }

  $grpc.ResponseFuture<$0.Output> getOutput($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getOutput, request, options: options);
  }

  $grpc.ResponseFuture<$0.Output> updateOutput($0.Output request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateOutput, request, options: options);
  }

  $grpc.ResponseFuture<$0.Output> addBinaryOutput($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinaryOutput, request, options: options);
  }

  $grpc.ResponseFuture<$0.Module> deleteOutput($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteOutput, request, options: options);
  }

  $grpc.ResponseFuture<$0.Route> getRoute($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getRoute, request, options: options);
  }

  $grpc.ResponseFuture<$0.Route> updateRoute($0.Route request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateRoute, request, options: options);
  }

  $grpc.ResponseFuture<$0.Route> addRoute($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addRoute, request, options: options);
  }

  $grpc.ResponseFuture<$0.Module> deleteRoute($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteRoute, request, options: options);
  }

  $grpc.ResponseFuture<$0.Route> addRouteCrossingJunctionSwitch(
      $2.AddRouteCrossingJunctionSwitchRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addRouteCrossingJunctionSwitch, request,
        options: options);
  }

  $grpc.ResponseFuture<$0.Route> removeRouteCrossingJunction(
      $2.RemoveRouteCrossingJunctionRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$removeRouteCrossingJunction, request,
        options: options);
  }

  $grpc.ResponseFuture<$0.Route> addRouteBinaryOutput(
      $2.AddRouteBinaryOutputRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addRouteBinaryOutput, request, options: options);
  }

  $grpc.ResponseFuture<$0.Route> removeRouteOutput(
      $2.RemoveRouteOutputRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$removeRouteOutput, request, options: options);
  }

  $grpc.ResponseFuture<$0.Route> addRouteEvent($2.AddRouteEventRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addRouteEvent, request, options: options);
  }

  $grpc.ResponseFuture<$0.Route> moveRouteEventUp(
      $2.MoveRouteEventRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$moveRouteEventUp, request, options: options);
  }

  $grpc.ResponseFuture<$0.Route> moveRouteEventDown(
      $2.MoveRouteEventRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$moveRouteEventDown, request, options: options);
  }

  $grpc.ResponseFuture<$0.Route> removeRouteEvent(
      $2.RemoveRouteEventRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$removeRouteEvent, request, options: options);
  }

  $grpc.ResponseFuture<$0.Route> addRouteEventBehavior(
      $2.AddRouteEventBehaviorRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addRouteEventBehavior, request, options: options);
  }

  $grpc.ResponseFuture<$0.Route> removeRouteEventBehavior(
      $2.RemoveRouteEventBehaviorRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$removeRouteEventBehavior, request,
        options: options);
  }

  $grpc.ResponseFuture<$0.Sensor> getSensor($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getSensor, request, options: options);
  }

  $grpc.ResponseFuture<$0.Sensor> updateSensor($0.Sensor request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateSensor, request, options: options);
  }

  $grpc.ResponseFuture<$0.Sensor> addBinarySensor($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinarySensor, request, options: options);
  }

  $grpc.ResponseFuture<$0.Module> deleteSensor($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteSensor, request, options: options);
  }

  $grpc.ResponseFuture<$0.Signal> getSignal($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getSignal, request, options: options);
  }

  $grpc.ResponseFuture<$0.Signal> addBlockSignal($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBlockSignal, request, options: options);
  }

  $grpc.ResponseFuture<$0.Signal> updateSignal($0.Signal request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateSignal, request, options: options);
  }

  $grpc.ResponseFuture<$0.Module> deleteSignal($2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteSignal, request, options: options);
  }

  $grpc.ResponseFuture<$0.BinkyNetLocalWorker> getBinkyNetLocalWorker(
      $2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getBinkyNetLocalWorker, request,
        options: options);
  }

  $grpc.ResponseFuture<$0.BinkyNetLocalWorker> updateBinkyNetLocalWorker(
      $0.BinkyNetLocalWorker request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateBinkyNetLocalWorker, request,
        options: options);
  }

  $grpc.ResponseFuture<$0.CommandStation> deleteBinkyNetLocalWorker(
      $2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteBinkyNetLocalWorker, request,
        options: options);
  }

  $grpc.ResponseFuture<$0.BinkyNetLocalWorker> addBinkyNetLocalWorker(
      $2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinkyNetLocalWorker, request,
        options: options);
  }

  $grpc.ResponseFuture<$0.BinkyNetRouter> addBinkyNetRouter(
      $2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinkyNetRouter, request, options: options);
  }

  $grpc.ResponseFuture<$0.BinkyNetLocalWorker> deleteBinkyNetRouter(
      $2.SubIDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteBinkyNetRouter, request, options: options);
  }

  $grpc.ResponseFuture<$0.BinkyNetDevice> addBinkyNetDevice(
      $2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinkyNetDevice, request, options: options);
  }

  $grpc.ResponseFuture<$0.BinkyNetLocalWorker> deleteBinkyNetDevice(
      $2.SubIDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteBinkyNetDevice, request, options: options);
  }

  $grpc.ResponseFuture<$0.BinkyNetObject> addBinkyNetObject(
      $2.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinkyNetObject, request, options: options);
  }

  $grpc.ResponseFuture<$0.BinkyNetLocalWorker> deleteBinkyNetObject(
      $2.SubIDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$deleteBinkyNetObject, request, options: options);
  }

  $grpc.ResponseFuture<$0.BinkyNetLocalWorker> addBinkyNetObjectsGroup(
      $2.AddBinkyNetObjectsGroupRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$addBinkyNetObjectsGroup, request,
        options: options);
  }
}

abstract class ModelServiceBase extends $grpc.Service {
  $core.String get $name => 'binkyrailways.v1.ModelService';

  ModelServiceBase() {
    $addMethod(
        $grpc.ServiceMethod<$2.ParseAddressRequest, $2.ParseAddressResult>(
            'ParseAddress',
            parseAddress_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $2.ParseAddressRequest.fromBuffer(value),
            ($2.ParseAddressResult value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.ParsePermissionRequest,
            $2.ParsePermissionResult>(
        'ParsePermission',
        parsePermission_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $2.ParsePermissionRequest.fromBuffer(value),
        ($2.ParsePermissionResult value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $2.SerialPortList>(
        'GetSerialPorts',
        getSerialPorts_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($2.SerialPortList value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$3.RailwayEntry, $0.Railway>(
        'LoadRailway',
        loadRailway_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $3.RailwayEntry.fromBuffer(value),
        ($0.Railway value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $0.Empty>(
        'CloseRailway',
        closeRailway_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($0.Empty value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $3.RailwayEntry>(
        'GetRailwayEntry',
        getRailwayEntry_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($3.RailwayEntry value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $0.Railway>(
        'GetRailway',
        getRailway_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($0.Railway value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Railway, $0.Railway>(
        'UpdateRailway',
        updateRailway_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Railway.fromBuffer(value),
        ($0.Railway value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $0.Empty>(
        'Save',
        save_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($0.Empty value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Module>(
        'GetModule',
        getModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Image>(
        'GetModuleBackgroundImage',
        getModuleBackgroundImage_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Image value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Module, $0.Module>(
        'UpdateModule',
        updateModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Module.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.ImageIDRequest, $0.Module>(
        'UpdateModuleBackgroundImage',
        updateModuleBackgroundImage_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.ImageIDRequest.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $0.Module>(
        'AddModule',
        addModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Empty>(
        'DeleteModule',
        deleteModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Empty value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Loc>(
        'GetLoc',
        getLoc_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Loc value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Loc, $0.Loc>(
        'UpdateLoc',
        updateLoc_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Loc.fromBuffer(value),
        ($0.Loc value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.ImageIDRequest, $0.Loc>(
        'UpdateLocImage',
        updateLocImage_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.ImageIDRequest.fromBuffer(value),
        ($0.Loc value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $0.Loc>(
        'AddLoc',
        addLoc_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($0.Loc value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Empty>(
        'DeleteLoc',
        deleteLoc_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Empty value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.LocGroup>(
        'GetLocGroup',
        getLocGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.LocGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.LocGroup, $0.LocGroup>(
        'UpdateLocGroup',
        updateLocGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.LocGroup.fromBuffer(value),
        ($0.LocGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $0.LocGroup>(
        'AddLocGroup',
        addLocGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($0.LocGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Empty>(
        'DeleteLocGroup',
        deleteLocGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Empty value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.CommandStation>(
        'GetCommandStation',
        getCommandStation_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.CommandStation value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.CommandStation, $0.CommandStation>(
        'UpdateCommandStation',
        updateCommandStation_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.CommandStation.fromBuffer(value),
        ($0.CommandStation value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $0.CommandStation>(
        'AddBidibCommandStation',
        addBidibCommandStation_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($0.CommandStation value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $0.CommandStation>(
        'AddBinkyNetCommandStation',
        addBinkyNetCommandStation_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($0.CommandStation value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Block>(
        'GetBlock',
        getBlock_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Block value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Block, $0.Block>(
        'UpdateBlock',
        updateBlock_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Block.fromBuffer(value),
        ($0.Block value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Block>(
        'AddBlock',
        addBlock_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Block value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Module>(
        'DeleteBlock',
        deleteBlock_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.BlockGroup>(
        'GetBlockGroup',
        getBlockGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.BlockGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.BlockGroup, $0.BlockGroup>(
        'UpdateBlockGroup',
        updateBlockGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.BlockGroup.fromBuffer(value),
        ($0.BlockGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.BlockGroup>(
        'AddBlockGroup',
        addBlockGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.BlockGroup value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Module>(
        'DeleteBlockGroup',
        deleteBlockGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Edge>(
        'GetEdge',
        getEdge_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Edge value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Edge, $0.Edge>(
        'UpdateEdge',
        updateEdge_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Edge.fromBuffer(value),
        ($0.Edge value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Edge>(
        'AddEdge',
        addEdge_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Edge value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Module>(
        'DeleteEdge',
        deleteEdge_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Junction>(
        'GetJunction',
        getJunction_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Junction value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Junction, $0.Junction>(
        'UpdateJunction',
        updateJunction_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Junction.fromBuffer(value),
        ($0.Junction value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Junction>(
        'AddSwitch',
        addSwitch_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Junction value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Module>(
        'DeleteJunction',
        deleteJunction_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Output>(
        'GetOutput',
        getOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Output value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Output, $0.Output>(
        'UpdateOutput',
        updateOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Output.fromBuffer(value),
        ($0.Output value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Output>(
        'AddBinaryOutput',
        addBinaryOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Output value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Module>(
        'DeleteOutput',
        deleteOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Route>(
        'GetRoute',
        getRoute_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Route, $0.Route>(
        'UpdateRoute',
        updateRoute_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Route.fromBuffer(value),
        ($0.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Route>(
        'AddRoute',
        addRoute_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Module>(
        'DeleteRoute',
        deleteRoute_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$2.AddRouteCrossingJunctionSwitchRequest, $0.Route>(
            'AddRouteCrossingJunctionSwitch',
            addRouteCrossingJunctionSwitch_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $2.AddRouteCrossingJunctionSwitchRequest.fromBuffer(value),
            ($0.Route value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$2.RemoveRouteCrossingJunctionRequest, $0.Route>(
            'RemoveRouteCrossingJunction',
            removeRouteCrossingJunction_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $2.RemoveRouteCrossingJunctionRequest.fromBuffer(value),
            ($0.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.AddRouteBinaryOutputRequest, $0.Route>(
        'AddRouteBinaryOutput',
        addRouteBinaryOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $2.AddRouteBinaryOutputRequest.fromBuffer(value),
        ($0.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.RemoveRouteOutputRequest, $0.Route>(
        'RemoveRouteOutput',
        removeRouteOutput_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $2.RemoveRouteOutputRequest.fromBuffer(value),
        ($0.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.AddRouteEventRequest, $0.Route>(
        'AddRouteEvent',
        addRouteEvent_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $2.AddRouteEventRequest.fromBuffer(value),
        ($0.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.MoveRouteEventRequest, $0.Route>(
        'MoveRouteEventUp',
        moveRouteEventUp_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $2.MoveRouteEventRequest.fromBuffer(value),
        ($0.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.MoveRouteEventRequest, $0.Route>(
        'MoveRouteEventDown',
        moveRouteEventDown_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $2.MoveRouteEventRequest.fromBuffer(value),
        ($0.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.RemoveRouteEventRequest, $0.Route>(
        'RemoveRouteEvent',
        removeRouteEvent_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $2.RemoveRouteEventRequest.fromBuffer(value),
        ($0.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.AddRouteEventBehaviorRequest, $0.Route>(
        'AddRouteEventBehavior',
        addRouteEventBehavior_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $2.AddRouteEventBehaviorRequest.fromBuffer(value),
        ($0.Route value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$2.RemoveRouteEventBehaviorRequest, $0.Route>(
            'RemoveRouteEventBehavior',
            removeRouteEventBehavior_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $2.RemoveRouteEventBehaviorRequest.fromBuffer(value),
            ($0.Route value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Sensor>(
        'GetSensor',
        getSensor_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Sensor value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Sensor, $0.Sensor>(
        'UpdateSensor',
        updateSensor_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Sensor.fromBuffer(value),
        ($0.Sensor value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Sensor>(
        'AddBinarySensor',
        addBinarySensor_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Sensor value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Module>(
        'DeleteSensor',
        deleteSensor_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Signal>(
        'GetSignal',
        getSignal_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Signal value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Signal>(
        'AddBlockSignal',
        addBlockSignal_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Signal value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Signal, $0.Signal>(
        'UpdateSignal',
        updateSignal_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Signal.fromBuffer(value),
        ($0.Signal value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.Module>(
        'DeleteSignal',
        deleteSignal_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.BinkyNetLocalWorker>(
        'GetBinkyNetLocalWorker',
        getBinkyNetLocalWorker_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.BinkyNetLocalWorker value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$0.BinkyNetLocalWorker, $0.BinkyNetLocalWorker>(
            'UpdateBinkyNetLocalWorker',
            updateBinkyNetLocalWorker_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $0.BinkyNetLocalWorker.fromBuffer(value),
            ($0.BinkyNetLocalWorker value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.CommandStation>(
        'DeleteBinkyNetLocalWorker',
        deleteBinkyNetLocalWorker_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.CommandStation value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.BinkyNetLocalWorker>(
        'AddBinkyNetLocalWorker',
        addBinkyNetLocalWorker_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.BinkyNetLocalWorker value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.BinkyNetRouter>(
        'AddBinkyNetRouter',
        addBinkyNetRouter_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.BinkyNetRouter value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.SubIDRequest, $0.BinkyNetLocalWorker>(
        'DeleteBinkyNetRouter',
        deleteBinkyNetRouter_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.SubIDRequest.fromBuffer(value),
        ($0.BinkyNetLocalWorker value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.BinkyNetDevice>(
        'AddBinkyNetDevice',
        addBinkyNetDevice_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.BinkyNetDevice value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.SubIDRequest, $0.BinkyNetLocalWorker>(
        'DeleteBinkyNetDevice',
        deleteBinkyNetDevice_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.SubIDRequest.fromBuffer(value),
        ($0.BinkyNetLocalWorker value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.IDRequest, $0.BinkyNetObject>(
        'AddBinkyNetObject',
        addBinkyNetObject_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.IDRequest.fromBuffer(value),
        ($0.BinkyNetObject value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.SubIDRequest, $0.BinkyNetLocalWorker>(
        'DeleteBinkyNetObject',
        deleteBinkyNetObject_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $2.SubIDRequest.fromBuffer(value),
        ($0.BinkyNetLocalWorker value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$2.AddBinkyNetObjectsGroupRequest,
            $0.BinkyNetLocalWorker>(
        'AddBinkyNetObjectsGroup',
        addBinkyNetObjectsGroup_Pre,
        false,
        false,
        ($core.List<$core.int> value) =>
            $2.AddBinkyNetObjectsGroupRequest.fromBuffer(value),
        ($0.BinkyNetLocalWorker value) => value.writeToBuffer()));
  }

  $async.Future<$2.ParseAddressResult> parseAddress_Pre($grpc.ServiceCall call,
      $async.Future<$2.ParseAddressRequest> request) async {
    return parseAddress(call, await request);
  }

  $async.Future<$2.ParsePermissionResult> parsePermission_Pre(
      $grpc.ServiceCall call,
      $async.Future<$2.ParsePermissionRequest> request) async {
    return parsePermission(call, await request);
  }

  $async.Future<$2.SerialPortList> getSerialPorts_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return getSerialPorts(call, await request);
  }

  $async.Future<$0.Railway> loadRailway_Pre(
      $grpc.ServiceCall call, $async.Future<$3.RailwayEntry> request) async {
    return loadRailway(call, await request);
  }

  $async.Future<$0.Empty> closeRailway_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return closeRailway(call, await request);
  }

  $async.Future<$3.RailwayEntry> getRailwayEntry_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return getRailwayEntry(call, await request);
  }

  $async.Future<$0.Railway> getRailway_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return getRailway(call, await request);
  }

  $async.Future<$0.Railway> updateRailway_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Railway> request) async {
    return updateRailway(call, await request);
  }

  $async.Future<$0.Empty> save_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return save(call, await request);
  }

  $async.Future<$0.Module> getModule_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getModule(call, await request);
  }

  $async.Future<$0.Image> getModuleBackgroundImage_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getModuleBackgroundImage(call, await request);
  }

  $async.Future<$0.Module> updateModule_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Module> request) async {
    return updateModule(call, await request);
  }

  $async.Future<$0.Module> updateModuleBackgroundImage_Pre(
      $grpc.ServiceCall call, $async.Future<$2.ImageIDRequest> request) async {
    return updateModuleBackgroundImage(call, await request);
  }

  $async.Future<$0.Module> addModule_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return addModule(call, await request);
  }

  $async.Future<$0.Empty> deleteModule_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return deleteModule(call, await request);
  }

  $async.Future<$0.Loc> getLoc_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getLoc(call, await request);
  }

  $async.Future<$0.Loc> updateLoc_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Loc> request) async {
    return updateLoc(call, await request);
  }

  $async.Future<$0.Loc> updateLocImage_Pre(
      $grpc.ServiceCall call, $async.Future<$2.ImageIDRequest> request) async {
    return updateLocImage(call, await request);
  }

  $async.Future<$0.Loc> addLoc_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return addLoc(call, await request);
  }

  $async.Future<$0.Empty> deleteLoc_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return deleteLoc(call, await request);
  }

  $async.Future<$0.LocGroup> getLocGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getLocGroup(call, await request);
  }

  $async.Future<$0.LocGroup> updateLocGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$0.LocGroup> request) async {
    return updateLocGroup(call, await request);
  }

  $async.Future<$0.LocGroup> addLocGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return addLocGroup(call, await request);
  }

  $async.Future<$0.Empty> deleteLocGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return deleteLocGroup(call, await request);
  }

  $async.Future<$0.CommandStation> getCommandStation_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getCommandStation(call, await request);
  }

  $async.Future<$0.CommandStation> updateCommandStation_Pre(
      $grpc.ServiceCall call, $async.Future<$0.CommandStation> request) async {
    return updateCommandStation(call, await request);
  }

  $async.Future<$0.CommandStation> addBidibCommandStation_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return addBidibCommandStation(call, await request);
  }

  $async.Future<$0.CommandStation> addBinkyNetCommandStation_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return addBinkyNetCommandStation(call, await request);
  }

  $async.Future<$0.Block> getBlock_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getBlock(call, await request);
  }

  $async.Future<$0.Block> updateBlock_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Block> request) async {
    return updateBlock(call, await request);
  }

  $async.Future<$0.Block> addBlock_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return addBlock(call, await request);
  }

  $async.Future<$0.Module> deleteBlock_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return deleteBlock(call, await request);
  }

  $async.Future<$0.BlockGroup> getBlockGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getBlockGroup(call, await request);
  }

  $async.Future<$0.BlockGroup> updateBlockGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$0.BlockGroup> request) async {
    return updateBlockGroup(call, await request);
  }

  $async.Future<$0.BlockGroup> addBlockGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return addBlockGroup(call, await request);
  }

  $async.Future<$0.Module> deleteBlockGroup_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return deleteBlockGroup(call, await request);
  }

  $async.Future<$0.Edge> getEdge_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getEdge(call, await request);
  }

  $async.Future<$0.Edge> updateEdge_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Edge> request) async {
    return updateEdge(call, await request);
  }

  $async.Future<$0.Edge> addEdge_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return addEdge(call, await request);
  }

  $async.Future<$0.Module> deleteEdge_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return deleteEdge(call, await request);
  }

  $async.Future<$0.Junction> getJunction_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getJunction(call, await request);
  }

  $async.Future<$0.Junction> updateJunction_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Junction> request) async {
    return updateJunction(call, await request);
  }

  $async.Future<$0.Junction> addSwitch_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return addSwitch(call, await request);
  }

  $async.Future<$0.Module> deleteJunction_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return deleteJunction(call, await request);
  }

  $async.Future<$0.Output> getOutput_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getOutput(call, await request);
  }

  $async.Future<$0.Output> updateOutput_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Output> request) async {
    return updateOutput(call, await request);
  }

  $async.Future<$0.Output> addBinaryOutput_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return addBinaryOutput(call, await request);
  }

  $async.Future<$0.Module> deleteOutput_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return deleteOutput(call, await request);
  }

  $async.Future<$0.Route> getRoute_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getRoute(call, await request);
  }

  $async.Future<$0.Route> updateRoute_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Route> request) async {
    return updateRoute(call, await request);
  }

  $async.Future<$0.Route> addRoute_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return addRoute(call, await request);
  }

  $async.Future<$0.Module> deleteRoute_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return deleteRoute(call, await request);
  }

  $async.Future<$0.Route> addRouteCrossingJunctionSwitch_Pre(
      $grpc.ServiceCall call,
      $async.Future<$2.AddRouteCrossingJunctionSwitchRequest> request) async {
    return addRouteCrossingJunctionSwitch(call, await request);
  }

  $async.Future<$0.Route> removeRouteCrossingJunction_Pre(
      $grpc.ServiceCall call,
      $async.Future<$2.RemoveRouteCrossingJunctionRequest> request) async {
    return removeRouteCrossingJunction(call, await request);
  }

  $async.Future<$0.Route> addRouteBinaryOutput_Pre($grpc.ServiceCall call,
      $async.Future<$2.AddRouteBinaryOutputRequest> request) async {
    return addRouteBinaryOutput(call, await request);
  }

  $async.Future<$0.Route> removeRouteOutput_Pre($grpc.ServiceCall call,
      $async.Future<$2.RemoveRouteOutputRequest> request) async {
    return removeRouteOutput(call, await request);
  }

  $async.Future<$0.Route> addRouteEvent_Pre($grpc.ServiceCall call,
      $async.Future<$2.AddRouteEventRequest> request) async {
    return addRouteEvent(call, await request);
  }

  $async.Future<$0.Route> moveRouteEventUp_Pre($grpc.ServiceCall call,
      $async.Future<$2.MoveRouteEventRequest> request) async {
    return moveRouteEventUp(call, await request);
  }

  $async.Future<$0.Route> moveRouteEventDown_Pre($grpc.ServiceCall call,
      $async.Future<$2.MoveRouteEventRequest> request) async {
    return moveRouteEventDown(call, await request);
  }

  $async.Future<$0.Route> removeRouteEvent_Pre($grpc.ServiceCall call,
      $async.Future<$2.RemoveRouteEventRequest> request) async {
    return removeRouteEvent(call, await request);
  }

  $async.Future<$0.Route> addRouteEventBehavior_Pre($grpc.ServiceCall call,
      $async.Future<$2.AddRouteEventBehaviorRequest> request) async {
    return addRouteEventBehavior(call, await request);
  }

  $async.Future<$0.Route> removeRouteEventBehavior_Pre($grpc.ServiceCall call,
      $async.Future<$2.RemoveRouteEventBehaviorRequest> request) async {
    return removeRouteEventBehavior(call, await request);
  }

  $async.Future<$0.Sensor> getSensor_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getSensor(call, await request);
  }

  $async.Future<$0.Sensor> updateSensor_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Sensor> request) async {
    return updateSensor(call, await request);
  }

  $async.Future<$0.Sensor> addBinarySensor_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return addBinarySensor(call, await request);
  }

  $async.Future<$0.Module> deleteSensor_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return deleteSensor(call, await request);
  }

  $async.Future<$0.Signal> getSignal_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getSignal(call, await request);
  }

  $async.Future<$0.Signal> addBlockSignal_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return addBlockSignal(call, await request);
  }

  $async.Future<$0.Signal> updateSignal_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Signal> request) async {
    return updateSignal(call, await request);
  }

  $async.Future<$0.Module> deleteSignal_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return deleteSignal(call, await request);
  }

  $async.Future<$0.BinkyNetLocalWorker> getBinkyNetLocalWorker_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return getBinkyNetLocalWorker(call, await request);
  }

  $async.Future<$0.BinkyNetLocalWorker> updateBinkyNetLocalWorker_Pre(
      $grpc.ServiceCall call,
      $async.Future<$0.BinkyNetLocalWorker> request) async {
    return updateBinkyNetLocalWorker(call, await request);
  }

  $async.Future<$0.CommandStation> deleteBinkyNetLocalWorker_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return deleteBinkyNetLocalWorker(call, await request);
  }

  $async.Future<$0.BinkyNetLocalWorker> addBinkyNetLocalWorker_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return addBinkyNetLocalWorker(call, await request);
  }

  $async.Future<$0.BinkyNetRouter> addBinkyNetRouter_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return addBinkyNetRouter(call, await request);
  }

  $async.Future<$0.BinkyNetLocalWorker> deleteBinkyNetRouter_Pre(
      $grpc.ServiceCall call, $async.Future<$2.SubIDRequest> request) async {
    return deleteBinkyNetRouter(call, await request);
  }

  $async.Future<$0.BinkyNetDevice> addBinkyNetDevice_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return addBinkyNetDevice(call, await request);
  }

  $async.Future<$0.BinkyNetLocalWorker> deleteBinkyNetDevice_Pre(
      $grpc.ServiceCall call, $async.Future<$2.SubIDRequest> request) async {
    return deleteBinkyNetDevice(call, await request);
  }

  $async.Future<$0.BinkyNetObject> addBinkyNetObject_Pre(
      $grpc.ServiceCall call, $async.Future<$2.IDRequest> request) async {
    return addBinkyNetObject(call, await request);
  }

  $async.Future<$0.BinkyNetLocalWorker> deleteBinkyNetObject_Pre(
      $grpc.ServiceCall call, $async.Future<$2.SubIDRequest> request) async {
    return deleteBinkyNetObject(call, await request);
  }

  $async.Future<$0.BinkyNetLocalWorker> addBinkyNetObjectsGroup_Pre(
      $grpc.ServiceCall call,
      $async.Future<$2.AddBinkyNetObjectsGroupRequest> request) async {
    return addBinkyNetObjectsGroup(call, await request);
  }

  $async.Future<$2.ParseAddressResult> parseAddress(
      $grpc.ServiceCall call, $2.ParseAddressRequest request);
  $async.Future<$2.ParsePermissionResult> parsePermission(
      $grpc.ServiceCall call, $2.ParsePermissionRequest request);
  $async.Future<$2.SerialPortList> getSerialPorts(
      $grpc.ServiceCall call, $0.Empty request);
  $async.Future<$0.Railway> loadRailway(
      $grpc.ServiceCall call, $3.RailwayEntry request);
  $async.Future<$0.Empty> closeRailway(
      $grpc.ServiceCall call, $0.Empty request);
  $async.Future<$3.RailwayEntry> getRailwayEntry(
      $grpc.ServiceCall call, $0.Empty request);
  $async.Future<$0.Railway> getRailway(
      $grpc.ServiceCall call, $0.Empty request);
  $async.Future<$0.Railway> updateRailway(
      $grpc.ServiceCall call, $0.Railway request);
  $async.Future<$0.Empty> save($grpc.ServiceCall call, $0.Empty request);
  $async.Future<$0.Module> getModule(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Image> getModuleBackgroundImage(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Module> updateModule(
      $grpc.ServiceCall call, $0.Module request);
  $async.Future<$0.Module> updateModuleBackgroundImage(
      $grpc.ServiceCall call, $2.ImageIDRequest request);
  $async.Future<$0.Module> addModule($grpc.ServiceCall call, $0.Empty request);
  $async.Future<$0.Empty> deleteModule(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Loc> getLoc($grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Loc> updateLoc($grpc.ServiceCall call, $0.Loc request);
  $async.Future<$0.Loc> updateLocImage(
      $grpc.ServiceCall call, $2.ImageIDRequest request);
  $async.Future<$0.Loc> addLoc($grpc.ServiceCall call, $0.Empty request);
  $async.Future<$0.Empty> deleteLoc(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.LocGroup> getLocGroup(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.LocGroup> updateLocGroup(
      $grpc.ServiceCall call, $0.LocGroup request);
  $async.Future<$0.LocGroup> addLocGroup(
      $grpc.ServiceCall call, $0.Empty request);
  $async.Future<$0.Empty> deleteLocGroup(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.CommandStation> getCommandStation(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.CommandStation> updateCommandStation(
      $grpc.ServiceCall call, $0.CommandStation request);
  $async.Future<$0.CommandStation> addBidibCommandStation(
      $grpc.ServiceCall call, $0.Empty request);
  $async.Future<$0.CommandStation> addBinkyNetCommandStation(
      $grpc.ServiceCall call, $0.Empty request);
  $async.Future<$0.Block> getBlock(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Block> updateBlock($grpc.ServiceCall call, $0.Block request);
  $async.Future<$0.Block> addBlock(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Module> deleteBlock(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.BlockGroup> getBlockGroup(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.BlockGroup> updateBlockGroup(
      $grpc.ServiceCall call, $0.BlockGroup request);
  $async.Future<$0.BlockGroup> addBlockGroup(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Module> deleteBlockGroup(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Edge> getEdge($grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Edge> updateEdge($grpc.ServiceCall call, $0.Edge request);
  $async.Future<$0.Edge> addEdge($grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Module> deleteEdge(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Junction> getJunction(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Junction> updateJunction(
      $grpc.ServiceCall call, $0.Junction request);
  $async.Future<$0.Junction> addSwitch(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Module> deleteJunction(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Output> getOutput(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Output> updateOutput(
      $grpc.ServiceCall call, $0.Output request);
  $async.Future<$0.Output> addBinaryOutput(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Module> deleteOutput(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Route> getRoute(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Route> updateRoute($grpc.ServiceCall call, $0.Route request);
  $async.Future<$0.Route> addRoute(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Module> deleteRoute(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Route> addRouteCrossingJunctionSwitch(
      $grpc.ServiceCall call, $2.AddRouteCrossingJunctionSwitchRequest request);
  $async.Future<$0.Route> removeRouteCrossingJunction(
      $grpc.ServiceCall call, $2.RemoveRouteCrossingJunctionRequest request);
  $async.Future<$0.Route> addRouteBinaryOutput(
      $grpc.ServiceCall call, $2.AddRouteBinaryOutputRequest request);
  $async.Future<$0.Route> removeRouteOutput(
      $grpc.ServiceCall call, $2.RemoveRouteOutputRequest request);
  $async.Future<$0.Route> addRouteEvent(
      $grpc.ServiceCall call, $2.AddRouteEventRequest request);
  $async.Future<$0.Route> moveRouteEventUp(
      $grpc.ServiceCall call, $2.MoveRouteEventRequest request);
  $async.Future<$0.Route> moveRouteEventDown(
      $grpc.ServiceCall call, $2.MoveRouteEventRequest request);
  $async.Future<$0.Route> removeRouteEvent(
      $grpc.ServiceCall call, $2.RemoveRouteEventRequest request);
  $async.Future<$0.Route> addRouteEventBehavior(
      $grpc.ServiceCall call, $2.AddRouteEventBehaviorRequest request);
  $async.Future<$0.Route> removeRouteEventBehavior(
      $grpc.ServiceCall call, $2.RemoveRouteEventBehaviorRequest request);
  $async.Future<$0.Sensor> getSensor(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Sensor> updateSensor(
      $grpc.ServiceCall call, $0.Sensor request);
  $async.Future<$0.Sensor> addBinarySensor(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Module> deleteSensor(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Signal> getSignal(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Signal> addBlockSignal(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.Signal> updateSignal(
      $grpc.ServiceCall call, $0.Signal request);
  $async.Future<$0.Module> deleteSignal(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.BinkyNetLocalWorker> getBinkyNetLocalWorker(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.BinkyNetLocalWorker> updateBinkyNetLocalWorker(
      $grpc.ServiceCall call, $0.BinkyNetLocalWorker request);
  $async.Future<$0.CommandStation> deleteBinkyNetLocalWorker(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.BinkyNetLocalWorker> addBinkyNetLocalWorker(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.BinkyNetRouter> addBinkyNetRouter(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.BinkyNetLocalWorker> deleteBinkyNetRouter(
      $grpc.ServiceCall call, $2.SubIDRequest request);
  $async.Future<$0.BinkyNetDevice> addBinkyNetDevice(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.BinkyNetLocalWorker> deleteBinkyNetDevice(
      $grpc.ServiceCall call, $2.SubIDRequest request);
  $async.Future<$0.BinkyNetObject> addBinkyNetObject(
      $grpc.ServiceCall call, $2.IDRequest request);
  $async.Future<$0.BinkyNetLocalWorker> deleteBinkyNetObject(
      $grpc.ServiceCall call, $2.SubIDRequest request);
  $async.Future<$0.BinkyNetLocalWorker> addBinkyNetObjectsGroup(
      $grpc.ServiceCall call, $2.AddBinkyNetObjectsGroupRequest request);
}
