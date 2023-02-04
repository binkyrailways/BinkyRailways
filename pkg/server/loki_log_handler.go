// Copyright 2023 Ewout Prangsma
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Author Ewout Prangsma
//

package server

import (
	"fmt"
	"net/http"
	"sort"
	"strconv"
	"strings"
	"sync"
	"time"

	"github.com/binkynet/BinkyNet/loki"
	"github.com/labstack/echo/v4"
	"github.com/rs/zerolog"
)

const (
	colorBlack = iota + 30
	colorRed
	colorGreen
	colorYellow
	colorBlue
	colorMagenta
	colorCyan
	colorWhite

	colorBold     = 1
	colorDarkGray = 90
)

func (s *Server) handlePushLokiRequest(c echo.Context) error {
	var streams loki.PushRequest
	if err := c.Bind(&streams); err != nil {
		s.log.Warn().Err(err).Msg("Bad push request")
		return c.String(http.StatusBadRequest, "Bad push request")
	}
	for _, stream := range streams.Streams {
		header := formatStreamHeader(stream)
		for _, line := range stream.Values {
			if len(line) != 2 {
				continue
			}
			level := "?"
			msg := line[1]
			// Try to parse level prefix
			if msgParts := strings.SplitN(msg, " ", 2); len(msgParts) == 2 {
				if _, err := zerolog.ParseLevel(msgParts[0]); err == nil {
					level = msgParts[0]
					msg = msgParts[1]
				}
			}
			// Time stamp
			nanos, _ := strconv.ParseInt(line[0], 10, 64)
			ts := time.Unix(0, nanos)
			fmt.Print(ts.Format("15:04:05.000"))
			fmt.Print(" ")
			// Level
			fmt.Print(colorizeLevel(level))
			fmt.Print(" ")
			// Job header
			fmt.Print(header)
			fmt.Print(" ")
			// Content
			fmt.Println(msg)
		}
	}
	return c.String(http.StatusNoContent, "")
}

// formatStreamHeader returns a sorted key=value list for the Stream part of the given adapter.
func formatStreamHeader(stream loki.StreamAdapter) string {
	list := make([]string, 0, len(stream.Stream))
	for k, v := range stream.Stream {
		list = append(list, colorize(k, v))
	}
	sort.Strings(list)
	return strings.Join(list, " ")
}

var (
	jobColorPairs = [][]int{
		// Ext. background, foregroup
		{52, 37},
		{64, 37},
		{88, 37},
		{100, 37},
		{124, 37},
		{136, 37},
		{148, 30},
		{172, 30},
		{208, 30},
		{55, 37},
		{68, 37},
		{92, 37},
		{104, 37},
		{128, 37},
		{140, 37},
		{152, 30},
		{176, 30},
		{212, 30},
	}
	lastJobColorIndex = 0
	jobColorIndexes   = make(map[string]int)
	jobColorsMutex    sync.Mutex
)

const (
	jobValueLength = 10
)

// colorize returns the string s wrapped in ANSI code c, unless disabled is true.
func colorize(k, v string) string {
	if k != "job" {
		return k + "=" + v
	}
	// Pad job value length
	if len(v) < jobValueLength {
		v = v + strings.Repeat(" ", jobValueLength-len(v))
	}
	// Determine job color
	jobColorsMutex.Lock()
	defer jobColorsMutex.Unlock()
	idx, found := jobColorIndexes[v]
	if !found {
		lastJobColorIndex++
		idx = lastJobColorIndex
		jobColorIndexes[v] = idx
	}
	// Return colorized job=value
	bg := jobColorPairs[idx][0]
	fg := jobColorPairs[idx][1]
	return fmt.Sprintf("\x1b[%dm\x1b[48;5;%dm%s\x1b[0m", fg, bg, v)
}

func colorizeLevel(level string) string {
	// colorize returns the string s wrapped in ANSI code c, unless disabled is true.
	colorize := func(s interface{}, c int) string {
		return fmt.Sprintf("\x1b[%dm%v\x1b[0m", c, s)
	}

	switch level {
	case "trace":
		return colorize("TRC", colorMagenta)
	case "debug":
		return colorize("DBG", colorYellow)
	case "info":
		return colorize("INF", colorGreen)
	case "warn":
		return colorize("WRN", colorRed)
	case "error":
		return colorize(colorize("ERR", colorRed), colorBold)
	case "fatal":
		return colorize(colorize("FTL", colorRed), colorBold)
	case "panic":
		return colorize(colorize("PNC", colorRed), colorBold)
	default:
		return colorize("???", colorBold)
	}
}
