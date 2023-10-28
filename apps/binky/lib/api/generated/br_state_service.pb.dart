///
//  Generated code. Do not modify.
//  source: br_state_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

import 'dart:core' as $core;

import 'package:protobuf/protobuf.dart' as $pb;

import 'br_state_types.pb.dart' as $3;

import 'br_state_types.pbenum.dart' as $3;
import 'br_model_types.pbenum.dart' as $1;

class EnableRunModeRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'EnableRunModeRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOB(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'virtual')
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'autoRun')
    ..hasRequiredFields = false
  ;

  EnableRunModeRequest._() : super();
  factory EnableRunModeRequest({
    $core.bool? virtual,
    $core.bool? autoRun,
  }) {
    final _result = create();
    if (virtual != null) {
      _result.virtual = virtual;
    }
    if (autoRun != null) {
      _result.autoRun = autoRun;
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

  @$pb.TagNumber(2)
  $core.bool get autoRun => $_getBF(1);
  @$pb.TagNumber(2)
  set autoRun($core.bool v) { $_setBool(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasAutoRun() => $_has(1);
  @$pb.TagNumber(2)
  void clearAutoRun() => clearField(2);
}

class GetStateChangesRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'GetStateChangesRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOB(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'bootstrap')
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'bootstrapOnly')
    ..hasRequiredFields = false
  ;

  GetStateChangesRequest._() : super();
  factory GetStateChangesRequest({
    $core.bool? bootstrap,
    $core.bool? bootstrapOnly,
  }) {
    final _result = create();
    if (bootstrap != null) {
      _result.bootstrap = bootstrap;
    }
    if (bootstrapOnly != null) {
      _result.bootstrapOnly = bootstrapOnly;
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

  @$pb.TagNumber(2)
  $core.bool get bootstrapOnly => $_getBF(1);
  @$pb.TagNumber(2)
  set bootstrapOnly($core.bool v) { $_setBool(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasBootstrapOnly() => $_has(1);
  @$pb.TagNumber(2)
  void clearBootstrapOnly() => clearField(2);
}

class StateChange extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'StateChange', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$3.RailwayState>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'railway', subBuilder: $3.RailwayState.create)
    ..aOM<$3.LocState>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'loc', subBuilder: $3.LocState.create)
    ..aOM<$3.CommandStationState>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'commandStation', protoName: 'commandStation', subBuilder: $3.CommandStationState.create)
    ..aOM<$3.BlockState>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'block', subBuilder: $3.BlockState.create)
    ..aOM<$3.BlockGroupState>(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'blockGroup', protoName: 'blockGroup', subBuilder: $3.BlockGroupState.create)
    ..aOM<$3.JunctionState>(6, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'junction', subBuilder: $3.JunctionState.create)
    ..aOM<$3.OutputState>(7, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'output', subBuilder: $3.OutputState.create)
    ..aOM<$3.RouteState>(8, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'route', subBuilder: $3.RouteState.create)
    ..aOM<$3.SensorState>(9, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'sensor', subBuilder: $3.SensorState.create)
    ..aOM<$3.SignalState>(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'signal', subBuilder: $3.SignalState.create)
    ..hasRequiredFields = false
  ;

  StateChange._() : super();
  factory StateChange({
    $3.RailwayState? railway,
    $3.LocState? loc,
    $3.CommandStationState? commandStation,
    $3.BlockState? block,
    $3.BlockGroupState? blockGroup,
    $3.JunctionState? junction,
    $3.OutputState? output,
    $3.RouteState? route,
    $3.SensorState? sensor,
    $3.SignalState? signal,
  }) {
    final _result = create();
    if (railway != null) {
      _result.railway = railway;
    }
    if (loc != null) {
      _result.loc = loc;
    }
    if (commandStation != null) {
      _result.commandStation = commandStation;
    }
    if (block != null) {
      _result.block = block;
    }
    if (blockGroup != null) {
      _result.blockGroup = blockGroup;
    }
    if (junction != null) {
      _result.junction = junction;
    }
    if (output != null) {
      _result.output = output;
    }
    if (route != null) {
      _result.route = route;
    }
    if (sensor != null) {
      _result.sensor = sensor;
    }
    if (signal != null) {
      _result.signal = signal;
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
  $3.RailwayState get railway => $_getN(0);
  @$pb.TagNumber(1)
  set railway($3.RailwayState v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasRailway() => $_has(0);
  @$pb.TagNumber(1)
  void clearRailway() => clearField(1);
  @$pb.TagNumber(1)
  $3.RailwayState ensureRailway() => $_ensure(0);

  @$pb.TagNumber(2)
  $3.LocState get loc => $_getN(1);
  @$pb.TagNumber(2)
  set loc($3.LocState v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasLoc() => $_has(1);
  @$pb.TagNumber(2)
  void clearLoc() => clearField(2);
  @$pb.TagNumber(2)
  $3.LocState ensureLoc() => $_ensure(1);

  @$pb.TagNumber(3)
  $3.CommandStationState get commandStation => $_getN(2);
  @$pb.TagNumber(3)
  set commandStation($3.CommandStationState v) { setField(3, v); }
  @$pb.TagNumber(3)
  $core.bool hasCommandStation() => $_has(2);
  @$pb.TagNumber(3)
  void clearCommandStation() => clearField(3);
  @$pb.TagNumber(3)
  $3.CommandStationState ensureCommandStation() => $_ensure(2);

  @$pb.TagNumber(4)
  $3.BlockState get block => $_getN(3);
  @$pb.TagNumber(4)
  set block($3.BlockState v) { setField(4, v); }
  @$pb.TagNumber(4)
  $core.bool hasBlock() => $_has(3);
  @$pb.TagNumber(4)
  void clearBlock() => clearField(4);
  @$pb.TagNumber(4)
  $3.BlockState ensureBlock() => $_ensure(3);

  @$pb.TagNumber(5)
  $3.BlockGroupState get blockGroup => $_getN(4);
  @$pb.TagNumber(5)
  set blockGroup($3.BlockGroupState v) { setField(5, v); }
  @$pb.TagNumber(5)
  $core.bool hasBlockGroup() => $_has(4);
  @$pb.TagNumber(5)
  void clearBlockGroup() => clearField(5);
  @$pb.TagNumber(5)
  $3.BlockGroupState ensureBlockGroup() => $_ensure(4);

  @$pb.TagNumber(6)
  $3.JunctionState get junction => $_getN(5);
  @$pb.TagNumber(6)
  set junction($3.JunctionState v) { setField(6, v); }
  @$pb.TagNumber(6)
  $core.bool hasJunction() => $_has(5);
  @$pb.TagNumber(6)
  void clearJunction() => clearField(6);
  @$pb.TagNumber(6)
  $3.JunctionState ensureJunction() => $_ensure(5);

  @$pb.TagNumber(7)
  $3.OutputState get output => $_getN(6);
  @$pb.TagNumber(7)
  set output($3.OutputState v) { setField(7, v); }
  @$pb.TagNumber(7)
  $core.bool hasOutput() => $_has(6);
  @$pb.TagNumber(7)
  void clearOutput() => clearField(7);
  @$pb.TagNumber(7)
  $3.OutputState ensureOutput() => $_ensure(6);

  @$pb.TagNumber(8)
  $3.RouteState get route => $_getN(7);
  @$pb.TagNumber(8)
  set route($3.RouteState v) { setField(8, v); }
  @$pb.TagNumber(8)
  $core.bool hasRoute() => $_has(7);
  @$pb.TagNumber(8)
  void clearRoute() => clearField(8);
  @$pb.TagNumber(8)
  $3.RouteState ensureRoute() => $_ensure(7);

  @$pb.TagNumber(9)
  $3.SensorState get sensor => $_getN(8);
  @$pb.TagNumber(9)
  set sensor($3.SensorState v) { setField(9, v); }
  @$pb.TagNumber(9)
  $core.bool hasSensor() => $_has(8);
  @$pb.TagNumber(9)
  void clearSensor() => clearField(9);
  @$pb.TagNumber(9)
  $3.SensorState ensureSensor() => $_ensure(8);

  @$pb.TagNumber(10)
  $3.SignalState get signal => $_getN(9);
  @$pb.TagNumber(10)
  set signal($3.SignalState v) { setField(10, v); }
  @$pb.TagNumber(10)
  $core.bool hasSignal() => $_has(9);
  @$pb.TagNumber(10)
  void clearSignal() => clearField(10);
  @$pb.TagNumber(10)
  $3.SignalState ensureSignal() => $_ensure(9);
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

class SetAutomaticControlRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SetAutomaticControlRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOB(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'enabled')
    ..hasRequiredFields = false
  ;

  SetAutomaticControlRequest._() : super();
  factory SetAutomaticControlRequest({
    $core.bool? enabled,
  }) {
    final _result = create();
    if (enabled != null) {
      _result.enabled = enabled;
    }
    return _result;
  }
  factory SetAutomaticControlRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SetAutomaticControlRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SetAutomaticControlRequest clone() => SetAutomaticControlRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SetAutomaticControlRequest copyWith(void Function(SetAutomaticControlRequest) updates) => super.copyWith((message) => updates(message as SetAutomaticControlRequest)) as SetAutomaticControlRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SetAutomaticControlRequest create() => SetAutomaticControlRequest._();
  SetAutomaticControlRequest createEmptyInstance() => create();
  static $pb.PbList<SetAutomaticControlRequest> createRepeated() => $pb.PbList<SetAutomaticControlRequest>();
  @$core.pragma('dart2js:noInline')
  static SetAutomaticControlRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SetAutomaticControlRequest>(create);
  static SetAutomaticControlRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.bool get enabled => $_getBF(0);
  @$pb.TagNumber(1)
  set enabled($core.bool v) { $_setBool(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasEnabled() => $_has(0);
  @$pb.TagNumber(1)
  void clearEnabled() => clearField(1);
}

class SetLocSpeedAndDirectionRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SetLocSpeedAndDirectionRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..a<$core.int>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'speed', $pb.PbFieldType.O3)
    ..e<$3.LocDirection>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'direction', $pb.PbFieldType.OE, defaultOrMaker: $3.LocDirection.FORWARD, valueOf: $3.LocDirection.valueOf, enumValues: $3.LocDirection.values)
    ..hasRequiredFields = false
  ;

  SetLocSpeedAndDirectionRequest._() : super();
  factory SetLocSpeedAndDirectionRequest({
    $core.String? id,
    $core.int? speed,
    $3.LocDirection? direction,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (speed != null) {
      _result.speed = speed;
    }
    if (direction != null) {
      _result.direction = direction;
    }
    return _result;
  }
  factory SetLocSpeedAndDirectionRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SetLocSpeedAndDirectionRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SetLocSpeedAndDirectionRequest clone() => SetLocSpeedAndDirectionRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SetLocSpeedAndDirectionRequest copyWith(void Function(SetLocSpeedAndDirectionRequest) updates) => super.copyWith((message) => updates(message as SetLocSpeedAndDirectionRequest)) as SetLocSpeedAndDirectionRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SetLocSpeedAndDirectionRequest create() => SetLocSpeedAndDirectionRequest._();
  SetLocSpeedAndDirectionRequest createEmptyInstance() => create();
  static $pb.PbList<SetLocSpeedAndDirectionRequest> createRepeated() => $pb.PbList<SetLocSpeedAndDirectionRequest>();
  @$core.pragma('dart2js:noInline')
  static SetLocSpeedAndDirectionRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SetLocSpeedAndDirectionRequest>(create);
  static SetLocSpeedAndDirectionRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.int get speed => $_getIZ(1);
  @$pb.TagNumber(2)
  set speed($core.int v) { $_setSignedInt32(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasSpeed() => $_has(1);
  @$pb.TagNumber(2)
  void clearSpeed() => clearField(2);

  @$pb.TagNumber(3)
  $3.LocDirection get direction => $_getN(2);
  @$pb.TagNumber(3)
  set direction($3.LocDirection v) { setField(3, v); }
  @$pb.TagNumber(3)
  $core.bool hasDirection() => $_has(2);
  @$pb.TagNumber(3)
  void clearDirection() => clearField(3);
}

class SetLocControlledAutomaticallyRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SetLocControlledAutomaticallyRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'enabled')
    ..hasRequiredFields = false
  ;

  SetLocControlledAutomaticallyRequest._() : super();
  factory SetLocControlledAutomaticallyRequest({
    $core.String? id,
    $core.bool? enabled,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (enabled != null) {
      _result.enabled = enabled;
    }
    return _result;
  }
  factory SetLocControlledAutomaticallyRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SetLocControlledAutomaticallyRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SetLocControlledAutomaticallyRequest clone() => SetLocControlledAutomaticallyRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SetLocControlledAutomaticallyRequest copyWith(void Function(SetLocControlledAutomaticallyRequest) updates) => super.copyWith((message) => updates(message as SetLocControlledAutomaticallyRequest)) as SetLocControlledAutomaticallyRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SetLocControlledAutomaticallyRequest create() => SetLocControlledAutomaticallyRequest._();
  SetLocControlledAutomaticallyRequest createEmptyInstance() => create();
  static $pb.PbList<SetLocControlledAutomaticallyRequest> createRepeated() => $pb.PbList<SetLocControlledAutomaticallyRequest>();
  @$core.pragma('dart2js:noInline')
  static SetLocControlledAutomaticallyRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SetLocControlledAutomaticallyRequest>(create);
  static SetLocControlledAutomaticallyRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.bool get enabled => $_getBF(1);
  @$pb.TagNumber(2)
  set enabled($core.bool v) { $_setBool(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasEnabled() => $_has(1);
  @$pb.TagNumber(2)
  void clearEnabled() => clearField(2);
}

class SetLocFunctionsRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SetLocFunctionsRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..pc<LocFunction>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'functions', $pb.PbFieldType.PM, subBuilder: LocFunction.create)
    ..hasRequiredFields = false
  ;

  SetLocFunctionsRequest._() : super();
  factory SetLocFunctionsRequest({
    $core.String? id,
    $core.Iterable<LocFunction>? functions,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (functions != null) {
      _result.functions.addAll(functions);
    }
    return _result;
  }
  factory SetLocFunctionsRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SetLocFunctionsRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SetLocFunctionsRequest clone() => SetLocFunctionsRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SetLocFunctionsRequest copyWith(void Function(SetLocFunctionsRequest) updates) => super.copyWith((message) => updates(message as SetLocFunctionsRequest)) as SetLocFunctionsRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SetLocFunctionsRequest create() => SetLocFunctionsRequest._();
  SetLocFunctionsRequest createEmptyInstance() => create();
  static $pb.PbList<SetLocFunctionsRequest> createRepeated() => $pb.PbList<SetLocFunctionsRequest>();
  @$core.pragma('dart2js:noInline')
  static SetLocFunctionsRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SetLocFunctionsRequest>(create);
  static SetLocFunctionsRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.List<LocFunction> get functions => $_getList(1);
}

class LocFunction extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'LocFunction', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..a<$core.int>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'index', $pb.PbFieldType.O3)
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'value')
    ..hasRequiredFields = false
  ;

  LocFunction._() : super();
  factory LocFunction({
    $core.int? index,
    $core.bool? value,
  }) {
    final _result = create();
    if (index != null) {
      _result.index = index;
    }
    if (value != null) {
      _result.value = value;
    }
    return _result;
  }
  factory LocFunction.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory LocFunction.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  LocFunction clone() => LocFunction()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  LocFunction copyWith(void Function(LocFunction) updates) => super.copyWith((message) => updates(message as LocFunction)) as LocFunction; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static LocFunction create() => LocFunction._();
  LocFunction createEmptyInstance() => create();
  static $pb.PbList<LocFunction> createRepeated() => $pb.PbList<LocFunction>();
  @$core.pragma('dart2js:noInline')
  static LocFunction getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<LocFunction>(create);
  static LocFunction? _defaultInstance;

  @$pb.TagNumber(1)
  $core.int get index => $_getIZ(0);
  @$pb.TagNumber(1)
  set index($core.int v) { $_setSignedInt32(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasIndex() => $_has(0);
  @$pb.TagNumber(1)
  void clearIndex() => clearField(1);

  @$pb.TagNumber(2)
  $core.bool get value => $_getBF(1);
  @$pb.TagNumber(2)
  set value($core.bool v) { $_setBool(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasValue() => $_has(1);
  @$pb.TagNumber(2)
  void clearValue() => clearField(2);
}

class SetSwitchDirectionRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SetSwitchDirectionRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..e<$1.SwitchDirection>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'direction', $pb.PbFieldType.OE, defaultOrMaker: $1.SwitchDirection.STRAIGHT, valueOf: $1.SwitchDirection.valueOf, enumValues: $1.SwitchDirection.values)
    ..hasRequiredFields = false
  ;

  SetSwitchDirectionRequest._() : super();
  factory SetSwitchDirectionRequest({
    $core.String? id,
    $1.SwitchDirection? direction,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (direction != null) {
      _result.direction = direction;
    }
    return _result;
  }
  factory SetSwitchDirectionRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SetSwitchDirectionRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SetSwitchDirectionRequest clone() => SetSwitchDirectionRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SetSwitchDirectionRequest copyWith(void Function(SetSwitchDirectionRequest) updates) => super.copyWith((message) => updates(message as SetSwitchDirectionRequest)) as SetSwitchDirectionRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SetSwitchDirectionRequest create() => SetSwitchDirectionRequest._();
  SetSwitchDirectionRequest createEmptyInstance() => create();
  static $pb.PbList<SetSwitchDirectionRequest> createRepeated() => $pb.PbList<SetSwitchDirectionRequest>();
  @$core.pragma('dart2js:noInline')
  static SetSwitchDirectionRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SetSwitchDirectionRequest>(create);
  static SetSwitchDirectionRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $1.SwitchDirection get direction => $_getN(1);
  @$pb.TagNumber(2)
  set direction($1.SwitchDirection v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasDirection() => $_has(1);
  @$pb.TagNumber(2)
  void clearDirection() => clearField(2);
}

class SetBinaryOutputActiveRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SetBinaryOutputActiveRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'active')
    ..hasRequiredFields = false
  ;

  SetBinaryOutputActiveRequest._() : super();
  factory SetBinaryOutputActiveRequest({
    $core.String? id,
    $core.bool? active,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (active != null) {
      _result.active = active;
    }
    return _result;
  }
  factory SetBinaryOutputActiveRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SetBinaryOutputActiveRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SetBinaryOutputActiveRequest clone() => SetBinaryOutputActiveRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SetBinaryOutputActiveRequest copyWith(void Function(SetBinaryOutputActiveRequest) updates) => super.copyWith((message) => updates(message as SetBinaryOutputActiveRequest)) as SetBinaryOutputActiveRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SetBinaryOutputActiveRequest create() => SetBinaryOutputActiveRequest._();
  SetBinaryOutputActiveRequest createEmptyInstance() => create();
  static $pb.PbList<SetBinaryOutputActiveRequest> createRepeated() => $pb.PbList<SetBinaryOutputActiveRequest>();
  @$core.pragma('dart2js:noInline')
  static SetBinaryOutputActiveRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SetBinaryOutputActiveRequest>(create);
  static SetBinaryOutputActiveRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.bool get active => $_getBF(1);
  @$pb.TagNumber(2)
  set active($core.bool v) { $_setBool(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasActive() => $_has(1);
  @$pb.TagNumber(2)
  void clearActive() => clearField(2);
}

class ClickVirtualSensorRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'ClickVirtualSensorRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  ClickVirtualSensorRequest._() : super();
  factory ClickVirtualSensorRequest({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory ClickVirtualSensorRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory ClickVirtualSensorRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  ClickVirtualSensorRequest clone() => ClickVirtualSensorRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  ClickVirtualSensorRequest copyWith(void Function(ClickVirtualSensorRequest) updates) => super.copyWith((message) => updates(message as ClickVirtualSensorRequest)) as ClickVirtualSensorRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static ClickVirtualSensorRequest create() => ClickVirtualSensorRequest._();
  ClickVirtualSensorRequest createEmptyInstance() => create();
  static $pb.PbList<ClickVirtualSensorRequest> createRepeated() => $pb.PbList<ClickVirtualSensorRequest>();
  @$core.pragma('dart2js:noInline')
  static ClickVirtualSensorRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<ClickVirtualSensorRequest>(create);
  static ClickVirtualSensorRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

class AssignLocToBlockRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'AssignLocToBlockRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'locId')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'blockId')
    ..e<$1.BlockSide>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'blockSide', $pb.PbFieldType.OE, defaultOrMaker: $1.BlockSide.FRONT, valueOf: $1.BlockSide.valueOf, enumValues: $1.BlockSide.values)
    ..hasRequiredFields = false
  ;

  AssignLocToBlockRequest._() : super();
  factory AssignLocToBlockRequest({
    $core.String? locId,
    $core.String? blockId,
    $1.BlockSide? blockSide,
  }) {
    final _result = create();
    if (locId != null) {
      _result.locId = locId;
    }
    if (blockId != null) {
      _result.blockId = blockId;
    }
    if (blockSide != null) {
      _result.blockSide = blockSide;
    }
    return _result;
  }
  factory AssignLocToBlockRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory AssignLocToBlockRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  AssignLocToBlockRequest clone() => AssignLocToBlockRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  AssignLocToBlockRequest copyWith(void Function(AssignLocToBlockRequest) updates) => super.copyWith((message) => updates(message as AssignLocToBlockRequest)) as AssignLocToBlockRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static AssignLocToBlockRequest create() => AssignLocToBlockRequest._();
  AssignLocToBlockRequest createEmptyInstance() => create();
  static $pb.PbList<AssignLocToBlockRequest> createRepeated() => $pb.PbList<AssignLocToBlockRequest>();
  @$core.pragma('dart2js:noInline')
  static AssignLocToBlockRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<AssignLocToBlockRequest>(create);
  static AssignLocToBlockRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get locId => $_getSZ(0);
  @$pb.TagNumber(1)
  set locId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasLocId() => $_has(0);
  @$pb.TagNumber(1)
  void clearLocId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get blockId => $_getSZ(1);
  @$pb.TagNumber(2)
  set blockId($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasBlockId() => $_has(1);
  @$pb.TagNumber(2)
  void clearBlockId() => clearField(2);

  @$pb.TagNumber(3)
  $1.BlockSide get blockSide => $_getN(2);
  @$pb.TagNumber(3)
  set blockSide($1.BlockSide v) { setField(3, v); }
  @$pb.TagNumber(3)
  $core.bool hasBlockSide() => $_has(2);
  @$pb.TagNumber(3)
  void clearBlockSide() => clearField(3);
}

class TakeLocOfTrackRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'TakeLocOfTrackRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'locId')
    ..hasRequiredFields = false
  ;

  TakeLocOfTrackRequest._() : super();
  factory TakeLocOfTrackRequest({
    $core.String? locId,
  }) {
    final _result = create();
    if (locId != null) {
      _result.locId = locId;
    }
    return _result;
  }
  factory TakeLocOfTrackRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory TakeLocOfTrackRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  TakeLocOfTrackRequest clone() => TakeLocOfTrackRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  TakeLocOfTrackRequest copyWith(void Function(TakeLocOfTrackRequest) updates) => super.copyWith((message) => updates(message as TakeLocOfTrackRequest)) as TakeLocOfTrackRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static TakeLocOfTrackRequest create() => TakeLocOfTrackRequest._();
  TakeLocOfTrackRequest createEmptyInstance() => create();
  static $pb.PbList<TakeLocOfTrackRequest> createRepeated() => $pb.PbList<TakeLocOfTrackRequest>();
  @$core.pragma('dart2js:noInline')
  static TakeLocOfTrackRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<TakeLocOfTrackRequest>(create);
  static TakeLocOfTrackRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get locId => $_getSZ(0);
  @$pb.TagNumber(1)
  set locId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasLocId() => $_has(0);
  @$pb.TagNumber(1)
  void clearLocId() => clearField(1);
}

class SetBlockClosedRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SetBlockClosedRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'closed')
    ..hasRequiredFields = false
  ;

  SetBlockClosedRequest._() : super();
  factory SetBlockClosedRequest({
    $core.String? id,
    $core.bool? closed,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (closed != null) {
      _result.closed = closed;
    }
    return _result;
  }
  factory SetBlockClosedRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SetBlockClosedRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SetBlockClosedRequest clone() => SetBlockClosedRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SetBlockClosedRequest copyWith(void Function(SetBlockClosedRequest) updates) => super.copyWith((message) => updates(message as SetBlockClosedRequest)) as SetBlockClosedRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SetBlockClosedRequest create() => SetBlockClosedRequest._();
  SetBlockClosedRequest createEmptyInstance() => create();
  static $pb.PbList<SetBlockClosedRequest> createRepeated() => $pb.PbList<SetBlockClosedRequest>();
  @$core.pragma('dart2js:noInline')
  static SetBlockClosedRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SetBlockClosedRequest>(create);
  static SetBlockClosedRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.bool get closed => $_getBF(1);
  @$pb.TagNumber(2)
  set closed($core.bool v) { $_setBool(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasClosed() => $_has(1);
  @$pb.TagNumber(2)
  void clearClosed() => clearField(2);
}

class DiscoverHardwareRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'DiscoverHardwareRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'hardwareModuleId')
    ..hasRequiredFields = false
  ;

  DiscoverHardwareRequest._() : super();
  factory DiscoverHardwareRequest({
    $core.String? hardwareModuleId,
  }) {
    final _result = create();
    if (hardwareModuleId != null) {
      _result.hardwareModuleId = hardwareModuleId;
    }
    return _result;
  }
  factory DiscoverHardwareRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory DiscoverHardwareRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  DiscoverHardwareRequest clone() => DiscoverHardwareRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  DiscoverHardwareRequest copyWith(void Function(DiscoverHardwareRequest) updates) => super.copyWith((message) => updates(message as DiscoverHardwareRequest)) as DiscoverHardwareRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static DiscoverHardwareRequest create() => DiscoverHardwareRequest._();
  DiscoverHardwareRequest createEmptyInstance() => create();
  static $pb.PbList<DiscoverHardwareRequest> createRepeated() => $pb.PbList<DiscoverHardwareRequest>();
  @$core.pragma('dart2js:noInline')
  static DiscoverHardwareRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<DiscoverHardwareRequest>(create);
  static DiscoverHardwareRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get hardwareModuleId => $_getSZ(0);
  @$pb.TagNumber(1)
  set hardwareModuleId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasHardwareModuleId() => $_has(0);
  @$pb.TagNumber(1)
  void clearHardwareModuleId() => clearField(1);
}

class DiscoverHardwareResponse extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'DiscoverHardwareResponse', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..hasRequiredFields = false
  ;

  DiscoverHardwareResponse._() : super();
  factory DiscoverHardwareResponse() => create();
  factory DiscoverHardwareResponse.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory DiscoverHardwareResponse.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  DiscoverHardwareResponse clone() => DiscoverHardwareResponse()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  DiscoverHardwareResponse copyWith(void Function(DiscoverHardwareResponse) updates) => super.copyWith((message) => updates(message as DiscoverHardwareResponse)) as DiscoverHardwareResponse; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static DiscoverHardwareResponse create() => DiscoverHardwareResponse._();
  DiscoverHardwareResponse createEmptyInstance() => create();
  static $pb.PbList<DiscoverHardwareResponse> createRepeated() => $pb.PbList<DiscoverHardwareResponse>();
  @$core.pragma('dart2js:noInline')
  static DiscoverHardwareResponse getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<DiscoverHardwareResponse>(create);
  static DiscoverHardwareResponse? _defaultInstance;
}

