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
@$core.Deprecated('Use binkyNetDeviceTypeDescriptor instead')
const BinkyNetDeviceType$json = const {
  '1': 'BinkyNetDeviceType',
  '2': const [
    const {'1': 'MCP23008', '2': 0},
    const {'1': 'MCP23017', '2': 1},
    const {'1': 'PCA9685', '2': 2},
    const {'1': 'PCF8574', '2': 3},
  ],
};

/// Descriptor for `BinkyNetDeviceType`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List binkyNetDeviceTypeDescriptor = $convert.base64Decode('ChJCaW5reU5ldERldmljZVR5cGUSDAoITUNQMjMwMDgQABIMCghNQ1AyMzAxNxABEgsKB1BDQTk2ODUQAhILCgdQQ0Y4NTc0EAM=');
@$core.Deprecated('Use binkyNetObjectTypeDescriptor instead')
const BinkyNetObjectType$json = const {
  '1': 'BinkyNetObjectType',
  '2': const [
    const {'1': 'BINARYSENSOR', '2': 0},
    const {'1': 'BINARYOUTPUT', '2': 1},
    const {'1': 'SERVOSWITCH', '2': 2},
    const {'1': 'RELAYSWITCH', '2': 3},
    const {'1': 'TRACKINVERTER', '2': 4},
  ],
};

/// Descriptor for `BinkyNetObjectType`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List binkyNetObjectTypeDescriptor = $convert.base64Decode('ChJCaW5reU5ldE9iamVjdFR5cGUSEAoMQklOQVJZU0VOU09SEAASEAoMQklOQVJZT1VUUFVUEAESDwoLU0VSVk9TV0lUQ0gQAhIPCgtSRUxBWVNXSVRDSBADEhEKDVRSQUNLSU5WRVJURVIQBA==');
@$core.Deprecated('Use switchDirectionDescriptor instead')
const SwitchDirection$json = const {
  '1': 'SwitchDirection',
  '2': const [
    const {'1': 'STRAIGHT', '2': 0},
    const {'1': 'OFF', '2': 1},
  ],
};

/// Descriptor for `SwitchDirection`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List switchDirectionDescriptor = $convert.base64Decode('Cg9Td2l0Y2hEaXJlY3Rpb24SDAoIU1RSQUlHSFQQABIHCgNPRkYQAQ==');
@$core.Deprecated('Use binaryOutputTypeDescriptor instead')
const BinaryOutputType$json = const {
  '1': 'BinaryOutputType',
  '2': const [
    const {'1': 'BOT_DEFAULT', '2': 0},
    const {'1': 'BOT_TRACKINVERTER', '2': 1},
  ],
};

/// Descriptor for `BinaryOutputType`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List binaryOutputTypeDescriptor = $convert.base64Decode('ChBCaW5hcnlPdXRwdXRUeXBlEg8KC0JPVF9ERUZBVUxUEAASFQoRQk9UX1RSQUNLSU5WRVJURVIQAQ==');
@$core.Deprecated('Use blockSideDescriptor instead')
const BlockSide$json = const {
  '1': 'BlockSide',
  '2': const [
    const {'1': 'FRONT', '2': 0},
    const {'1': 'BACK', '2': 1},
  ],
};

/// Descriptor for `BlockSide`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List blockSideDescriptor = $convert.base64Decode('CglCbG9ja1NpZGUSCQoFRlJPTlQQABIICgRCQUNLEAE=');
@$core.Deprecated('Use routeStateBehaviorDescriptor instead')
const RouteStateBehavior$json = const {
  '1': 'RouteStateBehavior',
  '2': const [
    const {'1': 'RSB_NOCHANGE', '2': 0},
    const {'1': 'RSB_ENTER', '2': 1},
    const {'1': 'RSB_REACHED', '2': 2},
  ],
};

/// Descriptor for `RouteStateBehavior`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List routeStateBehaviorDescriptor = $convert.base64Decode('ChJSb3V0ZVN0YXRlQmVoYXZpb3ISEAoMUlNCX05PQ0hBTkdFEAASDQoJUlNCX0VOVEVSEAESDwoLUlNCX1JFQUNIRUQQAg==');
@$core.Deprecated('Use locSpeedBehaviorDescriptor instead')
const LocSpeedBehavior$json = const {
  '1': 'LocSpeedBehavior',
  '2': const [
    const {'1': 'LSB_DEFAULT', '2': 0},
    const {'1': 'LSB_NOCHANGE', '2': 1},
    const {'1': 'LSB_MEDIUM', '2': 2},
    const {'1': 'LSB_MINIMUM', '2': 3},
    const {'1': 'LSB_MAXIMUM', '2': 4},
  ],
};

/// Descriptor for `LocSpeedBehavior`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List locSpeedBehaviorDescriptor = $convert.base64Decode('ChBMb2NTcGVlZEJlaGF2aW9yEg8KC0xTQl9ERUZBVUxUEAASEAoMTFNCX05PQ0hBTkdFEAESDgoKTFNCX01FRElVTRACEg8KC0xTQl9NSU5JTVVNEAMSDwoLTFNCX01BWElNVU0QBA==');
@$core.Deprecated('Use shapeDescriptor instead')
const Shape$json = const {
  '1': 'Shape',
  '2': const [
    const {'1': 'CIRCLE', '2': 0},
    const {'1': 'TRIANGLE', '2': 1},
    const {'1': 'SQUARE', '2': 2},
    const {'1': 'DIAMOND', '2': 3},
  ],
};

/// Descriptor for `Shape`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List shapeDescriptor = $convert.base64Decode('CgVTaGFwZRIKCgZDSVJDTEUQABIMCghUUklBTkdMRRABEgoKBlNRVUFSRRACEgsKB0RJQU1PTkQQAw==');
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
    const {'1': 'edges', '3': 102, '4': 3, '5': 11, '6': '.binkyrailways.v1.EdgeRef', '10': 'edges'},
    const {'1': 'junctions', '3': 103, '4': 3, '5': 11, '6': '.binkyrailways.v1.JunctionRef', '10': 'junctions'},
    const {'1': 'outputs', '3': 104, '4': 3, '5': 11, '6': '.binkyrailways.v1.OutputRef', '10': 'outputs'},
    const {'1': 'routes', '3': 105, '4': 3, '5': 11, '6': '.binkyrailways.v1.RouteRef', '10': 'routes'},
    const {'1': 'sensors', '3': 106, '4': 3, '5': 11, '6': '.binkyrailways.v1.SensorRef', '10': 'sensors'},
    const {'1': 'signals', '3': 107, '4': 3, '5': 11, '6': '.binkyrailways.v1.SignalRef', '10': 'signals'},
    const {'1': 'layers', '3': 200, '4': 3, '5': 9, '10': 'layers'},
  ],
};

/// Descriptor for `Module`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List moduleDescriptor = $convert.base64Decode('CgZNb2R1bGUSDgoCaWQYASABKAlSAmlkEiAKC2Rlc2NyaXB0aW9uGAIgASgJUgtkZXNjcmlwdGlvbhIUCgV3aWR0aBgDIAEoBVIFd2lkdGgSFgoGaGVpZ2h0GAQgASgFUgZoZWlnaHQSMAoUaGFzX2JhY2tncm91bmRfaW1hZ2UYBSABKAhSEmhhc0JhY2tncm91bmRJbWFnZRIyCgZibG9ja3MYZCADKAsyGi5iaW5reXJhaWx3YXlzLnYxLkJsb2NrUmVmUgZibG9ja3MSQQoLYmxvY2tHcm91cHMYZSADKAsyHy5iaW5reXJhaWx3YXlzLnYxLkJsb2NrR3JvdXBSZWZSC2Jsb2NrR3JvdXBzEi8KBWVkZ2VzGGYgAygLMhkuYmlua3lyYWlsd2F5cy52MS5FZGdlUmVmUgVlZGdlcxI7CglqdW5jdGlvbnMYZyADKAsyHS5iaW5reXJhaWx3YXlzLnYxLkp1bmN0aW9uUmVmUglqdW5jdGlvbnMSNQoHb3V0cHV0cxhoIAMoCzIbLmJpbmt5cmFpbHdheXMudjEuT3V0cHV0UmVmUgdvdXRwdXRzEjIKBnJvdXRlcxhpIAMoCzIaLmJpbmt5cmFpbHdheXMudjEuUm91dGVSZWZSBnJvdXRlcxI1CgdzZW5zb3JzGGogAygLMhsuYmlua3lyYWlsd2F5cy52MS5TZW5zb3JSZWZSB3NlbnNvcnMSNQoHc2lnbmFscxhrIAMoCzIbLmJpbmt5cmFpbHdheXMudjEuU2lnbmFsUmVmUgdzaWduYWxzEhcKBmxheWVycxjIASADKAlSBmxheWVycw==');
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
    const {'1': 'locs', '3': 3, '4': 3, '5': 11, '6': '.binkyrailways.v1.LocRef', '10': 'locs'},
  ],
};

/// Descriptor for `LocGroup`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List locGroupDescriptor = $convert.base64Decode('CghMb2NHcm91cBIOCgJpZBgBIAEoCVICaWQSIAoLZGVzY3JpcHRpb24YAiABKAlSC2Rlc2NyaXB0aW9uEiwKBGxvY3MYAyADKAsyGC5iaW5reXJhaWx3YXlzLnYxLkxvY1JlZlIEbG9jcw==');
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
    const {'1': 'binkynet_command_station', '3': 10, '4': 1, '5': 11, '6': '.binkyrailways.v1.BinkyNetCommandStation', '10': 'binkynetCommandStation'},
  ],
};

/// Descriptor for `CommandStation`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List commandStationDescriptor = $convert.base64Decode('Cg5Db21tYW5kU3RhdGlvbhIOCgJpZBgBIAEoCVICaWQSIAoLZGVzY3JpcHRpb24YAiABKAlSC2Rlc2NyaXB0aW9uEmIKGGJpbmt5bmV0X2NvbW1hbmRfc3RhdGlvbhgKIAEoCzIoLmJpbmt5cmFpbHdheXMudjEuQmlua3lOZXRDb21tYW5kU3RhdGlvblIWYmlua3luZXRDb21tYW5kU3RhdGlvbg==');
@$core.Deprecated('Use commandStationRefDescriptor instead')
const CommandStationRef$json = const {
  '1': 'CommandStationRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `CommandStationRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List commandStationRefDescriptor = $convert.base64Decode('ChFDb21tYW5kU3RhdGlvblJlZhIOCgJpZBgBIAEoCVICaWQ=');
@$core.Deprecated('Use binkyNetCommandStationDescriptor instead')
const BinkyNetCommandStation$json = const {
  '1': 'BinkyNetCommandStation',
  '2': const [
    const {'1': 'server_host', '3': 1, '4': 1, '5': 9, '10': 'serverHost'},
    const {'1': 'grpc_port', '3': 2, '4': 1, '5': 5, '10': 'grpcPort'},
    const {'1': 'required_worker_version', '3': 3, '4': 1, '5': 9, '10': 'requiredWorkerVersion'},
    const {'1': 'local_workers', '3': 4, '4': 3, '5': 11, '6': '.binkyrailways.v1.BinkyNetLocalWorkerRef', '10': 'localWorkers'},
  ],
};

/// Descriptor for `BinkyNetCommandStation`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List binkyNetCommandStationDescriptor = $convert.base64Decode('ChZCaW5reU5ldENvbW1hbmRTdGF0aW9uEh8KC3NlcnZlcl9ob3N0GAEgASgJUgpzZXJ2ZXJIb3N0EhsKCWdycGNfcG9ydBgCIAEoBVIIZ3JwY1BvcnQSNgoXcmVxdWlyZWRfd29ya2VyX3ZlcnNpb24YAyABKAlSFXJlcXVpcmVkV29ya2VyVmVyc2lvbhJNCg1sb2NhbF93b3JrZXJzGAQgAygLMiguYmlua3lyYWlsd2F5cy52MS5CaW5reU5ldExvY2FsV29ya2VyUmVmUgxsb2NhbFdvcmtlcnM=');
@$core.Deprecated('Use binkyNetLocalWorkerDescriptor instead')
const BinkyNetLocalWorker$json = const {
  '1': 'BinkyNetLocalWorker',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'command_station_id', '3': 3, '4': 1, '5': 9, '10': 'commandStationId'},
    const {'1': 'hardware_id', '3': 4, '4': 1, '5': 9, '10': 'hardwareId'},
    const {'1': 'alias', '3': 5, '4': 1, '5': 9, '10': 'alias'},
    const {'1': 'devices', '3': 10, '4': 3, '5': 11, '6': '.binkyrailways.v1.BinkyNetDevice', '10': 'devices'},
    const {'1': 'objects', '3': 11, '4': 3, '5': 11, '6': '.binkyrailways.v1.BinkyNetObject', '10': 'objects'},
  ],
};

/// Descriptor for `BinkyNetLocalWorker`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List binkyNetLocalWorkerDescriptor = $convert.base64Decode('ChNCaW5reU5ldExvY2FsV29ya2VyEg4KAmlkGAEgASgJUgJpZBIgCgtkZXNjcmlwdGlvbhgCIAEoCVILZGVzY3JpcHRpb24SLAoSY29tbWFuZF9zdGF0aW9uX2lkGAMgASgJUhBjb21tYW5kU3RhdGlvbklkEh8KC2hhcmR3YXJlX2lkGAQgASgJUgpoYXJkd2FyZUlkEhQKBWFsaWFzGAUgASgJUgVhbGlhcxI6CgdkZXZpY2VzGAogAygLMiAuYmlua3lyYWlsd2F5cy52MS5CaW5reU5ldERldmljZVIHZGV2aWNlcxI6CgdvYmplY3RzGAsgAygLMiAuYmlua3lyYWlsd2F5cy52MS5CaW5reU5ldE9iamVjdFIHb2JqZWN0cw==');
@$core.Deprecated('Use binkyNetLocalWorkerRefDescriptor instead')
const BinkyNetLocalWorkerRef$json = const {
  '1': 'BinkyNetLocalWorkerRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `BinkyNetLocalWorkerRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List binkyNetLocalWorkerRefDescriptor = $convert.base64Decode('ChZCaW5reU5ldExvY2FsV29ya2VyUmVmEg4KAmlkGAEgASgJUgJpZA==');
@$core.Deprecated('Use binkyNetDeviceDescriptor instead')
const BinkyNetDevice$json = const {
  '1': 'BinkyNetDevice',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'device_id', '3': 2, '4': 1, '5': 9, '10': 'deviceId'},
    const {'1': 'device_type', '3': 3, '4': 1, '5': 14, '6': '.binkyrailways.v1.BinkyNetDeviceType', '10': 'deviceType'},
    const {'1': 'address', '3': 4, '4': 1, '5': 9, '10': 'address'},
    const {'1': 'can_add_mgv93_group', '3': 10, '4': 1, '5': 8, '10': 'canAddMgv93Group'},
  ],
};

/// Descriptor for `BinkyNetDevice`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List binkyNetDeviceDescriptor = $convert.base64Decode('Cg5CaW5reU5ldERldmljZRIOCgJpZBgBIAEoCVICaWQSGwoJZGV2aWNlX2lkGAIgASgJUghkZXZpY2VJZBJFCgtkZXZpY2VfdHlwZRgDIAEoDjIkLmJpbmt5cmFpbHdheXMudjEuQmlua3lOZXREZXZpY2VUeXBlUgpkZXZpY2VUeXBlEhgKB2FkZHJlc3MYBCABKAlSB2FkZHJlc3MSLQoTY2FuX2FkZF9tZ3Y5M19ncm91cBgKIAEoCFIQY2FuQWRkTWd2OTNHcm91cA==');
@$core.Deprecated('Use binkyNetObjectDescriptor instead')
const BinkyNetObject$json = const {
  '1': 'BinkyNetObject',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'object_id', '3': 2, '4': 1, '5': 9, '10': 'objectId'},
    const {'1': 'object_type', '3': 3, '4': 1, '5': 14, '6': '.binkyrailways.v1.BinkyNetObjectType', '10': 'objectType'},
    const {'1': 'connections', '3': 4, '4': 3, '5': 11, '6': '.binkyrailways.v1.BinkyNetConnection', '10': 'connections'},
  ],
};

/// Descriptor for `BinkyNetObject`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List binkyNetObjectDescriptor = $convert.base64Decode('Cg5CaW5reU5ldE9iamVjdBIOCgJpZBgBIAEoCVICaWQSGwoJb2JqZWN0X2lkGAIgASgJUghvYmplY3RJZBJFCgtvYmplY3RfdHlwZRgDIAEoDjIkLmJpbmt5cmFpbHdheXMudjEuQmlua3lOZXRPYmplY3RUeXBlUgpvYmplY3RUeXBlEkYKC2Nvbm5lY3Rpb25zGAQgAygLMiQuYmlua3lyYWlsd2F5cy52MS5CaW5reU5ldENvbm5lY3Rpb25SC2Nvbm5lY3Rpb25z');
@$core.Deprecated('Use binkyNetConnectionDescriptor instead')
const BinkyNetConnection$json = const {
  '1': 'BinkyNetConnection',
  '2': const [
    const {'1': 'key', '3': 1, '4': 1, '5': 9, '10': 'key'},
    const {'1': 'pins', '3': 2, '4': 3, '5': 11, '6': '.binkyrailways.v1.BinkyNetDevicePin', '10': 'pins'},
    const {'1': 'configuration', '3': 3, '4': 3, '5': 11, '6': '.binkyrailways.v1.BinkyNetConnection.ConfigurationEntry', '10': 'configuration'},
  ],
  '3': const [BinkyNetConnection_ConfigurationEntry$json],
};

@$core.Deprecated('Use binkyNetConnectionDescriptor instead')
const BinkyNetConnection_ConfigurationEntry$json = const {
  '1': 'ConfigurationEntry',
  '2': const [
    const {'1': 'key', '3': 1, '4': 1, '5': 9, '10': 'key'},
    const {'1': 'value', '3': 2, '4': 1, '5': 9, '10': 'value'},
  ],
  '7': const {'7': true},
};

/// Descriptor for `BinkyNetConnection`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List binkyNetConnectionDescriptor = $convert.base64Decode('ChJCaW5reU5ldENvbm5lY3Rpb24SEAoDa2V5GAEgASgJUgNrZXkSNwoEcGlucxgCIAMoCzIjLmJpbmt5cmFpbHdheXMudjEuQmlua3lOZXREZXZpY2VQaW5SBHBpbnMSXQoNY29uZmlndXJhdGlvbhgDIAMoCzI3LmJpbmt5cmFpbHdheXMudjEuQmlua3lOZXRDb25uZWN0aW9uLkNvbmZpZ3VyYXRpb25FbnRyeVINY29uZmlndXJhdGlvbhpAChJDb25maWd1cmF0aW9uRW50cnkSEAoDa2V5GAEgASgJUgNrZXkSFAoFdmFsdWUYAiABKAlSBXZhbHVlOgI4AQ==');
@$core.Deprecated('Use binkyNetDevicePinDescriptor instead')
const BinkyNetDevicePin$json = const {
  '1': 'BinkyNetDevicePin',
  '2': const [
    const {'1': 'device_id', '3': 1, '4': 1, '5': 9, '10': 'deviceId'},
    const {'1': 'index', '3': 2, '4': 1, '5': 13, '10': 'index'},
  ],
};

/// Descriptor for `BinkyNetDevicePin`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List binkyNetDevicePinDescriptor = $convert.base64Decode('ChFCaW5reU5ldERldmljZVBpbhIbCglkZXZpY2VfaWQYASABKAlSCGRldmljZUlkEhQKBWluZGV4GAIgASgNUgVpbmRleA==');
@$core.Deprecated('Use blockDescriptor instead')
const Block$json = const {
  '1': 'Block',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'module_id', '3': 3, '4': 1, '5': 9, '10': 'moduleId'},
    const {'1': 'position', '3': 4, '4': 1, '5': 11, '6': '.binkyrailways.v1.Position', '10': 'position'},
    const {'1': 'wait_probability', '3': 10, '4': 1, '5': 5, '10': 'waitProbability'},
    const {'1': 'minimum_wait_time', '3': 11, '4': 1, '5': 5, '10': 'minimumWaitTime'},
    const {'1': 'maximum_wait_time', '3': 12, '4': 1, '5': 5, '10': 'maximumWaitTime'},
    const {'1': 'wait_permissions', '3': 13, '4': 1, '5': 9, '10': 'waitPermissions'},
    const {'1': 'reverse_sides', '3': 14, '4': 1, '5': 8, '10': 'reverseSides'},
    const {'1': 'change_direction', '3': 15, '4': 1, '5': 14, '6': '.binkyrailways.v1.ChangeDirection', '10': 'changeDirection'},
    const {'1': 'change_direction_reversing_locs', '3': 16, '4': 1, '5': 8, '10': 'changeDirectionReversingLocs'},
    const {'1': 'is_station', '3': 18, '4': 1, '5': 8, '10': 'isStation'},
    const {'1': 'block_group', '3': 19, '4': 1, '5': 11, '6': '.binkyrailways.v1.BlockGroupRef', '10': 'blockGroup'},
  ],
};

/// Descriptor for `Block`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List blockDescriptor = $convert.base64Decode('CgVCbG9jaxIOCgJpZBgBIAEoCVICaWQSIAoLZGVzY3JpcHRpb24YAiABKAlSC2Rlc2NyaXB0aW9uEhsKCW1vZHVsZV9pZBgDIAEoCVIIbW9kdWxlSWQSNgoIcG9zaXRpb24YBCABKAsyGi5iaW5reXJhaWx3YXlzLnYxLlBvc2l0aW9uUghwb3NpdGlvbhIpChB3YWl0X3Byb2JhYmlsaXR5GAogASgFUg93YWl0UHJvYmFiaWxpdHkSKgoRbWluaW11bV93YWl0X3RpbWUYCyABKAVSD21pbmltdW1XYWl0VGltZRIqChFtYXhpbXVtX3dhaXRfdGltZRgMIAEoBVIPbWF4aW11bVdhaXRUaW1lEikKEHdhaXRfcGVybWlzc2lvbnMYDSABKAlSD3dhaXRQZXJtaXNzaW9ucxIjCg1yZXZlcnNlX3NpZGVzGA4gASgIUgxyZXZlcnNlU2lkZXMSTAoQY2hhbmdlX2RpcmVjdGlvbhgPIAEoDjIhLmJpbmt5cmFpbHdheXMudjEuQ2hhbmdlRGlyZWN0aW9uUg9jaGFuZ2VEaXJlY3Rpb24SRQofY2hhbmdlX2RpcmVjdGlvbl9yZXZlcnNpbmdfbG9jcxgQIAEoCFIcY2hhbmdlRGlyZWN0aW9uUmV2ZXJzaW5nTG9jcxIdCgppc19zdGF0aW9uGBIgASgIUglpc1N0YXRpb24SQAoLYmxvY2tfZ3JvdXAYEyABKAsyHy5iaW5reXJhaWx3YXlzLnYxLkJsb2NrR3JvdXBSZWZSCmJsb2NrR3JvdXA=');
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
@$core.Deprecated('Use edgeDescriptor instead')
const Edge$json = const {
  '1': 'Edge',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'module_id', '3': 3, '4': 1, '5': 9, '10': 'moduleId'},
    const {'1': 'position', '3': 4, '4': 1, '5': 11, '6': '.binkyrailways.v1.Position', '10': 'position'},
  ],
};

/// Descriptor for `Edge`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List edgeDescriptor = $convert.base64Decode('CgRFZGdlEg4KAmlkGAEgASgJUgJpZBIgCgtkZXNjcmlwdGlvbhgCIAEoCVILZGVzY3JpcHRpb24SGwoJbW9kdWxlX2lkGAMgASgJUghtb2R1bGVJZBI2Cghwb3NpdGlvbhgEIAEoCzIaLmJpbmt5cmFpbHdheXMudjEuUG9zaXRpb25SCHBvc2l0aW9u');
@$core.Deprecated('Use edgeRefDescriptor instead')
const EdgeRef$json = const {
  '1': 'EdgeRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `EdgeRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List edgeRefDescriptor = $convert.base64Decode('CgdFZGdlUmVmEg4KAmlkGAEgASgJUgJpZA==');
@$core.Deprecated('Use junctionDescriptor instead')
const Junction$json = const {
  '1': 'Junction',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'module_id', '3': 3, '4': 1, '5': 9, '10': 'moduleId'},
    const {'1': 'position', '3': 4, '4': 1, '5': 11, '6': '.binkyrailways.v1.Position', '10': 'position'},
    const {'1': 'block', '3': 5, '4': 1, '5': 11, '6': '.binkyrailways.v1.BlockRef', '10': 'block'},
    const {'1': 'switch', '3': 6, '4': 1, '5': 11, '6': '.binkyrailways.v1.Switch', '10': 'switch'},
  ],
};

/// Descriptor for `Junction`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List junctionDescriptor = $convert.base64Decode('CghKdW5jdGlvbhIOCgJpZBgBIAEoCVICaWQSIAoLZGVzY3JpcHRpb24YAiABKAlSC2Rlc2NyaXB0aW9uEhsKCW1vZHVsZV9pZBgDIAEoCVIIbW9kdWxlSWQSNgoIcG9zaXRpb24YBCABKAsyGi5iaW5reXJhaWx3YXlzLnYxLlBvc2l0aW9uUghwb3NpdGlvbhIwCgVibG9jaxgFIAEoCzIaLmJpbmt5cmFpbHdheXMudjEuQmxvY2tSZWZSBWJsb2NrEjAKBnN3aXRjaBgGIAEoCzIYLmJpbmt5cmFpbHdheXMudjEuU3dpdGNoUgZzd2l0Y2g=');
@$core.Deprecated('Use junctionRefDescriptor instead')
const JunctionRef$json = const {
  '1': 'JunctionRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `JunctionRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List junctionRefDescriptor = $convert.base64Decode('CgtKdW5jdGlvblJlZhIOCgJpZBgBIAEoCVICaWQ=');
@$core.Deprecated('Use switchDescriptor instead')
const Switch$json = const {
  '1': 'Switch',
  '2': const [
    const {'1': 'address', '3': 1, '4': 1, '5': 9, '10': 'address'},
    const {'1': 'has_feedback', '3': 2, '4': 1, '5': 8, '10': 'hasFeedback'},
    const {'1': 'feedback_address', '3': 3, '4': 1, '5': 9, '10': 'feedbackAddress'},
    const {'1': 'switch_duration', '3': 4, '4': 1, '5': 5, '10': 'switchDuration'},
    const {'1': 'invert', '3': 5, '4': 1, '5': 8, '10': 'invert'},
    const {'1': 'invert_feedback', '3': 6, '4': 1, '5': 8, '10': 'invertFeedback'},
    const {'1': 'initial_direction', '3': 7, '4': 1, '5': 14, '6': '.binkyrailways.v1.SwitchDirection', '10': 'initialDirection'},
    const {'1': 'is_left', '3': 8, '4': 1, '5': 8, '10': 'isLeft'},
  ],
};

/// Descriptor for `Switch`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List switchDescriptor = $convert.base64Decode('CgZTd2l0Y2gSGAoHYWRkcmVzcxgBIAEoCVIHYWRkcmVzcxIhCgxoYXNfZmVlZGJhY2sYAiABKAhSC2hhc0ZlZWRiYWNrEikKEGZlZWRiYWNrX2FkZHJlc3MYAyABKAlSD2ZlZWRiYWNrQWRkcmVzcxInCg9zd2l0Y2hfZHVyYXRpb24YBCABKAVSDnN3aXRjaER1cmF0aW9uEhYKBmludmVydBgFIAEoCFIGaW52ZXJ0EicKD2ludmVydF9mZWVkYmFjaxgGIAEoCFIOaW52ZXJ0RmVlZGJhY2sSTgoRaW5pdGlhbF9kaXJlY3Rpb24YByABKA4yIS5iaW5reXJhaWx3YXlzLnYxLlN3aXRjaERpcmVjdGlvblIQaW5pdGlhbERpcmVjdGlvbhIXCgdpc19sZWZ0GAggASgIUgZpc0xlZnQ=');
@$core.Deprecated('Use outputDescriptor instead')
const Output$json = const {
  '1': 'Output',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'module_id', '3': 3, '4': 1, '5': 9, '10': 'moduleId'},
    const {'1': 'position', '3': 4, '4': 1, '5': 11, '6': '.binkyrailways.v1.Position', '10': 'position'},
    const {'1': 'binary_output', '3': 5, '4': 1, '5': 11, '6': '.binkyrailways.v1.BinaryOutput', '10': 'binaryOutput'},
  ],
};

/// Descriptor for `Output`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List outputDescriptor = $convert.base64Decode('CgZPdXRwdXQSDgoCaWQYASABKAlSAmlkEiAKC2Rlc2NyaXB0aW9uGAIgASgJUgtkZXNjcmlwdGlvbhIbCgltb2R1bGVfaWQYAyABKAlSCG1vZHVsZUlkEjYKCHBvc2l0aW9uGAQgASgLMhouYmlua3lyYWlsd2F5cy52MS5Qb3NpdGlvblIIcG9zaXRpb24SQwoNYmluYXJ5X291dHB1dBgFIAEoCzIeLmJpbmt5cmFpbHdheXMudjEuQmluYXJ5T3V0cHV0UgxiaW5hcnlPdXRwdXQ=');
@$core.Deprecated('Use outputRefDescriptor instead')
const OutputRef$json = const {
  '1': 'OutputRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `OutputRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List outputRefDescriptor = $convert.base64Decode('CglPdXRwdXRSZWYSDgoCaWQYASABKAlSAmlk');
@$core.Deprecated('Use binaryOutputDescriptor instead')
const BinaryOutput$json = const {
  '1': 'BinaryOutput',
  '2': const [
    const {'1': 'address', '3': 1, '4': 1, '5': 9, '10': 'address'},
    const {'1': 'output_type', '3': 2, '4': 1, '5': 14, '6': '.binkyrailways.v1.BinaryOutputType', '10': 'outputType'},
    const {'1': 'active_text', '3': 3, '4': 1, '5': 9, '10': 'activeText'},
    const {'1': 'inactive_text', '3': 4, '4': 1, '5': 9, '10': 'inactiveText'},
  ],
};

/// Descriptor for `BinaryOutput`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List binaryOutputDescriptor = $convert.base64Decode('CgxCaW5hcnlPdXRwdXQSGAoHYWRkcmVzcxgBIAEoCVIHYWRkcmVzcxJDCgtvdXRwdXRfdHlwZRgCIAEoDjIiLmJpbmt5cmFpbHdheXMudjEuQmluYXJ5T3V0cHV0VHlwZVIKb3V0cHV0VHlwZRIfCgthY3RpdmVfdGV4dBgDIAEoCVIKYWN0aXZlVGV4dBIjCg1pbmFjdGl2ZV90ZXh0GAQgASgJUgxpbmFjdGl2ZVRleHQ=');
@$core.Deprecated('Use routeDescriptor instead')
const Route$json = const {
  '1': 'Route',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'module_id', '3': 3, '4': 1, '5': 9, '10': 'moduleId'},
    const {'1': 'from', '3': 4, '4': 1, '5': 11, '6': '.binkyrailways.v1.Endpoint', '10': 'from'},
    const {'1': 'to', '3': 5, '4': 1, '5': 11, '6': '.binkyrailways.v1.Endpoint', '10': 'to'},
    const {'1': 'crossing_junctions', '3': 6, '4': 3, '5': 11, '6': '.binkyrailways.v1.JunctionWithState', '10': 'crossingJunctions'},
    const {'1': 'outputs', '3': 7, '4': 3, '5': 11, '6': '.binkyrailways.v1.OutputWithState', '10': 'outputs'},
    const {'1': 'events', '3': 8, '4': 3, '5': 11, '6': '.binkyrailways.v1.RouteEvent', '10': 'events'},
    const {'1': 'speed', '3': 10, '4': 1, '5': 5, '10': 'speed'},
    const {'1': 'choose_probability', '3': 11, '4': 1, '5': 5, '10': 'chooseProbability'},
    const {'1': 'permissions', '3': 12, '4': 1, '5': 9, '10': 'permissions'},
    const {'1': 'closed', '3': 13, '4': 1, '5': 8, '10': 'closed'},
    const {'1': 'max_duration', '3': 14, '4': 1, '5': 5, '10': 'maxDuration'},
  ],
};

/// Descriptor for `Route`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List routeDescriptor = $convert.base64Decode('CgVSb3V0ZRIOCgJpZBgBIAEoCVICaWQSIAoLZGVzY3JpcHRpb24YAiABKAlSC2Rlc2NyaXB0aW9uEhsKCW1vZHVsZV9pZBgDIAEoCVIIbW9kdWxlSWQSLgoEZnJvbRgEIAEoCzIaLmJpbmt5cmFpbHdheXMudjEuRW5kcG9pbnRSBGZyb20SKgoCdG8YBSABKAsyGi5iaW5reXJhaWx3YXlzLnYxLkVuZHBvaW50UgJ0bxJSChJjcm9zc2luZ19qdW5jdGlvbnMYBiADKAsyIy5iaW5reXJhaWx3YXlzLnYxLkp1bmN0aW9uV2l0aFN0YXRlUhFjcm9zc2luZ0p1bmN0aW9ucxI7CgdvdXRwdXRzGAcgAygLMiEuYmlua3lyYWlsd2F5cy52MS5PdXRwdXRXaXRoU3RhdGVSB291dHB1dHMSNAoGZXZlbnRzGAggAygLMhwuYmlua3lyYWlsd2F5cy52MS5Sb3V0ZUV2ZW50UgZldmVudHMSFAoFc3BlZWQYCiABKAVSBXNwZWVkEi0KEmNob29zZV9wcm9iYWJpbGl0eRgLIAEoBVIRY2hvb3NlUHJvYmFiaWxpdHkSIAoLcGVybWlzc2lvbnMYDCABKAlSC3Blcm1pc3Npb25zEhYKBmNsb3NlZBgNIAEoCFIGY2xvc2VkEiEKDG1heF9kdXJhdGlvbhgOIAEoBVILbWF4RHVyYXRpb24=');
@$core.Deprecated('Use routeRefDescriptor instead')
const RouteRef$json = const {
  '1': 'RouteRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `RouteRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List routeRefDescriptor = $convert.base64Decode('CghSb3V0ZVJlZhIOCgJpZBgBIAEoCVICaWQ=');
@$core.Deprecated('Use endpointDescriptor instead')
const Endpoint$json = const {
  '1': 'Endpoint',
  '2': const [
    const {'1': 'block', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.BlockRef', '10': 'block'},
    const {'1': 'edge', '3': 2, '4': 1, '5': 11, '6': '.binkyrailways.v1.EdgeRef', '10': 'edge'},
    const {'1': 'block_side', '3': 3, '4': 1, '5': 14, '6': '.binkyrailways.v1.BlockSide', '10': 'blockSide'},
  ],
};

/// Descriptor for `Endpoint`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List endpointDescriptor = $convert.base64Decode('CghFbmRwb2ludBIwCgVibG9jaxgBIAEoCzIaLmJpbmt5cmFpbHdheXMudjEuQmxvY2tSZWZSBWJsb2NrEi0KBGVkZ2UYAiABKAsyGS5iaW5reXJhaWx3YXlzLnYxLkVkZ2VSZWZSBGVkZ2USOgoKYmxvY2tfc2lkZRgDIAEoDjIbLmJpbmt5cmFpbHdheXMudjEuQmxvY2tTaWRlUglibG9ja1NpZGU=');
@$core.Deprecated('Use junctionWithStateDescriptor instead')
const JunctionWithState$json = const {
  '1': 'JunctionWithState',
  '2': const [
    const {'1': 'junction', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.JunctionRef', '10': 'junction'},
    const {'1': 'switch_state', '3': 10, '4': 1, '5': 11, '6': '.binkyrailways.v1.SwitchWithState', '10': 'switchState'},
  ],
};

/// Descriptor for `JunctionWithState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List junctionWithStateDescriptor = $convert.base64Decode('ChFKdW5jdGlvbldpdGhTdGF0ZRI5CghqdW5jdGlvbhgBIAEoCzIdLmJpbmt5cmFpbHdheXMudjEuSnVuY3Rpb25SZWZSCGp1bmN0aW9uEkQKDHN3aXRjaF9zdGF0ZRgKIAEoCzIhLmJpbmt5cmFpbHdheXMudjEuU3dpdGNoV2l0aFN0YXRlUgtzd2l0Y2hTdGF0ZQ==');
@$core.Deprecated('Use switchWithStateDescriptor instead')
const SwitchWithState$json = const {
  '1': 'SwitchWithState',
  '2': const [
    const {'1': 'direction', '3': 1, '4': 1, '5': 14, '6': '.binkyrailways.v1.SwitchDirection', '10': 'direction'},
  ],
};

/// Descriptor for `SwitchWithState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List switchWithStateDescriptor = $convert.base64Decode('Cg9Td2l0Y2hXaXRoU3RhdGUSPwoJZGlyZWN0aW9uGAEgASgOMiEuYmlua3lyYWlsd2F5cy52MS5Td2l0Y2hEaXJlY3Rpb25SCWRpcmVjdGlvbg==');
@$core.Deprecated('Use outputWithStateDescriptor instead')
const OutputWithState$json = const {
  '1': 'OutputWithState',
  '2': const [
    const {'1': 'output', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.OutputRef', '10': 'output'},
    const {'1': 'binary_output_state', '3': 10, '4': 1, '5': 11, '6': '.binkyrailways.v1.BinaryOutputWithState', '10': 'binaryOutputState'},
  ],
};

/// Descriptor for `OutputWithState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List outputWithStateDescriptor = $convert.base64Decode('Cg9PdXRwdXRXaXRoU3RhdGUSMwoGb3V0cHV0GAEgASgLMhsuYmlua3lyYWlsd2F5cy52MS5PdXRwdXRSZWZSBm91dHB1dBJXChNiaW5hcnlfb3V0cHV0X3N0YXRlGAogASgLMicuYmlua3lyYWlsd2F5cy52MS5CaW5hcnlPdXRwdXRXaXRoU3RhdGVSEWJpbmFyeU91dHB1dFN0YXRl');
@$core.Deprecated('Use binaryOutputWithStateDescriptor instead')
const BinaryOutputWithState$json = const {
  '1': 'BinaryOutputWithState',
  '2': const [
    const {'1': 'active', '3': 1, '4': 1, '5': 8, '10': 'active'},
  ],
};

/// Descriptor for `BinaryOutputWithState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List binaryOutputWithStateDescriptor = $convert.base64Decode('ChVCaW5hcnlPdXRwdXRXaXRoU3RhdGUSFgoGYWN0aXZlGAEgASgIUgZhY3RpdmU=');
@$core.Deprecated('Use routeEventDescriptor instead')
const RouteEvent$json = const {
  '1': 'RouteEvent',
  '2': const [
    const {'1': 'sensor', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.SensorRef', '10': 'sensor'},
    const {'1': 'behaviors', '3': 2, '4': 3, '5': 11, '6': '.binkyrailways.v1.RouteEventBehavior', '10': 'behaviors'},
  ],
};

/// Descriptor for `RouteEvent`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List routeEventDescriptor = $convert.base64Decode('CgpSb3V0ZUV2ZW50EjMKBnNlbnNvchgBIAEoCzIbLmJpbmt5cmFpbHdheXMudjEuU2Vuc29yUmVmUgZzZW5zb3ISQgoJYmVoYXZpb3JzGAIgAygLMiQuYmlua3lyYWlsd2F5cy52MS5Sb3V0ZUV2ZW50QmVoYXZpb3JSCWJlaGF2aW9ycw==');
@$core.Deprecated('Use routeEventBehaviorDescriptor instead')
const RouteEventBehavior$json = const {
  '1': 'RouteEventBehavior',
  '2': const [
    const {'1': 'applies_to', '3': 1, '4': 1, '5': 9, '10': 'appliesTo'},
    const {'1': 'state_behavior', '3': 2, '4': 1, '5': 14, '6': '.binkyrailways.v1.RouteStateBehavior', '10': 'stateBehavior'},
    const {'1': 'speed_behavior', '3': 3, '4': 1, '5': 14, '6': '.binkyrailways.v1.LocSpeedBehavior', '10': 'speedBehavior'},
  ],
};

/// Descriptor for `RouteEventBehavior`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List routeEventBehaviorDescriptor = $convert.base64Decode('ChJSb3V0ZUV2ZW50QmVoYXZpb3ISHQoKYXBwbGllc190bxgBIAEoCVIJYXBwbGllc1RvEksKDnN0YXRlX2JlaGF2aW9yGAIgASgOMiQuYmlua3lyYWlsd2F5cy52MS5Sb3V0ZVN0YXRlQmVoYXZpb3JSDXN0YXRlQmVoYXZpb3ISSQoOc3BlZWRfYmVoYXZpb3IYAyABKA4yIi5iaW5reXJhaWx3YXlzLnYxLkxvY1NwZWVkQmVoYXZpb3JSDXNwZWVkQmVoYXZpb3I=');
@$core.Deprecated('Use sensorDescriptor instead')
const Sensor$json = const {
  '1': 'Sensor',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'module_id', '3': 3, '4': 1, '5': 9, '10': 'moduleId'},
    const {'1': 'position', '3': 4, '4': 1, '5': 11, '6': '.binkyrailways.v1.Position', '10': 'position'},
    const {'1': 'address', '3': 5, '4': 1, '5': 9, '10': 'address'},
    const {'1': 'block', '3': 6, '4': 1, '5': 11, '6': '.binkyrailways.v1.BlockRef', '10': 'block'},
    const {'1': 'shape', '3': 7, '4': 1, '5': 14, '6': '.binkyrailways.v1.Shape', '10': 'shape'},
    const {'1': 'binary_sensor', '3': 8, '4': 1, '5': 11, '6': '.binkyrailways.v1.BinarySensor', '10': 'binarySensor'},
  ],
};

/// Descriptor for `Sensor`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List sensorDescriptor = $convert.base64Decode('CgZTZW5zb3ISDgoCaWQYASABKAlSAmlkEiAKC2Rlc2NyaXB0aW9uGAIgASgJUgtkZXNjcmlwdGlvbhIbCgltb2R1bGVfaWQYAyABKAlSCG1vZHVsZUlkEjYKCHBvc2l0aW9uGAQgASgLMhouYmlua3lyYWlsd2F5cy52MS5Qb3NpdGlvblIIcG9zaXRpb24SGAoHYWRkcmVzcxgFIAEoCVIHYWRkcmVzcxIwCgVibG9jaxgGIAEoCzIaLmJpbmt5cmFpbHdheXMudjEuQmxvY2tSZWZSBWJsb2NrEi0KBXNoYXBlGAcgASgOMhcuYmlua3lyYWlsd2F5cy52MS5TaGFwZVIFc2hhcGUSQwoNYmluYXJ5X3NlbnNvchgIIAEoCzIeLmJpbmt5cmFpbHdheXMudjEuQmluYXJ5U2Vuc29yUgxiaW5hcnlTZW5zb3I=');
@$core.Deprecated('Use sensorRefDescriptor instead')
const SensorRef$json = const {
  '1': 'SensorRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `SensorRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List sensorRefDescriptor = $convert.base64Decode('CglTZW5zb3JSZWYSDgoCaWQYASABKAlSAmlk');
@$core.Deprecated('Use binarySensorDescriptor instead')
const BinarySensor$json = const {
  '1': 'BinarySensor',
};

/// Descriptor for `BinarySensor`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List binarySensorDescriptor = $convert.base64Decode('CgxCaW5hcnlTZW5zb3I=');
@$core.Deprecated('Use signalDescriptor instead')
const Signal$json = const {
  '1': 'Signal',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'description', '3': 2, '4': 1, '5': 9, '10': 'description'},
    const {'1': 'module_id', '3': 3, '4': 1, '5': 9, '10': 'moduleId'},
    const {'1': 'position', '3': 4, '4': 1, '5': 11, '6': '.binkyrailways.v1.Position', '10': 'position'},
  ],
};

/// Descriptor for `Signal`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List signalDescriptor = $convert.base64Decode('CgZTaWduYWwSDgoCaWQYASABKAlSAmlkEiAKC2Rlc2NyaXB0aW9uGAIgASgJUgtkZXNjcmlwdGlvbhIbCgltb2R1bGVfaWQYAyABKAlSCG1vZHVsZUlkEjYKCHBvc2l0aW9uGAQgASgLMhouYmlua3lyYWlsd2F5cy52MS5Qb3NpdGlvblIIcG9zaXRpb24=');
@$core.Deprecated('Use signalRefDescriptor instead')
const SignalRef$json = const {
  '1': 'SignalRef',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
  ],
};

/// Descriptor for `SignalRef`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List signalRefDescriptor = $convert.base64Decode('CglTaWduYWxSZWYSDgoCaWQYASABKAlSAmlk');
