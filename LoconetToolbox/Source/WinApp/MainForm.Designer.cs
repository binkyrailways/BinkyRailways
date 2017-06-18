namespace LocoNetToolBox.WinApp
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.locoBufferView1 = new LocoNetToolBox.WinApp.Controls.LocoBufferView();
            this.commandControl1 = new LocoNetToolBox.WinApp.Controls.CommandControl();
            this.locoIOList1 = new LocoNetToolBox.WinApp.Controls.LocoIOList();
            this.locoNetMonitor = new LocoNetToolBox.WinApp.Controls.LocoNetMonitor();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lbVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.locoBufferView1, 0, 0);
            this.tlpMain.Controls.Add(this.commandControl1, 1, 0);
            this.tlpMain.Controls.Add(this.locoIOList1, 2, 0);
            this.tlpMain.Controls.Add(this.locoNetMonitor, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 24);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(897, 465);
            this.tlpMain.TabIndex = 0;
            // 
            // locoBufferView1
            // 
            this.locoBufferView1.AutoSize = true;
            this.locoBufferView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.locoBufferView1.Location = new System.Drawing.Point(3, 3);
            this.locoBufferView1.Name = "locoBufferView1";
            this.locoBufferView1.Size = new System.Drawing.Size(367, 141);
            this.locoBufferView1.TabIndex = 0;
            this.locoBufferView1.LocoBufferChanged += new System.EventHandler(this.LocoBufferView1LocoBufferChanged);
            // 
            // commandControl1
            // 
            this.commandControl1.AutoSize = true;
            this.commandControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.commandControl1.Location = new System.Drawing.Point(376, 3);
            this.commandControl1.Name = "commandControl1";
            this.commandControl1.Size = new System.Drawing.Size(144, 172);
            this.commandControl1.TabIndex = 1;
            // 
            // locoIOList1
            // 
            this.locoIOList1.Dock = System.Windows.Forms.DockStyle.Top;
            this.locoIOList1.Location = new System.Drawing.Point(526, 3);
            this.locoIOList1.Name = "locoIOList1";
            this.locoIOList1.Size = new System.Drawing.Size(368, 331);
            this.locoIOList1.TabIndex = 2;
            // 
            // locoNetMonitor
            // 
            this.tlpMain.SetColumnSpan(this.locoNetMonitor, 3);
            this.locoNetMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.locoNetMonitor.Location = new System.Drawing.Point(3, 340);
            this.locoNetMonitor.Name = "locoNetMonitor";
            this.locoNetMonitor.Size = new System.Drawing.Size(891, 122);
            this.locoNetMonitor.TabIndex = 3;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbVersion});
            this.statusStrip.Location = new System.Drawing.Point(0, 489);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(897, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // lbVersion
            // 
            this.lbVersion.IsLink = true;
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(57, 17);
            this.lbVersion.Text = "Version: ?";
            this.lbVersion.Click += new System.EventHandler(this.lbVersion_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(897, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpen,
            this.miSave,
            this.miSaveAs});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(37, 20);
            this.miFile.Text = "&File";
            // 
            // miOpen
            // 
            this.miOpen.Name = "miOpen";
            this.miOpen.Size = new System.Drawing.Size(114, 22);
            this.miOpen.Text = "&Open";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // miSave
            // 
            this.miSave.Name = "miSave";
            this.miSave.Size = new System.Drawing.Size(114, 22);
            this.miSave.Text = "&Save";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miSaveAs
            // 
            this.miSaveAs.Name = "miSaveAs";
            this.miSaveAs.Size = new System.Drawing.Size(114, 22);
            this.miSaveAs.Text = "Save &As";
            this.miSaveAs.Click += new System.EventHandler(this.miSaveAs_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 511);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "MGV LocoNet ToolBox";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private LocoNetToolBox.WinApp.Controls.LocoBufferView locoBufferView1;
        private LocoNetToolBox.WinApp.Controls.CommandControl commandControl1;
        private LocoNetToolBox.WinApp.Controls.LocoIOList locoIOList1;
        private LocoNetToolBox.WinApp.Controls.LocoNetMonitor locoNetMonitor;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lbVersion;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.ToolStripMenuItem miSaveAs;
    }
}

