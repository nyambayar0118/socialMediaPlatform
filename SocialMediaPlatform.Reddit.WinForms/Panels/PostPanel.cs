using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Core.Infrastructure;
using SocialMediaPlatform.Reddit.Core.Domain.DTOs;
using SocialMediaPlatform.Reddit.Core.Infrastructure;
using SocialMediaPlatform.Reddit.WinForms.Dialogs;

namespace SocialMediaPlatform.Reddit.WinForms.Panels
{
    public partial class PostPanel : UserControl
    {
        private readonly Controller _controller;
        private readonly MainForm _mainForm;
        private readonly Session _session;
        private TimelinePostDTO? _currentPost;

        public PostPanel(Controller controller, MainForm mainForm)
        {
            _controller = controller;
            _mainForm = mainForm;
            _session = Session.GetInstance();
            InitializeComponent();
        }

        // ─── LOAD ─────────────────────────────────────────────────────

        public void Load(PostId postId)
        {
            try
            {
                var posts = _controller.GetFeed();
                _currentPost = posts.FirstOrDefault(p => p.Id.Value == postId.Value);
                if (_currentPost == null)
                {
                    _mainForm.ShowError("Post not found.");
                    _mainForm.NavigateBackToFeed();
                    return;
                }
                RenderPostDetail();
                LoadComments();
            }
            catch (Exception ex)
            {
                _mainForm.ShowError("Failed to load post: " + ex.Message);
                _mainForm.NavigateBackToFeed();
            }
        }

        // ─── POST DETAIL ──────────────────────────────────────────────

        private void RenderPostDetail()
        {
            if (_currentPost == null) return;

            string author = ResolveUsername(_currentPost.AuthorId);
            titleLabel.Text = _currentPost.Title;
            metaLabel.Text = $"u/{author}  ·  {_currentPost.Visibility}  ·  {_currentPost.CreatedAt:yyyy-MM-dd}";
            contentArea.Text = _currentPost.Content;

            RefreshPostReactions();

            bool isOwner = _currentPost.AuthorId.Value == _session.GetCurrentUser().Id.Value;
            editPostBtn.Visible = isOwner;
            deletePostBtn.Visible = isOwner;
        }

        private void RefreshPostReactions()
        {
            if (_currentPost == null) return;

            var counts = _controller.GetReactionCount(_currentPost.Id.Value, ReactionTargetType.Post);
            uint upvotes = counts.GetValueOrDefault("Upvote", 0u);
            uint downvotes = counts.GetValueOrDefault("Downvote", 0u);

            upvoteBtn.Text = $"△ {upvotes}";
            downvoteBtn.Text = $"▽ {downvotes}";
        }

        private void upvoteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _controller.React(_currentPost!.Id.Value, ReactionTargetType.Post, "Upvote");
                RefreshPostReactions();
            }
            catch (Exception ex) { _mainForm.ShowError(ex.Message); }
        }

        private void downvoteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _controller.React(_currentPost!.Id.Value, ReactionTargetType.Post, "Downvote");
                RefreshPostReactions();
            }
            catch (Exception ex) { _mainForm.ShowError(ex.Message); }
        }

        private void editPostBtn_Click(object sender, EventArgs e)
        {
            var dialog = new CreateEditPostDialog(_controller, _currentPost);
            dialog.ShowDialog(_mainForm);
            Load(_currentPost!.Id);
        }

        private void deletePostBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                $"Delete \"{_currentPost!.Title}\"?",
                "Delete post",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (result == DialogResult.Yes)
            {
                try
                {
                    _controller.DeletePost(_currentPost.Id);
                    _mainForm.NavigateBackToFeed();
                }
                catch (Exception ex) { _mainForm.ShowError(ex.Message); }
            }
        }

        private void backBtn_Click(object sender, EventArgs e) => _mainForm.NavigateBackToFeed();

        // ─── COMMENTS ─────────────────────────────────────────────────

        private void LoadComments()
        {
            commentsPanel.Controls.Clear();
            try
            {
                var comments = _controller.GetComments(_currentPost!.Id);
                if (comments.Count == 0)
                {
                    commentsPanel.Controls.Add(new Label
                    {
                        Text = "No comments yet. Add one below!",
                        ForeColor = SystemColors.GrayText,
                        AutoSize = true,
                        Margin = new Padding(8, 16, 0, 0)
                    });
                    return;
                }
                foreach (var comment in comments)
                    commentsPanel.Controls.Add(BuildCommentCard(comment));
            }
            catch (Exception ex) { _mainForm.ShowError("Failed to load comments: " + ex.Message); }
        }

        private Panel BuildCommentCard(CommentDTO comment)
        {
            string author = ResolveUsername(comment.AuthorId);

            var card = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = commentsPanel.ClientSize.Width - 20,
                Height = 110,
                Margin = new Padding(0, 0, 0, 6)
            };

            // author + date
            var authorLabel = new Label
            {
                Text = $"u/{author}",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };

            var dateLabel = new Label
            {
                Text = comment.CreatedAt.ToString("yyyy-MM-dd"),
                ForeColor = SystemColors.GrayText,
                AutoSize = true,
                Location = new Point(card.Width - 100, 10)
            };

            // content
            var contentLabel = new Label
            {
                Text = comment.Content,
                AutoSize = true,
                MaximumSize = new Size(card.Width - 20, 0),
                Location = new Point(10, 34)
            };

            // reactions
            var counts = _controller.GetReactionCount(comment.Id.Value, ReactionTargetType.Comment);
            uint upvotes = counts.GetValueOrDefault("Upvote", 0u);
            uint downvotes = counts.GetValueOrDefault("Downvote", 0u);

            var upBtn = new Button
            {
                Text = $"△ {upvotes}",
                Size = new Size(65, 24),
                Location = new Point(10, 76)
            };
            var downBtn = new Button
            {
                Text = $"▽ {downvotes}",
                Size = new Size(65, 24),
                Location = new Point(80, 76)
            };

            upBtn.Click += (s, e) => { _controller.React(comment.Id.Value, ReactionTargetType.Comment, "Upvote"); LoadComments(); };
            downBtn.Click += (s, e) => { _controller.React(comment.Id.Value, ReactionTargetType.Comment, "Downvote"); LoadComments(); };

            card.Controls.Add(authorLabel);
            card.Controls.Add(dateLabel);
            card.Controls.Add(contentLabel);
            card.Controls.Add(upBtn);
            card.Controls.Add(downBtn);

            // delete — only for comment author
            bool isAuthor = comment.AuthorId.Value == _session.GetCurrentUser().Id.Value;
            if (isAuthor)
            {
                var deleteBtn = new Button
                {
                    Text = "Delete",
                    Size = new Size(65, 24),
                    Location = new Point(card.Width - 80, 76)
                };
                deleteBtn.Click += (s, e) => HandleDeleteComment(comment.Id);
                card.Controls.Add(deleteBtn);
            }

            return card;
        }

        private void submitCommentBtn_Click(object sender, EventArgs e)
        {
            string content = commentInputArea.Text.Trim();
            if (string.IsNullOrEmpty(content))
            {
                _mainForm.ShowError("Comment cannot be empty");
                return;
            }
            try
            {
                _controller.AddComment(_currentPost!.Id, content);
                commentInputArea.Clear();
                LoadComments();
            }
            catch (Exception ex) { _mainForm.ShowError(ex.Message); }
        }

        private void HandleDeleteComment(CommentId commentId)
        {
            var result = MessageBox.Show(
                "Delete this comment?",
                "Delete comment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (result == DialogResult.Yes)
            {
                try
                {
                    _controller.DeleteComment(commentId);
                    LoadComments();
                }
                catch (Exception ex) { _mainForm.ShowError(ex.Message); }
            }
        }

        // ─── HELPERS ──────────────────────────────────────────────────

        private string ResolveUsername(UserId userId)
        {
            try { return _controller.GetUser(userId).Username; }
            catch { return "u/" + userId.Value; }
        }
    }
}