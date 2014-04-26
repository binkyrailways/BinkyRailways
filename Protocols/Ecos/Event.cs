namespace BinkyRailways.Protocols.Ecos
{
    public class Event : Message
    {
        /// <summary>
        /// The ID of the object that is involved in this event
        /// </summary>
        public int Id { get; internal set; }
    }
}
