namespace SocialMediaPlatform.Reddit.WinForms.Dialogs
{
    partial class EditUserDialog
    {
        private System.ComponentModel.IContainer components = null;

        private Label titleLabel;
        private Label usernameLabel;
        private Label emailLabel;
        private Label pictureLabel;
        private TextBox usernameField;
        private TextBox emailField;
        private TextBox picturePathField;
        private Button browseButton;
        private Label errorLabel;
        private Button saveButton;
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
            usernameLabel = new Label();
            emailLabel = new Label();
            pictureLabel = new Label();
            usernameField = new TextBox();
            emailField = new TextBox();
            picturePathField = new TextBox();
            browseButton = new Button();
            errorLabel = new Label();
            saveButton = new Button();
            cancelButton = new Button();
            SuspendLayout();

            // titleLabel
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            titleLabel.Location = new Point(32, 28);
            titleLabel.Name = "titleLabel";
            titleLabel.Text = "Edit profile";

            // usernameLabel
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(32, 72);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Text = "Username";

            // usernameField
            usernameField.Location = new Point(32, 92);
            usernameField.Name = "usernameField";
            usernameField.Size = new Size(296, 27);
            usernameField.TabIndex = 0;

            // emailLabel
            emailLabel.AutoSize = true;
            emailLabel.Location = new Point(32, 130);
            emailLabel.Name = "emailLabel";
            emailLabel.Text = "Email";

            // emailField
            emailField.Location = new Point(32, 150);
            emailField.Name = "emailField";
            emailField.Size = new Size(296, 27);
            emailField.TabIndex = 1;

            // pictureLabel
            pictureLabel.AutoSize = true;
            pictureLabel.Location = new Point(32, 190);
            pictureLabel.Name = "pictureLabel";
            pictureLabel.Text = "Profile picture";

            // picturePathField
            picturePathField.Location = new Point(32, 210);
            picturePathField.Name = "picturePathField";
            picturePathField.Size = new Size(210, 27);
            picturePathField.TabIndex = 2;
            picturePathField.ReadOnly = true;

            // browseButton
            browseButton.Location = new Point(250, 210);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(80, 27);
            browseButton.TabIndex = 3;
            browseButton.Text = "Browse...";
            browseButton.Click += browseButton_Click;

            // errorLabel
            errorLabel.AutoSize = true;
            errorLabel.ForeColor = Color.Red;
            errorLabel.Location = new Point(32, 250);
            errorLabel.Name = "errorLabel";
            errorLabel.Text = "";

            // cancelButton
            cancelButton.Location = new Point(160, 278);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(80, 28);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Cancel";
            cancelButton.Click += cancelButton_Click;

            // saveButton
            saveButton.Location = new Point(248, 278);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(80, 28);
            saveButton.TabIndex = 5;
            saveButton.Text = "Save";
            saveButton.Click += saveButton_Click;

            // EditUserDialog
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(360, 330);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditUserDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit profile";

            Controls.Add(titleLabel);
            Controls.Add(usernameLabel);
            Controls.Add(usernameField);
            Controls.Add(emailLabel);
            Controls.Add(emailField);
            Controls.Add(pictureLabel);
            Controls.Add(picturePathField);
            Controls.Add(browseButton);
            Controls.Add(errorLabel);
            Controls.Add(cancelButton);
            Controls.Add(saveButton);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}