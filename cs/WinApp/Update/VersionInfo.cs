using System;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace BinkyRailways.WinApp.Update
{
    /// <summary>
    /// Update information found on the server.
    /// </summary>
    internal class VersionInfo
    {
        /// <summary>
        /// Load the data of this version information file from the given url
        /// </summary>
        public VersionInfo(string url)
        {
            var xml = string.Empty;
            using (var webClient = new WebClient())
            {
                using (var stream = webClient.OpenRead(url))
                {
                    if (stream != null)
                    {
                        xml = new StreamReader(stream).ReadToEnd();
                    }
                }
            }

            var root = string.IsNullOrEmpty(xml) ? null : XDocument.Parse(xml).Root;
            var version = (root != null) ? root.Element("Version") : null;
            var setupUrl = (root != null) ? root.Element("SetupUrl") : null;
            var setupArgs = (root != null) ? root.Element("SetupArguments") : null;

            Version = (version != null) ? new Version(version.Value) : new Version(0, 0, 0, 0);
            SetupUrl = (setupUrl != null) ? setupUrl.Value : string.Empty;
            SetupArguments = (setupArgs != null) ? setupArgs.Value : string.Empty;            
        }

        /// <summary>
        /// Version available on the server.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// URL for the installer
        /// </summary>
        public string SetupUrl { get; set; }

        /// <summary>
        /// Arguments needed to pass to installer
        /// </summary>
        public string SetupArguments { get; set; }
    }
}
