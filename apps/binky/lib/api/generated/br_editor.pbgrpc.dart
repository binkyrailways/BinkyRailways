///
//  Generated code. Do not modify.
//  source: br_editor.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields

import 'dart:async' as $async;

import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'br_types.pb.dart' as $0;
import 'br_editor.pb.dart' as $1;
export 'br_editor.pb.dart';

class EditorServiceClient extends $grpc.Client {
  static final _$getRailway = $grpc.ClientMethod<$0.Empty, $0.Railway>(
      '/binkyrailways.v1.EditorService/GetRailway',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Railway.fromBuffer(value));
  static final _$updateRailway = $grpc.ClientMethod<$0.Railway, $0.Railway>(
      '/binkyrailways.v1.EditorService/UpdateRailway',
      ($0.Railway value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Railway.fromBuffer(value));
  static final _$save = $grpc.ClientMethod<$0.Empty, $0.Empty>(
      '/binkyrailways.v1.EditorService/Save',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Empty.fromBuffer(value));
  static final _$getModule = $grpc.ClientMethod<$1.IDRequest, $0.Module>(
      '/binkyrailways.v1.EditorService/GetModule',
      ($1.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$updateModule = $grpc.ClientMethod<$0.Module, $0.Module>(
      '/binkyrailways.v1.EditorService/UpdateModule',
      ($0.Module value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Module.fromBuffer(value));
  static final _$getLoc = $grpc.ClientMethod<$1.IDRequest, $0.Loc>(
      '/binkyrailways.v1.EditorService/GetLoc',
      ($1.IDRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Loc.fromBuffer(value));
  static final _$updateLoc = $grpc.ClientMethod<$0.Loc, $0.Loc>(
      '/binkyrailways.v1.EditorService/UpdateLoc',
      ($0.Loc value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Loc.fromBuffer(value));

  EditorServiceClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options, interceptors: interceptors);

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

  $grpc.ResponseFuture<$0.Module> getModule($1.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getModule, request, options: options);
  }

  $grpc.ResponseFuture<$0.Module> updateModule($0.Module request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateModule, request, options: options);
  }

  $grpc.ResponseFuture<$0.Loc> getLoc($1.IDRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getLoc, request, options: options);
  }

  $grpc.ResponseFuture<$0.Loc> updateLoc($0.Loc request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$updateLoc, request, options: options);
  }
}

abstract class EditorServiceBase extends $grpc.Service {
  $core.String get $name => 'binkyrailways.v1.EditorService';

  EditorServiceBase() {
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
    $addMethod($grpc.ServiceMethod<$1.IDRequest, $0.Module>(
        'GetModule',
        getModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.IDRequest.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Module, $0.Module>(
        'UpdateModule',
        updateModule_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Module.fromBuffer(value),
        ($0.Module value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.IDRequest, $0.Loc>(
        'GetLoc',
        getLoc_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.IDRequest.fromBuffer(value),
        ($0.Loc value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Loc, $0.Loc>(
        'UpdateLoc',
        updateLoc_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Loc.fromBuffer(value),
        ($0.Loc value) => value.writeToBuffer()));
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
      $grpc.ServiceCall call, $async.Future<$1.IDRequest> request) async {
    return getModule(call, await request);
  }

  $async.Future<$0.Module> updateModule_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Module> request) async {
    return updateModule(call, await request);
  }

  $async.Future<$0.Loc> getLoc_Pre(
      $grpc.ServiceCall call, $async.Future<$1.IDRequest> request) async {
    return getLoc(call, await request);
  }

  $async.Future<$0.Loc> updateLoc_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Loc> request) async {
    return updateLoc(call, await request);
  }

  $async.Future<$0.Railway> getRailway(
      $grpc.ServiceCall call, $0.Empty request);
  $async.Future<$0.Railway> updateRailway(
      $grpc.ServiceCall call, $0.Railway request);
  $async.Future<$0.Empty> save($grpc.ServiceCall call, $0.Empty request);
  $async.Future<$0.Module> getModule(
      $grpc.ServiceCall call, $1.IDRequest request);
  $async.Future<$0.Module> updateModule(
      $grpc.ServiceCall call, $0.Module request);
  $async.Future<$0.Loc> getLoc($grpc.ServiceCall call, $1.IDRequest request);
  $async.Future<$0.Loc> updateLoc($grpc.ServiceCall call, $0.Loc request);
}
