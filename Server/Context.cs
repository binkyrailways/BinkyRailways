using Nancy;
using Nancy.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinkyRailways.Server
{
    internal class Context : IContext
    {
        internal static Context context = new Context();

        public Core.Model.IPackage Package
        {
            get; set;
        }

        public Core.State.IRailwayState State
        {
            get;set;
        }
    }

    public class ContextRegistrations : Registrations
    {
        public ContextRegistrations(ITypeCatalog typeCatalog)
            : base(typeCatalog)
        {
            this.Register<IContext>(Context.context);
        }
    }

}
