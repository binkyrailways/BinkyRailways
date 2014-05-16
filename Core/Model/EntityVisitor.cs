namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Visitor pattern
    /// </summary>
    public abstract class EntityVisitor<TReturn, TData>
    {
        public virtual TReturn Visit(IAction entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(IBinaryOutput entity, TData data) { return Visit((IOutput)entity, data); }
        public virtual TReturn Visit(IBinarySensor entity, TData data) { return Visit((ISensor)entity, data); }
        public virtual TReturn Visit(IBlock entity, TData data) { return Visit((IEndPoint)entity, data); }
        public virtual TReturn Visit(IBlockGroup entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(IBlockSignal entity, TData data) { return Visit((ISignal)entity, data); }
        public virtual TReturn Visit(IClock4StageOutput entity, TData data) { return Visit((IOutput)entity, data); }
        public virtual TReturn Visit(ICommandStation entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(ICommandStationRef entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(IDccOverRs232CommandStation entity, TData data) { return Visit((ICommandStation)entity, data); }
        public virtual TReturn Visit(IEcosCommandStation entity, TData data) { return Visit((ICommandStation)entity, data); }
        public virtual TReturn Visit(IEdge entity, TData data) { return Visit((IEndPoint)entity, data); }
        public virtual TReturn Visit(IEndPoint entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(ILoc entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(ILocFunction entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(ILocGroup entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(ILocRef entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(ILocoBufferCommandStation entity, TData data) { return Visit((ICommandStation)entity, data); }
        public virtual TReturn Visit(IModule entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(IModuleConnection entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(IModuleRef entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(IOutput entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(IRailway entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(IRoute entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(IRouteEvent entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(IRouteEventBehavior entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(ISensor entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(ISignal entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(IVirtualCommandStation entity, TData data) { return Visit((ICommandStation)entity, data); }

        // Junctions
        public virtual TReturn Visit(IJunction entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(IJunctionWithState entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(IPassiveJunction entity, TData data) { return Visit((IJunction)entity, data); }
        public virtual TReturn Visit(IPassiveJunctionWithState entity, TData data) { return Visit((IJunctionWithState)entity, data); }
        public virtual TReturn Visit(ISwitch entity, TData data) { return Visit((IJunction)entity, data); }
        public virtual TReturn Visit(ISwitchWithState entity, TData data) { return Visit((IJunctionWithState)entity, data); }
        public virtual TReturn Visit(ITurnTable entity, TData data) { return Visit((IJunction)entity, data); }
        public virtual TReturn Visit(ITurnTableWithState entity, TData data) { return Visit((IJunctionWithState)entity, data); }

        // Predicate
        public virtual TReturn Visit(ILocPredicate entity, TData data) { return default(TReturn); }
        public virtual TReturn Visit(ILocAndPredicate entity, TData data) { return Visit((ILocPredicate)entity, data); }
        public virtual TReturn Visit(ILocOrPredicate entity, TData data) { return Visit((ILocPredicate)entity, data); }
        public virtual TReturn Visit(ILocCanChangeDirectionPredicate entity, TData data) { return Visit((ILocPredicate)entity, data); }
        public virtual TReturn Visit(ILocEqualsPredicate entity, TData data) { return Visit((ILocPredicate)entity, data); }
        public virtual TReturn Visit(ILocGroupEqualsPredicate entity, TData data) { return Visit((ILocPredicate)entity, data); }
        public virtual TReturn Visit(ILocStandardPredicate entity, TData data) { return Visit((ILocPredicate)entity, data); }
        public virtual TReturn Visit(ILocTimePredicate entity, TData data) { return Visit((ILocPredicate)entity, data); }

        // Actions
        public virtual TReturn Visit(IInitializeJunctionAction entity, TData data) { return Visit((IModuleAction)entity, data); }
        public virtual TReturn Visit(ILocAction entity, TData data) { return Visit((IAction)entity, data); }
        public virtual TReturn Visit(ILocFunctionAction entity, TData data) { return Visit((ILocAction)entity, data); }
        public virtual TReturn Visit(IModuleAction entity, TData data) { return Visit((IAction)entity, data); }
        public virtual TReturn Visit(IPlaySoundAction entity, TData data) { return Visit((IAction)entity, data); }
    }
}
