using System.Collections.Generic;
using System.Linq;

namespace BinkyRailways.Protocols.Ecos
{
    public class MessageRow : Dictionary<string, Option>
    {
        private readonly int id;

        public MessageRow(int id)
        {
            this.id = id;
        }

        public int Id
        {
            get { return id; }
        }

        internal void AddOption(Option option)
        {
            this[option.Name] = option;
        }

        public override string ToString()
        {
            return id + " " + string.Join(", ", Values.Select(x => x.ToString()));
        }
    }
}
