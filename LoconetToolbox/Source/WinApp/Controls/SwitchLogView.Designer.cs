namespace LocoNetToolBox.WinApp.Controls
{
    partial class SwitchLogView
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
            this.lvInputs = new System.Windows.Forms.ListView();
            this.chMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miClear = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvInputs
            // 
            this.lvInputs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMessage});
            this.lvInputs.ContextMenuStrip = this.contextMenu;
            this.lvInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvInputs.Location = new System.Drawing.Point(0, 0);
            this.lvInputs.Name = "lvInputs";
            this.lvInputs.Size = new System.Drawing.Size(442, 233);
            this.lvInputs.TabIndex = 0;
            this.lvInputs.UseCompatibleStateImageBehavior = false;
            this.lvInputs.View = System.Windows.Forms.View.Details;
            // 
            // chMessage
            // 
            this.chMessage.Text = "Message";
            this.chMessage.Width = 300;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miClear});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(102, 26);
            // 
            // miClear
            // 
            this.miClear.Name = "miClear";
            this.miClear.Size = new System.Drawing.Size(152, 22);
            this.miClear.Text = "Clear";
            this.miClear.Click += new System.EventHandler(this.miClear_Click);
            // 
            // SwitchLogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvInputs);
            this.Name = "SwitchLogView";
            this.Size = new System.Drawing.Size(442, 233);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvInputs;
        private System.Windows.Forms.ColumnHeader chMessage;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem miClear;
    }
}
