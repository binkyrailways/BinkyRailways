namespace BinkyRailways.WinApp.Forms
{
    partial class WebServerForm
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.cmdOpenBrowser = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(16, 16);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(256, 256);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // cmdOpenBrowser
            // 
            this.cmdOpenBrowser.Location = new System.Drawing.Point(288, 16);
            this.cmdOpenBrowser.Name = "cmdOpenBrowser";
            this.cmdOpenBrowser.Size = new System.Drawing.Size(136, 40);
            this.cmdOpenBrowser.TabIndex = 1;
            this.cmdOpenBrowser.Text = "&Open in browser";
            this.cmdOpenBrowser.UseVisualStyleBackColor = true;
            this.cmdOpenBrowser.Click += new System.EventHandler(this.OnOpenBrowserClick);
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(288, 224);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(136, 40);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "&Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            // 
            // WebServerForm
            // 
            this.AcceptButton = this.cmdOpenBrowser;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(437, 286);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOpenBrowser);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WebServerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Remote connection";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button cmdOpenBrowser;
        private System.Windows.Forms.Button cmdClose;
    }
}