using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Forms
{
    public partial class BlockSignalPatternEditorForm : AppForm, IPatternEditorForm
    {
        private readonly IBlockSignal entity;
        private readonly PatternCheckBoxes red;
        private readonly PatternCheckBoxes green;
        private readonly PatternCheckBoxes yellow;
        private readonly PatternCheckBoxes white;

        /// <summary>
        /// Designer ctor
        /// </summary>
        public BlockSignalPatternEditorForm()
            : this(null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        internal BlockSignalPatternEditorForm(IBlockSignal entity)
        {
            this.entity = entity;
            InitializeComponent();
            red = new PatternCheckBoxes(this, cbRedEnabled, lbRedInfo, cbR1, cbR2, cbR3, cbR4);
            green = new PatternCheckBoxes(this, cbGreenEnabled, lbGreenInfo, cbG1, cbG2, cbG3, cbG4);
            yellow = new PatternCheckBoxes(this, cbYellowEnabled, lbYellowInfo, cbY1, cbY2, cbY3, cbY4);
            white = new PatternCheckBoxes(this, cbWhiteEnabled, lbWhiteInfo, cbW1, cbW2, cbW3, cbW4);
            if (entity != null)
            {
                red.Load(entity.RedPattern);
                green.Load(entity.GreenPattern);
                yellow.Load(entity.YellowPattern);
                white.Load(entity.WhitePattern);
            }
            UpdateAllUI();
        }

        /// <summary>
        /// Gets all patterns
        /// </summary>
        private IEnumerable<PatternCheckBoxes> Patterns
        {
            get
            {
                yield return red;
                yield return green;
                yield return yellow;
                yield return white;
            }
        }

        /// <summary>
        /// Update the UI of all patterns
        /// </summary>
        public void UpdateAllUI()
        {
            red.UpdateUI();
            green.UpdateUI();
            yellow.UpdateUI();
            white.UpdateUI();            
        }

        /// <summary>
        /// Save changes
        /// </summary>
        private void cmdOk_Click(object sender, System.EventArgs e)
        {
            entity.RedPattern = red.GetPattern();
            entity.GreenPattern = green.GetPattern();
            entity.YellowPattern = yellow.GetPattern();
            entity.WhitePattern = white.GetPattern();
            DialogResult = DialogResult.OK;            
        }

        private class PatternCheckBoxes
        {
            private readonly BlockSignalPatternEditorForm form;
            private readonly CheckBox cbEnabled;
            private readonly Label lbInfo;
            private readonly CheckBox[] boxes;

            public PatternCheckBoxes(BlockSignalPatternEditorForm form, CheckBox cbEnabled, Label lbInfo, params CheckBox[] boxes)
            {
                if (boxes.Length != 4)
                    throw new ArgumentException("4 boxes expected");
                this.form = form;
                this.cbEnabled = cbEnabled;
                this.lbInfo = lbInfo;
                this.boxes = boxes;

                cbEnabled.CheckedChanged += (s, x) => form.UpdateAllUI();
                foreach (var cb in boxes)
                {
                    cb.CheckedChanged += (s, x) => form.UpdateAllUI();
                }
            }

            /// <summary>
            /// Is this pattern enabled?
            /// </summary>
            private bool IsEnabled
            {
                get { return cbEnabled.Checked; }
                set
                {
                    cbEnabled.Checked = value;
                    foreach (var cb in boxes)
                    {
                        cb.Enabled = value;
                        if (!value)
                        {
                            cb.Checked = false;
                        }
                    }
                }
            }

            /// <summary>
            /// Set the checked property of the checkboxes.
            /// </summary>
            internal void Load(int pattern)
            {
                if (pattern == BlockSignalPatterns.Disabled)
                {
                    IsEnabled = false;
                }
                else
                {
                    IsEnabled = true;
                    boxes[0].Checked = ((pattern & 0x01) != 0);
                    boxes[1].Checked = ((pattern & 0x02) != 0);
                    boxes[2].Checked = ((pattern & 0x04) != 0);
                    boxes[3].Checked = ((pattern & 0x08) != 0);
                }
            }

            /// <summary>
            /// Calculate the checked pattern.
            /// </summary>
            internal int GetPattern()
            {
                var pattern = BlockSignalPatterns.Disabled;
                if (cbEnabled.Checked)
                {
                    pattern = 0;
                    if (boxes[0].Checked) pattern |= 0x01;
                    if (boxes[1].Checked) pattern |= 0x02;
                    if (boxes[2].Checked) pattern |= 0x04;
                    if (boxes[3].Checked) pattern |= 0x08;
                }
                return pattern;
            }

            /// <summary>
            /// Update entire UI.
            /// </summary>
            internal void UpdateUI()
            {
                OnEnabledCheckedChanged();
                OnBoxCheckedChanged();
            }

            /// <summary>
            /// Checkbox state has changed.
            /// </summary>
            private void OnEnabledCheckedChanged()
            {
                IsEnabled = cbEnabled.Checked;
            }

            /// <summary>
            /// Checkbox state has changed.
            /// </summary>
            private void OnBoxCheckedChanged()
            {
                if (IsEnabled)
                {
                    if (form.Patterns.Any(x => (x != this) && (x.IsEnabled) && (x.GetPattern() == GetPattern())))
                    {
                        // Found exactly the same pattern
                        lbInfo.Text = "Duplicate";
                    }
                    else
                    {
                        lbInfo.Text = "Valid";
                    }
                }
                else
                {
                    lbInfo.Text = "Disabled";
                }
            }
        }
    }
}
