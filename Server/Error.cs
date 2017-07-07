using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinkyRailways.Server
{
    public class Error
    {
        public Error(string msg)
        {
            Message = msg;
        }

        [JsonProperty("error")]
        public bool ErrorFlag { get { return true; } }

        [JsonProperty("message")]
        public string Message { get; private set; }
    }
}
