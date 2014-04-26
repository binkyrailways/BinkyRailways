using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Single command station reference.
    /// </summary>
    public sealed class CommandStationRef : PersistentEntityRef<ICommandStation>, ICommandStationRef
    {
        private readonly Property<string[]> addressSpaces;

        /// <summary>
        /// Default ctor
        /// </summary>
        public CommandStationRef()
        {
            addressSpaces = new Property<string[]>(this, Empty<string>.Array);
        }

        /// <summary>
        /// The names of address spaces served by this command station
        /// </summary>
        /// <remarks>Should never return null</remarks>
        public string[] AddressSpaces
        {
            get { return addressSpaces.Value; }
            set { addressSpaces.Value = value ?? Empty<string>.Array; }
        }
        
        /// <summary>
        /// Try to resolve the entity.
        /// </summary>
        /// <returns>True on success, false otherwise</returns>
        public override bool TryResolve(out ICommandStation entity)
        {
            entity = null;
            var railway = Railway;
            if (railway == null)
                return false;
            entity = railway.Package.GetCommandStation(Id);
            return (entity != null);
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }
    }
}
