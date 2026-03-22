namespace SocialMediaPlatform.Reddit.WinForms.Panels
{
    partial class LoginPanel
    {
        private System.ComponentModel.IContainer components = null;

        private Panel cardPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private Label usernameLabel;
        private Label passwordLabel;
        private TextBox usernameField;
        private TextBox passwordField;
        private Label errorLabel;
        private Button loginButton;
        private Label noAccountLabel;
        private Button registerLink;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            cardPanel = new Panel();
            titleLabel = new Label();
            subtitleLabel = new Label();
            usernameLabel = new Label();
            passwordLabel = new Label();
            usernameField = new TextBox();
            passwordField = new TextBox();
            errorLabel = new Label();
            loginButton = new Button();
            noAccountLabel = new Label();
            registerLink = new Button();
            SuspendLayout();

            // ─── CARD PANEL ────────────────────────────────────────────
            cardPanel.BorderStyle = BorderStyle.FixedSingle;
            cardPanel.Size = new Size(340, 340);
            cardPanel.Name = "cardPanel";

            // titleLabel
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            titleLabel.Location = new Point(40, 32);
            titleLabel.Name = "titleLabel";
            titleLabel.Text = "Sign in";

            // subtitleLabel
            subtitleLabel.AutoSize = true;
            subtitleLabel.ForeColor = SystemColors.GrayText;
            subtitleLabel.Location = new Point(40, 60);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Text = "Enter your credentials to continue";

            // usernameLabel
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(40, 96);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Text = "Username";

            // usernameField
            usernameField.Location = new Point(40, 116);
            usernameField.Name = "usernameField";
            usernameField.Size = new Size(260, 27);
            usernameField.TabIndex = 0;

            // passwordLabel
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(40, 154);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Text = "Password";

            // passwordField
            passwordField.Location = new Point(40, 174);
            passwordField.Name = "passwordField";
            passwordField.Size = new Size(260, 27);
            passwordField.TabIndex = 1;
            passwordField.PasswordChar = '●';

            // errorLabel
            errorLabel.AutoSize = true;
            errorLabel.ForeColor = Color.Red;
            errorLabel.Location = new Point(40, 210);
            errorLabel.Name = "errorLabel";
            errorLabel.Text = "";

            // loginButton
            loginButton.Location = new Point(40, 232);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(260, 30);
            loginButton.TabIndex = 2;
            loginButton.Text = "Sign in";
            loginButton.Click += loginButton_Click;

            // noAccountLabel
            noAccountLabel.AutoSize = true;
            noAccountLabel.Location = new Point(40, 274);
            noAccountLabel.Name = "noAccountLabel";
            noAccountLabel.Text = "No account?";

            // registerLink
            registerLink.AutoSize = true;
            registerLink.FlatStyle = FlatStyle.Flat;
            registerLink.FlatAppearance.BorderSize = 0;
            registerLink.ForeColor = Color.FromArgb(0x18, 0x5F, 0xA5);
            registerLink.Location = new Point(118, 270);
            registerLink.Name = "registerLink";
            registerLink.Text = "Register";
            registerLink.TabIndex = 3;
            registerLink.Click += registerLink_Click;

            cardPanel.Controls.Add(titleLabel);
            cardPanel.Controls.Add(subtitleLabel);
            cardPanel.Controls.Add(usernameLabel);
            cardPanel.Controls.Add(usernameField);
            cardPanel.Controls.Add(passwordLabel);
            cardPanel.Controls.Add(passwordField);
            cardPanel.Controls.Add(errorLabel);
            cardPanel.Controls.Add(loginButton);
            cardPanel.Controls.Add(noAccountLabel);
            cardPanel.Controls.Add(registerLink);

            // ─── LOGIN PANEL ───────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "LoginPanel";
            Size = new Size(1100, 640);

            Controls.Add(cardPanel);

            ResumeLayout(false);
            PerformLayout();

            // center the card after layout
            cardPanel.Location = new Point(
                (Width - cardPanel.Width) / 2,
                (Height - cardPanel.Height) / 2
            );
        }
    }
}