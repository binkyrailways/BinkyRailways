using System;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Controls;

namespace BinkyRailways.WinApp.Forms
{
    /// <summary>
    /// Editor for sensor sets.
    /// </summary>
    public partial class RouteEventSetEditorForm : AppForm, IPredicateChangedListener
    {
        private readonly IRailway railway;
        private readonly IModule module;
        private IRouteEventSet events;

        /// <summary>
        /// Default ctor
        /// </summary>
        [Obsolete("Designer only")]
        public RouteEventSetEditorForm()
            : this(null, null, null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteEventSetEditorForm(IRailway railway, IModule module, IRouteEventSet events)
        {
            this.railway = railway;
            this.module = module;
            InitializeComponent();
            Initialize(events);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            behaviorGrid.RegisterService<IPredicateChangedListener>(this);
        }

        /// <summary>
        /// Initialize the controls for the given set.
        /// </summary>
        private void Initialize(IRouteEventSet events)
        {
            this.events = events;
            lbAll.Items.Clear();
            lbSet.Nodes.Clear();
            behaviorGrid.SelectedObject = null;
            if (module != null)
            {
                foreach (var sensor in module.Sensors.Where(x => events.All(e => e.Sensor != x)).OrderBy(x => x.Description, NameWithNumbersComparer.Instance))
                {
                    lbAll.Items.Add(sensor);
                }
                foreach (var @event in events.OrderBy(x => x.Description, NameWithNumbersComparer.Instance))
                {
                    lbSet.Nodes.Add(new EventNode(railway, @event));
                }
                if (lbAll.Items.Count > 0)
                {
                    lbAll.SelectedIndex = 0;
                }
                if (lbSet.Nodes.Count > 0)
                {
                    lbSet.SelectedNode = lbSet.Nodes[0];
                }
                lbSetSensors.Text = "Events";
            }
            UpdateComponents();
        }

        /// <summary>
        /// Update the state of the components
        /// </summary>
        private void UpdateComponents()
        {
            var hasLeftSelection = (lbAll.SelectedIndex >= 0);
            var hasEventSelection = (lbSet.SelectedNode is EventNode);
            var behaviorNode = lbSet.SelectedNode as BehaviorNode;
            cmdAdd.Enabled = hasLeftSelection;
            cmdRemove.Enabled = hasEventSelection;
            cmdAddBehavior.Enabled = hasEventSelection || (behaviorNode != null);
            cmdRemoveBehavior.Enabled = (behaviorNode != null) && (behaviorNode.Parent.Nodes.Count >= 2);
            cmdUpBehavior.Enabled = (behaviorNode != null) && (behaviorNode.Index > 0);
            cmdDownBehavior.Enabled = (behaviorNode != null) && (behaviorNode.Index < behaviorNode.Parent.Nodes.Count - 1);
            behaviorGrid.SelectedObject = lbSet.SelectedNode as BehaviorNode;
        }

        /// <summary>
        /// Loc selection changed.
        /// </summary>
        private void lbLocs_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateComponents();
        }

        /// <summary>
        /// Event selection changed
        /// </summary>
        private void lbSet_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateComponents();
        }

        /// <summary>
        /// Add selected to set.
        /// </summary>
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            var item = lbAll.SelectedItem as ISensor;
            if (item != null)
            {
                lbAll.Items.Remove(item);
                var eventNode = new EventNode(railway, item);
                var behaviorNode = new BehaviorNode(railway);
                eventNode.Nodes.Add(behaviorNode);
                lbSet.Nodes.Add(eventNode);
                lbSet.SelectedNode = behaviorNode;
            }
        }

        /// <summary>
        /// Add on double click
        /// </summary>
        private void lbAll_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbAll.SelectedItemContains(e.Location))
            {
                cmdAdd_Click(sender, e);
            }
        }

        /// <summary>
        /// Remove from set to all.
        /// </summary>
        private void cmdRemove_Click(object sender, EventArgs e)
        {
            var node = lbSet.SelectedNode as EventNode;
            if (node == null)
                return;
            lbSet.Nodes.Remove(node);
            lbAll.Items.Add(node.Sensor);
        }

        /// <summary>
        /// Add a new behavior
        /// </summary>
        private void cmdAddBehavior_Click(object sender, EventArgs e)
        {
            var node = lbSet.SelectedNode as EventNode;
            if ((node == null) && (lbSet.SelectedNode is BehaviorNode))
            {
                node = (EventNode) lbSet.SelectedNode.Parent;
            }
            if (node == null)
                return;
            var newNode = new BehaviorNode(railway);
            node.Nodes.Add(newNode);
            lbSet.SelectedNode = newNode;
            lbSet.Select();
        }

        /// <summary>
        /// Remove the selected behavior
        /// </summary>
        private void cmdRemoveBehavior_Click(object sender, EventArgs e)
        {
            var node = lbSet.SelectedNode as BehaviorNode;
            var parent = (node != null) ? node.Parent : null;
            if ((node == null) || (parent == null))
                return;
            parent.Nodes.Remove(node);
            lbSet.Select();
        }

        /// <summary>
        /// Move selected behavior up
        /// </summary>
        private void cmdUpBehavior_Click(object sender, EventArgs e)
        {
            var node = lbSet.SelectedNode as BehaviorNode;
            var parent = (node != null) ? node.Parent : null;
            if ((node == null) || (parent == null))
                return;
            lbSet.BeginUpdate();
            var index = Math.Max(0, node.Index - 1);
            parent.Nodes.Remove(node);
            parent.Nodes.Insert(index, node);
            lbSet.SelectedNode = node;
            lbSet.EndUpdate();
            if (index == 0)
            {
                lbSet.Select();
            }
        }

        /// <summary>
        /// Move selected behavior node down
        /// </summary>
        private void cmdDownBehavior_Click(object sender, EventArgs e)
        {
            var node = lbSet.SelectedNode as BehaviorNode;
            var parent = (node != null) ? node.Parent : null;
            if ((node == null) || (parent == null))
                return;
            lbSet.BeginUpdate();
            var index = Math.Min(node.Index + 1, parent.Nodes.Count - 1);
            parent.Nodes.Remove(node);
            parent.Nodes.Insert(index, node);
            lbSet.SelectedNode = node;
            lbSet.EndUpdate();
            if (index == parent.Nodes.Count - 1)
            {
                lbSet.Select();
            }
        }

        /// <summary>
        /// Remove from set.
        /// </summary>
        private void lbSet_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*if (lbSet.SelectedItemContains(e.Location))
            {
                cmdRemove_Click(sender, e);
            }*/
        }

        /// <summary>
        /// Commit change
        /// </summary>
        private void cmdOk_Click(object sender, EventArgs e)
        {
            events.Clear();
            foreach (var eventNode in lbSet.Nodes.Cast<EventNode>())
            {
                var @event = events.Add(eventNode.Sensor);
                foreach (var behaviorNode in eventNode.Nodes.Cast<BehaviorNode>())
                {
                    var behavior = @event.Behaviors.Add(behaviorNode.AppliesTo);
                    behavior.StateBehavior = behaviorNode.StateBehavior;
                    behavior.SpeedBehavior = behaviorNode.SpeedBehavior;
                }                
            }
        }

        /// <summary>
        /// A behavior property has changed.
        /// </summary>
        private void behaviorGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var node = lbSet.SelectedNode as BehaviorNode;
            if (node != null)
                node.UpdateText();
        }

        /// <summary>
        /// Update text on predicate changed.
        /// </summary>
        void IPredicateChangedListener.PredicateChanged(ILocPredicate predicate)
        {
            foreach (var eventNode in lbSet.Nodes.Cast<EventNode>())
            {
                foreach (var behaviorNode in eventNode.Nodes.Cast<BehaviorNode>())
                {
                    if (behaviorNode.AppliesTo == predicate)
                    {
                        behaviorNode.UpdateText();
                        return;
                    }
                }
            }
        }
    }
}
