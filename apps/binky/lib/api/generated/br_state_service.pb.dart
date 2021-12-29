///
//  Generated code. Do not modify.
//  source: br_state_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields

import 'dart:core' as $core;

import 'package:protobuf/protobuf.dart' as $pb;

import 'br_state_types.pb.dart' as $2;

class EnableRunModeRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'EnableRunModeRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOB(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'virtual')
    ..hasRequiredFields = false
  ;

  EnableRunModeRequest._() : super();
  factory EnableRunModeRequest({
    $core.bool? virtual,
  }) {
    final _result = create();
    if (virtual != null) {
      _result.virtual = virtual;
    }
    return _result;
  }
  factory EnableRunModeRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory EnableRunModeRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  EnableRunModeRequest clone() => EnableRunModeRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  EnableRunModeRequest copyWith(void Function(EnableRunModeRequest) updates) => super.copyWith((message) => updates(message as EnableRunModeRequest)) as EnableRunModeRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static EnableRunModeRequest create() => EnableRunModeRequest._();
  EnableRunModeRequest createEmptyInstance() => create();
  static $pb.PbList<EnableRunModeRequest> createRepeated() => $pb.PbList<EnableRunModeRequest>();
  @$core.pragma('dart2js:noInline')
  static EnableRunModeRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<EnableRunModeRequest>(create);
  static EnableRunModeRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.bool get virtual => $_getBF(0);
  @$pb.TagNumber(1)
  set virtual($core.bool v) { $_setBool(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasVirtual() => $_has(0);
  @$pb.TagNumber(1)
  void clearVirtual() => clearField(1);
}

class GetStateChangesRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'GetStateChangesRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOB(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'bootstrap')
    ..hasRequiredFields = false
  ;

  GetStateChangesRequest._() : super();
  factory GetStateChangesRequest({
    $core.bool? bootstrap,
  }) {
    final _result = create();
    if (bootstrap != null) {
      _result.bootstrap = bootstrap;
    }
    return _result;
  }
  factory GetStateChangesRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory GetStateChangesRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  GetStateChangesRequest clone() => GetStateChangesRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  GetStateChangesRequest copyWith(void Function(GetStateChangesRequest) updates) => super.copyWith((message) => updates(message as GetStateChangesRequest)) as GetStateChangesRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static GetStateChangesRequest create() => GetStateChangesRequest._();
  GetStateChangesRequest createEmptyInstance() => create();
  static $pb.PbList<GetStateChangesRequest> createRepeated() => $pb.PbList<GetStateChangesRequest>();
  @$core.pragma('dart2js:noInline')
  static GetStateChangesRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<GetStateChangesRequest>(create);
  static GetStateChangesRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.bool get bootstrap => $_getBF(0);
  @$pb.TagNumber(1)
  set bootstrap($core.bool v) { $_setBool(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasBootstrap() => $_has(0);
  @$pb.TagNumber(1)
  void clearBootstrap() => clearField(1);
}

class StateChange extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'StateChange', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$2.RailwayState>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'railway', subBuilder: $2.RailwayState.create)
    ..aOM<$2.BlockState>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'block', subBuilder: $2.BlockState.create)
    ..aOM<$2.LocState>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'loc', subBuilder: $2.LocState.create)
    ..hasRequiredFields = false
  ;

  StateChange._() : super();
  factory StateChange({
    $2.RailwayState? railway,
    $2.BlockState? block,
    $2.LocState? loc,
  }) {
    final _result = create();
    if (railway != null) {
      _result.railway = railway;
    }
    if (block != null) {
      _result.block = block;
    }
    if (loc != null) {
      _result.loc = loc;
    }
    return _result;
  }
  factory StateChange.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory StateChange.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  StateChange clone() => StateChange()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  StateChange copyWith(void Function(StateChange) updates) => super.copyWith((message) => updates(message as StateChange)) as StateChange; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static StateChange create() => StateChange._();
  StateChange createEmptyInstance() => create();
  static $pb.PbList<StateChange> createRepeated() => $pb.PbList<StateChange>();
  @$core.pragma('dart2js:noInline')
  static StateChange getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<StateChange>(create);
  static StateChange? _defaultInstance;

  @$pb.TagNumber(1)
  $2.RailwayState get railway => $_getN(0);
  @$pb.TagNumber(1)
  set railway($2.RailwayState v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasRailway() => $_has(0);
  @$pb.TagNumber(1)
  void clearRailway() => clearField(1);
  @$pb.TagNumber(1)
  $2.RailwayState ensureRailway() => $_ensure(0);

  @$pb.TagNumber(2)
  $2.BlockState get block => $_getN(1);
  @$pb.TagNumber(2)
  set block($2.BlockState v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasBlock() => $_has(1);
  @$pb.TagNumber(2)
  void clearBlock() => clearField(2);
  @$pb.TagNumber(2)
  $2.BlockState ensureBlock() => $_ensure(1);

  @$pb.TagNumber(3)
  $2.LocState get loc => $_getN(2);
  @$pb.TagNumber(3)
  set loc($2.LocState v) { setField(3, v); }
  @$pb.TagNumber(3)
  $core.bool hasLoc() => $_has(2);
  @$pb.TagNumber(3)
  void clearLoc() => clearField(3);
  @$pb.TagNumber(3)
  $2.LocState ensureLoc() => $_ensure(2);
}

class SetPowerRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SetPowerRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOB(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'enabled')
    ..hasRequiredFields = false
  ;

  SetPowerRequest._() : super();
  factory SetPowerRequest({
    $core.bool? enabled,
  }) {
    final _result = create();
    if (enabled != null) {
      _result.enabled = enabled;
    }
    return _result;
  }
  factory SetPowerRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SetPowerRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SetPowerRequest clone() => SetPowerRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SetPowerRequest copyWith(void Function(SetPowerRequest) updates) => super.copyWith((message) => updates(message as SetPowerRequest)) as SetPowerRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SetPowerRequest create() => SetPowerRequest._();
  SetPowerRequest createEmptyInstance() => create();
  static $pb.PbList<SetPowerRequest> createRepeated() => $pb.PbList<SetPowerRequest>();
  @$core.pragma('dart2js:noInline')
  static SetPowerRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SetPowerRequest>(create);
  static SetPowerRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.bool get enabled => $_getBF(0);
  @$pb.TagNumber(1)
  set enabled($core.bool v) { $_setBool(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasEnabled() => $_has(0);
  @$pb.TagNumber(1)
  void clearEnabled() => clearField(1);
}

