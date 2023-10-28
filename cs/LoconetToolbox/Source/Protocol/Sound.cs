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
using System;

namespace LocoNetToolBox.Protocol
{
    /// <summary>
    /// SOUND bits
    /// </summary>
    [Flags]
    public enum Sound
    {
        Snd1 = 0x01,
        Snd2 = 0x02,
        Snd3 = 0x04,
        Snd4 = 0x08,
        None = 0,
    }
}
