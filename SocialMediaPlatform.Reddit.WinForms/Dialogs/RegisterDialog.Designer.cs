namespace SocialMediaPlatform.Reddit.WinForms.Dialogs
{
    partial class RegisterDialog
    {
        private System.ComponentModel.IContainer components = null;

        // CONTROLS
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
            components = new System.ComponentModel.Container();
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

            // ─── FORM ──────────────────────────────────────────────────
            Text = "Register";
            ClientSize = new Size(360, 420);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            AutoScaleMode = AutoScaleMode.Font;
            Padding = new Padding(32, 28, 32, 28);

            int left = 32;
            int width = 296;
            int y = 28;

            // TITLE
            titleLabel.Text = "Create account";
            titleLabel.Font = new Font(Font.FontFamily, 13f, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(left, y);
            y += 28;

            // SUBTITLE
            subtitleLabel.Text = "Fill in your details to register";
            subtitleLabel.Font = new Font(Font.FontFamily, 9f);
            subtitleLabel.ForeColor = SystemColors.GrayText;
            subtitleLabel.AutoSize = true;
            subtitleLabel.Location = new Point(left, y);
            y += 28;

            // USERNAME
            usernameLabel.Text = "Username";
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(left, y);
            y += 20;

            usernameField.Location = new Point(left, y);
            usernameField.Width = width;
            y += 30;

            // EMAIl
            emailLabel.Text = "Email";
            emailLabel.AutoSize = true;
            emailLabel.Location = new Point(left, y);
            y += 20;

            emailField.Location = new Point(left, y);
            emailField.Width = width;
            y += 30;

            // PASSWORD
            passwordLabel.Text = "Password";
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(left, y);
            y += 20;

            passwordField.Location = new Point(left, y);
            passwordField.Width = width;
            passwordField.PasswordChar = '●';
            y += 30;

            // CONFIRM PASSWORD
            confirmPasswordLabel.Text = "Confirm password";
            confirmPasswordLabel.AutoSize = true;
            confirmPasswordLabel.Location = new Point(left, y);
            y += 20;

            confirmPasswordField.Location = new Point(left, y);
            confirmPasswordField.Width = width;
            confirmPasswordField.PasswordChar = '●';
            y += 30;

            // ERROR LABEL
            errorLabel.Text = " ";
            errorLabel.ForeColor = Color.Red;
            errorLabel.AutoSize = true;
            errorLabel.Location = new Point(left, y);
            y += 24;

            // ACTION BUTTONS
            cancelButton.Text = "Cancel";
            cancelButton.Width = 80;
            cancelButton.Height = 28;
            cancelButton.Location = new Point(left + width - 80 - 88, y);
            cancelButton.Click += cancelButton_Click;

            registerButton.Text = "Register";
            registerButton.Width = 80;
            registerButton.Height = 28;
            registerButton.Location = new Point(left + width - 80, y);
            registerButton.Click += registerButton_Click;

            // BIND
            Controls.AddRange([
                titleLabel, subtitleLabel,
                usernameLabel, usernameField,
                emailLabel, emailField,
                passwordLabel, passwordField,
                confirmPasswordLabel, confirmPasswordField,
                errorLabel,
                cancelButton, registerButton
            ]);
        }
    }
}