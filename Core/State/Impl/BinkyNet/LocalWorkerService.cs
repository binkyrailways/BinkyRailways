using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinkyNet.Apis.V1;
using NLog;

namespace BinkyRailways.Core.State.Impl.BinkyNet
{
    internal class LocalWorkerServiceImpl : LocalWorkerService.LocalWorkerServiceBase
    {
        private readonly BinkyNetCommandStationState cst;
        private readonly Logger Log;
        private readonly BlockingCollection<bool> powerQueue = new BlockingCollection<bool>();

        public LocalWorkerServiceImpl(BinkyNetCommandStationState cst,   Logger log)
        {
            this.cst = cst;
            Log = log;
        }

        internal bool Stop { get; set; }

        public override Task GetLocRequests(LocRequestsOptions request, global::Grpc.Core.IServerStreamWriter<Loc> responseStream, global::Grpc.Core.ServerCallContext context)
        {
            return base.GetLocRequests(request, responseStream, context);
        }

        internal void SendPower(bool enabled)
        {
            powerQueue.Add(enabled);
        }

        public override async Task GetPowerRequests(PowerRequestsOptions request, global::Grpc.Core.IServerStreamWriter<Power> responseStream, global::Grpc.Core.ServerCallContext context)
        {
            while (true)
            {
                bool enabled;
                if (powerQueue.TryTake(out enabled, 1000))
                {
                    await responseStream.WriteAsync(new Power { Enabled = enabled });
                }
                if(Stop)
                {
                    break;
                }
            }
        }

        public override async Task<Empty> Ping(LocalWorkerInfo request, global::Grpc.Core.ServerCallContext context)
        {
            Log.Info("Ping from " + request.Id);
            return new Empty();
        }

        public override Task<Empty> SetLocActuals(global::Grpc.Core.IAsyncStreamReader<Loc> requestStream, global::Grpc.Core.ServerCallContext context)
        {
            return base.SetLocActuals(requestStream, context);
        }

        public override async Task<Empty> SetPowerActuals(global::Grpc.Core.IAsyncStreamReader<Power> requestStream, global::Grpc.Core.ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var msg = requestStream.Current;
                cst.Power.Actual = msg.Enabled;
            }

            return new Empty();
        }
    }
}
