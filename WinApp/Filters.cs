using BinkyRailways.Core.Storage;

namespace BinkyRailways.WinApp
{
    internal static class Filters
    {
        /// <summary>
        /// Dialog filter used for package files
        /// </summary>
        internal static readonly string PackagesFilter = string.Format(Strings.PackageFilterX, "*" + Package.DefaultExt);

        /// <summary>
        /// Dialog filter used for *.*
        /// </summary>
        internal static readonly string AllFilesFilter = Strings.AllFilesFilter;

        /// <summary>
        /// Dialog filter used for package files
        /// </summary>
        internal static readonly string PackagesOrAllFilter = PackagesFilter + "|" + AllFilesFilter;

        /// <summary>
        /// Dialog filter used for image files
        /// </summary>
        internal static readonly string ImageFilter = Strings.ImageFiles + "|*.png;*.bmp;*.gif;*.jpg;*.jpeg;*.wmf;*.emf";

        /// <summary>
        /// Dialog filter used for png files
        /// </summary>
        internal static readonly string PngFilter = Strings.ImageFiles + "|*.png";

        /// <summary>
        /// Dialog filter used for sound files
        /// </summary>
        internal static readonly string SoundFilter = Strings.SoundFiles + "|*.wav";
    }
}
