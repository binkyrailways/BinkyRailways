using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Model.Impl;
using BinkyRailways.WinApp.Controls.Edit.Settings;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Forms
{
    partial class RouteEventSetEditorForm
    {
        /// <summary>
        /// Node for an event.
        /// </summary>
        private class BehaviorNode : TreeNode, IGatherProperties
        {
            private readonly IRailway railway;

            /// <summary>
            /// Default ctor
            /// </summary>
            public BehaviorNode(IRailway railway)
            {
                this.railway = railway;
                AppliesTo = new LocStandardPredicate();
                StateBehavior = DefaultValues.DefaultRouteEventBehaviorStateBehavior;
                SpeedBehavior = DefaultValues.DefaultRouteEventBehaviorSpeedBehavior;
                UpdateText();
            }

            /// <summary>
            /// Default ctor
            /// </summary>
            public BehaviorNode(IRailway railway, IRouteEventBehavior behavior)
            {
                this.railway = railway;
                AppliesTo = behavior.AppliesTo.Clone(true);
                StateBehavior = behavior.StateBehavior;
                SpeedBehavior = behavior.SpeedBehavior;
                UpdateText();
            }

            /// <summary>
            /// Gets the current railway
            /// </summary>
            IRailway IGatherProperties.Railway
            {
                get { return railway; }
            }

            /// <summary>
            /// Add all visible properties of this settings object to the given property collection.
            /// </summary>
            void IGatherProperties.GatherProperties(ExPropertyDescriptorCollection properties)
            {
                properties.Add(() => AppliesTo, Strings.TabGeneral, "Applies to", "Select the locs to which these rule applies");
                properties.Add(() => StateBehavior, Strings.TabGeneral, "State change", "Select how the state of the route will change when this event occurs");
                if (StateBehavior != RouteStateBehavior.Reached)
                    properties.Add(() => SpeedBehavior, Strings.TabGeneral, "Speed change", "Select how the speed of the loc will change when this event occurs");
            }

            /// <summary>
            /// Applies to predicate
            /// </summary>
            [TypeConverter(typeof(EntityTypeConverter))]
            [Editor(typeof(LocPredicateEditor), typeof(UITypeEditor))]
            [MergableProperty(false)]
            public ILocPredicate AppliesTo { get; private set; }

            /// <summary>
            /// Change of state
            /// </summary>
            [TypeConverter(typeof(RouteStateBehaviorTypeConverter))]
            public RouteStateBehavior StateBehavior { get; set; }

            /// <summary>
            /// Change of speed
            /// </summary>
            [TypeConverter(typeof(LocSpeedBehaviorTypeConverter))]
            public LocSpeedBehavior SpeedBehavior { get; set; }

            /// <summary>
            /// Update the text property.
            /// </summary>
            internal void UpdateText()
            {
                Text = string.Format(Strings.BehaviorNodeText, AppliesTo, StateBehavior, SpeedBehavior);
                if (Text.Contains("?"))
                {
                    
                }
            }
        }
    }
}
