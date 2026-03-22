using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.DTOs;
using SocialMediaPlatform.Reddit.Core.Infrastructure;

namespace SocialMediaPlatform.Reddit.WinForms.Dialogs
{
    public partial class CreateEditPostDialog : Form
    {
        private readonly Controller _controller;
        private readonly TimelinePostDTO? _existingPost;

        public CreateEditPostDialog(Controller controller, TimelinePostDTO? existingPost = null)
        {
            _controller = controller;
            _existingPost = existingPost;
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            Text = _existingPost == null ? "New post" : "Edit post";
            submitButton.Text = _existingPost == null ? "Post" : "Save";

            if (_existingPost != null)
            {
                titleField.Text = _existingPost.Title;
                contentArea.Text = _existingPost.Content;
            }
        }

        // ─── ACTIONS ──────────────────────────────────────────────────

        private void submitButton_Click(object sender, EventArgs e)
        {
            string title = titleField.Text.Trim();
            string content = contentArea.Text.Trim();

            if (string.IsNullOrEmpty(title))
            {
                errorLabel.Text = "Title cannot be empty";
                return;
            }

            if (string.IsNullOrEmpty(content))
            {
                errorLabel.Text = "Content cannot be empty";
                return;
            }

            try
            {
                if (_existingPost == null)
                    _controller.CreatePost(title, content);
                else
                    _controller.EditPost(_existingPost.Id, title, content);

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