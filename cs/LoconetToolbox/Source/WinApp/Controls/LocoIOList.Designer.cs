namespace LocoNetToolBox.WinApp.Controls
{
    partial class LocoIOList
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
            this.lbModules = new System.Windows.Forms.ListView();
            this.chAddress = new System.Windows.Forms.ColumnHeader();
            this.chVersion = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.cmdConfigureMgv50 = new System.Windows.Forms.Button();
            this.cmdConfigMgv50Advanced = new System.Windows.Forms.Button();
            this.cmdChangeAddress = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbModules
            // 
            this.lbModules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAddress,
            this.chVersion});
            this.lbModules.ContextMenuStrip = this.contextMenuStrip1;
            this.lbModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbModules.FullRowSelect = true;
            this.lbModules.HideSelection = false;
            this.lbModules.Location = new System.Drawing.Point(3, 3);
            this.lbModules.MultiSelect = false;
            this.lbModules.Name = "lbModules";
            this.tlpMain.SetRowSpan(this.lbModules, 4);
            this.lbModules.Size = new System.Drawing.Size(314, 404);
            this.lbModules.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lbModules.TabIndex = 0;
            this.lbModules.UseCompatibleStateImageBehavior = false;
            this.lbModules.View = System.Windows.Forms.View.Details;
            this.lbModules.ItemActivate += new System.EventHandler(this.LbModulesItemActivate);
            this.lbModules.SelectedIndexChanged += new System.EventHandler(this.LbModulesSelectedIndexChanged);
            // 
            // chAddress
            // 
            this.chAddress.Text = "Address";
            // 
            // chVersion
            // 
            this.chVersion.Text = "Version";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxProgram});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 26);
            // 
            // ctxProgram
            // 
            this.ctxProgram.Name = "ctxProgram";
            this.ctxProgram.Size = new System.Drawing.Size(168, 22);
            this.ctxProgram.Text = "Configure MGV50";
            this.ctxProgram.Click += new System.EventHandler(this.CmdConfigureMgv50Click);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.Controls.Add(this.cmdChangeAddress, 0, 2);
            this.tlpMain.Controls.Add(this.lbModules, 0, 0);
            this.tlpMain.Controls.Add(this.cmdConfigureMgv50, 1, 0);
            this.tlpMain.Controls.Add(this.cmdConfigMgv50Advanced, 1, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(450, 410);
            this.tlpMain.TabIndex = 1;
            // 
            // cmdConfigureMgv50
            // 
            this.cmdConfigureMgv50.Location = new System.Drawing.Point(323, 3);
            this.cmdConfigureMgv50.Name = "cmdConfigureMgv50";
            this.cmdConfigureMgv50.Size = new System.Drawing.Size(124, 37);
            this.cmdConfigureMgv50.TabIndex = 1;
            this.cmdConfigureMgv50.Text = "&Configure MGV50";
            this.cmdConfigureMgv50.UseVisualStyleBackColor = true;
            this.cmdConfigureMgv50.Click += new System.EventHandler(this.CmdConfigureMgv50Click);
            // 
            // cmdConfigMgv50Advanced
            // 
            this.cmdConfigMgv50Advanced.Location = new System.Drawing.Point(323, 46);
            this.cmdConfigMgv50Advanced.Name = "cmdConfigMgv50Advanced";
            this.cmdConfigMgv50Advanced.Size = new System.Drawing.Size(124, 37);
            this.cmdConfigMgv50Advanced.TabIndex = 2;
            this.cmdConfigMgv50Advanced.Text = "&Advanced MGV50 configuration";
            this.cmdConfigMgv50Advanced.UseVisualStyleBackColor = true;
            this.cmdConfigMgv50Advanced.Click += new System.EventHandler(this.CmdConfigMgv50AdvancedClick);
            // 
            // cmdChangeAddress
            // 
            this.cmdChangeAddress.Location = new System.Drawing.Point(323, 89);
            this.cmdChangeAddress.Name = "cmdChangeAddress";
            this.cmdChangeAddress.Size = new System.Drawing.Size(124, 37);
            this.cmdChangeAddress.TabIndex = 3;
            this.cmdChangeAddress.Text = "Change MGV50 a&ddress";
            this.cmdChangeAddress.UseVisualStyleBackColor = true;
            this.cmdChangeAddress.Click += new System.EventHandler(this.cmdChangeAddress_Click);
            // 
            // LocoIOList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "LocoIOList";
            this.Size = new System.Drawing.Size(450, 410);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader chAddress;
        private System.Windows.Forms.ColumnHeader chVersion;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ctxProgram;
        private System.Windows.Forms.ListView lbModules;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Button cmdConfigureMgv50;
        private System.Windows.Forms.Button cmdConfigMgv50Advanced;
        private System.Windows.Forms.Button cmdChangeAddress;
    }
}
