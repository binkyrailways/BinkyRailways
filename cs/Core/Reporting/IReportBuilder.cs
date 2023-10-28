namespace BinkyRailways.Core.Reporting
{
    /// <summary>
    /// Interface implemented by all report builders.
    /// </summary>
    public interface IReportBuilder
    {
        /// <summary>
        /// Gets the filename extension for this report.
        /// </summary>
        string ReportExtension { get; }

        /// <summary>
        /// Generate a report now in a file with the given path.
        /// </summary>
        void Generate(string targetPath);
    }
}
