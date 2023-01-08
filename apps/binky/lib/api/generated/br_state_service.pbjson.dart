///
//  Generated code. Do not modify.
//  source: br_state_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,deprecated_member_use_from_same_package,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

import 'dart:core' as $core;
import 'dart:convert' as $convert;
import 'dart:typed_data' as $typed_data;
@$core.Deprecated('Use enableRunModeRequestDescriptor instead')
const EnableRunModeRequest$json = const {
  '1': 'EnableRunModeRequest',
  '2': const [
    const {'1': 'virtual', '3': 1, '4': 1, '5': 8, '10': 'virtual'},
    const {'1': 'auto_run', '3': 2, '4': 1, '5': 8, '10': 'autoRun'},
  ],
};

/// Descriptor for `EnableRunModeRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List enableRunModeRequestDescriptor = $convert.base64Decode('ChRFbmFibGVSdW5Nb2RlUmVxdWVzdBIYCgd2aXJ0dWFsGAEgASgIUgd2aXJ0dWFsEhkKCGF1dG9fcnVuGAIgASgIUgdhdXRvUnVu');
@$core.Deprecated('Use getStateChangesRequestDescriptor instead')
const GetStateChangesRequest$json = const {
  '1': 'GetStateChangesRequest',
  '2': const [
    const {'1': 'bootstrap', '3': 1, '4': 1, '5': 8, '10': 'bootstrap'},
    const {'1': 'bootstrap_only', '3': 2, '4': 1, '5': 8, '10': 'bootstrapOnly'},
  ],
};

/// Descriptor for `GetStateChangesRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List getStateChangesRequestDescriptor = $convert.base64Decode('ChZHZXRTdGF0ZUNoYW5nZXNSZXF1ZXN0EhwKCWJvb3RzdHJhcBgBIAEoCFIJYm9vdHN0cmFwEiUKDmJvb3RzdHJhcF9vbmx5GAIgASgIUg1ib290c3RyYXBPbmx5');
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
@$core.Deprecated('Use setAutomaticControlRequestDescriptor instead')
const SetAutomaticControlRequest$json = const {
  '1': 'SetAutomaticControlRequest',
  '2': const [
    const {'1': 'enabled', '3': 1, '4': 1, '5': 8, '10': 'enabled'},
  ],
};

/// Descriptor for `SetAutomaticControlRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List setAutomaticControlRequestDescriptor = $convert.base64Decode('ChpTZXRBdXRvbWF0aWNDb250cm9sUmVxdWVzdBIYCgdlbmFibGVkGAEgASgIUgdlbmFibGVk');
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
@$core.Deprecated('Use setLocControlledAutomaticallyRequestDescriptor instead')
const SetLocControlledAutomaticallyRequest$json = const {
  '1': 'SetLocControlledAutomaticallyRequest',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'enabled', '3': 2, '4': 1, '5': 8, '10': 'enabled'},
  ],
};

/// Descriptor for `SetLocControlledAutomaticallyRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List setLocControlledAutomaticallyRequestDescriptor = $convert.base64Decode('CiRTZXRMb2NDb250cm9sbGVkQXV0b21hdGljYWxseVJlcXVlc3QSDgoCaWQYASABKAlSAmlkEhgKB2VuYWJsZWQYAiABKAhSB2VuYWJsZWQ=');
@$core.Deprecated('Use setLocFunctionsRequestDescriptor instead')
const SetLocFunctionsRequest$json = const {
  '1': 'SetLocFunctionsRequest',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'functions', '3': 2, '4': 3, '5': 11, '6': '.binkyrailways.v1.LocFunction', '10': 'functions'},
  ],
};

/// Descriptor for `SetLocFunctionsRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List setLocFunctionsRequestDescriptor = $convert.base64Decode('ChZTZXRMb2NGdW5jdGlvbnNSZXF1ZXN0Eg4KAmlkGAEgASgJUgJpZBI7CglmdW5jdGlvbnMYAiADKAsyHS5iaW5reXJhaWx3YXlzLnYxLkxvY0Z1bmN0aW9uUglmdW5jdGlvbnM=');
@$core.Deprecated('Use locFunctionDescriptor instead')
const LocFunction$json = const {
  '1': 'LocFunction',
  '2': const [
    const {'1': 'index', '3': 1, '4': 1, '5': 5, '10': 'index'},
    const {'1': 'value', '3': 2, '4': 1, '5': 8, '10': 'value'},
  ],
};

/// Descriptor for `LocFunction`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List locFunctionDescriptor = $convert.base64Decode('CgtMb2NGdW5jdGlvbhIUCgVpbmRleBgBIAEoBVIFaW5kZXgSFAoFdmFsdWUYAiABKAhSBXZhbHVl');
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
@$core.Deprecated('Use clickVirtualSensorRequestDescriptor instead')
const ClickVirtualSensorRequest$json = const {
  '1': 'ClickVirtualSensorRequest',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `ClickVirtualSensorRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List clickVirtualSensorRequestDescriptor = $convert.base64Decode('ChlDbGlja1ZpcnR1YWxTZW5zb3JSZXF1ZXN0Eg4KAmlkGAEgASgJUgJpZA==');
@$core.Deprecated('Use assignLocToBlockRequestDescriptor instead')
const AssignLocToBlockRequest$json = const {
  '1': 'AssignLocToBlockRequest',
  '2': const [
    const {'1': 'loc_id', '3': 1, '4': 1, '5': 9, '10': 'locId'},
    const {'1': 'block_id', '3': 2, '4': 1, '5': 9, '10': 'blockId'},
    const {'1': 'block_side', '3': 3, '4': 1, '5': 14, '6': '.binkyrailways.v1.BlockSide', '10': 'blockSide'},
  ],
};

/// Descriptor for `AssignLocToBlockRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List assignLocToBlockRequestDescriptor = $convert.base64Decode('ChdBc3NpZ25Mb2NUb0Jsb2NrUmVxdWVzdBIVCgZsb2NfaWQYASABKAlSBWxvY0lkEhkKCGJsb2NrX2lkGAIgASgJUgdibG9ja0lkEjoKCmJsb2NrX3NpZGUYAyABKA4yGy5iaW5reXJhaWx3YXlzLnYxLkJsb2NrU2lkZVIJYmxvY2tTaWRl');
@$core.Deprecated('Use takeLocOfTrackRequestDescriptor instead')
const TakeLocOfTrackRequest$json = const {
  '1': 'TakeLocOfTrackRequest',
  '2': const [
    const {'1': 'loc_id', '3': 1, '4': 1, '5': 9, '10': 'locId'},
  ],
};

/// Descriptor for `TakeLocOfTrackRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List takeLocOfTrackRequestDescriptor = $convert.base64Decode('ChVUYWtlTG9jT2ZUcmFja1JlcXVlc3QSFQoGbG9jX2lkGAEgASgJUgVsb2NJZA==');
@$core.Deprecated('Use setBlockClosedRequestDescriptor instead')
const SetBlockClosedRequest$json = const {
  '1': 'SetBlockClosedRequest',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'closed', '3': 2, '4': 1, '5': 8, '10': 'closed'},
  ],
};

/// Descriptor for `SetBlockClosedRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List setBlockClosedRequestDescriptor = $convert.base64Decode('ChVTZXRCbG9ja0Nsb3NlZFJlcXVlc3QSDgoCaWQYASABKAlSAmlkEhYKBmNsb3NlZBgCIAEoCFIGY2xvc2Vk');
@$core.Deprecated('Use discoverHardwareRequestDescriptor instead')
const DiscoverHardwareRequest$json = const {
  '1': 'DiscoverHardwareRequest',
  '2': const [
    const {'1': 'hardware_module_id', '3': 1, '4': 1, '5': 9, '10': 'hardwareModuleId'},
  ],
};

/// Descriptor for `DiscoverHardwareRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List discoverHardwareRequestDescriptor = $convert.base64Decode('ChdEaXNjb3ZlckhhcmR3YXJlUmVxdWVzdBIsChJoYXJkd2FyZV9tb2R1bGVfaWQYASABKAlSEGhhcmR3YXJlTW9kdWxlSWQ=');
@$core.Deprecated('Use discoverHardwareResponseDescriptor instead')
const DiscoverHardwareResponse$json = const {
  '1': 'DiscoverHardwareResponse',
};

/// Descriptor for `DiscoverHardwareResponse`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List discoverHardwareResponseDescriptor = $convert.base64Decode('ChhEaXNjb3ZlckhhcmR3YXJlUmVzcG9uc2U=');
