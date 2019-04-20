namespace LocoNetToolBox.WinApp.Controls
{
    partial class ServoProgrammer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServoProgrammer));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.step4 = new LocoNetToolBox.WinApp.Controls.ServoProgrammerStep4();
            this.step3 = new LocoNetToolBox.WinApp.Controls.ServoProgrammerStep3();
            this.step2 = new LocoNetToolBox.WinApp.Controls.ServoProgrammerStep2();
            this.step1 = new LocoNetToolBox.WinApp.Controls.ServoProgrammerStep1();
            this.lbStep = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.step4, 0, 4);
            this.tlpMain.Controls.Add(this.step3, 0, 3);
            this.tlpMain.Controls.Add(this.step2, 0, 2);
            this.tlpMain.Controls.Add(this.step1, 0, 1);
            this.tlpMain.Controls.Add(this.lbStep, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 6;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(728, 392);
            this.tlpMain.TabIndex = 4;
            // 
            // step4
            // 
            this.step4.AutoSize = true;
            this.step4.Dock = System.Windows.Forms.DockStyle.Top;
            this.step4.Location = new System.Drawing.Point(3, 251);
            this.step4.MinimumSize = new System.Drawing.Size(500, 38);
            this.step4.Name = "step4";
            this.step4.Size = new System.Drawing.Size(722, 236);
            this.step4.TabIndex = 4;
            this.step4.Visible = false;
            // 
            // step3
            // 
            this.step3.AutoSize = true;
            this.step3.Dock = System.Windows.Forms.DockStyle.Top;
            this.step3.Location = new System.Drawing.Point(3, 198);
            this.step3.MinimumSize = new System.Drawing.Size(500, 38);
            this.step3.Name = "step3";
            this.step3.Size = new System.Drawing.Size(722, 47);
            this.step3.TabIndex = 3;
            this.step3.Visible = false;
            this.step3.Continue += new System.EventHandler(this.step3_Continue);
            // 
            // step2
            // 
            this.step2.AutoSize = true;
            this.step2.Dock = System.Windows.Forms.DockStyle.Top;
            this.step2.Location = new System.Drawing.Point(3, 145);
            this.step2.MinimumSize = new System.Drawing.Size(500, 38);
            this.step2.Name = "step2";
            this.step2.Size = new System.Drawing.Size(722, 47);
            this.step2.TabIndex = 2;
            this.step2.Visible = false;
            this.step2.Continue += new System.EventHandler(this.step2_Continue);
            // 
            // step1
            // 
            this.step1.AutoSize = true;
            this.step1.Dock = System.Windows.Forms.DockStyle.Top;
            this.step1.Location = new System.Drawing.Point(3, 29);
            this.step1.MinimumSize = new System.Drawing.Size(500, 38);
            this.step1.Name = "step1";
            this.step1.Size = new System.Drawing.Size(722, 110);
            this.step1.TabIndex = 1;
            this.step1.Continue += new System.EventHandler(this.step1_Continue);
            // 
            // lbStep
            // 
            this.lbStep.AutoSize = true;
            this.lbStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStep.Location = new System.Drawing.Point(3, 0);
            this.lbStep.Name = "lbStep";
            this.lbStep.Size = new System.Drawing.Size(93, 26);
            this.lbStep.TabIndex = 5;
            this.lbStep.Text = "Step 1/4";
            // 
            // ServoProgrammer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 392);
            this.Controls.Add(this.tlpMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 38);
            this.Name = "ServoProgrammer";
            this.Text = "MGV Servo Configurator";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private ServoProgrammerStep4 step4;
        private ServoProgrammerStep3 step3;
        private ServoProgrammerStep2 step2;
        private ServoProgrammerStep1 step1;
        private System.Windows.Forms.Label lbStep;




    }
}