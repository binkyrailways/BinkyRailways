using System;
using System.Net;

namespace LocoNetToolBox.WinApp
{
    internal class VersionCheckResult
    {
        private const string Url = "http://code.google.com/p/mgv/downloads/list";

        private readonly bool succes;
        private readonly Version latestVersion;

        /// <summary>
        /// Default ctor
        /// </summary>
        private VersionCheckResult(bool succes, Version latestVersion)
        {
            this.succes = succes;
            this.latestVersion = latestVersion;
        }

        public Version LatestVersion
        {
            get { return latestVersion; }
        }

        public bool Succeeded
        {
            get { return succes; }
        }

        /// <summary>
        /// Load the latest version
        /// </summary>
        internal static VersionCheckResult Load()
        {
            const string Prefix = "LocoNetToolboxSetup-";
            try
            {
                var client = new WebClient();
                var contents = client.DownloadString(Url);
                while (true)
                {
                    var index = contents.IndexOf(Prefix, StringComparison.OrdinalIgnoreCase);
                    if (index < 0)
                        return new VersionCheckResult(false, new Version(0, 0, 0, 0));
                    contents = contents.Substring(index + Prefix.Length);

                    Version version;
                    if (TryGetVersion(contents, out version))
                    {
                        return new VersionCheckResult(true, version);
                    }
                }
            }
            catch
            {
                return new VersionCheckResult(false, new Version(0, 0, 0, 0));
            }
        }

        private static bool TryGetVersion(string s, out Version version)
        {
            var index = 0;
            while ((index < s.Length) && ((s[index] == '.') || char.IsDigit(s[index])))
            {
                index++;
            }
            s = s.Substring(0, index);
            if (s.EndsWith("."))
                s = s.Substring(0, s.Length - 1);
            return Version.TryParse(s, out version);
        }
    }
}
