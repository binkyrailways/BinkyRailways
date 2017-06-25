using System;
using System.Windows.Forms;
using BinkyRailways.Core.State;
using BinkyRailways.Core.Util;

namespace BinkyRailways.WinApp.Controls.Run
{
    partial class RunRailwayControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunRailwayControl));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbCloseAndEdit = new System.Windows.Forms.ToolStripSplitButton();
            this.tbShowQuickEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.tbStopAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tbShowDescriptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tbOptionsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbLearn = new System.Windows.Forms.ToolStripMenuItem();
            this.tbTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tbShowLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tbToolsSep = new System.Windows.Forms.ToolStripSeparator();
            this.tbRouteInspector = new System.Windows.Forms.ToolStripMenuItem();
            this.tbLocoIOInspector = new System.Windows.Forms.ToolStripMenuItem();
            this.tbQuickEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.tbbVirtualMode = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbVirtualModeAutoRun = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.locsControl = new BinkyRailways.WinApp.Controls.Run.LocsControl();
            this.viewControl = new BinkyRailways.WinApp.Controls.Run.RailwayViewControl();
            this.statusStrip = new RailwayStatusStrip();
            this.tlpMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.toolStrip1, 0, 0);
            this.tlpMain.Controls.Add(this.splitContainer, 0, 1);
            this.tlpMain.Controls.Add(this.statusStrip, 0, 2);
            this.tlpMain.Name = "tlpMain";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbCloseAndEdit,
            this.tbStopAll,
            this.toolStripSeparator2,
            this.tbOptions,
            this.tbTools,
            this.tbbVirtualMode});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tbCloseAndEdit
            // 
            this.tbCloseAndEdit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbCloseAndEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbShowQuickEditor});
            this.tbCloseAndEdit.Image = global::BinkyRailways.Properties.Resources.edit_22;
            resources.ApplyResources(this.tbCloseAndEdit, "tbCloseAndEdit");
            this.tbCloseAndEdit.Name = "tbCloseAndEdit";
            this.tbCloseAndEdit.ButtonClick += new System.EventHandler(this.OnCloseAndEditClick);
            // 
            // tbShowQuickEditor
            // 
            this.tbShowQuickEditor.Name = "tbShowQuickEditor";
            resources.ApplyResources(this.tbShowQuickEditor, "tbShowQuickEditor");
            this.tbShowQuickEditor.Click += new System.EventHandler(this.OnQuickEditorClick);
            // 
            // tbStopAll
            // 
            this.tbStopAll.Image = global::BinkyRailways.Properties.Resources.stop_22;
            resources.ApplyResources(this.tbStopAll, "tbStopAll");
            this.tbStopAll.Name = "tbStopAll";
            this.tbStopAll.Click += new System.EventHandler(this.OnStopAllClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tbOptions
            // 
            this.tbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbShowDescriptions,
            this.tbOptionsSep1,
            this.tbLearn});
            resources.ApplyResources(this.tbOptions, "tbOptions");
            this.tbOptions.Name = "tbOptions";
            // 
            // tbShowDescriptions
            // 
            this.tbShowDescriptions.CheckOnClick = true;
            this.tbShowDescriptions.Name = "tbShowDescriptions";
            resources.ApplyResources(this.tbShowDescriptions, "tbShowDescriptions");
            this.tbShowDescriptions.CheckedChanged += new System.EventHandler(this.OnShowDescriptionsCheckedChanged);
            // 
            // tbOptionsSep1
            // 
            this.tbOptionsSep1.Name = "tbOptionsSep1";
            resources.ApplyResources(this.tbOptionsSep1, "tbOptionsSep1");
            // 
            // tbLearn
            // 
            this.tbLearn.CheckOnClick = true;
            this.tbLearn.Image = global::BinkyRailways.Properties.Resources.system_search_22;
            resources.ApplyResources(this.tbLearn, "tbLearn");
            this.tbLearn.Name = "tbLearn";
            this.tbLearn.CheckedChanged += new System.EventHandler(this.OnLearnCheckedChanged);
            // 
            // tbTools
            // 
            this.tbTools.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbShowLog,
            this.tbToolsSep,
            this.tbRouteInspector,
            this.tbLocoIOInspector,
            this.tbQuickEditor});
            resources.ApplyResources(this.tbTools, "tbTools");
            this.tbTools.Name = "tbTools";
            // 
            // tbShowLog
            // 
            this.tbShowLog.Name = "tbShowLog";
            resources.ApplyResources(this.tbShowLog, "tbShowLog");
            this.tbShowLog.Click += new System.EventHandler(this.OnShowLogClick);
            // 
            // tbToolsSep
            // 
            this.tbToolsSep.Name = "tbToolsSep";
            resources.ApplyResources(this.tbToolsSep, "tbToolsSep");
            // 
            // tbRouteInspector
            // 
            this.tbRouteInspector.Name = "tbRouteInspector";
            resources.ApplyResources(this.tbRouteInspector, "tbRouteInspector");
            this.tbRouteInspector.Click += new System.EventHandler(this.tbRouteInspector_Click);
            // 
            // tbLocoIOInspector
            // 
            this.tbLocoIOInspector.Name = "tbLocoIOInspector";
            resources.ApplyResources(this.tbLocoIOInspector, "tbLocoIOInspector");
            this.tbLocoIOInspector.Click += new System.EventHandler(this.OnLocoIoInspectorClick);
            // 
            // tbQuickEditor
            // 
            this.tbQuickEditor.Name = "tbQuickEditor";
            resources.ApplyResources(this.tbQuickEditor, "tbQuickEditor");
            this.tbQuickEditor.Click += new System.EventHandler(this.OnQuickEditorClick);
            // 
            // tbbVirtualMode
            // 
            this.tbbVirtualMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbbVirtualMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbVirtualModeAutoRun});
            resources.ApplyResources(this.tbbVirtualMode, "tbbVirtualMode");
            this.tbbVirtualMode.Name = "tbbVirtualMode";
            // 
            // tbVirtualModeAutoRun
            // 
            this.tbVirtualModeAutoRun.CheckOnClick = true;
            this.tbVirtualModeAutoRun.Name = "tbVirtualModeAutoRun";
            resources.ApplyResources(this.tbVirtualModeAutoRun, "tbVirtualModeAutoRun");
            this.tbVirtualModeAutoRun.CheckStateChanged += new System.EventHandler(this.OnVirtualModeAutoRunCheckStateChanged);
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.locsControl);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.viewControl);
            // 
            // locsControl
            // 
            resources.ApplyResources(this.locsControl, "locsControl");
            this.locsControl.Name = "locsControl";
            this.locsControl.ShowLocProperties += new System.EventHandler<BinkyRailways.Core.Util.ObjectEventArgs<BinkyRailways.Core.State.ILocState>>(this.OnShowLocProperties);
            // 
            // viewControl
            // 
            resources.ApplyResources(this.viewControl, "viewControl");
            this.viewControl.Name = "viewControl";
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // RunRailwayControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "RunRailwayControl";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer;
        private LocsControl locsControl;
        private RailwayViewControl viewControl;
        private System.Windows.Forms.ToolStripMenuItem tbLearn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tbStopAll;
        private System.Windows.Forms.ToolStripMenuItem tbOptions;
        private ToolStripSeparator tbOptionsSep1;
        private ToolStripMenuItem tbShowDescriptions;
        private ToolStripSplitButton tbCloseAndEdit;
        private ToolStripMenuItem tbShowQuickEditor;
        private ToolStripMenuItem tbTools;
        private ToolStripMenuItem tbLocoIOInspector;
        private ToolStripMenuItem tbShowLog;
        private ToolStripSeparator tbToolsSep;
        private ToolStripMenuItem tbQuickEditor;
        private ToolStripDropDownButton tbbVirtualMode;
        private ToolStripMenuItem tbVirtualModeAutoRun;
        private ToolStripMenuItem tbRouteInspector;
        private RailwayStatusStrip statusStrip;
    }
}
