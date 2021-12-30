///
//  Generated code. Do not modify.
//  source: br_state_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields,deprecated_member_use_from_same_package

import 'dart:core' as $core;
import 'dart:convert' as $convert;
import 'dart:typed_data' as $typed_data;
@$core.Deprecated('Use railwayStateDescriptor instead')
const RailwayState$json = const {
  '1': 'RailwayState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.Railway', '10': 'model'},
    const {'1': 'is_run_mode_enabled', '3': 2, '4': 1, '5': 8, '10': 'isRunModeEnabled'},
    const {'1': 'is_virtual_mode_enabled', '3': 3, '4': 1, '5': 8, '10': 'isVirtualModeEnabled'},
    const {'1': 'is_virtual_autorun_enabled', '3': 4, '4': 1, '5': 8, '10': 'isVirtualAutorunEnabled'},
    const {'1': 'power_actual', '3': 10, '4': 1, '5': 8, '10': 'powerActual'},
    const {'1': 'power_requested', '3': 11, '4': 1, '5': 8, '10': 'powerRequested'},
  ],
};

/// Descriptor for `RailwayState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List railwayStateDescriptor = $convert.base64Decode('CgxSYWlsd2F5U3RhdGUSLwoFbW9kZWwYASABKAsyGS5iaW5reXJhaWx3YXlzLnYxLlJhaWx3YXlSBW1vZGVsEi0KE2lzX3J1bl9tb2RlX2VuYWJsZWQYAiABKAhSEGlzUnVuTW9kZUVuYWJsZWQSNQoXaXNfdmlydHVhbF9tb2RlX2VuYWJsZWQYAyABKAhSFGlzVmlydHVhbE1vZGVFbmFibGVkEjsKGmlzX3ZpcnR1YWxfYXV0b3J1bl9lbmFibGVkGAQgASgIUhdpc1ZpcnR1YWxBdXRvcnVuRW5hYmxlZBIhCgxwb3dlcl9hY3R1YWwYCiABKAhSC3Bvd2VyQWN0dWFsEicKD3Bvd2VyX3JlcXVlc3RlZBgLIAEoCFIOcG93ZXJSZXF1ZXN0ZWQ=');
@$core.Deprecated('Use locStateDescriptor instead')
const LocState$json = const {
  '1': 'LocState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.Loc', '10': 'model'},
  ],
};

/// Descriptor for `LocState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List locStateDescriptor = $convert.base64Decode('CghMb2NTdGF0ZRIrCgVtb2RlbBgBIAEoCzIVLmJpbmt5cmFpbHdheXMudjEuTG9jUgVtb2RlbA==');
@$core.Deprecated('Use commandStationStateDescriptor instead')
const CommandStationState$json = const {
  '1': 'CommandStationState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.CommandStation', '10': 'model'},
  ],
};

/// Descriptor for `CommandStationState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List commandStationStateDescriptor = $convert.base64Decode('ChNDb21tYW5kU3RhdGlvblN0YXRlEjYKBW1vZGVsGAEgASgLMiAuYmlua3lyYWlsd2F5cy52MS5Db21tYW5kU3RhdGlvblIFbW9kZWw=');
@$core.Deprecated('Use blockStateDescriptor instead')
const BlockState$json = const {
  '1': 'BlockState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.Block', '10': 'model'},
    const {'1': 'closed_actual', '3': 10, '4': 1, '5': 8, '10': 'closedActual'},
    const {'1': 'closed_requested', '3': 11, '4': 1, '5': 8, '10': 'closedRequested'},
    const {'1': 'is_deadend', '3': 20, '4': 1, '5': 8, '10': 'isDeadend'},
    const {'1': 'is_station', '3': 21, '4': 1, '5': 8, '10': 'isStation'},
    const {'1': 'has_waiting_loc', '3': 22, '4': 1, '5': 8, '10': 'hasWaitingLoc'},
  ],
};

/// Descriptor for `BlockState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List blockStateDescriptor = $convert.base64Decode('CgpCbG9ja1N0YXRlEi0KBW1vZGVsGAEgASgLMhcuYmlua3lyYWlsd2F5cy52MS5CbG9ja1IFbW9kZWwSIwoNY2xvc2VkX2FjdHVhbBgKIAEoCFIMY2xvc2VkQWN0dWFsEikKEGNsb3NlZF9yZXF1ZXN0ZWQYCyABKAhSD2Nsb3NlZFJlcXVlc3RlZBIdCgppc19kZWFkZW5kGBQgASgIUglpc0RlYWRlbmQSHQoKaXNfc3RhdGlvbhgVIAEoCFIJaXNTdGF0aW9uEiYKD2hhc193YWl0aW5nX2xvYxgWIAEoCFINaGFzV2FpdGluZ0xvYw==');
@$core.Deprecated('Use blockGroupStateDescriptor instead')
const BlockGroupState$json = const {
  '1': 'BlockGroupState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.BlockGroup', '10': 'model'},
  ],
};

/// Descriptor for `BlockGroupState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List blockGroupStateDescriptor = $convert.base64Decode('Cg9CbG9ja0dyb3VwU3RhdGUSMgoFbW9kZWwYASABKAsyHC5iaW5reXJhaWx3YXlzLnYxLkJsb2NrR3JvdXBSBW1vZGVs');
@$core.Deprecated('Use junctionStateDescriptor instead')
const JunctionState$json = const {
  '1': 'JunctionState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.Junction', '10': 'model'},
  ],
};

/// Descriptor for `JunctionState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List junctionStateDescriptor = $convert.base64Decode('Cg1KdW5jdGlvblN0YXRlEjAKBW1vZGVsGAEgASgLMhouYmlua3lyYWlsd2F5cy52MS5KdW5jdGlvblIFbW9kZWw=');
@$core.Deprecated('Use outputStateDescriptor instead')
const OutputState$json = const {
  '1': 'OutputState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.Output', '10': 'model'},
  ],
};

/// Descriptor for `OutputState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List outputStateDescriptor = $convert.base64Decode('CgtPdXRwdXRTdGF0ZRIuCgVtb2RlbBgBIAEoCzIYLmJpbmt5cmFpbHdheXMudjEuT3V0cHV0UgVtb2RlbA==');
@$core.Deprecated('Use routeStateDescriptor instead')
const RouteState$json = const {
  '1': 'RouteState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.Route', '10': 'model'},
  ],
};

/// Descriptor for `RouteState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List routeStateDescriptor = $convert.base64Decode('CgpSb3V0ZVN0YXRlEi0KBW1vZGVsGAEgASgLMhcuYmlua3lyYWlsd2F5cy52MS5Sb3V0ZVIFbW9kZWw=');
@$core.Deprecated('Use sensorStateDescriptor instead')
const SensorState$json = const {
  '1': 'SensorState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.Sensor', '10': 'model'},
  ],
};

/// Descriptor for `SensorState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List sensorStateDescriptor = $convert.base64Decode('CgtTZW5zb3JTdGF0ZRIuCgVtb2RlbBgBIAEoCzIYLmJpbmt5cmFpbHdheXMudjEuU2Vuc29yUgVtb2RlbA==');
@$core.Deprecated('Use signalStateDescriptor instead')
const SignalState$json = const {
  '1': 'SignalState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.Signal', '10': 'model'},
  ],
};

/// Descriptor for `SignalState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List signalStateDescriptor = $convert.base64Decode('CgtTaWduYWxTdGF0ZRIuCgVtb2RlbBgBIAEoCzIYLmJpbmt5cmFpbHdheXMudjEuU2lnbmFsUgVtb2RlbA==');
