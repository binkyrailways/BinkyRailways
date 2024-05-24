///
//  Generated code. Do not modify.
//  source: br_storage_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

import 'dart:async' as $async;

import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'br_storage_service.pb.dart' as $6;
import 'br_storage_types.pb.dart' as $3;
export 'br_storage_service.pb.dart';

class StorageServiceClient extends $grpc.Client {
  static final _$createRailwayEntry =
      $grpc.ClientMethod<$6.CreateRailwayEntryRequest, $3.RailwayEntry>(
          '/binkyrailways.v1.StorageService/CreateRailwayEntry',
          ($6.CreateRailwayEntryRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $3.RailwayEntry.fromBuffer(value));
  static final _$getRailwayEntries =
      $grpc.ClientMethod<$6.GetRailwayEntriesRequest, $3.RailwayEntryList>(
          '/binkyrailways.v1.StorageService/GetRailwayEntries',
          ($6.GetRailwayEntriesRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $3.RailwayEntryList.fromBuffer(value));

  StorageServiceClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options, interceptors: interceptors);

  $grpc.ResponseFuture<$3.RailwayEntry> createRailwayEntry(
      $6.CreateRailwayEntryRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$createRailwayEntry, request, options: options);
  }

  $grpc.ResponseFuture<$3.RailwayEntryList> getRailwayEntries(
      $6.GetRailwayEntriesRequest request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getRailwayEntries, request, options: options);
  }
}

abstract class StorageServiceBase extends $grpc.Service {
  $core.String get $name => 'binkyrailways.v1.StorageService';

  StorageServiceBase() {
    $addMethod(
        $grpc.ServiceMethod<$6.CreateRailwayEntryRequest, $3.RailwayEntry>(
            'CreateRailwayEntry',
            createRailwayEntry_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $6.CreateRailwayEntryRequest.fromBuffer(value),
            ($3.RailwayEntry value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$6.GetRailwayEntriesRequest, $3.RailwayEntryList>(
            'GetRailwayEntries',
            getRailwayEntries_Pre,
            false,
            false,
            ($core.List<$core.int> value) =>
                $6.GetRailwayEntriesRequest.fromBuffer(value),
            ($3.RailwayEntryList value) => value.writeToBuffer()));
  }

  $async.Future<$3.RailwayEntry> createRailwayEntry_Pre($grpc.ServiceCall call,
      $async.Future<$6.CreateRailwayEntryRequest> request) async {
    return createRailwayEntry(call, await request);
  }

  $async.Future<$3.RailwayEntryList> getRailwayEntries_Pre(
      $grpc.ServiceCall call,
      $async.Future<$6.GetRailwayEntriesRequest> request) async {
    return getRailwayEntries(call, await request);
  }

  $async.Future<$3.RailwayEntry> createRailwayEntry(
      $grpc.ServiceCall call, $6.CreateRailwayEntryRequest request);
  $async.Future<$3.RailwayEntryList> getRailwayEntries(
      $grpc.ServiceCall call, $6.GetRailwayEntriesRequest request);
}
