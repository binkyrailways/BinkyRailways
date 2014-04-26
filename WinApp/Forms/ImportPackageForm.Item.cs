using System.Windows.Forms;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Forms
{
    partial class ImportPackageForm
    {
        /// <summary>
        /// Item in the import list
        /// </summary>
        private class Item : ListViewItem
        {
            private readonly IImportableEntity entity;

            /// <summary>
            /// Default ctor
            /// </summary>
            public Item(IImportableEntity sourceEntity, IImportableEntity targetEntity)
            {
                entity = sourceEntity;
                Text = entity.Description;
                SubItems.Add(entity.TypeName);
                var remarks = string.Empty;
                SafeToImport = false;
                switch (sourceEntity.CompareTo(targetEntity))
                {
                    case ImportComparison.TargetDoesNotExists:
                        remarks = string.Empty;
                        SafeToImport = true;
                        break;
                    case ImportComparison.SourceInNewer:
                        remarks = "This item already exists, but is newer then what you have now.";
                        SafeToImport = true;
                        break;
                    case ImportComparison.TargetIsNewer:
                        remarks = "This item already exists and it older then what you have now.";
                        SafeToImport = false;
                        break;
                    case ImportComparison.TargetExists:
                        remarks = "This item already exists. It is unknown which version is older.";
                        SafeToImport = false;
                        break;
                }
                SubItems.Add(remarks);
                Checked = (targetEntity == null);
            }

            /// <summary>
            /// The entity in this item
            /// </summary>
            public IImportableEntity Entity
            {
                get { return entity; }
            }

            /// <summary>
            /// Can this entity be safely imported without overwriting something newer?
            /// </summary>
            public bool SafeToImport { get; private set; }
        }
    }
}
