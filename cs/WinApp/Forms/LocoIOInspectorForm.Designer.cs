namespace BinkyRailways.WinApp.Forms
{
    partial class LocoIOInspectorForm
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
            inspectorControl.RailwayState = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocoIOInspectorForm));
            this.inspectorControl = new BinkyRailways.WinApp.Controls.Run.LocoIOInspectorControl();
            this.SuspendLayout();
            // 
            // inspectorControl
            // 
            resources.ApplyResources(this.inspectorControl, "inspectorControl");
            this.inspectorControl.Name = "inspectorControl";
            this.inspectorControl.RailwayState = null;
            // 
            // LocoIOInspectorForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.inspectorControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LocoIOInspectorForm";
            this.ResumeLayout(false);

        }

        #endregion

        private BinkyRailways.WinApp.Controls.Run.LocoIOInspectorControl inspectorControl;
    }
}