///
//  Generated code. Do not modify.
//  source: br_model_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields,deprecated_member_use_from_same_package

import 'dart:core' as $core;
import 'dart:convert' as $convert;
import 'dart:typed_data' as $typed_data;
@$core.Deprecated('Use addressTypeDescriptor instead')
const AddressType$json = const {
  '1': 'AddressType',
  '2': const [
    const {'1': 'BINKYNET', '2': 0},
    const {'1': 'DCC', '2': 1},
    const {'1': 'LOCONET', '2': 2},
    const {'1': 'MOTOROLA', '2': 3},
    const {'1': 'MFX', '2': 4},
    const {'1': 'MQTT', '2': 5},
  ],
};

/// Descriptor for `AddressType`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List addressTypeDescriptor = $convert.base64Decode('CgtBZGRyZXNzVHlwZRIMCghCSU5LWU5FVBAAEgcKA0RDQxABEgsKB0xPQ09ORVQQAhIMCghNT1RPUk9MQRADEgcKA01GWBAEEggKBE1RVFQQBQ==');
@$core.Deprecated('Use changeDirectionDescriptor instead')
const ChangeDirection$json = const {
  '1': 'ChangeDirection',
  '2': const [
    const {'1': 'ALLOW', '2': 0},
    const {'1': 'AVOID', '2': 1},
  ],
};

/// Descriptor for `ChangeDirection`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List changeDirectionDescriptor = $convert.base64Decode('Cg9DaGFuZ2VEaXJlY3Rpb24SCQoFQUxMT1cQABIJCgVBVk9JRBAB');
@$core.Deprecated('Use emptyDescriptor instead')
const Empty$json = const {
  '1': 'Empty',
};

/// Descriptor for `Empty`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List emptyDescriptor = $convert.base64Decode('CgVFbXB0eQ==');
@$core.Deprecated('Use railwayDescriptor instead')
const Railway$json = const {
  '1': 'Railway',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'dirty', '3': 3, '4': 1, '5': 8, '10': 'dirty'},
    const {'1': 'modules', '3': 100, '4': 3, '5': 11, '6': '.binkyrailways.v1.ModuleRef', '10': 'modules'},
    const {'1': 'locs', '3': 101, '4': 3, '5': 11, '6': '.binkyrailways.v1.LocRef', '10': 'locs'},
  ],
};

/// Descriptor for `Railway`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List railwayDescriptor = $convert.base64Decode('CgdSYWlsd2F5Eg4KAmlkGAEgASgJUgJpZBIgCgtkZXNjcmlwdGlvbhgCIAEoCVILZGVzY3JpcHRpb24SFAoFZGlydHkYAyABKAhSBWRpcnR5EjUKB21vZHVsZXMYZCADKAsyGy5iaW5reXJhaWx3YXlzLnYxLk1vZHVsZVJlZlIHbW9kdWxlcxIsCgRsb2NzGGUgAygLMhguYmlua3lyYWlsd2F5cy52MS5Mb2NSZWZSBGxvY3M=');
@$core.Deprecated('Use moduleDescriptor instead')
const Module$json = const {
  '1': 'Module',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
  ],
};

/// Descriptor for `Module`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List moduleDescriptor = $convert.base64Decode('CgZNb2R1bGUSDgoCaWQYASABKAlSAmlkEiAKC2Rlc2NyaXB0aW9uGAIgASgJUgtkZXNjcmlwdGlvbg==');
@$core.Deprecated('Use moduleRefDescriptor instead')
const ModuleRef$json = const {
  '1': 'ModuleRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'position', '3': 2, '4': 1, '5': 11, '6': '.binkyrailways.v1.Position', '10': 'position'},
    const {'1': 'zoom_factor', '3': 3, '4': 1, '5': 5, '10': 'zoomFactor'},
    const {'1': 'locked', '3': 4, '4': 1, '5': 8, '10': 'locked'},
  ],
};

/// Descriptor for `ModuleRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List moduleRefDescriptor = $convert.base64Decode('CglNb2R1bGVSZWYSDgoCaWQYASABKAlSAmlkEjYKCHBvc2l0aW9uGAIgASgLMhouYmlua3lyYWlsd2F5cy52MS5Qb3NpdGlvblIIcG9zaXRpb24SHwoLem9vbV9mYWN0b3IYAyABKAVSCnpvb21GYWN0b3ISFgoGbG9ja2VkGAQgASgIUgZsb2NrZWQ=');
@$core.Deprecated('Use positionDescriptor instead')
const Position$json = const {
  '1': 'Position',
  '2': const [
    const {'1': 'x', '3': 1, '4': 1, '5': 5, '10': 'x'},
    const {'1': 'y', '3': 2, '4': 1, '5': 5, '10': 'y'},
    const {'1': 'width', '3': 3, '4': 1, '5': 5, '10': 'width'},
    const {'1': 'height', '3': 4, '4': 1, '5': 5, '10': 'height'},
    const {'1': 'rotation', '3': 5, '4': 1, '5': 5, '10': 'rotation'},
    const {'1': 'layer', '3': 6, '4': 1, '5': 9, '10': 'layer'},
  ],
};

/// Descriptor for `Position`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List positionDescriptor = $convert.base64Decode('CghQb3NpdGlvbhIMCgF4GAEgASgFUgF4EgwKAXkYAiABKAVSAXkSFAoFd2lkdGgYAyABKAVSBXdpZHRoEhYKBmhlaWdodBgEIAEoBVIGaGVpZ2h0EhoKCHJvdGF0aW9uGAUgASgFUghyb3RhdGlvbhIUCgVsYXllchgGIAEoCVIFbGF5ZXI=');
@$core.Deprecated('Use addressDescriptor instead')
const Address$json = const {
  '1': 'Address',
  '2': const [
    const {'1': 'type', '3': 1, '4': 1, '5': 14, '6': '.binkyrailways.v1.AddressType', '10': 'type'},
    const {'1': 'space', '3': 2, '4': 1, '5': 9, '10': 'space'},
    const {'1': 'value', '3': 3, '4': 1, '5': 9, '10': 'value'},
  ],
};

/// Descriptor for `Address`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List addressDescriptor = $convert.base64Decode('CgdBZGRyZXNzEjEKBHR5cGUYASABKA4yHS5iaW5reXJhaWx3YXlzLnYxLkFkZHJlc3NUeXBlUgR0eXBlEhQKBXNwYWNlGAIgASgJUgVzcGFjZRIUCgV2YWx1ZRgDIAEoCVIFdmFsdWU=');
@$core.Deprecated('Use locDescriptor instead')
const Loc$json = const {
  '1': 'Loc',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'owner', '3': 3, '4': 1, '5': 9, '10': 'owner'},
    const {'1': 'remarks', '3': 4, '4': 1, '5': 9, '10': 'remarks'},
    const {'1': 'address', '3': 5, '4': 1, '5': 11, '6': '.binkyrailways.v1.Address', '10': 'address'},
    const {'1': 'slow_speed', '3': 100, '4': 1, '5': 5, '10': 'slowSpeed'},
    const {'1': 'medium_speed', '3': 101, '4': 1, '5': 5, '10': 'mediumSpeed'},
    const {'1': 'maximum_speed', '3': 102, '4': 1, '5': 5, '10': 'maximumSpeed'},
    const {'1': 'speed_steps', '3': 110, '4': 1, '5': 5, '10': 'speedSteps'},
    const {'1': 'change_direction', '3': 120, '4': 1, '5': 14, '6': '.binkyrailways.v1.ChangeDirection', '10': 'changeDirection'},
  ],
};

/// Descriptor for `Loc`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List locDescriptor = $convert.base64Decode('CgNMb2MSDgoCaWQYASABKAlSAmlkEiAKC2Rlc2NyaXB0aW9uGAIgASgJUgtkZXNjcmlwdGlvbhIUCgVvd25lchgDIAEoCVIFb3duZXISGAoHcmVtYXJrcxgEIAEoCVIHcmVtYXJrcxIzCgdhZGRyZXNzGAUgASgLMhkuYmlua3lyYWlsd2F5cy52MS5BZGRyZXNzUgdhZGRyZXNzEh0KCnNsb3dfc3BlZWQYZCABKAVSCXNsb3dTcGVlZBIhCgxtZWRpdW1fc3BlZWQYZSABKAVSC21lZGl1bVNwZWVkEiMKDW1heGltdW1fc3BlZWQYZiABKAVSDG1heGltdW1TcGVlZBIfCgtzcGVlZF9zdGVwcxhuIAEoBVIKc3BlZWRTdGVwcxJMChBjaGFuZ2VfZGlyZWN0aW9uGHggASgOMiEuYmlua3lyYWlsd2F5cy52MS5DaGFuZ2VEaXJlY3Rpb25SD2NoYW5nZURpcmVjdGlvbg==');
@$core.Deprecated('Use locRefDescriptor instead')
const LocRef$json = const {
  '1': 'LocRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `LocRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List locRefDescriptor = $convert.base64Decode('CgZMb2NSZWYSDgoCaWQYASABKAlSAmlk');