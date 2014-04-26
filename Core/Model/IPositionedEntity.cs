using System;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Entity with graphical position
    /// </summary>
    public interface IPositionedEntity : IEntity
    {
        /// <summary>
        /// The position or size of this entity has changed.
        /// </summary>
        event EventHandler PositionChanged;

        /// <summary>
        /// Horizontal left position (in pixels) of this entity.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// Vertical top position (in pixels) of this entity.
        /// </summary>
        int Y { get; set; }

        /// <summary>
        /// Horizontal size (in pixels) of this entity.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Vertical size (in pixels) of this entity.
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// Rotation in degrees of the content of this entity.
        /// </summary>
        int Rotation { get; set; }

        /// <summary>
        /// If set, the mouse will no longer move and/or resize this entity.
        /// </summary>
        bool Locked { get; set; }
    }
}
