namespace BinkyRailways.WinApp.Controls.Run
{
    partial class RailwayStateControlPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RailwayStateControlPanel));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbAutoControl = new System.Windows.Forms.Label();
            this.cmdPowerOff = new System.Windows.Forms.Button();
            this.lbPower = new System.Windows.Forms.Label();
            this.cmdPowerOn = new System.Windows.Forms.Button();
            this.cmdAutoOn = new System.Windows.Forms.Button();
            this.cmdAutoOff = new System.Windows.Forms.Button();
            this.pbPower = new System.Windows.Forms.PictureBox();
            this.pbAuto = new System.Windows.Forms.PictureBox();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAuto)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.lbAutoControl, 1, 1);
            this.tlpMain.Controls.Add(this.cmdPowerOff, 3, 0);
            this.tlpMain.Controls.Add(this.lbPower, 1, 0);
            this.tlpMain.Controls.Add(this.cmdPowerOn, 2, 0);
            this.tlpMain.Controls.Add(this.cmdAutoOn, 2, 1);
            this.tlpMain.Controls.Add(this.cmdAutoOff, 3, 1);
            this.tlpMain.Controls.Add(this.pbPower, 0, 0);
            this.tlpMain.Controls.Add(this.pbAuto, 0, 1);
            this.tlpMain.Name = "tlpMain";
            // 
            // lbAutoControl
            // 
            this.lbAutoControl.AutoEllipsis = true;
            resources.ApplyResources(this.lbAutoControl, "lbAutoControl");
            this.lbAutoControl.Name = "lbAutoControl";
            // 
            // cmdPowerOff
            // 
            resources.ApplyResources(this.cmdPowerOff, "cmdPowerOff");
            this.cmdPowerOff.Name = "cmdPowerOff";
            this.cmdPowerOff.UseVisualStyleBackColor = true;
            this.cmdPowerOff.Click += new System.EventHandler(this.OnPowerOffClick);
            // 
            // lbPower
            // 
            this.lbPower.AutoEllipsis = true;
            resources.ApplyResources(this.lbPower, "lbPower");
            this.lbPower.Name = "lbPower";
            // 
            // cmdPowerOn
            // 
            resources.ApplyResources(this.cmdPowerOn, "cmdPowerOn");
            this.cmdPowerOn.Name = "cmdPowerOn";
            this.cmdPowerOn.UseVisualStyleBackColor = true;
            this.cmdPowerOn.Click += new System.EventHandler(this.OnPowerOnClick);
            // 
            // cmdAutoOn
            // 
            resources.ApplyResources(this.cmdAutoOn, "cmdAutoOn");
            this.cmdAutoOn.Name = "cmdAutoOn";
            this.cmdAutoOn.UseVisualStyleBackColor = true;
            this.cmdAutoOn.Click += new System.EventHandler(this.tbAutomatic_Click);
            // 
            // cmdAutoOff
            // 
            resources.ApplyResources(this.cmdAutoOff, "cmdAutoOff");
            this.cmdAutoOff.Name = "cmdAutoOff";
            this.cmdAutoOff.UseVisualStyleBackColor = true;
            this.cmdAutoOff.Click += new System.EventHandler(this.tbManual_Click);
            // 
            // pbPower
            // 
            resources.ApplyResources(this.pbPower, "pbPower");
            this.pbPower.Name = "pbPower";
            this.pbPower.TabStop = false;
            // 
            // pbAuto
            // 
            resources.ApplyResources(this.pbAuto, "pbAuto");
            this.pbAuto.Name = "pbAuto";
            this.pbAuto.TabStop = false;
            // 
            // RailwayStateControlPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "RailwayStateControlPanel";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAuto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbAutoControl;
        private System.Windows.Forms.Button cmdPowerOff;
        private System.Windows.Forms.Label lbPower;
        private System.Windows.Forms.Button cmdPowerOn;
        private System.Windows.Forms.Button cmdAutoOn;
        private System.Windows.Forms.Button cmdAutoOff;
        private System.Windows.Forms.PictureBox pbPower;
        private System.Windows.Forms.PictureBox pbAuto;

    }
}
