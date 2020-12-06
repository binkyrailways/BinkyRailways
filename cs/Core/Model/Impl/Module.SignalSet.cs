namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    partial class Module 
    {
        /// <summary>
        /// Set of sensors contained in this module.
        /// </summary>
        public class SignalSet : ModuleEntitySet<Signal, ISignal>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal SignalSet(Module module)
                : base(module, module)
            {
            }

            /// <summary>
            /// Implementation of ISignalSet
            /// </summary>
            public new ISignalSet Set
            {
                get { return (ISignalSet)base.Set; }
            }

            /// <summary>
            /// Create an implementation of IEntitySet.
            /// </summary>
            protected override SetImpl CreateSetImpl()
            {
                return new SignalSetImpl(this);
            }

            private sealed class SignalSetImpl : SetImpl, ISignalSet
            {
                /// <summary>
                /// Default ctor
                /// </summary>
                internal SignalSetImpl(SignalSet impl)
                    : base(impl)
                {
                }

                /// <summary>
                /// Add a new block signal
                /// </summary>
                public IBlockSignal AddNewBlockSignal()
                {
                    var entity = new BlockSignal();
                    Add(entity);
                    return entity;
                }
            }
        }
    }
}
