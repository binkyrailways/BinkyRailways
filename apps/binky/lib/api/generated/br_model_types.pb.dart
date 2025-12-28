///
//  Generated code. Do not modify.
//  source: br_model_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,constant_identifier_names,directives_ordering,library_prefixes,non_constant_identifier_names,prefer_final_fields,return_of_invalid_type,unnecessary_const,unnecessary_import,unnecessary_this,unused_import,unused_shown_name

import 'dart:core' as $core;

import 'package:protobuf/protobuf.dart' as $pb;

import 'br_model_types.pbenum.dart';

export 'br_model_types.pbenum.dart';

class Empty extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Empty', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..hasRequiredFields = false
  ;

  Empty._() : super();
  factory Empty() => create();
  factory Empty.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Empty.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Empty clone() => Empty()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Empty copyWith(void Function(Empty) updates) => super.copyWith((message) => updates(message as Empty)) as Empty; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Empty create() => Empty._();
  Empty createEmptyInstance() => create();
  static $pb.PbList<Empty> createRepeated() => $pb.PbList<Empty>();
  @$core.pragma('dart2js:noInline')
  static Empty getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Empty>(create);
  static Empty? _defaultInstance;
}

class Image extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Image', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'contentBase64')
    ..hasRequiredFields = false
  ;

  Image._() : super();
  factory Image({
    $core.String? contentBase64,
  }) {
    final _result = create();
    if (contentBase64 != null) {
      _result.contentBase64 = contentBase64;
    }
    return _result;
  }
  factory Image.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Image.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Image clone() => Image()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Image copyWith(void Function(Image) updates) => super.copyWith((message) => updates(message as Image)) as Image; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Image create() => Image._();
  Image createEmptyInstance() => create();
  static $pb.PbList<Image> createRepeated() => $pb.PbList<Image>();
  @$core.pragma('dart2js:noInline')
  static Image getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Image>(create);
  static Image? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get contentBase64 => $_getSZ(0);
  @$pb.TagNumber(1)
  set contentBase64($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasContentBase64() => $_has(0);
  @$pb.TagNumber(1)
  void clearContentBase64() => clearField(1);
}

class Railway extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Railway', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..aOB(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'dirty')
    ..pc<ModuleRef>(100, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'modules', $pb.PbFieldType.PM, subBuilder: ModuleRef.create)
    ..pc<LocRef>(101, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'locs', $pb.PbFieldType.PM, subBuilder: LocRef.create)
    ..pc<LocGroupRef>(102, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'locGroups', $pb.PbFieldType.PM, protoName: 'locGroups', subBuilder: LocGroupRef.create)
    ..pc<CommandStationRef>(103, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'commandStations', $pb.PbFieldType.PM, protoName: 'commandStations', subBuilder: CommandStationRef.create)
    ..hasRequiredFields = false
  ;

  Railway._() : super();
  factory Railway({
    $core.String? id,
    $core.String? description,
    $core.bool? dirty,
    $core.Iterable<ModuleRef>? modules,
    $core.Iterable<LocRef>? locs,
    $core.Iterable<LocGroupRef>? locGroups,
    $core.Iterable<CommandStationRef>? commandStations,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (dirty != null) {
      _result.dirty = dirty;
    }
    if (modules != null) {
      _result.modules.addAll(modules);
    }
    if (locs != null) {
      _result.locs.addAll(locs);
    }
    if (locGroups != null) {
      _result.locGroups.addAll(locGroups);
    }
    if (commandStations != null) {
      _result.commandStations.addAll(commandStations);
    }
    return _result;
  }
  factory Railway.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Railway.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Railway clone() => Railway()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Railway copyWith(void Function(Railway) updates) => super.copyWith((message) => updates(message as Railway)) as Railway; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Railway create() => Railway._();
  Railway createEmptyInstance() => create();
  static $pb.PbList<Railway> createRepeated() => $pb.PbList<Railway>();
  @$core.pragma('dart2js:noInline')
  static Railway getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Railway>(create);
  static Railway? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.bool get dirty => $_getBF(2);
  @$pb.TagNumber(3)
  set dirty($core.bool v) { $_setBool(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasDirty() => $_has(2);
  @$pb.TagNumber(3)
  void clearDirty() => clearField(3);

  @$pb.TagNumber(100)
  $core.List<ModuleRef> get modules => $_getList(3);

  @$pb.TagNumber(101)
  $core.List<LocRef> get locs => $_getList(4);

  @$pb.TagNumber(102)
  $core.List<LocGroupRef> get locGroups => $_getList(5);

  @$pb.TagNumber(103)
  $core.List<CommandStationRef> get commandStations => $_getList(6);
}

class Module extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Module', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..a<$core.int>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'width', $pb.PbFieldType.O3)
    ..a<$core.int>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'height', $pb.PbFieldType.O3)
    ..aOB(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'hasBackgroundImage')
    ..aOS(6, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'backgroundImageUrl')
    ..pc<BlockRef>(100, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'blocks', $pb.PbFieldType.PM, subBuilder: BlockRef.create)
    ..pc<BlockGroupRef>(101, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'blockGroups', $pb.PbFieldType.PM, protoName: 'blockGroups', subBuilder: BlockGroupRef.create)
    ..pc<EdgeRef>(102, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'edges', $pb.PbFieldType.PM, subBuilder: EdgeRef.create)
    ..pc<JunctionRef>(103, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'junctions', $pb.PbFieldType.PM, subBuilder: JunctionRef.create)
    ..pc<OutputRef>(104, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'outputs', $pb.PbFieldType.PM, subBuilder: OutputRef.create)
    ..pc<RouteRef>(105, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'routes', $pb.PbFieldType.PM, subBuilder: RouteRef.create)
    ..pc<SensorRef>(106, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'sensors', $pb.PbFieldType.PM, subBuilder: SensorRef.create)
    ..pc<SignalRef>(107, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'signals', $pb.PbFieldType.PM, subBuilder: SignalRef.create)
    ..pPS(200, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'layers')
    ..hasRequiredFields = false
  ;

  Module._() : super();
  factory Module({
    $core.String? id,
    $core.String? description,
    $core.int? width,
    $core.int? height,
    $core.bool? hasBackgroundImage,
    $core.String? backgroundImageUrl,
    $core.Iterable<BlockRef>? blocks,
    $core.Iterable<BlockGroupRef>? blockGroups,
    $core.Iterable<EdgeRef>? edges,
    $core.Iterable<JunctionRef>? junctions,
    $core.Iterable<OutputRef>? outputs,
    $core.Iterable<RouteRef>? routes,
    $core.Iterable<SensorRef>? sensors,
    $core.Iterable<SignalRef>? signals,
    $core.Iterable<$core.String>? layers,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (width != null) {
      _result.width = width;
    }
    if (height != null) {
      _result.height = height;
    }
    if (hasBackgroundImage != null) {
      _result.hasBackgroundImage = hasBackgroundImage;
    }
    if (backgroundImageUrl != null) {
      _result.backgroundImageUrl = backgroundImageUrl;
    }
    if (blocks != null) {
      _result.blocks.addAll(blocks);
    }
    if (blockGroups != null) {
      _result.blockGroups.addAll(blockGroups);
    }
    if (edges != null) {
      _result.edges.addAll(edges);
    }
    if (junctions != null) {
      _result.junctions.addAll(junctions);
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
    if (layers != null) {
      _result.layers.addAll(layers);
    }
    return _result;
  }
  factory Module.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Module.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Module clone() => Module()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Module copyWith(void Function(Module) updates) => super.copyWith((message) => updates(message as Module)) as Module; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Module create() => Module._();
  Module createEmptyInstance() => create();
  static $pb.PbList<Module> createRepeated() => $pb.PbList<Module>();
  @$core.pragma('dart2js:noInline')
  static Module getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Module>(create);
  static Module? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.int get width => $_getIZ(2);
  @$pb.TagNumber(3)
  set width($core.int v) { $_setSignedInt32(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasWidth() => $_has(2);
  @$pb.TagNumber(3)
  void clearWidth() => clearField(3);

  @$pb.TagNumber(4)
  $core.int get height => $_getIZ(3);
  @$pb.TagNumber(4)
  set height($core.int v) { $_setSignedInt32(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasHeight() => $_has(3);
  @$pb.TagNumber(4)
  void clearHeight() => clearField(4);

  @$pb.TagNumber(5)
  $core.bool get hasBackgroundImage => $_getBF(4);
  @$pb.TagNumber(5)
  set hasBackgroundImage($core.bool v) { $_setBool(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasHasBackgroundImage() => $_has(4);
  @$pb.TagNumber(5)
  void clearHasBackgroundImage() => clearField(5);

  @$pb.TagNumber(6)
  $core.String get backgroundImageUrl => $_getSZ(5);
  @$pb.TagNumber(6)
  set backgroundImageUrl($core.String v) { $_setString(5, v); }
  @$pb.TagNumber(6)
  $core.bool hasBackgroundImageUrl() => $_has(5);
  @$pb.TagNumber(6)
  void clearBackgroundImageUrl() => clearField(6);

  @$pb.TagNumber(100)
  $core.List<BlockRef> get blocks => $_getList(6);

  @$pb.TagNumber(101)
  $core.List<BlockGroupRef> get blockGroups => $_getList(7);

  @$pb.TagNumber(102)
  $core.List<EdgeRef> get edges => $_getList(8);

  @$pb.TagNumber(103)
  $core.List<JunctionRef> get junctions => $_getList(9);

  @$pb.TagNumber(104)
  $core.List<OutputRef> get outputs => $_getList(10);

  @$pb.TagNumber(105)
  $core.List<RouteRef> get routes => $_getList(11);

  @$pb.TagNumber(106)
  $core.List<SensorRef> get sensors => $_getList(12);

  @$pb.TagNumber(107)
  $core.List<SignalRef> get signals => $_getList(13);

  @$pb.TagNumber(200)
  $core.List<$core.String> get layers => $_getList(14);
}

class ModuleRef extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'ModuleRef', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOM<Position>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'position', subBuilder: Position.create)
    ..a<$core.int>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'zoomFactor', $pb.PbFieldType.O3)
    ..aOB(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'locked')
    ..hasRequiredFields = false
  ;

  ModuleRef._() : super();
  factory ModuleRef({
    $core.String? id,
    Position? position,
    $core.int? zoomFactor,
    $core.bool? locked,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (position != null) {
      _result.position = position;
    }
    if (zoomFactor != null) {
      _result.zoomFactor = zoomFactor;
    }
    if (locked != null) {
      _result.locked = locked;
    }
    return _result;
  }
  factory ModuleRef.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory ModuleRef.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  ModuleRef clone() => ModuleRef()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  ModuleRef copyWith(void Function(ModuleRef) updates) => super.copyWith((message) => updates(message as ModuleRef)) as ModuleRef; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static ModuleRef create() => ModuleRef._();
  ModuleRef createEmptyInstance() => create();
  static $pb.PbList<ModuleRef> createRepeated() => $pb.PbList<ModuleRef>();
  @$core.pragma('dart2js:noInline')
  static ModuleRef getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<ModuleRef>(create);
  static ModuleRef? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  Position get position => $_getN(1);
  @$pb.TagNumber(2)
  set position(Position v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasPosition() => $_has(1);
  @$pb.TagNumber(2)
  void clearPosition() => clearField(2);
  @$pb.TagNumber(2)
  Position ensurePosition() => $_ensure(1);

  @$pb.TagNumber(3)
  $core.int get zoomFactor => $_getIZ(2);
  @$pb.TagNumber(3)
  set zoomFactor($core.int v) { $_setSignedInt32(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasZoomFactor() => $_has(2);
  @$pb.TagNumber(3)
  void clearZoomFactor() => clearField(3);

  @$pb.TagNumber(4)
  $core.bool get locked => $_getBF(3);
  @$pb.TagNumber(4)
  set locked($core.bool v) { $_setBool(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasLocked() => $_has(3);
  @$pb.TagNumber(4)
  void clearLocked() => clearField(4);
}

class Position extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Position', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..a<$core.int>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'x', $pb.PbFieldType.O3)
    ..a<$core.int>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'y', $pb.PbFieldType.O3)
    ..a<$core.int>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'width', $pb.PbFieldType.O3)
    ..a<$core.int>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'height', $pb.PbFieldType.O3)
    ..a<$core.int>(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'rotation', $pb.PbFieldType.O3)
    ..aOS(6, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'layer')
    ..hasRequiredFields = false
  ;

  Position._() : super();
  factory Position({
    $core.int? x,
    $core.int? y,
    $core.int? width,
    $core.int? height,
    $core.int? rotation,
    $core.String? layer,
  }) {
    final _result = create();
    if (x != null) {
      _result.x = x;
    }
    if (y != null) {
      _result.y = y;
    }
    if (width != null) {
      _result.width = width;
    }
    if (height != null) {
      _result.height = height;
    }
    if (rotation != null) {
      _result.rotation = rotation;
    }
    if (layer != null) {
      _result.layer = layer;
    }
    return _result;
  }
  factory Position.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Position.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Position clone() => Position()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Position copyWith(void Function(Position) updates) => super.copyWith((message) => updates(message as Position)) as Position; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Position create() => Position._();
  Position createEmptyInstance() => create();
  static $pb.PbList<Position> createRepeated() => $pb.PbList<Position>();
  @$core.pragma('dart2js:noInline')
  static Position getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Position>(create);
  static Position? _defaultInstance;

  @$pb.TagNumber(1)
  $core.int get x => $_getIZ(0);
  @$pb.TagNumber(1)
  set x($core.int v) { $_setSignedInt32(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasX() => $_has(0);
  @$pb.TagNumber(1)
  void clearX() => clearField(1);

  @$pb.TagNumber(2)
  $core.int get y => $_getIZ(1);
  @$pb.TagNumber(2)
  set y($core.int v) { $_setSignedInt32(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasY() => $_has(1);
  @$pb.TagNumber(2)
  void clearY() => clearField(2);

  @$pb.TagNumber(3)
  $core.int get width => $_getIZ(2);
  @$pb.TagNumber(3)
  set width($core.int v) { $_setSignedInt32(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasWidth() => $_has(2);
  @$pb.TagNumber(3)
  void clearWidth() => clearField(3);

  @$pb.TagNumber(4)
  $core.int get height => $_getIZ(3);
  @$pb.TagNumber(4)
  set height($core.int v) { $_setSignedInt32(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasHeight() => $_has(3);
  @$pb.TagNumber(4)
  void clearHeight() => clearField(4);

  @$pb.TagNumber(5)
  $core.int get rotation => $_getIZ(4);
  @$pb.TagNumber(5)
  set rotation($core.int v) { $_setSignedInt32(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasRotation() => $_has(4);
  @$pb.TagNumber(5)
  void clearRotation() => clearField(5);

  @$pb.TagNumber(6)
  $core.String get layer => $_getSZ(5);
  @$pb.TagNumber(6)
  set layer($core.String v) { $_setString(5, v); }
  @$pb.TagNumber(6)
  $core.bool hasLayer() => $_has(5);
  @$pb.TagNumber(6)
  void clearLayer() => clearField(6);
}

class Loc extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Loc', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'owner')
    ..aOS(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'remarks')
    ..aOS(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'address')
    ..aOS(6, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'imageUrl')
    ..a<$core.int>(100, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'slowSpeed', $pb.PbFieldType.O3)
    ..a<$core.int>(101, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'mediumSpeed', $pb.PbFieldType.O3)
    ..a<$core.int>(102, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'maximumSpeed', $pb.PbFieldType.O3)
    ..a<$core.int>(110, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'speedSteps', $pb.PbFieldType.O3)
    ..e<ChangeDirection>(120, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'changeDirection', $pb.PbFieldType.OE, defaultOrMaker: ChangeDirection.ALLOW, valueOf: ChangeDirection.valueOf, enumValues: ChangeDirection.values)
    ..e<VehicleType>(121, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'vehicleType', $pb.PbFieldType.OE, defaultOrMaker: VehicleType.LOC, valueOf: VehicleType.valueOf, enumValues: VehicleType.values)
    ..hasRequiredFields = false
  ;

  Loc._() : super();
  factory Loc({
    $core.String? id,
    $core.String? description,
    $core.String? owner,
    $core.String? remarks,
    $core.String? address,
    $core.String? imageUrl,
    $core.int? slowSpeed,
    $core.int? mediumSpeed,
    $core.int? maximumSpeed,
    $core.int? speedSteps,
    ChangeDirection? changeDirection,
    VehicleType? vehicleType,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (owner != null) {
      _result.owner = owner;
    }
    if (remarks != null) {
      _result.remarks = remarks;
    }
    if (address != null) {
      _result.address = address;
    }
    if (imageUrl != null) {
      _result.imageUrl = imageUrl;
    }
    if (slowSpeed != null) {
      _result.slowSpeed = slowSpeed;
    }
    if (mediumSpeed != null) {
      _result.mediumSpeed = mediumSpeed;
    }
    if (maximumSpeed != null) {
      _result.maximumSpeed = maximumSpeed;
    }
    if (speedSteps != null) {
      _result.speedSteps = speedSteps;
    }
    if (changeDirection != null) {
      _result.changeDirection = changeDirection;
    }
    if (vehicleType != null) {
      _result.vehicleType = vehicleType;
    }
    return _result;
  }
  factory Loc.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Loc.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Loc clone() => Loc()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Loc copyWith(void Function(Loc) updates) => super.copyWith((message) => updates(message as Loc)) as Loc; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Loc create() => Loc._();
  Loc createEmptyInstance() => create();
  static $pb.PbList<Loc> createRepeated() => $pb.PbList<Loc>();
  @$core.pragma('dart2js:noInline')
  static Loc getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Loc>(create);
  static Loc? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get owner => $_getSZ(2);
  @$pb.TagNumber(3)
  set owner($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasOwner() => $_has(2);
  @$pb.TagNumber(3)
  void clearOwner() => clearField(3);

  @$pb.TagNumber(4)
  $core.String get remarks => $_getSZ(3);
  @$pb.TagNumber(4)
  set remarks($core.String v) { $_setString(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasRemarks() => $_has(3);
  @$pb.TagNumber(4)
  void clearRemarks() => clearField(4);

  @$pb.TagNumber(5)
  $core.String get address => $_getSZ(4);
  @$pb.TagNumber(5)
  set address($core.String v) { $_setString(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasAddress() => $_has(4);
  @$pb.TagNumber(5)
  void clearAddress() => clearField(5);

  @$pb.TagNumber(6)
  $core.String get imageUrl => $_getSZ(5);
  @$pb.TagNumber(6)
  set imageUrl($core.String v) { $_setString(5, v); }
  @$pb.TagNumber(6)
  $core.bool hasImageUrl() => $_has(5);
  @$pb.TagNumber(6)
  void clearImageUrl() => clearField(6);

  @$pb.TagNumber(100)
  $core.int get slowSpeed => $_getIZ(6);
  @$pb.TagNumber(100)
  set slowSpeed($core.int v) { $_setSignedInt32(6, v); }
  @$pb.TagNumber(100)
  $core.bool hasSlowSpeed() => $_has(6);
  @$pb.TagNumber(100)
  void clearSlowSpeed() => clearField(100);

  @$pb.TagNumber(101)
  $core.int get mediumSpeed => $_getIZ(7);
  @$pb.TagNumber(101)
  set mediumSpeed($core.int v) { $_setSignedInt32(7, v); }
  @$pb.TagNumber(101)
  $core.bool hasMediumSpeed() => $_has(7);
  @$pb.TagNumber(101)
  void clearMediumSpeed() => clearField(101);

  @$pb.TagNumber(102)
  $core.int get maximumSpeed => $_getIZ(8);
  @$pb.TagNumber(102)
  set maximumSpeed($core.int v) { $_setSignedInt32(8, v); }
  @$pb.TagNumber(102)
  $core.bool hasMaximumSpeed() => $_has(8);
  @$pb.TagNumber(102)
  void clearMaximumSpeed() => clearField(102);

  @$pb.TagNumber(110)
  $core.int get speedSteps => $_getIZ(9);
  @$pb.TagNumber(110)
  set speedSteps($core.int v) { $_setSignedInt32(9, v); }
  @$pb.TagNumber(110)
  $core.bool hasSpeedSteps() => $_has(9);
  @$pb.TagNumber(110)
  void clearSpeedSteps() => clearField(110);

  @$pb.TagNumber(120)
  ChangeDirection get changeDirection => $_getN(10);
  @$pb.TagNumber(120)
  set changeDirection(ChangeDirection v) { setField(120, v); }
  @$pb.TagNumber(120)
  $core.bool hasChangeDirection() => $_has(10);
  @$pb.TagNumber(120)
  void clearChangeDirection() => clearField(120);

  @$pb.TagNumber(121)
  VehicleType get vehicleType => $_getN(11);
  @$pb.TagNumber(121)
  set vehicleType(VehicleType v) { setField(121, v); }
  @$pb.TagNumber(121)
  $core.bool hasVehicleType() => $_has(11);
  @$pb.TagNumber(121)
  void clearVehicleType() => clearField(121);
}

class LocRef extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'LocRef', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  LocRef._() : super();
  factory LocRef({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory LocRef.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory LocRef.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  LocRef clone() => LocRef()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  LocRef copyWith(void Function(LocRef) updates) => super.copyWith((message) => updates(message as LocRef)) as LocRef; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static LocRef create() => LocRef._();
  LocRef createEmptyInstance() => create();
  static $pb.PbList<LocRef> createRepeated() => $pb.PbList<LocRef>();
  @$core.pragma('dart2js:noInline')
  static LocRef getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<LocRef>(create);
  static LocRef? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

class LocGroup extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'LocGroup', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..pc<LocRef>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'locs', $pb.PbFieldType.PM, subBuilder: LocRef.create)
    ..hasRequiredFields = false
  ;

  LocGroup._() : super();
  factory LocGroup({
    $core.String? id,
    $core.String? description,
    $core.Iterable<LocRef>? locs,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (locs != null) {
      _result.locs.addAll(locs);
    }
    return _result;
  }
  factory LocGroup.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory LocGroup.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  LocGroup clone() => LocGroup()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  LocGroup copyWith(void Function(LocGroup) updates) => super.copyWith((message) => updates(message as LocGroup)) as LocGroup; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static LocGroup create() => LocGroup._();
  LocGroup createEmptyInstance() => create();
  static $pb.PbList<LocGroup> createRepeated() => $pb.PbList<LocGroup>();
  @$core.pragma('dart2js:noInline')
  static LocGroup getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<LocGroup>(create);
  static LocGroup? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.List<LocRef> get locs => $_getList(2);
}

class LocGroupRef extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'LocGroupRef', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  LocGroupRef._() : super();
  factory LocGroupRef({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory LocGroupRef.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory LocGroupRef.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  LocGroupRef clone() => LocGroupRef()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  LocGroupRef copyWith(void Function(LocGroupRef) updates) => super.copyWith((message) => updates(message as LocGroupRef)) as LocGroupRef; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static LocGroupRef create() => LocGroupRef._();
  LocGroupRef createEmptyInstance() => create();
  static $pb.PbList<LocGroupRef> createRepeated() => $pb.PbList<LocGroupRef>();
  @$core.pragma('dart2js:noInline')
  static LocGroupRef getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<LocGroupRef>(create);
  static LocGroupRef? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

class CommandStation extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'CommandStation', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..pPS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'addressSpaces')
    ..aOM<BinkyNetCommandStation>(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'binkynetCommandStation', subBuilder: BinkyNetCommandStation.create)
    ..aOM<BidibCommandStation>(11, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'bidibCommandStation', subBuilder: BidibCommandStation.create)
    ..hasRequiredFields = false
  ;

  CommandStation._() : super();
  factory CommandStation({
    $core.String? id,
    $core.String? description,
    $core.Iterable<$core.String>? addressSpaces,
    BinkyNetCommandStation? binkynetCommandStation,
    BidibCommandStation? bidibCommandStation,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (addressSpaces != null) {
      _result.addressSpaces.addAll(addressSpaces);
    }
    if (binkynetCommandStation != null) {
      _result.binkynetCommandStation = binkynetCommandStation;
    }
    if (bidibCommandStation != null) {
      _result.bidibCommandStation = bidibCommandStation;
    }
    return _result;
  }
  factory CommandStation.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory CommandStation.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  CommandStation clone() => CommandStation()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  CommandStation copyWith(void Function(CommandStation) updates) => super.copyWith((message) => updates(message as CommandStation)) as CommandStation; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static CommandStation create() => CommandStation._();
  CommandStation createEmptyInstance() => create();
  static $pb.PbList<CommandStation> createRepeated() => $pb.PbList<CommandStation>();
  @$core.pragma('dart2js:noInline')
  static CommandStation getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<CommandStation>(create);
  static CommandStation? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.List<$core.String> get addressSpaces => $_getList(2);

  @$pb.TagNumber(10)
  BinkyNetCommandStation get binkynetCommandStation => $_getN(3);
  @$pb.TagNumber(10)
  set binkynetCommandStation(BinkyNetCommandStation v) { setField(10, v); }
  @$pb.TagNumber(10)
  $core.bool hasBinkynetCommandStation() => $_has(3);
  @$pb.TagNumber(10)
  void clearBinkynetCommandStation() => clearField(10);
  @$pb.TagNumber(10)
  BinkyNetCommandStation ensureBinkynetCommandStation() => $_ensure(3);

  @$pb.TagNumber(11)
  BidibCommandStation get bidibCommandStation => $_getN(4);
  @$pb.TagNumber(11)
  set bidibCommandStation(BidibCommandStation v) { setField(11, v); }
  @$pb.TagNumber(11)
  $core.bool hasBidibCommandStation() => $_has(4);
  @$pb.TagNumber(11)
  void clearBidibCommandStation() => clearField(11);
  @$pb.TagNumber(11)
  BidibCommandStation ensureBidibCommandStation() => $_ensure(4);
}

class CommandStationRef extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'CommandStationRef', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  CommandStationRef._() : super();
  factory CommandStationRef({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory CommandStationRef.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory CommandStationRef.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  CommandStationRef clone() => CommandStationRef()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  CommandStationRef copyWith(void Function(CommandStationRef) updates) => super.copyWith((message) => updates(message as CommandStationRef)) as CommandStationRef; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static CommandStationRef create() => CommandStationRef._();
  CommandStationRef createEmptyInstance() => create();
  static $pb.PbList<CommandStationRef> createRepeated() => $pb.PbList<CommandStationRef>();
  @$core.pragma('dart2js:noInline')
  static CommandStationRef getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<CommandStationRef>(create);
  static CommandStationRef? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

class BidibCommandStation extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BidibCommandStation', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'serialPortName')
    ..hasRequiredFields = false
  ;

  BidibCommandStation._() : super();
  factory BidibCommandStation({
    $core.String? serialPortName,
  }) {
    final _result = create();
    if (serialPortName != null) {
      _result.serialPortName = serialPortName;
    }
    return _result;
  }
  factory BidibCommandStation.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BidibCommandStation.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BidibCommandStation clone() => BidibCommandStation()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BidibCommandStation copyWith(void Function(BidibCommandStation) updates) => super.copyWith((message) => updates(message as BidibCommandStation)) as BidibCommandStation; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BidibCommandStation create() => BidibCommandStation._();
  BidibCommandStation createEmptyInstance() => create();
  static $pb.PbList<BidibCommandStation> createRepeated() => $pb.PbList<BidibCommandStation>();
  @$core.pragma('dart2js:noInline')
  static BidibCommandStation getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BidibCommandStation>(create);
  static BidibCommandStation? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get serialPortName => $_getSZ(0);
  @$pb.TagNumber(1)
  set serialPortName($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasSerialPortName() => $_has(0);
  @$pb.TagNumber(1)
  void clearSerialPortName() => clearField(1);
}

class BinkyNetCommandStation extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BinkyNetCommandStation', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'serverHost')
    ..a<$core.int>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'grpcPort', $pb.PbFieldType.O3)
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'requiredWorkerVersion')
    ..pc<BinkyNetLocalWorkerRef>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'localWorkers', $pb.PbFieldType.PM, subBuilder: BinkyNetLocalWorkerRef.create)
    ..aOB(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'excludeUnusedObjects')
    ..aOS(6, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'domain')
    ..hasRequiredFields = false
  ;

  BinkyNetCommandStation._() : super();
  factory BinkyNetCommandStation({
    $core.String? serverHost,
    $core.int? grpcPort,
    $core.String? requiredWorkerVersion,
    $core.Iterable<BinkyNetLocalWorkerRef>? localWorkers,
    $core.bool? excludeUnusedObjects,
    $core.String? domain,
  }) {
    final _result = create();
    if (serverHost != null) {
      _result.serverHost = serverHost;
    }
    if (grpcPort != null) {
      _result.grpcPort = grpcPort;
    }
    if (requiredWorkerVersion != null) {
      _result.requiredWorkerVersion = requiredWorkerVersion;
    }
    if (localWorkers != null) {
      _result.localWorkers.addAll(localWorkers);
    }
    if (excludeUnusedObjects != null) {
      _result.excludeUnusedObjects = excludeUnusedObjects;
    }
    if (domain != null) {
      _result.domain = domain;
    }
    return _result;
  }
  factory BinkyNetCommandStation.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BinkyNetCommandStation.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BinkyNetCommandStation clone() => BinkyNetCommandStation()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BinkyNetCommandStation copyWith(void Function(BinkyNetCommandStation) updates) => super.copyWith((message) => updates(message as BinkyNetCommandStation)) as BinkyNetCommandStation; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BinkyNetCommandStation create() => BinkyNetCommandStation._();
  BinkyNetCommandStation createEmptyInstance() => create();
  static $pb.PbList<BinkyNetCommandStation> createRepeated() => $pb.PbList<BinkyNetCommandStation>();
  @$core.pragma('dart2js:noInline')
  static BinkyNetCommandStation getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BinkyNetCommandStation>(create);
  static BinkyNetCommandStation? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get serverHost => $_getSZ(0);
  @$pb.TagNumber(1)
  set serverHost($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasServerHost() => $_has(0);
  @$pb.TagNumber(1)
  void clearServerHost() => clearField(1);

  @$pb.TagNumber(2)
  $core.int get grpcPort => $_getIZ(1);
  @$pb.TagNumber(2)
  set grpcPort($core.int v) { $_setSignedInt32(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasGrpcPort() => $_has(1);
  @$pb.TagNumber(2)
  void clearGrpcPort() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get requiredWorkerVersion => $_getSZ(2);
  @$pb.TagNumber(3)
  set requiredWorkerVersion($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasRequiredWorkerVersion() => $_has(2);
  @$pb.TagNumber(3)
  void clearRequiredWorkerVersion() => clearField(3);

  @$pb.TagNumber(4)
  $core.List<BinkyNetLocalWorkerRef> get localWorkers => $_getList(3);

  @$pb.TagNumber(5)
  $core.bool get excludeUnusedObjects => $_getBF(4);
  @$pb.TagNumber(5)
  set excludeUnusedObjects($core.bool v) { $_setBool(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasExcludeUnusedObjects() => $_has(4);
  @$pb.TagNumber(5)
  void clearExcludeUnusedObjects() => clearField(5);

  @$pb.TagNumber(6)
  $core.String get domain => $_getSZ(5);
  @$pb.TagNumber(6)
  set domain($core.String v) { $_setString(5, v); }
  @$pb.TagNumber(6)
  $core.bool hasDomain() => $_has(5);
  @$pb.TagNumber(6)
  void clearDomain() => clearField(6);
}

class BinkyNetLocalWorker extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BinkyNetLocalWorker', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'commandStationId')
    ..aOS(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'hardwareId')
    ..aOS(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'alias')
    ..e<BinkyNetLocalWorkerType>(6, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'localWorkerType', $pb.PbFieldType.OE, defaultOrMaker: BinkyNetLocalWorkerType.LINUX, valueOf: BinkyNetLocalWorkerType.valueOf, enumValues: BinkyNetLocalWorkerType.values)
    ..pc<BinkyNetDevice>(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'devices', $pb.PbFieldType.PM, subBuilder: BinkyNetDevice.create)
    ..pc<BinkyNetObject>(11, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'objects', $pb.PbFieldType.PM, subBuilder: BinkyNetObject.create)
    ..pc<BinkyNetRouter>(12, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'routers', $pb.PbFieldType.PM, subBuilder: BinkyNetRouter.create)
    ..pPS(20, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'validationFindings')
    ..hasRequiredFields = false
  ;

  BinkyNetLocalWorker._() : super();
  factory BinkyNetLocalWorker({
    $core.String? id,
    $core.String? description,
    $core.String? commandStationId,
    $core.String? hardwareId,
    $core.String? alias,
    BinkyNetLocalWorkerType? localWorkerType,
    $core.Iterable<BinkyNetDevice>? devices,
    $core.Iterable<BinkyNetObject>? objects,
    $core.Iterable<BinkyNetRouter>? routers,
    $core.Iterable<$core.String>? validationFindings,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (commandStationId != null) {
      _result.commandStationId = commandStationId;
    }
    if (hardwareId != null) {
      _result.hardwareId = hardwareId;
    }
    if (alias != null) {
      _result.alias = alias;
    }
    if (localWorkerType != null) {
      _result.localWorkerType = localWorkerType;
    }
    if (devices != null) {
      _result.devices.addAll(devices);
    }
    if (objects != null) {
      _result.objects.addAll(objects);
    }
    if (routers != null) {
      _result.routers.addAll(routers);
    }
    if (validationFindings != null) {
      _result.validationFindings.addAll(validationFindings);
    }
    return _result;
  }
  factory BinkyNetLocalWorker.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BinkyNetLocalWorker.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BinkyNetLocalWorker clone() => BinkyNetLocalWorker()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BinkyNetLocalWorker copyWith(void Function(BinkyNetLocalWorker) updates) => super.copyWith((message) => updates(message as BinkyNetLocalWorker)) as BinkyNetLocalWorker; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BinkyNetLocalWorker create() => BinkyNetLocalWorker._();
  BinkyNetLocalWorker createEmptyInstance() => create();
  static $pb.PbList<BinkyNetLocalWorker> createRepeated() => $pb.PbList<BinkyNetLocalWorker>();
  @$core.pragma('dart2js:noInline')
  static BinkyNetLocalWorker getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BinkyNetLocalWorker>(create);
  static BinkyNetLocalWorker? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get commandStationId => $_getSZ(2);
  @$pb.TagNumber(3)
  set commandStationId($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasCommandStationId() => $_has(2);
  @$pb.TagNumber(3)
  void clearCommandStationId() => clearField(3);

  @$pb.TagNumber(4)
  $core.String get hardwareId => $_getSZ(3);
  @$pb.TagNumber(4)
  set hardwareId($core.String v) { $_setString(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasHardwareId() => $_has(3);
  @$pb.TagNumber(4)
  void clearHardwareId() => clearField(4);

  @$pb.TagNumber(5)
  $core.String get alias => $_getSZ(4);
  @$pb.TagNumber(5)
  set alias($core.String v) { $_setString(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasAlias() => $_has(4);
  @$pb.TagNumber(5)
  void clearAlias() => clearField(5);

  @$pb.TagNumber(6)
  BinkyNetLocalWorkerType get localWorkerType => $_getN(5);
  @$pb.TagNumber(6)
  set localWorkerType(BinkyNetLocalWorkerType v) { setField(6, v); }
  @$pb.TagNumber(6)
  $core.bool hasLocalWorkerType() => $_has(5);
  @$pb.TagNumber(6)
  void clearLocalWorkerType() => clearField(6);

  @$pb.TagNumber(10)
  $core.List<BinkyNetDevice> get devices => $_getList(6);

  @$pb.TagNumber(11)
  $core.List<BinkyNetObject> get objects => $_getList(7);

  @$pb.TagNumber(12)
  $core.List<BinkyNetRouter> get routers => $_getList(8);

  @$pb.TagNumber(20)
  $core.List<$core.String> get validationFindings => $_getList(9);
}

class BinkyNetLocalWorkerRef extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BinkyNetLocalWorkerRef', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  BinkyNetLocalWorkerRef._() : super();
  factory BinkyNetLocalWorkerRef({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory BinkyNetLocalWorkerRef.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BinkyNetLocalWorkerRef.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BinkyNetLocalWorkerRef clone() => BinkyNetLocalWorkerRef()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BinkyNetLocalWorkerRef copyWith(void Function(BinkyNetLocalWorkerRef) updates) => super.copyWith((message) => updates(message as BinkyNetLocalWorkerRef)) as BinkyNetLocalWorkerRef; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BinkyNetLocalWorkerRef create() => BinkyNetLocalWorkerRef._();
  BinkyNetLocalWorkerRef createEmptyInstance() => create();
  static $pb.PbList<BinkyNetLocalWorkerRef> createRepeated() => $pb.PbList<BinkyNetLocalWorkerRef>();
  @$core.pragma('dart2js:noInline')
  static BinkyNetLocalWorkerRef getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BinkyNetLocalWorkerRef>(create);
  static BinkyNetLocalWorkerRef? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

class BinkyNetRouter extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BinkyNetRouter', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..pPS(20, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'validationFindings')
    ..hasRequiredFields = false
  ;

  BinkyNetRouter._() : super();
  factory BinkyNetRouter({
    $core.String? id,
    $core.String? description,
    $core.Iterable<$core.String>? validationFindings,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (validationFindings != null) {
      _result.validationFindings.addAll(validationFindings);
    }
    return _result;
  }
  factory BinkyNetRouter.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BinkyNetRouter.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BinkyNetRouter clone() => BinkyNetRouter()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BinkyNetRouter copyWith(void Function(BinkyNetRouter) updates) => super.copyWith((message) => updates(message as BinkyNetRouter)) as BinkyNetRouter; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BinkyNetRouter create() => BinkyNetRouter._();
  BinkyNetRouter createEmptyInstance() => create();
  static $pb.PbList<BinkyNetRouter> createRepeated() => $pb.PbList<BinkyNetRouter>();
  @$core.pragma('dart2js:noInline')
  static BinkyNetRouter getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BinkyNetRouter>(create);
  static BinkyNetRouter? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(20)
  $core.List<$core.String> get validationFindings => $_getList(2);
}

class BinkyNetDevice extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BinkyNetDevice', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'deviceId')
    ..e<BinkyNetDeviceType>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'deviceType', $pb.PbFieldType.OE, defaultOrMaker: BinkyNetDeviceType.MCP23008, valueOf: BinkyNetDeviceType.valueOf, enumValues: BinkyNetDeviceType.values)
    ..aOS(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'address')
    ..aOB(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'disabled')
    ..aOS(6, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'routerId')
    ..aOB(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'canAddSensors8Group', protoName: 'can_add_sensors_8_group')
    ..aOB(11, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'canAddSensors4Group', protoName: 'can_add_sensors_4_group')
    ..pPS(20, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'validationFindings')
    ..hasRequiredFields = false
  ;

  BinkyNetDevice._() : super();
  factory BinkyNetDevice({
    $core.String? id,
    $core.String? deviceId,
    BinkyNetDeviceType? deviceType,
    $core.String? address,
    $core.bool? disabled,
    $core.String? routerId,
    $core.bool? canAddSensors8Group,
    $core.bool? canAddSensors4Group,
    $core.Iterable<$core.String>? validationFindings,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (deviceId != null) {
      _result.deviceId = deviceId;
    }
    if (deviceType != null) {
      _result.deviceType = deviceType;
    }
    if (address != null) {
      _result.address = address;
    }
    if (disabled != null) {
      _result.disabled = disabled;
    }
    if (routerId != null) {
      _result.routerId = routerId;
    }
    if (canAddSensors8Group != null) {
      _result.canAddSensors8Group = canAddSensors8Group;
    }
    if (canAddSensors4Group != null) {
      _result.canAddSensors4Group = canAddSensors4Group;
    }
    if (validationFindings != null) {
      _result.validationFindings.addAll(validationFindings);
    }
    return _result;
  }
  factory BinkyNetDevice.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BinkyNetDevice.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BinkyNetDevice clone() => BinkyNetDevice()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BinkyNetDevice copyWith(void Function(BinkyNetDevice) updates) => super.copyWith((message) => updates(message as BinkyNetDevice)) as BinkyNetDevice; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BinkyNetDevice create() => BinkyNetDevice._();
  BinkyNetDevice createEmptyInstance() => create();
  static $pb.PbList<BinkyNetDevice> createRepeated() => $pb.PbList<BinkyNetDevice>();
  @$core.pragma('dart2js:noInline')
  static BinkyNetDevice getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BinkyNetDevice>(create);
  static BinkyNetDevice? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get deviceId => $_getSZ(1);
  @$pb.TagNumber(2)
  set deviceId($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDeviceId() => $_has(1);
  @$pb.TagNumber(2)
  void clearDeviceId() => clearField(2);

  @$pb.TagNumber(3)
  BinkyNetDeviceType get deviceType => $_getN(2);
  @$pb.TagNumber(3)
  set deviceType(BinkyNetDeviceType v) { setField(3, v); }
  @$pb.TagNumber(3)
  $core.bool hasDeviceType() => $_has(2);
  @$pb.TagNumber(3)
  void clearDeviceType() => clearField(3);

  @$pb.TagNumber(4)
  $core.String get address => $_getSZ(3);
  @$pb.TagNumber(4)
  set address($core.String v) { $_setString(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasAddress() => $_has(3);
  @$pb.TagNumber(4)
  void clearAddress() => clearField(4);

  @$pb.TagNumber(5)
  $core.bool get disabled => $_getBF(4);
  @$pb.TagNumber(5)
  set disabled($core.bool v) { $_setBool(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasDisabled() => $_has(4);
  @$pb.TagNumber(5)
  void clearDisabled() => clearField(5);

  @$pb.TagNumber(6)
  $core.String get routerId => $_getSZ(5);
  @$pb.TagNumber(6)
  set routerId($core.String v) { $_setString(5, v); }
  @$pb.TagNumber(6)
  $core.bool hasRouterId() => $_has(5);
  @$pb.TagNumber(6)
  void clearRouterId() => clearField(6);

  @$pb.TagNumber(10)
  $core.bool get canAddSensors8Group => $_getBF(6);
  @$pb.TagNumber(10)
  set canAddSensors8Group($core.bool v) { $_setBool(6, v); }
  @$pb.TagNumber(10)
  $core.bool hasCanAddSensors8Group() => $_has(6);
  @$pb.TagNumber(10)
  void clearCanAddSensors8Group() => clearField(10);

  @$pb.TagNumber(11)
  $core.bool get canAddSensors4Group => $_getBF(7);
  @$pb.TagNumber(11)
  set canAddSensors4Group($core.bool v) { $_setBool(7, v); }
  @$pb.TagNumber(11)
  $core.bool hasCanAddSensors4Group() => $_has(7);
  @$pb.TagNumber(11)
  void clearCanAddSensors4Group() => clearField(11);

  @$pb.TagNumber(20)
  $core.List<$core.String> get validationFindings => $_getList(8);
}

class BinkyNetObject extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BinkyNetObject', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'objectId')
    ..e<BinkyNetObjectType>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'objectType', $pb.PbFieldType.OE, defaultOrMaker: BinkyNetObjectType.BINARYSENSOR, valueOf: BinkyNetObjectType.valueOf, enumValues: BinkyNetObjectType.values)
    ..pc<BinkyNetConnection>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'connections', $pb.PbFieldType.PM, subBuilder: BinkyNetConnection.create)
    ..m<$core.String, $core.String>(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'configuration', entryClassName: 'BinkyNetObject.ConfigurationEntry', keyFieldType: $pb.PbFieldType.OS, valueFieldType: $pb.PbFieldType.OS, packageName: const $pb.PackageName('binkyrailways.v1'))
    ..pPS(20, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'validationFindings')
    ..hasRequiredFields = false
  ;

  BinkyNetObject._() : super();
  factory BinkyNetObject({
    $core.String? id,
    $core.String? objectId,
    BinkyNetObjectType? objectType,
    $core.Iterable<BinkyNetConnection>? connections,
    $core.Map<$core.String, $core.String>? configuration,
    $core.Iterable<$core.String>? validationFindings,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (objectId != null) {
      _result.objectId = objectId;
    }
    if (objectType != null) {
      _result.objectType = objectType;
    }
    if (connections != null) {
      _result.connections.addAll(connections);
    }
    if (configuration != null) {
      _result.configuration.addAll(configuration);
    }
    if (validationFindings != null) {
      _result.validationFindings.addAll(validationFindings);
    }
    return _result;
  }
  factory BinkyNetObject.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BinkyNetObject.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BinkyNetObject clone() => BinkyNetObject()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BinkyNetObject copyWith(void Function(BinkyNetObject) updates) => super.copyWith((message) => updates(message as BinkyNetObject)) as BinkyNetObject; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BinkyNetObject create() => BinkyNetObject._();
  BinkyNetObject createEmptyInstance() => create();
  static $pb.PbList<BinkyNetObject> createRepeated() => $pb.PbList<BinkyNetObject>();
  @$core.pragma('dart2js:noInline')
  static BinkyNetObject getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BinkyNetObject>(create);
  static BinkyNetObject? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get objectId => $_getSZ(1);
  @$pb.TagNumber(2)
  set objectId($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasObjectId() => $_has(1);
  @$pb.TagNumber(2)
  void clearObjectId() => clearField(2);

  @$pb.TagNumber(3)
  BinkyNetObjectType get objectType => $_getN(2);
  @$pb.TagNumber(3)
  set objectType(BinkyNetObjectType v) { setField(3, v); }
  @$pb.TagNumber(3)
  $core.bool hasObjectType() => $_has(2);
  @$pb.TagNumber(3)
  void clearObjectType() => clearField(3);

  @$pb.TagNumber(4)
  $core.List<BinkyNetConnection> get connections => $_getList(3);

  @$pb.TagNumber(5)
  $core.Map<$core.String, $core.String> get configuration => $_getMap(4);

  @$pb.TagNumber(20)
  $core.List<$core.String> get validationFindings => $_getList(5);
}

class BinkyNetConnection extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BinkyNetConnection', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'key')
    ..pc<BinkyNetDevicePin>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'pins', $pb.PbFieldType.PM, subBuilder: BinkyNetDevicePin.create)
    ..m<$core.String, $core.String>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'configuration', entryClassName: 'BinkyNetConnection.ConfigurationEntry', keyFieldType: $pb.PbFieldType.OS, valueFieldType: $pb.PbFieldType.OS, packageName: const $pb.PackageName('binkyrailways.v1'))
    ..hasRequiredFields = false
  ;

  BinkyNetConnection._() : super();
  factory BinkyNetConnection({
    $core.String? key,
    $core.Iterable<BinkyNetDevicePin>? pins,
    $core.Map<$core.String, $core.String>? configuration,
  }) {
    final _result = create();
    if (key != null) {
      _result.key = key;
    }
    if (pins != null) {
      _result.pins.addAll(pins);
    }
    if (configuration != null) {
      _result.configuration.addAll(configuration);
    }
    return _result;
  }
  factory BinkyNetConnection.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BinkyNetConnection.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BinkyNetConnection clone() => BinkyNetConnection()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BinkyNetConnection copyWith(void Function(BinkyNetConnection) updates) => super.copyWith((message) => updates(message as BinkyNetConnection)) as BinkyNetConnection; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BinkyNetConnection create() => BinkyNetConnection._();
  BinkyNetConnection createEmptyInstance() => create();
  static $pb.PbList<BinkyNetConnection> createRepeated() => $pb.PbList<BinkyNetConnection>();
  @$core.pragma('dart2js:noInline')
  static BinkyNetConnection getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BinkyNetConnection>(create);
  static BinkyNetConnection? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get key => $_getSZ(0);
  @$pb.TagNumber(1)
  set key($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasKey() => $_has(0);
  @$pb.TagNumber(1)
  void clearKey() => clearField(1);

  @$pb.TagNumber(2)
  $core.List<BinkyNetDevicePin> get pins => $_getList(1);

  @$pb.TagNumber(3)
  $core.Map<$core.String, $core.String> get configuration => $_getMap(2);
}

class BinkyNetDevicePin extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BinkyNetDevicePin', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'deviceId')
    ..a<$core.int>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'index', $pb.PbFieldType.OU3)
    ..hasRequiredFields = false
  ;

  BinkyNetDevicePin._() : super();
  factory BinkyNetDevicePin({
    $core.String? deviceId,
    $core.int? index,
  }) {
    final _result = create();
    if (deviceId != null) {
      _result.deviceId = deviceId;
    }
    if (index != null) {
      _result.index = index;
    }
    return _result;
  }
  factory BinkyNetDevicePin.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BinkyNetDevicePin.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BinkyNetDevicePin clone() => BinkyNetDevicePin()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BinkyNetDevicePin copyWith(void Function(BinkyNetDevicePin) updates) => super.copyWith((message) => updates(message as BinkyNetDevicePin)) as BinkyNetDevicePin; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BinkyNetDevicePin create() => BinkyNetDevicePin._();
  BinkyNetDevicePin createEmptyInstance() => create();
  static $pb.PbList<BinkyNetDevicePin> createRepeated() => $pb.PbList<BinkyNetDevicePin>();
  @$core.pragma('dart2js:noInline')
  static BinkyNetDevicePin getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BinkyNetDevicePin>(create);
  static BinkyNetDevicePin? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get deviceId => $_getSZ(0);
  @$pb.TagNumber(1)
  set deviceId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasDeviceId() => $_has(0);
  @$pb.TagNumber(1)
  void clearDeviceId() => clearField(1);

  @$pb.TagNumber(2)
  $core.int get index => $_getIZ(1);
  @$pb.TagNumber(2)
  set index($core.int v) { $_setUnsignedInt32(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasIndex() => $_has(1);
  @$pb.TagNumber(2)
  void clearIndex() => clearField(2);
}

class Block extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Block', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'moduleId')
    ..aOM<Position>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'position', subBuilder: Position.create)
    ..a<$core.int>(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'waitProbability', $pb.PbFieldType.O3)
    ..a<$core.int>(11, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'minimumWaitTime', $pb.PbFieldType.O3)
    ..a<$core.int>(12, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'maximumWaitTime', $pb.PbFieldType.O3)
    ..aOS(13, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'waitPermissions')
    ..aOB(14, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'reverseSides')
    ..e<ChangeDirection>(15, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'changeDirection', $pb.PbFieldType.OE, defaultOrMaker: ChangeDirection.ALLOW, valueOf: ChangeDirection.valueOf, enumValues: ChangeDirection.values)
    ..aOB(16, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'changeDirectionReversingLocs')
    ..aOB(18, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isStation')
    ..aOM<BlockGroupRef>(19, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'blockGroup', subBuilder: BlockGroupRef.create)
    ..hasRequiredFields = false
  ;

  Block._() : super();
  factory Block({
    $core.String? id,
    $core.String? description,
    $core.String? moduleId,
    Position? position,
    $core.int? waitProbability,
    $core.int? minimumWaitTime,
    $core.int? maximumWaitTime,
    $core.String? waitPermissions,
    $core.bool? reverseSides,
    ChangeDirection? changeDirection,
    $core.bool? changeDirectionReversingLocs,
    $core.bool? isStation,
    BlockGroupRef? blockGroup,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (moduleId != null) {
      _result.moduleId = moduleId;
    }
    if (position != null) {
      _result.position = position;
    }
    if (waitProbability != null) {
      _result.waitProbability = waitProbability;
    }
    if (minimumWaitTime != null) {
      _result.minimumWaitTime = minimumWaitTime;
    }
    if (maximumWaitTime != null) {
      _result.maximumWaitTime = maximumWaitTime;
    }
    if (waitPermissions != null) {
      _result.waitPermissions = waitPermissions;
    }
    if (reverseSides != null) {
      _result.reverseSides = reverseSides;
    }
    if (changeDirection != null) {
      _result.changeDirection = changeDirection;
    }
    if (changeDirectionReversingLocs != null) {
      _result.changeDirectionReversingLocs = changeDirectionReversingLocs;
    }
    if (isStation != null) {
      _result.isStation = isStation;
    }
    if (blockGroup != null) {
      _result.blockGroup = blockGroup;
    }
    return _result;
  }
  factory Block.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Block.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Block clone() => Block()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Block copyWith(void Function(Block) updates) => super.copyWith((message) => updates(message as Block)) as Block; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Block create() => Block._();
  Block createEmptyInstance() => create();
  static $pb.PbList<Block> createRepeated() => $pb.PbList<Block>();
  @$core.pragma('dart2js:noInline')
  static Block getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Block>(create);
  static Block? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get moduleId => $_getSZ(2);
  @$pb.TagNumber(3)
  set moduleId($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasModuleId() => $_has(2);
  @$pb.TagNumber(3)
  void clearModuleId() => clearField(3);

  @$pb.TagNumber(4)
  Position get position => $_getN(3);
  @$pb.TagNumber(4)
  set position(Position v) { setField(4, v); }
  @$pb.TagNumber(4)
  $core.bool hasPosition() => $_has(3);
  @$pb.TagNumber(4)
  void clearPosition() => clearField(4);
  @$pb.TagNumber(4)
  Position ensurePosition() => $_ensure(3);

  @$pb.TagNumber(10)
  $core.int get waitProbability => $_getIZ(4);
  @$pb.TagNumber(10)
  set waitProbability($core.int v) { $_setSignedInt32(4, v); }
  @$pb.TagNumber(10)
  $core.bool hasWaitProbability() => $_has(4);
  @$pb.TagNumber(10)
  void clearWaitProbability() => clearField(10);

  @$pb.TagNumber(11)
  $core.int get minimumWaitTime => $_getIZ(5);
  @$pb.TagNumber(11)
  set minimumWaitTime($core.int v) { $_setSignedInt32(5, v); }
  @$pb.TagNumber(11)
  $core.bool hasMinimumWaitTime() => $_has(5);
  @$pb.TagNumber(11)
  void clearMinimumWaitTime() => clearField(11);

  @$pb.TagNumber(12)
  $core.int get maximumWaitTime => $_getIZ(6);
  @$pb.TagNumber(12)
  set maximumWaitTime($core.int v) { $_setSignedInt32(6, v); }
  @$pb.TagNumber(12)
  $core.bool hasMaximumWaitTime() => $_has(6);
  @$pb.TagNumber(12)
  void clearMaximumWaitTime() => clearField(12);

  @$pb.TagNumber(13)
  $core.String get waitPermissions => $_getSZ(7);
  @$pb.TagNumber(13)
  set waitPermissions($core.String v) { $_setString(7, v); }
  @$pb.TagNumber(13)
  $core.bool hasWaitPermissions() => $_has(7);
  @$pb.TagNumber(13)
  void clearWaitPermissions() => clearField(13);

  @$pb.TagNumber(14)
  $core.bool get reverseSides => $_getBF(8);
  @$pb.TagNumber(14)
  set reverseSides($core.bool v) { $_setBool(8, v); }
  @$pb.TagNumber(14)
  $core.bool hasReverseSides() => $_has(8);
  @$pb.TagNumber(14)
  void clearReverseSides() => clearField(14);

  @$pb.TagNumber(15)
  ChangeDirection get changeDirection => $_getN(9);
  @$pb.TagNumber(15)
  set changeDirection(ChangeDirection v) { setField(15, v); }
  @$pb.TagNumber(15)
  $core.bool hasChangeDirection() => $_has(9);
  @$pb.TagNumber(15)
  void clearChangeDirection() => clearField(15);

  @$pb.TagNumber(16)
  $core.bool get changeDirectionReversingLocs => $_getBF(10);
  @$pb.TagNumber(16)
  set changeDirectionReversingLocs($core.bool v) { $_setBool(10, v); }
  @$pb.TagNumber(16)
  $core.bool hasChangeDirectionReversingLocs() => $_has(10);
  @$pb.TagNumber(16)
  void clearChangeDirectionReversingLocs() => clearField(16);

  @$pb.TagNumber(18)
  $core.bool get isStation => $_getBF(11);
  @$pb.TagNumber(18)
  set isStation($core.bool v) { $_setBool(11, v); }
  @$pb.TagNumber(18)
  $core.bool hasIsStation() => $_has(11);
  @$pb.TagNumber(18)
  void clearIsStation() => clearField(18);

  @$pb.TagNumber(19)
  BlockGroupRef get blockGroup => $_getN(12);
  @$pb.TagNumber(19)
  set blockGroup(BlockGroupRef v) { setField(19, v); }
  @$pb.TagNumber(19)
  $core.bool hasBlockGroup() => $_has(12);
  @$pb.TagNumber(19)
  void clearBlockGroup() => clearField(19);
  @$pb.TagNumber(19)
  BlockGroupRef ensureBlockGroup() => $_ensure(12);
}

class BlockRef extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BlockRef', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  BlockRef._() : super();
  factory BlockRef({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory BlockRef.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BlockRef.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BlockRef clone() => BlockRef()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BlockRef copyWith(void Function(BlockRef) updates) => super.copyWith((message) => updates(message as BlockRef)) as BlockRef; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BlockRef create() => BlockRef._();
  BlockRef createEmptyInstance() => create();
  static $pb.PbList<BlockRef> createRepeated() => $pb.PbList<BlockRef>();
  @$core.pragma('dart2js:noInline')
  static BlockRef getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BlockRef>(create);
  static BlockRef? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

class BlockGroup extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BlockGroup', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'moduleId')
    ..a<$core.int>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'minimumLocsInGroup', $pb.PbFieldType.O3)
    ..a<$core.int>(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'minimumLocsOnTrack', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  BlockGroup._() : super();
  factory BlockGroup({
    $core.String? id,
    $core.String? description,
    $core.String? moduleId,
    $core.int? minimumLocsInGroup,
    $core.int? minimumLocsOnTrack,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (moduleId != null) {
      _result.moduleId = moduleId;
    }
    if (minimumLocsInGroup != null) {
      _result.minimumLocsInGroup = minimumLocsInGroup;
    }
    if (minimumLocsOnTrack != null) {
      _result.minimumLocsOnTrack = minimumLocsOnTrack;
    }
    return _result;
  }
  factory BlockGroup.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BlockGroup.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BlockGroup clone() => BlockGroup()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BlockGroup copyWith(void Function(BlockGroup) updates) => super.copyWith((message) => updates(message as BlockGroup)) as BlockGroup; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BlockGroup create() => BlockGroup._();
  BlockGroup createEmptyInstance() => create();
  static $pb.PbList<BlockGroup> createRepeated() => $pb.PbList<BlockGroup>();
  @$core.pragma('dart2js:noInline')
  static BlockGroup getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BlockGroup>(create);
  static BlockGroup? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get moduleId => $_getSZ(2);
  @$pb.TagNumber(3)
  set moduleId($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasModuleId() => $_has(2);
  @$pb.TagNumber(3)
  void clearModuleId() => clearField(3);

  @$pb.TagNumber(4)
  $core.int get minimumLocsInGroup => $_getIZ(3);
  @$pb.TagNumber(4)
  set minimumLocsInGroup($core.int v) { $_setSignedInt32(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasMinimumLocsInGroup() => $_has(3);
  @$pb.TagNumber(4)
  void clearMinimumLocsInGroup() => clearField(4);

  @$pb.TagNumber(5)
  $core.int get minimumLocsOnTrack => $_getIZ(4);
  @$pb.TagNumber(5)
  set minimumLocsOnTrack($core.int v) { $_setSignedInt32(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasMinimumLocsOnTrack() => $_has(4);
  @$pb.TagNumber(5)
  void clearMinimumLocsOnTrack() => clearField(5);
}

class BlockGroupRef extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BlockGroupRef', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  BlockGroupRef._() : super();
  factory BlockGroupRef({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory BlockGroupRef.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BlockGroupRef.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BlockGroupRef clone() => BlockGroupRef()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BlockGroupRef copyWith(void Function(BlockGroupRef) updates) => super.copyWith((message) => updates(message as BlockGroupRef)) as BlockGroupRef; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BlockGroupRef create() => BlockGroupRef._();
  BlockGroupRef createEmptyInstance() => create();
  static $pb.PbList<BlockGroupRef> createRepeated() => $pb.PbList<BlockGroupRef>();
  @$core.pragma('dart2js:noInline')
  static BlockGroupRef getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BlockGroupRef>(create);
  static BlockGroupRef? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

class Edge extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Edge', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'moduleId')
    ..aOM<Position>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'position', subBuilder: Position.create)
    ..hasRequiredFields = false
  ;

  Edge._() : super();
  factory Edge({
    $core.String? id,
    $core.String? description,
    $core.String? moduleId,
    Position? position,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (moduleId != null) {
      _result.moduleId = moduleId;
    }
    if (position != null) {
      _result.position = position;
    }
    return _result;
  }
  factory Edge.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Edge.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Edge clone() => Edge()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Edge copyWith(void Function(Edge) updates) => super.copyWith((message) => updates(message as Edge)) as Edge; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Edge create() => Edge._();
  Edge createEmptyInstance() => create();
  static $pb.PbList<Edge> createRepeated() => $pb.PbList<Edge>();
  @$core.pragma('dart2js:noInline')
  static Edge getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Edge>(create);
  static Edge? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get moduleId => $_getSZ(2);
  @$pb.TagNumber(3)
  set moduleId($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasModuleId() => $_has(2);
  @$pb.TagNumber(3)
  void clearModuleId() => clearField(3);

  @$pb.TagNumber(4)
  Position get position => $_getN(3);
  @$pb.TagNumber(4)
  set position(Position v) { setField(4, v); }
  @$pb.TagNumber(4)
  $core.bool hasPosition() => $_has(3);
  @$pb.TagNumber(4)
  void clearPosition() => clearField(4);
  @$pb.TagNumber(4)
  Position ensurePosition() => $_ensure(3);
}

class EdgeRef extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'EdgeRef', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  EdgeRef._() : super();
  factory EdgeRef({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory EdgeRef.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory EdgeRef.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  EdgeRef clone() => EdgeRef()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  EdgeRef copyWith(void Function(EdgeRef) updates) => super.copyWith((message) => updates(message as EdgeRef)) as EdgeRef; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static EdgeRef create() => EdgeRef._();
  EdgeRef createEmptyInstance() => create();
  static $pb.PbList<EdgeRef> createRepeated() => $pb.PbList<EdgeRef>();
  @$core.pragma('dart2js:noInline')
  static EdgeRef getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<EdgeRef>(create);
  static EdgeRef? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

class Junction extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Junction', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'moduleId')
    ..aOM<Position>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'position', subBuilder: Position.create)
    ..aOM<BlockRef>(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'block', subBuilder: BlockRef.create)
    ..aOM<Switch>(6, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'switch', subBuilder: Switch.create)
    ..hasRequiredFields = false
  ;

  Junction._() : super();
  factory Junction({
    $core.String? id,
    $core.String? description,
    $core.String? moduleId,
    Position? position,
    BlockRef? block,
    Switch? switch_6,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (moduleId != null) {
      _result.moduleId = moduleId;
    }
    if (position != null) {
      _result.position = position;
    }
    if (block != null) {
      _result.block = block;
    }
    if (switch_6 != null) {
      _result.switch_6 = switch_6;
    }
    return _result;
  }
  factory Junction.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Junction.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Junction clone() => Junction()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Junction copyWith(void Function(Junction) updates) => super.copyWith((message) => updates(message as Junction)) as Junction; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Junction create() => Junction._();
  Junction createEmptyInstance() => create();
  static $pb.PbList<Junction> createRepeated() => $pb.PbList<Junction>();
  @$core.pragma('dart2js:noInline')
  static Junction getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Junction>(create);
  static Junction? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get moduleId => $_getSZ(2);
  @$pb.TagNumber(3)
  set moduleId($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasModuleId() => $_has(2);
  @$pb.TagNumber(3)
  void clearModuleId() => clearField(3);

  @$pb.TagNumber(4)
  Position get position => $_getN(3);
  @$pb.TagNumber(4)
  set position(Position v) { setField(4, v); }
  @$pb.TagNumber(4)
  $core.bool hasPosition() => $_has(3);
  @$pb.TagNumber(4)
  void clearPosition() => clearField(4);
  @$pb.TagNumber(4)
  Position ensurePosition() => $_ensure(3);

  @$pb.TagNumber(5)
  BlockRef get block => $_getN(4);
  @$pb.TagNumber(5)
  set block(BlockRef v) { setField(5, v); }
  @$pb.TagNumber(5)
  $core.bool hasBlock() => $_has(4);
  @$pb.TagNumber(5)
  void clearBlock() => clearField(5);
  @$pb.TagNumber(5)
  BlockRef ensureBlock() => $_ensure(4);

  @$pb.TagNumber(6)
  Switch get switch_6 => $_getN(5);
  @$pb.TagNumber(6)
  set switch_6(Switch v) { setField(6, v); }
  @$pb.TagNumber(6)
  $core.bool hasSwitch_6() => $_has(5);
  @$pb.TagNumber(6)
  void clearSwitch_6() => clearField(6);
  @$pb.TagNumber(6)
  Switch ensureSwitch_6() => $_ensure(5);
}

class JunctionRef extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'JunctionRef', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  JunctionRef._() : super();
  factory JunctionRef({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory JunctionRef.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory JunctionRef.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  JunctionRef clone() => JunctionRef()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  JunctionRef copyWith(void Function(JunctionRef) updates) => super.copyWith((message) => updates(message as JunctionRef)) as JunctionRef; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static JunctionRef create() => JunctionRef._();
  JunctionRef createEmptyInstance() => create();
  static $pb.PbList<JunctionRef> createRepeated() => $pb.PbList<JunctionRef>();
  @$core.pragma('dart2js:noInline')
  static JunctionRef getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<JunctionRef>(create);
  static JunctionRef? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

class Switch extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Switch', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'address')
    ..aOB(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'hasFeedback')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'feedbackAddress')
    ..a<$core.int>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'switchDuration', $pb.PbFieldType.O3)
    ..aOB(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'invert')
    ..aOB(6, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'invertFeedback')
    ..e<SwitchDirection>(7, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'initialDirection', $pb.PbFieldType.OE, defaultOrMaker: SwitchDirection.STRAIGHT, valueOf: SwitchDirection.valueOf, enumValues: SwitchDirection.values)
    ..aOB(8, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isLeft')
    ..hasRequiredFields = false
  ;

  Switch._() : super();
  factory Switch({
    $core.String? address,
    $core.bool? hasFeedback,
    $core.String? feedbackAddress,
    $core.int? switchDuration,
    $core.bool? invert,
    $core.bool? invertFeedback,
    SwitchDirection? initialDirection,
    $core.bool? isLeft,
  }) {
    final _result = create();
    if (address != null) {
      _result.address = address;
    }
    if (hasFeedback != null) {
      _result.hasFeedback = hasFeedback;
    }
    if (feedbackAddress != null) {
      _result.feedbackAddress = feedbackAddress;
    }
    if (switchDuration != null) {
      _result.switchDuration = switchDuration;
    }
    if (invert != null) {
      _result.invert = invert;
    }
    if (invertFeedback != null) {
      _result.invertFeedback = invertFeedback;
    }
    if (initialDirection != null) {
      _result.initialDirection = initialDirection;
    }
    if (isLeft != null) {
      _result.isLeft = isLeft;
    }
    return _result;
  }
  factory Switch.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Switch.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Switch clone() => Switch()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Switch copyWith(void Function(Switch) updates) => super.copyWith((message) => updates(message as Switch)) as Switch; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Switch create() => Switch._();
  Switch createEmptyInstance() => create();
  static $pb.PbList<Switch> createRepeated() => $pb.PbList<Switch>();
  @$core.pragma('dart2js:noInline')
  static Switch getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Switch>(create);
  static Switch? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get address => $_getSZ(0);
  @$pb.TagNumber(1)
  set address($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasAddress() => $_has(0);
  @$pb.TagNumber(1)
  void clearAddress() => clearField(1);

  @$pb.TagNumber(2)
  $core.bool get hasFeedback => $_getBF(1);
  @$pb.TagNumber(2)
  set hasFeedback($core.bool v) { $_setBool(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasHasFeedback() => $_has(1);
  @$pb.TagNumber(2)
  void clearHasFeedback() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get feedbackAddress => $_getSZ(2);
  @$pb.TagNumber(3)
  set feedbackAddress($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasFeedbackAddress() => $_has(2);
  @$pb.TagNumber(3)
  void clearFeedbackAddress() => clearField(3);

  @$pb.TagNumber(4)
  $core.int get switchDuration => $_getIZ(3);
  @$pb.TagNumber(4)
  set switchDuration($core.int v) { $_setSignedInt32(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasSwitchDuration() => $_has(3);
  @$pb.TagNumber(4)
  void clearSwitchDuration() => clearField(4);

  @$pb.TagNumber(5)
  $core.bool get invert => $_getBF(4);
  @$pb.TagNumber(5)
  set invert($core.bool v) { $_setBool(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasInvert() => $_has(4);
  @$pb.TagNumber(5)
  void clearInvert() => clearField(5);

  @$pb.TagNumber(6)
  $core.bool get invertFeedback => $_getBF(5);
  @$pb.TagNumber(6)
  set invertFeedback($core.bool v) { $_setBool(5, v); }
  @$pb.TagNumber(6)
  $core.bool hasInvertFeedback() => $_has(5);
  @$pb.TagNumber(6)
  void clearInvertFeedback() => clearField(6);

  @$pb.TagNumber(7)
  SwitchDirection get initialDirection => $_getN(6);
  @$pb.TagNumber(7)
  set initialDirection(SwitchDirection v) { setField(7, v); }
  @$pb.TagNumber(7)
  $core.bool hasInitialDirection() => $_has(6);
  @$pb.TagNumber(7)
  void clearInitialDirection() => clearField(7);

  @$pb.TagNumber(8)
  $core.bool get isLeft => $_getBF(7);
  @$pb.TagNumber(8)
  set isLeft($core.bool v) { $_setBool(7, v); }
  @$pb.TagNumber(8)
  $core.bool hasIsLeft() => $_has(7);
  @$pb.TagNumber(8)
  void clearIsLeft() => clearField(8);
}

class Output extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Output', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'moduleId')
    ..aOM<Position>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'position', subBuilder: Position.create)
    ..aOM<BinaryOutput>(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'binaryOutput', subBuilder: BinaryOutput.create)
    ..hasRequiredFields = false
  ;

  Output._() : super();
  factory Output({
    $core.String? id,
    $core.String? description,
    $core.String? moduleId,
    Position? position,
    BinaryOutput? binaryOutput,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (moduleId != null) {
      _result.moduleId = moduleId;
    }
    if (position != null) {
      _result.position = position;
    }
    if (binaryOutput != null) {
      _result.binaryOutput = binaryOutput;
    }
    return _result;
  }
  factory Output.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Output.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Output clone() => Output()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Output copyWith(void Function(Output) updates) => super.copyWith((message) => updates(message as Output)) as Output; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Output create() => Output._();
  Output createEmptyInstance() => create();
  static $pb.PbList<Output> createRepeated() => $pb.PbList<Output>();
  @$core.pragma('dart2js:noInline')
  static Output getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Output>(create);
  static Output? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get moduleId => $_getSZ(2);
  @$pb.TagNumber(3)
  set moduleId($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasModuleId() => $_has(2);
  @$pb.TagNumber(3)
  void clearModuleId() => clearField(3);

  @$pb.TagNumber(4)
  Position get position => $_getN(3);
  @$pb.TagNumber(4)
  set position(Position v) { setField(4, v); }
  @$pb.TagNumber(4)
  $core.bool hasPosition() => $_has(3);
  @$pb.TagNumber(4)
  void clearPosition() => clearField(4);
  @$pb.TagNumber(4)
  Position ensurePosition() => $_ensure(3);

  @$pb.TagNumber(5)
  BinaryOutput get binaryOutput => $_getN(4);
  @$pb.TagNumber(5)
  set binaryOutput(BinaryOutput v) { setField(5, v); }
  @$pb.TagNumber(5)
  $core.bool hasBinaryOutput() => $_has(4);
  @$pb.TagNumber(5)
  void clearBinaryOutput() => clearField(5);
  @$pb.TagNumber(5)
  BinaryOutput ensureBinaryOutput() => $_ensure(4);
}

class OutputRef extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'OutputRef', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  OutputRef._() : super();
  factory OutputRef({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory OutputRef.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory OutputRef.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  OutputRef clone() => OutputRef()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  OutputRef copyWith(void Function(OutputRef) updates) => super.copyWith((message) => updates(message as OutputRef)) as OutputRef; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static OutputRef create() => OutputRef._();
  OutputRef createEmptyInstance() => create();
  static $pb.PbList<OutputRef> createRepeated() => $pb.PbList<OutputRef>();
  @$core.pragma('dart2js:noInline')
  static OutputRef getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<OutputRef>(create);
  static OutputRef? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

class BinaryOutput extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BinaryOutput', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'address')
    ..e<BinaryOutputType>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'outputType', $pb.PbFieldType.OE, defaultOrMaker: BinaryOutputType.BOT_DEFAULT, valueOf: BinaryOutputType.valueOf, enumValues: BinaryOutputType.values)
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'activeText')
    ..aOS(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'inactiveText')
    ..hasRequiredFields = false
  ;

  BinaryOutput._() : super();
  factory BinaryOutput({
    $core.String? address,
    BinaryOutputType? outputType,
    $core.String? activeText,
    $core.String? inactiveText,
  }) {
    final _result = create();
    if (address != null) {
      _result.address = address;
    }
    if (outputType != null) {
      _result.outputType = outputType;
    }
    if (activeText != null) {
      _result.activeText = activeText;
    }
    if (inactiveText != null) {
      _result.inactiveText = inactiveText;
    }
    return _result;
  }
  factory BinaryOutput.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BinaryOutput.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BinaryOutput clone() => BinaryOutput()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BinaryOutput copyWith(void Function(BinaryOutput) updates) => super.copyWith((message) => updates(message as BinaryOutput)) as BinaryOutput; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BinaryOutput create() => BinaryOutput._();
  BinaryOutput createEmptyInstance() => create();
  static $pb.PbList<BinaryOutput> createRepeated() => $pb.PbList<BinaryOutput>();
  @$core.pragma('dart2js:noInline')
  static BinaryOutput getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BinaryOutput>(create);
  static BinaryOutput? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get address => $_getSZ(0);
  @$pb.TagNumber(1)
  set address($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasAddress() => $_has(0);
  @$pb.TagNumber(1)
  void clearAddress() => clearField(1);

  @$pb.TagNumber(2)
  BinaryOutputType get outputType => $_getN(1);
  @$pb.TagNumber(2)
  set outputType(BinaryOutputType v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasOutputType() => $_has(1);
  @$pb.TagNumber(2)
  void clearOutputType() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get activeText => $_getSZ(2);
  @$pb.TagNumber(3)
  set activeText($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasActiveText() => $_has(2);
  @$pb.TagNumber(3)
  void clearActiveText() => clearField(3);

  @$pb.TagNumber(4)
  $core.String get inactiveText => $_getSZ(3);
  @$pb.TagNumber(4)
  set inactiveText($core.String v) { $_setString(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasInactiveText() => $_has(3);
  @$pb.TagNumber(4)
  void clearInactiveText() => clearField(4);
}

class Route extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Route', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'moduleId')
    ..aOM<Endpoint>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'from', subBuilder: Endpoint.create)
    ..aOM<Endpoint>(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'to', subBuilder: Endpoint.create)
    ..pc<JunctionWithState>(6, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'crossingJunctions', $pb.PbFieldType.PM, subBuilder: JunctionWithState.create)
    ..pc<OutputWithState>(7, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'outputs', $pb.PbFieldType.PM, subBuilder: OutputWithState.create)
    ..pc<RouteEvent>(8, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'events', $pb.PbFieldType.PM, subBuilder: RouteEvent.create)
    ..a<$core.int>(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'speed', $pb.PbFieldType.O3)
    ..a<$core.int>(11, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'chooseProbability', $pb.PbFieldType.O3)
    ..aOS(12, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'permissions')
    ..aOB(13, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'closed')
    ..a<$core.int>(14, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'maxDuration', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  Route._() : super();
  factory Route({
    $core.String? id,
    $core.String? description,
    $core.String? moduleId,
    Endpoint? from,
    Endpoint? to,
    $core.Iterable<JunctionWithState>? crossingJunctions,
    $core.Iterable<OutputWithState>? outputs,
    $core.Iterable<RouteEvent>? events,
    $core.int? speed,
    $core.int? chooseProbability,
    $core.String? permissions,
    $core.bool? closed,
    $core.int? maxDuration,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (moduleId != null) {
      _result.moduleId = moduleId;
    }
    if (from != null) {
      _result.from = from;
    }
    if (to != null) {
      _result.to = to;
    }
    if (crossingJunctions != null) {
      _result.crossingJunctions.addAll(crossingJunctions);
    }
    if (outputs != null) {
      _result.outputs.addAll(outputs);
    }
    if (events != null) {
      _result.events.addAll(events);
    }
    if (speed != null) {
      _result.speed = speed;
    }
    if (chooseProbability != null) {
      _result.chooseProbability = chooseProbability;
    }
    if (permissions != null) {
      _result.permissions = permissions;
    }
    if (closed != null) {
      _result.closed = closed;
    }
    if (maxDuration != null) {
      _result.maxDuration = maxDuration;
    }
    return _result;
  }
  factory Route.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Route.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Route clone() => Route()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Route copyWith(void Function(Route) updates) => super.copyWith((message) => updates(message as Route)) as Route; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Route create() => Route._();
  Route createEmptyInstance() => create();
  static $pb.PbList<Route> createRepeated() => $pb.PbList<Route>();
  @$core.pragma('dart2js:noInline')
  static Route getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Route>(create);
  static Route? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get moduleId => $_getSZ(2);
  @$pb.TagNumber(3)
  set moduleId($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasModuleId() => $_has(2);
  @$pb.TagNumber(3)
  void clearModuleId() => clearField(3);

  @$pb.TagNumber(4)
  Endpoint get from => $_getN(3);
  @$pb.TagNumber(4)
  set from(Endpoint v) { setField(4, v); }
  @$pb.TagNumber(4)
  $core.bool hasFrom() => $_has(3);
  @$pb.TagNumber(4)
  void clearFrom() => clearField(4);
  @$pb.TagNumber(4)
  Endpoint ensureFrom() => $_ensure(3);

  @$pb.TagNumber(5)
  Endpoint get to => $_getN(4);
  @$pb.TagNumber(5)
  set to(Endpoint v) { setField(5, v); }
  @$pb.TagNumber(5)
  $core.bool hasTo() => $_has(4);
  @$pb.TagNumber(5)
  void clearTo() => clearField(5);
  @$pb.TagNumber(5)
  Endpoint ensureTo() => $_ensure(4);

  @$pb.TagNumber(6)
  $core.List<JunctionWithState> get crossingJunctions => $_getList(5);

  @$pb.TagNumber(7)
  $core.List<OutputWithState> get outputs => $_getList(6);

  @$pb.TagNumber(8)
  $core.List<RouteEvent> get events => $_getList(7);

  @$pb.TagNumber(10)
  $core.int get speed => $_getIZ(8);
  @$pb.TagNumber(10)
  set speed($core.int v) { $_setSignedInt32(8, v); }
  @$pb.TagNumber(10)
  $core.bool hasSpeed() => $_has(8);
  @$pb.TagNumber(10)
  void clearSpeed() => clearField(10);

  @$pb.TagNumber(11)
  $core.int get chooseProbability => $_getIZ(9);
  @$pb.TagNumber(11)
  set chooseProbability($core.int v) { $_setSignedInt32(9, v); }
  @$pb.TagNumber(11)
  $core.bool hasChooseProbability() => $_has(9);
  @$pb.TagNumber(11)
  void clearChooseProbability() => clearField(11);

  @$pb.TagNumber(12)
  $core.String get permissions => $_getSZ(10);
  @$pb.TagNumber(12)
  set permissions($core.String v) { $_setString(10, v); }
  @$pb.TagNumber(12)
  $core.bool hasPermissions() => $_has(10);
  @$pb.TagNumber(12)
  void clearPermissions() => clearField(12);

  @$pb.TagNumber(13)
  $core.bool get closed => $_getBF(11);
  @$pb.TagNumber(13)
  set closed($core.bool v) { $_setBool(11, v); }
  @$pb.TagNumber(13)
  $core.bool hasClosed() => $_has(11);
  @$pb.TagNumber(13)
  void clearClosed() => clearField(13);

  @$pb.TagNumber(14)
  $core.int get maxDuration => $_getIZ(12);
  @$pb.TagNumber(14)
  set maxDuration($core.int v) { $_setSignedInt32(12, v); }
  @$pb.TagNumber(14)
  $core.bool hasMaxDuration() => $_has(12);
  @$pb.TagNumber(14)
  void clearMaxDuration() => clearField(14);
}

class RouteRef extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'RouteRef', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  RouteRef._() : super();
  factory RouteRef({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory RouteRef.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory RouteRef.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  RouteRef clone() => RouteRef()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  RouteRef copyWith(void Function(RouteRef) updates) => super.copyWith((message) => updates(message as RouteRef)) as RouteRef; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static RouteRef create() => RouteRef._();
  RouteRef createEmptyInstance() => create();
  static $pb.PbList<RouteRef> createRepeated() => $pb.PbList<RouteRef>();
  @$core.pragma('dart2js:noInline')
  static RouteRef getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<RouteRef>(create);
  static RouteRef? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

class Endpoint extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Endpoint', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<BlockRef>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'block', subBuilder: BlockRef.create)
    ..aOM<EdgeRef>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'edge', subBuilder: EdgeRef.create)
    ..e<BlockSide>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'blockSide', $pb.PbFieldType.OE, defaultOrMaker: BlockSide.FRONT, valueOf: BlockSide.valueOf, enumValues: BlockSide.values)
    ..hasRequiredFields = false
  ;

  Endpoint._() : super();
  factory Endpoint({
    BlockRef? block,
    EdgeRef? edge,
    BlockSide? blockSide,
  }) {
    final _result = create();
    if (block != null) {
      _result.block = block;
    }
    if (edge != null) {
      _result.edge = edge;
    }
    if (blockSide != null) {
      _result.blockSide = blockSide;
    }
    return _result;
  }
  factory Endpoint.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Endpoint.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Endpoint clone() => Endpoint()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Endpoint copyWith(void Function(Endpoint) updates) => super.copyWith((message) => updates(message as Endpoint)) as Endpoint; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Endpoint create() => Endpoint._();
  Endpoint createEmptyInstance() => create();
  static $pb.PbList<Endpoint> createRepeated() => $pb.PbList<Endpoint>();
  @$core.pragma('dart2js:noInline')
  static Endpoint getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Endpoint>(create);
  static Endpoint? _defaultInstance;

  @$pb.TagNumber(1)
  BlockRef get block => $_getN(0);
  @$pb.TagNumber(1)
  set block(BlockRef v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasBlock() => $_has(0);
  @$pb.TagNumber(1)
  void clearBlock() => clearField(1);
  @$pb.TagNumber(1)
  BlockRef ensureBlock() => $_ensure(0);

  @$pb.TagNumber(2)
  EdgeRef get edge => $_getN(1);
  @$pb.TagNumber(2)
  set edge(EdgeRef v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasEdge() => $_has(1);
  @$pb.TagNumber(2)
  void clearEdge() => clearField(2);
  @$pb.TagNumber(2)
  EdgeRef ensureEdge() => $_ensure(1);

  @$pb.TagNumber(3)
  BlockSide get blockSide => $_getN(2);
  @$pb.TagNumber(3)
  set blockSide(BlockSide v) { setField(3, v); }
  @$pb.TagNumber(3)
  $core.bool hasBlockSide() => $_has(2);
  @$pb.TagNumber(3)
  void clearBlockSide() => clearField(3);
}

class JunctionWithState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'JunctionWithState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<JunctionRef>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'junction', subBuilder: JunctionRef.create)
    ..aOM<SwitchWithState>(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'switchState', subBuilder: SwitchWithState.create)
    ..hasRequiredFields = false
  ;

  JunctionWithState._() : super();
  factory JunctionWithState({
    JunctionRef? junction,
    SwitchWithState? switchState,
  }) {
    final _result = create();
    if (junction != null) {
      _result.junction = junction;
    }
    if (switchState != null) {
      _result.switchState = switchState;
    }
    return _result;
  }
  factory JunctionWithState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory JunctionWithState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  JunctionWithState clone() => JunctionWithState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  JunctionWithState copyWith(void Function(JunctionWithState) updates) => super.copyWith((message) => updates(message as JunctionWithState)) as JunctionWithState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static JunctionWithState create() => JunctionWithState._();
  JunctionWithState createEmptyInstance() => create();
  static $pb.PbList<JunctionWithState> createRepeated() => $pb.PbList<JunctionWithState>();
  @$core.pragma('dart2js:noInline')
  static JunctionWithState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<JunctionWithState>(create);
  static JunctionWithState? _defaultInstance;

  @$pb.TagNumber(1)
  JunctionRef get junction => $_getN(0);
  @$pb.TagNumber(1)
  set junction(JunctionRef v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasJunction() => $_has(0);
  @$pb.TagNumber(1)
  void clearJunction() => clearField(1);
  @$pb.TagNumber(1)
  JunctionRef ensureJunction() => $_ensure(0);

  @$pb.TagNumber(10)
  SwitchWithState get switchState => $_getN(1);
  @$pb.TagNumber(10)
  set switchState(SwitchWithState v) { setField(10, v); }
  @$pb.TagNumber(10)
  $core.bool hasSwitchState() => $_has(1);
  @$pb.TagNumber(10)
  void clearSwitchState() => clearField(10);
  @$pb.TagNumber(10)
  SwitchWithState ensureSwitchState() => $_ensure(1);
}

class SwitchWithState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SwitchWithState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..e<SwitchDirection>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'direction', $pb.PbFieldType.OE, defaultOrMaker: SwitchDirection.STRAIGHT, valueOf: SwitchDirection.valueOf, enumValues: SwitchDirection.values)
    ..hasRequiredFields = false
  ;

  SwitchWithState._() : super();
  factory SwitchWithState({
    SwitchDirection? direction,
  }) {
    final _result = create();
    if (direction != null) {
      _result.direction = direction;
    }
    return _result;
  }
  factory SwitchWithState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SwitchWithState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SwitchWithState clone() => SwitchWithState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SwitchWithState copyWith(void Function(SwitchWithState) updates) => super.copyWith((message) => updates(message as SwitchWithState)) as SwitchWithState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SwitchWithState create() => SwitchWithState._();
  SwitchWithState createEmptyInstance() => create();
  static $pb.PbList<SwitchWithState> createRepeated() => $pb.PbList<SwitchWithState>();
  @$core.pragma('dart2js:noInline')
  static SwitchWithState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SwitchWithState>(create);
  static SwitchWithState? _defaultInstance;

  @$pb.TagNumber(1)
  SwitchDirection get direction => $_getN(0);
  @$pb.TagNumber(1)
  set direction(SwitchDirection v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasDirection() => $_has(0);
  @$pb.TagNumber(1)
  void clearDirection() => clearField(1);
}

class OutputWithState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'OutputWithState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<OutputRef>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'output', subBuilder: OutputRef.create)
    ..aOM<BinaryOutputWithState>(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'binaryOutputState', subBuilder: BinaryOutputWithState.create)
    ..hasRequiredFields = false
  ;

  OutputWithState._() : super();
  factory OutputWithState({
    OutputRef? output,
    BinaryOutputWithState? binaryOutputState,
  }) {
    final _result = create();
    if (output != null) {
      _result.output = output;
    }
    if (binaryOutputState != null) {
      _result.binaryOutputState = binaryOutputState;
    }
    return _result;
  }
  factory OutputWithState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory OutputWithState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  OutputWithState clone() => OutputWithState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  OutputWithState copyWith(void Function(OutputWithState) updates) => super.copyWith((message) => updates(message as OutputWithState)) as OutputWithState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static OutputWithState create() => OutputWithState._();
  OutputWithState createEmptyInstance() => create();
  static $pb.PbList<OutputWithState> createRepeated() => $pb.PbList<OutputWithState>();
  @$core.pragma('dart2js:noInline')
  static OutputWithState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<OutputWithState>(create);
  static OutputWithState? _defaultInstance;

  @$pb.TagNumber(1)
  OutputRef get output => $_getN(0);
  @$pb.TagNumber(1)
  set output(OutputRef v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasOutput() => $_has(0);
  @$pb.TagNumber(1)
  void clearOutput() => clearField(1);
  @$pb.TagNumber(1)
  OutputRef ensureOutput() => $_ensure(0);

  @$pb.TagNumber(10)
  BinaryOutputWithState get binaryOutputState => $_getN(1);
  @$pb.TagNumber(10)
  set binaryOutputState(BinaryOutputWithState v) { setField(10, v); }
  @$pb.TagNumber(10)
  $core.bool hasBinaryOutputState() => $_has(1);
  @$pb.TagNumber(10)
  void clearBinaryOutputState() => clearField(10);
  @$pb.TagNumber(10)
  BinaryOutputWithState ensureBinaryOutputState() => $_ensure(1);
}

class BinaryOutputWithState extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BinaryOutputWithState', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOB(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'active')
    ..hasRequiredFields = false
  ;

  BinaryOutputWithState._() : super();
  factory BinaryOutputWithState({
    $core.bool? active,
  }) {
    final _result = create();
    if (active != null) {
      _result.active = active;
    }
    return _result;
  }
  factory BinaryOutputWithState.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BinaryOutputWithState.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BinaryOutputWithState clone() => BinaryOutputWithState()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BinaryOutputWithState copyWith(void Function(BinaryOutputWithState) updates) => super.copyWith((message) => updates(message as BinaryOutputWithState)) as BinaryOutputWithState; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BinaryOutputWithState create() => BinaryOutputWithState._();
  BinaryOutputWithState createEmptyInstance() => create();
  static $pb.PbList<BinaryOutputWithState> createRepeated() => $pb.PbList<BinaryOutputWithState>();
  @$core.pragma('dart2js:noInline')
  static BinaryOutputWithState getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BinaryOutputWithState>(create);
  static BinaryOutputWithState? _defaultInstance;

  @$pb.TagNumber(1)
  $core.bool get active => $_getBF(0);
  @$pb.TagNumber(1)
  set active($core.bool v) { $_setBool(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasActive() => $_has(0);
  @$pb.TagNumber(1)
  void clearActive() => clearField(1);
}

class RouteEvent extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'RouteEvent', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOM<SensorRef>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'sensor', subBuilder: SensorRef.create)
    ..pc<RouteEventBehavior>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'behaviors', $pb.PbFieldType.PM, subBuilder: RouteEventBehavior.create)
    ..hasRequiredFields = false
  ;

  RouteEvent._() : super();
  factory RouteEvent({
    SensorRef? sensor,
    $core.Iterable<RouteEventBehavior>? behaviors,
  }) {
    final _result = create();
    if (sensor != null) {
      _result.sensor = sensor;
    }
    if (behaviors != null) {
      _result.behaviors.addAll(behaviors);
    }
    return _result;
  }
  factory RouteEvent.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory RouteEvent.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  RouteEvent clone() => RouteEvent()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  RouteEvent copyWith(void Function(RouteEvent) updates) => super.copyWith((message) => updates(message as RouteEvent)) as RouteEvent; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static RouteEvent create() => RouteEvent._();
  RouteEvent createEmptyInstance() => create();
  static $pb.PbList<RouteEvent> createRepeated() => $pb.PbList<RouteEvent>();
  @$core.pragma('dart2js:noInline')
  static RouteEvent getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<RouteEvent>(create);
  static RouteEvent? _defaultInstance;

  @$pb.TagNumber(1)
  SensorRef get sensor => $_getN(0);
  @$pb.TagNumber(1)
  set sensor(SensorRef v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasSensor() => $_has(0);
  @$pb.TagNumber(1)
  void clearSensor() => clearField(1);
  @$pb.TagNumber(1)
  SensorRef ensureSensor() => $_ensure(0);

  @$pb.TagNumber(2)
  $core.List<RouteEventBehavior> get behaviors => $_getList(1);
}

class RouteEventBehavior extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'RouteEventBehavior', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'appliesTo')
    ..e<RouteStateBehavior>(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'stateBehavior', $pb.PbFieldType.OE, defaultOrMaker: RouteStateBehavior.RSB_NOCHANGE, valueOf: RouteStateBehavior.valueOf, enumValues: RouteStateBehavior.values)
    ..e<LocSpeedBehavior>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'speedBehavior', $pb.PbFieldType.OE, defaultOrMaker: LocSpeedBehavior.LSB_DEFAULT, valueOf: LocSpeedBehavior.valueOf, enumValues: LocSpeedBehavior.values)
    ..hasRequiredFields = false
  ;

  RouteEventBehavior._() : super();
  factory RouteEventBehavior({
    $core.String? appliesTo,
    RouteStateBehavior? stateBehavior,
    LocSpeedBehavior? speedBehavior,
  }) {
    final _result = create();
    if (appliesTo != null) {
      _result.appliesTo = appliesTo;
    }
    if (stateBehavior != null) {
      _result.stateBehavior = stateBehavior;
    }
    if (speedBehavior != null) {
      _result.speedBehavior = speedBehavior;
    }
    return _result;
  }
  factory RouteEventBehavior.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory RouteEventBehavior.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  RouteEventBehavior clone() => RouteEventBehavior()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  RouteEventBehavior copyWith(void Function(RouteEventBehavior) updates) => super.copyWith((message) => updates(message as RouteEventBehavior)) as RouteEventBehavior; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static RouteEventBehavior create() => RouteEventBehavior._();
  RouteEventBehavior createEmptyInstance() => create();
  static $pb.PbList<RouteEventBehavior> createRepeated() => $pb.PbList<RouteEventBehavior>();
  @$core.pragma('dart2js:noInline')
  static RouteEventBehavior getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<RouteEventBehavior>(create);
  static RouteEventBehavior? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get appliesTo => $_getSZ(0);
  @$pb.TagNumber(1)
  set appliesTo($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasAppliesTo() => $_has(0);
  @$pb.TagNumber(1)
  void clearAppliesTo() => clearField(1);

  @$pb.TagNumber(2)
  RouteStateBehavior get stateBehavior => $_getN(1);
  @$pb.TagNumber(2)
  set stateBehavior(RouteStateBehavior v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasStateBehavior() => $_has(1);
  @$pb.TagNumber(2)
  void clearStateBehavior() => clearField(2);

  @$pb.TagNumber(3)
  LocSpeedBehavior get speedBehavior => $_getN(2);
  @$pb.TagNumber(3)
  set speedBehavior(LocSpeedBehavior v) { setField(3, v); }
  @$pb.TagNumber(3)
  $core.bool hasSpeedBehavior() => $_has(2);
  @$pb.TagNumber(3)
  void clearSpeedBehavior() => clearField(3);
}

class Sensor extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Sensor', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'moduleId')
    ..aOM<Position>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'position', subBuilder: Position.create)
    ..aOS(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'address')
    ..aOM<BlockRef>(6, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'block', subBuilder: BlockRef.create)
    ..e<Shape>(7, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'shape', $pb.PbFieldType.OE, defaultOrMaker: Shape.CIRCLE, valueOf: Shape.valueOf, enumValues: Shape.values)
    ..aOM<BinarySensor>(8, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'binarySensor', subBuilder: BinarySensor.create)
    ..hasRequiredFields = false
  ;

  Sensor._() : super();
  factory Sensor({
    $core.String? id,
    $core.String? description,
    $core.String? moduleId,
    Position? position,
    $core.String? address,
    BlockRef? block,
    Shape? shape,
    BinarySensor? binarySensor,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (moduleId != null) {
      _result.moduleId = moduleId;
    }
    if (position != null) {
      _result.position = position;
    }
    if (address != null) {
      _result.address = address;
    }
    if (block != null) {
      _result.block = block;
    }
    if (shape != null) {
      _result.shape = shape;
    }
    if (binarySensor != null) {
      _result.binarySensor = binarySensor;
    }
    return _result;
  }
  factory Sensor.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Sensor.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Sensor clone() => Sensor()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Sensor copyWith(void Function(Sensor) updates) => super.copyWith((message) => updates(message as Sensor)) as Sensor; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Sensor create() => Sensor._();
  Sensor createEmptyInstance() => create();
  static $pb.PbList<Sensor> createRepeated() => $pb.PbList<Sensor>();
  @$core.pragma('dart2js:noInline')
  static Sensor getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Sensor>(create);
  static Sensor? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get moduleId => $_getSZ(2);
  @$pb.TagNumber(3)
  set moduleId($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasModuleId() => $_has(2);
  @$pb.TagNumber(3)
  void clearModuleId() => clearField(3);

  @$pb.TagNumber(4)
  Position get position => $_getN(3);
  @$pb.TagNumber(4)
  set position(Position v) { setField(4, v); }
  @$pb.TagNumber(4)
  $core.bool hasPosition() => $_has(3);
  @$pb.TagNumber(4)
  void clearPosition() => clearField(4);
  @$pb.TagNumber(4)
  Position ensurePosition() => $_ensure(3);

  @$pb.TagNumber(5)
  $core.String get address => $_getSZ(4);
  @$pb.TagNumber(5)
  set address($core.String v) { $_setString(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasAddress() => $_has(4);
  @$pb.TagNumber(5)
  void clearAddress() => clearField(5);

  @$pb.TagNumber(6)
  BlockRef get block => $_getN(5);
  @$pb.TagNumber(6)
  set block(BlockRef v) { setField(6, v); }
  @$pb.TagNumber(6)
  $core.bool hasBlock() => $_has(5);
  @$pb.TagNumber(6)
  void clearBlock() => clearField(6);
  @$pb.TagNumber(6)
  BlockRef ensureBlock() => $_ensure(5);

  @$pb.TagNumber(7)
  Shape get shape => $_getN(6);
  @$pb.TagNumber(7)
  set shape(Shape v) { setField(7, v); }
  @$pb.TagNumber(7)
  $core.bool hasShape() => $_has(6);
  @$pb.TagNumber(7)
  void clearShape() => clearField(7);

  @$pb.TagNumber(8)
  BinarySensor get binarySensor => $_getN(7);
  @$pb.TagNumber(8)
  set binarySensor(BinarySensor v) { setField(8, v); }
  @$pb.TagNumber(8)
  $core.bool hasBinarySensor() => $_has(7);
  @$pb.TagNumber(8)
  void clearBinarySensor() => clearField(8);
  @$pb.TagNumber(8)
  BinarySensor ensureBinarySensor() => $_ensure(7);
}

class SensorRef extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SensorRef', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  SensorRef._() : super();
  factory SensorRef({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory SensorRef.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SensorRef.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SensorRef clone() => SensorRef()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SensorRef copyWith(void Function(SensorRef) updates) => super.copyWith((message) => updates(message as SensorRef)) as SensorRef; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SensorRef create() => SensorRef._();
  SensorRef createEmptyInstance() => create();
  static $pb.PbList<SensorRef> createRepeated() => $pb.PbList<SensorRef>();
  @$core.pragma('dart2js:noInline')
  static SensorRef getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SensorRef>(create);
  static SensorRef? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

class BinarySensor extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BinarySensor', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..hasRequiredFields = false
  ;

  BinarySensor._() : super();
  factory BinarySensor() => create();
  factory BinarySensor.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BinarySensor.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BinarySensor clone() => BinarySensor()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BinarySensor copyWith(void Function(BinarySensor) updates) => super.copyWith((message) => updates(message as BinarySensor)) as BinarySensor; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BinarySensor create() => BinarySensor._();
  BinarySensor createEmptyInstance() => create();
  static $pb.PbList<BinarySensor> createRepeated() => $pb.PbList<BinarySensor>();
  @$core.pragma('dart2js:noInline')
  static BinarySensor getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BinarySensor>(create);
  static BinarySensor? _defaultInstance;
}

class Signal extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Signal', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'moduleId')
    ..aOM<Position>(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'position', subBuilder: Position.create)
    ..aOM<BlockSignal>(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'blockSignal', subBuilder: BlockSignal.create)
    ..hasRequiredFields = false
  ;

  Signal._() : super();
  factory Signal({
    $core.String? id,
    $core.String? description,
    $core.String? moduleId,
    Position? position,
    BlockSignal? blockSignal,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
    }
    if (moduleId != null) {
      _result.moduleId = moduleId;
    }
    if (position != null) {
      _result.position = position;
    }
    if (blockSignal != null) {
      _result.blockSignal = blockSignal;
    }
    return _result;
  }
  factory Signal.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Signal.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Signal clone() => Signal()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Signal copyWith(void Function(Signal) updates) => super.copyWith((message) => updates(message as Signal)) as Signal; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Signal create() => Signal._();
  Signal createEmptyInstance() => create();
  static $pb.PbList<Signal> createRepeated() => $pb.PbList<Signal>();
  @$core.pragma('dart2js:noInline')
  static Signal getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Signal>(create);
  static Signal? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get description => $_getSZ(1);
  @$pb.TagNumber(2)
  set description($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasDescription() => $_has(1);
  @$pb.TagNumber(2)
  void clearDescription() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get moduleId => $_getSZ(2);
  @$pb.TagNumber(3)
  set moduleId($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasModuleId() => $_has(2);
  @$pb.TagNumber(3)
  void clearModuleId() => clearField(3);

  @$pb.TagNumber(4)
  Position get position => $_getN(3);
  @$pb.TagNumber(4)
  set position(Position v) { setField(4, v); }
  @$pb.TagNumber(4)
  $core.bool hasPosition() => $_has(3);
  @$pb.TagNumber(4)
  void clearPosition() => clearField(4);
  @$pb.TagNumber(4)
  Position ensurePosition() => $_ensure(3);

  @$pb.TagNumber(5)
  BlockSignal get blockSignal => $_getN(4);
  @$pb.TagNumber(5)
  set blockSignal(BlockSignal v) { setField(5, v); }
  @$pb.TagNumber(5)
  $core.bool hasBlockSignal() => $_has(4);
  @$pb.TagNumber(5)
  void clearBlockSignal() => clearField(5);
  @$pb.TagNumber(5)
  BlockSignal ensureBlockSignal() => $_ensure(4);
}

class BlockSignal extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'BlockSignal', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'address1')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'address2')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'address3')
    ..aOS(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'address4')
    ..aOB(10, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isRedAvailable')
    ..a<$core.int>(11, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'redPattern', $pb.PbFieldType.O3)
    ..aOB(20, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isGreenAvailable')
    ..a<$core.int>(21, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'greenPattern', $pb.PbFieldType.O3)
    ..aOB(30, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isYellowAvailable')
    ..a<$core.int>(31, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'yellowPattern', $pb.PbFieldType.O3)
    ..aOB(40, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'isWhiteAvailable')
    ..a<$core.int>(41, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'whitePattern', $pb.PbFieldType.O3)
    ..aOM<BlockRef>(50, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'block', subBuilder: BlockRef.create)
    ..e<BlockSide>(51, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'blockSide', $pb.PbFieldType.OE, defaultOrMaker: BlockSide.FRONT, valueOf: BlockSide.valueOf, enumValues: BlockSide.values)
    ..e<BlockSignalType>(60, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'type', $pb.PbFieldType.OE, defaultOrMaker: BlockSignalType.ENTRY, valueOf: BlockSignalType.valueOf, enumValues: BlockSignalType.values)
    ..hasRequiredFields = false
  ;

  BlockSignal._() : super();
  factory BlockSignal({
    $core.String? address1,
    $core.String? address2,
    $core.String? address3,
    $core.String? address4,
    $core.bool? isRedAvailable,
    $core.int? redPattern,
    $core.bool? isGreenAvailable,
    $core.int? greenPattern,
    $core.bool? isYellowAvailable,
    $core.int? yellowPattern,
    $core.bool? isWhiteAvailable,
    $core.int? whitePattern,
    BlockRef? block,
    BlockSide? blockSide,
    BlockSignalType? type,
  }) {
    final _result = create();
    if (address1 != null) {
      _result.address1 = address1;
    }
    if (address2 != null) {
      _result.address2 = address2;
    }
    if (address3 != null) {
      _result.address3 = address3;
    }
    if (address4 != null) {
      _result.address4 = address4;
    }
    if (isRedAvailable != null) {
      _result.isRedAvailable = isRedAvailable;
    }
    if (redPattern != null) {
      _result.redPattern = redPattern;
    }
    if (isGreenAvailable != null) {
      _result.isGreenAvailable = isGreenAvailable;
    }
    if (greenPattern != null) {
      _result.greenPattern = greenPattern;
    }
    if (isYellowAvailable != null) {
      _result.isYellowAvailable = isYellowAvailable;
    }
    if (yellowPattern != null) {
      _result.yellowPattern = yellowPattern;
    }
    if (isWhiteAvailable != null) {
      _result.isWhiteAvailable = isWhiteAvailable;
    }
    if (whitePattern != null) {
      _result.whitePattern = whitePattern;
    }
    if (block != null) {
      _result.block = block;
    }
    if (blockSide != null) {
      _result.blockSide = blockSide;
    }
    if (type != null) {
      _result.type = type;
    }
    return _result;
  }
  factory BlockSignal.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BlockSignal.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BlockSignal clone() => BlockSignal()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BlockSignal copyWith(void Function(BlockSignal) updates) => super.copyWith((message) => updates(message as BlockSignal)) as BlockSignal; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BlockSignal create() => BlockSignal._();
  BlockSignal createEmptyInstance() => create();
  static $pb.PbList<BlockSignal> createRepeated() => $pb.PbList<BlockSignal>();
  @$core.pragma('dart2js:noInline')
  static BlockSignal getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BlockSignal>(create);
  static BlockSignal? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get address1 => $_getSZ(0);
  @$pb.TagNumber(1)
  set address1($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasAddress1() => $_has(0);
  @$pb.TagNumber(1)
  void clearAddress1() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get address2 => $_getSZ(1);
  @$pb.TagNumber(2)
  set address2($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasAddress2() => $_has(1);
  @$pb.TagNumber(2)
  void clearAddress2() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get address3 => $_getSZ(2);
  @$pb.TagNumber(3)
  set address3($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasAddress3() => $_has(2);
  @$pb.TagNumber(3)
  void clearAddress3() => clearField(3);

  @$pb.TagNumber(4)
  $core.String get address4 => $_getSZ(3);
  @$pb.TagNumber(4)
  set address4($core.String v) { $_setString(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasAddress4() => $_has(3);
  @$pb.TagNumber(4)
  void clearAddress4() => clearField(4);

  @$pb.TagNumber(10)
  $core.bool get isRedAvailable => $_getBF(4);
  @$pb.TagNumber(10)
  set isRedAvailable($core.bool v) { $_setBool(4, v); }
  @$pb.TagNumber(10)
  $core.bool hasIsRedAvailable() => $_has(4);
  @$pb.TagNumber(10)
  void clearIsRedAvailable() => clearField(10);

  @$pb.TagNumber(11)
  $core.int get redPattern => $_getIZ(5);
  @$pb.TagNumber(11)
  set redPattern($core.int v) { $_setSignedInt32(5, v); }
  @$pb.TagNumber(11)
  $core.bool hasRedPattern() => $_has(5);
  @$pb.TagNumber(11)
  void clearRedPattern() => clearField(11);

  @$pb.TagNumber(20)
  $core.bool get isGreenAvailable => $_getBF(6);
  @$pb.TagNumber(20)
  set isGreenAvailable($core.bool v) { $_setBool(6, v); }
  @$pb.TagNumber(20)
  $core.bool hasIsGreenAvailable() => $_has(6);
  @$pb.TagNumber(20)
  void clearIsGreenAvailable() => clearField(20);

  @$pb.TagNumber(21)
  $core.int get greenPattern => $_getIZ(7);
  @$pb.TagNumber(21)
  set greenPattern($core.int v) { $_setSignedInt32(7, v); }
  @$pb.TagNumber(21)
  $core.bool hasGreenPattern() => $_has(7);
  @$pb.TagNumber(21)
  void clearGreenPattern() => clearField(21);

  @$pb.TagNumber(30)
  $core.bool get isYellowAvailable => $_getBF(8);
  @$pb.TagNumber(30)
  set isYellowAvailable($core.bool v) { $_setBool(8, v); }
  @$pb.TagNumber(30)
  $core.bool hasIsYellowAvailable() => $_has(8);
  @$pb.TagNumber(30)
  void clearIsYellowAvailable() => clearField(30);

  @$pb.TagNumber(31)
  $core.int get yellowPattern => $_getIZ(9);
  @$pb.TagNumber(31)
  set yellowPattern($core.int v) { $_setSignedInt32(9, v); }
  @$pb.TagNumber(31)
  $core.bool hasYellowPattern() => $_has(9);
  @$pb.TagNumber(31)
  void clearYellowPattern() => clearField(31);

  @$pb.TagNumber(40)
  $core.bool get isWhiteAvailable => $_getBF(10);
  @$pb.TagNumber(40)
  set isWhiteAvailable($core.bool v) { $_setBool(10, v); }
  @$pb.TagNumber(40)
  $core.bool hasIsWhiteAvailable() => $_has(10);
  @$pb.TagNumber(40)
  void clearIsWhiteAvailable() => clearField(40);

  @$pb.TagNumber(41)
  $core.int get whitePattern => $_getIZ(11);
  @$pb.TagNumber(41)
  set whitePattern($core.int v) { $_setSignedInt32(11, v); }
  @$pb.TagNumber(41)
  $core.bool hasWhitePattern() => $_has(11);
  @$pb.TagNumber(41)
  void clearWhitePattern() => clearField(41);

  @$pb.TagNumber(50)
  BlockRef get block => $_getN(12);
  @$pb.TagNumber(50)
  set block(BlockRef v) { setField(50, v); }
  @$pb.TagNumber(50)
  $core.bool hasBlock() => $_has(12);
  @$pb.TagNumber(50)
  void clearBlock() => clearField(50);
  @$pb.TagNumber(50)
  BlockRef ensureBlock() => $_ensure(12);

  @$pb.TagNumber(51)
  BlockSide get blockSide => $_getN(13);
  @$pb.TagNumber(51)
  set blockSide(BlockSide v) { setField(51, v); }
  @$pb.TagNumber(51)
  $core.bool hasBlockSide() => $_has(13);
  @$pb.TagNumber(51)
  void clearBlockSide() => clearField(51);

  @$pb.TagNumber(60)
  BlockSignalType get type => $_getN(14);
  @$pb.TagNumber(60)
  set type(BlockSignalType v) { setField(60, v); }
  @$pb.TagNumber(60)
  $core.bool hasType() => $_has(14);
  @$pb.TagNumber(60)
  void clearType() => clearField(60);
}

class SignalRef extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'SignalRef', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  SignalRef._() : super();
  factory SignalRef({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory SignalRef.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SignalRef.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SignalRef clone() => SignalRef()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SignalRef copyWith(void Function(SignalRef) updates) => super.copyWith((message) => updates(message as SignalRef)) as SignalRef; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static SignalRef create() => SignalRef._();
  SignalRef createEmptyInstance() => create();
  static $pb.PbList<SignalRef> createRepeated() => $pb.PbList<SignalRef>();
  @$core.pragma('dart2js:noInline')
  static SignalRef getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SignalRef>(create);
  static SignalRef? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

