///
//  Generated code. Do not modify.
//  source: br_model_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

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

class VehicleType extends $pb.ProtobufEnum {
  static const VehicleType LOC = VehicleType._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'LOC');
  static const VehicleType CAR = VehicleType._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'CAR');

  static const $core.List<VehicleType> values = <VehicleType> [
    LOC,
    CAR,
  ];

  static final $core.Map<$core.int, VehicleType> _byValue = $pb.ProtobufEnum.initByValue(values);
  static VehicleType? valueOf($core.int value) => _byValue[value];

  const VehicleType._($core.int v, $core.String n) : super(v, n);
}

class BinkyNetLocalWorkerType extends $pb.ProtobufEnum {
  static const BinkyNetLocalWorkerType LINUX = BinkyNetLocalWorkerType._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'LINUX');
  static const BinkyNetLocalWorkerType ESPHOME = BinkyNetLocalWorkerType._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'ESPHOME');

  static const $core.List<BinkyNetLocalWorkerType> values = <BinkyNetLocalWorkerType> [
    LINUX,
    ESPHOME,
  ];

  static final $core.Map<$core.int, BinkyNetLocalWorkerType> _byValue = $pb.ProtobufEnum.initByValue(values);
  static BinkyNetLocalWorkerType? valueOf($core.int value) => _byValue[value];

  const BinkyNetLocalWorkerType._($core.int v, $core.String n) : super(v, n);
}

class BinkyNetDeviceType extends $pb.ProtobufEnum {
  static const BinkyNetDeviceType MCP23008 = BinkyNetDeviceType._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'MCP23008');
  static const BinkyNetDeviceType MCP23017 = BinkyNetDeviceType._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'MCP23017');
  static const BinkyNetDeviceType PCA9685 = BinkyNetDeviceType._(2, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'PCA9685');
  static const BinkyNetDeviceType PCF8574 = BinkyNetDeviceType._(3, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'PCF8574');
  static const BinkyNetDeviceType ADS1115 = BinkyNetDeviceType._(4, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'ADS1115');
  static const BinkyNetDeviceType BINKYCARSENSOR = BinkyNetDeviceType._(5, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'BINKYCARSENSOR');
  static const BinkyNetDeviceType MQTT_GPIO = BinkyNetDeviceType._(6, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'MQTT_GPIO');
  static const BinkyNetDeviceType MQTT_SERVO = BinkyNetDeviceType._(7, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'MQTT_SERVO');

  static const $core.List<BinkyNetDeviceType> values = <BinkyNetDeviceType> [
    MCP23008,
    MCP23017,
    PCA9685,
    PCF8574,
    ADS1115,
    BINKYCARSENSOR,
    MQTT_GPIO,
    MQTT_SERVO,
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
  static const BinkyNetObjectType MAGNETICSWITCH = BinkyNetObjectType._(5, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'MAGNETICSWITCH');

  static const $core.List<BinkyNetObjectType> values = <BinkyNetObjectType> [
    BINARYSENSOR,
    BINARYOUTPUT,
    SERVOSWITCH,
    RELAYSWITCH,
    TRACKINVERTER,
    MAGNETICSWITCH,
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

class BinaryOutputType extends $pb.ProtobufEnum {
  static const BinaryOutputType BOT_DEFAULT = BinaryOutputType._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'BOT_DEFAULT');
  static const BinaryOutputType BOT_TRACKINVERTER = BinaryOutputType._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'BOT_TRACKINVERTER');

  static const $core.List<BinaryOutputType> values = <BinaryOutputType> [
    BOT_DEFAULT,
    BOT_TRACKINVERTER,
  ];

  static final $core.Map<$core.int, BinaryOutputType> _byValue = $pb.ProtobufEnum.initByValue(values);
  static BinaryOutputType? valueOf($core.int value) => _byValue[value];

  const BinaryOutputType._($core.int v, $core.String n) : super(v, n);
}

class BlockSide extends $pb.ProtobufEnum {
  static const BlockSide FRONT = BlockSide._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'FRONT');
  static const BlockSide BACK = BlockSide._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'BACK');

  static const $core.List<BlockSide> values = <BlockSide> [
    FRONT,
    BACK,
  ];

  static final $core.Map<$core.int, BlockSide> _byValue = $pb.ProtobufEnum.initByValue(values);
  static BlockSide? valueOf($core.int value) => _byValue[value];

  const BlockSide._($core.int v, $core.String n) : super(v, n);
}

class RouteStateBehavior extends $pb.ProtobufEnum {
  static const RouteStateBehavior RSB_NOCHANGE = RouteStateBehavior._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'RSB_NOCHANGE');
  static const RouteStateBehavior RSB_ENTER = RouteStateBehavior._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'RSB_ENTER');
  static const RouteStateBehavior RSB_REACHED = RouteStateBehavior._(2, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'RSB_REACHED');

  static const $core.List<RouteStateBehavior> values = <RouteStateBehavior> [
    RSB_NOCHANGE,
    RSB_ENTER,
    RSB_REACHED,
  ];

  static final $core.Map<$core.int, RouteStateBehavior> _byValue = $pb.ProtobufEnum.initByValue(values);
  static RouteStateBehavior? valueOf($core.int value) => _byValue[value];

  const RouteStateBehavior._($core.int v, $core.String n) : super(v, n);
}

class LocSpeedBehavior extends $pb.ProtobufEnum {
  static const LocSpeedBehavior LSB_DEFAULT = LocSpeedBehavior._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'LSB_DEFAULT');
  static const LocSpeedBehavior LSB_NOCHANGE = LocSpeedBehavior._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'LSB_NOCHANGE');
  static const LocSpeedBehavior LSB_MEDIUM = LocSpeedBehavior._(2, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'LSB_MEDIUM');
  static const LocSpeedBehavior LSB_MINIMUM = LocSpeedBehavior._(3, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'LSB_MINIMUM');
  static const LocSpeedBehavior LSB_MAXIMUM = LocSpeedBehavior._(4, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'LSB_MAXIMUM');

  static const $core.List<LocSpeedBehavior> values = <LocSpeedBehavior> [
    LSB_DEFAULT,
    LSB_NOCHANGE,
    LSB_MEDIUM,
    LSB_MINIMUM,
    LSB_MAXIMUM,
  ];

  static final $core.Map<$core.int, LocSpeedBehavior> _byValue = $pb.ProtobufEnum.initByValue(values);
  static LocSpeedBehavior? valueOf($core.int value) => _byValue[value];

  const LocSpeedBehavior._($core.int v, $core.String n) : super(v, n);
}

class Shape extends $pb.ProtobufEnum {
  static const Shape CIRCLE = Shape._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'CIRCLE');
  static const Shape TRIANGLE = Shape._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'TRIANGLE');
  static const Shape SQUARE = Shape._(2, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'SQUARE');
  static const Shape DIAMOND = Shape._(3, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'DIAMOND');

  static const $core.List<Shape> values = <Shape> [
    CIRCLE,
    TRIANGLE,
    SQUARE,
    DIAMOND,
  ];

  static final $core.Map<$core.int, Shape> _byValue = $pb.ProtobufEnum.initByValue(values);
  static Shape? valueOf($core.int value) => _byValue[value];

  const Shape._($core.int v, $core.String n) : super(v, n);
}

class BlockSignalType extends $pb.ProtobufEnum {
  static const BlockSignalType ENTRY = BlockSignalType._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'ENTRY');
  static const BlockSignalType EXIT = BlockSignalType._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'EXIT');

  static const $core.List<BlockSignalType> values = <BlockSignalType> [
    ENTRY,
    EXIT,
  ];

  static final $core.Map<$core.int, BlockSignalType> _byValue = $pb.ProtobufEnum.initByValue(values);
  static BlockSignalType? valueOf($core.int value) => _byValue[value];

  const BlockSignalType._($core.int v, $core.String n) : super(v, n);
}

