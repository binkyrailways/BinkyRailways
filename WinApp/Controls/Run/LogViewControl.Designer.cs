namespace BinkyRailways.WinApp.Controls.Run
{
    partial class LogViewControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogViewControl));
            this.lvLog = new System.Windows.Forms.ListView();
            this.chMessage = new System.Windows.Forms.ColumnHeader();
            this.chSource = new System.Windows.Forms.ColumnHeader();
            this.chDetails = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxClear = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvLog
            // 
            this.lvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMessage,
            this.chSource,
            this.chDetails});
            this.lvLog.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.lvLog, "lvLog");
            this.lvLog.FullRowSelect = true;
            this.lvLog.Name = "lvLog";
            this.lvLog.UseCompatibleStateImageBehavior = false;
            this.lvLog.View = System.Windows.Forms.View.Details;
            // 
            // chMessage
            // 
            resources.ApplyResources(this.chMessage, "chMessage");
            // 
            // chSource
            // 
            resources.ApplyResources(this.chSource, "chSource");
            // 
            // chDetails
            // 
            resources.ApplyResources(this.chDetails, "chDetails");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxClear});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // ctxClear
            // 
            this.ctxClear.Name = "ctxClear";
            resources.ApplyResources(this.ctxClear, "ctxClear");
            this.ctxClear.Click += new System.EventHandler(this.ctxClear_Click);
            // 
            // LogViewControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvLog);
            this.Name = "LogViewControl";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvLog;
        private System.Windows.Forms.ColumnHeader chMessage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ctxClear;
        private System.Windows.Forms.ColumnHeader chDetails;
        private System.Windows.Forms.ColumnHeader chSource;
    }
}
