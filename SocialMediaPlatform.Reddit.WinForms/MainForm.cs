using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Core.Infrastructure;
using SocialMediaPlatform.Reddit.Core.Infrastructure;
using SocialMediaPlatform.Reddit.WinForms.Dialogs;
using SocialMediaPlatform.Reddit.WinForms.Panels;

namespace SocialMediaPlatform.Reddit.WinForms
{
    public partial class MainForm : Form
    {
        private readonly Controller _controller;
        private readonly Session _session;

        private LoginPanel _loginPanel;
        private FeedPanel _feedPanel;
        private PostPanel _postPanel;

        public MainForm(Controller controller)
        {
            _controller = controller;
            _session = Session.GetInstance();
            InitializeComponent();
            _loginPanel = new LoginPanel(_controller, this);
            _feedPanel = new FeedPanel(_controller, this);
            _postPanel = new PostPanel(_controller, this);
            ShowLoginPanel();
        }

        // ─── NAVIGATION ───────────────────────────────────────────────

        private void ShowPanel(UserControl panel)
        {
            contentPanel.Controls.Clear();
            panel.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(panel);
        }

        public void ShowLoginPanel()
        {
            SetToolbarVisible(false);
            ShowPanel(_loginPanel);
        }

        public void ShowFeedPanel()
        {
            SetToolbarVisible(true);
            usernameLabel.Text = _session.GetCurrentUser().Username;
            _feedPanel.Refresh();
            ShowPanel(_feedPanel);
        }

        public void ShowPostPanel(PostId postId)
        {
            _postPanel.Load(postId);
            ShowPanel(_postPanel);
        }

        public void NavigateBackToFeed()
        {
            _feedPanel.Refresh();
            ShowPanel(_feedPanel);
        }

        // ─── TOOLBAR ACTIONS ──────────────────────────────────────────

        private void editProfileButton_Click(object sender, EventArgs e)
        {
            var dialog = new EditUserDialog(_controller);
            dialog.ShowDialog(this);
            if (_session.IsLoggedIn())
                usernameLabel.Text = _session.GetCurrentUser().Username;
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to sign out?",
                "Sign out",
                MessageBoxButtons.YesNo
            );
            if (result == DialogResult.Yes)
            {
                _controller.Logout();
                ShowLoginPanel();
            }
        }

        private void toolbarPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(
                SystemPens.ControlDark,
                0, toolbarPanel.Height - 1,
                toolbarPanel.Width, toolbarPanel.Height - 1
            );
        }

        // ─── HELPERS ──────────────────────────────────────────────────

        private void SetToolbarVisible(bool visible)
        {
            usernameLabel.Visible = visible;
            editProfileButton.Visible = visible;
            logoutButton.Visible = visible;
        }

        public void ShowError(string message) =>
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ShowInfo(string message) =>
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}