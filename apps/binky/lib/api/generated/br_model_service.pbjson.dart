///
//  Generated code. Do not modify.
//  source: br_model_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields,deprecated_member_use_from_same_package

import 'dart:core' as $core;
import 'dart:convert' as $convert;
import 'dart:typed_data' as $typed_data;
@$core.Deprecated('Use iDRequestDescriptor instead')
const IDRequest$json = const {
  '1': 'IDRequest',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `IDRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List iDRequestDescriptor = $convert.base64Decode('CglJRFJlcXVlc3QSDgoCaWQYASABKAlSAmlk');
@$core.Deprecated('Use parseAddressRequestDescriptor instead')
const ParseAddressRequest$json = const {
  '1': 'ParseAddressRequest',
  '2': const [
    const {'1': 'value', '3': 1, '4': 1, '5': 9, '10': 'value'},
  ],
};

/// Descriptor for `ParseAddressRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List parseAddressRequestDescriptor = $convert.base64Decode('ChNQYXJzZUFkZHJlc3NSZXF1ZXN0EhQKBXZhbHVlGAEgASgJUgV2YWx1ZQ==');
@$core.Deprecated('Use parseAddressResultDescriptor instead')
const ParseAddressResult$json = const {
  '1': 'ParseAddressResult',
  '2': const [
    const {'1': 'valid', '3': 1, '4': 1, '5': 8, '10': 'valid'},
    const {'1': 'message', '3': 2, '4': 1, '5': 9, '10': 'message'},
    const {'1': 'formatted_value', '3': 3, '4': 1, '5': 9, '10': 'formattedValue'},
  ],
};

/// Descriptor for `ParseAddressResult`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List parseAddressResultDescriptor = $convert.base64Decode('ChJQYXJzZUFkZHJlc3NSZXN1bHQSFAoFdmFsaWQYASABKAhSBXZhbGlkEhgKB21lc3NhZ2UYAiABKAlSB21lc3NhZ2USJwoPZm9ybWF0dGVkX3ZhbHVlGAMgASgJUg5mb3JtYXR0ZWRWYWx1ZQ==');
@$core.Deprecated('Use addRouteCrossingJunctionSwitchRequestDescriptor instead')
const AddRouteCrossingJunctionSwitchRequest$json = const {
  '1': 'AddRouteCrossingJunctionSwitchRequest',
  '2': const [
    const {'1': 'route_id', '3': 1, '4': 1, '5': 9, '10': 'routeId'},
    const {'1': 'junction_id', '3': 2, '4': 1, '5': 9, '10': 'junctionId'},
    const {'1': 'direction', '3': 3, '4': 1, '5': 14, '6': '.binkyrailways.v1.SwitchDirection', '10': 'direction'},
  ],
};

/// Descriptor for `AddRouteCrossingJunctionSwitchRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List addRouteCrossingJunctionSwitchRequestDescriptor = $convert.base64Decode('CiVBZGRSb3V0ZUNyb3NzaW5nSnVuY3Rpb25Td2l0Y2hSZXF1ZXN0EhkKCHJvdXRlX2lkGAEgASgJUgdyb3V0ZUlkEh8KC2p1bmN0aW9uX2lkGAIgASgJUgpqdW5jdGlvbklkEj8KCWRpcmVjdGlvbhgDIAEoDjIhLmJpbmt5cmFpbHdheXMudjEuU3dpdGNoRGlyZWN0aW9uUglkaXJlY3Rpb24=');
@$core.Deprecated('Use removeRouteCrossingJunctionRequestDescriptor instead')
const RemoveRouteCrossingJunctionRequest$json = const {
  '1': 'RemoveRouteCrossingJunctionRequest',
  '2': const [
    const {'1': 'route_id', '3': 1, '4': 1, '5': 9, '10': 'routeId'},
    const {'1': 'junction_id', '3': 2, '4': 1, '5': 9, '10': 'junctionId'},
  ],
};

/// Descriptor for `RemoveRouteCrossingJunctionRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List removeRouteCrossingJunctionRequestDescriptor = $convert.base64Decode('CiJSZW1vdmVSb3V0ZUNyb3NzaW5nSnVuY3Rpb25SZXF1ZXN0EhkKCHJvdXRlX2lkGAEgASgJUgdyb3V0ZUlkEh8KC2p1bmN0aW9uX2lkGAIgASgJUgpqdW5jdGlvbklk');
