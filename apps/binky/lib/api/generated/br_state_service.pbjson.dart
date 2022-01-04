///
//  Generated code. Do not modify.
//  source: br_state_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields,deprecated_member_use_from_same_package

import 'dart:core' as $core;
import 'dart:convert' as $convert;
import 'dart:typed_data' as $typed_data;
@$core.Deprecated('Use enableRunModeRequestDescriptor instead')
const EnableRunModeRequest$json = const {
  '1': 'EnableRunModeRequest',
  '2': const [
    const {'1': 'virtual', '3': 1, '4': 1, '5': 8, '10': 'virtual'},
  ],
};

/// Descriptor for `EnableRunModeRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List enableRunModeRequestDescriptor = $convert.base64Decode('ChRFbmFibGVSdW5Nb2RlUmVxdWVzdBIYCgd2aXJ0dWFsGAEgASgIUgd2aXJ0dWFs');
@$core.Deprecated('Use getStateChangesRequestDescriptor instead')
const GetStateChangesRequest$json = const {
  '1': 'GetStateChangesRequest',
  '2': const [
    const {'1': 'bootstrap', '3': 1, '4': 1, '5': 8, '10': 'bootstrap'},
  ],
};

/// Descriptor for `GetStateChangesRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List getStateChangesRequestDescriptor = $convert.base64Decode('ChZHZXRTdGF0ZUNoYW5nZXNSZXF1ZXN0EhwKCWJvb3RzdHJhcBgBIAEoCFIJYm9vdHN0cmFw');
@$core.Deprecated('Use stateChangeDescriptor instead')
const StateChange$json = const {
  '1': 'StateChange',
  '2': const [
    const {'1': 'railway', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.RailwayState', '10': 'railway'},
    const {'1': 'loc', '3': 2, '4': 1, '5': 11, '6': '.binkyrailways.v1.LocState', '10': 'loc'},
    const {'1': 'commandStation', '3': 3, '4': 1, '5': 11, '6': '.binkyrailways.v1.CommandStationState', '10': 'commandStation'},
    const {'1': 'block', '3': 4, '4': 1, '5': 11, '6': '.binkyrailways.v1.BlockState', '10': 'block'},
    const {'1': 'blockGroup', '3': 5, '4': 1, '5': 11, '6': '.binkyrailways.v1.BlockGroupState', '10': 'blockGroup'},
    const {'1': 'junction', '3': 6, '4': 1, '5': 11, '6': '.binkyrailways.v1.JunctionState', '10': 'junction'},
    const {'1': 'output', '3': 7, '4': 1, '5': 11, '6': '.binkyrailways.v1.OutputState', '10': 'output'},
    const {'1': 'route', '3': 8, '4': 1, '5': 11, '6': '.binkyrailways.v1.RouteState', '10': 'route'},
    const {'1': 'sensor', '3': 9, '4': 1, '5': 11, '6': '.binkyrailways.v1.SensorState', '10': 'sensor'},
    const {'1': 'signal', '3': 10, '4': 1, '5': 11, '6': '.binkyrailways.v1.SignalState', '10': 'signal'},
  ],
};

/// Descriptor for `StateChange`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List stateChangeDescriptor = $convert.base64Decode('CgtTdGF0ZUNoYW5nZRI4CgdyYWlsd2F5GAEgASgLMh4uYmlua3lyYWlsd2F5cy52MS5SYWlsd2F5U3RhdGVSB3JhaWx3YXkSLAoDbG9jGAIgASgLMhouYmlua3lyYWlsd2F5cy52MS5Mb2NTdGF0ZVIDbG9jEk0KDmNvbW1hbmRTdGF0aW9uGAMgASgLMiUuYmlua3lyYWlsd2F5cy52MS5Db21tYW5kU3RhdGlvblN0YXRlUg5jb21tYW5kU3RhdGlvbhIyCgVibG9jaxgEIAEoCzIcLmJpbmt5cmFpbHdheXMudjEuQmxvY2tTdGF0ZVIFYmxvY2sSQQoKYmxvY2tHcm91cBgFIAEoCzIhLmJpbmt5cmFpbHdheXMudjEuQmxvY2tHcm91cFN0YXRlUgpibG9ja0dyb3VwEjsKCGp1bmN0aW9uGAYgASgLMh8uYmlua3lyYWlsd2F5cy52MS5KdW5jdGlvblN0YXRlUghqdW5jdGlvbhI1CgZvdXRwdXQYByABKAsyHS5iaW5reXJhaWx3YXlzLnYxLk91dHB1dFN0YXRlUgZvdXRwdXQSMgoFcm91dGUYCCABKAsyHC5iaW5reXJhaWx3YXlzLnYxLlJvdXRlU3RhdGVSBXJvdXRlEjUKBnNlbnNvchgJIAEoCzIdLmJpbmt5cmFpbHdheXMudjEuU2Vuc29yU3RhdGVSBnNlbnNvchI1CgZzaWduYWwYCiABKAsyHS5iaW5reXJhaWx3YXlzLnYxLlNpZ25hbFN0YXRlUgZzaWduYWw=');
@$core.Deprecated('Use setPowerRequestDescriptor instead')
const SetPowerRequest$json = const {
  '1': 'SetPowerRequest',
  '2': const [
    const {'1': 'enabled', '3': 1, '4': 1, '5': 8, '10': 'enabled'},
  ],
};

/// Descriptor for `SetPowerRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List setPowerRequestDescriptor = $convert.base64Decode('Cg9TZXRQb3dlclJlcXVlc3QSGAoHZW5hYmxlZBgBIAEoCFIHZW5hYmxlZA==');
@$core.Deprecated('Use setLocSpeedAndDirectionRequestDescriptor instead')
const SetLocSpeedAndDirectionRequest$json = const {
  '1': 'SetLocSpeedAndDirectionRequest',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'speed', '3': 2, '4': 1, '5': 5, '10': 'speed'},
    const {'1': 'direction', '3': 3, '4': 1, '5': 14, '6': '.binkyrailways.v1.LocDirection', '10': 'direction'},
  ],
};

/// Descriptor for `SetLocSpeedAndDirectionRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List setLocSpeedAndDirectionRequestDescriptor = $convert.base64Decode('Ch5TZXRMb2NTcGVlZEFuZERpcmVjdGlvblJlcXVlc3QSDgoCaWQYASABKAlSAmlkEhQKBXNwZWVkGAIgASgFUgVzcGVlZBI8CglkaXJlY3Rpb24YAyABKA4yHi5iaW5reXJhaWx3YXlzLnYxLkxvY0RpcmVjdGlvblIJZGlyZWN0aW9u');
@$core.Deprecated('Use setSwitchDirectionRequestDescriptor instead')
const SetSwitchDirectionRequest$json = const {
  '1': 'SetSwitchDirectionRequest',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'direction', '3': 2, '4': 1, '5': 14, '6': '.binkyrailways.v1.SwitchDirection', '10': 'direction'},
  ],
};

/// Descriptor for `SetSwitchDirectionRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List setSwitchDirectionRequestDescriptor = $convert.base64Decode('ChlTZXRTd2l0Y2hEaXJlY3Rpb25SZXF1ZXN0Eg4KAmlkGAEgASgJUgJpZBI/CglkaXJlY3Rpb24YAiABKA4yIS5iaW5reXJhaWx3YXlzLnYxLlN3aXRjaERpcmVjdGlvblIJZGlyZWN0aW9u');
@$core.Deprecated('Use setBinaryOutputActiveRequestDescriptor instead')
const SetBinaryOutputActiveRequest$json = const {
  '1': 'SetBinaryOutputActiveRequest',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'active', '3': 2, '4': 1, '5': 8, '10': 'active'},
  ],
};

/// Descriptor for `SetBinaryOutputActiveRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List setBinaryOutputActiveRequestDescriptor = $convert.base64Decode('ChxTZXRCaW5hcnlPdXRwdXRBY3RpdmVSZXF1ZXN0Eg4KAmlkGAEgASgJUgJpZBIWCgZhY3RpdmUYAiABKAhSBmFjdGl2ZQ==');
