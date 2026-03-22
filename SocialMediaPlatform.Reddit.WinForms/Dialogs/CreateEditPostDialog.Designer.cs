namespace SocialMediaPlatform.Reddit.WinForms.Dialogs
{
    partial class CreateEditPostDialog
    {
        private System.ComponentModel.IContainer components = null;

        private Label titleLabel;
        private Label contentLabel;
        private TextBox titleField;
        private RichTextBox contentArea;
        private Label errorLabel;
        private Button cancelButton;
        private Button submitButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            titleLabel = new Label();
            contentLabel = new Label();
            titleField = new TextBox();
            contentArea = new RichTextBox();
            errorLabel = new Label();
            cancelButton = new Button();
            submitButton = new Button();
            SuspendLayout();

            // titleLabel
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(28, 24);
            titleLabel.Name = "titleLabel";
            titleLabel.Text = "Title";

            // titleField
            titleField.Location = new Point(28, 48);
            titleField.Name = "titleField";
            titleField.Size = new Size(464, 27);
            titleField.TabIndex = 0;

            // contentLabel
            contentLabel.AutoSize = true;
            contentLabel.Location = new Point(28, 90);
            contentLabel.Name = "contentLabel";
            contentLabel.Text = "Content";

            // contentArea
            contentArea.Location = new Point(28, 114);
            contentArea.Name = "contentArea";
            contentArea.Size = new Size(464, 160);
            contentArea.ScrollBars = RichTextBoxScrollBars.Vertical;
            contentArea.TabIndex = 1;

            // errorLabel
            errorLabel.AutoSize = true;
            errorLabel.ForeColor = Color.Red;
            errorLabel.Location = new Point(28, 286);
            errorLabel.Name = "errorLabel";
            errorLabel.Text = "";

            // cancelButton
            cancelButton.Location = new Point(324, 318);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(80, 28);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.Click += cancelButton_Click;

            // submitButton
            submitButton.Location = new Point(412, 318);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(80, 28);
            submitButton.TabIndex = 3;
            submitButton.Text = "Post";
            submitButton.Click += submitButton_Click;

            // CreateEditPostDialog
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(520, 370);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CreateEditPostDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "New post";

            Controls.Add(titleLabel);
            Controls.Add(titleField);
            Controls.Add(contentLabel);
            Controls.Add(contentArea);
            Controls.Add(errorLabel);
            Controls.Add(cancelButton);
            Controls.Add(submitButton);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}