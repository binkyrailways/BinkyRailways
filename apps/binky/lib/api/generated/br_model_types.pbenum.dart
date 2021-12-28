///
//  Generated code. Do not modify.
//  source: br_model_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields

// ignore_for_file: UNDEFINED_SHOWN_NAME
import 'dart:core' as $core;
import 'package:protobuf/protobuf.dart' as $pb;

class AddressType extends $pb.ProtobufEnum {
  static const AddressType BINKYNET = AddressType._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'BINKYNET');
  static const AddressType DCC = AddressType._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'DCC');
  static const AddressType LOCONET = AddressType._(2, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'LOCONET');
  static const AddressType MOTOROLA = AddressType._(3, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'MOTOROLA');
  static const AddressType MFX = AddressType._(4, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'MFX');
  static const AddressType MQTT = AddressType._(5, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'MQTT');

  static const $core.List<AddressType> values = <AddressType> [
    BINKYNET,
    DCC,
    LOCONET,
    MOTOROLA,
    MFX,
    MQTT,
  ];

  static final $core.Map<$core.int, AddressType> _byValue = $pb.ProtobufEnum.initByValue(values);
  static AddressType? valueOf($core.int value) => _byValue[value];

  const AddressType._($core.int v, $core.String n) : super(v, n);
}

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

