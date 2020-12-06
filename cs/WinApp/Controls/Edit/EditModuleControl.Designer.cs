using BinkyRailways.WinApp.Controls.Edit.Settings;

namespace BinkyRailways.WinApp.Controls.Edit
{
    partial class EditModuleControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditModuleControl));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.innerSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tvItems = new BinkyRailways.WinApp.Controls.Edit.EntityTreeView();
            this.viewEditor = new BinkyRailways.WinApp.Controls.Edit.ModuleViewEditorControl();
            this.propertyGrid = new BinkyRailways.WinApp.Controls.Edit.Settings.SettingsPropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbAdd = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbAddBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddBlockGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddEdge = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddJunction = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddSwitch = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddTurnTable = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddPassiveJunction = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddRoute = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddSensor = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddBinarySensor = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddSignal = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddBlockSignal = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAddBinaryOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAdd4stageClockOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRemove = new System.Windows.Forms.ToolStripButton();
            this.tbRename = new System.Windows.Forms.ToolStripButton();
            this.validationResultsControl = new BinkyRailways.WinApp.Controls.Edit.ValidationResultsControl();
            this.contextMenus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miAddReverseRoute = new System.Windows.Forms.ToolStripMenuItem();
            this.miSortByFromBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.miSortByToBlock = new System.Windows.Forms.ToolStripMenuItem();
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
            this.contextMenus.SuspendLayout();
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
            this.tvItems.Name = "tvItems";
            this.tvItems.ExpandStateChanged += new System.EventHandler(this.OnAfterCollapseOrExpand);
            // 
            // viewEditor
            // 
            resources.ApplyResources(this.viewEditor, "viewEditor");
            this.viewEditor.Name = "viewEditor";
            // 
            // propertyGrid
            // 
            resources.ApplyResources(this.propertyGrid, "propertyGrid");
            this.propertyGrid.InRunningState = false;
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAdd,
            this.tbRemove,
            this.tbRename});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tbAdd
            // 
            this.tbAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAddBlock,
            this.tbAddBlockGroup,
            this.tbAddEdge,
            this.tbAddJunction,
            this.tbAddRoute,
            this.tbAddSensor,
            this.tbAddSignal,
            this.tbAddOutput});
            this.tbAdd.Image = global::BinkyRailways.Properties.Resources.add_22;
            resources.ApplyResources(this.tbAdd, "tbAdd");
            this.tbAdd.Name = "tbAdd";
            // 
            // tbAddBlock
            // 
            this.tbAddBlock.Name = "tbAddBlock";
            resources.ApplyResources(this.tbAddBlock, "tbAddBlock");
            this.tbAddBlock.Click += new System.EventHandler(this.tbAddBlock_Click);
            // 
            // tbAddBlockGroup
            // 
            this.tbAddBlockGroup.Name = "tbAddBlockGroup";
            resources.ApplyResources(this.tbAddBlockGroup, "tbAddBlockGroup");
            this.tbAddBlockGroup.Click += new System.EventHandler(this.tbAddBlockGroup_Click);
            // 
            // tbAddEdge
            // 
            this.tbAddEdge.Name = "tbAddEdge";
            resources.ApplyResources(this.tbAddEdge, "tbAddEdge");
            this.tbAddEdge.Click += new System.EventHandler(this.tbAddEdge_Click);
            // 
            // tbAddJunction
            // 
            this.tbAddJunction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAddSwitch,
            this.tbAddTurnTable,
            this.miAddPassiveJunction});
            this.tbAddJunction.Name = "tbAddJunction";
            resources.ApplyResources(this.tbAddJunction, "tbAddJunction");
            // 
            // tbAddSwitch
            // 
            this.tbAddSwitch.Name = "tbAddSwitch";
            resources.ApplyResources(this.tbAddSwitch, "tbAddSwitch");
            this.tbAddSwitch.Click += new System.EventHandler(this.tbAddSwitch_Click);
            // 
            // tbAddTurnTable
            // 
            this.tbAddTurnTable.Name = "tbAddTurnTable";
            resources.ApplyResources(this.tbAddTurnTable, "tbAddTurnTable");
            this.tbAddTurnTable.Click += new System.EventHandler(this.tbAddTurnTable_Click);
            // 
            // miAddPassiveJunction
            // 
            this.miAddPassiveJunction.Name = "miAddPassiveJunction";
            resources.ApplyResources(this.miAddPassiveJunction, "miAddPassiveJunction");
            this.miAddPassiveJunction.Click += new System.EventHandler(this.miAddPassiveJunction_Click);
            // 
            // tbAddRoute
            // 
            this.tbAddRoute.Name = "tbAddRoute";
            resources.ApplyResources(this.tbAddRoute, "tbAddRoute");
            this.tbAddRoute.Click += new System.EventHandler(this.tbAddRoute_Click);
            // 
            // tbAddSensor
            // 
            this.tbAddSensor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAddBinarySensor});
            this.tbAddSensor.Name = "tbAddSensor";
            resources.ApplyResources(this.tbAddSensor, "tbAddSensor");
            // 
            // tbAddBinarySensor
            // 
            this.tbAddBinarySensor.Name = "tbAddBinarySensor";
            resources.ApplyResources(this.tbAddBinarySensor, "tbAddBinarySensor");
            this.tbAddBinarySensor.Click += new System.EventHandler(this.tbAddBinarySensor_Click);
            // 
            // tbAddSignal
            // 
            this.tbAddSignal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAddBlockSignal});
            this.tbAddSignal.Name = "tbAddSignal";
            resources.ApplyResources(this.tbAddSignal, "tbAddSignal");
            // 
            // tbAddBlockSignal
            // 
            this.tbAddBlockSignal.Name = "tbAddBlockSignal";
            resources.ApplyResources(this.tbAddBlockSignal, "tbAddBlockSignal");
            this.tbAddBlockSignal.Click += new System.EventHandler(this.tbAddBlockSignal_Click);
            // 
            // tbAddOutput
            // 
            this.tbAddOutput.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAddBinaryOutput,
            this.tbAdd4stageClockOutput});
            this.tbAddOutput.Name = "tbAddOutput";
            resources.ApplyResources(this.tbAddOutput, "tbAddOutput");
            // 
            // tbAddBinaryOutput
            // 
            this.tbAddBinaryOutput.Name = "tbAddBinaryOutput";
            resources.ApplyResources(this.tbAddBinaryOutput, "tbAddBinaryOutput");
            this.tbAddBinaryOutput.Click += new System.EventHandler(this.tbAddBinaryOutput_Click);
            // 
            // tbAdd4stageClockOutput
            // 
            this.tbAdd4stageClockOutput.Name = "tbAdd4stageClockOutput";
            resources.ApplyResources(this.tbAdd4stageClockOutput, "tbAdd4stageClockOutput");
            this.tbAdd4stageClockOutput.Click += new System.EventHandler(this.tbAdd4stageClockOutput_Click);
            // 
            // tbRemove
            // 
            this.tbRemove.Image = global::BinkyRailways.Properties.Resources.remove_22;
            resources.ApplyResources(this.tbRemove, "tbRemove");
            this.tbRemove.Name = "tbRemove";
            this.tbRemove.Click += new System.EventHandler(this.tbRemove_Click);
            // 
            // tbRename
            // 
            this.tbRename.Image = global::BinkyRailways.Properties.Resources.edit_rename_22;
            resources.ApplyResources(this.tbRename, "tbRename");
            this.tbRename.Name = "tbRename";
            this.tbRename.Click += new System.EventHandler(this.tbRename_Click);
            // 
            // validationResultsControl
            // 
            resources.ApplyResources(this.validationResultsControl, "validationResultsControl");
            this.validationResultsControl.Name = "validationResultsControl";
            this.validationResultsControl.ResultActivated += new System.EventHandler<BinkyRailways.Core.Util.PropertyEventArgs<BinkyRailways.Core.Model.ValidationResult>>(this.validationResultsControl_ResultActivated);
            // 
            // contextMenus
            // 
            this.contextMenus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAddReverseRoute,
            this.miSortByFromBlock,
            this.miSortByToBlock});
            this.contextMenus.Name = "contextMenus";
            resources.ApplyResources(this.contextMenus, "contextMenus");
            this.contextMenus.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenus_Opening);
            // 
            // miAddReverseRoute
            // 
            this.miAddReverseRoute.Name = "miAddReverseRoute";
            resources.ApplyResources(this.miAddReverseRoute, "miAddReverseRoute");
            this.miAddReverseRoute.Click += new System.EventHandler(this.miAddReverseRoute_Click);
            // 
            // miSortByFromBlock
            // 
            this.miSortByFromBlock.Name = "miSortByFromBlock";
            resources.ApplyResources(this.miSortByFromBlock, "miSortByFromBlock");
            this.miSortByFromBlock.Click += new System.EventHandler(this.miSortByFromBlock_Click);
            // 
            // miSortByToBlock
            // 
            this.miSortByToBlock.Name = "miSortByToBlock";
            resources.ApplyResources(this.miSortByToBlock, "miSortByToBlock");
            this.miSortByToBlock.Click += new System.EventHandler(this.miSortByToBlock_Click);
            // 
            // EditModuleControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "EditModuleControl";
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
            this.contextMenus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.SplitContainer splitContainer;
        private SettingsPropertyGrid propertyGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton tbAdd;
        private System.Windows.Forms.ToolStripMenuItem tbAddBlock;
        private System.Windows.Forms.ToolStripMenuItem tbAddRoute;
        private System.Windows.Forms.ToolStripMenuItem tbAddEdge;
        private System.Windows.Forms.ToolStripMenuItem tbAddJunction;
        private System.Windows.Forms.ToolStripMenuItem tbAddSensor;
        private System.Windows.Forms.ToolStripMenuItem tbAddSignal;
        private System.Windows.Forms.ToolStripMenuItem tbAddSwitch;
        private System.Windows.Forms.ToolStripMenuItem tbAddBinarySensor;
        private System.Windows.Forms.ToolStripButton tbRemove;
        private System.Windows.Forms.SplitContainer innerSplitContainer;
        private EntityTreeView tvItems;
        private ModuleViewEditorControl viewEditor;
        private ValidationResultsControl validationResultsControl;
        private System.Windows.Forms.ToolStripButton tbRename;
        private System.Windows.Forms.ContextMenuStrip contextMenus;
        private System.Windows.Forms.ToolStripMenuItem miAddReverseRoute;
        private System.Windows.Forms.ToolStripMenuItem tbAddTurnTable;
        private System.Windows.Forms.ToolStripMenuItem tbAddBlockSignal;
        private System.Windows.Forms.ToolStripMenuItem tbAddOutput;
        private System.Windows.Forms.ToolStripMenuItem tbAddBinaryOutput;
        private System.Windows.Forms.ToolStripMenuItem miAddPassiveJunction;
        private System.Windows.Forms.ToolStripMenuItem miSortByFromBlock;
        private System.Windows.Forms.ToolStripMenuItem miSortByToBlock;
        private System.Windows.Forms.ToolStripMenuItem tbAdd4stageClockOutput;
        private System.Windows.Forms.ToolStripMenuItem tbAddBlockGroup;

    }
}
