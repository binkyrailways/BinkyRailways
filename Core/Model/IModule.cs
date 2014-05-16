using System.IO;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    public interface IModule : IPersistentEntity, IImportableEntity 
    {
        /// <summary>
        /// Gets all blocks contained in this module.
        /// </summary>
        IEntitySet2<IBlock> Blocks { get; }

        /// <summary>
        /// Gets all block groups contained in this module.
        /// </summary>
        IEntitySet2<IBlockGroup> BlockGroups { get; }

        /// <summary>
        /// Gets all edges of this module.
        /// </summary>
        IEntitySet2<IEdge> Edges { get; }

        /// <summary>
        /// Gets all junctions contained in this module.
        /// </summary>
        IJunctionSet Junctions { get; }

        /// <summary>
        /// Gets all sensors contained in this module.
        /// </summary>
        ISensorSet Sensors { get; }

        /// <summary>
        /// Gets all signals contained in this module.
        /// </summary>
        ISignalSet Signals { get; }

        /// <summary>
        /// Gets all outputs contained in this module.
        /// </summary>
        IOutputSet Outputs { get; }

        /// <summary>
        /// Gets all routes contained in this module.
        /// </summary>
        IEntitySet2<IRoute> Routes { get; }

        /// <summary>
        /// Gets/sets the background image of the this module.
        /// </summary>
        /// <value>Null if there is no image.</value>
        /// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
        Stream BackgroundImage { get; set; }

        /// <summary>
        /// Horizontal size (in pixels) of this entity.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Vertical size (in pixels) of this entity.
        /// </summary>
        int Height { get; }
    }
}
