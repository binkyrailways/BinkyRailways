using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Forms
{
    partial class RouteEventSetEditorForm
    {
        /// <summary>
        /// Node for an event.
        /// </summary>
        private class EventNode : TreeNode
        {
            private readonly IRailway railway;
            private readonly ISensor sensor;

            /// <summary>
            /// Default ctor
            /// </summary>
            public EventNode(IRailway railway, ISensor sensor)
            {
                this.railway = railway;
                this.sensor = sensor;
                Text = sensor.ToString();
            }

            /// <summary>
            /// Default ctor
            /// </summary>
            public EventNode(IRailway railway, IRouteEvent @event)
                : this(railway, @event.Sensor)
            {
                Nodes.AddRange(@event.Behaviors.Select(x => new BehaviorNode(railway, x)).ToArray());
                Expand();
            }

            /// <summary>
            /// The sensor of this node.
            /// </summary>
            public ISensor Sensor
            {
                get { return sensor; }
            }
        }
    }
}
