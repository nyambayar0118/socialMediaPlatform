namespace SocialMediaPlatform.Reddit.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel contentPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            contentPanel = new Panel();
            SuspendLayout();

            // contentPanel
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Name = "contentPanel";

            // MainForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 680);
            MinimumSize = new Size(800, 500);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reddit Forums";

            Controls.Add(contentPanel);

            ResumeLayout(false);
        }
    }
}