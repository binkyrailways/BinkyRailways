module github.com/binkyrailways/BinkyRailways

go 1.24.8

require (
	github.com/binkynet/BinkyNet v1.13.0
	github.com/binkynet/LocalWorker v0.3.1-0.20260309184747-8340c70a36ad
	github.com/binkynet/NetManager v1.10.2
	github.com/binkynet/bidib v0.0.0-20231022123849-25701f6a5d6a
	github.com/cenkalti/backoff v2.2.1+incompatible
	github.com/dchest/uniuri v1.2.0
	github.com/golang/protobuf v1.5.4
	github.com/improbable-eng/grpc-web v0.15.0
	github.com/labstack/echo/v4 v4.12.0
	github.com/mattn/go-pubsub v0.0.0-20160821075316-7a151c7747cd
	github.com/mochi-mqtt/server/v2 v2.6.5
	github.com/prometheus/client_golang v1.20.0
	github.com/rs/zerolog v1.18.0
	github.com/samber/lo v1.51.0
	github.com/spf13/cobra v1.8.1
	github.com/stretchr/testify v1.9.0
	go.uber.org/multierr v1.6.0
	golang.org/x/net v0.49.0
	golang.org/x/sync v0.19.0
	google.golang.org/grpc v1.32.0
	gopkg.in/yaml.v3 v3.0.1
)

replace github.com/binkynet/NetManager => ../NetManager

require (
	github.com/anmitsu/go-shlex v0.0.0-20200514113438-38f4b401e2be // indirect
	github.com/aymanbagabas/go-osc52/v2 v2.0.1 // indirect
	github.com/beorn7/perks v1.0.1 // indirect
	github.com/cenkalti/backoff/v4 v4.1.1 // indirect
	github.com/cespare/xxhash/v2 v2.3.0 // indirect
	github.com/charmbracelet/bubbles v0.18.0 // indirect
	github.com/charmbracelet/bubbletea v0.27.0 // indirect
	github.com/charmbracelet/keygen v0.5.0 // indirect
	github.com/charmbracelet/lipgloss v0.12.1 // indirect
	github.com/charmbracelet/log v0.4.0 // indirect
	github.com/charmbracelet/ssh v0.0.0-20240725163421-eb71b85b27aa // indirect
	github.com/charmbracelet/wish v1.4.1 // indirect
	github.com/charmbracelet/x/ansi v0.1.4 // indirect
	github.com/charmbracelet/x/conpty v0.1.0 // indirect
	github.com/charmbracelet/x/errors v0.0.0-20240508181413-e8d8b6e2de86 // indirect
	github.com/charmbracelet/x/exp/term v0.0.0-20240503143715-36ea203beff4 // indirect
	github.com/charmbracelet/x/input v0.1.0 // indirect
	github.com/charmbracelet/x/term v0.1.1 // indirect
	github.com/charmbracelet/x/termios v0.1.0 // indirect
	github.com/charmbracelet/x/windows v0.1.0 // indirect
	github.com/creack/pty v1.1.21 // indirect
	github.com/davecgh/go-spew v1.1.1 // indirect
	github.com/desertbit/timer v0.0.0-20180107155436-c41aec40b27f // indirect
	github.com/dustin/go-humanize v1.0.1 // indirect
	github.com/ecc1/gpio v0.0.0-20230226182448-afe57342d422 // indirect
	github.com/eclipse/paho.mqtt.golang v1.5.0 // indirect
	github.com/erikgeiser/coninput v0.0.0-20211004153227-1c3628e74d0f // indirect
	github.com/ewoutp/go-aggregate-error v0.0.0-20141209171456-e0dbde632d55 // indirect
	github.com/go-logfmt/logfmt v0.6.0 // indirect
	github.com/gogo/protobuf v1.3.2 // indirect
	github.com/gorilla/websocket v1.5.3 // indirect
	github.com/grandcat/zeroconf v1.0.0 // indirect
	github.com/grpc-ecosystem/go-grpc-middleware v1.4.0 // indirect
	github.com/grpc-ecosystem/go-grpc-prometheus v1.2.0 // indirect
	github.com/inconshreveable/mousetrap v1.1.0 // indirect
	github.com/klauspost/compress v1.17.9 // indirect
	github.com/labstack/gommon v0.4.2 // indirect
	github.com/lucasb-eyer/go-colorful v1.2.0 // indirect
	github.com/mattn/go-colorable v0.1.13 // indirect
	github.com/mattn/go-isatty v0.0.20 // indirect
	github.com/mattn/go-localereader v0.0.1 // indirect
	github.com/mattn/go-runewidth v0.0.15 // indirect
	github.com/miekg/dns v1.1.27 // indirect
	github.com/muesli/ansi v0.0.0-20230316100256-276c6243b2f6 // indirect
	github.com/muesli/cancelreader v0.2.2 // indirect
	github.com/muesli/termenv v0.15.3-0.20240509142007-81b8f94111d5 // indirect
	github.com/munnerz/goautoneg v0.0.0-20191010083416-a7dc8b61c822 // indirect
	github.com/pkg/errors v0.9.1 // indirect
	github.com/pmezard/go-difflib v1.0.0 // indirect
	github.com/prometheus/client_model v0.6.1 // indirect
	github.com/prometheus/common v0.55.0 // indirect
	github.com/prometheus/procfs v0.15.1 // indirect
	github.com/rivo/uniseg v0.4.7 // indirect
	github.com/rs/cors v1.7.0 // indirect
	github.com/rs/xid v1.4.0 // indirect
	github.com/spf13/pflag v1.0.5 // indirect
	github.com/stretchr/objx v0.5.2 // indirect
	github.com/tarm/serial v0.0.0-20180830185346-98f6abe2eb07 // indirect
	github.com/valyala/bytebufferpool v1.0.0 // indirect
	github.com/valyala/fasttemplate v1.2.2 // indirect
	github.com/xo/terminfo v0.0.0-20220910002029-abceb7e1c41e // indirect
	go.uber.org/atomic v1.7.0 // indirect
	golang.org/x/crypto v0.48.0 // indirect
	golang.org/x/exp v0.0.0-20231006140011-7918f672742d // indirect
	golang.org/x/sys v0.41.0 // indirect
	golang.org/x/text v0.34.0 // indirect
	google.golang.org/genproto v0.0.0-20210126160654-44e461bb6506 // indirect
	google.golang.org/protobuf v1.34.2 // indirect
	nhooyr.io/websocket v1.8.6 // indirect
)
