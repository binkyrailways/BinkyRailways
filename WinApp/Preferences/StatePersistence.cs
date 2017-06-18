using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using Microsoft.Win32;

namespace BinkyRailways.WinApp.Preferences
{
    internal class StatePersistence : IStatePersistence
    {
        private const string ClosedKey = "Closed";
        private const string CurrentBlock = "CurrentBlock";
        private const string CurrentBlockEnterSide = "CurrentBlockEnterSide";
        private const string CurrentDirection = "CurrentDirection";
        private const string HttpPort = "HttpPort";

        /// <summary>
        /// Save the open/closed state of the given block.
        /// </summary>
        public void SetBlockState(IRailwayState railwayState, IBlockState block, bool closed)
        {
            // Write assignment
            using (var key = Registry.CurrentUser.CreateSubKey(GetBlockStateKey(block)))
            {
                key.SetValue(ClosedKey, closed ? 1 : 0);
            }
        }

        /// <summary>
        /// Load the open/closed state of the given block.
        /// </summary>
        /// <returns>True if the state was loaded properly.</returns>
        public bool TryGetBlockState(IRailwayState railwayState, IBlockState block, out bool closed)
        {
            closed = false;
            using (var key = Registry.CurrentUser.OpenSubKey(GetBlockStateKey(block)))
            {
                if (key == null)
                    return false;
                var value = key.GetValue(ClosedKey);
                if (value is int)
                {
                    closed = (((int) value) != 0);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Save the current block state of the given loc.
        /// </summary>
        public void SetLocState(IRailwayState railwayState, ILocState loc, IBlockState currentBlock, BlockSide currentBlockEnterSide, LocDirection currentDirection)
        {
            // Write assignment
            using (var key = Registry.CurrentUser.CreateSubKey(GetKey(loc)))
            {
                key.SetValue(CurrentBlock, (currentBlock != null) ? currentBlock.EntityId : string.Empty);
                key.SetValue(CurrentBlockEnterSide, (currentBlockEnterSide == BlockSide.Back) ? 0 : 1);
                key.SetValue(CurrentDirection, (currentDirection == LocDirection.Forward) ? 0 : 1);
            }
        }

        /// <summary>
        /// Load the current block state of the given loc.
        /// </summary>
        /// <returns>True if the state was loaded properly.</returns>
        public bool TryGetLocState(IRailwayState railwayState, ILocState loc, out IBlockState currentBlock, out BlockSide currentBlockEnterSide, out LocDirection currentDirection)
        {
            currentBlock = null;
            currentBlockEnterSide = BlockSide.Back;
            currentDirection = LocDirection.Forward;

            // Read assignment
            using (var key = Registry.CurrentUser.OpenSubKey(GetKey(loc)))
            {
                if (key == null)
                    return false;
                var blockId = key.GetValue(CurrentBlock) as string;
                if (string.IsNullOrEmpty(blockId))
                    return false;
                var currentBlockState = railwayState.BlockStates.FirstOrDefault(x => x.EntityId == blockId);
                if (currentBlockState == null)
                    return false;
                currentBlock = currentBlockState;
                var enterSide = key.GetValue(CurrentBlockEnterSide);
                if (!(enterSide is int))
                    return false;
                currentBlockEnterSide = ((int)enterSide == 0) ? BlockSide.Back : BlockSide.Front;
                var direction = key.GetValue(CurrentDirection);
                if (!(direction is int))
                    direction = 0;
                currentDirection = ((int)direction == 0) ? LocDirection.Forward : LocDirection.Reverse;
                return true;
            }
        }

        /// <summary>
        /// Try to get the port number for the HTTP connection.
        /// </summary>
        public bool TryGetHttpPort(out int port)
        {
            port = -1;

            using (var key = Registry.CurrentUser.OpenSubKey(GetWebServerKey()))
            {
                if (key == null)
                    return false;
                var httpPort = key.GetValue(HttpPort) as string;
                if (string.IsNullOrEmpty(httpPort))
                    return false;
                return int.TryParse(httpPort, out port);
            }
        }

        /// <summary>
        /// Store the port number for the HTTP connection.
        /// </summary>
        public void SaveHttpPort(int port)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(GetWebServerKey()))
            {
                key.SetValue(HttpPort, port.ToString());
            }
        }

        /// <summary>
        /// Gets a registry key for the given block.
        /// </summary>
        private static string GetBlockStateKey(IBlockState block)
        {
            return UserPreferences.RegistryPrefsBase + @"\BlockStates\" + block.EntityId;
        }

        /// <summary>
        /// Gets a registry key for the given loc.
        /// </summary>
        private static string GetKey(ILocState loc)
        {
            return UserPreferences.RegistryPrefsBase + @"\LocAssignments\" + loc.EntityId;
        }

        /// <summary>
        /// Gets a registry key for the webserver.
        /// </summary>
        private static string GetWebServerKey()
        {
            return UserPreferences.RegistryPrefsBase + @"\WebServer";
        }
    }
}
