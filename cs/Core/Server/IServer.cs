using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using System;

namespace BinkyRailways.Core.Server
{
    public interface IServer : IDisposable
    {
        /// <summary>
        /// Set the currently active railway.
        /// </summary>
        IRailway Railway { get; set; }

        /// <summary>
        /// Set the currently active railway state (if any)s
        /// </summary>
        IRailwayState RailwayState { get; set; }
    }
}
