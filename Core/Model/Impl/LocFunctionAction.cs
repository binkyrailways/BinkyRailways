namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Action that changes the function state of a loc.
    /// </summary>
    public sealed class LocFunctionAction : LocAction, ILocFunctionAction
    {
        private readonly Property<Model.LocFunction> function;
        private readonly Property<LocFunctionCommand> command;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocFunctionAction()
        {
            function = new Property<Model.LocFunction>(this, Model.LocFunction.Light);
            command = new Property<LocFunctionCommand>(this, LocFunctionCommand.On);
        }

        /// <summary>
        /// The function involved in the action.
        /// </summary>
        public Model.LocFunction Function
        {
            get { return function.Value; }
            set { function.Value = value; }
        }

        /// <summary>
        /// What to do with the function
        /// </summary>
        public LocFunctionCommand Command
        {
            get { return command.Value; }
            set { command.Value = value; }
        }

        /// <summary>
        /// Human readable description
        /// </summary>
        public override string Description
        {
            get 
            {
                var loc = Loc;
                return string.Format("{0}, {1}, {2}", Function, Command, (loc == null) ? "Current" : loc.ToString());
            }
            set { /* Do nothing */ }
        }

        /// <summary>
        /// Create a clone of this action.
        /// </summary>
        protected override Action Clone()
        {
            return new LocFunctionAction { Loc = Loc, Function = Function, Command = Command };
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }
    }
}
