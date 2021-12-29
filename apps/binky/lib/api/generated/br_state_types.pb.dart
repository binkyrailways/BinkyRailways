///
//  Generated code. Do not modify.
//  source: br_state_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields

import 'dart:core' as $core;

import 'package:protobuf/protobuf.dart' as $pb;

import 'br_model_types.pb.dart' as $1;

class RailwayState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'RailwayState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.Railway>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.Railway.create)
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isRunModeEnabled')
    ..aOB(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isVirtualModeEnabled')
    ..aOB(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isVirtualAutorunEnabled')
    ..aOB(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'powerActual')
    ..aOB(11, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'powerRequested')
    ..hasRequiredFields = false
  ;

  RailwayState._() : super();
  factory RailwayState({
    $1.Railway? model,
    $core.bool? isRunModeEnabled,
    $core.bool? isVirtualModeEnabled,
    $core.bool? isVirtualAutorunEnabled,
    $core.bool? powerActual,
    $core.bool? powerRequested,
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
    if (isVirtualAutorunEnabled != null) {
      _result.isVirtualAutorunEnabled = isVirtualAutorunEnabled;
    }
    if (powerActual != null) {
      _result.powerActual = powerActual;
    }
    if (powerRequested != null) {
      _result.powerRequested = powerRequested;
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
  $1.Railway get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($1.Railway v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $1.Railway ensureModel() => $_ensure(0);

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

  @$pb.TagNumber(4)
  $core.bool get isVirtualAutorunEnabled => $_getBF(3);
  @$pb.TagNumber(4)
  set isVirtualAutorunEnabled($core.bool v) { $_setBool(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasIsVirtualAutorunEnabled() => $_has(3);
  @$pb.TagNumber(4)
  void clearIsVirtualAutorunEnabled() => clearField(4);

  @$pb.TagNumber(10)
  $core.bool get powerActual => $_getBF(4);
  @$pb.TagNumber(10)
  set powerActual($core.bool v) { $_setBool(4, v); }
  @$pb.TagNumber(10)
  $core.bool hasPowerActual() => $_has(4);
  @$pb.TagNumber(10)
  void clearPowerActual() => clearField(10);

  @$pb.TagNumber(11)
  $core.bool get powerRequested => $_getBF(5);
  @$pb.TagNumber(11)
  set powerRequested($core.bool v) { $_setBool(5, v); }
  @$pb.TagNumber(11)
  $core.bool hasPowerRequested() => $_has(5);
  @$pb.TagNumber(11)
  void clearPowerRequested() => clearField(11);
}

class BlockState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BlockState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.Block>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.Block.create)
    ..aOB(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'closedActual')
    ..aOB(11, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'closedRequested')
    ..aOB(20, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isDeadend')
    ..aOB(21, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isStation')
    ..aOB(22, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'hasWaitingLoc')
    ..hasRequiredFields = false
  ;

  BlockState._() : super();
  factory BlockState({
    $1.Block? model,
    $core.bool? closedActual,
    $core.bool? closedRequested,
    $core.bool? isDeadend,
    $core.bool? isStation,
    $core.bool? hasWaitingLoc,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    if (closedActual != null) {
      _result.closedActual = closedActual;
    }
    if (closedRequested != null) {
      _result.closedRequested = closedRequested;
    }
    if (isDeadend != null) {
      _result.isDeadend = isDeadend;
    }
    if (isStation != null) {
      _result.isStation = isStation;
    }
    if (hasWaitingLoc != null) {
      _result.hasWaitingLoc = hasWaitingLoc;
    }
    return _result;
  }
  factory BlockState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BlockState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BlockState clone() => BlockState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BlockState copyWith(void Function(BlockState) updates) => super.copyWith((message) => updates(message as BlockState)) as BlockState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BlockState create() => BlockState._();
  BlockState createEmptyInstance() => create();
  static $pb.PbList<BlockState> createRepeated() => $pb.PbList<BlockState>();
  @$core.pragma('dart2js:noInline')
  static BlockState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BlockState>(create);
  static BlockState? _defaultInstance;

  @$pb.TagNumber(1)
  $1.Block get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($1.Block v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $1.Block ensureModel() => $_ensure(0);

  @$pb.TagNumber(10)
  $core.bool get closedActual => $_getBF(1);
  @$pb.TagNumber(10)
  set closedActual($core.bool v) { $_setBool(1, v); }
  @$pb.TagNumber(10)
  $core.bool hasClosedActual() => $_has(1);
  @$pb.TagNumber(10)
  void clearClosedActual() => clearField(10);

  @$pb.TagNumber(11)
  $core.bool get closedRequested => $_getBF(2);
  @$pb.TagNumber(11)
  set closedRequested($core.bool v) { $_setBool(2, v); }
  @$pb.TagNumber(11)
  $core.bool hasClosedRequested() => $_has(2);
  @$pb.TagNumber(11)
  void clearClosedRequested() => clearField(11);

  @$pb.TagNumber(20)
  $core.bool get isDeadend => $_getBF(3);
  @$pb.TagNumber(20)
  set isDeadend($core.bool v) { $_setBool(3, v); }
  @$pb.TagNumber(20)
  $core.bool hasIsDeadend() => $_has(3);
  @$pb.TagNumber(20)
  void clearIsDeadend() => clearField(20);

  @$pb.TagNumber(21)
  $core.bool get isStation => $_getBF(4);
  @$pb.TagNumber(21)
  set isStation($core.bool v) { $_setBool(4, v); }
  @$pb.TagNumber(21)
  $core.bool hasIsStation() => $_has(4);
  @$pb.TagNumber(21)
  void clearIsStation() => clearField(21);

  @$pb.TagNumber(22)
  $core.bool get hasWaitingLoc => $_getBF(5);
  @$pb.TagNumber(22)
  set hasWaitingLoc($core.bool v) { $_setBool(5, v); }
  @$pb.TagNumber(22)
  $core.bool hasHasWaitingLoc() => $_has(5);
  @$pb.TagNumber(22)
  void clearHasWaitingLoc() => clearField(22);
}

class LocState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'LocState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.Loc>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.Loc.create)
    ..hasRequiredFields = false
  ;

  LocState._() : super();
  factory LocState({
    $1.Loc? model,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    return _result;
  }
  factory LocState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory LocState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  LocState clone() => LocState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  LocState copyWith(void Function(LocState) updates) => super.copyWith((message) => updates(message as LocState)) as LocState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static LocState create() => LocState._();
  LocState createEmptyInstance() => create();
  static $pb.PbList<LocState> createRepeated() => $pb.PbList<LocState>();
  @$core.pragma('dart2js:noInline')
  static LocState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<LocState>(create);
  static LocState? _defaultInstance;

  @$pb.TagNumber(1)
  $1.Loc get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($1.Loc v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $1.Loc ensureModel() => $_ensure(0);
}

