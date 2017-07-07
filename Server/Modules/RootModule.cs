using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinkyRailways.Server.Modules
{
    public class RootModule : NancyModule
    {
        public RootModule()
            : base("/")
        {
            Get("/", parameters => "Hello from Binky");
        }
    }
}
