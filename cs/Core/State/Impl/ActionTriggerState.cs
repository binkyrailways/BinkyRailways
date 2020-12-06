using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BinkyRailways.Core.Logging;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using NLog;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State for a action trigger.
    /// </summary>
    public sealed class ActionTriggerState : EntityState, IActionTriggerState
    {
        private static readonly Logger Log = LogManager.GetLogger(LogNames.Actions);

        private readonly List<IActionState> actions;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal ActionTriggerState(IActionTrigger entity, RailwayState railwayState)
            : base(railwayState)
        {
            actions = entity.Select(x => x.Accept(Default<StateBuilder>.Instance, railwayState)).Cast<IActionState>().ToList();
        }

        /// <summary>
        /// Execute all actions.
        /// </summary>
        public void Execute(IActionContext context)
        {
            try
            {
                foreach (var action in actions)
                {
                    action.Execute(context);
                }
            }
            catch (Exception ex)
            {
                // Performing action failed.
                Log.ErrorException("Executing actions failed", ex);
            }
        }

        /// <summary>
        /// Is this set empty?
        /// </summary>
        [DisplayName(@"IsEmpty")]
        public bool IsEmpty { get { return (actions.Count == 0); } }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            var index = 0;
            while (index < actions.Count)
            {
                var x = (EntityState)actions[index];
                x.PrepareForUse(ui, statePersistence);
                if (!x.IsReadyForUse)
                {
                    actions.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
            return actions.Any();
        }
    }
}
