using SocialMediaPlatform.Core.Domain.DTO;

namespace SocialMediaPlatform.Reddit.WinForms.Dialogs
{
    public partial class UserProfileDialog : Form
    {
        private readonly UserDTO _user;

        public UserProfileDialog(UserDTO user)
        {
            _user = user;
            InitializeComponent();
            LoadProfile();
        }

        private void LoadProfile()
        {
            usernameValue.Text = _user.Username;
            emailValue.Text = _user.Email;
            joinedValue.Text = _user.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd");

            if (string.IsNullOrEmpty(_user.ProfilePicturePath) || !File.Exists(_user.ProfilePicturePath))
            {
                // show initials circle
                string initials = _user.Username.Length >= 2
                    ? _user.Username.Substring(0, 2).ToUpper()
                    : _user.Username.ToUpper();
                avatarLabel.Text = initials;
                avatarLabel.Visible = true;
                avatarPanel.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(0xB5, 0xD4, 0xF4)),
                        0, 0, avatarPanel.Width, avatarPanel.Height);
                };
            }
            else
            {
                // show actual profile picture
                avatarLabel.Visible = false;
                avatarPanel.BackgroundImage = Image.FromFile(_user.ProfilePicturePath);
                avatarPanel.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void closeButton_Click(object sender, EventArgs e) => Close();

        private void avatarLabel_Click(object sender, EventArgs e)
        {

        }
    }
}