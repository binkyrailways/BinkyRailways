using BinkyRailways.WinApp.Controls.Edit;
using BinkyRailways.WinApp.Controls.Run;

namespace BinkyRailways.WinApp.Forms
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miNew = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.miRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miReports = new System.Windows.Forms.ToolStripMenuItem();
            this.miLocsReport = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.miLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.editControl = new BinkyRailways.WinApp.Controls.Edit.EditRailwayControl();
            this.runControl = new BinkyRailways.WinApp.Controls.Run.RunRailwayControl();
            this.updateCheckWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miReports,
            this.miHelp});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNew,
            this.miOpen,
            this.miSave,
            this.miSaveAs,
            this.miSep1,
            this.miRecentFiles,
            this.miSep2,
            this.miExit});
            this.miFile.Name = "miFile";
            resources.ApplyResources(this.miFile, "miFile");
            // 
            // miNew
            // 
            this.miNew.Image = global::BinkyRailways.Properties.Resources.filenew_22;
            resources.ApplyResources(this.miNew, "miNew");
            this.miNew.Name = "miNew";
            this.miNew.Click += new System.EventHandler(this.miNew_Click);
            // 
            // miOpen
            // 
            this.miOpen.Image = global::BinkyRailways.Properties.Resources.fileopen_22;
            resources.ApplyResources(this.miOpen, "miOpen");
            this.miOpen.Name = "miOpen";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // miSave
            // 
            this.miSave.Image = global::BinkyRailways.Properties.Resources.filesave_22;
            resources.ApplyResources(this.miSave, "miSave");
            this.miSave.Name = "miSave";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miSaveAs
            // 
            this.miSaveAs.Name = "miSaveAs";
            resources.ApplyResources(this.miSaveAs, "miSaveAs");
            this.miSaveAs.Click += new System.EventHandler(this.miSaveAs_Click);
            // 
            // miSep1
            // 
            this.miSep1.Name = "miSep1";
            resources.ApplyResources(this.miSep1, "miSep1");
            // 
            // miRecentFiles
            // 
            this.miRecentFiles.Name = "miRecentFiles";
            resources.ApplyResources(this.miRecentFiles, "miRecentFiles");
            // 
            // miSep2
            // 
            this.miSep2.Name = "miSep2";
            resources.ApplyResources(this.miSep2, "miSep2");
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            resources.ApplyResources(this.miExit, "miExit");
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miReports
            // 
            this.miReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLocsReport});
            this.miReports.Name = "miReports";
            resources.ApplyResources(this.miReports, "miReports");
            // 
            // miLocsReport
            // 
            this.miLocsReport.Name = "miLocsReport";
            resources.ApplyResources(this.miLocsReport, "miLocsReport");
            this.miLocsReport.Click += new System.EventHandler(this.miLocsReport_Click);
            // 
            // miHelp
            // 
            this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbout,
            this.miLanguage});
            this.miHelp.Name = "miHelp";
            resources.ApplyResources(this.miHelp, "miHelp");
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            resources.ApplyResources(this.miAbout, "miAbout");
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // miLanguage
            // 
            this.miLanguage.Name = "miLanguage";
            resources.ApplyResources(this.miLanguage, "miLanguage");
            // 
            // editControl
            // 
            resources.ApplyResources(this.editControl, "editControl");
            this.editControl.Name = "editControl";
            // 
            // runControl
            // 
            resources.ApplyResources(this.runControl, "runControl");
            this.runControl.Name = "runControl";
            // 
            // updateCheckWorker
            // 
            this.updateCheckWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnUpdateCheckDoWork);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.runControl);
            this.Controls.Add(this.editControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.ToolStripSeparator miSep2;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miNew;
        private System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.ToolStripMenuItem miRecentFiles;
        private System.Windows.Forms.ToolStripSeparator miSep1;
        private EditRailwayControl editControl;
        private RunRailwayControl runControl;
        private System.ComponentModel.BackgroundWorker updateCheckWorker;
        private System.Windows.Forms.ToolStripMenuItem miHelp;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.ToolStripMenuItem miLanguage;
        private System.Windows.Forms.ToolStripMenuItem miSaveAs;
        private System.Windows.Forms.ToolStripMenuItem miReports;
        private System.Windows.Forms.ToolStripMenuItem miLocsReport;
    }
}