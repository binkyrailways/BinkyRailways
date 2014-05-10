namespace BinkyRailways.WinApp.Controls.Run
{
    partial class RouteInspectionControl
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
            this.lvRouteOptions = new System.Windows.Forms.ListView();
            this.chRoute = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvRouteOptions
            // 
            this.lvRouteOptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chRoute,
            this.chText});
            this.lvRouteOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvRouteOptions.Location = new System.Drawing.Point(0, 0);
            this.lvRouteOptions.MultiSelect = false;
            this.lvRouteOptions.Name = "lvRouteOptions";
            this.lvRouteOptions.Size = new System.Drawing.Size(559, 336);
            this.lvRouteOptions.TabIndex = 0;
            this.lvRouteOptions.UseCompatibleStateImageBehavior = false;
            this.lvRouteOptions.View = System.Windows.Forms.View.Details;
            // 
            // chRoute
            // 
            this.chRoute.Text = "Route";
            this.chRoute.Width = 120;
            // 
            // chText
            // 
            this.chText.Text = "Information";
            this.chText.Width = 300;
            // 
            // RouteInspectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvRouteOptions);
            this.Name = "RouteInspectionControl";
            this.Size = new System.Drawing.Size(559, 336);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvRouteOptions;
        private System.Windows.Forms.ColumnHeader chRoute;
        private System.Windows.Forms.ColumnHeader chText;
    }
}
