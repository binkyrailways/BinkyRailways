SHELL = bash
PROJECT := binkyrailways
ROOTDIR := $(shell pwd)
BUILDIMAGE := $(PROJECT)-apis-build
CACHEVOL := $(PROJECT)-apis-gocache
MODVOL := $(PROJECT)-apis-pkg-mod

ORGPATH := github.com/binkyrailways
REPONAME := BinkyRailways
REPOPATH := $(ORGPATH)/$(REPONAME)

BINKYHOST := 192.168.77.1
BINKYHOSTUSER := pi

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

SOURCES := $(shell find . -name '*.go') go.mod go.sum
APISOURCES := $(shell find pkg/api -name '*.proto')
APIGENSOURCES := $(shell find pkg/api -name '*.go')

all: binaries

clean:
	rm -Rf bin

.PHONY: binaries
binaries: generate binaries-server

.PHONY: binaries-server 
binaries-server: generate pkg/core/model/predicates/parser.go
	gox \
		-mod=readonly \
		-osarch="darwin/arm64 linux/amd64 linux/arm linux/arm64" \
		-ldflags="-X main.projectVersion=${VERSION} -X main.projectBuild=${COMMIT}" \
		-output="bin/{{.OS}}/{{.Arch}}/binky-server" \
		-tags="netgo" \
		github.com/binkyrailways/BinkyRailways

.PHONY: binaries-gui
binaries-gui: binaries-web binaries-macos

.PHONY: binaries-web
binaries-web: 
	cd apps/binky ; flutter build web --dart-define=FLUTTER_WEB_CANVASKIT_URL=/canvaskit/

.PHONY: binaries-macos
binaries-macos: 
	LANG="en_US.UTF-8" LC_COLLATE="en_US.UTF-8" LC_CTYPE="en_US.UTF-8" \
		LC_MESSAGES="en_US.UTF-8" LC_MONETARY="en_US.UTF-8" LC_NUMERIC="en_US.UTF-8" \
		LC_TIME="en_US.UTF-8" LC_ALL=en_US.UTF-8 \
		cd apps/binky ; flutter build macos
		open apps/binky/build/macos/Build/Products/Release/

.PHONY: develop-gui
develop-gui: 
	LANG="en_US.UTF-8" LC_COLLATE="en_US.UTF-8" LC_CTYPE="en_US.UTF-8" \
		LC_MESSAGES="en_US.UTF-8" LC_MONETARY="en_US.UTF-8" LC_NUMERIC="en_US.UTF-8" \
		LC_TIME="en_US.UTF-8" LC_ALL=en_US.UTF-8 \
		cd apps/binky ; flutter run -d macos

bootstrap:
	go get github.com/mitchellh/gox
	go install github.com/mna/pigeon@v1.1.0

test:
	go test -v github.com/binkyrailways/BinkyRailways/pkg/...

# Build docker builder image
.PHONY: build-image
build-image:
	#docker buildx uninstall
	docker build \
		-t $(BUILDIMAGE) \
		-f pkg/api/Dockerfile.build pkg/api

.PHONY: $(CACHEVOL)
$(CACHEVOL):
	@docker volume create $(CACHEVOL) || true
	docker run -it 	--rm -v $(CACHEVOL):/usr/gocache \
		$(BUILDIMAGE) \
		chown -R $(shell id -u):$(shell id -g) /usr/gocache

.PHONY: $(MODVOL)
$(MODVOL):
	@docker volume create $(MODVOL) || true
	docker run -it 	--rm -v $(MODVOL):/go/pkg/mod \
		$(BUILDIMAGE) \
		chown -R $(shell id -u):$(shell id -g) /go/pkg/mod

# Generate go code for k8s types & proto files
.PHONY: generate
generate: $(CACHEVOL) $(MODVOL)
#	$(DOCKERENV) ls -al /usr/lib/dart
	$(DOCKERENV) go generate pkg/api/v1/doc.go

pkg/core/model/predicates/parser.go: pkg/core/model/predicates/parser.peg
	pigeon -o pkg/core/model/predicates/parser.go pkg/core/model/predicates/parser.peg

update-modules:
	go get \
		github.com/binkynet/NetManager@v1.6.1 \
		github.com/binkynet/BinkyNet@v1.8.0
	go mod tidy

deploy:
	scp scripts/binkyrailways.service $(BINKYHOSTUSER)@$(BINKYHOST):/home/$(BINKYHOSTUSER)/binkyrailways.service
	ssh $(BINKYHOSTUSER)@$(BINKYHOST) /usr/bin/sudo systemctl link /home/$(BINKYHOSTUSER)/binkyrailways.service
	ssh $(BINKYHOSTUSER)@$(BINKYHOST) /usr/bin/sudo systemctl daemon-reload
	ssh $(BINKYHOSTUSER)@$(BINKYHOST) /usr/bin/sudo systemctl stop binkyrailways
	scp bin/linux/arm/binky-server $(BINKYHOSTUSER)@$(BINKYHOST):/home/$(BINKYHOSTUSER)/binky-server
	ssh $(BINKYHOSTUSER)@$(BINKYHOST) /usr/bin/sudo touch /home/$(BINKYHOSTUSER)/binky-server.env
	ssh $(BINKYHOSTUSER)@$(BINKYHOST) /usr/bin/sudo systemctl restart binkyrailways
	ssh $(BINKYHOSTUSER)@$(BINKYHOST) /usr/bin/sudo systemctl enable binkyrailways

