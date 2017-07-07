using BinkyRailways.Core.Model;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinkyRailways.Server.Modules
{
    public class ModelModule : NancyModule
    {
        private IPackage package; 

        public ModelModule(IContext context)
            : base("/model")
        {
            this.Before.AddItemToStartOfPipeline((ctx) => { 
                package = context.Package;
                if (package == null)
                {
                    return Response.AsJson(new Error("No package available"), HttpStatusCode.InternalServerError);
                }
                return null;
            });
            Get("/", _ => package);
            Get("/cs", _ => Response.AsJson(package.GetCommandStations()));
            Get("/cs/{id}", input =>
            {
                var id = (string)input.id;
                var cs = package.GetCommandStation(id);
                if (cs == null)
                {
                    return Response.AsJson(new Error("No such command station"), HttpStatusCode.NotFound);
                }
                return Response.AsJson(cs);
            });
            Get("/loc", _ => Response.AsJson(package.GetLocs()));
            Get("/loc/{id}", input =>
            {
                var id = (string)input.id;
                var loc = package.GetLoc(id);
                if (loc == null)
                {
                    return Response.AsJson(new Error("No such loc"), HttpStatusCode.NotFound);
                }
                return Response.AsJson(loc);
            });
            Get("/module", _ => Response.AsJson(package.GetModules()));
            Get("/module/{id}", input =>
            {
                var id = (string)input.id;
                var module = package.GetModule(id);
                if (module == null)
                {
                    return Response.AsJson(new Error("No such module"), HttpStatusCode.NotFound);
                }
                return Response.AsJson(module);
            });
            Get("/railway", _ => Response.AsJson(package.Railway));
        }
    }
}
