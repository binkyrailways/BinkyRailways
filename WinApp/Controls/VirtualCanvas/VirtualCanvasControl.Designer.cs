namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    partial class VirtualCanvasControl
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
                rootContainer.OnLostVisibility();
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
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.panel = new ItemsPanel(this);
            this.SuspendLayout();
            // 
            // vScrollBar
            // 
            this.vScrollBar.Location = new System.Drawing.Point(554, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(16, 498);
            this.vScrollBar.TabIndex = 0;
            // 
            // hScrollBar
            // 
            this.hScrollBar.Location = new System.Drawing.Point(0, 481);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(554, 17);
            this.hScrollBar.TabIndex = 1;
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(570, 498);
            this.panel.TabIndex = 2;
            // 
            // VirtualCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.panel);
            this.Name = "VirtualCanvas";
            this.Size = new System.Drawing.Size(570, 498);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private ItemsPanel panel;
    }
}
