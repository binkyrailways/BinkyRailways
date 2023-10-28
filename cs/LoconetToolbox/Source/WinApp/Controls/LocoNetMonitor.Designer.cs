namespace LocoNetToolBox.WinApp.Controls
{
    partial class LocoNetMonitor
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageAddressMonitor = new System.Windows.Forms.TabPage();
            this.lbInputs = new LocoNetToolBox.WinApp.Controls.InputStateView();
            this.tabPageFeedbackMonitor = new System.Windows.Forms.TabPage();
            this.inputLogView = new LocoNetToolBox.WinApp.Controls.InputLogView();
            this.tabPageSwitchMonitor = new System.Windows.Forms.TabPage();
            this.switchLogView = new LocoNetToolBox.WinApp.Controls.SwitchLogView();
            this.tabPageTrafficMonitor = new System.Windows.Forms.TabPage();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.contextMenuTrafficLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miClearTrafficLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl.SuspendLayout();
            this.tabPageAddressMonitor.SuspendLayout();
            this.tabPageFeedbackMonitor.SuspendLayout();
            this.tabPageSwitchMonitor.SuspendLayout();
            this.tabPageTrafficMonitor.SuspendLayout();
            this.contextMenuTrafficLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageAddressMonitor);
            this.tabControl.Controls.Add(this.tabPageFeedbackMonitor);
            this.tabControl.Controls.Add(this.tabPageSwitchMonitor);
            this.tabControl.Controls.Add(this.tabPageTrafficMonitor);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(720, 466);
            this.tabControl.TabIndex = 1;
            // 
            // tabPageAddressMonitor
            // 
            this.tabPageAddressMonitor.Controls.Add(this.lbInputs);
            this.tabPageAddressMonitor.Location = new System.Drawing.Point(4, 22);
            this.tabPageAddressMonitor.Name = "tabPageAddressMonitor";
            this.tabPageAddressMonitor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAddressMonitor.Size = new System.Drawing.Size(712, 440);
            this.tabPageAddressMonitor.TabIndex = 0;
            this.tabPageAddressMonitor.Text = "Address monitor";
            this.tabPageAddressMonitor.UseVisualStyleBackColor = true;
            // 
            // lbInputs
            // 
            this.lbInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInputs.Location = new System.Drawing.Point(3, 3);
            this.lbInputs.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.lbInputs.Name = "lbInputs";
            this.lbInputs.Size = new System.Drawing.Size(706, 434);
            this.lbInputs.TabIndex = 4;
            // 
            // tabPageFeedbackMonitor
            // 
            this.tabPageFeedbackMonitor.Controls.Add(this.inputLogView);
            this.tabPageFeedbackMonitor.Location = new System.Drawing.Point(4, 22);
            this.tabPageFeedbackMonitor.Name = "tabPageFeedbackMonitor";
            this.tabPageFeedbackMonitor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFeedbackMonitor.Size = new System.Drawing.Size(712, 440);
            this.tabPageFeedbackMonitor.TabIndex = 2;
            this.tabPageFeedbackMonitor.Text = "Feedback log";
            this.tabPageFeedbackMonitor.UseVisualStyleBackColor = true;
            // 
            // inputLogView
            // 
            this.inputLogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputLogView.Location = new System.Drawing.Point(3, 3);
            this.inputLogView.Name = "inputLogView";
            this.inputLogView.Size = new System.Drawing.Size(706, 434);
            this.inputLogView.TabIndex = 0;
            // 
            // tabPageSwitchMonitor
            // 
            this.tabPageSwitchMonitor.Controls.Add(this.switchLogView);
            this.tabPageSwitchMonitor.Location = new System.Drawing.Point(4, 22);
            this.tabPageSwitchMonitor.Name = "tabPageSwitchMonitor";
            this.tabPageSwitchMonitor.Size = new System.Drawing.Size(712, 440);
            this.tabPageSwitchMonitor.TabIndex = 3;
            this.tabPageSwitchMonitor.Text = "Switch log";
            this.tabPageSwitchMonitor.UseVisualStyleBackColor = true;
            // 
            // switchLogView
            // 
            this.switchLogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.switchLogView.Location = new System.Drawing.Point(0, 0);
            this.switchLogView.Name = "switchLogView";
            this.switchLogView.Size = new System.Drawing.Size(712, 440);
            this.switchLogView.TabIndex = 0;
            // 
            // tabPageTrafficMonitor
            // 
            this.tabPageTrafficMonitor.Controls.Add(this.lbLog);
            this.tabPageTrafficMonitor.Location = new System.Drawing.Point(4, 22);
            this.tabPageTrafficMonitor.Name = "tabPageTrafficMonitor";
            this.tabPageTrafficMonitor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTrafficMonitor.Size = new System.Drawing.Size(712, 440);
            this.tabPageTrafficMonitor.TabIndex = 1;
            this.tabPageTrafficMonitor.Text = "Traffic log";
            this.tabPageTrafficMonitor.UseVisualStyleBackColor = true;
            // 
            // lbLog
            // 
            this.lbLog.ContextMenuStrip = this.contextMenuTrafficLog;
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.FormattingEnabled = true;
            this.lbLog.HorizontalScrollbar = true;
            this.lbLog.Location = new System.Drawing.Point(3, 3);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(706, 434);
            this.lbLog.TabIndex = 3;
            // 
            // contextMenuTrafficLog
            // 
            this.contextMenuTrafficLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miClearTrafficLog});
            this.contextMenuTrafficLog.Name = "contextMenuTrafficLog";
            this.contextMenuTrafficLog.Size = new System.Drawing.Size(102, 26);
            // 
            // miClearTrafficLog
            // 
            this.miClearTrafficLog.Name = "miClearTrafficLog";
            this.miClearTrafficLog.Size = new System.Drawing.Size(101, 22);
            this.miClearTrafficLog.Text = "Clear";
            this.miClearTrafficLog.Click += new System.EventHandler(this.miClearTrafficLog_Click);
            // 
            // LocoNetMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "LocoNetMonitor";
            this.Size = new System.Drawing.Size(720, 466);
            this.tabControl.ResumeLayout(false);
            this.tabPageAddressMonitor.ResumeLayout(false);
            this.tabPageFeedbackMonitor.ResumeLayout(false);
            this.tabPageSwitchMonitor.ResumeLayout(false);
            this.tabPageTrafficMonitor.ResumeLayout(false);
            this.contextMenuTrafficLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageAddressMonitor;
        private InputStateView lbInputs;
        private System.Windows.Forms.TabPage tabPageTrafficMonitor;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.TabPage tabPageFeedbackMonitor;
        private InputLogView inputLogView;
        private System.Windows.Forms.TabPage tabPageSwitchMonitor;
        private SwitchLogView switchLogView;
        private System.Windows.Forms.ContextMenuStrip contextMenuTrafficLog;
        private System.Windows.Forms.ToolStripMenuItem miClearTrafficLog;

    }
}
