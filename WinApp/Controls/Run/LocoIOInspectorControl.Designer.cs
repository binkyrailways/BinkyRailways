namespace BinkyRailways.WinApp.Controls.Run
{
    partial class LocoIOInspectorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocoIOInspectorControl));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.cmdRead = new System.Windows.Forms.Button();
            this.lbDevices = new System.Windows.Forms.Label();
            this.lbConfig = new System.Windows.Forms.Label();
            this.lvDevices = new System.Windows.Forms.ListView();
            this.chAddress = new System.Windows.Forms.ColumnHeader();
            this.chVersion = new System.Windows.Forms.ColumnHeader();
            this.lvPorts = new System.Windows.Forms.ListView();
            this.chPin = new System.Windows.Forms.ColumnHeader();
            this.chPortAddress = new System.Windows.Forms.ColumnHeader();
            this.chConfiguration = new System.Windows.Forms.ColumnHeader();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.cmdRead, 1, 1);
            this.tlpMain.Controls.Add(this.lbDevices, 0, 0);
            this.tlpMain.Controls.Add(this.lbConfig, 2, 0);
            this.tlpMain.Controls.Add(this.lvDevices, 0, 1);
            this.tlpMain.Controls.Add(this.lvPorts, 2, 1);
            this.tlpMain.Controls.Add(this.cmdRefresh, 1, 2);
            this.tlpMain.Name = "tlpMain";
            // 
            // cmdRead
            // 
            resources.ApplyResources(this.cmdRead, "cmdRead");
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // lbDevices
            // 
            resources.ApplyResources(this.lbDevices, "lbDevices");
            this.lbDevices.Name = "lbDevices";
            // 
            // lbConfig
            // 
            resources.ApplyResources(this.lbConfig, "lbConfig");
            this.lbConfig.Name = "lbConfig";
            // 
            // lvDevices
            // 
            this.lvDevices.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            resources.ApplyResources(this.lvDevices, "lvDevices");
            this.lvDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAddress,
            this.chVersion});
            this.lvDevices.FullRowSelect = true;
            this.lvDevices.MultiSelect = false;
            this.lvDevices.Name = "lvDevices";
            this.tlpMain.SetRowSpan(this.lvDevices, 2);
            this.lvDevices.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvDevices.UseCompatibleStateImageBehavior = false;
            this.lvDevices.View = System.Windows.Forms.View.Details;
            this.lvDevices.ItemActivate += new System.EventHandler(this.cmdRead_Click);
            this.lvDevices.SelectedIndexChanged += new System.EventHandler(this.lvDevices_SelectedIndexChanged);
            // 
            // chAddress
            // 
            resources.ApplyResources(this.chAddress, "chAddress");
            // 
            // chVersion
            // 
            resources.ApplyResources(this.chVersion, "chVersion");
            // 
            // lvPorts
            // 
            this.lvPorts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPin,
            this.chPortAddress,
            this.chConfiguration});
            resources.ApplyResources(this.lvPorts, "lvPorts");
            this.lvPorts.Name = "lvPorts";
            this.tlpMain.SetRowSpan(this.lvPorts, 2);
            this.lvPorts.UseCompatibleStateImageBehavior = false;
            this.lvPorts.View = System.Windows.Forms.View.Details;
            // 
            // chPin
            // 
            resources.ApplyResources(this.chPin, "chPin");
            // 
            // chPortAddress
            // 
            resources.ApplyResources(this.chPortAddress, "chPortAddress");
            // 
            // chConfiguration
            // 
            resources.ApplyResources(this.chConfiguration, "chConfiguration");
            // 
            // cmdRefresh
            // 
            resources.ApplyResources(this.cmdRefresh, "cmdRefresh");
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // LocoIOInspectorControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "LocoIOInspectorControl";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ListView lvDevices;
        private System.Windows.Forms.Label lbDevices;
        private System.Windows.Forms.Label lbConfig;
        private System.Windows.Forms.ColumnHeader chAddress;
        private System.Windows.Forms.ColumnHeader chVersion;
        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.ListView lvPorts;
        private System.Windows.Forms.ColumnHeader chPin;
        private System.Windows.Forms.ColumnHeader chConfiguration;
        private System.Windows.Forms.ColumnHeader chPortAddress;
        private System.Windows.Forms.Button cmdRefresh;
    }
}
