using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// Evaluate a specific predicate
    /// </summary>
    internal sealed class LocPredicateEvaluator : EntityVisitor<bool, ILocState>
    {
        /// <summary>
        /// Evaluate 'loc can change direction' predicate
        /// </summary>
        public override bool Visit(ILocCanChangeDirectionPredicate entity, ILocState data)
        {
            return (data.ChangeDirection == ChangeDirection.Allow);
        }

        /// <summary>
        /// Evaluate 'loc equals to' predicate
        /// </summary>
        public override bool Visit(ILocEqualsPredicate entity, ILocState data)
        {
            return data.IsStateOf(entity.Loc);
        }

        /// <summary>
        /// Evaluate 'loc belongs in group' predicate
        /// </summary>
        public override bool Visit(ILocGroupEqualsPredicate entity, ILocState data)
        {
            var @group = entity.Group;
            return (@group != null) && @group.Locs.Any(data.IsStateOf);
        }

        /// <summary>
        /// Evaluate AND predicate
        /// </summary>
        public override bool Visit(ILocAndPredicate entity, ILocState data)
        {
            return entity.Predicates.All(x => x.Accept(this, data));
        }

        /// <summary>
        /// Evaluate OR predicate
        /// </summary>
        public override bool Visit(ILocOrPredicate entity, ILocState data)
        {
            return entity.Predicates.Any(x => x.Accept(this, data));
        }

        /// <summary>
        /// Evaluate standard predicate
        /// </summary>
        public override bool Visit(ILocStandardPredicate entity, ILocState data)
        {
            var includes = entity.Includes;
            var excludes = entity.Excludes;
            if (includes.IsEmpty && excludes.IsEmpty)
            {
                return true;
            }
            if (includes.IsEmpty)
            {
                return !excludes.Accept(this, data);
            }
            return includes.Accept(this, data) && !excludes.Accept(this, data);
        }

        public override bool Visit(ILocTimePredicate entity, ILocState data)
        {
            var time = data.RailwayState.ModelTime.Actual;
            return (time >= entity.PeriodStart) && (time <= entity.PeriodEnd);
        }
    }
}
