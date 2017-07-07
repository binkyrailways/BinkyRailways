using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinkyRailways.Server
{
    public class Host : IDisposable
    {
        private readonly NancyHost host;

        public Host()
        {
            var config = new HostConfiguration() { UrlReservations = new UrlReservations() { CreateAutomatically = true } };
            host = new NancyHost(config, new Uri("http://localhost:3412"));
        }

        public void Start()
        {
            host.Start();
        }

        public void SetPackage(IPackage p)
        {
            Context.context.Package = p;
        }

        public void SetState(IRailwayState s)
        {
            Context.context.State = s;
        }

        public void Dispose()
        {
            host.Dispose();
        }
    }
}
