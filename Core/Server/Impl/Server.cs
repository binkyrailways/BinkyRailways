using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;

namespace BinkyRailways.Core.Server.Impl
{
    public class Server : IServer
    {
        IRailway railway;
        IRailwayState railwayState;

        public Server()
        {

        }

        /// <summary>
        /// Currently active railway
        /// </summary>
        IRailway IServer.Railway
        {
            get => railway;
            set
            {
                railway = value;
            }
        }

        /// <summary>
        /// Currently active railway state.
        /// </summary>
        IRailwayState IServer.RailwayState
        {
            get => railwayState;
            set
            {
                railwayState = value;
            }
        }
    }
}
