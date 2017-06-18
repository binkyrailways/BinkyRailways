namespace LocoNetToolBox.WinApp.Controls
{
    partial class InputStateView
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
            this.chAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lvInputs
            // 
            this.lvInputs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAddress});
            this.lvInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvInputs.Location = new System.Drawing.Point(0, 0);
            this.lvInputs.Name = "lvInputs";
            this.lvInputs.Size = new System.Drawing.Size(442, 233);
            this.lvInputs.TabIndex = 0;
            this.lvInputs.TileSize = new System.Drawing.Size(64, 16);
            this.lvInputs.UseCompatibleStateImageBehavior = false;
            this.lvInputs.View = System.Windows.Forms.View.Tile;
            // 
            // chAddress
            // 
            this.chAddress.Text = "Address";
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 250;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // InputStateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvInputs);
            this.Name = "InputStateView";
            this.Size = new System.Drawing.Size(442, 233);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvInputs;
        private System.Windows.Forms.ColumnHeader chAddress;
        private System.Windows.Forms.Timer updateTimer;
    }
}
