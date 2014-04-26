using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp
{
    /// <summary>
    /// Data container for drag/drop support
    /// </summary>
    public class EntityStateContainer
    {
        public EntityStateContainer(IEntityState state)
        {
            State = state;
        }

        public IEntityState State { get; private set; }
    }
}
