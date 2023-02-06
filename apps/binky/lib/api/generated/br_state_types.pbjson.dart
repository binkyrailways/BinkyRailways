///
//  Generated code. Do not modify.
//  source: br_state_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,deprecated_member_use_from_same_package,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

import 'dart:core' as $core;
import 'dart:convert' as $convert;
import 'dart:typed_data' as $typed_data;
@$core.Deprecated('Use locDirectionDescriptor instead')
const LocDirection$json = const {
  '1': 'LocDirection',
  '2': const [
    const {'1': 'FORWARD', '2': 0},
    const {'1': 'REVERSE', '2': 1},
  ],
};

/// Descriptor for `LocDirection`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List locDirectionDescriptor = $convert.base64Decode('CgxMb2NEaXJlY3Rpb24SCwoHRk9SV0FSRBAAEgsKB1JFVkVSU0UQAQ==');
@$core.Deprecated('Use autoLocStateDescriptor instead')
const AutoLocState$json = const {
  '1': 'AutoLocState',
  '2': const [
    const {'1': 'ASSIGNROUTE', '2': 0},
    const {'1': 'REVERSINGWAITINGFORDIRECTIONCHANGE', '2': 1},
    const {'1': 'WAITINGFORASSIGNEDROUTEREADY', '2': 2},
    const {'1': 'RUNNING', '2': 3},
    const {'1': 'ENTERSENSORACTIVATED', '2': 4},
    const {'1': 'ENTERINGDESTINATION', '2': 5},
    const {'1': 'REACHEDSENSORACTIVATED', '2': 6},
    const {'1': 'REACHEDDESTINATION', '2': 7},
    const {'1': 'WAITINGFORDESTINATIONTIMEOUT', '2': 8},
    const {'1': 'WAITINGFORDESTINATIONGROUPMINIMUM', '2': 9},
  ],
};

/// Descriptor for `AutoLocState`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List autoLocStateDescriptor = $convert.base64Decode('CgxBdXRvTG9jU3RhdGUSDwoLQVNTSUdOUk9VVEUQABImCiJSRVZFUlNJTkdXQUlUSU5HRk9SRElSRUNUSU9OQ0hBTkdFEAESIAocV0FJVElOR0ZPUkFTU0lHTkVEUk9VVEVSRUFEWRACEgsKB1JVTk5JTkcQAxIYChRFTlRFUlNFTlNPUkFDVElWQVRFRBAEEhcKE0VOVEVSSU5HREVTVElOQVRJT04QBRIaChZSRUFDSEVEU0VOU09SQUNUSVZBVEVEEAYSFgoSUkVBQ0hFRERFU1RJTkFUSU9OEAcSIAocV0FJVElOR0ZPUkRFU1RJTkFUSU9OVElNRU9VVBAIEiUKIVdBSVRJTkdGT1JERVNUSU5BVElPTkdST1VQTUlOSU1VTRAJ');
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
    const {'1': 'automatic_control_actual', '3': 12, '4': 1, '5': 8, '10': 'automaticControlActual'},
    const {'1': 'automatic_control_requested', '3': 13, '4': 1, '5': 8, '10': 'automaticControlRequested'},
    const {'1': 'blocks', '3': 50, '4': 3, '5': 11, '6': '.binkyrailways.v1.BlockRef', '10': 'blocks'},
    const {'1': 'block_groups', '3': 51, '4': 3, '5': 11, '6': '.binkyrailways.v1.BlockGroupRef', '10': 'blockGroups'},
    const {'1': 'command_stations', '3': 52, '4': 3, '5': 11, '6': '.binkyrailways.v1.CommandStationRef', '10': 'commandStations'},
    const {'1': 'junctions', '3': 53, '4': 3, '5': 11, '6': '.binkyrailways.v1.JunctionRef', '10': 'junctions'},
    const {'1': 'locs', '3': 54, '4': 3, '5': 11, '6': '.binkyrailways.v1.LocRef', '10': 'locs'},
    const {'1': 'outputs', '3': 55, '4': 3, '5': 11, '6': '.binkyrailways.v1.OutputRef', '10': 'outputs'},
    const {'1': 'routes', '3': 56, '4': 3, '5': 11, '6': '.binkyrailways.v1.RouteRef', '10': 'routes'},
    const {'1': 'sensors', '3': 57, '4': 3, '5': 11, '6': '.binkyrailways.v1.SensorRef', '10': 'sensors'},
    const {'1': 'signals', '3': 58, '4': 3, '5': 11, '6': '.binkyrailways.v1.SignalRef', '10': 'signals'},
  ],
};

/// Descriptor for `RailwayState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List railwayStateDescriptor = $convert.base64Decode('CgxSYWlsd2F5U3RhdGUSLwoFbW9kZWwYASABKAsyGS5iaW5reXJhaWx3YXlzLnYxLlJhaWx3YXlSBW1vZGVsEi0KE2lzX3J1bl9tb2RlX2VuYWJsZWQYAiABKAhSEGlzUnVuTW9kZUVuYWJsZWQSNQoXaXNfdmlydHVhbF9tb2RlX2VuYWJsZWQYAyABKAhSFGlzVmlydHVhbE1vZGVFbmFibGVkEjsKGmlzX3ZpcnR1YWxfYXV0b3J1bl9lbmFibGVkGAQgASgIUhdpc1ZpcnR1YWxBdXRvcnVuRW5hYmxlZBIhCgxwb3dlcl9hY3R1YWwYCiABKAhSC3Bvd2VyQWN0dWFsEicKD3Bvd2VyX3JlcXVlc3RlZBgLIAEoCFIOcG93ZXJSZXF1ZXN0ZWQSOAoYYXV0b21hdGljX2NvbnRyb2xfYWN0dWFsGAwgASgIUhZhdXRvbWF0aWNDb250cm9sQWN0dWFsEj4KG2F1dG9tYXRpY19jb250cm9sX3JlcXVlc3RlZBgNIAEoCFIZYXV0b21hdGljQ29udHJvbFJlcXVlc3RlZBIyCgZibG9ja3MYMiADKAsyGi5iaW5reXJhaWx3YXlzLnYxLkJsb2NrUmVmUgZibG9ja3MSQgoMYmxvY2tfZ3JvdXBzGDMgAygLMh8uYmlua3lyYWlsd2F5cy52MS5CbG9ja0dyb3VwUmVmUgtibG9ja0dyb3VwcxJOChBjb21tYW5kX3N0YXRpb25zGDQgAygLMiMuYmlua3lyYWlsd2F5cy52MS5Db21tYW5kU3RhdGlvblJlZlIPY29tbWFuZFN0YXRpb25zEjsKCWp1bmN0aW9ucxg1IAMoCzIdLmJpbmt5cmFpbHdheXMudjEuSnVuY3Rpb25SZWZSCWp1bmN0aW9ucxIsCgRsb2NzGDYgAygLMhguYmlua3lyYWlsd2F5cy52MS5Mb2NSZWZSBGxvY3MSNQoHb3V0cHV0cxg3IAMoCzIbLmJpbmt5cmFpbHdheXMudjEuT3V0cHV0UmVmUgdvdXRwdXRzEjIKBnJvdXRlcxg4IAMoCzIaLmJpbmt5cmFpbHdheXMudjEuUm91dGVSZWZSBnJvdXRlcxI1CgdzZW5zb3JzGDkgAygLMhsuYmlua3lyYWlsd2F5cy52MS5TZW5zb3JSZWZSB3NlbnNvcnMSNQoHc2lnbmFscxg6IAMoCzIbLmJpbmt5cmFpbHdheXMudjEuU2lnbmFsUmVmUgdzaWduYWxz');
@$core.Deprecated('Use locStateDescriptor instead')
const LocState$json = const {
  '1': 'LocState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.Loc', '10': 'model'},
    const {'1': 'controlled_automatically_actual', '3': 10, '4': 1, '5': 8, '10': 'controlledAutomaticallyActual'},
    const {'1': 'controlled_automatically_requested', '3': 11, '4': 1, '5': 8, '10': 'controlledAutomaticallyRequested'},
    const {'1': 'can_be_controlled_automatically', '3': 12, '4': 1, '5': 8, '10': 'canBeControlledAutomatically'},
    const {'1': 'automatic_state', '3': 13, '4': 1, '5': 14, '6': '.binkyrailways.v1.AutoLocState', '10': 'automaticState'},
    const {'1': 'current_route', '3': 18, '4': 1, '5': 11, '6': '.binkyrailways.v1.RouteRef', '10': 'currentRoute'},
    const {'1': 'wait_after_current_route', '3': 20, '4': 1, '5': 8, '10': 'waitAfterCurrentRoute'},
    const {'1': 'is_current_route_duration_exceeded', '3': 22, '4': 1, '5': 8, '10': 'isCurrentRouteDurationExceeded'},
    const {'1': 'next_route', '3': 23, '4': 1, '5': 11, '6': '.binkyrailways.v1.RouteRef', '10': 'nextRoute'},
    const {'1': 'current_block', '3': 24, '4': 1, '5': 11, '6': '.binkyrailways.v1.BlockRef', '10': 'currentBlock'},
    const {'1': 'speed_actual', '3': 50, '4': 1, '5': 5, '10': 'speedActual'},
    const {'1': 'speed_requested', '3': 51, '4': 1, '5': 5, '10': 'speedRequested'},
    const {'1': 'speed_text', '3': 52, '4': 1, '5': 9, '10': 'speedText'},
    const {'1': 'state_text', '3': 53, '4': 1, '5': 9, '10': 'stateText'},
    const {'1': 'speed_in_steps_actual', '3': 54, '4': 1, '5': 5, '10': 'speedInStepsActual'},
    const {'1': 'speed_in_steps_requested', '3': 55, '4': 1, '5': 5, '10': 'speedInStepsRequested'},
    const {'1': 'direction_actual', '3': 56, '4': 1, '5': 14, '6': '.binkyrailways.v1.LocDirection', '10': 'directionActual'},
    const {'1': 'direction_requested', '3': 57, '4': 1, '5': 14, '6': '.binkyrailways.v1.LocDirection', '10': 'directionRequested'},
    const {'1': 'is_reversing', '3': 60, '4': 1, '5': 8, '10': 'isReversing'},
    const {'1': 'f0_actual', '3': 70, '4': 1, '5': 8, '10': 'f0Actual'},
    const {'1': 'f0_requested', '3': 71, '4': 1, '5': 8, '10': 'f0Requested'},
  ],
};

/// Descriptor for `LocState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List locStateDescriptor = $convert.base64Decode('CghMb2NTdGF0ZRIrCgVtb2RlbBgBIAEoCzIVLmJpbmt5cmFpbHdheXMudjEuTG9jUgVtb2RlbBJGCh9jb250cm9sbGVkX2F1dG9tYXRpY2FsbHlfYWN0dWFsGAogASgIUh1jb250cm9sbGVkQXV0b21hdGljYWxseUFjdHVhbBJMCiJjb250cm9sbGVkX2F1dG9tYXRpY2FsbHlfcmVxdWVzdGVkGAsgASgIUiBjb250cm9sbGVkQXV0b21hdGljYWxseVJlcXVlc3RlZBJFCh9jYW5fYmVfY29udHJvbGxlZF9hdXRvbWF0aWNhbGx5GAwgASgIUhxjYW5CZUNvbnRyb2xsZWRBdXRvbWF0aWNhbGx5EkcKD2F1dG9tYXRpY19zdGF0ZRgNIAEoDjIeLmJpbmt5cmFpbHdheXMudjEuQXV0b0xvY1N0YXRlUg5hdXRvbWF0aWNTdGF0ZRI/Cg1jdXJyZW50X3JvdXRlGBIgASgLMhouYmlua3lyYWlsd2F5cy52MS5Sb3V0ZVJlZlIMY3VycmVudFJvdXRlEjcKGHdhaXRfYWZ0ZXJfY3VycmVudF9yb3V0ZRgUIAEoCFIVd2FpdEFmdGVyQ3VycmVudFJvdXRlEkoKImlzX2N1cnJlbnRfcm91dGVfZHVyYXRpb25fZXhjZWVkZWQYFiABKAhSHmlzQ3VycmVudFJvdXRlRHVyYXRpb25FeGNlZWRlZBI5CgpuZXh0X3JvdXRlGBcgASgLMhouYmlua3lyYWlsd2F5cy52MS5Sb3V0ZVJlZlIJbmV4dFJvdXRlEj8KDWN1cnJlbnRfYmxvY2sYGCABKAsyGi5iaW5reXJhaWx3YXlzLnYxLkJsb2NrUmVmUgxjdXJyZW50QmxvY2sSIQoMc3BlZWRfYWN0dWFsGDIgASgFUgtzcGVlZEFjdHVhbBInCg9zcGVlZF9yZXF1ZXN0ZWQYMyABKAVSDnNwZWVkUmVxdWVzdGVkEh0KCnNwZWVkX3RleHQYNCABKAlSCXNwZWVkVGV4dBIdCgpzdGF0ZV90ZXh0GDUgASgJUglzdGF0ZVRleHQSMQoVc3BlZWRfaW5fc3RlcHNfYWN0dWFsGDYgASgFUhJzcGVlZEluU3RlcHNBY3R1YWwSNwoYc3BlZWRfaW5fc3RlcHNfcmVxdWVzdGVkGDcgASgFUhVzcGVlZEluU3RlcHNSZXF1ZXN0ZWQSSQoQZGlyZWN0aW9uX2FjdHVhbBg4IAEoDjIeLmJpbmt5cmFpbHdheXMudjEuTG9jRGlyZWN0aW9uUg9kaXJlY3Rpb25BY3R1YWwSTwoTZGlyZWN0aW9uX3JlcXVlc3RlZBg5IAEoDjIeLmJpbmt5cmFpbHdheXMudjEuTG9jRGlyZWN0aW9uUhJkaXJlY3Rpb25SZXF1ZXN0ZWQSIQoMaXNfcmV2ZXJzaW5nGDwgASgIUgtpc1JldmVyc2luZxIbCglmMF9hY3R1YWwYRiABKAhSCGYwQWN0dWFsEiEKDGYwX3JlcXVlc3RlZBhHIAEoCFILZjBSZXF1ZXN0ZWQ=');
@$core.Deprecated('Use commandStationStateDescriptor instead')
const CommandStationState$json = const {
  '1': 'CommandStationState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.CommandStation', '10': 'model'},
    const {'1': 'hardware_modules', '3': 2, '4': 3, '5': 11, '6': '.binkyrailways.v1.HardwareModule', '10': 'hardwareModules'},
  ],
};

/// Descriptor for `CommandStationState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List commandStationStateDescriptor = $convert.base64Decode('ChNDb21tYW5kU3RhdGlvblN0YXRlEjYKBW1vZGVsGAEgASgLMiAuYmlua3lyYWlsd2F5cy52MS5Db21tYW5kU3RhdGlvblIFbW9kZWwSSwoQaGFyZHdhcmVfbW9kdWxlcxgCIAMoCzIgLmJpbmt5cmFpbHdheXMudjEuSGFyZHdhcmVNb2R1bGVSD2hhcmR3YXJlTW9kdWxlcw==');
@$core.Deprecated('Use hardwareModuleDescriptor instead')
const HardwareModule$json = const {
  '1': 'HardwareModule',
  '2': const [
    const {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    const {'1': 'uptime', '3': 2, '4': 1, '5': 3, '10': 'uptime'},
    const {'1': 'last_updated_at', '3': 3, '4': 1, '5': 9, '10': 'lastUpdatedAt'},
    const {'1': 'error_messages', '3': 4, '4': 3, '5': 9, '10': 'errorMessages'},
    const {'1': 'address', '3': 5, '4': 1, '5': 9, '10': 'address'},
  ],
};

/// Descriptor for `HardwareModule`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List hardwareModuleDescriptor = $convert.base64Decode('Cg5IYXJkd2FyZU1vZHVsZRIOCgJpZBgBIAEoCVICaWQSFgoGdXB0aW1lGAIgASgDUgZ1cHRpbWUSJgoPbGFzdF91cGRhdGVkX2F0GAMgASgJUg1sYXN0VXBkYXRlZEF0EiUKDmVycm9yX21lc3NhZ2VzGAQgAygJUg1lcnJvck1lc3NhZ2VzEhgKB2FkZHJlc3MYBSABKAlSB2FkZHJlc3M=');
@$core.Deprecated('Use blockStateDescriptor instead')
const BlockState$json = const {
  '1': 'BlockState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.Block', '10': 'model'},
    const {'1': 'locked_by', '3': 2, '4': 1, '5': 11, '6': '.binkyrailways.v1.LocRef', '10': 'lockedBy'},
    const {'1': 'closed_actual', '3': 10, '4': 1, '5': 8, '10': 'closedActual'},
    const {'1': 'closed_requested', '3': 11, '4': 1, '5': 8, '10': 'closedRequested'},
    const {'1': 'is_deadend', '3': 20, '4': 1, '5': 8, '10': 'isDeadend'},
    const {'1': 'is_station', '3': 21, '4': 1, '5': 8, '10': 'isStation'},
    const {'1': 'has_waiting_loc', '3': 22, '4': 1, '5': 8, '10': 'hasWaitingLoc'},
  ],
};

/// Descriptor for `BlockState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List blockStateDescriptor = $convert.base64Decode('CgpCbG9ja1N0YXRlEi0KBW1vZGVsGAEgASgLMhcuYmlua3lyYWlsd2F5cy52MS5CbG9ja1IFbW9kZWwSNQoJbG9ja2VkX2J5GAIgASgLMhguYmlua3lyYWlsd2F5cy52MS5Mb2NSZWZSCGxvY2tlZEJ5EiMKDWNsb3NlZF9hY3R1YWwYCiABKAhSDGNsb3NlZEFjdHVhbBIpChBjbG9zZWRfcmVxdWVzdGVkGAsgASgIUg9jbG9zZWRSZXF1ZXN0ZWQSHQoKaXNfZGVhZGVuZBgUIAEoCFIJaXNEZWFkZW5kEh0KCmlzX3N0YXRpb24YFSABKAhSCWlzU3RhdGlvbhImCg9oYXNfd2FpdGluZ19sb2MYFiABKAhSDWhhc1dhaXRpbmdMb2M=');
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
    const {'1': 'switch', '3': 2, '4': 1, '5': 11, '6': '.binkyrailways.v1.SwitchState', '10': 'switch'},
  ],
};

/// Descriptor for `JunctionState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List junctionStateDescriptor = $convert.base64Decode('Cg1KdW5jdGlvblN0YXRlEjAKBW1vZGVsGAEgASgLMhouYmlua3lyYWlsd2F5cy52MS5KdW5jdGlvblIFbW9kZWwSNQoGc3dpdGNoGAIgASgLMh0uYmlua3lyYWlsd2F5cy52MS5Td2l0Y2hTdGF0ZVIGc3dpdGNo');
@$core.Deprecated('Use switchStateDescriptor instead')
const SwitchState$json = const {
  '1': 'SwitchState',
  '2': const [
    const {'1': 'direction_actual', '3': 1, '4': 1, '5': 14, '6': '.binkyrailways.v1.SwitchDirection', '10': 'directionActual'},
    const {'1': 'direction_requested', '3': 2, '4': 1, '5': 14, '6': '.binkyrailways.v1.SwitchDirection', '10': 'directionRequested'},
  ],
};

/// Descriptor for `SwitchState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List switchStateDescriptor = $convert.base64Decode('CgtTd2l0Y2hTdGF0ZRJMChBkaXJlY3Rpb25fYWN0dWFsGAEgASgOMiEuYmlua3lyYWlsd2F5cy52MS5Td2l0Y2hEaXJlY3Rpb25SD2RpcmVjdGlvbkFjdHVhbBJSChNkaXJlY3Rpb25fcmVxdWVzdGVkGAIgASgOMiEuYmlua3lyYWlsd2F5cy52MS5Td2l0Y2hEaXJlY3Rpb25SEmRpcmVjdGlvblJlcXVlc3RlZA==');
@$core.Deprecated('Use outputStateDescriptor instead')
const OutputState$json = const {
  '1': 'OutputState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.Output', '10': 'model'},
    const {'1': 'binary_output', '3': 2, '4': 1, '5': 11, '6': '.binkyrailways.v1.BinaryOutputState', '10': 'binaryOutput'},
  ],
};

/// Descriptor for `OutputState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List outputStateDescriptor = $convert.base64Decode('CgtPdXRwdXRTdGF0ZRIuCgVtb2RlbBgBIAEoCzIYLmJpbmt5cmFpbHdheXMudjEuT3V0cHV0UgVtb2RlbBJICg1iaW5hcnlfb3V0cHV0GAIgASgLMiMuYmlua3lyYWlsd2F5cy52MS5CaW5hcnlPdXRwdXRTdGF0ZVIMYmluYXJ5T3V0cHV0');
@$core.Deprecated('Use binaryOutputStateDescriptor instead')
const BinaryOutputState$json = const {
  '1': 'BinaryOutputState',
  '2': const [
    const {'1': 'active_actual', '3': 1, '4': 1, '5': 8, '10': 'activeActual'},
    const {'1': 'active_requested', '3': 2, '4': 1, '5': 8, '10': 'activeRequested'},
  ],
};

/// Descriptor for `BinaryOutputState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List binaryOutputStateDescriptor = $convert.base64Decode('ChFCaW5hcnlPdXRwdXRTdGF0ZRIjCg1hY3RpdmVfYWN0dWFsGAEgASgIUgxhY3RpdmVBY3R1YWwSKQoQYWN0aXZlX3JlcXVlc3RlZBgCIAEoCFIPYWN0aXZlUmVxdWVzdGVk');
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
    const {'1': 'active', '3': 2, '4': 1, '5': 8, '10': 'active'},
  ],
};

/// Descriptor for `SensorState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List sensorStateDescriptor = $convert.base64Decode('CgtTZW5zb3JTdGF0ZRIuCgVtb2RlbBgBIAEoCzIYLmJpbmt5cmFpbHdheXMudjEuU2Vuc29yUgVtb2RlbBIWCgZhY3RpdmUYAiABKAhSBmFjdGl2ZQ==');
@$core.Deprecated('Use signalStateDescriptor instead')
const SignalState$json = const {
  '1': 'SignalState',
  '2': const [
    const {'1': 'model', '3': 1, '4': 1, '5': 11, '6': '.binkyrailways.v1.Signal', '10': 'model'},
  ],
};

/// Descriptor for `SignalState`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List signalStateDescriptor = $convert.base64Decode('CgtTaWduYWxTdGF0ZRIuCgVtb2RlbBgBIAEoCzIYLmJpbmt5cmFpbHdheXMudjEuU2lnbmFsUgVtb2RlbA==');
