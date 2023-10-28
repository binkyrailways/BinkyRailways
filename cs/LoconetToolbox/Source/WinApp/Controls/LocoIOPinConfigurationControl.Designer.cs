namespace LocoNetToolBox.WinApp.Controls
{
    partial class LocoIOPinConfigurationControl
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
            this.modeControl = new LocoNetToolBox.WinApp.Controls.LocoIOPinModeControl();
            this.tbConfig = new System.Windows.Forms.TextBox();
            this.tbValue1 = new System.Windows.Forms.TextBox();
            this.tbValue2 = new System.Windows.Forms.TextBox();
            this.tbAddr = new System.Windows.Forms.NumericUpDown();
            this.cmdRead = new System.Windows.Forms.Button();
            this.cmdWrite = new System.Windows.Forms.Button();
            this.cmdNotUsed = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAddr)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 8;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.Controls.Add(this.modeControl, 0, 0);
            this.tlpMain.Controls.Add(this.tbConfig, 2, 0);
            this.tlpMain.Controls.Add(this.tbValue1, 3, 0);
            this.tlpMain.Controls.Add(this.tbValue2, 4, 0);
            this.tlpMain.Controls.Add(this.tbAddr, 1, 0);
            this.tlpMain.Controls.Add(this.cmdRead, 5, 0);
            this.tlpMain.Controls.Add(this.cmdWrite, 6, 0);
            this.tlpMain.Controls.Add(this.cmdNotUsed, 7, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(681, 27);
            this.tlpMain.TabIndex = 0;
            // 
            // modeControl
            // 
            this.modeControl.AutoSize = true;
            this.modeControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.modeControl.Location = new System.Drawing.Point(3, 3);
            this.modeControl.Mode = null;
            this.modeControl.Name = "modeControl";
            this.modeControl.Size = new System.Drawing.Size(371, 21);
            this.modeControl.TabIndex = 7;
            this.modeControl.Changed += new System.EventHandler(this.OnConfigChanged);
            // 
            // tbConfig
            // 
            this.tbConfig.Location = new System.Drawing.Point(444, 3);
            this.tbConfig.Name = "tbConfig";
            this.tbConfig.ReadOnly = true;
            this.tbConfig.Size = new System.Drawing.Size(26, 20);
            this.tbConfig.TabIndex = 9;
            // 
            // tbValue1
            // 
            this.tbValue1.Location = new System.Drawing.Point(476, 3);
            this.tbValue1.Name = "tbValue1";
            this.tbValue1.ReadOnly = true;
            this.tbValue1.Size = new System.Drawing.Size(26, 20);
            this.tbValue1.TabIndex = 10;
            // 
            // tbValue2
            // 
            this.tbValue2.Location = new System.Drawing.Point(508, 3);
            this.tbValue2.Name = "tbValue2";
            this.tbValue2.ReadOnly = true;
            this.tbValue2.Size = new System.Drawing.Size(27, 20);
            this.tbValue2.TabIndex = 11;
            // 
            // tbAddr
            // 
            this.tbAddr.Location = new System.Drawing.Point(380, 3);
            this.tbAddr.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.tbAddr.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tbAddr.Name = "tbAddr";
            this.tbAddr.Size = new System.Drawing.Size(58, 20);
            this.tbAddr.TabIndex = 12;
            this.tbAddr.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tbAddr.ValueChanged += new System.EventHandler(this.TbAddressValueChanged);
            // 
            // cmdRead
            // 
            this.cmdRead.AutoSize = true;
            this.cmdRead.Location = new System.Drawing.Point(541, 3);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(30, 23);
            this.cmdRead.TabIndex = 13;
            this.cmdRead.Text = "&R";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // cmdWrite
            // 
            this.cmdWrite.AutoSize = true;
            this.cmdWrite.Location = new System.Drawing.Point(577, 3);
            this.cmdWrite.Name = "cmdWrite";
            this.cmdWrite.Size = new System.Drawing.Size(30, 23);
            this.cmdWrite.TabIndex = 14;
            this.cmdWrite.Text = "&W";
            this.cmdWrite.UseVisualStyleBackColor = true;
            this.cmdWrite.Click += new System.EventHandler(this.cmdWrite_Click);
            // 
            // cmdNotUsed
            // 
            this.cmdNotUsed.AutoSize = true;
            this.cmdNotUsed.Location = new System.Drawing.Point(613, 3);
            this.cmdNotUsed.Name = "cmdNotUsed";
            this.cmdNotUsed.Size = new System.Drawing.Size(65, 23);
            this.cmdNotUsed.TabIndex = 15;
            this.cmdNotUsed.Text = "&Not in use";
            this.cmdNotUsed.UseVisualStyleBackColor = true;
            this.cmdNotUsed.Click += new System.EventHandler(this.cmdNotUsed_Click);
            // 
            // LocoIOPinConfigurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tlpMain);
            this.Name = "LocoIOPinConfigurationControl";
            this.Size = new System.Drawing.Size(681, 200);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAddr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private LocoIOPinModeControl modeControl;
        private System.Windows.Forms.TextBox tbValue2;
        private System.Windows.Forms.TextBox tbConfig;
        private System.Windows.Forms.TextBox tbValue1;
        private System.Windows.Forms.NumericUpDown tbAddr;
        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.Button cmdWrite;
        private System.Windows.Forms.Button cmdNotUsed;
    }
}
