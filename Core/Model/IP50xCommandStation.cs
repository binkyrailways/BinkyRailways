using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Command station using the P50x protocol.
    /// </summary>
    public interface IP50xCommandStation : ISerialPortCommandStation
    {
    }
}
