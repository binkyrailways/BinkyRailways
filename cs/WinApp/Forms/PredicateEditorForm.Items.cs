using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.WinApp.Forms
{
    partial class PredicateEditorForm
    {
        private sealed class ItemBuilder : EntityVisitor<PredicateItem, IRailway>
        {
            public override PredicateItem Visit(ILocAndPredicate entity, IRailway data)
            {
                return new AndPredicatesItem(entity, data);
            }
            public override PredicateItem Visit(ILocOrPredicate entity, IRailway data)
            {
                return new OrPredicatesItem(entity, data);
            }
            public override PredicateItem Visit(ILocStandardPredicate entity, IRailway data)
            {
                return new StandardPredicateItem(entity, data);
            }
            public override PredicateItem Visit(ILocCanChangeDirectionPredicate entity, IRailway data)
            {
                return new LocCanChangeDirectionPredicateItem(entity);
            }
            public override PredicateItem Visit(ILocEqualsPredicate entity, IRailway data)
            {
                return new LocEqualsPredicateItem(entity);
            }
            public override PredicateItem Visit(ILocGroupEqualsPredicate entity, IRailway data)
            {
                return new LocGroupEqualsPredicateItem(entity);
            }
            public override PredicateItem Visit(ILocTimePredicate entity, IRailway data)
            {
                return new LocTimePredicateItem(entity);
            }
        }

        private abstract class PredicateItem : TreeNode
        {
            /// <summary>
            /// Gets the predicate
            /// </summary>
            internal abstract ILocPredicate Predicate { get; }

            /// <summary>
            /// Can a given predicate be added to this node?
            /// </summary>
            internal abstract bool CanAdd(ILocPredicate p);

            /// <summary>
            /// Can the given predicate be removed from this node?
            /// </summary>
            internal abstract bool CanRemove(PredicateItem child);

            /// <summary>
            /// Add the given predicate to this node?
            /// </summary>
            internal abstract void Add(ILocPredicate p);

            /// <summary>
            /// Remove the given predicate from this node?
            /// </summary>
            internal abstract void Remove(PredicateItem child);

            /// <summary>
            /// Save the node state in the predicate
            /// </summary>
            internal abstract void Save();
        }

        private abstract class PredicateItem<T> : PredicateItem
            where T : ILocPredicate
        {
            protected readonly T Entity;

            /// <summary>
            /// Default ctor
            /// </summary>
            protected PredicateItem(T entity)
            {
                Entity = entity;
            }

            /// <summary>
            /// Gets the predicate
            /// </summary>
            internal override ILocPredicate Predicate { get { return Entity; } }
        }

        /// <summary>
        /// Base class for ILocPredicatesPredicate items
        /// </summary>
        private class PredicatesPredicateItem<T> : PredicateItem<T>
            where T : ILocPredicatesPredicate
        {
            private readonly IRailway railway;

            /// <summary>
            /// Default ctor
            /// </summary>
            protected PredicatesPredicateItem(T entity, IRailway railway)
                : base(entity)
            {
                this.railway = railway;
                foreach (var p in entity.Predicates)
                {
                    var node = p.Accept(Default<ItemBuilder>.Instance, railway);
                    Nodes.Add(node);
                }
            }

            /// <summary>
            /// Can a given predicate be added to this node?
            /// </summary>
            internal override bool CanAdd(ILocPredicate p)
            {
                return true;
            }

            /// <summary>
            /// Can the given predicate be removed from this node?
            /// </summary>
            internal override bool CanRemove(PredicateItem child)
            {
                return true;
            }

            /// <summary>
            /// Add the given predicate to this node?
            /// </summary>
            internal override void Add(ILocPredicate p)
            {
                var node = p.Accept(Default<ItemBuilder>.Instance, railway);
                Nodes.Add(node);    
                Expand();
            }

            /// <summary>
            /// Remove the given predicate from this node?
            /// </summary>
            internal override void Remove(PredicateItem child)
            {
                Nodes.Remove(child);
            }

            /// <summary>
            /// Save the node state in the predicate
            /// </summary>
            internal override void Save()
            {
                Entity.Predicates.Clear();
                foreach (PredicateItem node in Nodes)
                {
                    node.Save();
                    Entity.Predicates.Add(node.Predicate);
                }
            }
        }

        private class AndPredicatesItem : PredicatesPredicateItem<ILocAndPredicate>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            public AndPredicatesItem(ILocAndPredicate entity, IRailway railway)
                : base(entity, railway)
            {
                Text = "Match all";
            }

            /// <summary>
            /// Can a given predicate be added to this node?
            /// </summary>
            internal override bool CanAdd(ILocPredicate p)
            {
                if (p is ILocAndPredicate)
                    return false;
                return base.CanAdd(p);
            }
        }

        private class OrPredicatesItem : PredicatesPredicateItem<ILocOrPredicate>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            public OrPredicatesItem(ILocOrPredicate entity, IRailway railway)
                : base(entity, railway)
            {
                Text = "Match one or more";
            }

            /// <summary>
            /// Can a given predicate be added to this node?
            /// </summary>
            internal override bool CanAdd(ILocPredicate p)
            {
                if (p is ILocOrPredicate)
                    return false;
                return base.CanAdd(p);
            }
        }

        /// <summary>
        /// Node for ILocStandardPredicate
        /// </summary>
        private sealed class StandardPredicateItem : PredicateItem<ILocStandardPredicate>
        {
            private readonly OrPredicatesItem includes;
            private readonly OrPredicatesItem excludes;

            public StandardPredicateItem(ILocStandardPredicate entity, IRailway railway)
                : base(entity)
            {
                includes = new OrPredicatesItem(entity.Includes, railway);
                includes.Text = "Includes";
                Nodes.Add(includes);
                excludes = new OrPredicatesItem(entity.Excludes, railway);
                excludes.Text = "Excludes";
                Nodes.Add(excludes);
            }

            /// <summary>
            /// Can a given predicate be added to this node?
            /// </summary>
            internal override bool CanAdd(ILocPredicate p)
            {
                return false;
            }

            /// <summary>
            /// Can the given predicate be removed from this node?
            /// </summary>
            internal override bool CanRemove(PredicateItem child)
            {
                return false;
            }

            /// <summary>
            /// Add the given predicate to this node?
            /// </summary>
            internal override void Add(ILocPredicate p)
            {
                // Do nothing
            }

            /// <summary>
            /// Remove the given predicate from this node?
            /// </summary>
            internal override void Remove(PredicateItem child)
            {
                // Do nothing
            }

            /// <summary>
            /// Save the node state in the predicate
            /// </summary>
            internal override void Save()
            {
                includes.Save();
                excludes.Save();
            }
        }

        private class LocEqualsPredicateItem : PredicateItem<ILocEqualsPredicate>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            public LocEqualsPredicateItem(ILocEqualsPredicate entity)
                : base(entity)
            {
                Text = entity.Description;
            }

            /// <summary>
            /// Can a given predicate be added to this node?
            /// </summary>
            internal override bool CanAdd(ILocPredicate p)
            {
                return false;
            }

            /// <summary>
            /// Can the given predicate be removed from this node?
            /// </summary>
            internal override bool CanRemove(PredicateItem child)
            {
                return false;
            }

            /// <summary>
            /// Add the given predicate to this node?
            /// </summary>
            internal override void Add(ILocPredicate p)
            {
                // Do nothing
            }

            /// <summary>
            /// Remove the given predicate from this node?
            /// </summary>
            internal override void Remove(PredicateItem child)
            {
                // Do nothing
            }

            /// <summary>
            /// Save the node state in the predicate
            /// </summary>
            internal override void Save()
            {
                // TODO     
            }
        }

        private class LocGroupEqualsPredicateItem : PredicateItem<ILocGroupEqualsPredicate>
        {
            public LocGroupEqualsPredicateItem(ILocGroupEqualsPredicate entity)
                : base(entity)
            {
                Text = entity.Description;
            }

            /// <summary>
            /// Can a given predicate be added to this node?
            /// </summary>
            internal override bool CanAdd(ILocPredicate p)
            {
                return false;
            }

            /// <summary>
            /// Can the given predicate be removed from this node?
            /// </summary>
            internal override bool CanRemove(PredicateItem child)
            {
                return false;
            }

            /// <summary>
            /// Add the given predicate to this node?
            /// </summary>
            internal override void Add(ILocPredicate p)
            {
                // Do nothing
            }

            /// <summary>
            /// Remove the given predicate from this node?
            /// </summary>
            internal override void Remove(PredicateItem child)
            {
                // Do nothing
            }

            /// <summary>
            /// Save the node state in the predicate
            /// </summary>
            internal override void Save()
            {
                // TODO     
            }
        }

        private class LocCanChangeDirectionPredicateItem : PredicateItem<ILocCanChangeDirectionPredicate>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            public LocCanChangeDirectionPredicateItem(ILocCanChangeDirectionPredicate entity)
                : base(entity)
            {
                Text = entity.Description;
            }

            /// <summary>
            /// Can a given predicate be added to this node?
            /// </summary>
            internal override bool CanAdd(ILocPredicate p)
            {
                return false;
            }

            /// <summary>
            /// Can the given predicate be removed from this node?
            /// </summary>
            internal override bool CanRemove(PredicateItem child)
            {
                return false;
            }

            /// <summary>
            /// Add the given predicate to this node?
            /// </summary>
            internal override void Add(ILocPredicate p)
            {
                // Do nothing
            }

            /// <summary>
            /// Remove the given predicate from this node?
            /// </summary>
            internal override void Remove(PredicateItem child)
            {
                // Do nothing
            }

            /// <summary>
            /// Save the node state in the predicate
            /// </summary>
            internal override void Save()
            {
                // TODO     
            }
        }

        private class LocTimePredicateItem : PredicateItem<ILocTimePredicate>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            public LocTimePredicateItem(ILocTimePredicate entity)
                : base(entity)
            {
                Text = entity.Description;
            }

            /// <summary>
            /// Can a given predicate be added to this node?
            /// </summary>
            internal override bool CanAdd(ILocPredicate p)
            {
                return false;
            }

            /// <summary>
            /// Can the given predicate be removed from this node?
            /// </summary>
            internal override bool CanRemove(PredicateItem child)
            {
                return false;
            }

            /// <summary>
            /// Add the given predicate to this node?
            /// </summary>
            internal override void Add(ILocPredicate p)
            {
                // Do nothing
            }

            /// <summary>
            /// Remove the given predicate from this node?
            /// </summary>
            internal override void Remove(PredicateItem child)
            {
                // Do nothing
            }

            /// <summary>
            /// Save the node state in the predicate
            /// </summary>
            internal override void Save()
            {
                // TODO     
            }
        }
    }
}
