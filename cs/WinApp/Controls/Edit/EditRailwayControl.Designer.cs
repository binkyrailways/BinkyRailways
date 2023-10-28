using System.Windows.Forms;
using BinkyRailways.WinApp.Controls.Edit.Settings;

namespace BinkyRailways.WinApp.Controls.Edit
{
    partial class EditRailwayControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRailwayControl));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.innerSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tvItems = new BinkyRailways.WinApp.Controls.TreeViewX();
            this.viewEditor = new BinkyRailways.WinApp.Controls.Edit.RailwayViewEditorControl();
            this.propertyGrid = new BinkyRailways.WinApp.Controls.Edit.Settings.SettingsPropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddLoc = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddLocGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddModule = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddModuleConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddCommandStation = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddLocoBuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddDccOverRs232 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddEcos = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddBinkyNet = new System.Windows.Forms.ToolStripMenuItem();
            this.tbbAddP50x = new System.Windows.Forms.ToolStripMenuItem();
            this.tbEdit = new System.Windows.Forms.ToolStripButton();
            this.tbImport = new System.Windows.Forms.ToolStripButton();
            this.tbArchive = new System.Windows.Forms.ToolStripButton();
            this.tbActivate = new System.Windows.Forms.ToolStripButton();
            this.tbRemove = new System.Windows.Forms.ToolStripButton();
            this.tbRun = new System.Windows.Forms.ToolStripSplitButton();
            this.tbRunVirtual = new System.Windows.Forms.ToolStripMenuItem();
            this.validationResultsControl = new BinkyRailways.WinApp.Controls.Edit.ValidationResultsControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.innerSplitContainer)).BeginInit();
            this.innerSplitContainer.Panel1.SuspendLayout();
            this.innerSplitContainer.Panel2.SuspendLayout();
            this.innerSplitContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.splitContainer, 0, 1);
            this.tlpMain.Controls.Add(this.toolStrip1, 0, 0);
            this.tlpMain.Controls.Add(this.validationResultsControl, 0, 2);
            this.tlpMain.Name = "tlpMain";
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.innerSplitContainer);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.propertyGrid);
            // 
            // innerSplitContainer
            // 
            resources.ApplyResources(this.innerSplitContainer, "innerSplitContainer");
            this.innerSplitContainer.Name = "innerSplitContainer";
            // 
            // innerSplitContainer.Panel1
            // 
            this.innerSplitContainer.Panel1.Controls.Add(this.tvItems);
            // 
            // innerSplitContainer.Panel2
            // 
            this.innerSplitContainer.Panel2.Controls.Add(this.viewEditor);
            // 
            // tvItems
            // 
            resources.ApplyResources(this.tvItems, "tvItems");
            this.tvItems.HideSelection = false;
            this.tvItems.Name = "tvItems";
            this.tvItems.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterCollapseOrExpand);
            this.tvItems.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterCollapseOrExpand);
            this.tvItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvItems_AfterSelect);
            this.tvItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvItems_KeyDown);
            this.tvItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvItems_MouseDoubleClick);
            // 
            // viewEditor
            // 
            resources.ApplyResources(this.viewEditor, "viewEditor");
            this.viewEditor.Name = "viewEditor";
            this.viewEditor.SelectionChanged += new System.EventHandler(this.viewEditor_SelectionChanged);
            this.viewEditor.Reload += new System.EventHandler(this.viewEditor_Reload);
            // 
            // propertyGrid
            // 
            resources.ApplyResources(this.propertyGrid, "propertyGrid");
            this.propertyGrid.InRunningState = false;
            this.propertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAdd,
            this.tbEdit,
            this.tbImport,
            this.tbArchive,
            this.tbActivate,
            this.tbRemove,
            this.tbRun});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tbAdd
            // 
            this.tbAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAddLoc,
            this.tbAddLocGroup,
            this.tbAddModule,
            this.tbAddModuleConnection,
            this.tbAddCommandStation});
            this.tbAdd.Image = global::BinkyRailways.Properties.Resources.add_22;
            resources.ApplyResources(this.tbAdd, "tbAdd");
            this.tbAdd.Name = "tbAdd";
            // 
            // tbAddLoc
            // 
            this.tbAddLoc.Name = "tbAddLoc";
            resources.ApplyResources(this.tbAddLoc, "tbAddLoc");
            this.tbAddLoc.Click += new System.EventHandler(this.tbAddLoc_Click);
            // 
            // tbAddLocGroup
            // 
            this.tbAddLocGroup.Name = "tbAddLocGroup";
            resources.ApplyResources(this.tbAddLocGroup, "tbAddLocGroup");
            this.tbAddLocGroup.Click += new System.EventHandler(this.tbAddLocGroup_Click);
            // 
            // tbAddModule
            // 
            this.tbAddModule.Name = "tbAddModule";
            resources.ApplyResources(this.tbAddModule, "tbAddModule");
            this.tbAddModule.Click += new System.EventHandler(this.tbAddModule_Click);
            // 
            // tbAddModuleConnection
            // 
            this.tbAddModuleConnection.Name = "tbAddModuleConnection";
            resources.ApplyResources(this.tbAddModuleConnection, "tbAddModuleConnection");
            this.tbAddModuleConnection.Click += new System.EventHandler(this.tbAddModuleConnection_Click);
            // 
            // tbAddCommandStation
            // 
            this.tbAddCommandStation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAddBinkyNet,
            this.tbAddLocoBuffer,
            this.tbAddDccOverRs232,
            this.tbAddEcos,
            this.tbbAddP50x});
            this.tbAddCommandStation.Name = "tbAddCommandStation";
            resources.ApplyResources(this.tbAddCommandStation, "tbAddCommandStation");
            // 
            // tbAddLocoBuffer
            // 
            this.tbAddLocoBuffer.Name = "tbAddLocoBuffer";
            resources.ApplyResources(this.tbAddLocoBuffer, "tbAddLocoBuffer");
            this.tbAddLocoBuffer.Click += new System.EventHandler(this.tbAddLocoBuffer_Click);
            // 
            // tbAddDccOverRs232
            // 
            this.tbAddDccOverRs232.Name = "tbAddDccOverRs232";
            resources.ApplyResources(this.tbAddDccOverRs232, "tbAddDccOverRs232");
            this.tbAddDccOverRs232.Click += new System.EventHandler(this.tbAddDccOverRs232_Click);
            // 
            // tbAddEcos
            // 
            this.tbAddEcos.Name = "tbAddEcos";
            resources.ApplyResources(this.tbAddEcos, "tbAddEcos");
            this.tbAddEcos.Click += new System.EventHandler(this.tbAddEcos_Click);
            // 
            // tbAddBinkyNet
            // 
            this.tbAddBinkyNet.Name = "tbAddBinkyNet";
            resources.ApplyResources(this.tbAddBinkyNet, "tbAddBinkyNet");
            this.tbAddBinkyNet.Click += new System.EventHandler(this.tbAddBinkyNet_Click);
            // 
            // tbbAddP50x
            // 
            this.tbbAddP50x.Name = "tbbAddP50x";
            resources.ApplyResources(this.tbbAddP50x, "tbbAddP50x");
            this.tbbAddP50x.Click += new System.EventHandler(this.tbbAddP50x_Click);
            // 
            // tbEdit
            // 
            this.tbEdit.Image = global::BinkyRailways.Properties.Resources.edit_22;
            resources.ApplyResources(this.tbEdit, "tbEdit");
            this.tbEdit.Name = "tbEdit";
            this.tbEdit.Click += new System.EventHandler(this.tbEdit_Click);
            // 
            // tbImport
            // 
            this.tbImport.Image = global::BinkyRailways.Properties.Resources.fileimport_22;
            resources.ApplyResources(this.tbImport, "tbImport");
            this.tbImport.Name = "tbImport";
            this.tbImport.Click += new System.EventHandler(this.tbImport_Click);
            // 
            // tbArchive
            // 
            this.tbArchive.Image = global::BinkyRailways.Properties.Resources.ark_addfile_22;
            resources.ApplyResources(this.tbArchive, "tbArchive");
            this.tbArchive.Name = "tbArchive";
            this.tbArchive.Click += new System.EventHandler(this.tbArchive_Click);
            // 
            // tbActivate
            // 
            this.tbActivate.Image = global::BinkyRailways.Properties.Resources.archive_extract_22;
            resources.ApplyResources(this.tbActivate, "tbActivate");
            this.tbActivate.Name = "tbActivate";
            this.tbActivate.Click += new System.EventHandler(this.tbActivate_Click);
            // 
            // tbRemove
            // 
            this.tbRemove.Image = global::BinkyRailways.Properties.Resources.remove_22;
            resources.ApplyResources(this.tbRemove, "tbRemove");
            this.tbRemove.Name = "tbRemove";
            this.tbRemove.Click += new System.EventHandler(this.tbRemove_Click);
            // 
            // tbRun
            // 
            this.tbRun.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbRun.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbRunVirtual});
            this.tbRun.Image = global::BinkyRailways.Properties.Resources.smiley_22;
            resources.ApplyResources(this.tbRun, "tbRun");
            this.tbRun.Name = "tbRun";
            this.tbRun.ButtonClick += new System.EventHandler(this.tbRun_Click);
            // 
            // tbRunVirtual
            // 
            this.tbRunVirtual.Name = "tbRunVirtual";
            resources.ApplyResources(this.tbRunVirtual, "tbRunVirtual");
            this.tbRunVirtual.Click += new System.EventHandler(this.tbRunVirtual_Click);
            // 
            // validationResultsControl
            // 
            resources.ApplyResources(this.validationResultsControl, "validationResultsControl");
            this.validationResultsControl.Name = "validationResultsControl";
            this.validationResultsControl.ResultActivated += new System.EventHandler<BinkyRailways.Core.Util.PropertyEventArgs<BinkyRailways.Core.Model.ValidationResult>>(this.OnValidationResultActivated);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.OnContextMenuStripOpening);
            // 
            // EditRailwayControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "EditRailwayControl";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.innerSplitContainer.Panel1.ResumeLayout(false);
            this.innerSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.innerSplitContainer)).EndInit();
            this.innerSplitContainer.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.SplitContainer splitContainer;
        private SettingsPropertyGrid propertyGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem tbAdd;
        private System.Windows.Forms.ToolStripMenuItem tbAddLoc;
        private System.Windows.Forms.ToolStripMenuItem tbAddModule;
        private System.Windows.Forms.ToolStripMenuItem tbAddCommandStation;
        private System.Windows.Forms.ToolStripButton tbEdit;
        private System.Windows.Forms.ToolStripButton tbArchive;
        private System.Windows.Forms.ToolStripButton tbRemove;
        private System.Windows.Forms.ToolStripMenuItem tbAddLocoBuffer;
        private System.Windows.Forms.SplitContainer innerSplitContainer;
        private TreeViewX tvItems;
        private RailwayViewEditorControl viewEditor;
        private System.Windows.Forms.ToolStripSplitButton tbRun;
        private System.Windows.Forms.ToolStripMenuItem tbRunVirtual;
        private System.Windows.Forms.ToolStripButton tbActivate;
        private ValidationResultsControl validationResultsControl;
        private System.Windows.Forms.ToolStripMenuItem tbAddModuleConnection;
        private System.Windows.Forms.ToolStripMenuItem tbAddLocGroup;
        private ToolStripButton tbImport;
        private ToolStripMenuItem tbAddDccOverRs232;
        private ToolStripMenuItem tbAddEcos;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem tbAddBinkyNet;
        private ToolStripMenuItem tbbAddP50x;

    }
}
