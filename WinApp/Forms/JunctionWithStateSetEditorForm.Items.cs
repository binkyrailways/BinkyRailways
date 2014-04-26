using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Forms
{
    partial class JunctionWithStateSetEditorForm
    {
        /// <summary>
        /// Builder for state items
        /// </summary>
        private sealed class StateItemBuilder : EntityVisitor<JunctionStateItem, object>
        {
            public override JunctionStateItem Visit(IPassiveJunction entity, object data)
            {
                return new PassiveJunctionStateItem(entity);
            }
            public override JunctionStateItem Visit(IPassiveJunctionWithState entity, object data)
            {
                return new PassiveJunctionStateItem((IPassiveJunction) entity.Junction);
            }
            public override JunctionStateItem Visit(ISwitch entity, object data)
            {
                return new SwitchStateItem(entity);
            }
            public override JunctionStateItem Visit(ISwitchWithState entity, object data)
            {
                return new SwitchStateItem((ISwitch)entity.Junction) { Direction = entity.Direction };
            }
            public override JunctionStateItem Visit(ITurnTable entity, object data)
            {
                return new TurnTableStateItem(entity);
            }
            public override JunctionStateItem Visit(ITurnTableWithState entity, object data)
            {
                return new TurnTableStateItem((ITurnTable)entity.Junction) { Position = entity.Position };
            }
        }

        /// <summary>
        /// Entry in the right listbox.
        /// </summary>
        private abstract class JunctionStateItem
        {
            private readonly IJunction junction;

            /// <summary>
            /// Default ctor
            /// </summary>
            protected JunctionStateItem(IJunction junction)
            {
                this.junction = junction;
            }

            /// <summary>
            /// The junction involved
            /// </summary>
            public IJunction Junction
            {
                get { return junction; }
            }

            /// <summary>
            /// Add this junction with state to the given set.
            /// </summary>
            public abstract void AddTo(IJunctionWithStateSet junctionWithStateSet);

            /// <summary>
            /// This item has been selected.
            /// Setup the proper control to edit the state in the given form.
            /// </summary>
            public abstract void InitializeStateControls(JunctionWithStateSetEditorForm form);

            /// <summary>
            /// Move to the next possible state
            /// </summary>
            public abstract void ToggleState();

            /// <summary>
            /// Convert to string
            /// </summary>
            public override string ToString()
            {
                return junction.ToString();
            }
        }

        /// <summary>
        /// Passive junction.
        /// </summary>
        private sealed class PassiveJunctionStateItem : JunctionStateItem
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            public PassiveJunctionStateItem(IPassiveJunction junction)
                : base(junction)
            {
            }

            /// <summary>
            /// Add this junction with state to the given set.
            /// </summary>
            public override void AddTo(IJunctionWithStateSet junctionWithStateSet)
            {
                junctionWithStateSet.Add((IPassiveJunction) Junction);
            }

            /// <summary>
            /// This item has been selected.
            /// Setup the proper control to edit the state in the given form.
            /// </summary>
            public override void InitializeStateControls(JunctionWithStateSetEditorForm form)
            {
                form.cbSwitchDirection.Visible = false;
                form.cbSwitchDirection.Enabled = false;
            }

            /// <summary>
            /// Move to the next possible state
            /// </summary>
            public override void ToggleState()
            {
                // Do nothing
            }
        }

        /// <summary>
        /// Switch entry in right listbox.
        /// </summary>
        private sealed class SwitchStateItem : JunctionStateItem
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            public SwitchStateItem(ISwitch junction)
                : base(junction)
            {
            }

            /// <summary>
            /// Intended direction
            /// </summary>
            public SwitchDirection Direction { get; set; }

            /// <summary>
            /// Add this junction with state to the given set.
            /// </summary>
            public override void AddTo(IJunctionWithStateSet junctionWithStateSet)
            {
                junctionWithStateSet.Add((ISwitch)Junction, Direction);
            }

            /// <summary>
            /// This item has been selected.
            /// Setup the proper control to edit the state in the given form.
            /// </summary>
            public override void InitializeStateControls(JunctionWithStateSetEditorForm form)
            {
                form.cbSwitchDirection.SelectedItem = Direction;
                form.cbSwitchDirection.Visible = true;
                form.cbSwitchDirection.Enabled = true;
            }

            /// <summary>
            /// Move to the next possible state
            /// </summary>
            public override void ToggleState()
            {
                Direction = Direction.Invert();
            }

            /// <summary>
            /// Convert to string
            /// </summary>
            public override string ToString()
            {
                return base.ToString() + " - " + Direction;
            }
        }

        /// <summary>
        /// TurnTable entry in right listbox.
        /// </summary>
        private sealed class TurnTableStateItem : JunctionStateItem
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            public TurnTableStateItem(ITurnTable junction)
                : base(junction)
            {
                Position = junction.InitialPosition;
            }

            /// <summary>
            /// Intended position
            /// </summary>
            public int Position { get; set; }

            /// <summary>
            /// Add this junction with state to the given set.
            /// </summary>
            public override void AddTo(IJunctionWithStateSet junctionWithStateSet)
            {
                junctionWithStateSet.Add((ITurnTable)Junction, Position);
            }

            /// <summary>
            /// This item has been selected.
            /// Setup the proper control to edit the state in the given form.
            /// </summary>
            public override void InitializeStateControls(JunctionWithStateSetEditorForm form)
            {
                form.udTurnTablePosition.Minimum = 0;
                form.udTurnTablePosition.Maximum = int.MaxValue;
                form.udTurnTablePosition.Value = Position;

                var tt = (ITurnTable)Junction;
                form.udTurnTablePosition.Minimum = tt.FirstPosition;
                form.udTurnTablePosition.Maximum = tt.LastPosition;
                form.udTurnTablePosition.Visible = true;
                form.udTurnTablePosition.Enabled = true;
            }

            /// <summary>
            /// Move to the next possible state
            /// </summary>
            public override void ToggleState()
            {
                var tt = (ITurnTable) Junction;
                if (Position < tt.LastPosition)
                {
                    Position = Position + 1;
                } else
                {
                    Position = tt.FirstPosition;
                }
            }

            /// <summary>
            /// Convert to string
            /// </summary>
            public override string ToString()
            {
                return base.ToString() + " - " + Position;
            }
        }
    }
}
