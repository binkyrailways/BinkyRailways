using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Forms
{
    public partial class Clock4StageOutputPatternEditorForm : AppForm, IPatternEditorForm
    {
        private readonly IClock4StageOutput entity;
        private readonly PatternCheckBoxes morning;
        private readonly PatternCheckBoxes afternoon;
        private readonly PatternCheckBoxes evening;
        private readonly PatternCheckBoxes night;

        /// <summary>
        /// Designer ctor
        /// </summary>
        public Clock4StageOutputPatternEditorForm()
            : this(null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        internal Clock4StageOutputPatternEditorForm(IClock4StageOutput entity)
        {
            this.entity = entity;
            InitializeComponent();
            morning = new PatternCheckBoxes(this, lbRedInfo, cbR1, cbR2);
            afternoon = new PatternCheckBoxes(this, lbGreenInfo, cbG1, cbG2);
            evening = new PatternCheckBoxes(this, lbYellowInfo, cbY1, cbY2);
            night = new PatternCheckBoxes(this, lbWhiteInfo, cbW1, cbW2);
            if (entity != null)
            {
                morning.Load(entity.MorningPattern);
                afternoon.Load(entity.AfternoonPattern);
                evening.Load(entity.EveningPattern);
                night.Load(entity.NightPattern);
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
                yield return morning;
                yield return afternoon;
                yield return evening;
                yield return night;
            }
        }

        /// <summary>
        /// Update the UI of all patterns
        /// </summary>
        public void UpdateAllUI()
        {
            morning.UpdateUI();
            afternoon.UpdateUI();
            evening.UpdateUI();
            night.UpdateUI();            
        }

        /// <summary>
        /// Save changes
        /// </summary>
        private void cmdOk_Click(object sender, System.EventArgs e)
        {
            entity.MorningPattern = morning.GetPattern();
            entity.AfternoonPattern = afternoon.GetPattern();
            entity.EveningPattern = evening.GetPattern();
            entity.NightPattern = night.GetPattern();
            DialogResult = DialogResult.OK;            
        }

        private class PatternCheckBoxes
        {
            private readonly Clock4StageOutputPatternEditorForm form;
            private readonly Label lbInfo;
            private readonly CheckBox[] boxes;

            public PatternCheckBoxes(Clock4StageOutputPatternEditorForm form, Label lbInfo, params CheckBox[] boxes)
            {
                if (boxes.Length != 2)
                    throw new ArgumentException("2 boxes expected");
                this.form = form;
                this.lbInfo = lbInfo;
                this.boxes = boxes;

                foreach (var cb in boxes)
                {
                    cb.CheckedChanged += (s, x) => form.UpdateAllUI();
                }
            }

            /// <summary>
            /// Set the checked property of the checkboxes.
            /// </summary>
            internal void Load(int pattern)
            {
                boxes[0].Checked = ((pattern & 0x01) != 0);
                boxes[1].Checked = ((pattern & 0x02) != 0);
            }

            /// <summary>
            /// Calculate the checked pattern.
            /// </summary>
            internal int GetPattern()
            {
                var pattern = 0;
                if (boxes[0].Checked) pattern |= 0x01;
                if (boxes[1].Checked) pattern |= 0x02;
                return pattern;
            }

            /// <summary>
            /// Update entire UI.
            /// </summary>
            internal void UpdateUI()
            {
                OnBoxCheckedChanged();
            }

            /// <summary>
            /// Checkbox state has changed.
            /// </summary>
            private void OnBoxCheckedChanged()
            {
                if (form.Patterns.Any(x => (x != this) && (x.GetPattern() == GetPattern())))
                {
                    // Found exactly the same pattern
                    lbInfo.Text = "Duplicate";
                }
                else
                {
                    lbInfo.Text = "Valid";
                }
            }
        }
    }
}
