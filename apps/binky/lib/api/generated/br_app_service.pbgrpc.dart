///
//  Generated code. Do not modify.
//  source: br_app_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

import 'dart:async' as $async;

import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'br_model_types.pb.dart' as $0;
import 'br_app_types.pb.dart' as $1;
export 'br_app_service.pb.dart';

class AppServiceClient extends $grpc.Client {
  static final _$getAppInfo = $grpc.ClientMethod<$0.Empty, $1.AppInfo>(
      '/binkyrailways.v1.AppService/GetAppInfo',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.AppInfo.fromBuffer(value));

  AppServiceClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options, interceptors: interceptors);

  $grpc.ResponseFuture<$1.AppInfo> getAppInfo($0.Empty request,
      {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getAppInfo, request, options: options);
  }
}

abstract class AppServiceBase extends $grpc.Service {
  $core.String get $name => 'binkyrailways.v1.AppService';

  AppServiceBase() {
    $addMethod($grpc.ServiceMethod<$0.Empty, $1.AppInfo>(
        'GetAppInfo',
        getAppInfo_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($1.AppInfo value) => value.writeToBuffer()));
  }

  $async.Future<$1.AppInfo> getAppInfo_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return getAppInfo(call, await request);
  }

  $async.Future<$1.AppInfo> getAppInfo(
      $grpc.ServiceCall call, $0.Empty request);
}
