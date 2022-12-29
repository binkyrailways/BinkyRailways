package eventlog

import (
	"context"
	"fmt"
	"os"
	"path/filepath"
	"strconv"
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/rs/zerolog"
)

// NewEventLogger creates a new event logger implementation
func NewEventLogger(ctx context.Context, railway state.Railway, log zerolog.Logger) (state.EventLogger, error) {
	now := time.Now().Local()
	name := fmt.Sprintf("binky-%s.log", now.Format("2006-01-02-15-04"))
	el := &eventLogger{
		start:   time.Now(),
		log:     log,
		path:    filepath.Join("logs", name),
		railway: railway,
		events:  make(chan eventWithTime, 128),
	}
	el.enabled.eventLogger = el
	el.enabled.SetRequested(ctx, true)
	railway.Subscribe(ctx, el.onEvent)
	return el, nil
}

type eventLogger struct {
	start   time.Time
	log     zerolog.Logger
	path    string
	railway state.Railway
	enabled enabledProperty
	events  chan eventWithTime
}

type eventWithTime struct {
	ts    time.Time
	event state.Event
}

// Is logging enabled?
func (el *eventLogger) GetEnabled() state.BoolProperty {
	return &el.enabled
}

// Close the event logger
func (el *eventLogger) Close(ctx context.Context) {
	close(el.events)
}

// onEvent logs the event
func (el *eventLogger) onEvent(evt state.Event) {
	if el.enabled.requested {
		el.events <- eventWithTime{
			ts:    time.Now(),
			event: evt,
		}
	}
}

func (el *eventLogger) startWritingEvents() {
	go el.writeEvents()
}

// writeEvents stores events in the log file
func (el *eventLogger) writeEvents() {
	// Ensure directory exists
	dir := filepath.Dir(el.path)
	os.MkdirAll(dir, 0744)
	// Open file
	f, err := os.OpenFile(el.path, os.O_APPEND|os.O_CREATE|os.O_RDWR, 0644)
	if err != nil {
		el.log.Error().Err(err).Msg("Failed to open event log file")
		el.enabled.SetRequested(context.Background(), false)
	}
	defer f.Close()
	for evt := range el.events {
		kvs := evt.event.LogFormat()
		timeDelta := evt.ts.Sub(el.start)
		for idx, kv := range kvs {
			if idx == 0 {
				f.WriteString(fmt.Sprintf(`{ "ts": "%02d:%02d:%02d.%03d" `,
					int(timeDelta.Hours()),
					int(timeDelta.Minutes())%60,
					int(timeDelta.Seconds())%60,
					int(timeDelta.Milliseconds())%1000,
				))
			} else {
				f.WriteString(", ")
			}
			f.WriteString(fmt.Sprintf("%s: %s", strconv.Quote(kv.Key), strconv.Quote(kv.Value)))
		}
		f.WriteString(" }\n")
	}
}
