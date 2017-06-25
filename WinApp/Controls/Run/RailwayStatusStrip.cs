using BinkyRailways.Core.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.Run
{
    public class RailwayStatusStrip : StatusStrip
    {
        public void Initialize(IRailwayState state)
        {
            Items.Clear();
            if (state != null)
            {
                foreach (var cs in state.CommandStationStates)
                {
                    var lb = new ToolStripLabel(cs.Model.Description);
                    lb.Tag = cs;
                    lb.Image = getPowerImage(cs);
                    Items.Add(lb);
                    cs.Power.ActualChanged += (s, e) => lb.Image = getPowerImage(cs);
                    cs.Power.RequestedChanged += (s, e) => lb.Image = getPowerImage(cs);
                }
            }
        }

        private static Bitmap getPowerImage(ICommandStationState cs)
        {
            if (!cs.Power.IsConsistent)
            {
                return Strings.loc_state_error;
            }
            if (cs.Power.Actual)
            {
                return Strings.loc_state_ok;
            }
            return Strings.loc_state_unassigned;
        }
    }
}
