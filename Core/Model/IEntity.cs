using System;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Base class for railway objects.
    /// </summary>
    public interface IEntity : IValidationSubject
    {
        /// <summary>
        /// A property of this entity has changed.
        /// </summary>
        event EventHandler PropertyChanged;

        /// <summary>
        /// Identification value. Must be unique within it's context.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Human readable description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Does this entity generate it's own description?
        /// </summary>
        bool HasAutomaticDescription { get; }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        string TypeName { get; }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data);

        /// <summary>
        /// Find all entities that use this entity.
        /// </summary>
        UsedByInfos GetUsageInfo();
    }
}
