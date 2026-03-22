namespace SocialMediaPlatform.Reddit.WinForms.Dialogs
{
    partial class RegisterDialog
    {
        private System.ComponentModel.IContainer components = null;

        private Label titleLabel;
        private Label subtitleLabel;
        private Label usernameLabel;
        private Label emailLabel;
        private Label passwordLabel;
        private Label confirmPasswordLabel;
        private TextBox usernameField;
        private TextBox emailField;
        private TextBox passwordField;
        private TextBox confirmPasswordField;
        private Label errorLabel;
        private Button registerButton;
        private Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            titleLabel = new Label();
            subtitleLabel = new Label();
            usernameLabel = new Label();
            emailLabel = new Label();
            passwordLabel = new Label();
            confirmPasswordLabel = new Label();
            usernameField = new TextBox();
            emailField = new TextBox();
            passwordField = new TextBox();
            confirmPasswordField = new TextBox();
            errorLabel = new Label();
            registerButton = new Button();
            cancelButton = new Button();
            SuspendLayout();

            // titleLabel
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            titleLabel.Location = new Point(32, 28);
            titleLabel.Name = "titleLabel";
            titleLabel.Text = "Create account";

            // subtitleLabel
            subtitleLabel.AutoSize = true;
            subtitleLabel.ForeColor = SystemColors.GrayText;
            subtitleLabel.Location = new Point(32, 56);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Text = "Fill in your details to register";

            // usernameLabel
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(32, 84);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Text = "Username";

            // usernameField
            usernameField.Location = new Point(32, 104);
            usernameField.Name = "usernameField";
            usernameField.Size = new Size(296, 27);
            usernameField.TabIndex = 0;

            // emailLabel
            emailLabel.AutoSize = true;
            emailLabel.Location = new Point(32, 134);
            emailLabel.Name = "emailLabel";
            emailLabel.Text = "Email";

            // emailField
            emailField.Location = new Point(32, 154);
            emailField.Name = "emailField";
            emailField.Size = new Size(296, 27);
            emailField.TabIndex = 1;

            // passwordLabel
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(32, 184);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Text = "Password";

            // passwordField
            passwordField.Location = new Point(32, 204);
            passwordField.Name = "passwordField";
            passwordField.Size = new Size(296, 27);
            passwordField.TabIndex = 2;
            passwordField.PasswordChar = '●';

            // confirmPasswordLabel
            confirmPasswordLabel.AutoSize = true;
            confirmPasswordLabel.Location = new Point(32, 234);
            confirmPasswordLabel.Name = "confirmPasswordLabel";
            confirmPasswordLabel.Text = "Confirm password";

            // confirmPasswordField
            confirmPasswordField.Location = new Point(32, 254);
            confirmPasswordField.Name = "confirmPasswordField";
            confirmPasswordField.Size = new Size(296, 27);
            confirmPasswordField.TabIndex = 3;
            confirmPasswordField.PasswordChar = '●';

            // errorLabel
            errorLabel.AutoSize = true;
            errorLabel.ForeColor = Color.Red;
            errorLabel.Location = new Point(32, 290);
            errorLabel.Name = "errorLabel";
            errorLabel.Text = "";

            // cancelButton
            cancelButton.Location = new Point(160, 318);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(80, 28);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Cancel";
            cancelButton.Click += cancelButton_Click;

            // registerButton
            registerButton.Location = new Point(248, 318);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(80, 28);
            registerButton.TabIndex = 5;
            registerButton.Text = "Register";
            registerButton.Click += registerButton_Click;

            // RegisterDialog
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(360, 370);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegisterDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Register";

            Controls.Add(titleLabel);
            Controls.Add(subtitleLabel);
            Controls.Add(usernameLabel);
            Controls.Add(usernameField);
            Controls.Add(emailLabel);
            Controls.Add(emailField);
            Controls.Add(passwordLabel);
            Controls.Add(passwordField);
            Controls.Add(confirmPasswordLabel);
            Controls.Add(confirmPasswordField);
            Controls.Add(errorLabel);
            Controls.Add(cancelButton);
            Controls.Add(registerButton);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}