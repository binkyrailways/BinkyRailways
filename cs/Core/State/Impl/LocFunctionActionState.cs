using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// Function action on a loc.
    /// </summary>
    public sealed class LocFunctionActionState : LocActionState<ILocFunctionAction>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public LocFunctionActionState(ILocFunctionAction entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
        }

        /// <summary>
        /// Execute this action on the given loc.
        /// </summary>
        protected override void Execute(ILocState loc, IActionContext context)
        {
            IStateProperty<bool> property;

            switch (Entity.Function)
            {
                case LocFunction.Light:
                    property = loc.F0;
                    break;
                default:
                    if (!loc.TryGetFunctionState(Entity.Function, out property))
                    {
                        return;
                    }
                    break;
            }

            switch (Entity.Command)
            {
                case LocFunctionCommand.On:
                    property.Requested = true;
                    break;
                case LocFunctionCommand.Off:
                    property.Requested = false;
                    break;
                case LocFunctionCommand.Toggle:
                    property.Requested = !property.Requested;
                    break;
            }                      
        }
    }
}
