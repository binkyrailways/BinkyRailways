using System.Drawing;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Items
{
    /// <summary>
    /// Base class for standard items such as labels and buttons.
    /// </summary>
    public abstract class VCBaseItem : VCItem
    {
        public readonly static Font DefaultFont = new Font(FontFamily.GenericSansSerif, 10.0f);
        private Font font = DefaultFont;
        private Color textColor = Color.Black;
        private string text = string.Empty;
        private Size size = new Size(32, 16);
        private bool enabled = true;
        private bool mouseOver = false;
        private bool mouseDown = false;

        /// <summary>
        /// Default ctor
        /// </summary>
        public VCBaseItem()
        {
        }

        /// <summary>
        /// Is the mouse over this item
        /// </summary>
        public bool IsMouseOver
        {
            get { return mouseOver; }
            protected set
            {
                if (mouseOver != value)
                {
                    mouseOver = value;
                    OnMouseOverChanged();
                }
            }
        }

        /// <summary>
        /// IsMouseOver setting has changed.
        /// </summary>
        protected virtual void OnMouseOverChanged()
        {
        }

        /// <summary>
        /// Is the mouse button down over this item?
        /// </summary>
        public bool IsMouseDown
        {
            get { return mouseDown; }
            protected set
            {
                if (mouseDown != value)
                {
                    mouseDown = value;
                    OnMouseDownChanged();
                }
            }
        }

        /// <summary>
        /// IsMouseDown setting has changed.
        /// </summary>
        protected virtual void OnMouseDownChanged()
        {
        }

        /// <summary>
        /// Gets / Sets the text of the label
        /// </summary>
        public string Text
        {
            get { return text; }
            set 
            {
                if (value == null) { value = string.Empty; }
                if (text != value)
                {
                    text = value;
                    OnTextChanged();
                }
            }
        }

        /// <summary>
        /// This method is called when the text has changed.
        /// </summary>
        protected virtual void OnTextChanged()
        {
            RaiseSizeChanged();
        }

        /// <summary>
        /// Gets font used to draw the text.
        /// </summary>
        public Font Font
        {
            get { return font; }
            set 
            {
                if (font != value)
                {
                    font = value;
                    RaiseSizeChanged();
                }
            }
        }

        /// <summary>
        /// Gets / Sets color of text
        /// </summary>
        public Color TextColor
        {
            get { return textColor; }
            set
            {
                if (textColor != value)
                {
                    textColor = value;
                    RaiseRedraw();
                }
            }
        }

        /// <summary>
        /// Is this item enabled.
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    RaiseRedraw();
                }
            }
        }

        /// <summary>
        /// Gets / sets actual size of this item
        /// </summary>
        public override Size Size
        {
            get { return size; }
            set 
            {
                if (size != value)
                {
                    size = value;
                    RaiseSizeChanged();
                }
            }
        }
    }
}
