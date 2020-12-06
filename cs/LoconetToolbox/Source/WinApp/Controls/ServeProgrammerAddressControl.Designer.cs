namespace LocoNetToolBox.WinApp.Controls
{
    partial class ServeProgrammerAddressControl
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
            this.udAddress4 = new System.Windows.Forms.NumericUpDown();
            this.udAddress3 = new System.Windows.Forms.NumericUpDown();
            this.udAddress2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.udAddress1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAddress4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAddress3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAddress2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAddress1)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.Controls.Add(this.udAddress4, 1, 3);
            this.tlpMain.Controls.Add(this.udAddress3, 1, 2);
            this.tlpMain.Controls.Add(this.udAddress2, 1, 1);
            this.tlpMain.Controls.Add(this.label2, 0, 1);
            this.tlpMain.Controls.Add(this.label1, 0, 0);
            this.tlpMain.Controls.Add(this.udAddress1, 1, 0);
            this.tlpMain.Controls.Add(this.label3, 0, 2);
            this.tlpMain.Controls.Add(this.label4, 0, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(248, 104);
            this.tlpMain.TabIndex = 0;
            // 
            // udAddress4
            // 
            this.udAddress4.Location = new System.Drawing.Point(63, 81);
            this.udAddress4.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.udAddress4.Name = "udAddress4";
            this.udAddress4.Size = new System.Drawing.Size(60, 20);
            this.udAddress4.TabIndex = 7;
            this.udAddress4.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.udAddress4.ValueChanged += new System.EventHandler(this.udAddress4_ValueChanged);
            // 
            // udAddress3
            // 
            this.udAddress3.Location = new System.Drawing.Point(63, 55);
            this.udAddress3.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.udAddress3.Name = "udAddress3";
            this.udAddress3.Size = new System.Drawing.Size(60, 20);
            this.udAddress3.TabIndex = 6;
            this.udAddress3.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.udAddress3.ValueChanged += new System.EventHandler(this.udAddress3_ValueChanged);
            // 
            // udAddress2
            // 
            this.udAddress2.Location = new System.Drawing.Point(63, 29);
            this.udAddress2.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.udAddress2.Name = "udAddress2";
            this.udAddress2.Size = new System.Drawing.Size(60, 20);
            this.udAddress2.TabIndex = 5;
            this.udAddress2.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.udAddress2.ValueChanged += new System.EventHandler(this.udAddress2_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Address 2";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address 1";
            // 
            // udAddress1
            // 
            this.udAddress1.Location = new System.Drawing.Point(63, 3);
            this.udAddress1.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.udAddress1.Name = "udAddress1";
            this.udAddress1.Size = new System.Drawing.Size(60, 20);
            this.udAddress1.TabIndex = 1;
            this.udAddress1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udAddress1.ValueChanged += new System.EventHandler(this.udAddress1_ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Address 3";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Address 4";
            // 
            // ServeProgrammerAddressControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "ServeProgrammerAddressControl";
            this.Size = new System.Drawing.Size(248, 180);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAddress4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAddress3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAddress2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAddress1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown udAddress4;
        private System.Windows.Forms.NumericUpDown udAddress3;
        private System.Windows.Forms.NumericUpDown udAddress2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udAddress1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
