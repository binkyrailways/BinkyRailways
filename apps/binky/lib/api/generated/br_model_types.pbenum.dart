///
//  Generated code. Do not modify.
//  source: br_model_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields

// ignore_for_file: UNDEFINED_SHOWN_NAME
import 'dart:core' as $core;
import 'package:protobuf/protobuf.dart' as $pb;

class ChangeDirection extends $pb.ProtobufEnum {
  static const ChangeDirection ALLOW = ChangeDirection._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'ALLOW');
  static const ChangeDirection AVOID = ChangeDirection._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'AVOID');

  static const $core.List<ChangeDirection> values = <ChangeDirection> [
    ALLOW,
    AVOID,
  ];

  static final $core.Map<$core.int, ChangeDirection> _byValue = $pb.ProtobufEnum.initByValue(values);
  static ChangeDirection? valueOf($core.int value) => _byValue[value];

  const ChangeDirection._($core.int v, $core.String n) : super(v, n);
}

class BinkyNetDeviceType extends $pb.ProtobufEnum {
  static const BinkyNetDeviceType MCP23008 = BinkyNetDeviceType._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'MCP23008');
  static const BinkyNetDeviceType MCP23017 = BinkyNetDeviceType._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'MCP23017');
  static const BinkyNetDeviceType PCA9685 = BinkyNetDeviceType._(2, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'PCA9685');
  static const BinkyNetDeviceType PCF8574 = BinkyNetDeviceType._(3, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'PCF8574');

  static const $core.List<BinkyNetDeviceType> values = <BinkyNetDeviceType> [
    MCP23008,
    MCP23017,
    PCA9685,
    PCF8574,
  ];

  static final $core.Map<$core.int, BinkyNetDeviceType> _byValue = $pb.ProtobufEnum.initByValue(values);
  static BinkyNetDeviceType? valueOf($core.int value) => _byValue[value];

  const BinkyNetDeviceType._($core.int v, $core.String n) : super(v, n);
}

class BinkyNetObjectType extends $pb.ProtobufEnum {
  static const BinkyNetObjectType BINARYSENSOR = BinkyNetObjectType._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'BINARYSENSOR');
  static const BinkyNetObjectType BINARYOUTPUT = BinkyNetObjectType._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'BINARYOUTPUT');
  static const BinkyNetObjectType SERVOSWITCH = BinkyNetObjectType._(2, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'SERVOSWITCH');
  static const BinkyNetObjectType RELAYSWITCH = BinkyNetObjectType._(3, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'RELAYSWITCH');
  static const BinkyNetObjectType TRACKINVERTER = BinkyNetObjectType._(4, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'TRACKINVERTER');

  static const $core.List<BinkyNetObjectType> values = <BinkyNetObjectType> [
    BINARYSENSOR,
    BINARYOUTPUT,
    SERVOSWITCH,
    RELAYSWITCH,
    TRACKINVERTER,
  ];

  static final $core.Map<$core.int, BinkyNetObjectType> _byValue = $pb.ProtobufEnum.initByValue(values);
  static BinkyNetObjectType? valueOf($core.int value) => _byValue[value];

  const BinkyNetObjectType._($core.int v, $core.String n) : super(v, n);
}

class SwitchDirection extends $pb.ProtobufEnum {
  static const SwitchDirection STRAIGHT = SwitchDirection._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'STRAIGHT');
  static const SwitchDirection OFF = SwitchDirection._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'OFF');

  static const $core.List<SwitchDirection> values = <SwitchDirection> [
    STRAIGHT,
    OFF,
  ];

  static final $core.Map<$core.int, SwitchDirection> _byValue = $pb.ProtobufEnum.initByValue(values);
  static SwitchDirection? valueOf($core.int value) => _byValue[value];

  const SwitchDirection._($core.int v, $core.String n) : super(v, n);
}

