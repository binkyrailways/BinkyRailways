namespace BinkyRailways.WinApp.Controls.Run
{
    partial class LocListView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocListView));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lvLocs = new ListView();
            this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chOwner = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAdvanced = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.locContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miLocProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.locItemImages = new System.Windows.Forms.ImageList(this.components);
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.tlpMain.SuspendLayout();
            this.locContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.lvLocs, 0, 2);
            this.tlpMain.Name = "tlpMain";
            // 
            // lvLocs
            // 
            this.lvLocs.CheckBoxes = true;
            this.lvLocs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDescription,
            this.chState,
            this.chSpeed,
            this.chOwner,
            this.chAdvanced});
            this.lvLocs.ContextMenuStrip = this.locContextMenu;
            resources.ApplyResources(this.lvLocs, "lvLocs");
            this.lvLocs.FullRowSelect = true;
            this.lvLocs.HideSelection = false;
            this.lvLocs.MultiSelect = false;
            this.lvLocs.Name = "lvLocs";
            this.lvLocs.SmallImageList = this.locItemImages;
            this.lvLocs.UseCompatibleStateImageBehavior = false;
            this.lvLocs.View = System.Windows.Forms.View.Details;
            this.lvLocs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.OnLocsItemCheck);
            this.lvLocs.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.OnLocsItemChecked);
            this.lvLocs.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.OnLocsItemDrag);
            this.lvLocs.SelectedIndexChanged += new System.EventHandler(this.OnLocsSelectedIndexChanged);
            // 
            // chDescription
            // 
            resources.ApplyResources(this.chDescription, "chDescription");
            // 
            // chState
            // 
            resources.ApplyResources(this.chState, "chState");
            // 
            // chSpeed
            // 
            resources.ApplyResources(this.chSpeed, "chSpeed");
            // 
            // chOwner
            // 
            resources.ApplyResources(this.chOwner, "chOwner");
            // 
            // chAdvanced
            // 
            resources.ApplyResources(this.chAdvanced, "chAdvanced");
            // 
            // locContextMenu
            // 
            this.locContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLocProperties});
            this.locContextMenu.Name = "locContextMenu";
            resources.ApplyResources(this.locContextMenu, "locContextMenu");
            this.locContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.OnLocContextMenuOpening);
            // 
            // miLocProperties
            // 
            this.miLocProperties.Name = "miLocProperties";
            resources.ApplyResources(this.miLocProperties, "miLocProperties");
            this.miLocProperties.Click += new System.EventHandler(this.OnLocPropertiesClick);
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
            // LocListView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "LocListView";
            this.tlpMain.ResumeLayout(false);
            this.locContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private ListView lvLocs;
        private System.Windows.Forms.ColumnHeader chDescription;
        private System.Windows.Forms.ColumnHeader chSpeed;
        private System.Windows.Forms.ColumnHeader chState;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ColumnHeader chAdvanced;
        private System.Windows.Forms.ImageList locItemImages;
        private System.Windows.Forms.ColumnHeader chOwner;
        private System.Windows.Forms.ContextMenuStrip locContextMenu;
        private System.Windows.Forms.ToolStripMenuItem miLocProperties;

    }
}
