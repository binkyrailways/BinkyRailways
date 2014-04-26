using System;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Forms
{
    public partial class TimePeriodEditorForm : AppForm
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public TimePeriodEditorForm()
        {
            InitializeComponent();
            OnTextChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Start time
        /// </summary>
        public Time PeriodStart
        {
            get
            {
                Time value;
                return Time.TryParse(tbStart.Text, out value) ? value : Time.MinValue;
            }
            set { tbStart.Text = value.ToString(); }
        }

        /// <summary>
        /// End time
        /// </summary>
        public Time PeriodEnd
        {
            get
            {
                Time value;
                return Time.TryParse(tbEnd.Text, out value) ? value : Time.MaxValue;
            }
            set { tbEnd.Text = value.ToString(); }
        }

        /// <summary>
        /// Validate input
        /// </summary>
        private void OnTextChanged(object sender, System.EventArgs e)
        {
            var isValid = false;
            Time start, end;
            if (!Time.TryParse(tbStart.Text, out start))
            {
                lbMessage.Text = Strings.EnterValidStartTime;
            }
            else if (!Time.TryParse(tbEnd.Text, out end))
            {
                lbMessage.Text = Strings.EnterValidEndTime;                
            } else if (start > end)
            {
                lbMessage.Text = Strings.StartTimeMustBeBeforeEndTime;
            }
            else
            {
                isValid = true;
                lbMessage.Text = "";
            }
            cmdOk.Enabled = isValid;
        }
    }
}
