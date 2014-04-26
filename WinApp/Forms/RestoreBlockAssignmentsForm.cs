using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Forms
{
    /// <summary>
    /// Form used to restore previously persisted block assignments
    /// </summary>
    public partial class RestoreBlockAssignmentsForm : AppForm
    {
        private bool initialized;
        private IRailwayState railwayState;

        /// <summary>
        /// Default ctor
        /// </summary>
        public RestoreBlockAssignmentsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize all items
        /// </summary>
        /// <returns>True if there are assignment options</returns>
        internal bool Initialize(IRailwayState railwayState, IStatePersistence statePersistence)
        {
            this.railwayState = railwayState;

            lvLocs.BeginUpdate();
            lvLocs.Items.Clear();
            
            foreach (var locState in railwayState.LocStates.Where(x => (x.CurrentBlock.Actual == null)).OrderBy(x => x.Description))
            {
                BlockSide currentBlockEnterSide;
                IBlockState blockState;
                LocDirection locDirection;
                if (statePersistence.TryGetLocState(railwayState, locState, out blockState, out currentBlockEnterSide, out locDirection))
                {
                    var item = new Item(locState, blockState, currentBlockEnterSide, locDirection);
                    lvLocs.Items.Add(item);
                }
            }

            lvLocs.EndUpdate();
            UpdateButtonState();
            return (lvLocs.Items.Count > 0);
        }

        protected override void OnVisibleChanged(System.EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
                initialized = true;
        }

        /// <summary>
        /// Assign all checked items
        /// </summary>
        private void OnAssignClick(object sender, System.EventArgs e)
        {
            var checkedItems = lvLocs.Items.Cast<Item>().Where(x => x.Checked).ToList();
            var task = Task.Factory.StartNew(() => {
                foreach (var item in checkedItems)
                {
                    item.Assign();
                }                                                           
            });
            task.ContinueWith(t => Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Close without assigning.
        /// </summary>
        private void OnCancelClick(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Item has been checked/unchecked
        /// </summary>
        private void OnItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (initialized)
            {
                UpdateButtonState();
            }
        }

        /// <summary>
        /// Update the state of the buttons.
        /// </summary>
        private void UpdateButtonState()
        {
            try
            {
                var hasCheckedItems = lvLocs.Items.Cast<Item>().Where(x => x.Checked).Any();
                cmdAssign.Enabled = hasCheckedItems;
            } catch
            {
                // Ignore errors.
                // Sometime ListViewItem.Checked throws an error.
            }
        }

        /// <summary>
        /// Single assignment item
        /// </summary>
        private class Item : ListViewItem
        {
            private readonly ILocState loc;
            private readonly IBlockState block;
            private readonly BlockSide currentBlockEnterSide;
            private readonly LocDirection direction;

            /// <summary>
            /// Default ctor
            /// </summary>
            public Item(ILocState loc, IBlockState block, BlockSide currentBlockEnterSide, LocDirection direction)
            {
                this.loc = loc;
                this.block = block;
                this.currentBlockEnterSide = currentBlockEnterSide;
                this.direction = direction;
                Checked = true;
                Text = loc.Description;
                var facingText = (currentBlockEnterSide.Invert() == BlockSide.Front) ? Strings.FacingFrontOfBlock : Strings.FacingBackOfBlock;
                SubItems.Add(string.Format(Strings.XFacingY, block.Description, facingText));
                SubItems.Add(direction == LocDirection.Forward ? "Forward" : "Reverse");
            }

            /// <summary>
            /// Assign the loc to the current block
            /// </summary>
            public void Assign()
            {
                loc.Direction.Requested = direction;
                loc.AssignTo(block, currentBlockEnterSide);
            }
        }
    }
}
