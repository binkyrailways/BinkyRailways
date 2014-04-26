using System.Linq;
using BinkyRailways.Core.State;
using Microsoft.Win32;

namespace BinkyRailways.WinApp.Preferences
{
    /// <summary>
    /// Last known block assignments for locs.
    /// </summary>
    public static class LocAssignments
    {
        private const string CurrentBlock = "CurrentBlock";

        /// <summary>
        /// Save the current assignment of the given loc.
        /// </summary>
        public static void SaveLocAssignment(ILocState  loc)
        {
            var block = loc.CurrentBlock.Actual;
            if (block != null)
            {
                // Write assignment
                using (var key = Registry.CurrentUser.CreateSubKey(GetKey(loc)))
                {
                    key.SetValue(CurrentBlock, block.Entity.Id);
                }
            }
        }

        /// <summary>
        /// Load the current assignment of the given loc.
        /// </summary>
        public static IBlockState LoadLocAssignment(ILocState loc)
        {
            // Read assignment
            using (var key = Registry.CurrentUser.OpenSubKey(GetKey(loc)))
            {
                if (key == null)
                    return null;
                var blockId = key.GetValue(CurrentBlock) as string;
                if (string.IsNullOrEmpty(blockId))
                    return null;
                return loc.RailwayState.BlockStates.FirstOrDefault(x => x.Entity.Id == blockId);
            }
        }

        /// <summary>
        /// Gets a registry key for the given loc.
        /// </summary>
        private static string GetKey(ILocState loc)
        {
            return UserPreferences.RegistryPrefsBase + @"\LocAssignments\" + loc.Entity.Id;
        }
    }
}
