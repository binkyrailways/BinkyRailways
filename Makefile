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
