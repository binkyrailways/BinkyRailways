using System.ComponentModel;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls
{
    /// <summary>
    /// Combobox for all values in an enumerator.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumComboBox<T> : ComboBox
        where T : struct
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public EnumComboBox(ComboBoxStyle dropDownStyle)
        {
            DropDownStyle = dropDownStyle;
        }

        /// <summary>
        /// Default ctor, load items from type converted.
        /// </summary>
        public EnumComboBox(ComboBoxStyle dropDownStyle, TypeConverter converter)
        {
            DropDownStyle = dropDownStyle;
            foreach (T value in converter.GetStandardValues())
            {
                AddItem(converter.ConvertToString(value), value);
            }
        }

        /// <summary>
        /// Add a text + value item.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        protected void AddItem(string text, T value)
        {
            Items.Add(new EnumItem(text, value));
        }

        /// <summary>
        /// Gets / Sets the selected item.
        /// </summary>
        public new T SelectedItem
        {
            get
            {
                EnumItem selection = base.SelectedItem as EnumItem;
                return (selection != null) ? selection.Value : default(T);
            } 
            set
            {
                foreach (EnumItem item in this.Items)
                {
                    if (item.Value.Equals(value))
                    {
                        base.SelectedItem = item;
                        return;
                    }
                }
                base.SelectedItem = null;
            }
        }

        /// <summary>
        /// Item +text wrapper class
        /// </summary>
        private class EnumItem
        {
            private readonly string text;
            private readonly T value;

            internal EnumItem(string text, T value)
            {
                this.text = text;
                this.value = value;
            }

            internal T Value { get { return value; } }

            public override string ToString()
            {
                return text;
            }
        }
    }
}
