using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinkyRailways.Protocols.P50x
{
    public class XVersion
    {
        private readonly List<byte[]> rows = new List<byte[]>();

        internal void addRow(byte[] row)
        {
            rows.Add(row);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var first = true;
            foreach (var row in rows)
            {
                if (!first)
                {
                    sb.Append(", ");
                }
                sb.Append(BitConverter.ToString(row));
                first = false;
            }
            return sb.ToString();
        }
    }
}
