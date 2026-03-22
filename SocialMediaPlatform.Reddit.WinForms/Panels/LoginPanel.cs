using SocialMediaPlatform.Reddit.Core.Infrastructure;
using SocialMediaPlatform.Reddit.WinForms.Dialogs;

namespace SocialMediaPlatform.Reddit.WinForms.Panels
{
    public partial class LoginPanel : UserControl
    {
        private readonly Controller _controller;
        private readonly MainForm _mainForm;

        public LoginPanel(Controller controller, MainForm mainForm)
        {
            _controller = controller;
            _mainForm = mainForm;
            InitializeComponent();
        }

        // ─── ACTIONS ──────────────────────────────────────────────────

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameField.Text.Trim();
            string password = passwordField.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                errorLabel.Text = "Please fill in all fields";
                return;
            }

            try
            {
                _controller.Login(username, password);
                ClearFields();
                _mainForm.ShowFeedPanel();
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message;
                passwordField.Clear();
            }
        }

        private void registerLink_Click(object sender, EventArgs e)
        {
            var dialog = new RegisterDialog(_controller);
            dialog.ShowDialog(_mainForm);
        }

        // ─── HELPERS ──────────────────────────────────────────────────

        private void ClearFields()
        {
            usernameField.Clear();
            passwordField.Clear();
            errorLabel.Text = "";
        }
    }
}