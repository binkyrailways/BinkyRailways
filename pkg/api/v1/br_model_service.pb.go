// Code generated by protoc-gen-gogo. DO NOT EDIT.
// source: br_model_service.proto

package v1

import (
	context "context"
	fmt "fmt"
	proto "github.com/golang/protobuf/proto"
	grpc "google.golang.org/grpc"
	codes "google.golang.org/grpc/codes"
	status "google.golang.org/grpc/status"
	io "io"
	math "math"
	math_bits "math/bits"
)

// Reference imports to suppress errors if they are not otherwise used.
var _ = proto.Marshal
var _ = fmt.Errorf
var _ = math.Inf

// This is a compile-time assertion to ensure that this generated file
// is compatible with the proto package it is being compiled against.
// A compilation error at this line likely means your copy of the
// proto package needs to be updated.
const _ = proto.ProtoPackageIsVersion3 // please upgrade the proto package

// Request by ID
type IDRequest struct {
	Id                   string   `protobuf:"bytes,1,opt,name=id,proto3" json:"id,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *IDRequest) Reset()         { *m = IDRequest{} }
func (m *IDRequest) String() string { return proto.CompactTextString(m) }
func (*IDRequest) ProtoMessage()    {}
func (*IDRequest) Descriptor() ([]byte, []int) {
	return fileDescriptor_0d5e66fb0029ff04, []int{0}
}
func (m *IDRequest) XXX_Unmarshal(b []byte) error {
	return m.Unmarshal(b)
}
func (m *IDRequest) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	if deterministic {
		return xxx_messageInfo_IDRequest.Marshal(b, m, deterministic)
	} else {
		b = b[:cap(b)]
		n, err := m.MarshalToSizedBuffer(b)
		if err != nil {
			return nil, err
		}
		return b[:n], nil
	}
}
func (m *IDRequest) XXX_Merge(src proto.Message) {
	xxx_messageInfo_IDRequest.Merge(m, src)
}
func (m *IDRequest) XXX_Size() int {
	return m.Size()
}
func (m *IDRequest) XXX_DiscardUnknown() {
	xxx_messageInfo_IDRequest.DiscardUnknown(m)
}

var xxx_messageInfo_IDRequest proto.InternalMessageInfo

func (m *IDRequest) GetId() string {
	if m != nil {
		return m.Id
	}
	return ""
}

func init() {
	proto.RegisterType((*IDRequest)(nil), "binkyrailways.v1.IDRequest")
}

func init() { proto.RegisterFile("br_model_service.proto", fileDescriptor_0d5e66fb0029ff04) }

var fileDescriptor_0d5e66fb0029ff04 = []byte{
	// 347 bytes of a gzipped FileDescriptorProto
	0x1f, 0x8b, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0xff, 0xe2, 0x12, 0x4b, 0x2a, 0x8a, 0xcf,
	0xcd, 0x4f, 0x49, 0xcd, 0x89, 0x2f, 0x4e, 0x2d, 0x2a, 0xcb, 0x4c, 0x4e, 0xd5, 0x2b, 0x28, 0xca,
	0x2f, 0xc9, 0x17, 0x12, 0x48, 0xca, 0xcc, 0xcb, 0xae, 0x2c, 0x4a, 0xcc, 0xcc, 0x29, 0x4f, 0xac,
	0x2c, 0xd6, 0x2b, 0x33, 0x94, 0x12, 0x81, 0xab, 0x2c, 0xa9, 0x2c, 0x48, 0x2d, 0x86, 0xa8, 0x53,
	0x92, 0xe6, 0xe2, 0xf4, 0x74, 0x09, 0x4a, 0x2d, 0x2c, 0x4d, 0x2d, 0x2e, 0x11, 0xe2, 0xe3, 0x62,
	0xca, 0x4c, 0x91, 0x60, 0x54, 0x60, 0xd4, 0xe0, 0x0c, 0x62, 0xca, 0x4c, 0x31, 0x5a, 0xce, 0xca,
	0xc5, 0xe3, 0x0b, 0xd2, 0x12, 0x0c, 0x31, 0x5b, 0xc8, 0x81, 0x8b, 0xcb, 0x3d, 0xb5, 0x24, 0x08,
	0x62, 0xaa, 0x90, 0xb8, 0x1e, 0xba, 0x25, 0x7a, 0xae, 0xb9, 0x05, 0x25, 0x95, 0x52, 0x92, 0x98,
	0x12, 0x30, 0x3d, 0xae, 0x5c, 0xbc, 0xa1, 0x05, 0x29, 0x89, 0x25, 0xa9, 0x30, 0x01, 0xdc, 0x6a,
	0xf1, 0x19, 0x63, 0xc1, 0xc5, 0x12, 0x9c, 0x58, 0x96, 0x8a, 0xdb, 0x09, 0xb8, 0x24, 0x84, 0x9c,
	0xb8, 0x38, 0xdd, 0x53, 0x4b, 0x7c, 0xf3, 0x53, 0x4a, 0x73, 0x52, 0x85, 0xa4, 0x31, 0x55, 0xc1,
	0x43, 0x43, 0x4a, 0x02, 0x53, 0x12, 0xaa, 0x2d, 0x80, 0x4b, 0x02, 0x6e, 0x86, 0x53, 0x62, 0x72,
	0x76, 0x7a, 0x51, 0x7e, 0x69, 0x5e, 0x8a, 0x67, 0x6e, 0x62, 0x3a, 0x01, 0x23, 0xb1, 0xb8, 0x0a,
	0xa2, 0xcb, 0x89, 0x8b, 0x07, 0x12, 0x2c, 0x50, 0x1b, 0x70, 0xda, 0x8d, 0xc7, 0x55, 0x36, 0x5c,
	0x6c, 0xee, 0xa9, 0x25, 0x3e, 0xf9, 0xc9, 0xf8, 0xdd, 0x20, 0x8a, 0x29, 0x09, 0xd2, 0x63, 0xc9,
	0xc5, 0x09, 0x71, 0x01, 0x88, 0x83, 0x5d, 0x0d, 0x2e, 0xad, 0x0e, 0x5c, 0x1c, 0xee, 0xa9, 0x25,
	0x4e, 0x39, 0xf9, 0xc9, 0xd9, 0x24, 0x7b, 0x1f, 0xa2, 0xcb, 0x9e, 0x8b, 0x1b, 0x62, 0x39, 0x84,
	0x8b, 0x4b, 0x1d, 0x4e, 0x03, 0x9c, 0x9c, 0x4f, 0x3c, 0x92, 0x63, 0xbc, 0xf0, 0x48, 0x8e, 0xf1,
	0xc1, 0x23, 0x39, 0xc6, 0x19, 0x8f, 0xe5, 0x18, 0xa2, 0x0c, 0xd3, 0x33, 0x4b, 0x32, 0x4a, 0x93,
	0xf4, 0x92, 0xf3, 0x73, 0xf5, 0x51, 0x34, 0xe9, 0x3b, 0x81, 0x78, 0x41, 0x30, 0x5e, 0x41, 0x76,
	0xba, 0x7e, 0x62, 0x41, 0xa6, 0x7e, 0x99, 0x61, 0x12, 0x1b, 0x38, 0x4b, 0x18, 0x03, 0x02, 0x00,
	0x00, 0xff, 0xff, 0x13, 0x18, 0x5d, 0x5c, 0x54, 0x03, 0x00, 0x00,
}

// Reference imports to suppress errors if they are not otherwise used.
var _ context.Context
var _ grpc.ClientConn

// This is a compile-time assertion to ensure that this generated file
// is compatible with the grpc package it is being compiled against.
const _ = grpc.SupportPackageIsVersion4

// ModelServiceClient is the client API for ModelService service.
//
// For semantics around ctx use and closing/ending streaming RPCs, please refer to https://godoc.org/google.golang.org/grpc#ClientConn.NewStream.
type ModelServiceClient interface {
	// Gets the current railway
	GetRailway(ctx context.Context, in *Empty, opts ...grpc.CallOption) (*Railway, error)
	// Update the current railway
	UpdateRailway(ctx context.Context, in *Railway, opts ...grpc.CallOption) (*Railway, error)
	// Save changes to disk
	Save(ctx context.Context, in *Empty, opts ...grpc.CallOption) (*Empty, error)
	// Gets a module by ID.
	GetModule(ctx context.Context, in *IDRequest, opts ...grpc.CallOption) (*Module, error)
	// Get the background image of a module by ID.
	GetModuleBackgroundImage(ctx context.Context, in *IDRequest, opts ...grpc.CallOption) (*Image, error)
	// Update a module by ID.
	UpdateModule(ctx context.Context, in *Module, opts ...grpc.CallOption) (*Module, error)
	// Gets a loc by ID.
	GetLoc(ctx context.Context, in *IDRequest, opts ...grpc.CallOption) (*Loc, error)
	// Update a loc by ID.
	UpdateLoc(ctx context.Context, in *Loc, opts ...grpc.CallOption) (*Loc, error)
	// Gets a block by ID.
	GetBlock(ctx context.Context, in *IDRequest, opts ...grpc.CallOption) (*Block, error)
	// Update a block by ID.
	UpdateBlock(ctx context.Context, in *Block, opts ...grpc.CallOption) (*Block, error)
}

type modelServiceClient struct {
	cc *grpc.ClientConn
}

func NewModelServiceClient(cc *grpc.ClientConn) ModelServiceClient {
	return &modelServiceClient{cc}
}

func (c *modelServiceClient) GetRailway(ctx context.Context, in *Empty, opts ...grpc.CallOption) (*Railway, error) {
	out := new(Railway)
	err := c.cc.Invoke(ctx, "/binkyrailways.v1.ModelService/GetRailway", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *modelServiceClient) UpdateRailway(ctx context.Context, in *Railway, opts ...grpc.CallOption) (*Railway, error) {
	out := new(Railway)
	err := c.cc.Invoke(ctx, "/binkyrailways.v1.ModelService/UpdateRailway", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *modelServiceClient) Save(ctx context.Context, in *Empty, opts ...grpc.CallOption) (*Empty, error) {
	out := new(Empty)
	err := c.cc.Invoke(ctx, "/binkyrailways.v1.ModelService/Save", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *modelServiceClient) GetModule(ctx context.Context, in *IDRequest, opts ...grpc.CallOption) (*Module, error) {
	out := new(Module)
	err := c.cc.Invoke(ctx, "/binkyrailways.v1.ModelService/GetModule", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *modelServiceClient) GetModuleBackgroundImage(ctx context.Context, in *IDRequest, opts ...grpc.CallOption) (*Image, error) {
	out := new(Image)
	err := c.cc.Invoke(ctx, "/binkyrailways.v1.ModelService/GetModuleBackgroundImage", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *modelServiceClient) UpdateModule(ctx context.Context, in *Module, opts ...grpc.CallOption) (*Module, error) {
	out := new(Module)
	err := c.cc.Invoke(ctx, "/binkyrailways.v1.ModelService/UpdateModule", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *modelServiceClient) GetLoc(ctx context.Context, in *IDRequest, opts ...grpc.CallOption) (*Loc, error) {
	out := new(Loc)
	err := c.cc.Invoke(ctx, "/binkyrailways.v1.ModelService/GetLoc", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *modelServiceClient) UpdateLoc(ctx context.Context, in *Loc, opts ...grpc.CallOption) (*Loc, error) {
	out := new(Loc)
	err := c.cc.Invoke(ctx, "/binkyrailways.v1.ModelService/UpdateLoc", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *modelServiceClient) GetBlock(ctx context.Context, in *IDRequest, opts ...grpc.CallOption) (*Block, error) {
	out := new(Block)
	err := c.cc.Invoke(ctx, "/binkyrailways.v1.ModelService/GetBlock", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *modelServiceClient) UpdateBlock(ctx context.Context, in *Block, opts ...grpc.CallOption) (*Block, error) {
	out := new(Block)
	err := c.cc.Invoke(ctx, "/binkyrailways.v1.ModelService/UpdateBlock", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

// ModelServiceServer is the server API for ModelService service.
type ModelServiceServer interface {
	// Gets the current railway
	GetRailway(context.Context, *Empty) (*Railway, error)
	// Update the current railway
	UpdateRailway(context.Context, *Railway) (*Railway, error)
	// Save changes to disk
	Save(context.Context, *Empty) (*Empty, error)
	// Gets a module by ID.
	GetModule(context.Context, *IDRequest) (*Module, error)
	// Get the background image of a module by ID.
	GetModuleBackgroundImage(context.Context, *IDRequest) (*Image, error)
	// Update a module by ID.
	UpdateModule(context.Context, *Module) (*Module, error)
	// Gets a loc by ID.
	GetLoc(context.Context, *IDRequest) (*Loc, error)
	// Update a loc by ID.
	UpdateLoc(context.Context, *Loc) (*Loc, error)
	// Gets a block by ID.
	GetBlock(context.Context, *IDRequest) (*Block, error)
	// Update a block by ID.
	UpdateBlock(context.Context, *Block) (*Block, error)
}

// UnimplementedModelServiceServer can be embedded to have forward compatible implementations.
type UnimplementedModelServiceServer struct {
}

func (*UnimplementedModelServiceServer) GetRailway(ctx context.Context, req *Empty) (*Railway, error) {
	return nil, status.Errorf(codes.Unimplemented, "method GetRailway not implemented")
}
func (*UnimplementedModelServiceServer) UpdateRailway(ctx context.Context, req *Railway) (*Railway, error) {
	return nil, status.Errorf(codes.Unimplemented, "method UpdateRailway not implemented")
}
func (*UnimplementedModelServiceServer) Save(ctx context.Context, req *Empty) (*Empty, error) {
	return nil, status.Errorf(codes.Unimplemented, "method Save not implemented")
}
func (*UnimplementedModelServiceServer) GetModule(ctx context.Context, req *IDRequest) (*Module, error) {
	return nil, status.Errorf(codes.Unimplemented, "method GetModule not implemented")
}
func (*UnimplementedModelServiceServer) GetModuleBackgroundImage(ctx context.Context, req *IDRequest) (*Image, error) {
	return nil, status.Errorf(codes.Unimplemented, "method GetModuleBackgroundImage not implemented")
}
func (*UnimplementedModelServiceServer) UpdateModule(ctx context.Context, req *Module) (*Module, error) {
	return nil, status.Errorf(codes.Unimplemented, "method UpdateModule not implemented")
}
func (*UnimplementedModelServiceServer) GetLoc(ctx context.Context, req *IDRequest) (*Loc, error) {
	return nil, status.Errorf(codes.Unimplemented, "method GetLoc not implemented")
}
func (*UnimplementedModelServiceServer) UpdateLoc(ctx context.Context, req *Loc) (*Loc, error) {
	return nil, status.Errorf(codes.Unimplemented, "method UpdateLoc not implemented")
}
func (*UnimplementedModelServiceServer) GetBlock(ctx context.Context, req *IDRequest) (*Block, error) {
	return nil, status.Errorf(codes.Unimplemented, "method GetBlock not implemented")
}
func (*UnimplementedModelServiceServer) UpdateBlock(ctx context.Context, req *Block) (*Block, error) {
	return nil, status.Errorf(codes.Unimplemented, "method UpdateBlock not implemented")
}

func RegisterModelServiceServer(s *grpc.Server, srv ModelServiceServer) {
	s.RegisterService(&_ModelService_serviceDesc, srv)
}

func _ModelService_GetRailway_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(Empty)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(ModelServiceServer).GetRailway(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/binkyrailways.v1.ModelService/GetRailway",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(ModelServiceServer).GetRailway(ctx, req.(*Empty))
	}
	return interceptor(ctx, in, info, handler)
}

func _ModelService_UpdateRailway_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(Railway)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(ModelServiceServer).UpdateRailway(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/binkyrailways.v1.ModelService/UpdateRailway",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(ModelServiceServer).UpdateRailway(ctx, req.(*Railway))
	}
	return interceptor(ctx, in, info, handler)
}

func _ModelService_Save_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(Empty)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(ModelServiceServer).Save(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/binkyrailways.v1.ModelService/Save",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(ModelServiceServer).Save(ctx, req.(*Empty))
	}
	return interceptor(ctx, in, info, handler)
}

func _ModelService_GetModule_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(IDRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(ModelServiceServer).GetModule(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/binkyrailways.v1.ModelService/GetModule",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(ModelServiceServer).GetModule(ctx, req.(*IDRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _ModelService_GetModuleBackgroundImage_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(IDRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(ModelServiceServer).GetModuleBackgroundImage(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/binkyrailways.v1.ModelService/GetModuleBackgroundImage",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(ModelServiceServer).GetModuleBackgroundImage(ctx, req.(*IDRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _ModelService_UpdateModule_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(Module)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(ModelServiceServer).UpdateModule(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/binkyrailways.v1.ModelService/UpdateModule",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(ModelServiceServer).UpdateModule(ctx, req.(*Module))
	}
	return interceptor(ctx, in, info, handler)
}

func _ModelService_GetLoc_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(IDRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(ModelServiceServer).GetLoc(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/binkyrailways.v1.ModelService/GetLoc",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(ModelServiceServer).GetLoc(ctx, req.(*IDRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _ModelService_UpdateLoc_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(Loc)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(ModelServiceServer).UpdateLoc(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/binkyrailways.v1.ModelService/UpdateLoc",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(ModelServiceServer).UpdateLoc(ctx, req.(*Loc))
	}
	return interceptor(ctx, in, info, handler)
}

func _ModelService_GetBlock_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(IDRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(ModelServiceServer).GetBlock(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/binkyrailways.v1.ModelService/GetBlock",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(ModelServiceServer).GetBlock(ctx, req.(*IDRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _ModelService_UpdateBlock_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(Block)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(ModelServiceServer).UpdateBlock(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/binkyrailways.v1.ModelService/UpdateBlock",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(ModelServiceServer).UpdateBlock(ctx, req.(*Block))
	}
	return interceptor(ctx, in, info, handler)
}

var _ModelService_serviceDesc = grpc.ServiceDesc{
	ServiceName: "binkyrailways.v1.ModelService",
	HandlerType: (*ModelServiceServer)(nil),
	Methods: []grpc.MethodDesc{
		{
			MethodName: "GetRailway",
			Handler:    _ModelService_GetRailway_Handler,
		},
		{
			MethodName: "UpdateRailway",
			Handler:    _ModelService_UpdateRailway_Handler,
		},
		{
			MethodName: "Save",
			Handler:    _ModelService_Save_Handler,
		},
		{
			MethodName: "GetModule",
			Handler:    _ModelService_GetModule_Handler,
		},
		{
			MethodName: "GetModuleBackgroundImage",
			Handler:    _ModelService_GetModuleBackgroundImage_Handler,
		},
		{
			MethodName: "UpdateModule",
			Handler:    _ModelService_UpdateModule_Handler,
		},
		{
			MethodName: "GetLoc",
			Handler:    _ModelService_GetLoc_Handler,
		},
		{
			MethodName: "UpdateLoc",
			Handler:    _ModelService_UpdateLoc_Handler,
		},
		{
			MethodName: "GetBlock",
			Handler:    _ModelService_GetBlock_Handler,
		},
		{
			MethodName: "UpdateBlock",
			Handler:    _ModelService_UpdateBlock_Handler,
		},
	},
	Streams:  []grpc.StreamDesc{},
	Metadata: "br_model_service.proto",
}

func (m *IDRequest) Marshal() (dAtA []byte, err error) {
	size := m.Size()
	dAtA = make([]byte, size)
	n, err := m.MarshalToSizedBuffer(dAtA[:size])
	if err != nil {
		return nil, err
	}
	return dAtA[:n], nil
}

func (m *IDRequest) MarshalTo(dAtA []byte) (int, error) {
	size := m.Size()
	return m.MarshalToSizedBuffer(dAtA[:size])
}

func (m *IDRequest) MarshalToSizedBuffer(dAtA []byte) (int, error) {
	i := len(dAtA)
	_ = i
	var l int
	_ = l
	if m.XXX_unrecognized != nil {
		i -= len(m.XXX_unrecognized)
		copy(dAtA[i:], m.XXX_unrecognized)
	}
	if len(m.Id) > 0 {
		i -= len(m.Id)
		copy(dAtA[i:], m.Id)
		i = encodeVarintBrModelService(dAtA, i, uint64(len(m.Id)))
		i--
		dAtA[i] = 0xa
	}
	return len(dAtA) - i, nil
}

func encodeVarintBrModelService(dAtA []byte, offset int, v uint64) int {
	offset -= sovBrModelService(v)
	base := offset
	for v >= 1<<7 {
		dAtA[offset] = uint8(v&0x7f | 0x80)
		v >>= 7
		offset++
	}
	dAtA[offset] = uint8(v)
	return base
}
func (m *IDRequest) Size() (n int) {
	if m == nil {
		return 0
	}
	var l int
	_ = l
	l = len(m.Id)
	if l > 0 {
		n += 1 + l + sovBrModelService(uint64(l))
	}
	if m.XXX_unrecognized != nil {
		n += len(m.XXX_unrecognized)
	}
	return n
}

func sovBrModelService(x uint64) (n int) {
	return (math_bits.Len64(x|1) + 6) / 7
}
func sozBrModelService(x uint64) (n int) {
	return sovBrModelService(uint64((x << 1) ^ uint64((int64(x) >> 63))))
}
func (m *IDRequest) Unmarshal(dAtA []byte) error {
	l := len(dAtA)
	iNdEx := 0
	for iNdEx < l {
		preIndex := iNdEx
		var wire uint64
		for shift := uint(0); ; shift += 7 {
			if shift >= 64 {
				return ErrIntOverflowBrModelService
			}
			if iNdEx >= l {
				return io.ErrUnexpectedEOF
			}
			b := dAtA[iNdEx]
			iNdEx++
			wire |= uint64(b&0x7F) << shift
			if b < 0x80 {
				break
			}
		}
		fieldNum := int32(wire >> 3)
		wireType := int(wire & 0x7)
		if wireType == 4 {
			return fmt.Errorf("proto: IDRequest: wiretype end group for non-group")
		}
		if fieldNum <= 0 {
			return fmt.Errorf("proto: IDRequest: illegal tag %d (wire type %d)", fieldNum, wire)
		}
		switch fieldNum {
		case 1:
			if wireType != 2 {
				return fmt.Errorf("proto: wrong wireType = %d for field Id", wireType)
			}
			var stringLen uint64
			for shift := uint(0); ; shift += 7 {
				if shift >= 64 {
					return ErrIntOverflowBrModelService
				}
				if iNdEx >= l {
					return io.ErrUnexpectedEOF
				}
				b := dAtA[iNdEx]
				iNdEx++
				stringLen |= uint64(b&0x7F) << shift
				if b < 0x80 {
					break
				}
			}
			intStringLen := int(stringLen)
			if intStringLen < 0 {
				return ErrInvalidLengthBrModelService
			}
			postIndex := iNdEx + intStringLen
			if postIndex < 0 {
				return ErrInvalidLengthBrModelService
			}
			if postIndex > l {
				return io.ErrUnexpectedEOF
			}
			m.Id = string(dAtA[iNdEx:postIndex])
			iNdEx = postIndex
		default:
			iNdEx = preIndex
			skippy, err := skipBrModelService(dAtA[iNdEx:])
			if err != nil {
				return err
			}
			if (skippy < 0) || (iNdEx+skippy) < 0 {
				return ErrInvalidLengthBrModelService
			}
			if (iNdEx + skippy) > l {
				return io.ErrUnexpectedEOF
			}
			m.XXX_unrecognized = append(m.XXX_unrecognized, dAtA[iNdEx:iNdEx+skippy]...)
			iNdEx += skippy
		}
	}

	if iNdEx > l {
		return io.ErrUnexpectedEOF
	}
	return nil
}
func skipBrModelService(dAtA []byte) (n int, err error) {
	l := len(dAtA)
	iNdEx := 0
	depth := 0
	for iNdEx < l {
		var wire uint64
		for shift := uint(0); ; shift += 7 {
			if shift >= 64 {
				return 0, ErrIntOverflowBrModelService
			}
			if iNdEx >= l {
				return 0, io.ErrUnexpectedEOF
			}
			b := dAtA[iNdEx]
			iNdEx++
			wire |= (uint64(b) & 0x7F) << shift
			if b < 0x80 {
				break
			}
		}
		wireType := int(wire & 0x7)
		switch wireType {
		case 0:
			for shift := uint(0); ; shift += 7 {
				if shift >= 64 {
					return 0, ErrIntOverflowBrModelService
				}
				if iNdEx >= l {
					return 0, io.ErrUnexpectedEOF
				}
				iNdEx++
				if dAtA[iNdEx-1] < 0x80 {
					break
				}
			}
		case 1:
			iNdEx += 8
		case 2:
			var length int
			for shift := uint(0); ; shift += 7 {
				if shift >= 64 {
					return 0, ErrIntOverflowBrModelService
				}
				if iNdEx >= l {
					return 0, io.ErrUnexpectedEOF
				}
				b := dAtA[iNdEx]
				iNdEx++
				length |= (int(b) & 0x7F) << shift
				if b < 0x80 {
					break
				}
			}
			if length < 0 {
				return 0, ErrInvalidLengthBrModelService
			}
			iNdEx += length
		case 3:
			depth++
		case 4:
			if depth == 0 {
				return 0, ErrUnexpectedEndOfGroupBrModelService
			}
			depth--
		case 5:
			iNdEx += 4
		default:
			return 0, fmt.Errorf("proto: illegal wireType %d", wireType)
		}
		if iNdEx < 0 {
			return 0, ErrInvalidLengthBrModelService
		}
		if depth == 0 {
			return iNdEx, nil
		}
	}
	return 0, io.ErrUnexpectedEOF
}

var (
	ErrInvalidLengthBrModelService        = fmt.Errorf("proto: negative length found during unmarshaling")
	ErrIntOverflowBrModelService          = fmt.Errorf("proto: integer overflow")
	ErrUnexpectedEndOfGroupBrModelService = fmt.Errorf("proto: unexpected end of group")
)
