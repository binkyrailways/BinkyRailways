///
//  Generated code. Do not modify.
//  source: br_model_types.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields

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

class Railway extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Railway', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..aOB(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'dirty')
    ..pc<ModuleRef>(100, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'modules', $pb.PbFieldType.PM, subBuilder: ModuleRef.create)
    ..pc<LocRef>(101, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'locs', $pb.PbFieldType.PM, subBuilder: LocRef.create)
    ..hasRequiredFields = false
  ;

  Railway._() : super();
  factory Railway({
    $core.String? id,
    $core.String? description,
    $core.bool? dirty,
    $core.Iterable<ModuleRef>? modules,
    $core.Iterable<LocRef>? locs,
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
}

class Module extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Module', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..hasRequiredFields = false
  ;

  Module._() : super();
  factory Module({
    $core.String? id,
    $core.String? description,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    if (description != null) {
      _result.description = description;
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

class Address extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Address', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..e<AddressType>(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'type', $pb.PbFieldType.OE, defaultOrMaker: AddressType.BINKYNET, valueOf: AddressType.valueOf, enumValues: AddressType.values)
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'space')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'value')
    ..hasRequiredFields = false
  ;

  Address._() : super();
  factory Address({
    AddressType? type,
    $core.String? space,
    $core.String? value,
  }) {
    final _result = create();
    if (type != null) {
      _result.type = type;
    }
    if (space != null) {
      _result.space = space;
    }
    if (value != null) {
      _result.value = value;
    }
    return _result;
  }
  factory Address.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Address.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Address clone() => Address()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Address copyWith(void Function(Address) updates) => super.copyWith((message) => updates(message as Address)) as Address; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Address create() => Address._();
  Address createEmptyInstance() => create();
  static $pb.PbList<Address> createRepeated() => $pb.PbList<Address>();
  @$core.pragma('dart2js:noInline')
  static Address getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Address>(create);
  static Address? _defaultInstance;

  @$pb.TagNumber(1)
  AddressType get type => $_getN(0);
  @$pb.TagNumber(1)
  set type(AddressType v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasType() => $_has(0);
  @$pb.TagNumber(1)
  void clearType() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get space => $_getSZ(1);
  @$pb.TagNumber(2)
  set space($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasSpace() => $_has(1);
  @$pb.TagNumber(2)
  void clearSpace() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get value => $_getSZ(2);
  @$pb.TagNumber(3)
  set value($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasValue() => $_has(2);
  @$pb.TagNumber(3)
  void clearValue() => clearField(3);
}

class Loc extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'Loc', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'description')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'owner')
    ..aOS(4, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'remarks')
    ..aOM<Address>(5, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'address', subBuilder: Address.create)
    ..a<$core.int>(100, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'slowSpeed', $pb.PbFieldType.O3)
    ..a<$core.int>(101, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'mediumSpeed', $pb.PbFieldType.O3)
    ..a<$core.int>(102, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'maximumSpeed', $pb.PbFieldType.O3)
    ..a<$core.int>(110, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'speedSteps', $pb.PbFieldType.O3)
    ..e<ChangeDirection>(120, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'changeDirection', $pb.PbFieldType.OE, defaultOrMaker: ChangeDirection.ALLOW, valueOf: ChangeDirection.valueOf, enumValues: ChangeDirection.values)
    ..hasRequiredFields = false
  ;

  Loc._() : super();
  factory Loc({
    $core.String? id,
    $core.String? description,
    $core.String? owner,
    $core.String? remarks,
    Address? address,
    $core.int? slowSpeed,
    $core.int? mediumSpeed,
    $core.int? maximumSpeed,
    $core.int? speedSteps,
    ChangeDirection? changeDirection,
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
  Address get address => $_getN(4);
  @$pb.TagNumber(5)
  set address(Address v) { setField(5, v); }
  @$pb.TagNumber(5)
  $core.bool hasAddress() => $_has(4);
  @$pb.TagNumber(5)
  void clearAddress() => clearField(5);
  @$pb.TagNumber(5)
  Address ensureAddress() => $_ensure(4);

  @$pb.TagNumber(100)
  $core.int get slowSpeed => $_getIZ(5);
  @$pb.TagNumber(100)
  set slowSpeed($core.int v) { $_setSignedInt32(5, v); }
  @$pb.TagNumber(100)
  $core.bool hasSlowSpeed() => $_has(5);
  @$pb.TagNumber(100)
  void clearSlowSpeed() => clearField(100);

  @$pb.TagNumber(101)
  $core.int get mediumSpeed => $_getIZ(6);
  @$pb.TagNumber(101)
  set mediumSpeed($core.int v) { $_setSignedInt32(6, v); }
  @$pb.TagNumber(101)
  $core.bool hasMediumSpeed() => $_has(6);
  @$pb.TagNumber(101)
  void clearMediumSpeed() => clearField(101);

  @$pb.TagNumber(102)
  $core.int get maximumSpeed => $_getIZ(7);
  @$pb.TagNumber(102)
  set maximumSpeed($core.int v) { $_setSignedInt32(7, v); }
  @$pb.TagNumber(102)
  $core.bool hasMaximumSpeed() => $_has(7);
  @$pb.TagNumber(102)
  void clearMaximumSpeed() => clearField(102);

  @$pb.TagNumber(110)
  $core.int get speedSteps => $_getIZ(8);
  @$pb.TagNumber(110)
  set speedSteps($core.int v) { $_setSignedInt32(8, v); }
  @$pb.TagNumber(110)
  $core.bool hasSpeedSteps() => $_has(8);
  @$pb.TagNumber(110)
  void clearSpeedSteps() => clearField(110);

  @$pb.TagNumber(120)
  ChangeDirection get changeDirection => $_getN(9);
  @$pb.TagNumber(120)
  set changeDirection(ChangeDirection v) { setField(120, v); }
  @$pb.TagNumber(120)
  $core.bool hasChangeDirection() => $_has(9);
  @$pb.TagNumber(120)
  void clearChangeDirection() => clearField(120);
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
