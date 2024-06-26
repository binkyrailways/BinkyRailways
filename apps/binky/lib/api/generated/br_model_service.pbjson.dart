///
//  Generated code. Do not modify.
//  source: br_model_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,deprecated_member_use_from_same_package,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

import 'dart:core' as $core;
import 'dart:convert' as $convert;
import 'dart:typed_data' as $typed_data;
@$core.Deprecated('Use binkyNetObjectsGroupTypeDescriptor instead')
const BinkyNetObjectsGroupType$json = const {
  '1': 'BinkyNetObjectsGroupType',
  '2': const [
    const {'1': 'SENSORS_8', '2': 0},
    const {'1': 'SENSORS_4', '2': 1},
  ],
};

/// Descriptor for `BinkyNetObjectsGroupType`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List binkyNetObjectsGroupTypeDescriptor = $convert.base64Decode('ChhCaW5reU5ldE9iamVjdHNHcm91cFR5cGUSDQoJU0VOU09SU184EAASDQoJU0VOU09SU180EAE=');
@$core.Deprecated('Use serialPortListDescriptor instead')
const SerialPortList$json = const {
  '1': 'SerialPortList',
  '2': const [
    const {'1': 'ports', '3': 1, '4': 3, '5': 9, '10': 'ports'},
  ],
};

/// Descriptor for `SerialPortList`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List serialPortListDescriptor = $convert.base64Decode('Cg5TZXJpYWxQb3J0TGlzdBIUCgVwb3J0cxgBIAMoCVIFcG9ydHM=');
@$core.Deprecated('Use iDRequestDescriptor instead')
const IDRequest$json = const {
  '1': 'IDRequest',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `IDRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List iDRequestDescriptor = $convert.base64Decode('CglJRFJlcXVlc3QSDgoCaWQYASABKAlSAmlk');
@$core.Deprecated('Use subIDRequestDescriptor instead')
const SubIDRequest$json = const {
  '1': 'SubIDRequest',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'sub_id', '3': 2, '4': 1, '5': 9, '10': 'subId'},
  ],
};

/// Descriptor for `SubIDRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List subIDRequestDescriptor = $convert.base64Decode('CgxTdWJJRFJlcXVlc3QSDgoCaWQYASABKAlSAmlkEhUKBnN1Yl9pZBgCIAEoCVIFc3ViSWQ=');
@$core.Deprecated('Use imageIDRequestDescriptor instead')
const ImageIDRequest$json = const {
  '1': 'ImageIDRequest',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'image', '3': 2, '4': 1, '5': 12, '10': 'image'},
  ],
};

/// Descriptor for `ImageIDRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List imageIDRequestDescriptor = $convert.base64Decode('Cg5JbWFnZUlEUmVxdWVzdBIOCgJpZBgBIAEoCVICaWQSFAoFaW1hZ2UYAiABKAxSBWltYWdl');
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
@$core.Deprecated('Use parsePermissionRequestDescriptor instead')
const ParsePermissionRequest$json = const {
  '1': 'ParsePermissionRequest',
  '2': const [
    const {'1': 'value', '3': 1, '4': 1, '5': 9, '10': 'value'},
  ],
};

/// Descriptor for `ParsePermissionRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List parsePermissionRequestDescriptor = $convert.base64Decode('ChZQYXJzZVBlcm1pc3Npb25SZXF1ZXN0EhQKBXZhbHVlGAEgASgJUgV2YWx1ZQ==');
@$core.Deprecated('Use parsePermissionResultDescriptor instead')
const ParsePermissionResult$json = const {
  '1': 'ParsePermissionResult',
  '2': const [
    const {'1': 'valid', '3': 1, '4': 1, '5': 8, '10': 'valid'},
    const {'1': 'message', '3': 2, '4': 1, '5': 9, '10': 'message'},
    const {'1': 'formatted_value', '3': 3, '4': 1, '5': 9, '10': 'formattedValue'},
  ],
};

/// Descriptor for `ParsePermissionResult`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List parsePermissionResultDescriptor = $convert.base64Decode('ChVQYXJzZVBlcm1pc3Npb25SZXN1bHQSFAoFdmFsaWQYASABKAhSBXZhbGlkEhgKB21lc3NhZ2UYAiABKAlSB21lc3NhZ2USJwoPZm9ybWF0dGVkX3ZhbHVlGAMgASgJUg5mb3JtYXR0ZWRWYWx1ZQ==');
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
@$core.Deprecated('Use addRouteBinaryOutputRequestDescriptor instead')
const AddRouteBinaryOutputRequest$json = const {
  '1': 'AddRouteBinaryOutputRequest',
  '2': const [
    const {'1': 'route_id', '3': 1, '4': 1, '5': 9, '10': 'routeId'},
    const {'1': 'output_id', '3': 2, '4': 1, '5': 9, '10': 'outputId'},
    const {'1': 'active', '3': 3, '4': 1, '5': 8, '10': 'active'},
  ],
};

/// Descriptor for `AddRouteBinaryOutputRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List addRouteBinaryOutputRequestDescriptor = $convert.base64Decode('ChtBZGRSb3V0ZUJpbmFyeU91dHB1dFJlcXVlc3QSGQoIcm91dGVfaWQYASABKAlSB3JvdXRlSWQSGwoJb3V0cHV0X2lkGAIgASgJUghvdXRwdXRJZBIWCgZhY3RpdmUYAyABKAhSBmFjdGl2ZQ==');
@$core.Deprecated('Use removeRouteOutputRequestDescriptor instead')
const RemoveRouteOutputRequest$json = const {
  '1': 'RemoveRouteOutputRequest',
  '2': const [
    const {'1': 'route_id', '3': 1, '4': 1, '5': 9, '10': 'routeId'},
    const {'1': 'output_id', '3': 2, '4': 1, '5': 9, '10': 'outputId'},
  ],
};

/// Descriptor for `RemoveRouteOutputRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List removeRouteOutputRequestDescriptor = $convert.base64Decode('ChhSZW1vdmVSb3V0ZU91dHB1dFJlcXVlc3QSGQoIcm91dGVfaWQYASABKAlSB3JvdXRlSWQSGwoJb3V0cHV0X2lkGAIgASgJUghvdXRwdXRJZA==');
@$core.Deprecated('Use addRouteEventRequestDescriptor instead')
const AddRouteEventRequest$json = const {
  '1': 'AddRouteEventRequest',
  '2': const [
    const {'1': 'route_id', '3': 1, '4': 1, '5': 9, '10': 'routeId'},
    const {'1': 'sensor_id', '3': 2, '4': 1, '5': 9, '10': 'sensorId'},
  ],
};

/// Descriptor for `AddRouteEventRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List addRouteEventRequestDescriptor = $convert.base64Decode('ChRBZGRSb3V0ZUV2ZW50UmVxdWVzdBIZCghyb3V0ZV9pZBgBIAEoCVIHcm91dGVJZBIbCglzZW5zb3JfaWQYAiABKAlSCHNlbnNvcklk');
@$core.Deprecated('Use moveRouteEventRequestDescriptor instead')
const MoveRouteEventRequest$json = const {
  '1': 'MoveRouteEventRequest',
  '2': const [
    const {'1': 'route_id', '3': 1, '4': 1, '5': 9, '10': 'routeId'},
    const {'1': 'sensor_id', '3': 2, '4': 1, '5': 9, '10': 'sensorId'},
  ],
};

/// Descriptor for `MoveRouteEventRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List moveRouteEventRequestDescriptor = $convert.base64Decode('ChVNb3ZlUm91dGVFdmVudFJlcXVlc3QSGQoIcm91dGVfaWQYASABKAlSB3JvdXRlSWQSGwoJc2Vuc29yX2lkGAIgASgJUghzZW5zb3JJZA==');
@$core.Deprecated('Use removeRouteEventRequestDescriptor instead')
const RemoveRouteEventRequest$json = const {
  '1': 'RemoveRouteEventRequest',
  '2': const [
    const {'1': 'route_id', '3': 1, '4': 1, '5': 9, '10': 'routeId'},
    const {'1': 'sensor_id', '3': 2, '4': 1, '5': 9, '10': 'sensorId'},
  ],
};

/// Descriptor for `RemoveRouteEventRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List removeRouteEventRequestDescriptor = $convert.base64Decode('ChdSZW1vdmVSb3V0ZUV2ZW50UmVxdWVzdBIZCghyb3V0ZV9pZBgBIAEoCVIHcm91dGVJZBIbCglzZW5zb3JfaWQYAiABKAlSCHNlbnNvcklk');
@$core.Deprecated('Use addRouteEventBehaviorRequestDescriptor instead')
const AddRouteEventBehaviorRequest$json = const {
  '1': 'AddRouteEventBehaviorRequest',
  '2': const [
    const {'1': 'route_id', '3': 1, '4': 1, '5': 9, '10': 'routeId'},
    const {'1': 'sensor_id', '3': 2, '4': 1, '5': 9, '10': 'sensorId'},
  ],
};

/// Descriptor for `AddRouteEventBehaviorRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List addRouteEventBehaviorRequestDescriptor = $convert.base64Decode('ChxBZGRSb3V0ZUV2ZW50QmVoYXZpb3JSZXF1ZXN0EhkKCHJvdXRlX2lkGAEgASgJUgdyb3V0ZUlkEhsKCXNlbnNvcl9pZBgCIAEoCVIIc2Vuc29ySWQ=');
@$core.Deprecated('Use removeRouteEventBehaviorRequestDescriptor instead')
const RemoveRouteEventBehaviorRequest$json = const {
  '1': 'RemoveRouteEventBehaviorRequest',
  '2': const [
    const {'1': 'route_id', '3': 1, '4': 1, '5': 9, '10': 'routeId'},
    const {'1': 'sensor_id', '3': 2, '4': 1, '5': 9, '10': 'sensorId'},
    const {'1': 'index', '3': 3, '4': 1, '5': 5, '10': 'index'},
  ],
};

/// Descriptor for `RemoveRouteEventBehaviorRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List removeRouteEventBehaviorRequestDescriptor = $convert.base64Decode('Ch9SZW1vdmVSb3V0ZUV2ZW50QmVoYXZpb3JSZXF1ZXN0EhkKCHJvdXRlX2lkGAEgASgJUgdyb3V0ZUlkEhsKCXNlbnNvcl9pZBgCIAEoCVIIc2Vuc29ySWQSFAoFaW5kZXgYAyABKAVSBWluZGV4');
@$core.Deprecated('Use addBinkyNetObjectsGroupRequestDescriptor instead')
const AddBinkyNetObjectsGroupRequest$json = const {
  '1': 'AddBinkyNetObjectsGroupRequest',
  '2': const [
    const {'1': 'local_worker_id', '3': 1, '4': 1, '5': 9, '10': 'localWorkerId'},
    const {'1': 'device_id', '3': 2, '4': 1, '5': 9, '10': 'deviceId'},
    const {'1': 'type', '3': 3, '4': 1, '5': 14, '6': '.binkyrailways.v1.BinkyNetObjectsGroupType', '10': 'type'},
  ],
};

/// Descriptor for `AddBinkyNetObjectsGroupRequest`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List addBinkyNetObjectsGroupRequestDescriptor = $convert.base64Decode('Ch5BZGRCaW5reU5ldE9iamVjdHNHcm91cFJlcXVlc3QSJgoPbG9jYWxfd29ya2VyX2lkGAEgASgJUg1sb2NhbFdvcmtlcklkEhsKCWRldmljZV9pZBgCIAEoCVIIZGV2aWNlSWQSPgoEdHlwZRgDIAEoDjIqLmJpbmt5cmFpbHdheXMudjEuQmlua3lOZXRPYmplY3RzR3JvdXBUeXBlUgR0eXBl');
