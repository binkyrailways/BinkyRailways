///
//  Generated code. Do not modify.
//  source: br_model_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields,deprecated_member_use_from_same_package

import 'dart:core' as $core;
import 'dart:convert' as $convert;
import 'dart:typed_data' as $typed_data;
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
@$core.Deprecated('Use imageDescriptor instead')
const Image$json = const {
  '1': 'Image',
  '2': const [
    const {'1': 'content_base64', '3': 1, '4': 1, '5': 9, '10': 'contentBase64'},
  ],
};

/// Descriptor for `Image`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List imageDescriptor = $convert.base64Decode('CgVJbWFnZRIlCg5jb250ZW50X2Jhc2U2NBgBIAEoCVINY29udGVudEJhc2U2NA==');
@$core.Deprecated('Use railwayDescriptor instead')
const Railway$json = const {
  '1': 'Railway',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'dirty', '3': 3, '4': 1, '5': 8, '10': 'dirty'},
    const {'1': 'modules', '3': 100, '4': 3, '5': 11, '6': '.binkyrailways.v1.ModuleRef', '10': 'modules'},
    const {'1': 'locs', '3': 101, '4': 3, '5': 11, '6': '.binkyrailways.v1.LocRef', '10': 'locs'},
    const {'1': 'locGroups', '3': 102, '4': 3, '5': 11, '6': '.binkyrailways.v1.LocGroupRef', '10': 'locGroups'},
    const {'1': 'commandStations', '3': 103, '4': 3, '5': 11, '6': '.binkyrailways.v1.CommandStationRef', '10': 'commandStations'},
  ],
};

/// Descriptor for `Railway`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List railwayDescriptor = $convert.base64Decode('CgdSYWlsd2F5Eg4KAmlkGAEgASgJUgJpZBIgCgtkZXNjcmlwdGlvbhgCIAEoCVILZGVzY3JpcHRpb24SFAoFZGlydHkYAyABKAhSBWRpcnR5EjUKB21vZHVsZXMYZCADKAsyGy5iaW5reXJhaWx3YXlzLnYxLk1vZHVsZVJlZlIHbW9kdWxlcxIsCgRsb2NzGGUgAygLMhguYmlua3lyYWlsd2F5cy52MS5Mb2NSZWZSBGxvY3MSOwoJbG9jR3JvdXBzGGYgAygLMh0uYmlua3lyYWlsd2F5cy52MS5Mb2NHcm91cFJlZlIJbG9jR3JvdXBzEk0KD2NvbW1hbmRTdGF0aW9ucxhnIAMoCzIjLmJpbmt5cmFpbHdheXMudjEuQ29tbWFuZFN0YXRpb25SZWZSD2NvbW1hbmRTdGF0aW9ucw==');
@$core.Deprecated('Use moduleDescriptor instead')
const Module$json = const {
  '1': 'Module',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'width', '3': 3, '4': 1, '5': 5, '10': 'width'},
    const {'1': 'height', '3': 4, '4': 1, '5': 5, '10': 'height'},
    const {'1': 'has_background_image', '3': 5, '4': 1, '5': 8, '10': 'hasBackgroundImage'},
    const {'1': 'blocks', '3': 100, '4': 3, '5': 11, '6': '.binkyrailways.v1.BlockRef', '10': 'blocks'},
    const {'1': 'blockGroups', '3': 101, '4': 3, '5': 11, '6': '.binkyrailways.v1.BlockGroupRef', '10': 'blockGroups'},
    const {'1': 'junctions', '3': 102, '4': 3, '5': 11, '6': '.binkyrailways.v1.JunctionRef', '10': 'junctions'},
    const {'1': 'outputs', '3': 103, '4': 3, '5': 11, '6': '.binkyrailways.v1.OutputRef', '10': 'outputs'},
  ],
};

/// Descriptor for `Module`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List moduleDescriptor = $convert.base64Decode('CgZNb2R1bGUSDgoCaWQYASABKAlSAmlkEiAKC2Rlc2NyaXB0aW9uGAIgASgJUgtkZXNjcmlwdGlvbhIUCgV3aWR0aBgDIAEoBVIFd2lkdGgSFgoGaGVpZ2h0GAQgASgFUgZoZWlnaHQSMAoUaGFzX2JhY2tncm91bmRfaW1hZ2UYBSABKAhSEmhhc0JhY2tncm91bmRJbWFnZRIyCgZibG9ja3MYZCADKAsyGi5iaW5reXJhaWx3YXlzLnYxLkJsb2NrUmVmUgZibG9ja3MSQQoLYmxvY2tHcm91cHMYZSADKAsyHy5iaW5reXJhaWx3YXlzLnYxLkJsb2NrR3JvdXBSZWZSC2Jsb2NrR3JvdXBzEjsKCWp1bmN0aW9ucxhmIAMoCzIdLmJpbmt5cmFpbHdheXMudjEuSnVuY3Rpb25SZWZSCWp1bmN0aW9ucxI1CgdvdXRwdXRzGGcgAygLMhsuYmlua3lyYWlsd2F5cy52MS5PdXRwdXRSZWZSB291dHB1dHM=');
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
@$core.Deprecated('Use locDescriptor instead')
const Loc$json = const {
  '1': 'Loc',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'owner', '3': 3, '4': 1, '5': 9, '10': 'owner'},
    const {'1': 'remarks', '3': 4, '4': 1, '5': 9, '10': 'remarks'},
    const {'1': 'address', '3': 5, '4': 1, '5': 9, '10': 'address'},
    const {'1': 'slow_speed', '3': 100, '4': 1, '5': 5, '10': 'slowSpeed'},
    const {'1': 'medium_speed', '3': 101, '4': 1, '5': 5, '10': 'mediumSpeed'},
    const {'1': 'maximum_speed', '3': 102, '4': 1, '5': 5, '10': 'maximumSpeed'},
    const {'1': 'speed_steps', '3': 110, '4': 1, '5': 5, '10': 'speedSteps'},
    const {'1': 'change_direction', '3': 120, '4': 1, '5': 14, '6': '.binkyrailways.v1.ChangeDirection', '10': 'changeDirection'},
  ],
};

/// Descriptor for `Loc`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List locDescriptor = $convert.base64Decode('CgNMb2MSDgoCaWQYASABKAlSAmlkEiAKC2Rlc2NyaXB0aW9uGAIgASgJUgtkZXNjcmlwdGlvbhIUCgVvd25lchgDIAEoCVIFb3duZXISGAoHcmVtYXJrcxgEIAEoCVIHcmVtYXJrcxIYCgdhZGRyZXNzGAUgASgJUgdhZGRyZXNzEh0KCnNsb3dfc3BlZWQYZCABKAVSCXNsb3dTcGVlZBIhCgxtZWRpdW1fc3BlZWQYZSABKAVSC21lZGl1bVNwZWVkEiMKDW1heGltdW1fc3BlZWQYZiABKAVSDG1heGltdW1TcGVlZBIfCgtzcGVlZF9zdGVwcxhuIAEoBVIKc3BlZWRTdGVwcxJMChBjaGFuZ2VfZGlyZWN0aW9uGHggASgOMiEuYmlua3lyYWlsd2F5cy52MS5DaGFuZ2VEaXJlY3Rpb25SD2NoYW5nZURpcmVjdGlvbg==');
@$core.Deprecated('Use locRefDescriptor instead')
const LocRef$json = const {
  '1': 'LocRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `LocRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List locRefDescriptor = $convert.base64Decode('CgZMb2NSZWYSDgoCaWQYASABKAlSAmlk');
@$core.Deprecated('Use locGroupDescriptor instead')
const LocGroup$json = const {
  '1': 'LocGroup',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
  ],
};

/// Descriptor for `LocGroup`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List locGroupDescriptor = $convert.base64Decode('CghMb2NHcm91cBIOCgJpZBgBIAEoCVICaWQSIAoLZGVzY3JpcHRpb24YAiABKAlSC2Rlc2NyaXB0aW9u');
@$core.Deprecated('Use locGroupRefDescriptor instead')
const LocGroupRef$json = const {
  '1': 'LocGroupRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `LocGroupRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List locGroupRefDescriptor = $convert.base64Decode('CgtMb2NHcm91cFJlZhIOCgJpZBgBIAEoCVICaWQ=');
@$core.Deprecated('Use commandStationDescriptor instead')
const CommandStation$json = const {
  '1': 'CommandStation',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
  ],
};

/// Descriptor for `CommandStation`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List commandStationDescriptor = $convert.base64Decode('Cg5Db21tYW5kU3RhdGlvbhIOCgJpZBgBIAEoCVICaWQSIAoLZGVzY3JpcHRpb24YAiABKAlSC2Rlc2NyaXB0aW9u');
@$core.Deprecated('Use commandStationRefDescriptor instead')
const CommandStationRef$json = const {
  '1': 'CommandStationRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `CommandStationRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List commandStationRefDescriptor = $convert.base64Decode('ChFDb21tYW5kU3RhdGlvblJlZhIOCgJpZBgBIAEoCVICaWQ=');
@$core.Deprecated('Use blockDescriptor instead')
const Block$json = const {
  '1': 'Block',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'module_id', '3': 3, '4': 1, '5': 9, '10': 'moduleId'},
    const {'1': 'position', '3': 4, '4': 1, '5': 11, '6': '.binkyrailways.v1.Position', '10': 'position'},
  ],
};

/// Descriptor for `Block`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List blockDescriptor = $convert.base64Decode('CgVCbG9jaxIOCgJpZBgBIAEoCVICaWQSIAoLZGVzY3JpcHRpb24YAiABKAlSC2Rlc2NyaXB0aW9uEhsKCW1vZHVsZV9pZBgDIAEoCVIIbW9kdWxlSWQSNgoIcG9zaXRpb24YBCABKAsyGi5iaW5reXJhaWx3YXlzLnYxLlBvc2l0aW9uUghwb3NpdGlvbg==');
@$core.Deprecated('Use blockRefDescriptor instead')
const BlockRef$json = const {
  '1': 'BlockRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `BlockRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List blockRefDescriptor = $convert.base64Decode('CghCbG9ja1JlZhIOCgJpZBgBIAEoCVICaWQ=');
@$core.Deprecated('Use blockGroupDescriptor instead')
const BlockGroup$json = const {
  '1': 'BlockGroup',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'module_id', '3': 3, '4': 1, '5': 9, '10': 'moduleId'},
  ],
};

/// Descriptor for `BlockGroup`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List blockGroupDescriptor = $convert.base64Decode('CgpCbG9ja0dyb3VwEg4KAmlkGAEgASgJUgJpZBIgCgtkZXNjcmlwdGlvbhgCIAEoCVILZGVzY3JpcHRpb24SGwoJbW9kdWxlX2lkGAMgASgJUghtb2R1bGVJZA==');
@$core.Deprecated('Use blockGroupRefDescriptor instead')
const BlockGroupRef$json = const {
  '1': 'BlockGroupRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `BlockGroupRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List blockGroupRefDescriptor = $convert.base64Decode('Cg1CbG9ja0dyb3VwUmVmEg4KAmlkGAEgASgJUgJpZA==');
@$core.Deprecated('Use junctionDescriptor instead')
const Junction$json = const {
  '1': 'Junction',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'module_id', '3': 3, '4': 1, '5': 9, '10': 'moduleId'},
    const {'1': 'position', '3': 4, '4': 1, '5': 11, '6': '.binkyrailways.v1.Position', '10': 'position'},
  ],
};

/// Descriptor for `Junction`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List junctionDescriptor = $convert.base64Decode('CghKdW5jdGlvbhIOCgJpZBgBIAEoCVICaWQSIAoLZGVzY3JpcHRpb24YAiABKAlSC2Rlc2NyaXB0aW9uEhsKCW1vZHVsZV9pZBgDIAEoCVIIbW9kdWxlSWQSNgoIcG9zaXRpb24YBCABKAsyGi5iaW5reXJhaWx3YXlzLnYxLlBvc2l0aW9uUghwb3NpdGlvbg==');
@$core.Deprecated('Use junctionRefDescriptor instead')
const JunctionRef$json = const {
  '1': 'JunctionRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `JunctionRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List junctionRefDescriptor = $convert.base64Decode('CgtKdW5jdGlvblJlZhIOCgJpZBgBIAEoCVICaWQ=');
@$core.Deprecated('Use outputDescriptor instead')
const Output$json = const {
  '1': 'Output',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'module_id', '3': 3, '4': 1, '5': 9, '10': 'moduleId'},
    const {'1': 'position', '3': 4, '4': 1, '5': 11, '6': '.binkyrailways.v1.Position', '10': 'position'},
  ],
};

/// Descriptor for `Output`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List outputDescriptor = $convert.base64Decode('CgZPdXRwdXQSDgoCaWQYASABKAlSAmlkEiAKC2Rlc2NyaXB0aW9uGAIgASgJUgtkZXNjcmlwdGlvbhIbCgltb2R1bGVfaWQYAyABKAlSCG1vZHVsZUlkEjYKCHBvc2l0aW9uGAQgASgLMhouYmlua3lyYWlsd2F5cy52MS5Qb3NpdGlvblIIcG9zaXRpb24=');
@$core.Deprecated('Use outputRefDescriptor instead')
const OutputRef$json = const {
  '1': 'OutputRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `OutputRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List outputRefDescriptor = $convert.base64Decode('CglPdXRwdXRSZWYSDgoCaWQYASABKAlSAmlk');
