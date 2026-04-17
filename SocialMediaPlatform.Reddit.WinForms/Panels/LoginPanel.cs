// SocialMediaPlatform.Reddit.WinForms/Panels/LoginPanel.cs
using SocialMediaPlatform.Core.Infrastructure;
using SocialMediaPlatform.Reddit.Core.Service;
using SocialMediaPlatform.Reddit.WinForms.Dialogs;

namespace SocialMediaPlatform.Reddit.WinForms.Panels
{
    public partial class LoginPanel : UserControl
    {
        private readonly UserService _userService;
        private readonly Session _session;
        private readonly MainForm _mainForm;

        public LoginPanel(UserService userService, MainForm mainForm)
        {
            _userService = userService;
            _session = Session.GetInstance();
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
                var userDto = _userService.Login(username, password);
                _session.Login(userDto);
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
            var dialog = new RegisterDialog(_userService);
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