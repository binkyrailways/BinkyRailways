namespace BinkyRailways.WinApp.Controls.Edit
{
    partial class ValidationResultsControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValidationResultsControl));
            this.lvLog = new System.Windows.Forms.ListView();
            this.chMessage = new System.Windows.Forms.ColumnHeader();
            this.chSource = new System.Windows.Forms.ColumnHeader();
            this.chType = new System.Windows.Forms.ColumnHeader();
            this.images = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lvLog
            // 
            this.lvLog.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.lvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMessage,
            this.chSource,
            this.chType});
            resources.ApplyResources(this.lvLog, "lvLog");
            this.lvLog.FullRowSelect = true;
            this.lvLog.Name = "lvLog";
            this.lvLog.SmallImageList = this.images;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            this.lvLog.View = System.Windows.Forms.View.Details;
            this.lvLog.ItemActivate += new System.EventHandler(this.lvLog_ItemActivate);
            // 
            // chMessage
            // 
            resources.ApplyResources(this.chMessage, "chMessage");
            // 
            // chSource
            // 
            resources.ApplyResources(this.chSource, "chSource");
            // 
            // chType
            // 
            resources.ApplyResources(this.chType, "chType");
            // 
            // images
            // 
            this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
            this.images.TransparentColor = System.Drawing.Color.Transparent;
            this.images.Images.SetKeyName(0, "messagebox_critical_16.png");
            this.images.Images.SetKeyName(1, "messagebox_warning_16.png");
            this.images.Images.SetKeyName(2, "dialog_ok_apply_16.png");
            // 
            // ValidationResultsControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvLog);
            this.Name = "ValidationResultsControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvLog;
        private System.Windows.Forms.ColumnHeader chMessage;
        private System.Windows.Forms.ColumnHeader chSource;
        private System.Windows.Forms.ImageList images;
        private System.Windows.Forms.ColumnHeader chType;
    }
}
