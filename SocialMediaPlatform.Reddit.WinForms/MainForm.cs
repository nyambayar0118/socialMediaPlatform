// SocialMediaPlatform.Reddit.WinForms/MainForm.cs
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Core.Infrastructure;
using SocialMediaPlatform.Reddit.Infrastructure;
using SocialMediaPlatform.Reddit.WinForms.Dialogs;
using SocialMediaPlatform.Reddit.WinForms.Panels;

namespace SocialMediaPlatform.Reddit.WinForms
{
    public partial class MainForm : Form
    {
        private readonly AppConfig _appConfig;
        private readonly Session _session;

        private LoginPanel _loginPanel;
        private FeedPanel _feedPanel;
        private PostPanel _postPanel;

        public MainForm(AppConfig appConfig)
        {
            _appConfig = appConfig;
            _session = Session.GetInstance();
            InitializeComponent();
            InitializePanels();
            ShowLoginPanel();
        }

        private void InitializePanels()
        {
            _loginPanel = new LoginPanel(_appConfig.UserService, this);
            _feedPanel = new FeedPanel(
                _appConfig.UserService,
                _appConfig.PostService,
                _appConfig.ReactionService,
                this
            );
            _postPanel = new PostPanel(
                _appConfig.UserService,
                _appConfig.PostService,
                _appConfig.CommentService,
                _appConfig.ReactionService,
                this
            );
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
            var user = _session.GetCurrentUser();
            usernameLabel.Text = user.Username;
            RefreshToolbarAvatar();
            _feedPanel.Refresh();
            ShowPanel(_feedPanel);
        }

        public void RefreshToolbarAvatar()
        {
            var user = _session.GetCurrentUser();
            toolbarAvatar.Image?.Dispose();
            var avatar = AvatarHelper.Create(user.ProfilePicturePath, user.Username, 28);
            if (avatar != null)
            {
                toolbarAvatar.Image = avatar.Image;
                toolbarAvatar.Visible = true;
            }
            else
            {
                toolbarAvatar.Visible = false;
            }
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
            var dialog = new EditUserDialog(_appConfig.UserService);
            dialog.ShowDialog(this);
            if (_session.IsLoggedIn())
            {
                usernameLabel.Text = _session.GetCurrentUser().Username;
                RefreshToolbarAvatar();
            }
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
                _session.Logout();
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
            toolbarAvatar.Visible = visible;
        }

        public void ShowError(string message) =>
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ShowInfo(string message) =>
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}