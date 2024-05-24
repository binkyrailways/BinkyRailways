///
//  Generated code. Do not modify.
//  source: br_state_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

import 'dart:core' as $core;

import 'package:fixnum/fixnum.dart' as $fixnum;
import 'package:protobuf/protobuf.dart' as $pb;

import 'br_model_types.pb.dart' as $0;

import 'br_state_types.pbenum.dart';
import 'br_model_types.pbenum.dart' as $0;

export 'br_state_types.pbenum.dart';

class RailwayState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'RailwayState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isRunModeEnabled')
    ..aOB(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isVirtualModeEnabled')
    ..aOB(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isVirtualAutorunEnabled')
    ..aOB(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'powerActual')
    ..aOB(11, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'powerRequested')
    ..aOB(12, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'automaticControlActual')
    ..aOB(13, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'automaticControlRequested')
    ..pc<$0.BlockRef>(50, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'blocks', $pb.PbFieldType.PM, subBuilder: $0.BlockRef.create)
    ..pc<$0.BlockGroupRef>(51, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'blockGroups', $pb.PbFieldType.PM, subBuilder: $0.BlockGroupRef.create)
    ..pc<$0.CommandStationRef>(52, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'commandStations', $pb.PbFieldType.PM, subBuilder: $0.CommandStationRef.create)
    ..pc<$0.JunctionRef>(53, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'junctions', $pb.PbFieldType.PM, subBuilder: $0.JunctionRef.create)
    ..pc<$0.LocRef>(54, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'locs', $pb.PbFieldType.PM, subBuilder: $0.LocRef.create)
    ..pc<$0.OutputRef>(55, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'outputs', $pb.PbFieldType.PM, subBuilder: $0.OutputRef.create)
    ..pc<$0.RouteRef>(56, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'routes', $pb.PbFieldType.PM, subBuilder: $0.RouteRef.create)
    ..pc<$0.SensorRef>(57, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'sensors', $pb.PbFieldType.PM, subBuilder: $0.SensorRef.create)
    ..pc<$0.SignalRef>(58, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'signals', $pb.PbFieldType.PM, subBuilder: $0.SignalRef.create)
    ..hasRequiredFields = false
  ;

  RailwayState._() : super();
  factory RailwayState({
    $core.bool? isRunModeEnabled,
    $core.bool? isVirtualModeEnabled,
    $core.bool? isVirtualAutorunEnabled,
    $core.bool? powerActual,
    $core.bool? powerRequested,
    $core.bool? automaticControlActual,
    $core.bool? automaticControlRequested,
    $core.Iterable<$0.BlockRef>? blocks,
    $core.Iterable<$0.BlockGroupRef>? blockGroups,
    $core.Iterable<$0.CommandStationRef>? commandStations,
    $core.Iterable<$0.JunctionRef>? junctions,
    $core.Iterable<$0.LocRef>? locs,
    $core.Iterable<$0.OutputRef>? outputs,
    $core.Iterable<$0.RouteRef>? routes,
    $core.Iterable<$0.SensorRef>? sensors,
    $core.Iterable<$0.SignalRef>? signals,
  }) {
    final _result = create();
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

  @$pb.TagNumber(2)
  $core.bool get isRunModeEnabled => $_getBF(0);
  @$pb.TagNumber(2)
  set isRunModeEnabled($core.bool v) { $_setBool(0, v); }
  @$pb.TagNumber(2)
  $core.bool hasIsRunModeEnabled() => $_has(0);
  @$pb.TagNumber(2)
  void clearIsRunModeEnabled() => clearField(2);

  @$pb.TagNumber(3)
  $core.bool get isVirtualModeEnabled => $_getBF(1);
  @$pb.TagNumber(3)
  set isVirtualModeEnabled($core.bool v) { $_setBool(1, v); }
  @$pb.TagNumber(3)
  $core.bool hasIsVirtualModeEnabled() => $_has(1);
  @$pb.TagNumber(3)
  void clearIsVirtualModeEnabled() => clearField(3);

  @$pb.TagNumber(4)
  $core.bool get isVirtualAutorunEnabled => $_getBF(2);
  @$pb.TagNumber(4)
  set isVirtualAutorunEnabled($core.bool v) { $_setBool(2, v); }
  @$pb.TagNumber(4)
  $core.bool hasIsVirtualAutorunEnabled() => $_has(2);
  @$pb.TagNumber(4)
  void clearIsVirtualAutorunEnabled() => clearField(4);

  @$pb.TagNumber(10)
  $core.bool get powerActual => $_getBF(3);
  @$pb.TagNumber(10)
  set powerActual($core.bool v) { $_setBool(3, v); }
  @$pb.TagNumber(10)
  $core.bool hasPowerActual() => $_has(3);
  @$pb.TagNumber(10)
  void clearPowerActual() => clearField(10);

  @$pb.TagNumber(11)
  $core.bool get powerRequested => $_getBF(4);
  @$pb.TagNumber(11)
  set powerRequested($core.bool v) { $_setBool(4, v); }
  @$pb.TagNumber(11)
  $core.bool hasPowerRequested() => $_has(4);
  @$pb.TagNumber(11)
  void clearPowerRequested() => clearField(11);

  @$pb.TagNumber(12)
  $core.bool get automaticControlActual => $_getBF(5);
  @$pb.TagNumber(12)
  set automaticControlActual($core.bool v) { $_setBool(5, v); }
  @$pb.TagNumber(12)
  $core.bool hasAutomaticControlActual() => $_has(5);
  @$pb.TagNumber(12)
  void clearAutomaticControlActual() => clearField(12);

  @$pb.TagNumber(13)
  $core.bool get automaticControlRequested => $_getBF(6);
  @$pb.TagNumber(13)
  set automaticControlRequested($core.bool v) { $_setBool(6, v); }
  @$pb.TagNumber(13)
  $core.bool hasAutomaticControlRequested() => $_has(6);
  @$pb.TagNumber(13)
  void clearAutomaticControlRequested() => clearField(13);

  @$pb.TagNumber(50)
  $core.List<$0.BlockRef> get blocks => $_getList(7);

  @$pb.TagNumber(51)
  $core.List<$0.BlockGroupRef> get blockGroups => $_getList(8);

  @$pb.TagNumber(52)
  $core.List<$0.CommandStationRef> get commandStations => $_getList(9);

  @$pb.TagNumber(53)
  $core.List<$0.JunctionRef> get junctions => $_getList(10);

  @$pb.TagNumber(54)
  $core.List<$0.LocRef> get locs => $_getList(11);

  @$pb.TagNumber(55)
  $core.List<$0.OutputRef> get outputs => $_getList(12);

  @$pb.TagNumber(56)
  $core.List<$0.RouteRef> get routes => $_getList(13);

  @$pb.TagNumber(57)
  $core.List<$0.SensorRef> get sensors => $_getList(14);

  @$pb.TagNumber(58)
  $core.List<$0.SignalRef> get signals => $_getList(15);
}

class LocState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'LocState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$0.Loc>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $0.Loc.create)
    ..aOB(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'controlledAutomaticallyActual')
    ..aOB(11, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'controlledAutomaticallyRequested')
    ..aOB(12, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'canBeControlledAutomatically')
    ..e<AutoLocState>(13, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'automaticState', $pb.PbFieldType.OE, defaultOrMaker: AutoLocState.ASSIGNROUTE, valueOf: AutoLocState.valueOf, enumValues: AutoLocState.values)
    ..aOM<$0.RouteRef>(18, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'currentRoute', subBuilder: $0.RouteRef.create)
    ..aOB(20, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'waitAfterCurrentRoute')
    ..aOB(22, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isCurrentRouteDurationExceeded')
    ..aOM<$0.RouteRef>(23, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'nextRoute', subBuilder: $0.RouteRef.create)
    ..aOM<$0.BlockRef>(24, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'currentBlock', subBuilder: $0.BlockRef.create)
    ..e<$0.BlockSide>(25, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'currentBlockEnterSide', $pb.PbFieldType.OE, defaultOrMaker: $0.BlockSide.FRONT, valueOf: $0.BlockSide.valueOf, enumValues: $0.BlockSide.values)
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
    ..aOB(80, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isEnabled')
    ..hasRequiredFields = false
  ;

  LocState._() : super();
  factory LocState({
    $0.Loc? model,
    $core.bool? controlledAutomaticallyActual,
    $core.bool? controlledAutomaticallyRequested,
    $core.bool? canBeControlledAutomatically,
    AutoLocState? automaticState,
    $0.RouteRef? currentRoute,
    $core.bool? waitAfterCurrentRoute,
    $core.bool? isCurrentRouteDurationExceeded,
    $0.RouteRef? nextRoute,
    $0.BlockRef? currentBlock,
    $0.BlockSide? currentBlockEnterSide,
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
    $core.bool? isEnabled,
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
    if (currentBlockEnterSide != null) {
      _result.currentBlockEnterSide = currentBlockEnterSide;
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
    if (isEnabled != null) {
      _result.isEnabled = isEnabled;
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
  $0.Loc get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($0.Loc v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $0.Loc ensureModel() => $_ensure(0);

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
  $0.RouteRef get currentRoute => $_getN(5);
  @$pb.TagNumber(18)
  set currentRoute($0.RouteRef v) { setField(18, v); }
  @$pb.TagNumber(18)
  $core.bool hasCurrentRoute() => $_has(5);
  @$pb.TagNumber(18)
  void clearCurrentRoute() => clearField(18);
  @$pb.TagNumber(18)
  $0.RouteRef ensureCurrentRoute() => $_ensure(5);

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
  $0.RouteRef get nextRoute => $_getN(8);
  @$pb.TagNumber(23)
  set nextRoute($0.RouteRef v) { setField(23, v); }
  @$pb.TagNumber(23)
  $core.bool hasNextRoute() => $_has(8);
  @$pb.TagNumber(23)
  void clearNextRoute() => clearField(23);
  @$pb.TagNumber(23)
  $0.RouteRef ensureNextRoute() => $_ensure(8);

  @$pb.TagNumber(24)
  $0.BlockRef get currentBlock => $_getN(9);
  @$pb.TagNumber(24)
  set currentBlock($0.BlockRef v) { setField(24, v); }
  @$pb.TagNumber(24)
  $core.bool hasCurrentBlock() => $_has(9);
  @$pb.TagNumber(24)
  void clearCurrentBlock() => clearField(24);
  @$pb.TagNumber(24)
  $0.BlockRef ensureCurrentBlock() => $_ensure(9);

  @$pb.TagNumber(25)
  $0.BlockSide get currentBlockEnterSide => $_getN(10);
  @$pb.TagNumber(25)
  set currentBlockEnterSide($0.BlockSide v) { setField(25, v); }
  @$pb.TagNumber(25)
  $core.bool hasCurrentBlockEnterSide() => $_has(10);
  @$pb.TagNumber(25)
  void clearCurrentBlockEnterSide() => clearField(25);

  @$pb.TagNumber(50)
  $core.int get speedActual => $_getIZ(11);
  @$pb.TagNumber(50)
  set speedActual($core.int v) { $_setSignedInt32(11, v); }
  @$pb.TagNumber(50)
  $core.bool hasSpeedActual() => $_has(11);
  @$pb.TagNumber(50)
  void clearSpeedActual() => clearField(50);

  @$pb.TagNumber(51)
  $core.int get speedRequested => $_getIZ(12);
  @$pb.TagNumber(51)
  set speedRequested($core.int v) { $_setSignedInt32(12, v); }
  @$pb.TagNumber(51)
  $core.bool hasSpeedRequested() => $_has(12);
  @$pb.TagNumber(51)
  void clearSpeedRequested() => clearField(51);

  @$pb.TagNumber(52)
  $core.String get speedText => $_getSZ(13);
  @$pb.TagNumber(52)
  set speedText($core.String v) { $_setString(13, v); }
  @$pb.TagNumber(52)
  $core.bool hasSpeedText() => $_has(13);
  @$pb.TagNumber(52)
  void clearSpeedText() => clearField(52);

  @$pb.TagNumber(53)
  $core.String get stateText => $_getSZ(14);
  @$pb.TagNumber(53)
  set stateText($core.String v) { $_setString(14, v); }
  @$pb.TagNumber(53)
  $core.bool hasStateText() => $_has(14);
  @$pb.TagNumber(53)
  void clearStateText() => clearField(53);

  @$pb.TagNumber(54)
  $core.int get speedInStepsActual => $_getIZ(15);
  @$pb.TagNumber(54)
  set speedInStepsActual($core.int v) { $_setSignedInt32(15, v); }
  @$pb.TagNumber(54)
  $core.bool hasSpeedInStepsActual() => $_has(15);
  @$pb.TagNumber(54)
  void clearSpeedInStepsActual() => clearField(54);

  @$pb.TagNumber(55)
  $core.int get speedInStepsRequested => $_getIZ(16);
  @$pb.TagNumber(55)
  set speedInStepsRequested($core.int v) { $_setSignedInt32(16, v); }
  @$pb.TagNumber(55)
  $core.bool hasSpeedInStepsRequested() => $_has(16);
  @$pb.TagNumber(55)
  void clearSpeedInStepsRequested() => clearField(55);

  @$pb.TagNumber(56)
  LocDirection get directionActual => $_getN(17);
  @$pb.TagNumber(56)
  set directionActual(LocDirection v) { setField(56, v); }
  @$pb.TagNumber(56)
  $core.bool hasDirectionActual() => $_has(17);
  @$pb.TagNumber(56)
  void clearDirectionActual() => clearField(56);

  @$pb.TagNumber(57)
  LocDirection get directionRequested => $_getN(18);
  @$pb.TagNumber(57)
  set directionRequested(LocDirection v) { setField(57, v); }
  @$pb.TagNumber(57)
  $core.bool hasDirectionRequested() => $_has(18);
  @$pb.TagNumber(57)
  void clearDirectionRequested() => clearField(57);

  @$pb.TagNumber(60)
  $core.bool get isReversing => $_getBF(19);
  @$pb.TagNumber(60)
  set isReversing($core.bool v) { $_setBool(19, v); }
  @$pb.TagNumber(60)
  $core.bool hasIsReversing() => $_has(19);
  @$pb.TagNumber(60)
  void clearIsReversing() => clearField(60);

  @$pb.TagNumber(70)
  $core.bool get f0Actual => $_getBF(20);
  @$pb.TagNumber(70)
  set f0Actual($core.bool v) { $_setBool(20, v); }
  @$pb.TagNumber(70)
  $core.bool hasF0Actual() => $_has(20);
  @$pb.TagNumber(70)
  void clearF0Actual() => clearField(70);

  @$pb.TagNumber(71)
  $core.bool get f0Requested => $_getBF(21);
  @$pb.TagNumber(71)
  set f0Requested($core.bool v) { $_setBool(21, v); }
  @$pb.TagNumber(71)
  $core.bool hasF0Requested() => $_has(21);
  @$pb.TagNumber(71)
  void clearF0Requested() => clearField(71);

  @$pb.TagNumber(80)
  $core.bool get isEnabled => $_getBF(22);
  @$pb.TagNumber(80)
  set isEnabled($core.bool v) { $_setBool(22, v); }
  @$pb.TagNumber(80)
  $core.bool hasIsEnabled() => $_has(22);
  @$pb.TagNumber(80)
  void clearIsEnabled() => clearField(80);
}

class CommandStationState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'CommandStationState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$0.CommandStation>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $0.CommandStation.create)
    ..pc<HardwareModule>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'hardwareModules', $pb.PbFieldType.PM, subBuilder: HardwareModule.create)
    ..hasRequiredFields = false
  ;

  CommandStationState._() : super();
  factory CommandStationState({
    $0.CommandStation? model,
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
  $0.CommandStation get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($0.CommandStation v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $0.CommandStation ensureModel() => $_ensure(0);

  @$pb.TagNumber(2)
  $core.List<HardwareModule> get hardwareModules => $_getList(1);
}

class HardwareModule extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'HardwareModule', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aInt64(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'uptime')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'lastUpdatedAt')
    ..pPS(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'errorMessages')
    ..aOS(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'address')
    ..aInt64(6, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'secondsSinceLastUpdated')
    ..aOS(7, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'metricsUrl')
    ..aOS(8, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'dccGeneratorUrl')
    ..aOS(9, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'sshUrl')
    ..hasRequiredFields = false
  ;

  HardwareModule._() : super();
  factory HardwareModule({
    $core.String? id,
    $fixnum.Int64? uptime,
    $core.String? lastUpdatedAt,
    $core.Iterable<$core.String>? errorMessages,
    $core.String? address,
    $fixnum.Int64? secondsSinceLastUpdated,
    $core.String? metricsUrl,
    $core.String? dccGeneratorUrl,
    $core.String? sshUrl,
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
    if (errorMessages != null) {
      _result.errorMessages.addAll(errorMessages);
    }
    if (address != null) {
      _result.address = address;
    }
    if (secondsSinceLastUpdated != null) {
      _result.secondsSinceLastUpdated = secondsSinceLastUpdated;
    }
    if (metricsUrl != null) {
      _result.metricsUrl = metricsUrl;
    }
    if (dccGeneratorUrl != null) {
      _result.dccGeneratorUrl = dccGeneratorUrl;
    }
    if (sshUrl != null) {
      _result.sshUrl = sshUrl;
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

  @$pb.TagNumber(4)
  $core.List<$core.String> get errorMessages => $_getList(3);

  @$pb.TagNumber(5)
  $core.String get address => $_getSZ(4);
  @$pb.TagNumber(5)
  set address($core.String v) { $_setString(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasAddress() => $_has(4);
  @$pb.TagNumber(5)
  void clearAddress() => clearField(5);

  @$pb.TagNumber(6)
  $fixnum.Int64 get secondsSinceLastUpdated => $_getI64(5);
  @$pb.TagNumber(6)
  set secondsSinceLastUpdated($fixnum.Int64 v) { $_setInt64(5, v); }
  @$pb.TagNumber(6)
  $core.bool hasSecondsSinceLastUpdated() => $_has(5);
  @$pb.TagNumber(6)
  void clearSecondsSinceLastUpdated() => clearField(6);

  @$pb.TagNumber(7)
  $core.String get metricsUrl => $_getSZ(6);
  @$pb.TagNumber(7)
  set metricsUrl($core.String v) { $_setString(6, v); }
  @$pb.TagNumber(7)
  $core.bool hasMetricsUrl() => $_has(6);
  @$pb.TagNumber(7)
  void clearMetricsUrl() => clearField(7);

  @$pb.TagNumber(8)
  $core.String get dccGeneratorUrl => $_getSZ(7);
  @$pb.TagNumber(8)
  set dccGeneratorUrl($core.String v) { $_setString(7, v); }
  @$pb.TagNumber(8)
  $core.bool hasDccGeneratorUrl() => $_has(7);
  @$pb.TagNumber(8)
  void clearDccGeneratorUrl() => clearField(8);

  @$pb.TagNumber(9)
  $core.String get sshUrl => $_getSZ(8);
  @$pb.TagNumber(9)
  set sshUrl($core.String v) { $_setString(8, v); }
  @$pb.TagNumber(9)
  $core.bool hasSshUrl() => $_has(8);
  @$pb.TagNumber(9)
  void clearSshUrl() => clearField(9);
}

class BlockState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BlockState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$0.Block>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $0.Block.create)
    ..aOM<$0.LocRef>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'lockedBy', subBuilder: $0.LocRef.create)
    ..e<BlockStateState>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'state', $pb.PbFieldType.OE, defaultOrMaker: BlockStateState.FREE, valueOf: BlockStateState.valueOf, enumValues: BlockStateState.values)
    ..aOB(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'closedActual')
    ..aOB(11, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'closedRequested')
    ..aOB(20, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isDeadend')
    ..aOB(21, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isStation')
    ..aOB(22, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'hasWaitingLoc')
    ..hasRequiredFields = false
  ;

  BlockState._() : super();
  factory BlockState({
    $0.Block? model,
    $0.LocRef? lockedBy,
    BlockStateState? state,
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
    if (state != null) {
      _result.state = state;
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
  $0.Block get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($0.Block v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $0.Block ensureModel() => $_ensure(0);

  @$pb.TagNumber(2)
  $0.LocRef get lockedBy => $_getN(1);
  @$pb.TagNumber(2)
  set lockedBy($0.LocRef v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasLockedBy() => $_has(1);
  @$pb.TagNumber(2)
  void clearLockedBy() => clearField(2);
  @$pb.TagNumber(2)
  $0.LocRef ensureLockedBy() => $_ensure(1);

  @$pb.TagNumber(3)
  BlockStateState get state => $_getN(2);
  @$pb.TagNumber(3)
  set state(BlockStateState v) { setField(3, v); }
  @$pb.TagNumber(3)
  $core.bool hasState() => $_has(2);
  @$pb.TagNumber(3)
  void clearState() => clearField(3);

  @$pb.TagNumber(10)
  $core.bool get closedActual => $_getBF(3);
  @$pb.TagNumber(10)
  set closedActual($core.bool v) { $_setBool(3, v); }
  @$pb.TagNumber(10)
  $core.bool hasClosedActual() => $_has(3);
  @$pb.TagNumber(10)
  void clearClosedActual() => clearField(10);

  @$pb.TagNumber(11)
  $core.bool get closedRequested => $_getBF(4);
  @$pb.TagNumber(11)
  set closedRequested($core.bool v) { $_setBool(4, v); }
  @$pb.TagNumber(11)
  $core.bool hasClosedRequested() => $_has(4);
  @$pb.TagNumber(11)
  void clearClosedRequested() => clearField(11);

  @$pb.TagNumber(20)
  $core.bool get isDeadend => $_getBF(5);
  @$pb.TagNumber(20)
  set isDeadend($core.bool v) { $_setBool(5, v); }
  @$pb.TagNumber(20)
  $core.bool hasIsDeadend() => $_has(5);
  @$pb.TagNumber(20)
  void clearIsDeadend() => clearField(20);

  @$pb.TagNumber(21)
  $core.bool get isStation => $_getBF(6);
  @$pb.TagNumber(21)
  set isStation($core.bool v) { $_setBool(6, v); }
  @$pb.TagNumber(21)
  $core.bool hasIsStation() => $_has(6);
  @$pb.TagNumber(21)
  void clearIsStation() => clearField(21);

  @$pb.TagNumber(22)
  $core.bool get hasWaitingLoc => $_getBF(7);
  @$pb.TagNumber(22)
  set hasWaitingLoc($core.bool v) { $_setBool(7, v); }
  @$pb.TagNumber(22)
  $core.bool hasHasWaitingLoc() => $_has(7);
  @$pb.TagNumber(22)
  void clearHasWaitingLoc() => clearField(22);
}

class BlockGroupState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BlockGroupState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$0.BlockGroup>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $0.BlockGroup.create)
    ..hasRequiredFields = false
  ;

  BlockGroupState._() : super();
  factory BlockGroupState({
    $0.BlockGroup? model,
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
  $0.BlockGroup get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($0.BlockGroup v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $0.BlockGroup ensureModel() => $_ensure(0);
}

class JunctionState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'JunctionState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$0.Junction>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $0.Junction.create)
    ..aOM<SwitchState>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'switch', subBuilder: SwitchState.create)
    ..hasRequiredFields = false
  ;

  JunctionState._() : super();
  factory JunctionState({
    $0.Junction? model,
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
  $0.Junction get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($0.Junction v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $0.Junction ensureModel() => $_ensure(0);

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
    ..e<$0.SwitchDirection>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'directionActual', $pb.PbFieldType.OE, defaultOrMaker: $0.SwitchDirection.STRAIGHT, valueOf: $0.SwitchDirection.valueOf, enumValues: $0.SwitchDirection.values)
    ..e<$0.SwitchDirection>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'directionRequested', $pb.PbFieldType.OE, defaultOrMaker: $0.SwitchDirection.STRAIGHT, valueOf: $0.SwitchDirection.valueOf, enumValues: $0.SwitchDirection.values)
    ..hasRequiredFields = false
  ;

  SwitchState._() : super();
  factory SwitchState({
    $0.SwitchDirection? directionActual,
    $0.SwitchDirection? directionRequested,
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
  $0.SwitchDirection get directionActual => $_getN(0);
  @$pb.TagNumber(1)
  set directionActual($0.SwitchDirection v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasDirectionActual() => $_has(0);
  @$pb.TagNumber(1)
  void clearDirectionActual() => clearField(1);

  @$pb.TagNumber(2)
  $0.SwitchDirection get directionRequested => $_getN(1);
  @$pb.TagNumber(2)
  set directionRequested($0.SwitchDirection v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasDirectionRequested() => $_has(1);
  @$pb.TagNumber(2)
  void clearDirectionRequested() => clearField(2);
}

class OutputState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'OutputState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$0.Output>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $0.Output.create)
    ..aOM<BinaryOutputState>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'binaryOutput', subBuilder: BinaryOutputState.create)
    ..hasRequiredFields = false
  ;

  OutputState._() : super();
  factory OutputState({
    $0.Output? model,
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
  $0.Output get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($0.Output v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $0.Output ensureModel() => $_ensure(0);

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
    ..aOM<$0.Route>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $0.Route.create)
    ..hasRequiredFields = false
  ;

  RouteState._() : super();
  factory RouteState({
    $0.Route? model,
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
  $0.Route get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($0.Route v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $0.Route ensureModel() => $_ensure(0);
}

class SensorState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SensorState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<$0.Sensor>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $0.Sensor.create)
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'active')
    ..hasRequiredFields = false
  ;

  SensorState._() : super();
  factory SensorState({
    $0.Sensor? model,
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
  $0.Sensor get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($0.Sensor v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $0.Sensor ensureModel() => $_ensure(0);

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
    ..aOM<$0.Signal>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'model', subBuilder: $0.Signal.create)
    ..hasRequiredFields = false
  ;

  SignalState._() : super();
  factory SignalState({
    $0.Signal? model,
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
  $0.Signal get model => $_getN(0);
  @$pb.TagNumber(1)
  set model($0.Signal v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasModel() => $_has(0);
  @$pb.TagNumber(1)
  void clearModel() => clearField(1);
  @$pb.TagNumber(1)
  $0.Signal ensureModel() => $_ensure(0);
}

