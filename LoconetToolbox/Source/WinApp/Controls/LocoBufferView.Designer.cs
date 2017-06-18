namespace LocoNetToolBox.WinApp.Controls
{
    partial class LocoBufferView
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbHeader = new System.Windows.Forms.Label();
            this.locoBufferSettings = new LocoNetToolBox.WinApp.Controls.LocoBufferSettings();
            this.powerCommandControl1 = new LocoNetToolBox.WinApp.Controls.PowerCommandControl();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lbHeader, 0, 0);
            this.tlpMain.Controls.Add(this.locoBufferSettings, 0, 1);
            this.tlpMain.Controls.Add(this.powerCommandControl1, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(396, 106);
            this.tlpMain.TabIndex = 0;
            // 
            // lbHeader
            // 
            this.lbHeader.AutoSize = true;
            this.lbHeader.Location = new System.Drawing.Point(3, 0);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(59, 13);
            this.lbHeader.TabIndex = 0;
            this.lbHeader.Text = "LocoBuffer";
            // 
            // locoBufferSettings
            // 
            this.locoBufferSettings.AutoSize = true;
            this.locoBufferSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.locoBufferSettings.Location = new System.Drawing.Point(3, 16);
            this.locoBufferSettings.Name = "locoBufferSettings";
            this.locoBufferSettings.Size = new System.Drawing.Size(390, 46);
            this.locoBufferSettings.TabIndex = 1;
            this.locoBufferSettings.LocoBufferChanged += new System.EventHandler(this.LocoBufferSettingsLocoBufferChanged);
            // 
            // powerCommandControl1
            // 
            this.powerCommandControl1.AutoSize = true;
            this.powerCommandControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.powerCommandControl1.Location = new System.Drawing.Point(3, 68);
            this.powerCommandControl1.Name = "powerCommandControl1";
            this.powerCommandControl1.Size = new System.Drawing.Size(390, 35);
            this.powerCommandControl1.TabIndex = 2;
            // 
            // LocoBufferView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "LocoBufferView";
            this.Size = new System.Drawing.Size(396, 312);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbHeader;
        private LocoBufferSettings locoBufferSettings;
        private PowerCommandControl powerCommandControl1;
    }
}
