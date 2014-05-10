namespace BinkyRailways.WinApp.Controls.Run
{
    partial class LocsControl
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
            if (disposing)
            {
                updateTimer.Enabled = false;
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocsControl));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbRemoveFromTrack = new System.Windows.Forms.ToolStripButton();
            this.lvLocs = new BinkyRailways.WinApp.Controls.Run.LocListView();
            this.locControlPanel = new BinkyRailways.WinApp.Controls.Run.LocControlPanel();
            this.unexpectedSensorControl = new BinkyRailways.WinApp.Controls.Run.UnexpectedSensorControl();
            this.railwayStateControlPanel = new BinkyRailways.WinApp.Controls.Run.RailwayStateControlPanel();
            this.locItemImages = new System.Windows.Forms.ImageList(this.components);
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.tlpMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.toolStrip1, 0, 1);
            this.tlpMain.Controls.Add(this.lvLocs, 0, 2);
            this.tlpMain.Controls.Add(this.locControlPanel, 0, 4);
            this.tlpMain.Controls.Add(this.unexpectedSensorControl, 0, 3);
            this.tlpMain.Controls.Add(this.railwayStateControlPanel, 0, 0);
            this.tlpMain.Name = "tlpMain";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbRemoveFromTrack});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tbRemoveFromTrack
            // 
            this.tbRemoveFromTrack.Image = global::BinkyRailways.Properties.Resources.undo_22;
            resources.ApplyResources(this.tbRemoveFromTrack, "tbRemoveFromTrack");
            this.tbRemoveFromTrack.Name = "tbRemoveFromTrack";
            this.tbRemoveFromTrack.Click += new System.EventHandler(this.OnRemoveFromTrackClick);
            // 
            // lvLocs
            // 
            resources.ApplyResources(this.lvLocs, "lvLocs");
            this.lvLocs.Name = "lvLocs";
            this.lvLocs.SelectionChanged += new System.EventHandler(this.OnLocsSelectedIndexChanged);
            this.lvLocs.RouteDurationExceeded += new System.EventHandler(this.OnRouteDurationExceeded);
            this.lvLocs.LocActualStateChanged += new System.EventHandler(this.OnLocActualStateChanged);
            // 
            // locControlPanel
            // 
            resources.ApplyResources(this.locControlPanel, "locControlPanel");
            this.locControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.locControlPanel.Name = "locControlPanel";
            // 
            // unexpectedSensorControl
            // 
            resources.ApplyResources(this.unexpectedSensorControl, "unexpectedSensorControl");
            this.unexpectedSensorControl.Name = "unexpectedSensorControl";
            // 
            // railwayStateControlPanel
            // 
            resources.ApplyResources(this.railwayStateControlPanel, "railwayStateControlPanel");
            this.railwayStateControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.railwayStateControlPanel.Name = "railwayStateControlPanel";
            // 
            // locItemImages
            // 
            this.locItemImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("locItemImages.ImageStream")));
            this.locItemImages.TransparentColor = System.Drawing.Color.Transparent;
            this.locItemImages.Images.SetKeyName(0, "loc-state-ok.png");
            this.locItemImages.Images.SetKeyName(1, "loc-state-error.png");
            this.locItemImages.Images.SetKeyName(2, "loc-state-unassigned.png");
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 1000;
            this.updateTimer.Tick += new System.EventHandler(this.OnUpdateTimerTick);
            // 
            // LocsControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "LocsControl";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private LocListView lvLocs;
        private LocControlPanel locControlPanel;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ToolStripButton tbRemoveFromTrack;
        private UnexpectedSensorControl unexpectedSensorControl;
        private RailwayStateControlPanel railwayStateControlPanel;
        private System.Windows.Forms.ImageList locItemImages;

    }
}
