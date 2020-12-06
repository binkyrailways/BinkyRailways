using System;
using System.Windows.Forms;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Controls.Run
{
    partial class UnexpectedSensorControl
    {
        private class BlockItem : ListViewItem, IComparable<BlockItem>, IEquatable<BlockItem>
        {
            private readonly IBlockState block;

            /// <summary>
            /// Default ctor
            /// </summary>
            public BlockItem(IBlockState block)
            {
                this.block = block;
                Text = block.ToString();
            }

            /// <summary>
            /// The block contained in this item
            /// </summary>
            public IBlockState Block
            {
                get { return block; }
            }

            /// <summary>
            /// Compares the current object with another object of the same type.
            /// </summary>
            public int CompareTo(BlockItem other)
            {
                return block.ToString().CompareTo(other.block.ToString());
            }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            public bool Equals(BlockItem other)
            {
                return (other != null) && (other.block == block);
            }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            public override bool Equals(object obj)
            {
                return Equals(obj as BlockItem);
            }

            /// <summary>
            /// Hashing
            /// </summary>
            public override int GetHashCode()
            {
                return block.GetHashCode();
            }
        }
    }
}
