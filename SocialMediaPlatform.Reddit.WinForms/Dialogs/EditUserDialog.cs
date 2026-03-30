using SocialMediaPlatform.Core.Infrastructure;
using SocialMediaPlatform.Reddit.Core.Infrastructure;
using ProfilePictureProcessor;
using Processor = ProfilePictureProcessor.ProfilePictureProcessor;

namespace SocialMediaPlatform.Reddit.WinForms.Dialogs;

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
                string dest = Path.Combine("profilepics", _session.GetCurrentUser().Id.Value + ".png");
                var result = Processor.Process(picturePathField.Text, dest);

                if (!result.Success)
                {
                    errorLabel.Text = result.ErrorMessage;
                    return;
                }

                picturePath = result.OutputPath!;
            }

            var updated = _controller.EditUser(_session.GetCurrentUser().Id, username, email, picturePath);
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