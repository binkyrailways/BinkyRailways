using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinkyRailways.Server
{
    public interface IContext
    {
        IPackage Package { get; }
        IRailwayState State { get; }
    }
}
