using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace BinkyRailways.Core.Server
{
    internal static class IpUtils
    {
        /// <summary>
        /// Gets the local IP address.
        /// </summary>
        public static string GetIpAddress()
        {
            foreach (var adapter in NetworkInterface.GetAllNetworkInterfaces().Where(x => x.OperationalStatus == OperationalStatus.Up).OrderByDescending(x => x.Speed))
            {
                if (adapter.GetIPv4Statistics().BytesReceived > 0)
                {
                    foreach (var address in adapter.GetIPProperties().UnicastAddresses)
                    {
                        if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                            continue;
                        if (IPAddress.IsLoopback(address.Address))
                            continue;
                        return address.Address.ToString();
                    }
                }
            }
            return "127.0.0.1";
        }

    }
}
