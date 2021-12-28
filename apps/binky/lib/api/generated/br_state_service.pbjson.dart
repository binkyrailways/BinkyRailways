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
    const {'1': 'block', '3': 2, '4': 1, '5': 11, '6': '.binkyrailways.v1.BlockState', '10': 'block'},
    const {'1': 'loc', '3': 3, '4': 1, '5': 11, '6': '.binkyrailways.v1.LocState', '10': 'loc'},
  ],
};

/// Descriptor for `StateChange`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List stateChangeDescriptor = $convert.base64Decode('CgtTdGF0ZUNoYW5nZRI4CgdyYWlsd2F5GAEgASgLMh4uYmlua3lyYWlsd2F5cy52MS5SYWlsd2F5U3RhdGVSB3JhaWx3YXkSMgoFYmxvY2sYAiABKAsyHC5iaW5reXJhaWx3YXlzLnYxLkJsb2NrU3RhdGVSBWJsb2NrEiwKA2xvYxgDIAEoCzIaLmJpbmt5cmFpbHdheXMudjEuTG9jU3RhdGVSA2xvYw==');
