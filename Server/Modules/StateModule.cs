using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.Core.State.Impl;
using Nancy;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinkyRailways.Server.Modules
{
    public class StateModule : NancyModule
    {
        private IPackage package;
        private IRailwayState state;

        public StateModule(IContext context)
            : base("/state")
        {
            this.Before.AddItemToStartOfPipeline((ctx) =>
            {
                package = context.Package;
                if (package == null)
                {
                    return Response.AsJson(new Error("No package available"), HttpStatusCode.InternalServerError);
                }
                state = context.State ;
                if (state == null)
                {
                    return Response.AsJson(new Error("No state available"), HttpStatusCode.PreconditionFailed);
                }
                return null;
            });
            Get("/", _ => Response.AsJson(state));
            Get("/cs", _ => Response.AsJson(state.CommandStationStates));
            Get("/cs/{id}", input =>
            {
                var id = (string)input.id;
                var cs = package.GetCommandStation(id);
                if (cs == null)
                {
                    return Response.AsJson(new Error("No such command station"), HttpStatusCode.NotFound);
                }
                var csState = state.CommandStationStates[cs];
                return Response.AsJson(csState);
            });
            Get("/loc", _ => Response.AsJson(state.LocStates));
            Get("/loc/{id}", input =>
            {
                var id = (string)input.id;
                var loc = package.GetLoc(id);
                if (loc == null)
                {
                    return Response.AsJson(new Error("No such loc"), HttpStatusCode.NotFound);
                }
                var locState = state.LocStates[loc];
                return Response.AsJson<ILocState>(locState);
            });
            Get("/railway", _ => Response.AsJson(state));
        }
    }
}
