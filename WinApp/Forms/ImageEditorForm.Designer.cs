namespace BinkyRailways.WinApp.Forms
{
    partial class ImageEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageEditorForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdOpen = new System.Windows.Forms.ToolStripButton();
            this.cmdRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdSaveAs = new System.Windows.Forms.ToolStripButton();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.scrollPane = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            this.scrollPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdOpen,
            this.cmdRemove,
            this.toolStripSeparator1,
            this.cmdSaveAs});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // cmdOpen
            // 
            this.cmdOpen.Image = global::BinkyRailways.Properties.Resources.fileopen_22;
            resources.ApplyResources(this.cmdOpen, "cmdOpen");
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Click += new System.EventHandler(this.OnOpenClick);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Image = global::BinkyRailways.Properties.Resources.remove_22;
            resources.ApplyResources(this.cmdRemove, "cmdRemove");
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Click += new System.EventHandler(this.OnRemoveClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // cmdSaveAs
            // 
            this.cmdSaveAs.Image = global::BinkyRailways.Properties.Resources.filesave_22;
            resources.ApplyResources(this.cmdSaveAs, "cmdSaveAs");
            this.cmdSaveAs.Name = "cmdSaveAs";
            this.cmdSaveAs.Click += new System.EventHandler(this.OnSaveAsClick);
            // 
            // cmdOk
            // 
            resources.ApplyResources(this.cmdOk, "cmdOk");
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // scrollPane
            // 
            resources.ApplyResources(this.scrollPane, "scrollPane");
            this.scrollPane.Controls.Add(this.pictureBox);
            this.scrollPane.Name = "scrollPane";
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            // 
            // ImageEditorForm
            // 
            this.AcceptButton = this.cmdOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.scrollPane);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageEditorForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.scrollPane.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ToolStripButton cmdOpen;
        private System.Windows.Forms.ToolStripButton cmdSaveAs;
        private System.Windows.Forms.Panel scrollPane;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripButton cmdRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}