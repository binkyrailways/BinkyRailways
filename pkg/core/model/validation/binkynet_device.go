package validation

import (
	"fmt"
	"strings"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Validate this entity, returning all findings.
func (v *validator) VisitBinkyNetDevice(d model.BinkyNetDevice) interface{} {
	if lw := d.GetLocalWorker(); lw != nil {
		// Iterate over all objects and check for multiple usage of the same pin
		pinsUsedBy := make(map[api.DeviceIndex][]string)
		for obj := range lw.GetObjects().All() {
			for conn := range obj.GetConnections().All() {
				for pin := range conn.GetPins().All() {
					if pin.GetDeviceID() == d.GetDeviceID() {
						// Pin using this device
						usedBy := fmt.Sprintf("%s[%s]", obj.GetObjectID(), conn.GetKey())
						if index := pin.GetIndex(); index > 0 {
							pinsUsedBy[index] = append(pinsUsedBy[index], usedBy)
						}
					}
				}
			}
		}
		for index, allUsedBy := range pinsUsedBy {
			if len(allUsedBy) > 1 {
				v.findings = append(v.findings, Finding(fmt.Sprintf("Pin %d of %s in %s is used by %s", index, d.GetDeviceID(), lw.GetDescription(), strings.Join(allUsedBy, " & "))))
			}
		}
	}
	return v.AllEntityVisitor.VisitBinkyNetDevice(d)
}
