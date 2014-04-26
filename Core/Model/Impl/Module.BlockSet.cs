using System.Linq;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    partial class Module 
    {
        /// <summary>
        /// Set of blocks contained in this module.
        /// </summary>
        public class BlockSet : ModuleEntitySet2<Block, IBlock>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal BlockSet(Module module) : base(module)
            {
            }

            /// <summary>
            /// The given item has been removed from this set.
            /// </summary>
            protected override void OnRemoved(Block item)
            {
                // Remove block from crossing blocks lists
                foreach (var route in Module.Routes)
                {
                    if (route.From == item)
                    {
                        route.From = null;
                    }
                    if (route.To == item)
                    {
                        route.To = null;
                    }
                }
                // Remove block from signals
                foreach (var signal in Module.Signals.OfType<IBlockSignal>())
                {
                    if (signal.Block == item)
                    {
                        signal.Block = null;
                    }
                }
                // Remove block from sensors
                foreach (var sensor in Module.Sensors)
                {
                    if (sensor.Block == item)
                    {
                        sensor.Block = null;
                    }
                }
                base.OnRemoved(item);
            }
        }
    }
}
