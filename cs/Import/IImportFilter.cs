using BinkyRailways.Core.Model;

namespace BinkyRailways.Import
{
    public interface IImportFilter
    {
        /// <summary>
        /// Gets the priority of this filter.
        /// </summary>
        ImportFilterPriority Priority { get; }

        /// <summary>
        /// Gets the filter string used for opening files.
        /// </summary>
        string OpenFileDialogFilter { get; }

        /// <summary>
        /// Import the given file into the given railway.
        /// </summary>
        /// <returns>True on a succesful import, false if nothing has changed.</returns>
        bool Import(IPackage target, string path);
    }
}
