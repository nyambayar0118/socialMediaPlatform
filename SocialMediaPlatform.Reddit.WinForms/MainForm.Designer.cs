namespace SocialMediaPlatform.Reddit.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel toolbarPanel;
        private Panel contentPanel;
        private Label titleLabel;
        private Label usernameLabel;
        private Button editProfileButton;
        private Button logoutButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            toolbarPanel = new Panel();
            contentPanel = new Panel();
            titleLabel = new Label();
            usernameLabel = new Label();
            editProfileButton = new Button();
            logoutButton = new Button();
            SuspendLayout();

            // titleLabel
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            titleLabel.Location = new Point(8, 10);
            titleLabel.Name = "titleLabel";
            titleLabel.Text = "Reddit Forums";

            // usernameLabel
            usernameLabel.AutoSize = true;
            usernameLabel.Font = new Font("Segoe UI", 9F);
            usernameLabel.Location = new Point(870, 12);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Text = "";
            usernameLabel.Visible = false;

            // editProfileButton
            editProfileButton.Location = new Point(970, 8);
            editProfileButton.Name = "editProfileButton";
            editProfileButton.Size = new Size(90, 26);
            editProfileButton.TabIndex = 0;
            editProfileButton.Text = "Edit profile";
            editProfileButton.Visible = false;
            editProfileButton.Click += editProfileButton_Click;

            // logoutButton
            logoutButton.Location = new Point(1068, 8);
            logoutButton.Name = "logoutButton";
            logoutButton.Size = new Size(80, 26);
            logoutButton.TabIndex = 1;
            logoutButton.Text = "Sign out";
            logoutButton.Visible = false;
            logoutButton.Click += logoutButton_Click;

            // toolbarPanel
            toolbarPanel.Dock = DockStyle.Top;
            toolbarPanel.Height = 42;
            toolbarPanel.Name = "toolbarPanel";
            toolbarPanel.Paint += toolbarPanel_Paint;

            toolbarPanel.Controls.Add(titleLabel);
            toolbarPanel.Controls.Add(usernameLabel);
            toolbarPanel.Controls.Add(editProfileButton);
            toolbarPanel.Controls.Add(logoutButton);

            // contentPanel
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Name = "contentPanel";

            // MainForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 680);
            MinimumSize = new Size(800, 500);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reddit Forums";

            Controls.Add(contentPanel);
            Controls.Add(toolbarPanel);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}