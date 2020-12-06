using System.Windows.Forms;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Forms
{
    partial class ActionsEditorForm
    {
        private abstract class Item : TreeNode
        {
            /// <summary>
            /// Can actions be added here?
            /// </summary>
            internal abstract bool CanAdd { get; }

            /// <summary>
            /// Add the given action.
            /// </summary>
            internal virtual Item Add(IAction action)
            {
                // Override me
                return null;
            }

            /// <summary>
            /// Save me.
            /// </summary>
            internal virtual void Save() { /* Override me */ }

            /// <summary>
            /// A property value has changed. 
            /// Update node.
            /// </summary>
            internal abstract void OnValueChanged();
        }

        private class ActionItem : Item
        {
            private readonly IAction action;

            /// <summary>
            /// Default ctor
            /// </summary>
            public ActionItem(IAction action)
            {
                this.action = action.Clone();
                Text = action.Description;
            }

            /// <summary>
            /// Gets the cloned action
            /// </summary>
            public IAction Action
            {
                get { return action; }
            }

            /// <summary>
            /// Can actions be added here?
            /// </summary>
            internal override bool CanAdd { get { return false; } }

            /// <summary>
            /// A property value has changed. 
            /// Update node.
            /// </summary>
            internal override void OnValueChanged()
            {
                Text = action.Description;
            }
        }

        /// <summary>
        /// Node for an action trigger
        /// </summary>
        private class TriggerItem : Item
        {
            private readonly IActionTrigger trigger;

            /// <summary>
            /// Default ctor
            /// </summary>
            public TriggerItem(IActionTrigger trigger)
            {
                this.trigger = trigger;
                Text = trigger.Name;

                foreach (var action in trigger)
                {
                    Nodes.Add(new ActionItem(action));
                }
                ExpandAll();
            }

            /// <summary>
            /// Can actions be added here?
            /// </summary>
            internal override bool CanAdd { get { return true; } }

            /// <summary>
            /// Add the given action.
            /// </summary>
            internal override Item Add(IAction action)
            {
                var item = new ActionItem(action);
                Nodes.Add(item);
                ExpandAll();
                return item;
            }

            /// <summary>
            /// Save me.
            /// </summary>
            internal override void Save()
            {
                trigger.Clear();
                foreach (ActionItem item in Nodes)
                {
                    trigger.Add(item.Action);
                }
            }

            /// <summary>
            /// A property value has changed. 
            /// Update node.
            /// </summary>
            internal override void OnValueChanged()
            {
                foreach (ActionItem item in Nodes)
                {
                    item.OnValueChanged();
                }
            }
        }
    }
}
