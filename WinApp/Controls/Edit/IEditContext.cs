using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit
{
    public interface IEditContext
    {
        /// <summary>
        /// Reload the entire package
        /// </summary>
        void ReloadPackage();

        /// <summary>
        /// Gets the current package
        /// </summary>
        IPackage Package { get; }
    }
}
