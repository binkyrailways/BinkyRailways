///
//  Generated code. Do not modify.
//  source: br_state_service.proto
//
// @dart = 2.12
// ignore_for_file: annotate_overrides,camel_case_types,unnecessary_const,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type,unnecessary_this,prefer_final_fields

import 'dart:core' as $core;

import 'package:protobuf/protobuf.dart' as $pb;

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

