using System.Threading.Tasks;

namespace BinkyRailways.Protocols.Ecos.Objects
{
    /// <summary>
    /// SwitchManager object (id=11)
    /// </summary>
    public class SwitchManager : Object
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SwitchManager(Client client)
            : base(client, IdSwitchManager)
        {
        }

        /// <summary>
        /// Send a query objects command
        /// </summary>
        public new Task QueryObjects()
        {
            return Send(CmdQueryObjects, OptName1).ContinueWith(t => HandleReplyError(t)).ContinueWith(t => OnQueryObjects(t.Result));
        }
    }
}
