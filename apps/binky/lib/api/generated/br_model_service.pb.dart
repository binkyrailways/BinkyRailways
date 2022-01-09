///
//  Generated code. Do not modify.
//  source: br_model_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields

import 'dart:core' as $core;

import 'package:protobuf/protobuf.dart' as $pb;

import 'br_model_types.pbenum.dart' as $1;

class IDRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'IDRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'id')
    ..hasRequiredFields = false
  ;

  IDRequest._() : super();
  factory IDRequest({
    $core.String? id,
  }) {
    final _result = create();
    if (id != null) {
      _result.id = id;
    }
    return _result;
  }
  factory IDRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory IDRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  IDRequest clone() => IDRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  IDRequest copyWith(void Function(IDRequest) updates) => super.copyWith((message) => updates(message as IDRequest)) as IDRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static IDRequest create() => IDRequest._();
  IDRequest createEmptyInstance() => create();
  static $pb.PbList<IDRequest> createRepeated() => $pb.PbList<IDRequest>();
  @$core.pragma('dart2js:noInline')
  static IDRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<IDRequest>(create);
  static IDRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);
}

class ParseAddressRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'ParseAddressRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'value')
    ..hasRequiredFields = false
  ;

  ParseAddressRequest._() : super();
  factory ParseAddressRequest({
    $core.String? value,
  }) {
    final _result = create();
    if (value != null) {
      _result.value = value;
    }
    return _result;
  }
  factory ParseAddressRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory ParseAddressRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  ParseAddressRequest clone() => ParseAddressRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  ParseAddressRequest copyWith(void Function(ParseAddressRequest) updates) => super.copyWith((message) => updates(message as ParseAddressRequest)) as ParseAddressRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static ParseAddressRequest create() => ParseAddressRequest._();
  ParseAddressRequest createEmptyInstance() => create();
  static $pb.PbList<ParseAddressRequest> createRepeated() => $pb.PbList<ParseAddressRequest>();
  @$core.pragma('dart2js:noInline')
  static ParseAddressRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<ParseAddressRequest>(create);
  static ParseAddressRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get value => $_getSZ(0);
  @$pb.TagNumber(1)
  set value($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasValue() => $_has(0);
  @$pb.TagNumber(1)
  void clearValue() => clearField(1);
}

class ParseAddressResult extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'ParseAddressResult', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOB(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'valid')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'message')
    ..aOS(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'formattedValue')
    ..hasRequiredFields = false
  ;

  ParseAddressResult._() : super();
  factory ParseAddressResult({
    $core.bool? valid,
    $core.String? message,
    $core.String? formattedValue,
  }) {
    final _result = create();
    if (valid != null) {
      _result.valid = valid;
    }
    if (message != null) {
      _result.message = message;
    }
    if (formattedValue != null) {
      _result.formattedValue = formattedValue;
    }
    return _result;
  }
  factory ParseAddressResult.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory ParseAddressResult.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  ParseAddressResult clone() => ParseAddressResult()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  ParseAddressResult copyWith(void Function(ParseAddressResult) updates) => super.copyWith((message) => updates(message as ParseAddressResult)) as ParseAddressResult; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static ParseAddressResult create() => ParseAddressResult._();
  ParseAddressResult createEmptyInstance() => create();
  static $pb.PbList<ParseAddressResult> createRepeated() => $pb.PbList<ParseAddressResult>();
  @$core.pragma('dart2js:noInline')
  static ParseAddressResult getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<ParseAddressResult>(create);
  static ParseAddressResult? _defaultInstance;

  @$pb.TagNumber(1)
  $core.bool get valid => $_getBF(0);
  @$pb.TagNumber(1)
  set valid($core.bool v) { $_setBool(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasValid() => $_has(0);
  @$pb.TagNumber(1)
  void clearValid() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get message => $_getSZ(1);
  @$pb.TagNumber(2)
  set message($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasMessage() => $_has(1);
  @$pb.TagNumber(2)
  void clearMessage() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get formattedValue => $_getSZ(2);
  @$pb.TagNumber(3)
  set formattedValue($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasFormattedValue() => $_has(2);
  @$pb.TagNumber(3)
  void clearFormattedValue() => clearField(3);
}

class AddRouteCrossingJunctionSwitchRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'AddRouteCrossingJunctionSwitchRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'routeId')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'junctionId')
    ..e<$1.SwitchDirection>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'direction', $pb.PbFieldType.OE, defaultOrMaker: $1.SwitchDirection.STRAIGHT, valueOf: $1.SwitchDirection.valueOf, enumValues: $1.SwitchDirection.values)
    ..hasRequiredFields = false
  ;

  AddRouteCrossingJunctionSwitchRequest._() : super();
  factory AddRouteCrossingJunctionSwitchRequest({
    $core.String? routeId,
    $core.String? junctionId,
    $1.SwitchDirection? direction,
  }) {
    final _result = create();
    if (routeId != null) {
      _result.routeId = routeId;
    }
    if (junctionId != null) {
      _result.junctionId = junctionId;
    }
    if (direction != null) {
      _result.direction = direction;
    }
    return _result;
  }
  factory AddRouteCrossingJunctionSwitchRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory AddRouteCrossingJunctionSwitchRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  AddRouteCrossingJunctionSwitchRequest clone() => AddRouteCrossingJunctionSwitchRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  AddRouteCrossingJunctionSwitchRequest copyWith(void Function(AddRouteCrossingJunctionSwitchRequest) updates) => super.copyWith((message) => updates(message as AddRouteCrossingJunctionSwitchRequest)) as AddRouteCrossingJunctionSwitchRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static AddRouteCrossingJunctionSwitchRequest create() => AddRouteCrossingJunctionSwitchRequest._();
  AddRouteCrossingJunctionSwitchRequest createEmptyInstance() => create();
  static $pb.PbList<AddRouteCrossingJunctionSwitchRequest> createRepeated() => $pb.PbList<AddRouteCrossingJunctionSwitchRequest>();
  @$core.pragma('dart2js:noInline')
  static AddRouteCrossingJunctionSwitchRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<AddRouteCrossingJunctionSwitchRequest>(create);
  static AddRouteCrossingJunctionSwitchRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get routeId => $_getSZ(0);
  @$pb.TagNumber(1)
  set routeId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasRouteId() => $_has(0);
  @$pb.TagNumber(1)
  void clearRouteId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get junctionId => $_getSZ(1);
  @$pb.TagNumber(2)
  set junctionId($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasJunctionId() => $_has(1);
  @$pb.TagNumber(2)
  void clearJunctionId() => clearField(2);

  @$pb.TagNumber(3)
  $1.SwitchDirection get direction => $_getN(2);
  @$pb.TagNumber(3)
  set direction($1.SwitchDirection v) { setField(3, v); }
  @$pb.TagNumber(3)
  $core.bool hasDirection() => $_has(2);
  @$pb.TagNumber(3)
  void clearDirection() => clearField(3);
}

class RemoveRouteCrossingJunctionRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'RemoveRouteCrossingJunctionRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'routeId')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'junctionId')
    ..hasRequiredFields = false
  ;

  RemoveRouteCrossingJunctionRequest._() : super();
  factory RemoveRouteCrossingJunctionRequest({
    $core.String? routeId,
    $core.String? junctionId,
  }) {
    final _result = create();
    if (routeId != null) {
      _result.routeId = routeId;
    }
    if (junctionId != null) {
      _result.junctionId = junctionId;
    }
    return _result;
  }
  factory RemoveRouteCrossingJunctionRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory RemoveRouteCrossingJunctionRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  RemoveRouteCrossingJunctionRequest clone() => RemoveRouteCrossingJunctionRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  RemoveRouteCrossingJunctionRequest copyWith(void Function(RemoveRouteCrossingJunctionRequest) updates) => super.copyWith((message) => updates(message as RemoveRouteCrossingJunctionRequest)) as RemoveRouteCrossingJunctionRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static RemoveRouteCrossingJunctionRequest create() => RemoveRouteCrossingJunctionRequest._();
  RemoveRouteCrossingJunctionRequest createEmptyInstance() => create();
  static $pb.PbList<RemoveRouteCrossingJunctionRequest> createRepeated() => $pb.PbList<RemoveRouteCrossingJunctionRequest>();
  @$core.pragma('dart2js:noInline')
  static RemoveRouteCrossingJunctionRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<RemoveRouteCrossingJunctionRequest>(create);
  static RemoveRouteCrossingJunctionRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get routeId => $_getSZ(0);
  @$pb.TagNumber(1)
  set routeId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasRouteId() => $_has(0);
  @$pb.TagNumber(1)
  void clearRouteId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get junctionId => $_getSZ(1);
  @$pb.TagNumber(2)
  set junctionId($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasJunctionId() => $_has(1);
  @$pb.TagNumber(2)
  void clearJunctionId() => clearField(2);
}

class AddRouteEventRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'AddRouteEventRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'routeId')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'sensorId')
    ..hasRequiredFields = false
  ;

  AddRouteEventRequest._() : super();
  factory AddRouteEventRequest({
    $core.String? routeId,
    $core.String? sensorId,
  }) {
    final _result = create();
    if (routeId != null) {
      _result.routeId = routeId;
    }
    if (sensorId != null) {
      _result.sensorId = sensorId;
    }
    return _result;
  }
  factory AddRouteEventRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory AddRouteEventRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  AddRouteEventRequest clone() => AddRouteEventRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  AddRouteEventRequest copyWith(void Function(AddRouteEventRequest) updates) => super.copyWith((message) => updates(message as AddRouteEventRequest)) as AddRouteEventRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static AddRouteEventRequest create() => AddRouteEventRequest._();
  AddRouteEventRequest createEmptyInstance() => create();
  static $pb.PbList<AddRouteEventRequest> createRepeated() => $pb.PbList<AddRouteEventRequest>();
  @$core.pragma('dart2js:noInline')
  static AddRouteEventRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<AddRouteEventRequest>(create);
  static AddRouteEventRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get routeId => $_getSZ(0);
  @$pb.TagNumber(1)
  set routeId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasRouteId() => $_has(0);
  @$pb.TagNumber(1)
  void clearRouteId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get sensorId => $_getSZ(1);
  @$pb.TagNumber(2)
  set sensorId($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasSensorId() => $_has(1);
  @$pb.TagNumber(2)
  void clearSensorId() => clearField(2);
}

class RemoveRouteEventRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'RemoveRouteEventRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'routeId')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'sensorId')
    ..hasRequiredFields = false
  ;

  RemoveRouteEventRequest._() : super();
  factory RemoveRouteEventRequest({
    $core.String? routeId,
    $core.String? sensorId,
  }) {
    final _result = create();
    if (routeId != null) {
      _result.routeId = routeId;
    }
    if (sensorId != null) {
      _result.sensorId = sensorId;
    }
    return _result;
  }
  factory RemoveRouteEventRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory RemoveRouteEventRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  RemoveRouteEventRequest clone() => RemoveRouteEventRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  RemoveRouteEventRequest copyWith(void Function(RemoveRouteEventRequest) updates) => super.copyWith((message) => updates(message as RemoveRouteEventRequest)) as RemoveRouteEventRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static RemoveRouteEventRequest create() => RemoveRouteEventRequest._();
  RemoveRouteEventRequest createEmptyInstance() => create();
  static $pb.PbList<RemoveRouteEventRequest> createRepeated() => $pb.PbList<RemoveRouteEventRequest>();
  @$core.pragma('dart2js:noInline')
  static RemoveRouteEventRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<RemoveRouteEventRequest>(create);
  static RemoveRouteEventRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get routeId => $_getSZ(0);
  @$pb.TagNumber(1)
  set routeId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasRouteId() => $_has(0);
  @$pb.TagNumber(1)
  void clearRouteId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get sensorId => $_getSZ(1);
  @$pb.TagNumber(2)
  set sensorId($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasSensorId() => $_has(1);
  @$pb.TagNumber(2)
  void clearSensorId() => clearField(2);
}

class AddRouteEventBehaviorRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'AddRouteEventBehaviorRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'routeId')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'sensorId')
    ..hasRequiredFields = false
  ;

  AddRouteEventBehaviorRequest._() : super();
  factory AddRouteEventBehaviorRequest({
    $core.String? routeId,
    $core.String? sensorId,
  }) {
    final _result = create();
    if (routeId != null) {
      _result.routeId = routeId;
    }
    if (sensorId != null) {
      _result.sensorId = sensorId;
    }
    return _result;
  }
  factory AddRouteEventBehaviorRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory AddRouteEventBehaviorRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  AddRouteEventBehaviorRequest clone() => AddRouteEventBehaviorRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  AddRouteEventBehaviorRequest copyWith(void Function(AddRouteEventBehaviorRequest) updates) => super.copyWith((message) => updates(message as AddRouteEventBehaviorRequest)) as AddRouteEventBehaviorRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static AddRouteEventBehaviorRequest create() => AddRouteEventBehaviorRequest._();
  AddRouteEventBehaviorRequest createEmptyInstance() => create();
  static $pb.PbList<AddRouteEventBehaviorRequest> createRepeated() => $pb.PbList<AddRouteEventBehaviorRequest>();
  @$core.pragma('dart2js:noInline')
  static AddRouteEventBehaviorRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<AddRouteEventBehaviorRequest>(create);
  static AddRouteEventBehaviorRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get routeId => $_getSZ(0);
  @$pb.TagNumber(1)
  set routeId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasRouteId() => $_has(0);
  @$pb.TagNumber(1)
  void clearRouteId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get sensorId => $_getSZ(1);
  @$pb.TagNumber(2)
  set sensorId($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasSensorId() => $_has(1);
  @$pb.TagNumber(2)
  void clearSensorId() => clearField(2);
}

class RemoveRouteEventBehaviorRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'RemoveRouteEventBehaviorRequest', package: const $pb.PackageName(const $core.bool.fromEnvironment('protobuf.omit_message_names') ? '' : 'binkyrailways.v1'), createEmptyInstance: create)
    ..aOS(1, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'routeId')
    ..aOS(2, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'sensorId')
    ..a<$core.int>(3, const $core.bool.fromEnvironment('protobuf.omit_field_names') ? '' : 'index', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  RemoveRouteEventBehaviorRequest._() : super();
  factory RemoveRouteEventBehaviorRequest({
    $core.String? routeId,
    $core.String? sensorId,
    $core.int? index,
  }) {
    final _result = create();
    if (routeId != null) {
      _result.routeId = routeId;
    }
    if (sensorId != null) {
      _result.sensorId = sensorId;
    }
    if (index != null) {
      _result.index = index;
    }
    return _result;
  }
  factory RemoveRouteEventBehaviorRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory RemoveRouteEventBehaviorRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  RemoveRouteEventBehaviorRequest clone() => RemoveRouteEventBehaviorRequest()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  RemoveRouteEventBehaviorRequest copyWith(void Function(RemoveRouteEventBehaviorRequest) updates) => super.copyWith((message) => updates(message as RemoveRouteEventBehaviorRequest)) as RemoveRouteEventBehaviorRequest; // ignore: deprecated_member_use
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static RemoveRouteEventBehaviorRequest create() => RemoveRouteEventBehaviorRequest._();
  RemoveRouteEventBehaviorRequest createEmptyInstance() => create();
  static $pb.PbList<RemoveRouteEventBehaviorRequest> createRepeated() => $pb.PbList<RemoveRouteEventBehaviorRequest>();
  @$core.pragma('dart2js:noInline')
  static RemoveRouteEventBehaviorRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<RemoveRouteEventBehaviorRequest>(create);
  static RemoveRouteEventBehaviorRequest? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get routeId => $_getSZ(0);
  @$pb.TagNumber(1)
  set routeId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasRouteId() => $_has(0);
  @$pb.TagNumber(1)
  void clearRouteId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get sensorId => $_getSZ(1);
  @$pb.TagNumber(2)
  set sensorId($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasSensorId() => $_has(1);
  @$pb.TagNumber(2)
  void clearSensorId() => clearField(2);

  @$pb.TagNumber(3)
  $core.int get index => $_getIZ(2);
  @$pb.TagNumber(3)
  set index($core.int v) { $_setSignedInt32(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasIndex() => $_has(2);
  @$pb.TagNumber(3)
  void clearIndex() => clearField(3);
}

