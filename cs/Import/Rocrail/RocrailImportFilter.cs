using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp;
using BinkyRailways.WinApp.Forms;

namespace BinkyRailways.Import.Rocrail
{
    /// <summary>
    /// Import filter that imports Rocrail modules
    /// </summary>
    [Export(typeof(IImportFilter))]
    public class RocrailImportFilter : IImportFilter
    {
        /// <summary>
        /// Gets the priority of this filter.
        /// </summary>
        ImportFilterPriority IImportFilter.Priority { get { return ImportFilterPriority.ThirdPartyFileFormat; } }

        /// <summary>
        /// Gets the filter string used for opening files.
        /// </summary>
        string IImportFilter.OpenFileDialogFilter { get { return "Rocrail modules|*.xml"; } }

        /// <summary>
        /// Import the given file into the given railway.
        /// </summary>
        /// <returns>True on a succesful import, false if nothing has changed.</returns>
        bool IImportFilter.Import(IPackage target, string path)
        {
            XElement root;
            try
            {
                var doc = XDocument.Load(path);
                root = doc.Root;
                if (root == null)
                    throw new ArgumentException("File has no root");
                var localName = root.Name.LocalName;
                if ((localName != "plan") && (localName != "modplan"))
                    throw new ArgumentException("File is not a rocrail module");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Failed to open Rocrail module: {0}", ex.Message), Strings.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            bool result;
            List<ImportMessage> messages;

            if (root.Name.LocalName == "plan")
            {
                var importer = new ModuleImporter(root, path);
                result = (importer.Import(target) != null);
                messages = importer.Messages.ToList();
            }
            else 
            {
                var importer = new ModulePlanImporter(root, path);
                result = importer.Import(target);
                messages = importer.Messages.ToList();
            }

            // Show result
            if (messages.Any())
            {
                using (var dialog = new ImportMessagesForm("Import succeeded", messages))
                {
                    dialog.ShowDialog();
                }
            }

            
            return result;
        }
    }
}
