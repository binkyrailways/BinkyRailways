///
//  Generated code. Do not modify.
//  source: br_state_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields

import 'dart:core' as $core;

import 'package:protobuf/protobuf.dart' as $pb;

import 'br_model_types.pb.dart' as $0;

class RailwayState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'RailwayState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$0.Railway>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $0.Railway.create)
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isRunModeEnabled')
    ..aOB(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isVirtualModeEnabled')
    ..hasRequiredFields = false
  ;

  RailwayState._() : super();
  factory RailwayState({
    $0.Railway? model,
    $core.bool? isRunModeEnabled,
    $core.bool? isVirtualModeEnabled,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    if (isRunModeEnabled != null) {
      _result.isRunModeEnabled = isRunModeEnabled;
    }
    if (isVirtualModeEnabled != null) {
      _result.isVirtualModeEnabled = isVirtualModeEnabled;
    }
    return _result;
  }
  factory RailwayState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory RailwayState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  RailwayState clone() => RailwayState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  RailwayState copyWith(void Function(RailwayState) updates) => super.copyWith((message) => updates(message as RailwayState)) as RailwayState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static RailwayState create() => RailwayState._();
  RailwayState createEmptyInstance() => create();
  static $pb.PbList<RailwayState> createRepeated() => $pb.PbList<RailwayState>();
  @$core.pragma('dart2js:noInline')
  static RailwayState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<RailwayState>(create);
  static RailwayState? _defaultInstance;

  @$pb.TagNumber(1)
  $0.Railway get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($0.Railway v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $0.Railway ensureModel() => $_ensure(0);

  @$pb.TagNumber(2)
  $core.bool get isRunModeEnabled => $_getBF(1);
  @$pb.TagNumber(2)
  set isRunModeEnabled($core.bool v) { $_setBool(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasIsRunModeEnabled() => $_has(1);
  @$pb.TagNumber(2)
  void clearIsRunModeEnabled() => clearField(2);

  @$pb.TagNumber(3)
  $core.bool get isVirtualModeEnabled => $_getBF(2);
  @$pb.TagNumber(3)
  set isVirtualModeEnabled($core.bool v) { $_setBool(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasIsVirtualModeEnabled() => $_has(2);
  @$pb.TagNumber(3)
  void clearIsVirtualModeEnabled() => clearField(3);
}

