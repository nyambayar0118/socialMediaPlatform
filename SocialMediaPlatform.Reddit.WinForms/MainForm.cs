using SocialMediaPlatform.Reddit.Core.Infrastructure;
using SocialMediaPlatform.Reddit.WinForms.Panels;
using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Reddit.WinForms
{
    public partial class MainForm : Form
    {
        private readonly Controller _controller;
        private LoginPanel _loginPanel;

        public MainForm(Controller controller)
        {
            _controller = controller;
            InitializeComponent();
            _loginPanel = new LoginPanel(_controller, this);
            ShowLoginPanel();
        }

        public void ShowLoginPanel()
        {
            contentPanel.Controls.Clear();
            _loginPanel.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(_loginPanel);
        }

        public void ShowFeedPanel() { }    // implement later
        public void ShowPostPanel(PostId postId) { }  // implement later
        public void NavigateBackToFeed() { }   // implement later

        public void ShowError(string message) =>
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ShowInfo(string message) =>
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}