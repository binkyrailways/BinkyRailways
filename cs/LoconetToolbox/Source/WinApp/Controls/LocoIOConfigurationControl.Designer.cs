namespace LocoNetToolBox.WinApp.Controls
{
    partial class LocoIOConfigurationControl
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
            this.cmdWrite2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdWrite1 = new System.Windows.Forms.Button();
            this.connector1 = new LocoNetToolBox.WinApp.Controls.LocoIOConnectorConfigurationControl();
            this.connector2 = new LocoNetToolBox.WinApp.Controls.LocoIOConnectorConfigurationControl();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.cmdWrite2, 1, 2);
            this.tlpMain.Controls.Add(this.connector1, 0, 1);
            this.tlpMain.Controls.Add(this.connector2, 1, 1);
            this.tlpMain.Controls.Add(this.label1, 0, 0);
            this.tlpMain.Controls.Add(this.label2, 1, 0);
            this.tlpMain.Controls.Add(this.cmdWrite1, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.Size = new System.Drawing.Size(640, 287);
            this.tlpMain.TabIndex = 0;
            // 
            // cmdWrite2
            // 
            this.cmdWrite2.Location = new System.Drawing.Point(323, 250);
            this.cmdWrite2.Name = "cmdWrite2";
            this.cmdWrite2.Size = new System.Drawing.Size(125, 32);
            this.cmdWrite2.TabIndex = 5;
            this.cmdWrite2.Text = "Write";
            this.cmdWrite2.UseVisualStyleBackColor = true;
            this.cmdWrite2.Click += new System.EventHandler(this.CmdWrite2Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "First connector";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(323, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Second connector";
            // 
            // cmdWrite1
            // 
            this.cmdWrite1.Location = new System.Drawing.Point(3, 250);
            this.cmdWrite1.Name = "cmdWrite1";
            this.cmdWrite1.Size = new System.Drawing.Size(125, 32);
            this.cmdWrite1.TabIndex = 4;
            this.cmdWrite1.Text = "Write";
            this.cmdWrite1.UseVisualStyleBackColor = true;
            this.cmdWrite1.Click += new System.EventHandler(this.CmdWrite1Click);
            // 
            // connector1
            // 
            this.connector1.AutoSize = true;
            this.connector1.Dock = System.Windows.Forms.DockStyle.Top;
            this.connector1.Location = new System.Drawing.Point(3, 23);
            this.connector1.Name = "connector1";
            this.connector1.Size = new System.Drawing.Size(314, 221);
            this.connector1.TabIndex = 0;
            // 
            // connector2
            // 
            this.connector2.AutoSize = true;
            this.connector2.Dock = System.Windows.Forms.DockStyle.Top;
            this.connector2.Location = new System.Drawing.Point(323, 23);
            this.connector2.Name = "connector2";
            this.connector2.Size = new System.Drawing.Size(314, 221);
            this.connector2.TabIndex = 1;
            // 
            // LocoIOConfigurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "LocoIOConfigurationControl";
            this.Size = new System.Drawing.Size(640, 345);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private LocoIOConnectorConfigurationControl connector1;
        private LocoIOConnectorConfigurationControl connector2;
        private System.Windows.Forms.Button cmdWrite2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdWrite1;

    }
}
