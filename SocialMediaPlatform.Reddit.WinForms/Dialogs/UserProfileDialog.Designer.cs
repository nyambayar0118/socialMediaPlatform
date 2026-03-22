namespace SocialMediaPlatform.Reddit.WinForms.Dialogs
{
    partial class UserProfileDialog
    {
        private System.ComponentModel.IContainer components = null;

        private Panel avatarPanel;
        private Label avatarLabel;
        private Label usernameKeyLabel;
        private Label emailKeyLabel;
        private Label joinedKeyLabel;
        private Label usernameValue;
        private Label emailValue;
        private Label joinedValue;
        private Button closeButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            avatarPanel = new Panel();
            avatarLabel = new Label();
            usernameKeyLabel = new Label();
            emailKeyLabel = new Label();
            joinedKeyLabel = new Label();
            usernameValue = new Label();
            emailValue = new Label();
            joinedValue = new Label();
            closeButton = new Button();
            SuspendLayout();

            // avatarPanel
            avatarPanel.Location = new Point(32, 28);
            avatarPanel.Name = "avatarPanel";
            avatarPanel.Size = new Size(48, 48);

            // avatarLabel
            avatarLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            avatarLabel.ForeColor = Color.FromArgb(0x0C, 0x44, 0x7C);
            avatarLabel.Location = new Point(0, 0);
            avatarLabel.Name = "avatarLabel";
            avatarLabel.Size = new Size(48, 48);
            avatarLabel.TextAlign = ContentAlignment.MiddleCenter;
            avatarPanel.Controls.Add(avatarLabel);

            // usernameKeyLabel
            usernameKeyLabel.AutoSize = true;
            usernameKeyLabel.ForeColor = SystemColors.GrayText;
            usernameKeyLabel.Location = new Point(32, 96);
            usernameKeyLabel.Name = "usernameKeyLabel";
            usernameKeyLabel.Size = new Size(70, 20);
            usernameKeyLabel.Text = "Username";

            // usernameValue
            usernameValue.AutoSize = true;
            usernameValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            usernameValue.Location = new Point(120, 96);
            usernameValue.Name = "usernameValue";
            usernameValue.Text = "";

            // emailKeyLabel
            emailKeyLabel.AutoSize = true;
            emailKeyLabel.ForeColor = SystemColors.GrayText;
            emailKeyLabel.Location = new Point(32, 126);
            emailKeyLabel.Name = "emailKeyLabel";
            emailKeyLabel.Size = new Size(70, 20);
            emailKeyLabel.Text = "Email";

            // emailValue
            emailValue.AutoSize = true;
            emailValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            emailValue.Location = new Point(120, 126);
            emailValue.Name = "emailValue";
            emailValue.Text = "";

            // joinedKeyLabel
            joinedKeyLabel.AutoSize = true;
            joinedKeyLabel.ForeColor = SystemColors.GrayText;
            joinedKeyLabel.Location = new Point(32, 156);
            joinedKeyLabel.Name = "joinedKeyLabel";
            joinedKeyLabel.Size = new Size(70, 20);
            joinedKeyLabel.Text = "Joined";

            // joinedValue
            joinedValue.AutoSize = true;
            joinedValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            joinedValue.Location = new Point(120, 156);
            joinedValue.Name = "joinedValue";
            joinedValue.Text = "";

            // closeButton
            closeButton.Location = new Point(208, 196);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(80, 28);
            closeButton.TabIndex = 0;
            closeButton.Text = "Close";
            closeButton.Click += closeButton_Click;

            // UserProfileDialog
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(320, 244);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserProfileDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "User profile";

            Controls.Add(avatarPanel);
            Controls.Add(usernameKeyLabel);
            Controls.Add(usernameValue);
            Controls.Add(emailKeyLabel);
            Controls.Add(emailValue);
            Controls.Add(joinedKeyLabel);
            Controls.Add(joinedValue);
            Controls.Add(closeButton);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}