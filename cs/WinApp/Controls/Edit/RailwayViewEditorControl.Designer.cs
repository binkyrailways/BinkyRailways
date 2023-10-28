namespace BinkyRailways.WinApp.Controls.Edit
{
    partial class RailwayViewEditorControl
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
            if (context != null)
            {
                context.Changed -= OnContextChanged;
            }
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
            this.canvas = new BinkyRailways.WinApp.Controls.EntityCanvasControl();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.AllowDrop = true;
            this.canvas.BackColor = System.Drawing.Color.DarkGray;
            this.canvas.CanvasColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(401, 223);
            this.canvas.TabIndex = 0;
            this.canvas.VisibleLeft = 0;
            this.canvas.VisibleTop = 0;
            this.canvas.ZoomFactor = 1F;
            // 
            // RailwayViewEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.canvas);
            this.Name = "RailwayViewEditorControl";
            this.Size = new System.Drawing.Size(401, 223);
            this.ResumeLayout(false);

        }

        #endregion

        private EntityCanvasControl canvas;
    }
}
