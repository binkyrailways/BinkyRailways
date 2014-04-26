namespace BinkyRailways.Core.Server
{
    public interface IWebServerPersistence
    {
        /// <summary>
        /// Try to get the port number for the HTTP connection.
        /// </summary>
        bool TryGetHttpPort(out int port);

        /// <summary>
        /// Store the port number for the HTTP connection.
        /// </summary>
        void SaveHttpPort(int port);
    }
}
