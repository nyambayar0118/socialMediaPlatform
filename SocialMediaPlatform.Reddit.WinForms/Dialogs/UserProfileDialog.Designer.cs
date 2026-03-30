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
            avatarPanel.SuspendLayout();
            SuspendLayout();
            // 
            // avatarPanel
            // 
            avatarPanel.Controls.Add(avatarLabel);
            avatarPanel.Location = new Point(32, 28);
            avatarPanel.Name = "avatarPanel";
            avatarPanel.Size = new Size(48, 48);
            avatarPanel.TabIndex = 0;
            // 
            // avatarLabel
            // 
            avatarLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            avatarLabel.ForeColor = Color.FromArgb(12, 68, 124);
            avatarLabel.Location = new Point(0, 0);
            avatarLabel.Name = "avatarLabel";
            avatarLabel.Size = new Size(48, 48);
            avatarLabel.TabIndex = 0;
            avatarLabel.TextAlign = ContentAlignment.MiddleCenter;
            avatarLabel.Click += avatarLabel_Click;
            // 
            // usernameKeyLabel
            // 
            usernameKeyLabel.AutoSize = true;
            usernameKeyLabel.ForeColor = SystemColors.GrayText;
            usernameKeyLabel.Location = new Point(32, 96);
            usernameKeyLabel.Name = "usernameKeyLabel";
            usernameKeyLabel.Size = new Size(75, 20);
            usernameKeyLabel.TabIndex = 1;
            usernameKeyLabel.Text = "Username";
            // 
            // emailKeyLabel
            // 
            emailKeyLabel.AutoSize = true;
            emailKeyLabel.ForeColor = SystemColors.GrayText;
            emailKeyLabel.Location = new Point(32, 126);
            emailKeyLabel.Name = "emailKeyLabel";
            emailKeyLabel.Size = new Size(46, 20);
            emailKeyLabel.TabIndex = 3;
            emailKeyLabel.Text = "Email";
            // 
            // joinedKeyLabel
            // 
            joinedKeyLabel.AutoSize = true;
            joinedKeyLabel.ForeColor = SystemColors.GrayText;
            joinedKeyLabel.Location = new Point(32, 156);
            joinedKeyLabel.Name = "joinedKeyLabel";
            joinedKeyLabel.Size = new Size(52, 20);
            joinedKeyLabel.TabIndex = 5;
            joinedKeyLabel.Text = "Joined";
            // 
            // usernameValue
            // 
            usernameValue.AutoSize = true;
            usernameValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            usernameValue.Location = new Point(120, 96);
            usernameValue.Name = "usernameValue";
            usernameValue.Size = new Size(0, 20);
            usernameValue.TabIndex = 2;
            // 
            // emailValue
            // 
            emailValue.AutoSize = true;
            emailValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            emailValue.Location = new Point(120, 126);
            emailValue.Name = "emailValue";
            emailValue.Size = new Size(0, 20);
            emailValue.TabIndex = 4;
            // 
            // joinedValue
            // 
            joinedValue.AutoSize = true;
            joinedValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            joinedValue.Location = new Point(120, 156);
            joinedValue.Name = "joinedValue";
            joinedValue.Size = new Size(0, 20);
            joinedValue.TabIndex = 6;
            // 
            // closeButton
            // 
            closeButton.Location = new Point(208, 196);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(80, 28);
            closeButton.TabIndex = 0;
            closeButton.Text = "Close";
            closeButton.Click += closeButton_Click;
            // 
            // UserProfileDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(320, 244);
            Controls.Add(avatarPanel);
            Controls.Add(usernameKeyLabel);
            Controls.Add(usernameValue);
            Controls.Add(emailKeyLabel);
            Controls.Add(emailValue);
            Controls.Add(joinedKeyLabel);
            Controls.Add(joinedValue);
            Controls.Add(closeButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserProfileDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "User profile";
            avatarPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}