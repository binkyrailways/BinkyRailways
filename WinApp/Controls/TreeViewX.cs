using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls
{
    /// <summary>
    /// TreeView with multiple selection support.
    /// </summary>
    public class TreeViewX : TreeView
    {
        /// <summary>
        /// Fired when the contents of the <see cref="SelectedNodes"/> collection has changed.
        /// </summary>
        public event EventHandler SelectionChanged;

        private readonly SelectedNodesCollection selectedNodes;
        private TreeNode lastSelectedNode, firstSelectedNode;

        /// <summary>
        /// Default ctor
        /// </summary>
        public TreeViewX()
        {
            selectedNodes = new SelectedNodesCollection(this);
        }

        /// <summary>
        /// Gets all selected nodes.
        /// </summary>
        public ICollection<TreeNode> SelectedNodes
        {
            get { return selectedNodes; }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeSelect"/> event.
        /// </summary>
        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            base.OnBeforeSelect(e);

            bool bControl = (ModifierKeys == Keys.Control);
            bool bShift = (ModifierKeys == Keys.Shift);

            // selecting twice the node while pressing CTRL ?
            if (bControl && selectedNodes.Contains(e.Node))
            {
                // unselect it (let framework know we don't want selection this time)
                e.Cancel = true;

                // update nodes
                selectedNodes.Remove(e.Node);
                return;
            }

            lastSelectedNode = e.Node;
            if (!bShift) firstSelectedNode = e.Node; // store begin of shift sequence
        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.AfterSelect"/> event.
        /// </summary>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            var bControl = (ModifierKeys == Keys.Control);
            var bShift = (ModifierKeys == Keys.Shift);

            if (bControl)
            {
                if (!selectedNodes.Contains(e.Node)) // new node ?
                {
                    selectedNodes.Add(e.Node);
                }
                else // not new, remove it from the collection
                {
                    selectedNodes.Remove(e.Node);
                }
            }
            else
            {
                // SHIFT is pressed
                if (bShift)
                {
                    var myQueue = new Queue<TreeNode>();

                    var uppernode = firstSelectedNode;
                    var bottomnode = e.Node;
                    // case 1 : begin and end nodes are parent
                    var bParent = IsParent(firstSelectedNode, e.Node); // is m_firstNode parent (direct or not) of e.Node
                    if (!bParent)
                    {
                        bParent = IsParent(bottomnode, uppernode);
                        if (bParent) // swap nodes
                        {
                            var t = uppernode;
                            uppernode = bottomnode;
                            bottomnode = t;
                        }
                    }
                    if (bParent)
                    {
                        var n = bottomnode;
                        while (n != uppernode.Parent)
                        {
                            if (!selectedNodes.Contains(n)) // new node ?
                                myQueue.Enqueue(n);

                            n = n.Parent;
                        }
                    }
                    // case 2 : nor the begin nor the end node are descendant one another
                    else
                    {
                        if ((uppernode.Parent == null && bottomnode.Parent == null) || (uppernode.Parent != null && uppernode.Parent.Nodes.Contains(bottomnode))) // are they siblings ?
                        {
                            int nIndexUpper = uppernode.Index;
                            int nIndexBottom = bottomnode.Index;
                            if (nIndexBottom < nIndexUpper) // reversed?
                            {
                                TreeNode t = uppernode;
                                uppernode = bottomnode;
                                bottomnode = t;
                                nIndexUpper = uppernode.Index;
                                nIndexBottom = bottomnode.Index;
                            }

                            TreeNode n = uppernode;
                            while (nIndexUpper <= nIndexBottom)
                            {
                                if (!selectedNodes.Contains(n)) // new node ?
                                    myQueue.Enqueue(n);

                                n = n.NextNode;

                                nIndexUpper++;
                            } // end while

                        }
                        else
                        {
                            if (!selectedNodes.Contains(uppernode)) myQueue.Enqueue(uppernode);
                            if (!selectedNodes.Contains(bottomnode)) myQueue.Enqueue(bottomnode);
                        }
                    }

                    selectedNodes.AddRange(myQueue);
                    firstSelectedNode = e.Node; // let us chain several SHIFTs if we like it
                } // end if m_bShift
                else
                {
                    // in the case of a simple click, just add this item
                    selectedNodes.Clear();
                    selectedNodes.Add(e.Node);
                }
            }
            base.OnAfterSelect(e);
        }

        private static bool IsParent(TreeNode parentNode, TreeNode childNode)
        {
            if (parentNode == childNode)
                return true;

            var n = childNode;
            bool bFound = false;
            while (!bFound && n != null)
            {
                n = n.Parent;
                bFound = (n == parentNode);
            }
            return bFound;
        }

        /// <summary>
        /// Collection of selected nodes.
        /// </summary>
        private sealed class SelectedNodesCollection : ICollection<TreeNode>
        {
            private readonly TreeViewX treeView;
            private readonly List<TreeNode> list = new List<TreeNode>();

            /// <summary>
            /// Default ctor
            /// </summary>
            internal SelectedNodesCollection(TreeViewX treeView)
            {
                this.treeView = treeView;
            }

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
            /// </returns>
            /// <filterpriority>1</filterpriority>
            public IEnumerator<TreeNode> GetEnumerator()
            {
                return list.GetEnumerator();
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
            /// </returns>
            /// <filterpriority>2</filterpriority>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            /// <summary>
            /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
            /// </summary>
            /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
            ///                 </param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
            ///                 </exception>
            public void Add(TreeNode item)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                    SetSelectionColors(item);
                }
            }

            /// <summary>
            /// Add all given items to this collection.
            /// </summary>
            public void AddRange(IEnumerable<TreeNode> items)
            {
                foreach (var item in items)
                {
                    Add(item);
                }
            }

            /// <summary>
            /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
            /// </summary>
            /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only. 
            ///                 </exception>
            public void Clear()
            {
                if (list.Count > 0)
                {
                    foreach (var n in list)
                    {
                        RemoveSelectionColors(n);
                    }
                    list.Clear();
                }
            }

            /// <summary>
            /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
            /// </summary>
            /// <returns>
            /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
            /// </returns>
            /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
            ///                 </param>
            public bool Contains(TreeNode item)
            {
                return list.Contains(item);
            }

            /// <summary>
            /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.
            ///                 </param><param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.
            ///                 </param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.
            ///                 </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.
            ///                 </exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.
            ///                     -or-
            ///                 <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.
            ///                     -or-
            ///                     The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.
            ///                     -or-
            ///                     Type <paramref name="T"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
            ///                 </exception>
            public void CopyTo(TreeNode[] array, int arrayIndex)
            {
                list.CopyTo(array, arrayIndex);
            }

            /// <summary>
            /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
            /// </summary>
            /// <returns>
            /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
            /// </returns>
            /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
            ///                 </param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
            ///                 </exception>
            public bool Remove(TreeNode item)
            {
                if (!list.Remove(item))
                    return false;
                RemoveSelectionColors(item);
                return true;
            }

            /// <summary>
            /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
            /// </summary>
            /// <returns>
            /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
            /// </returns>
            public int Count
            {
                get { return list.Count; }
            }

            /// <summary>
            /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
            /// </summary>
            /// <returns>
            /// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
            /// </returns>
            public bool IsReadOnly
            {
                get { return false; }
            }

            /// <summary>
            /// Set the selection colors on the given node.
            /// </summary>
            private static void SetSelectionColors(TreeNode n)
            {
                n.BackColor = SystemColors.Highlight;
                n.ForeColor = SystemColors.HighlightText;
            }

            /// <summary>
            /// Set the normal colors on the given nodes.
            /// </summary>
            private void RemoveSelectionColors(TreeNode n)
            {
                n.BackColor = treeView.BackColor;
                n.ForeColor = treeView.ForeColor;
            }
        }
    }
}
