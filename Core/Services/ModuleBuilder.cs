using System;
using System.Collections.Generic;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Services
{
    /// <summary>
    /// Build up a module by listening to the railway events
    /// </summary>
    public class ModuleBuilder : IDisposable
    {
        private readonly object dataLock = new object();
        private readonly IRailwayState railwayState;
        private readonly IModule module;
        private readonly Dictionary<Address, IEntity> processed = new Dictionary<Address, IEntity>();
        private int lastX;
        private int lastY;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ModuleBuilder(IRailwayState railwayState, IModule module)
        {
            this.railwayState = railwayState;
            this.module = module;
            railwayState.UnknownSensor += OnUnknownSensor;
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            railwayState.UnknownSensor -= OnUnknownSensor;
        }

        /// <summary>
        /// Unknown sensor was detected.
        /// </summary>
        private void OnUnknownSensor(object sender, PropertyEventArgs<Address> e)
        {
            lock (dataLock)
            {
                if (!processed.ContainsKey(e.Value))
                {
                    var sensor = module.Sensors.AddNewBinarySensor();
                    sensor.Address = e.Value;
                    sensor.X = lastX;
                    sensor.Y = lastY;
                    sensor.Description = string.Format("Sensor-{0}-{1}", e.Value.Type, e.Value.Value);
                    processed.Add(e.Value, sensor);

                    lastX += (int)(sensor.Width * 1.5);
                    if (lastX + sensor.Width > module.Width)
                    {
                        lastX = 0;
                        lastY += (int) (sensor.Height*1.5);
                    }
                }
            }
        }
    }
}
