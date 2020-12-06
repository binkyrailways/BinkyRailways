using System;
using LocoNetToolBox.Protocol;

namespace BinkyRailways.Core.State.LocoNet
{
    /// <summary>
    /// Single slot in the masters slot table.
    /// </summary>
    public class Slot : IEquatable<Slot>
    {
        private readonly int slotNumber;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Slot(int slotNumber)
        {
            this.slotNumber = slotNumber;
            LastUsed = DateTime.Now;
        }

        /// <summary>
        /// Initialize from msg
        /// </summary>
        public Slot(SlotDataResponse msg)
            : this(msg.Slot)
        {
            Status1 = msg.Status1;
            Address = msg.Address;
            Speed = msg.Speed;
            DirF = msg.DirF;
            Track = msg.TrackStatus;
            Status2 = msg.Status2;
            Sound = msg.Sound;
        }

        /// <summary>
        /// Number of this slot
        /// </summary>
        public int SlotNumber
        {
            get { return slotNumber; }
        }

        /// <summary>
        /// Time stamp of last usage
        /// </summary>
        public DateTime LastUsed { get; set; }

        /// <summary>
        /// Set the last used time stamp to now.
        /// </summary>
        public void Touch()
        {
            LastUsed = DateTime.Now;
        }

        /// <summary>
        /// Has this slot been updated less than 100 seconds ago?
        /// </summary>
        public bool IsUpdate2Date
        {
            get { return (DateTime.Now.Subtract(LastUsed).TotalSeconds < 100); }
        }

        /// <summary>
        /// Has this slot been updated more than 200 seconds ago?
        /// </summary>
        public bool ShouldPurge
        {
            get { return (DateTime.Now.Subtract(LastUsed).TotalSeconds >= 200); }
        }

        /// <summary>
        /// Status 1.
        /// </summary>
        public SlotStatus1 Status1 { get; set; }

        /// <summary>
        /// Loco address in this slot.
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        /// Speed in steps.
        /// 0×00=SPEED 0 ,STOP
        /// 0×01=SPEED 0 EMERGENCY stop
        /// 0×02-0x7F increasing SPEED, 0x7F=MAX speed
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Gets DirF argument from this slot.
        /// </summary>
        public DirFunc DirF { get; set; }

        /// <summary>
        /// Track status
        /// </summary>
        public TrackStatus Track { get; set; }

        /// <summary>
        /// Secondary status field
        /// </summary>
        public SlotStatus2 Status2 { get; set; }

        /// <summary>
        /// Gets Sound argument from this slot.
        /// </summary>
        public Sound Sound { get; set;  }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        public bool Equals(Slot other)
        {
            return (other != null) &&
                   (slotNumber == other.slotNumber) &&
                   (Status1 == other.Status1) &&
                   (Address == other.Address) &&
                   (Speed == other.Speed) &&
                   (DirF == other.DirF) &&
                   (Track == other.Track) &&
                   (Status2 == other.Status2) &&
                   (Sound == other.Sound);
        }

        /// <summary>
        /// If this equal to obj?
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as Slot);
        }

        /// <summary>
        /// Hashing
        /// </summary>
        public override int GetHashCode()
        {
            return slotNumber << 8;
        }
    }
}
