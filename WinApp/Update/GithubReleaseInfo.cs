using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace BinkyRailways.WinApp.Update
{
    public class GithubReleaseInfo
    {
        public GithubReleaseInfo(string url)
        {
            var json = string.Empty;
            using (var webClient = new BinkyWebClient())
            {
                using (var stream = webClient.OpenRead(url))
                {
                    if (stream != null)
                    {
                        json = new StreamReader(stream).ReadToEnd();
                    }
                }
            }

            var release = JsonConvert.DeserializeObject<Release>(json);
            var setupAsset = release.Assets.FirstOrDefault(x => x.Name == "BinkyRailwaysSetup.exe");

            Version = new Version(release.TagName.TrimStart('v'));
            SetupUrl = (setupAsset != null) ? setupAsset.BrowserDownloadUrl : string.Empty;
            SetupArguments = "";
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

        [JsonObject]
        public class Release
        {
            [JsonProperty("tag_name")]
            public string TagName { get; set; }

            [JsonProperty("draft")]
            public bool Draft { get; set; }

            [JsonProperty("prerelease")]
            public bool PreRelease { get; set; }

            [JsonProperty("assets")]
            public Asset[] Assets { get; set; }
        }

        [JsonObject]
        public class Asset
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("browser_download_url")]
            public string BrowserDownloadUrl { get; set; }
        }

        internal class BinkyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                var req = base.GetWebRequest(address) as HttpWebRequest;
                req.UserAgent = "BinkRailways";
                return req;
            }
        }
    }
}
