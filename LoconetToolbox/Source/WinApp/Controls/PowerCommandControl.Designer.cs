namespace LocoNetToolBox.WinApp.Controls
{
    partial class PowerCommandControl
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
            this.cmdGpOn = new System.Windows.Forms.Button();
            this.cmdGpOff = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.cmdGpOn, 0, 0);
            this.tlpMain.Controls.Add(this.cmdGpOff, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(376, 35);
            this.tlpMain.TabIndex = 4;
            // 
            // cmdGpOn
            // 
            this.cmdGpOn.Location = new System.Drawing.Point(3, 3);
            this.cmdGpOn.Name = "cmdGpOn";
            this.cmdGpOn.Size = new System.Drawing.Size(117, 29);
            this.cmdGpOn.TabIndex = 8;
            this.cmdGpOn.Text = "Global Power On";
            this.cmdGpOn.UseVisualStyleBackColor = true;
            this.cmdGpOn.Click += new System.EventHandler(this.CmdGpOnClick);
            // 
            // cmdGpOff
            // 
            this.cmdGpOff.Location = new System.Drawing.Point(191, 3);
            this.cmdGpOff.Name = "cmdGpOff";
            this.cmdGpOff.Size = new System.Drawing.Size(117, 29);
            this.cmdGpOff.TabIndex = 9;
            this.cmdGpOff.Text = "Global Power Off";
            this.cmdGpOff.UseVisualStyleBackColor = true;
            this.cmdGpOff.Click += new System.EventHandler(this.CmdGpOffClick);
            // 
            // PowerCommandControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "PowerCommandControl";
            this.Size = new System.Drawing.Size(376, 359);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Button cmdGpOn;
        private System.Windows.Forms.Button cmdGpOff;
    }
}
