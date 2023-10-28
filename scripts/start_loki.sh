#!/bin/bash

SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

docker run -d --rm --name loki -v ${SCRIPT_DIR}:/mnt/config -p 3100:3100 grafana/loki:2.4.2 -config.file=/mnt/config/loki-config.yaml
docker run -d --rm --name promtail -v ${SCRIPT_DIR}:/mnt/config -v ${SCRIPT_DIR}/..:/var/log --link loki grafana/promtail:2.4.2 -config.file=/mnt/config/promtail-config.yaml
docker run -d  --name prometheus -p 9090:9090 -v ${SCRIPT_DIR}/prometheus.yml:/etc/prometheus/prometheus.yml prom/prometheus \
    --config.file=/etc/prometheus/prometheus.yml \
    --storage.tsdb.path=/prometheus \
    --web.console.libraries=/usr/share/prometheus/console_libraries \
    --web.console.templates=/usr/share/prometheus/consoles \
    --web.enable-lifecycle

docker run -d --rm --name grafana -v ${SCRIPT_DIR}:/etc/grafana:ro -p 3000:3000 --link loki --link prometheus grafana/grafana --config=/etc/grafana/grafana.ini