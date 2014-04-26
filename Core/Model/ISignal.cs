namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Device that outputs some signal on the railway.
    /// </summary>
    public interface ISignal : IAddressEntity, IPositionedEntity, IModuleEntity
    {
    }
}
