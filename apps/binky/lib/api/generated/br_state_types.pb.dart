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

class CommandStationState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'CommandStationState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.CommandStation>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.CommandStation.create)
    ..hasRequiredFields = false
  ;

  CommandStationState._() : super();
  factory CommandStationState({
    $1.CommandStation? model,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    return _result;
  }
  factory CommandStationState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory CommandStationState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  CommandStationState clone() => CommandStationState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  CommandStationState copyWith(void Function(CommandStationState) updates) => super.copyWith((message) => updates(message as CommandStationState)) as CommandStationState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static CommandStationState create() => CommandStationState._();
  CommandStationState createEmptyInstance() => create();
  static $pb.PbList<CommandStationState> createRepeated() => $pb.PbList<CommandStationState>();
  @$core.pragma('dart2js:noInline')
  static CommandStationState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<CommandStationState>(create);
  static CommandStationState? _defaultInstance;

  @$pb.TagNumber(1)
  $1.CommandStation get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($1.CommandStation v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $1.CommandStation ensureModel() => $_ensure(0);
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

class BlockGroupState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BlockGroupState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.BlockGroup>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.BlockGroup.create)
    ..hasRequiredFields = false
  ;

  BlockGroupState._() : super();
  factory BlockGroupState({
    $1.BlockGroup? model,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    return _result;
  }
  factory BlockGroupState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BlockGroupState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BlockGroupState clone() => BlockGroupState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BlockGroupState copyWith(void Function(BlockGroupState) updates) => super.copyWith((message) => updates(message as BlockGroupState)) as BlockGroupState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BlockGroupState create() => BlockGroupState._();
  BlockGroupState createEmptyInstance() => create();
  static $pb.PbList<BlockGroupState> createRepeated() => $pb.PbList<BlockGroupState>();
  @$core.pragma('dart2js:noInline')
  static BlockGroupState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BlockGroupState>(create);
  static BlockGroupState? _defaultInstance;

  @$pb.TagNumber(1)
  $1.BlockGroup get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($1.BlockGroup v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $1.BlockGroup ensureModel() => $_ensure(0);
}

class JunctionState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'JunctionState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.Junction>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.Junction.create)
    ..hasRequiredFields = false
  ;

  JunctionState._() : super();
  factory JunctionState({
    $1.Junction? model,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    return _result;
  }
  factory JunctionState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory JunctionState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  JunctionState clone() => JunctionState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  JunctionState copyWith(void Function(JunctionState) updates) => super.copyWith((message) => updates(message as JunctionState)) as JunctionState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static JunctionState create() => JunctionState._();
  JunctionState createEmptyInstance() => create();
  static $pb.PbList<JunctionState> createRepeated() => $pb.PbList<JunctionState>();
  @$core.pragma('dart2js:noInline')
  static JunctionState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<JunctionState>(create);
  static JunctionState? _defaultInstance;

  @$pb.TagNumber(1)
  $1.Junction get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($1.Junction v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $1.Junction ensureModel() => $_ensure(0);
}

class OutputState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'OutputState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.Output>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.Output.create)
    ..hasRequiredFields = false
  ;

  OutputState._() : super();
  factory OutputState({
    $1.Output? model,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    return _result;
  }
  factory OutputState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory OutputState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  OutputState clone() => OutputState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  OutputState copyWith(void Function(OutputState) updates) => super.copyWith((message) => updates(message as OutputState)) as OutputState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static OutputState create() => OutputState._();
  OutputState createEmptyInstance() => create();
  static $pb.PbList<OutputState> createRepeated() => $pb.PbList<OutputState>();
  @$core.pragma('dart2js:noInline')
  static OutputState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<OutputState>(create);
  static OutputState? _defaultInstance;

  @$pb.TagNumber(1)
  $1.Output get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($1.Output v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $1.Output ensureModel() => $_ensure(0);
}

class RouteState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'RouteState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.Route>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.Route.create)
    ..hasRequiredFields = false
  ;

  RouteState._() : super();
  factory RouteState({
    $1.Route? model,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    return _result;
  }
  factory RouteState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory RouteState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  RouteState clone() => RouteState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  RouteState copyWith(void Function(RouteState) updates) => super.copyWith((message) => updates(message as RouteState)) as RouteState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static RouteState create() => RouteState._();
  RouteState createEmptyInstance() => create();
  static $pb.PbList<RouteState> createRepeated() => $pb.PbList<RouteState>();
  @$core.pragma('dart2js:noInline')
  static RouteState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<RouteState>(create);
  static RouteState? _defaultInstance;

  @$pb.TagNumber(1)
  $1.Route get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($1.Route v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $1.Route ensureModel() => $_ensure(0);
}

class SensorState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SensorState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.Sensor>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.Sensor.create)
    ..hasRequiredFields = false
  ;

  SensorState._() : super();
  factory SensorState({
    $1.Sensor? model,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    return _result;
  }
  factory SensorState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SensorState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SensorState clone() => SensorState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SensorState copyWith(void Function(SensorState) updates) => super.copyWith((message) => updates(message as SensorState)) as SensorState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SensorState create() => SensorState._();
  SensorState createEmptyInstance() => create();
  static $pb.PbList<SensorState> createRepeated() => $pb.PbList<SensorState>();
  @$core.pragma('dart2js:noInline')
  static SensorState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SensorState>(create);
  static SensorState? _defaultInstance;

  @$pb.TagNumber(1)
  $1.Sensor get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($1.Sensor v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $1.Sensor ensureModel() => $_ensure(0);
}

class SignalState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SignalState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.Signal>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.Signal.create)
    ..hasRequiredFields = false
  ;

  SignalState._() : super();
  factory SignalState({
    $1.Signal? model,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    return _result;
  }
  factory SignalState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SignalState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SignalState clone() => SignalState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SignalState copyWith(void Function(SignalState) updates) => super.copyWith((message) => updates(message as SignalState)) as SignalState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SignalState create() => SignalState._();
  SignalState createEmptyInstance() => create();
  static $pb.PbList<SignalState> createRepeated() => $pb.PbList<SignalState>();
  @$core.pragma('dart2js:noInline')
  static SignalState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SignalState>(create);
  static SignalState? _defaultInstance;

  @$pb.TagNumber(1)
  $1.Signal get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($1.Signal v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $1.Signal ensureModel() => $_ensure(0);
}

