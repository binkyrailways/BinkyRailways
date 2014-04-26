using System;
using System.Globalization;
using System.ComponentModel.Composition.Hosting;
using System.Threading;
using System.Windows.Forms;
using BinkyRailways.WinApp.Forms;
using BinkyRailways.WinApp.Preferences;

namespace BinkyRailways.WinApp
{
    internal static class Program
    {
        // Do not change this name, since it is also used in the setup.
        private const string MUTEX_NAME = "C8F19148-A8C0-402D-9339-750D6617DECE";
        private static Mutex appMutex;
        private static CompositionContainer compositionContainer;
        private static readonly object globalsLock = new object();

        /// <summary>
        /// Get the MEF based composition container.
        /// </summary>
        internal static CompositionContainer CompositionContainer
        {
            get
            {
                if (compositionContainer == null)
                {
                    lock (globalsLock)
                    {
                        if (compositionContainer == null)
                        {
                            var catalog = new AggregateCatalog();
                            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
                            //catalog.Catalogs.Add(new DirectoryCatalog(".", "*.Plugin.dll"));

                            compositionContainer = new CompositionContainer(catalog);
                        }
                    }
                }
                return compositionContainer;
            }
        }

        /// <summary>
        /// Program entry point
        /// </summary>
        [STAThread]
        internal static void Main(string[] args)
        {
            // Create mutex
            appMutex = new Mutex(false, MUTEX_NAME);

            // Register exception handler
            UnhandledExceptionForm.Register();

            // Set locale
            var locale = UserPreferences.Preferences.Locale;
            if (!string.IsNullOrEmpty(locale))
            {
                var cultureInfo = CultureInfo.GetCultureInfo(locale);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }

            // Show main form
            var mainForm = new MainForm(args);
            Application.EnableVisualStyles();
            Application.Run(mainForm);
        }
    }
}
