/*
Loconet toolbox
Copyright (C) 2010 Modelspoorgroep Venlo, Ewout Prangsma

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/
namespace LocoNetToolBox.Protocol
{
    /// <summary>
    /// Visitor pattern
    /// </summary>
    public abstract class MessageVisitor<TReturn, TData>
    {
        public virtual TReturn Visit(Busy msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(GlobalPowerOn msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(GlobalPowerOff msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(Idle msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(InputReport msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(LinkSlotsRequest msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(LocoAddressRequest msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(LocoDirFuncRequest msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(LocoSpeedRequest msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(LongAck msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(MoveSlotsRequest msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(PeerXferRequest msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(PeerXferRequest1 msg, TData data) { return Visit((PeerXferRequest)msg, data); }
        public virtual TReturn Visit(PeerXferRequest2 msg, TData data) { return Visit((PeerXferRequest)msg, data); }
        public virtual TReturn Visit(PeerXferResponse msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(PeerXferResponse1 msg, TData data) { return Visit((PeerXferResponse)msg, data); }
        public virtual TReturn Visit(SlotDataRequest msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(SlotDataResponse msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(SlotStat1Request msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(SwitchReport msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(SwitchRequest msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(SwitchStateRequest msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(UnlinkSlotsRequest msg, TData data) { return default(TReturn); }
        public virtual TReturn Visit(WriteSlotData msg, TData data) { return default(TReturn); }
    }
}
