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
                case LocFunction.F1:
                    property = loc.F1;
                    break;
                case LocFunction.F2:
                    property = loc.F2;
                    break;
                case LocFunction.F3:
                    property = loc.F3;
                    break;
                case LocFunction.F4:
                    property = loc.F4;
                    break;
                case LocFunction.F5:
                    property = loc.F5;
                    break;
                case LocFunction.F6:
                    property = loc.F6;
                    break;
                case LocFunction.F7:
                    property = loc.F7;
                    break;
                case LocFunction.F8:
                    property = loc.F8;
                    break;
                default:
                    return;
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
