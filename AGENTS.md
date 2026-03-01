# Agent Guide for BinkyRailways-go

This document provides essential information for agents working on the BinkyRailways-go repository.

## Project Overview
BinkyRailways is a model railway automation system. This repository contains the Go-based backend (server, model, and state engine) and a Flutter-based GUI.

- **Backend**: Go (located in `pkg/`, `main.go`, `cmd/`)
- **Frontend**: Flutter (located in `apps/binky/`)
- **API**: gRPC / Protobuf (definitions in `pkg/api/v1/*.proto`)
- **MQTT**: An embedded MQTT server runs at the process level (default port 1883).

## Essential Commands

### Backend (Go)
- **Build**: `make binaries-server` (Builds for multiple platforms in `bin/`)
- **Test**: `make test` or `go test ./pkg/...`
- **Generate API**: `make generate` (Runs protoc to generate Go and Dart code)
- **Update Modules**: `make update-modules`

### Frontend (Flutter)
- **Build Web**: `make binaries-web`
- **Build MacOS**: `make binaries-macos`
- **Run (Development)**: `make develop-gui` (MacOS) or `make develop-web` (Chrome)

## Code Organization

- `pkg/core/model`: Interface definitions for the railway model (e.g., Loc, Block, Route).
- `pkg/core/model/impl`: Implementation of the railway model, including XML serialization.
- `pkg/core/state`: State engine that manages the live operation of the railway.
- `pkg/api/v1`: gRPC API definitions and generated code.
- `pkg/server`: gRPC and HTTP server implementation.
- `pkg/service`: High-level service that ties together model and state.
- `apps/binky`: Flutter application for managing and operating the railway.

## Development Patterns

### Model vs State
- **Model**: Represents the static configuration (saved to XML). Located in `pkg/core/model`.
- **State**: Represents the live, running state of the railway (e.g., current speed, block occupancy). Located in `pkg/core/state`.

### API Generation
The project uses gRPC for communication. When changing `.proto` files in `pkg/api/v1/`, you must run `make generate` to update both the Go backend and Flutter frontend code.

### Entity Pattern
Most model objects follow an "Entity" pattern:
- Defined by an interface in `pkg/core/model`.
- Implemented by a struct in `pkg/core/model/impl`.
- Uses a unique ID (randomly generated if missing).
- Supports visitors via the `Accept(v EntityVisitor)` method.

## Conventions

- **Logging**: Uses `zerolog`. Loggers are typically passed down via dependencies or context.
- **Errors**: Return errors instead of panicking.
- **Testing**: Use standard Go tests. Mocking is often done via interfaces.

## Gotchas

- **XML Serialization**: The model implementation uses XML tags for persistence. Be careful when renaming fields in `pkg/core/model/impl`.
- **Address Parsing**: Railway addresses (DCC, LocoNet, etc.) have a specific string format (e.g., "DCC 3"). Use `model.NewAddressFromString` for parsing.
- **Flutter Build**: Ensure Flutter is installed and on your PATH when working on the frontend.
- **Proto Vendor**: The project contains a `_proto_vendor` directory which may be used during generation.
