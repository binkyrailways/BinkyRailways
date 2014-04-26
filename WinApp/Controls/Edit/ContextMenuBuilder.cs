using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Forms;

namespace BinkyRailways.WinApp.Controls.Edit
{
    /// <summary>
    /// Builder for context menu of a node during editing
    /// </summary>
    internal sealed class ContextMenuBuilder : EntityVisitor<object, ContextMenuStrip>
    {
        private readonly IPackage package;
        private readonly IEditContext context;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ContextMenuBuilder(IPackage package, IEditContext context)
        {
            this.package = package;
            this.context = context;
        }

        public override object Visit(IEcosCommandStation entity, ContextMenuStrip data)
        {
            data.Items.Add("Read...", null, (s, x) => OnReadEcos(entity));
            return base.Visit(entity, data);
        }

        /// <summary>
        /// Read all data from the given command station.
        /// </summary>
        private void OnReadEcos(IEcosCommandStation entity)
        {
            using (var dialog = new EcosReaderForm(package, entity))
            {
                dialog.ShowDialog();
                context.ReloadPackage();
            }
        }
    }
}
