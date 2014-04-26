using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Model.Impl;

namespace BinkyRailways.WinApp.Forms
{
    partial class PredicateEditorForm
    {
        private interface IPredicateBuilder
        {
            ILocPredicate CreatePredicate(bool editSettings);
        }

        private sealed class LocEqualsMenuItem : ToolStripMenuItem, IPredicateBuilder
        {
            private readonly ILoc loc;
            private readonly IRailway railway;

            /// <summary>
            /// Default ctor
            /// </summary>
            public LocEqualsMenuItem(ILoc loc, IRailway railway)
            {
                this.loc = loc;
                this.railway = railway;
                Text = loc.ToString();
            }

            public ILocPredicate CreatePredicate(bool editSettings)
            {
                return railway.PredicateBuilder.CreateEquals(loc);
            }
        }

        private sealed class LocGroupEqualsMenuItem : ToolStripMenuItem, IPredicateBuilder
        {
            private readonly ILocGroup group;
            private readonly IRailway railway;

            /// <summary>
            /// Default ctor
            /// </summary>
            public LocGroupEqualsMenuItem(ILocGroup group, IRailway railway)
            {
                this.group = group;
                this.railway = railway;
                Text = group.ToString();
            }

            public ILocPredicate CreatePredicate(bool editSettings)
            {
                return railway.PredicateBuilder.CreateGroupEquals(group);
            }
        }

        private sealed class LocCanChangeDirectionMenuItem : ToolStripMenuItem, IPredicateBuilder
        {
            private readonly IRailway railway;

            /// <summary>
            /// Default ctor
            /// </summary>
            public LocCanChangeDirectionMenuItem(IRailway railway)
            {
                this.railway = railway;
                Text = Strings.CanChangeDirectionPredicateText;
            }

            public ILocPredicate CreatePredicate(bool editSettings)
            {
                return railway.PredicateBuilder.CreateCanChangeDirection();
            }
        }

        private sealed class LocTimeMenuItem : ToolStripMenuItem, IPredicateBuilder
        {
            private readonly IRailway railway;

            /// <summary>
            /// Default ctor
            /// </summary>
            public LocTimeMenuItem(IRailway railway)
            {
                this.railway = railway;
                Text = Strings.TimePeriodText;
            }

            public ILocPredicate CreatePredicate(bool editSettings)
            {
                if (!editSettings)
                    return new LocTimePredicate { PeriodStart = Time.MinValue, PeriodEnd = Time.MaxValue };
                using (var dialog = new TimePeriodEditorForm())
                {
                    dialog.PeriodStart = Time.MinValue;
                    dialog.PeriodEnd = Time.MaxValue;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        return railway.PredicateBuilder.CreateTime(dialog.PeriodStart, dialog.PeriodEnd);                        
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// AND predicate
        /// </summary>
        private sealed class LocAndMenuItem : ToolStripMenuItem, IPredicateBuilder
        {
            private readonly IRailway railway;

            /// <summary>
            /// Default ctor
            /// </summary>
            public LocAndMenuItem(IRailway railway)
            {
                this.railway = railway;
                Text = Strings.AndPredicateText;
            }

            public ILocPredicate CreatePredicate(bool editSettings)
            {
                return railway.PredicateBuilder.CreateAnd();
            }
        }

        /// <summary>
        /// OR predicate
        /// </summary>
        private sealed class LocOrMenuItem : ToolStripMenuItem, IPredicateBuilder
        {
            private readonly IRailway railway;

            /// <summary>
            /// Default ctor
            /// </summary>
            public LocOrMenuItem(IRailway railway)
            {
                this.railway = railway;
                Text = Strings.OrPredicateText;
            }

            public ILocPredicate CreatePredicate(bool editSettings)
            {
                return railway.PredicateBuilder.CreateOr();
            }
        }
    }
}
