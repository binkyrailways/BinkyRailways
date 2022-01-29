///
//  Generated code. Do not modify.
//  source: br_state_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields

import 'dart:core' as $core;

import 'package:fixnum/fixnum.dart' as $fixnum;
import 'package:protobuf/protobuf.dart' as $pb;

import 'br_model_types.pb.dart' as $1;

import 'br_state_types.pbenum.dart';
import 'br_model_types.pbenum.dart' as $1;

export 'br_state_types.pbenum.dart';

class RailwayState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'RailwayState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.Railway>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.Railway.create)
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isRunModeEnabled')
    ..aOB(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isVirtualModeEnabled')
    ..aOB(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isVirtualAutorunEnabled')
    ..aOB(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'powerActual')
    ..aOB(11, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'powerRequested')
    ..aOB(12, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'automaticControlActual')
    ..aOB(13, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'automaticControlRequested')
    ..pc<$1.BlockRef>(50, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'blocks', $pb.PbFieldType.PM, subBuilder: $1.BlockRef.create)
    ..pc<$1.BlockGroupRef>(51, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'blockGroups', $pb.PbFieldType.PM, subBuilder: $1.BlockGroupRef.create)
    ..pc<$1.CommandStationRef>(52, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'commandStations', $pb.PbFieldType.PM, subBuilder: $1.CommandStationRef.create)
    ..pc<$1.JunctionRef>(53, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'junctions', $pb.PbFieldType.PM, subBuilder: $1.JunctionRef.create)
    ..pc<$1.LocRef>(54, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'locs', $pb.PbFieldType.PM, subBuilder: $1.LocRef.create)
    ..pc<$1.OutputRef>(55, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'outputs', $pb.PbFieldType.PM, subBuilder: $1.OutputRef.create)
    ..pc<$1.RouteRef>(56, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'routes', $pb.PbFieldType.PM, subBuilder: $1.RouteRef.create)
    ..pc<$1.SensorRef>(57, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'sensors', $pb.PbFieldType.PM, subBuilder: $1.SensorRef.create)
    ..pc<$1.SignalRef>(58, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'signals', $pb.PbFieldType.PM, subBuilder: $1.SignalRef.create)
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
    $core.bool? automaticControlActual,
    $core.bool? automaticControlRequested,
    $core.Iterable<$1.BlockRef>? blocks,
    $core.Iterable<$1.BlockGroupRef>? blockGroups,
    $core.Iterable<$1.CommandStationRef>? commandStations,
    $core.Iterable<$1.JunctionRef>? junctions,
    $core.Iterable<$1.LocRef>? locs,
    $core.Iterable<$1.OutputRef>? outputs,
    $core.Iterable<$1.RouteRef>? routes,
    $core.Iterable<$1.SensorRef>? sensors,
    $core.Iterable<$1.SignalRef>? signals,
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
    if (automaticControlActual != null) {
      _result.automaticControlActual = automaticControlActual;
    }
    if (automaticControlRequested != null) {
      _result.automaticControlRequested = automaticControlRequested;
    }
    if (blocks != null) {
      _result.blocks.addAll(blocks);
    }
    if (blockGroups != null) {
      _result.blockGroups.addAll(blockGroups);
    }
    if (commandStations != null) {
      _result.commandStations.addAll(commandStations);
    }
    if (junctions != null) {
      _result.junctions.addAll(junctions);
    }
    if (locs != null) {
      _result.locs.addAll(locs);
    }
    if (outputs != null) {
      _result.outputs.addAll(outputs);
    }
    if (routes != null) {
      _result.routes.addAll(routes);
    }
    if (sensors != null) {
      _result.sensors.addAll(sensors);
    }
    if (signals != null) {
      _result.signals.addAll(signals);
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

  @$pb.TagNumber(12)
  $core.bool get automaticControlActual => $_getBF(6);
  @$pb.TagNumber(12)
  set automaticControlActual($core.bool v) { $_setBool(6, v); }
  @$pb.TagNumber(12)
  $core.bool hasAutomaticControlActual() => $_has(6);
  @$pb.TagNumber(12)
  void clearAutomaticControlActual() => clearField(12);

  @$pb.TagNumber(13)
  $core.bool get automaticControlRequested => $_getBF(7);
  @$pb.TagNumber(13)
  set automaticControlRequested($core.bool v) { $_setBool(7, v); }
  @$pb.TagNumber(13)
  $core.bool hasAutomaticControlRequested() => $_has(7);
  @$pb.TagNumber(13)
  void clearAutomaticControlRequested() => clearField(13);

  @$pb.TagNumber(50)
  $core.List<$1.BlockRef> get blocks => $_getList(8);

  @$pb.TagNumber(51)
  $core.List<$1.BlockGroupRef> get blockGroups => $_getList(9);

  @$pb.TagNumber(52)
  $core.List<$1.CommandStationRef> get commandStations => $_getList(10);

  @$pb.TagNumber(53)
  $core.List<$1.JunctionRef> get junctions => $_getList(11);

  @$pb.TagNumber(54)
  $core.List<$1.LocRef> get locs => $_getList(12);

  @$pb.TagNumber(55)
  $core.List<$1.OutputRef> get outputs => $_getList(13);

  @$pb.TagNumber(56)
  $core.List<$1.RouteRef> get routes => $_getList(14);

  @$pb.TagNumber(57)
  $core.List<$1.SensorRef> get sensors => $_getList(15);

  @$pb.TagNumber(58)
  $core.List<$1.SignalRef> get signals => $_getList(16);
}

class LocState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'LocState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.Loc>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.Loc.create)
    ..aOB(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'controlledAutomaticallyActual')
    ..aOB(11, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'controlledAutomaticallyRequested')
    ..aOB(12, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'canBeControlledAutomatically')
    ..e<AutoLocState>(13, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'automaticState', $pb.PbFieldType.OE, defaultOrMaker: AutoLocState.ASSIGNROUTE, valueOf: AutoLocState.valueOf, enumValues: AutoLocState.values)
    ..aOM<$1.RouteRef>(18, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'currentRoute', subBuilder: $1.RouteRef.create)
    ..aOB(20, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'waitAfterCurrentRoute')
    ..aOB(22, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isCurrentRouteDurationExceeded')
    ..aOM<$1.RouteRef>(23, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'nextRoute', subBuilder: $1.RouteRef.create)
    ..aOM<$1.BlockRef>(24, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'currentBlock', subBuilder: $1.BlockRef.create)
    ..a<$core.int>(50, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'speedActual', $pb.PbFieldType.O3)
    ..a<$core.int>(51, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'speedRequested', $pb.PbFieldType.O3)
    ..aOS(52, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'speedText')
    ..aOS(53, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'stateText')
    ..a<$core.int>(54, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'speedInStepsActual', $pb.PbFieldType.O3)
    ..a<$core.int>(55, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'speedInStepsRequested', $pb.PbFieldType.O3)
    ..e<LocDirection>(56, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'directionActual', $pb.PbFieldType.OE, defaultOrMaker: LocDirection.FORWARD, valueOf: LocDirection.valueOf, enumValues: LocDirection.values)
    ..e<LocDirection>(57, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'directionRequested', $pb.PbFieldType.OE, defaultOrMaker: LocDirection.FORWARD, valueOf: LocDirection.valueOf, enumValues: LocDirection.values)
    ..aOB(60, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isReversing')
    ..aOB(70, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'f0Actual')
    ..aOB(71, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'f0Requested')
    ..hasRequiredFields = false
  ;

  LocState._() : super();
  factory LocState({
    $1.Loc? model,
    $core.bool? controlledAutomaticallyActual,
    $core.bool? controlledAutomaticallyRequested,
    $core.bool? canBeControlledAutomatically,
    AutoLocState? automaticState,
    $1.RouteRef? currentRoute,
    $core.bool? waitAfterCurrentRoute,
    $core.bool? isCurrentRouteDurationExceeded,
    $1.RouteRef? nextRoute,
    $1.BlockRef? currentBlock,
    $core.int? speedActual,
    $core.int? speedRequested,
    $core.String? speedText,
    $core.String? stateText,
    $core.int? speedInStepsActual,
    $core.int? speedInStepsRequested,
    LocDirection? directionActual,
    LocDirection? directionRequested,
    $core.bool? isReversing,
    $core.bool? f0Actual,
    $core.bool? f0Requested,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    if (controlledAutomaticallyActual != null) {
      _result.controlledAutomaticallyActual = controlledAutomaticallyActual;
    }
    if (controlledAutomaticallyRequested != null) {
      _result.controlledAutomaticallyRequested = controlledAutomaticallyRequested;
    }
    if (canBeControlledAutomatically != null) {
      _result.canBeControlledAutomatically = canBeControlledAutomatically;
    }
    if (automaticState != null) {
      _result.automaticState = automaticState;
    }
    if (currentRoute != null) {
      _result.currentRoute = currentRoute;
    }
    if (waitAfterCurrentRoute != null) {
      _result.waitAfterCurrentRoute = waitAfterCurrentRoute;
    }
    if (isCurrentRouteDurationExceeded != null) {
      _result.isCurrentRouteDurationExceeded = isCurrentRouteDurationExceeded;
    }
    if (nextRoute != null) {
      _result.nextRoute = nextRoute;
    }
    if (currentBlock != null) {
      _result.currentBlock = currentBlock;
    }
    if (speedActual != null) {
      _result.speedActual = speedActual;
    }
    if (speedRequested != null) {
      _result.speedRequested = speedRequested;
    }
    if (speedText != null) {
      _result.speedText = speedText;
    }
    if (stateText != null) {
      _result.stateText = stateText;
    }
    if (speedInStepsActual != null) {
      _result.speedInStepsActual = speedInStepsActual;
    }
    if (speedInStepsRequested != null) {
      _result.speedInStepsRequested = speedInStepsRequested;
    }
    if (directionActual != null) {
      _result.directionActual = directionActual;
    }
    if (directionRequested != null) {
      _result.directionRequested = directionRequested;
    }
    if (isReversing != null) {
      _result.isReversing = isReversing;
    }
    if (f0Actual != null) {
      _result.f0Actual = f0Actual;
    }
    if (f0Requested != null) {
      _result.f0Requested = f0Requested;
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

  @$pb.TagNumber(10)
  $core.bool get controlledAutomaticallyActual => $_getBF(1);
  @$pb.TagNumber(10)
  set controlledAutomaticallyActual($core.bool v) { $_setBool(1, v); }
  @$pb.TagNumber(10)
  $core.bool hasControlledAutomaticallyActual() => $_has(1);
  @$pb.TagNumber(10)
  void clearControlledAutomaticallyActual() => clearField(10);

  @$pb.TagNumber(11)
  $core.bool get controlledAutomaticallyRequested => $_getBF(2);
  @$pb.TagNumber(11)
  set controlledAutomaticallyRequested($core.bool v) { $_setBool(2, v); }
  @$pb.TagNumber(11)
  $core.bool hasControlledAutomaticallyRequested() => $_has(2);
  @$pb.TagNumber(11)
  void clearControlledAutomaticallyRequested() => clearField(11);

  @$pb.TagNumber(12)
  $core.bool get canBeControlledAutomatically => $_getBF(3);
  @$pb.TagNumber(12)
  set canBeControlledAutomatically($core.bool v) { $_setBool(3, v); }
  @$pb.TagNumber(12)
  $core.bool hasCanBeControlledAutomatically() => $_has(3);
  @$pb.TagNumber(12)
  void clearCanBeControlledAutomatically() => clearField(12);

  @$pb.TagNumber(13)
  AutoLocState get automaticState => $_getN(4);
  @$pb.TagNumber(13)
  set automaticState(AutoLocState v) { setField(13, v); }
  @$pb.TagNumber(13)
  $core.bool hasAutomaticState() => $_has(4);
  @$pb.TagNumber(13)
  void clearAutomaticState() => clearField(13);

  @$pb.TagNumber(18)
  $1.RouteRef get currentRoute => $_getN(5);
  @$pb.TagNumber(18)
  set currentRoute($1.RouteRef v) { setField(18, v); }
  @$pb.TagNumber(18)
  $core.bool hasCurrentRoute() => $_has(5);
  @$pb.TagNumber(18)
  void clearCurrentRoute() => clearField(18);
  @$pb.TagNumber(18)
  $1.RouteRef ensureCurrentRoute() => $_ensure(5);

  @$pb.TagNumber(20)
  $core.bool get waitAfterCurrentRoute => $_getBF(6);
  @$pb.TagNumber(20)
  set waitAfterCurrentRoute($core.bool v) { $_setBool(6, v); }
  @$pb.TagNumber(20)
  $core.bool hasWaitAfterCurrentRoute() => $_has(6);
  @$pb.TagNumber(20)
  void clearWaitAfterCurrentRoute() => clearField(20);

  @$pb.TagNumber(22)
  $core.bool get isCurrentRouteDurationExceeded => $_getBF(7);
  @$pb.TagNumber(22)
  set isCurrentRouteDurationExceeded($core.bool v) { $_setBool(7, v); }
  @$pb.TagNumber(22)
  $core.bool hasIsCurrentRouteDurationExceeded() => $_has(7);
  @$pb.TagNumber(22)
  void clearIsCurrentRouteDurationExceeded() => clearField(22);

  @$pb.TagNumber(23)
  $1.RouteRef get nextRoute => $_getN(8);
  @$pb.TagNumber(23)
  set nextRoute($1.RouteRef v) { setField(23, v); }
  @$pb.TagNumber(23)
  $core.bool hasNextRoute() => $_has(8);
  @$pb.TagNumber(23)
  void clearNextRoute() => clearField(23);
  @$pb.TagNumber(23)
  $1.RouteRef ensureNextRoute() => $_ensure(8);

  @$pb.TagNumber(24)
  $1.BlockRef get currentBlock => $_getN(9);
  @$pb.TagNumber(24)
  set currentBlock($1.BlockRef v) { setField(24, v); }
  @$pb.TagNumber(24)
  $core.bool hasCurrentBlock() => $_has(9);
  @$pb.TagNumber(24)
  void clearCurrentBlock() => clearField(24);
  @$pb.TagNumber(24)
  $1.BlockRef ensureCurrentBlock() => $_ensure(9);

  @$pb.TagNumber(50)
  $core.int get speedActual => $_getIZ(10);
  @$pb.TagNumber(50)
  set speedActual($core.int v) { $_setSignedInt32(10, v); }
  @$pb.TagNumber(50)
  $core.bool hasSpeedActual() => $_has(10);
  @$pb.TagNumber(50)
  void clearSpeedActual() => clearField(50);

  @$pb.TagNumber(51)
  $core.int get speedRequested => $_getIZ(11);
  @$pb.TagNumber(51)
  set speedRequested($core.int v) { $_setSignedInt32(11, v); }
  @$pb.TagNumber(51)
  $core.bool hasSpeedRequested() => $_has(11);
  @$pb.TagNumber(51)
  void clearSpeedRequested() => clearField(51);

  @$pb.TagNumber(52)
  $core.String get speedText => $_getSZ(12);
  @$pb.TagNumber(52)
  set speedText($core.String v) { $_setString(12, v); }
  @$pb.TagNumber(52)
  $core.bool hasSpeedText() => $_has(12);
  @$pb.TagNumber(52)
  void clearSpeedText() => clearField(52);

  @$pb.TagNumber(53)
  $core.String get stateText => $_getSZ(13);
  @$pb.TagNumber(53)
  set stateText($core.String v) { $_setString(13, v); }
  @$pb.TagNumber(53)
  $core.bool hasStateText() => $_has(13);
  @$pb.TagNumber(53)
  void clearStateText() => clearField(53);

  @$pb.TagNumber(54)
  $core.int get speedInStepsActual => $_getIZ(14);
  @$pb.TagNumber(54)
  set speedInStepsActual($core.int v) { $_setSignedInt32(14, v); }
  @$pb.TagNumber(54)
  $core.bool hasSpeedInStepsActual() => $_has(14);
  @$pb.TagNumber(54)
  void clearSpeedInStepsActual() => clearField(54);

  @$pb.TagNumber(55)
  $core.int get speedInStepsRequested => $_getIZ(15);
  @$pb.TagNumber(55)
  set speedInStepsRequested($core.int v) { $_setSignedInt32(15, v); }
  @$pb.TagNumber(55)
  $core.bool hasSpeedInStepsRequested() => $_has(15);
  @$pb.TagNumber(55)
  void clearSpeedInStepsRequested() => clearField(55);

  @$pb.TagNumber(56)
  LocDirection get directionActual => $_getN(16);
  @$pb.TagNumber(56)
  set directionActual(LocDirection v) { setField(56, v); }
  @$pb.TagNumber(56)
  $core.bool hasDirectionActual() => $_has(16);
  @$pb.TagNumber(56)
  void clearDirectionActual() => clearField(56);

  @$pb.TagNumber(57)
  LocDirection get directionRequested => $_getN(17);
  @$pb.TagNumber(57)
  set directionRequested(LocDirection v) { setField(57, v); }
  @$pb.TagNumber(57)
  $core.bool hasDirectionRequested() => $_has(17);
  @$pb.TagNumber(57)
  void clearDirectionRequested() => clearField(57);

  @$pb.TagNumber(60)
  $core.bool get isReversing => $_getBF(18);
  @$pb.TagNumber(60)
  set isReversing($core.bool v) { $_setBool(18, v); }
  @$pb.TagNumber(60)
  $core.bool hasIsReversing() => $_has(18);
  @$pb.TagNumber(60)
  void clearIsReversing() => clearField(60);

  @$pb.TagNumber(70)
  $core.bool get f0Actual => $_getBF(19);
  @$pb.TagNumber(70)
  set f0Actual($core.bool v) { $_setBool(19, v); }
  @$pb.TagNumber(70)
  $core.bool hasF0Actual() => $_has(19);
  @$pb.TagNumber(70)
  void clearF0Actual() => clearField(70);

  @$pb.TagNumber(71)
  $core.bool get f0Requested => $_getBF(20);
  @$pb.TagNumber(71)
  set f0Requested($core.bool v) { $_setBool(20, v); }
  @$pb.TagNumber(71)
  $core.bool hasF0Requested() => $_has(20);
  @$pb.TagNumber(71)
  void clearF0Requested() => clearField(71);
}

class CommandStationState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'CommandStationState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.CommandStation>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.CommandStation.create)
    ..pc<HardwareModule>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'hardwareModules', $pb.PbFieldType.PM, subBuilder: HardwareModule.create)
    ..hasRequiredFields = false
  ;

  CommandStationState._() : super();
  factory CommandStationState({
    $1.CommandStation? model,
    $core.Iterable<HardwareModule>? hardwareModules,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    if (hardwareModules != null) {
      _result.hardwareModules.addAll(hardwareModules);
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

  @$pb.TagNumber(2)
  $core.List<HardwareModule> get hardwareModules => $_getList(1);
}

class HardwareModule extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'HardwareModule', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aInt64(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'uptime')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'lastUpdatedAt')
    ..hasRequiredFields = false
  ;

  HardwareModule._() : super();
  factory HardwareModule({
    $core.String? id,
    $fixnum.Int64? uptime,
    $core.String? lastUpdatedAt,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (uptime != null) {
      _result.uptime = uptime;
    }
    if (lastUpdatedAt != null) {
      _result.lastUpdatedAt = lastUpdatedAt;
    }
    return _result;
  }
  factory HardwareModule.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory HardwareModule.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  HardwareModule clone() => HardwareModule()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  HardwareModule copyWith(void Function(HardwareModule) updates) => super.copyWith((message) => updates(message as HardwareModule)) as HardwareModule; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static HardwareModule create() => HardwareModule._();
  HardwareModule createEmptyInstance() => create();
  static $pb.PbList<HardwareModule> createRepeated() => $pb.PbList<HardwareModule>();
  @$core.pragma('dart2js:noInline')
  static HardwareModule getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<HardwareModule>(create);
  static HardwareModule? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $fixnum.Int64 get uptime => $_getI64(1);
  @$pb.TagNumber(2)
  set uptime($fixnum.Int64 v) { $_setInt64(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasUptime() => $_has(1);
  @$pb.TagNumber(2)
  void clearUptime() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get lastUpdatedAt => $_getSZ(2);
  @$pb.TagNumber(3)
  set lastUpdatedAt($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasLastUpdatedAt() => $_has(2);
  @$pb.TagNumber(3)
  void clearLastUpdatedAt() => clearField(3);
}

class BlockState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BlockState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.Block>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.Block.create)
    ..aOM<$1.LocRef>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'lockedBy', subBuilder: $1.LocRef.create)
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
    $1.LocRef? lockedBy,
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
    if (lockedBy != null) {
      _result.lockedBy = lockedBy;
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

  @$pb.TagNumber(2)
  $1.LocRef get lockedBy => $_getN(1);
  @$pb.TagNumber(2)
  set lockedBy($1.LocRef v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasLockedBy() => $_has(1);
  @$pb.TagNumber(2)
  void clearLockedBy() => clearField(2);
  @$pb.TagNumber(2)
  $1.LocRef ensureLockedBy() => $_ensure(1);

  @$pb.TagNumber(10)
  $core.bool get closedActual => $_getBF(2);
  @$pb.TagNumber(10)
  set closedActual($core.bool v) { $_setBool(2, v); }
  @$pb.TagNumber(10)
  $core.bool hasClosedActual() => $_has(2);
  @$pb.TagNumber(10)
  void clearClosedActual() => clearField(10);

  @$pb.TagNumber(11)
  $core.bool get closedRequested => $_getBF(3);
  @$pb.TagNumber(11)
  set closedRequested($core.bool v) { $_setBool(3, v); }
  @$pb.TagNumber(11)
  $core.bool hasClosedRequested() => $_has(3);
  @$pb.TagNumber(11)
  void clearClosedRequested() => clearField(11);

  @$pb.TagNumber(20)
  $core.bool get isDeadend => $_getBF(4);
  @$pb.TagNumber(20)
  set isDeadend($core.bool v) { $_setBool(4, v); }
  @$pb.TagNumber(20)
  $core.bool hasIsDeadend() => $_has(4);
  @$pb.TagNumber(20)
  void clearIsDeadend() => clearField(20);

  @$pb.TagNumber(21)
  $core.bool get isStation => $_getBF(5);
  @$pb.TagNumber(21)
  set isStation($core.bool v) { $_setBool(5, v); }
  @$pb.TagNumber(21)
  $core.bool hasIsStation() => $_has(5);
  @$pb.TagNumber(21)
  void clearIsStation() => clearField(21);

  @$pb.TagNumber(22)
  $core.bool get hasWaitingLoc => $_getBF(6);
  @$pb.TagNumber(22)
  set hasWaitingLoc($core.bool v) { $_setBool(6, v); }
  @$pb.TagNumber(22)
  $core.bool hasHasWaitingLoc() => $_has(6);
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
    ..aOM<SwitchState>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'switch', subBuilder: SwitchState.create)
    ..hasRequiredFields = false
  ;

  JunctionState._() : super();
  factory JunctionState({
    $1.Junction? model,
    SwitchState? switch_2,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    if (switch_2 != null) {
      _result.switch_2 = switch_2;
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

  @$pb.TagNumber(2)
  SwitchState get switch_2 => $_getN(1);
  @$pb.TagNumber(2)
  set switch_2(SwitchState v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasSwitch_2() => $_has(1);
  @$pb.TagNumber(2)
  void clearSwitch_2() => clearField(2);
  @$pb.TagNumber(2)
  SwitchState ensureSwitch_2() => $_ensure(1);
}

class SwitchState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SwitchState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..e<$1.SwitchDirection>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'directionActual', $pb.PbFieldType.OE, defaultOrMaker: $1.SwitchDirection.STRAIGHT, valueOf: $1.SwitchDirection.valueOf, enumValues: $1.SwitchDirection.values)
    ..e<$1.SwitchDirection>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'directionRequested', $pb.PbFieldType.OE, defaultOrMaker: $1.SwitchDirection.STRAIGHT, valueOf: $1.SwitchDirection.valueOf, enumValues: $1.SwitchDirection.values)
    ..hasRequiredFields = false
  ;

  SwitchState._() : super();
  factory SwitchState({
    $1.SwitchDirection? directionActual,
    $1.SwitchDirection? directionRequested,
  }) {
    final _result = create();
    if (directionActual != null) {
      _result.directionActual = directionActual;
    }
    if (directionRequested != null) {
      _result.directionRequested = directionRequested;
    }
    return _result;
  }
  factory SwitchState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SwitchState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SwitchState clone() => SwitchState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SwitchState copyWith(void Function(SwitchState) updates) => super.copyWith((message) => updates(message as SwitchState)) as SwitchState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SwitchState create() => SwitchState._();
  SwitchState createEmptyInstance() => create();
  static $pb.PbList<SwitchState> createRepeated() => $pb.PbList<SwitchState>();
  @$core.pragma('dart2js:noInline')
  static SwitchState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SwitchState>(create);
  static SwitchState? _defaultInstance;

  @$pb.TagNumber(1)
  $1.SwitchDirection get directionActual => $_getN(0);
  @$pb.TagNumber(1)
  set directionActual($1.SwitchDirection v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasDirectionActual() => $_has(0);
  @$pb.TagNumber(1)
  void clearDirectionActual() => clearField(1);

  @$pb.TagNumber(2)
  $1.SwitchDirection get directionRequested => $_getN(1);
  @$pb.TagNumber(2)
  set directionRequested($1.SwitchDirection v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasDirectionRequested() => $_has(1);
  @$pb.TagNumber(2)
  void clearDirectionRequested() => clearField(2);
}

class OutputState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'OutputState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$1.Output>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $1.Output.create)
    ..aOM<BinaryOutputState>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'binaryOutput', subBuilder: BinaryOutputState.create)
    ..hasRequiredFields = false
  ;

  OutputState._() : super();
  factory OutputState({
    $1.Output? model,
    BinaryOutputState? binaryOutput,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    if (binaryOutput != null) {
      _result.binaryOutput = binaryOutput;
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

  @$pb.TagNumber(2)
  BinaryOutputState get binaryOutput => $_getN(1);
  @$pb.TagNumber(2)
  set binaryOutput(BinaryOutputState v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasBinaryOutput() => $_has(1);
  @$pb.TagNumber(2)
  void clearBinaryOutput() => clearField(2);
  @$pb.TagNumber(2)
  BinaryOutputState ensureBinaryOutput() => $_ensure(1);
}

class BinaryOutputState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BinaryOutputState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOB(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'activeActual')
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'activeRequested')
    ..hasRequiredFields = false
  ;

  BinaryOutputState._() : super();
  factory BinaryOutputState({
    $core.bool? activeActual,
    $core.bool? activeRequested,
  }) {
    final _result = create();
    if (activeActual != null) {
      _result.activeActual = activeActual;
    }
    if (activeRequested != null) {
      _result.activeRequested = activeRequested;
    }
    return _result;
  }
  factory BinaryOutputState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BinaryOutputState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BinaryOutputState clone() => BinaryOutputState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BinaryOutputState copyWith(void Function(BinaryOutputState) updates) => super.copyWith((message) => updates(message as BinaryOutputState)) as BinaryOutputState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BinaryOutputState create() => BinaryOutputState._();
  BinaryOutputState createEmptyInstance() => create();
  static $pb.PbList<BinaryOutputState> createRepeated() => $pb.PbList<BinaryOutputState>();
  @$core.pragma('dart2js:noInline')
  static BinaryOutputState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BinaryOutputState>(create);
  static BinaryOutputState? _defaultInstance;

  @$pb.TagNumber(1)
  $core.bool get activeActual => $_getBF(0);
  @$pb.TagNumber(1)
  set activeActual($core.bool v) { $_setBool(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasActiveActual() => $_has(0);
  @$pb.TagNumber(1)
  void clearActiveActual() => clearField(1);

  @$pb.TagNumber(2)
  $core.bool get activeRequested => $_getBF(1);
  @$pb.TagNumber(2)
  set activeRequested($core.bool v) { $_setBool(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasActiveRequested() => $_has(1);
  @$pb.TagNumber(2)
  void clearActiveRequested() => clearField(2);
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
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'active')
    ..hasRequiredFields = false
  ;

  SensorState._() : super();
  factory SensorState({
    $1.Sensor? model,
    $core.bool? active,
  }) {
    final _result = create();
    if (model != null) {
      _result.model = model;
    }
    if (active != null) {
      _result.active = active;
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

  @$pb.TagNumber(2)
  $core.bool get active => $_getBF(1);
  @$pb.TagNumber(2)
  set active($core.bool v) { $_setBool(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasActive() => $_has(1);
  @$pb.TagNumber(2)
  void clearActive() => clearField(2);
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

