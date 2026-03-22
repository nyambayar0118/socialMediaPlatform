using SocialMediaPlatform.Reddit.Core.Infrastructure;

namespace SocialMediaPlatform.Reddit.WinForms.Dialogs
{
    public partial class RegisterDialog : Form
    {
        private readonly Controller _controller;

        public RegisterDialog(Controller controller)
        {
            _controller = controller;
            InitializeComponent();
        }

        // LISTENERS

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
                _controller.Register(username, email, password);
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