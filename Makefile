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
		github.com/binkynet/NetManager@v0.3.1 \
		github.com/binkynet/BinkyNet@v0.8.1
	go mod tidy

update-gioui:
	go get -u \
		gioui.org \
		gioui.org/x