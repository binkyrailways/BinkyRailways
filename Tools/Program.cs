using System;
using System.Diagnostics;
using System.IO;
using BinkyRailways.Core.Storage;

namespace BinkyRailways.Tools
{
    internal static class Program
    {
        /// <summary>
        /// Entry point
        /// </summary>
        public static int Main(string[] args)
        {
            try
            {
                if ((args.Length == 3) && (args[0] == "-export"))
                {
                    Export(args[1], args[2]);
                    return 0;
                }
                if ((args.Length == 3) && (args[0] == "-import"))
                {
                    Import(args[1], args[2]);
                    return 0;
                }
                if ((args.Length == 4) && (args[0] == "-diff"))
                {
                    Diff(args[1], args[2], args[3]);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            return 1;
        }

        /// <summary>
        /// Export a package
        /// </summary>
        private static void Export(string source, string target)
        {
            if (!File.Exists(source))
                throw new ArgumentException("Source not found: " + source);
            target = Path.GetFullPath(target);
            var targetFolder = Path.GetDirectoryName(target);
            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            var pkg = (Package)Package.Load(source);
            pkg.Export(target);
        }

        /// <summary>
        /// Import a package
        /// </summary>
        private static void Import(string source, string target)
        {
            if (!File.Exists(source))
                throw new ArgumentException("Source not found: " + source);
            target = Path.GetFullPath(target);
            var targetFolder = Path.GetDirectoryName(target);
            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            var pkg = Package.Import(source);
            pkg.Save(target);
        }

        /// <summary>
        /// Diff two packages
        /// </summary>
        private static void Diff(string baseFile, string mineFile, string tortoiseMerge)
        {
            var tmpBase = Path.GetTempFileName();
            var tmpMine = Path.GetTempFileName();

            Export(baseFile, tmpBase);
            Export(mineFile, tmpMine);

            var process = Process.Start(tortoiseMerge, string.Format("\"{0}\" \"{1}\"", tmpBase, tmpMine));
            process.WaitForExit();
        }
    }
}
