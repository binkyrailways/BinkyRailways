package validation

import (
	"sort"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Entity validator
type validator struct {
	model.AllEntityVisitor

	findings []model.Finding
}

// Validate the given entity
func Validate(e model.Entity) []model.Finding {
	v := &validator{}
	v.Visitor = v
	e.Accept(v)
	sort.Slice(v.findings, func(i, j int) bool { return v.findings[i].GetDescription() < v.findings[j].GetDescription() })
	return v.findings
}
