using System;
using System.Diagnostics;
using BinkyRailways.Core.Util;
using NLog;

namespace BinkyRailways.WinApp.Update
{
    /// <summary>
    /// Helper used to check for updates 
    /// </summary>
    internal class UpdateChecker
    {
        public event EventHandler<PropertyEventArgs<Version>> UpdateAvailable;

        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private VersionInfo versionInfo;
        private const string VersionInfoUrl = "http://www.binkyrailways.net/download/version.xml";

        /// <summary>
        /// Check for updates now
        /// </summary>
        public UpdateCheckResult Check()
        {
            try
            {
                versionInfo = new VersionInfo(VersionInfoUrl);
                if ((versionInfo.Version > GetType().Assembly.GetName().Version) && !string.IsNullOrEmpty(versionInfo.SetupUrl))
                {
                    // There is a new version
                    UpdateAvailable.Fire(this, new PropertyEventArgs<Version>(versionInfo.Version));
                    return UpdateCheckResult.UpdateAvailable;
                }
                return UpdateCheckResult.Up2Date;
            }
            catch (Exception ex)
            {
                log.WarnException(Strings.UpdateCheckFailed, ex);
                return UpdateCheckResult.CheckFailed;
            }
        }

        /// <summary>
        /// Gets the latest available version.
        /// </summary>
        public Version LatestVersion
        {
            get { return (versionInfo != null) ? versionInfo.Version : GetType().Assembly.GetName().Version; }
        }

        /// <summary>
        /// Gets download URL
        /// </summary>
        public string SetupUrl
        {
            get { return (versionInfo != null) ? versionInfo.SetupUrl : string.Empty; }
        }

        /// <summary>
        /// Download the update
        /// </summary>
        public void InstallUpdate()
        {
            try
            {
                Process.Start(versionInfo.SetupUrl);
            }
            catch (Exception ex)
            {
                log.ErrorException("Cannot open download url.", ex);
            }
        }
    }
}
