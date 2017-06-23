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
                return new Address(cbType.SelectedItem, tbAddressSpace.Text.Trim(), tbValue.Text);                
            }
            set
            {
                cbType.SelectedItem = (value != null) ? value.Type : AddressType.Dcc;
                tbValue.Text = (value != null) ? value.Value : string.Empty;
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
            var value = tbValue.Text;
            var minValue = type.MinValue();
            var maxValue = type.MaxValue();

            // Limit value such that it fits within min/max value
            if (type.RequiresNumericValue()) 
            {
                int nValue;
                if (int.TryParse(value, out nValue)) 
                {
                    if ((nValue < minValue) || (nValue > maxValue))
                    {
                        //tbValue.Minimum = int.MinValue;
                        //tbValue.Maximum = int.MaxValue;
                        tbValue.Text = Math.Min(Math.Max(minValue, nValue), maxValue).ToString();
                    }
                }
            }
//            tbValue.Minimum = minValue;
//            tbValue.Maximum = maxValue;            
        }
    }
}
