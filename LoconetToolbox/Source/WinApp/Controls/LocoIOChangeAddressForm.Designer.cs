namespace LocoNetToolBox.WinApp.Controls
{
    partial class LocoIOChangeAddressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocoIOChangeAddressForm));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbWarning = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.upAddress = new System.Windows.Forms.NumericUpDown();
            this.upSubAddress = new System.Windows.Forms.NumericUpDown();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdChange = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upSubAddress)).BeginInit();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lbWarning, 0, 0);
            this.tlpMain.Controls.Add(this.label1, 0, 1);
            this.tlpMain.Controls.Add(this.label2, 0, 2);
            this.tlpMain.Controls.Add(this.upAddress, 1, 1);
            this.tlpMain.Controls.Add(this.upSubAddress, 1, 2);
            this.tlpMain.Controls.Add(this.tlpButtons, 0, 4);
            this.tlpMain.Controls.Add(this.lbStatus, 0, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(398, 124);
            this.tlpMain.TabIndex = 0;
            // 
            // lbWarning
            // 
            this.lbWarning.AutoSize = true;
            this.lbWarning.BackColor = System.Drawing.Color.Yellow;
            this.tlpMain.SetColumnSpan(this.lbWarning, 2);
            this.lbWarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbWarning.Location = new System.Drawing.Point(3, 0);
            this.lbWarning.Name = "lbWarning";
            this.lbWarning.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lbWarning.Size = new System.Drawing.Size(392, 23);
            this.lbWarning.TabIndex = 0;
            this.lbWarning.Text = "Important! Make sure only 1 LocoIO is attached to the Locobuffer.";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "New address:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "New sub address:";
            // 
            // upAddress
            // 
            this.upAddress.Location = new System.Drawing.Point(101, 26);
            this.upAddress.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.upAddress.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upAddress.Name = "upAddress";
            this.upAddress.Size = new System.Drawing.Size(83, 20);
            this.upAddress.TabIndex = 3;
            this.upAddress.Value = new decimal(new int[] {
            81,
            0,
            0,
            0});
            // 
            // upSubAddress
            // 
            this.upSubAddress.Location = new System.Drawing.Point(101, 52);
            this.upSubAddress.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.upSubAddress.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upSubAddress.Name = "upSubAddress";
            this.upSubAddress.Size = new System.Drawing.Size(83, 20);
            this.upSubAddress.TabIndex = 4;
            this.upSubAddress.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tlpButtons
            // 
            this.tlpButtons.AutoSize = true;
            this.tlpButtons.ColumnCount = 3;
            this.tlpMain.SetColumnSpan(this.tlpButtons, 2);
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.Controls.Add(this.cmdChange, 1, 0);
            this.tlpButtons.Controls.Add(this.cmdClose, 2, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpButtons.Location = new System.Drawing.Point(3, 91);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpButtons.Size = new System.Drawing.Size(392, 30);
            this.tlpButtons.TabIndex = 5;
            // 
            // cmdChange
            // 
            this.cmdChange.Location = new System.Drawing.Point(207, 3);
            this.cmdChange.Name = "cmdChange";
            this.cmdChange.Size = new System.Drawing.Size(88, 24);
            this.cmdChange.TabIndex = 0;
            this.cmdChange.Text = "&Change now";
            this.cmdChange.UseVisualStyleBackColor = true;
            this.cmdChange.Click += new System.EventHandler(this.cmdChange_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(301, 3);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(88, 24);
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "C&lose";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.lbStatus, 2);
            this.lbStatus.Location = new System.Drawing.Point(3, 75);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(46, 13);
            this.lbStatus.TabIndex = 6;
            this.lbStatus.Text = "Status...";
            // 
            // LocoIOChangeAddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(398, 127);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LocoIOChangeAddressForm";
            this.Text = "Change address";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upSubAddress)).EndInit();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbWarning;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown upAddress;
        private System.Windows.Forms.NumericUpDown upSubAddress;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button cmdChange;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Label lbStatus;
    }
}