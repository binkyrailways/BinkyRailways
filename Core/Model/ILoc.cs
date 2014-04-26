using System.IO;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Single locomotive.
    /// </summary>
    public interface ILoc : IAddressEntity, IPersistentEntity, IImportableEntity 
    {
        /// <summary>
        /// Percentage of speed steps for the slowest speed of this loc.
        /// Value between 1 and 100.
        /// </summary>
        int SlowSpeed { get; set; }

        /// <summary>
        /// Percentage of speed steps for the medium speed of this loc.
        /// Value between 1 and 100.
        /// </summary>
        int MediumSpeed { get; set; }

        /// <summary>
        /// Percentage of speed steps for the maximum speed of this loc.
        /// Value between 1 and 100.
        /// </summary>
        int MaximumSpeed { get; set; }

        /// <summary>
        /// Gets the number of speed steps supported by this loc.
        /// </summary>
        int SpeedSteps { get; }

        /// <summary>
        /// Gets/sets the image of the given loc.
        /// </summary>
        /// <value>Null if there is no image.</value>
        /// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
        Stream Image { get; set; }

        /// <summary>
        /// Is it allowed for this loc to change direction?
        /// </summary>
        ChangeDirection ChangeDirection { get; set; }

        /// <summary>
        /// Gets the names of all functions supported by this loc.
        /// </summary>
        ILocFunctions Functions { get; }

        /// <summary>
        /// Gets/sets the name of the person that owns this loc.
        /// </summary>
        string Owner { get; set; }

        /// <summary>
        /// Gets/sets remarks (free text) about this loc.
        /// </summary>
        string Remarks { get; set; }
    }
}
