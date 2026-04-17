// SocialMediaPlatform.Reddit.WinForms/Dialogs/RegisterDialog.cs
using SocialMediaPlatform.Core.Infrastructure;
using SocialMediaPlatform.Reddit.Core.Service;

namespace SocialMediaPlatform.Reddit.WinForms.Dialogs
{
    public partial class RegisterDialog : Form
    {
        private readonly UserService _userService;
        private readonly Session _session;

        public RegisterDialog(UserService userService)
        {
            _userService = userService;
            _session = Session.GetInstance();
            InitializeComponent();
        }

        // ─── ACTIONS ──────────────────────────────────────────────────

        private void registerButton_Click(object sender, EventArgs e)
        {
            string username = usernameField.Text.Trim();
            string email = emailField.Text.Trim();
            string password = passwordField.Text;
            string confirmPassword = confirmPasswordField.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                errorLabel.Text = "Please fill in all fields";
                return;
            }

            if (password != confirmPassword)
            {
                errorLabel.Text = "Passwords do not match";
                passwordField.Clear();
                confirmPasswordField.Clear();
                return;
            }

            if (password.Length < 8)
            {
                errorLabel.Text = "Password must be at least 8 characters";
                return;
            }

            try
            {
                var user = _userService.Register(username, email, password);
                _session.Login(user);

                MessageBox.Show(
                    "Account created! You can now sign in.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
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