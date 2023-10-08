///
//  Generated code. Do not modify.
//  source: br_storage_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

import 'dart:async' as $async;

import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'br_storage_service.pb.dart' as $5;
import 'br_storage_types.pb.dart' as $1;
export 'br_storage_service.pb.dart';

class StorageServiceClient extends $grpc.Client {
  static final _$createRailwayEntry =
      $grpc.ClientMethod<$5.CreateRailwayEntryRequest, $1.RailwayEntry>(
          '/binkyrailways.v1.StorageService/CreateRailwayEntry',
          ($5.CreateRailwayEntryRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $1.RailwayEntry.fromBuffer(value));
  static final _$getRailwayEntries =
      $grpc.ClientMethod<$5.GetRailwayEntriesRequest, $1.RailwayEntryList>(
          '/binkyrailways.v1.StorageService/GetRailwayEntries',
          ($5.GetRailwayEntriesRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $1.RailwayEntryList.fromBuffer(value));

  StorageServiceClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options, interceptors: interceptors);

  $grpc.ResponseFuture<$1.RailwayEntry> createRailwayEntry(
      $5.CreateRailwayEntryRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$createRailwayEntry, request, options: options);
  }

  $grpc.ResponseFuture<$1.RailwayEntryList> getRailwayEntries(
      $5.GetRailwayEntriesRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getRailwayEntries, request, options: options);
  }
}

abstract class StorageServiceBase extends $grpc.Service {
  $core.String get $name => 'binkyrailways.v1.StorageService';

  StorageServiceBase() {
    $addMethod(
        $grpc.ServiceMethod<$5.CreateRailwayEntryRequest, $1.RailwayEntry>(
            'CreateRailwayEntry',
            createRailwayEntry_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $5.CreateRailwayEntryRequest.fromBuffer(value),
            ($1.RailwayEntry value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$5.GetRailwayEntriesRequest, $1.RailwayEntryList>(
            'GetRailwayEntries',
            getRailwayEntries_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $5.GetRailwayEntriesRequest.fromBuffer(value),
            ($1.RailwayEntryList value) => value.writeToBuffer()));
  }

  $async.Future<$1.RailwayEntry> createRailwayEntry_Pre($grpc.ServiceCall call,
      $async.Future<$5.CreateRailwayEntryRequest> request) async {
    return createRailwayEntry(call, await request);
  }

  $async.Future<$1.RailwayEntryList> getRailwayEntries_Pre(
      $grpc.ServiceCall call,
      $async.Future<$5.GetRailwayEntriesRequest> request) async {
    return getRailwayEntries(call, await request);
  }

  $async.Future<$1.RailwayEntry> createRailwayEntry(
      $grpc.ServiceCall call, $5.CreateRailwayEntryRequest request);
  $async.Future<$1.RailwayEntryList> getRailwayEntries(
      $grpc.ServiceCall call, $5.GetRailwayEntriesRequest request);
}
