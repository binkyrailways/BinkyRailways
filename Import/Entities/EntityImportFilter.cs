using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Storage;
using BinkyRailways.WinApp;
using BinkyRailways.WinApp.Forms;

namespace BinkyRailways.Import.Entities
{
    /// <summary>
    /// Import filter that imports Binky Railway entities
    /// </summary>
    [Export(typeof(IImportFilter))]
    public sealed class EntityImportFilter : IImportFilter
    {
        /// <summary>
        /// Gets the priority of this filter.
        /// </summary>
        ImportFilterPriority IImportFilter.Priority { get { return ImportFilterPriority.ThirdPartyFileFormat; } }

        /// <summary>
        /// Gets the filter string used for opening files.
        /// </summary>
        string IImportFilter.OpenFileDialogFilter { get { return Filters.PackagesFilter; } }

        /// <summary>
        /// Import the given file into the given railway.
        /// </summary>
        /// <returns>True on a succesful import, false if nothing has changed.</returns>
        bool IImportFilter.Import(IPackage target, string path)
        {
            IPackage source;
            try
            {
                source = Package.Load(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Strings.OpenPackageFailedBecauseX, ex.Message), Strings.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using (var dialog = new ImportPackageForm(target, source))
            {
                return (dialog.ShowDialog() == DialogResult.OK);
            }
        }
    }
}
