using SocialMediaPlatform.Core.Infrastructure;
using SocialMediaPlatform.Reddit.Core.Infrastructure;

namespace SocialMediaPlatform.Reddit.WinForms.Dialogs
{
    public partial class EditUserDialog : Form
    {
        private readonly Controller _controller;
        private readonly Session _session;

        public EditUserDialog(Controller controller)
        {
            _controller = controller;
            _session = Session.GetInstance();
            InitializeComponent();
            LoadCurrentValues();
        }

        private void LoadCurrentValues()
        {
            var user = _session.GetCurrentUser();
            usernameField.Text = user.Username;
            emailField.Text = user.Email;
        }

        // ─── ACTIONS ──────────────────────────────────────────────────

        private void browseButton_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp";
            dialog.Title = "Select profile picture";

            if (dialog.ShowDialog() == DialogResult.OK)
                picturePathField.Text = dialog.FileName;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string username = usernameField.Text.Trim();
            string email = emailField.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
            {
                errorLabel.Text = "Please fill in all fields";
                return;
            }

            try
            {
                // save profile picture file if selected
                string picturePath = _session.GetCurrentUser().ProfilePicturePath ?? "";
                if (!string.IsNullOrEmpty(picturePathField.Text) && File.Exists(picturePathField.Text))
                {
                    Directory.CreateDirectory("profilepics");
                    string dest = Path.Combine("profilepics", _session.GetCurrentUser().Id.Value + Path.GetExtension(picturePathField.Text));
                    File.Copy(picturePathField.Text, dest, overwrite: true);
                    picturePath = dest;
                }

                var updated = _controller.EditUser(_session.GetCurrentUser().Id, username, email);
                _session.Login(updated);
                Close();
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e) => Close();
    }
}