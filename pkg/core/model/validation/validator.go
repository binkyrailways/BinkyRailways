package validation

import (
	"fmt"
	"sort"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Entity validator
type validator struct {
	model.EntityVisitor

	findings []model.Finding
}

// Validate the given entity
func Validate(e model.Entity) []model.Finding {
	v := &validator{}
	v.EntityVisitor = model.NewAllEntityVisitor(v)
	fmt.Printf("Start validing %T %s\n", e, e.GetDescription())
	e.Accept(v)
	sort.Slice(v.findings, func(i, j int) bool { return v.findings[i].GetDescription() < v.findings[j].GetDescription() })
	return v.findings
}
