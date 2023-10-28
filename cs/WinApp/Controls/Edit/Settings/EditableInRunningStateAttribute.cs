using System;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Indicates that a property can be edited while the railway is in running state
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EditableInRunningStateAttribute : Attribute
    {
    }
}
