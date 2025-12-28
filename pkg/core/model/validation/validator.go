package validation

import "github.com/binkyrailways/BinkyRailways/pkg/core/model"

// Entity validator
type validator struct {
	model.AllEntityVisitor

	findings []model.Finding
}

type Validator interface {
	model.EntityVisitor
	// Return all findings
	GetFindings() []model.Finding
}

// Construct and initialize a new validator
func NewValidator() Validator {
	v := &validator{}
	v.Visitor = v
	return v
}

// Return all findings
func (v *validator) GetFindings() []model.Finding {
	return v.findings
}
