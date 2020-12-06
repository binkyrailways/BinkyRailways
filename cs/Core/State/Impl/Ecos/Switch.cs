using BinkyRailways.Core.Model;
using BinkyRailways.Protocols.Ecos;

namespace BinkyRailways.Core.State.Impl.Ecos
{
    /// <summary>
    /// ECoS switch object
    /// </summary>
    internal class Switch : Protocols.Ecos.Objects.Switch
    {
        private readonly ISwitchState junctionState;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Switch(Client client, int id, ISwitchState junctionState)
            : base(client, id)
        {
            this.junctionState = junctionState;
        }

        /// <summary>
        /// Called when an event for this object is received.
        /// </summary>
        protected override void OnEvent(Event @event)
        {
            foreach (var row in @event.Rows)
            {
                Option option;
                if (row.TryGetValue(OptState, out option))
                {
                    var direction = (option.Value == "0") ? SwitchDirection.Straight : SwitchDirection.Off;
                    junctionState.Direction.Actual = direction;
                    junctionState.Direction.Requested = direction;
                }
            }
            base.OnEvent(@event);
        }
    }
}
