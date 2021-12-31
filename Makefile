SHELL = bash
PROJECT := binkyrailways
ROOTDIR := $(shell pwd)
BUILDIMAGE := $(PROJECT)-apis-build
CACHEVOL := $(PROJECT)-apis-gocache
MODVOL := $(PROJECT)-apis-pkg-mod

ORGPATH := github.com/binkyrailways
REPONAME := BinkyRailways
REPOPATH := $(ORGPATH)/$(REPONAME)

ifndef GOOS
	GOOS := $(shell go env GOOS)
endif
ifndef GOARCH
	GOARCH := $(shell go env GOARCH)
endif

DOCKERARGS := run -t --rm \
	-u $(shell id -u):$(shell id -g) \
	-v $(ROOTDIR):/go/src/$(REPOPATH):ro \
	-v $(ROOTDIR)/pkg:/go/src/$(REPOPATH)/pkg \
	-v $(ROOTDIR)/apps/binky/lib:/go/src/$(REPOPATH)/apps/binky/lib \
	-v $(CACHEVOL):/usr/gocache \
	-v $(MODVOL):/go/pkg/mod \
	-e GOCACHE=/usr/gocache \
	-e CGO_ENABLED=0 \
	-e GO111MODULE=off \
	-w /go/src/$(REPOPATH) \
	$(BUILDIMAGE)

DOCKERENV := docker $(DOCKERARGS)

SOURCES := $(shell find . -name '*.go')
APISOURCES := $(shell find pkg/api -name '*.proto')
APIGENSOURCES := $(shell find pkg/api -name '*.go')

all: binaries

clean:
	rm -Rf bin

binaries: generate
	gox \
		-mod=readonly \
		-osarch="darwin/amd64 darwin/arm64" \
		-ldflags="-X main.projectVersion=${VERSION} -X main.projectBuild=${COMMIT}" \
		-output="bin/{{.OS}}/{{.Arch}}/binky-server" \
		-tags="netgo" \
		github.com/binkyrailways/BinkyRailways

bootstrap:
	go get github.com/mitchellh/gox

test:
	go test -v ./...

# Build docker builder image
.PHONY: build-image
build-image:
	docker buildx uninstall
	docker build \
		-t $(BUILDIMAGE) \
		-f pkg/api/Dockerfile.build pkg/api

.PHONY: $(CACHEVOL)
$(CACHEVOL):
	@docker volume create $(CACHEVOL)
	docker run -it 	--rm -v $(CACHEVOL):/usr/gocache \
		$(BUILDIMAGE) \
		chown -R $(shell id -u):$(shell id -g) /usr/gocache

.PHONY: $(MODVOL)
$(MODVOL):
	@docker volume create $(MODVOL)
	docker run -it 	--rm -v $(MODVOL):/go/pkg/mod \
		$(BUILDIMAGE) \
		chown -R $(shell id -u):$(shell id -g) /go/pkg/mod

# Generate go code for k8s types & proto files
.PHONY: generate
generate: $(CACHEVOL) $(MODVOL)
#	$(DOCKERENV) ls -al /usr/lib/dart
	$(DOCKERENV) go generate pkg/api/v1/doc.go

update-modules:
	go get \
		github.com/binkynet/NetManager@v0.5.7 \
		github.com/binkynet/BinkyNet@v0.11.0
	go mod tidy

update-gioui:
	go get -u \
		gioui.org \
		gioui.org/x