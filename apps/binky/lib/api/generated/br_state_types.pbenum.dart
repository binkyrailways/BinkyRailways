///
//  Generated code. Do not modify.
//  source: br_state_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

// ignore_for_file: UNDEFINED_SHOWN_NAME
import 'dart:core' as $core;
import 'package:protobuf/protobuf.dart' as $pb;

class LocDirection extends $pb.ProtobufEnum {
  static const LocDirection FORWARD = LocDirection._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'FORWARD');
  static const LocDirection REVERSE = LocDirection._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'REVERSE');

  static const $core.List<LocDirection> values = <LocDirection> [
    FORWARD,
    REVERSE,
  ];

  static final $core.Map<$core.int, LocDirection> _byValue = $pb.ProtobufEnum.initByValue(values);
  static LocDirection? valueOf($core.int value) => _byValue[value];

  const LocDirection._($core.int v, $core.String n) : super(v, n);
}

class AutoLocState extends $pb.ProtobufEnum {
  static const AutoLocState ASSIGNROUTE = AutoLocState._(0, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'ASSIGNROUTE');
  static const AutoLocState REVERSINGWAITINGFORDIRECTIONCHANGE = AutoLocState._(1, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'REVERSINGWAITINGFORDIRECTIONCHANGE');
  static const AutoLocState WAITINGFORASSIGNEDROUTEREADY = AutoLocState._(2, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'WAITINGFORASSIGNEDROUTEREADY');
  static const AutoLocState RUNNING = AutoLocState._(3, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'RUNNING');
  static const AutoLocState ENTERSENSORACTIVATED = AutoLocState._(4, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'ENTERSENSORACTIVATED');
  static const AutoLocState ENTERINGDESTINATION = AutoLocState._(5, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'ENTERINGDESTINATION');
  static const AutoLocState REACHEDSENSORACTIVATED = AutoLocState._(6, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'REACHEDSENSORACTIVATED');
  static const AutoLocState REACHEDDESTINATION = AutoLocState._(7, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'REACHEDDESTINATION');
  static const AutoLocState WAITINGFORDESTINATIONTIMEOUT = AutoLocState._(8, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'WAITINGFORDESTINATIONTIMEOUT');
  static const AutoLocState WAITINGFORDESTINATIONGROUPMINIMUM = AutoLocState._(9, const $core.bool.fromEnvironment('protobuf.omit_enum_names') ? '' : 'WAITINGFORDESTINATIONGROUPMINIMUM');

  static const $core.List<AutoLocState> values = <AutoLocState> [
    ASSIGNROUTE,
    REVERSINGWAITINGFORDIRECTIONCHANGE,
    WAITINGFORASSIGNEDROUTEREADY,
    RUNNING,
    ENTERSENSORACTIVATED,
    ENTERINGDESTINATION,
    REACHEDSENSORACTIVATED,
    REACHEDDESTINATION,
    WAITINGFORDESTINATIONTIMEOUT,
    WAITINGFORDESTINATIONGROUPMINIMUM,
  ];

  static final $core.Map<$core.int, AutoLocState> _byValue = $pb.ProtobufEnum.initByValue(values);
  static AutoLocState? valueOf($core.int value) => _byValue[value];

  const AutoLocState._($core.int v, $core.String n) : super(v, n);
}

