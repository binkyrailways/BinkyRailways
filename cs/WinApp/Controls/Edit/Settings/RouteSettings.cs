using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.Properties;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class RouteSettings : EntitySettings<IRoute>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal RouteSettings(IRoute entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => From, Strings.TabGeneral, Strings.RouteFromName, Strings.RouteFromHelp);
            if (!(From is IEdge))
            {
                properties.Add(() => FromBlockSide, Strings.TabGeneral, Strings.RouteFromSideName, Strings.RouteFromSideHelp);
            }
            properties.Add(() => To, Strings.TabGeneral, Strings.RouteToName, Strings.RouteToHelp);
            if (!(To is IEdge))
            {
                properties.Add(() => ToBlockSide, Strings.TabGeneral, Strings.RouteToSideName, Strings.RouteToSideHelp);
            }
            properties.Add(() => Events, Strings.TabBehavior, "Events", "Events that control the state of this route and the speed of the loc that takes this route");
            properties.Add(() => JunctionsWithState, Strings.TabBehavior, Resources.RouteJunctionsName, Resources.RouteJunctionsHelp);
            properties.Add(() => Speed, Strings.TabBehavior, Resources.RouteSpeedName, Resources.RouteSpeedHelp);
            properties.Add(() => ChooseProbability, Strings.TabAdvBehavior, Strings.ChooseProbabilityName, Strings.ChooseProbabilityHelp);
            properties.Add(() => Permissions, Strings.TabAdvBehavior, Strings.RoutePermissionsName, Strings.RoutePermissionsHelp);
            properties.Add(() => Closed, Strings.TabAdvBehavior, Strings.RouteClosedName, Strings.RouteClosedHelp);
            properties.Add(() => MaxDuration, Strings.TabAdvBehavior, Strings.RouteMaxDurationName, Strings.RouteMaxDurationHelp);
            properties.Add(() => ActionTriggerSource, Strings.TabBehavior, Strings.ActionsName, Strings.ActionsHelp);
        }

        /// <summary>
        /// Starting point of the route
        /// </summary>
        [TypeConverter(typeof(EntityTypeConverter))]
        [Editor(typeof(EndPointEditor), typeof(UITypeEditor))]
        public IEndPoint From
        {
            get { return Entity.From; }
            set { Entity.From = value; }
        }

        /// <summary>
        /// Side of the <see cref="From"/> block at which this route will leave that block.
        /// </summary>
        [TypeConverter(typeof(BlockSideTypeConverter))]
        [DefaultValue(BlockSide.Front)]
        public BlockSide FromBlockSide
        {
            get { return Entity.FromBlockSide; }
            set { Entity.FromBlockSide = value; }
        }

        /// <summary>
        /// End point of the route
        /// </summary>
        [TypeConverter(typeof(EntityTypeConverter))]
        [Editor(typeof(EndPointEditor), typeof(UITypeEditor))]
        public IEndPoint To
        {
            get { return Entity.To; }
            set { Entity.To = value; }
        }

        /// <summary>
        /// Side of the <see cref="To"/> block at which this route will enter that block.
        /// </summary>
        [TypeConverter(typeof(BlockSideTypeConverter))]
        [DefaultValue(BlockSide.Back)]
        public BlockSide ToBlockSide
        {
            get { return Entity.ToBlockSide; }
            set { Entity.ToBlockSide = value; }
        }

        /// <summary>
        /// Set of sensors which (when active) indicate that a loc is has entered
        /// the To block. The loc will then slow down, but not stop.
        /// </summary>
        [TypeConverter(typeof(RouteEventSetTypeConverter))]
        [Editor(typeof(RouteEventSetEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public IRouteEventSet Events
        {
            get { return Entity.Events; }
        }

        /// <summary>
        /// Set of junctions which are crossed when taking this route.
        /// </summary>
        [TypeConverter(typeof(JunctionWithStateSetTypeConverter))]
        [Editor(typeof(JunctionWithStateSetEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public IJunctionWithStateSet JunctionsWithState
        {
            get { return Entity.CrossingJunctions; }
        }

        /// <summary>
        /// Speed of locs when going this route.
        /// This value is a percentage of the maximum / medium speed of the loc.
        /// </summary>
        /// <value>0..100</value>
        [TypeConverter(typeof(PercentageTypeConverter))]
        [DefaultValue(DefaultValues.DefaultRouteSpeed)]
        [EditableInRunningState]
        public int Speed
        {
            get { return Entity.Speed; }
            set { Entity.Speed = value; }
        }

        /// <summary>
        /// Probability (in percentage) that a loc will take this route.
        /// When multiple routes are available to choose from the route with the highest probability will have the highest
        /// chance or being chosen.
        /// </summary>
        /// <value>0..100</value>
        [TypeConverter(typeof(PercentageTypeConverter))]
        [DefaultValue(DefaultValues.DefaultRouteChooseProbability)]
        [EditableInRunningState]
        public int ChooseProbability
        {
            get { return Entity.ChooseProbability; }
            set { Entity.ChooseProbability = value; }
        }

        /// <summary>
        /// Route permission
        /// </summary>
        [TypeConverter(typeof(EntityTypeConverter))]
        [Editor(typeof(LocPredicateEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public ILocPredicate Permissions
        {
            get { return Entity.Permissions; }
        }

        /// <summary>
        /// Is this route open for traffic or not?
        /// Setting to true, allows for maintance etc. on this route.
        /// </summary>
        [DefaultValue(false)]
        [EditableInRunningState]
        public bool Closed
        {
            get { return Entity.Closed; }
            set { Entity.Closed = value; }
        }

        /// <summary>
        /// Speed of locs when going this route.
        /// This value is a percentage of the maximum / medium speed of the loc.
        /// </summary>
        /// <value>0..100</value>
        [TypeConverter(typeof(DurationInSecondsTypeConverter))]
        [DefaultValue(DefaultValues.DefaultRouteMaxDuration)]
        [EditableInRunningState]
        public int MaxDuration
        {
            get { return Entity.MaxDuration; }
            set { Entity.MaxDuration = value; }
        }

        /// <summary>
        /// Actions
        /// </summary>
        [TypeConverter(typeof(ActionTriggerSourceTypeConverter))]
        [Editor(typeof(ActionTriggerSourceEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public IActionTriggerSource ActionTriggerSource
        {
            get { return Entity; }
        }

        /// <summary>
        /// Should the description property be shown?
        /// </summary>
        protected override bool ShowDescription
        {
            get { return false; }
        }
    }
}
