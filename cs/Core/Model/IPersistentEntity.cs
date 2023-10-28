using System;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Base class for objects that have their own persistency.
    /// In other words, each persistent entity will result in it's own XML file.
    /// </summary>
    public interface IPersistentEntity : IEntity
    {
        /// <summary>
        /// Gets last modification date in UTC time.
        /// </summary>
        DateTime LastModifiedUtc { get; }

        /// <summary>
        /// Gets last modification date in local time.
        /// </summary>
        DateTime LastModified { get; }
    }
}
