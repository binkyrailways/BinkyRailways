#!/bin/bash

# Run loki, prometheus & grafana in docker compose
# and run binky-server telemetry agent locally.

docker compose -f scripts/compose.yaml up --detach
./bin/darwin/$(arch)/binky-server telemetry agent