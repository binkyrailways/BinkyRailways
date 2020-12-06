using System.Linq;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    partial class Module 
    {
        /// <summary>
        /// Set of block groups contained in this module.
        /// </summary>
        public class BlockGroupSet : ModuleEntitySet2<BlockGroup, IBlockGroup>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal BlockGroupSet(Module module) : base(module)
            {
            }
        }
    }
}
