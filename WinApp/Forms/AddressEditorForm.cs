using System;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Forms
{
    public partial class AddressEditorForm : AppForm
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public AddressEditorForm()
        {
            InitializeComponent();
            cbType.SelectedItem = AddressType.Dcc;
            UpdateMinMax();
        }

        /// <summary>
        /// Gets/sets the address
        /// </summary>
        public Address Address
        {
            get
            {
                return new Address(cbType.SelectedItem, tbAddressSpace.Text.Trim(), (int)tbValue.Value);                
            }
            set
            {
                cbType.SelectedItem = (value != null) ? value.Type : AddressType.Dcc;
                tbValue.Value = (value != null) ? value.Value : 1;
                tbAddressSpace.Text = (value != null) ? value.AddressSpace : string.Empty;
            }
        }

        /// <summary>
        /// Address type has changed.
        /// </summary>
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMinMax();
        }

        /// <summary>
        /// Update the minimum and maximum value.
        /// </summary>
        private void UpdateMinMax()
        {
            var type = cbType.SelectedItem;
            var value = tbValue.Value;
            var minValue = type.MinValue();
            var maxValue = type.MaxValue();

            // Limit value such that it fits within min/max value
            if ((value < minValue) || (value > maxValue))
            {
                tbValue.Minimum = int.MinValue;
                tbValue.Maximum = int.MaxValue;
                tbValue.Value = Math.Min(Math.Max(minValue, value), maxValue);
            }
            tbValue.Minimum = minValue;
            tbValue.Maximum = maxValue;            
        }
    }
}
