using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using BinkyRailways.WinApp.Controls.Edit.Settings;

namespace BinkyRailways.WinApp.UIEditors
{
    /// <summary>
    /// UI type editor for multiline free text
    /// </summary>
    internal class FreeTextEditor : UITypeEditor
    {
        /// <summary>
        /// Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)"/> method.
        /// </summary>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        /// <summary>
        /// Edit an endpoint
        /// </summary>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if ((context != null) && (context.Instance != null) && (provider != null))
            {
                var editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (editorService != null)
                {
                    // Create a listbox
                    var editor = new Controls.Edit.FreeTextEditorControl();
                    editor.Text = (string) (value ?? string.Empty);
                    editor.Cancel += (s, _) => editorService.CloseDropDown();
                    editor.Save += (s, _) => editorService.CloseDropDown();

                    editorService.DropDownControl(editor);
                    return editor.EditedText;
                }
            }

            return value;
        }
    }
}
