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
		github.com/binkynet/BinkyNet@v0.9.2
	go mod tidy

update-gioui:
	go get -u \
		gioui.org@e68ee35c86cb317e36af2b67cbaab9a4c4f41bca \
		gioui.org/x