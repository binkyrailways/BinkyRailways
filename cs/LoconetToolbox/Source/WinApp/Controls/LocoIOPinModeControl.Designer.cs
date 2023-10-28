namespace LocoNetToolBox.WinApp.Controls
{
    partial class LocoIOPinModeControl
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
            this.cbModes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbModes
            // 
            this.cbModes.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModes.FormattingEnabled = true;
            this.cbModes.Location = new System.Drawing.Point(0, 0);
            this.cbModes.Name = "cbModes";
            this.cbModes.Size = new System.Drawing.Size(221, 21);
            this.cbModes.TabIndex = 0;
            this.cbModes.SelectedIndexChanged += new System.EventHandler(this.cbModes_SelectedIndexChanged);
            // 
            // LocoIOPortInputConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.cbModes);
            this.Name = "LocoIOPortInputConfigControl";
            this.Size = new System.Drawing.Size(221, 166);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbModes;

    }
}
