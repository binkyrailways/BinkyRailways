SHELL = bash
PROJECT := binkyrailways

all: binaries

clean:
	rm -Rf bin

binaries:
	gox \
		-osarch="darwin/amd64" \
		-ldflags="-X main.projectVersion=${VERSION} -X main.projectBuild=${COMMIT}" \
		-output="bin/{{.OS}}/{{.Arch}}/$(PROJECT)" \
		-tags="netgo" \
		./...

bootstrap:
	go get github.com/mitchellh/gox

test:
	go test -v ./...

update-modules:
	go get \
		github.com/binkynet/NetManager@v0.4.0 \
		github.com/binkynet/BinkyNet@v0.9.5
	go mod tidy

update-gioui:
	go get -u \
		gioui.org@941aeaae910e011edf9707f2a9c40e1a32d06ac7 \
		gioui.org/x