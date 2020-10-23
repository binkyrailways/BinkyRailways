FROM scratch
ARG GOARCH=amd64

ADD ../bin/linux/${GOARCH}/binky-server /app/

ENTRYPOINT ["/app/binky-server"]
