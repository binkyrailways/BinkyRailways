// Copyright 2020 Ewout Prangsma
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

package model

// RouteEventBehaviorList is a list of route event behaviors.
type RouteEventBehaviorList interface {
	// Get number of entries
	GetCount() int

	// Invoke the callback for each item
	ForEach(cb func(RouteEventBehavior))

	// Remove the given item from this set.
	// Returns true if it was removed, false otherwise
	Remove(item RouteEventBehavior) bool

	// Remove all items.
	// Returns true if one or more items were removed, false otherwise
	Clear() bool

	// Add a blank route behavior to the list
	AddNew() RouteEventBehavior

	/// <summary>
	/// Add the given item to this set
	/// </summary>
	//IRouteEventBehavior Add(ILocPredicate appliesTo);
}
