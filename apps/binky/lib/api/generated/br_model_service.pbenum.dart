///
//  Generated code. Do not modify.
//  source: br_model_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

// ignore_for_file: UNDEFINED_SHOWN_NAME
import 'dart:core' as $core;
import 'package:protobuf/protobuf.dart' as $pb;

class BinkyNetObjectsGroupType extends $pb.ProtobufEnum {
  static const BinkyNetObjectsGroupType SENSORS_8 = BinkyNetObjectsGroupType._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'SENSORS_8');
  static const BinkyNetObjectsGroupType SENSORS_4 = BinkyNetObjectsGroupType._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'SENSORS_4');

  static const $core.List<BinkyNetObjectsGroupType> values = <BinkyNetObjectsGroupType> [
    SENSORS_8,
    SENSORS_4,
  ];

  static final $core.Map<$core.int, BinkyNetObjectsGroupType> _byValue = $pb.ProtobufEnum.initByValue(values);
  static BinkyNetObjectsGroupType? valueOf($core.int value) => _byValue[value];

  const BinkyNetObjectsGroupType._($core.int v, $core.String n) : super(v, n);
}

