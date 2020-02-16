namespace QR_Launcher
{
    partial class Core
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
            this.components = new System.ComponentModel.Container();
            this.Notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.Context = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Ticker = new System.Windows.Forms.Timer(this.components);
            this.Context.SuspendLayout();
            this.SuspendLayout();
            // 
            // Notify
            // 
            this.Notify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.Notify.BalloonTipText = "QR Launcher";
            this.Notify.BalloonTipTitle = "Launching Task onStart";
            this.Notify.ContextMenuStrip = this.Context;
            this.Notify.Text = "notifyIcon1";
            this.Notify.Visible = true;
            // 
            // Context
            // 
            this.Context.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.Context.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewTaskToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.Context.Name = "Context";
            this.Context.Size = new System.Drawing.Size(195, 94);
            // 
            // addNewTaskToolStripMenuItem
            // 
            this.addNewTaskToolStripMenuItem.Name = "addNewTaskToolStripMenuItem";
            this.addNewTaskToolStripMenuItem.Size = new System.Drawing.Size(194, 30);
            this.addNewTaskToolStripMenuItem.Text = "Manage Tasks";
            this.addNewTaskToolStripMenuItem.Click += new System.EventHandler(this.AddNewTaskToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(194, 30);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(194, 30);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // Ticker
            // 
            this.Ticker.Enabled = true;
            this.Ticker.Tick += new System.EventHandler(this.DoTick);
            // 
            // Core
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 171);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Core";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "QR Launcher Image Analysis";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Core_Load);
            this.Context.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon Notify;
        private System.Windows.Forms.ContextMenuStrip Context;
        private System.Windows.Forms.ToolStripMenuItem addNewTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer Ticker;
    }
}

