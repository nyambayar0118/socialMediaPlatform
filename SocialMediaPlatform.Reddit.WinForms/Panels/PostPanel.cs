// SocialMediaPlatform.Reddit.WinForms/Panels/PostPanel.cs
using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Core.Infrastructure;
using SocialMediaPlatform.Reddit.Core.Domain.DTOs;
using SocialMediaPlatform.Reddit.Core.Service;
using SocialMediaPlatform.Reddit.WinForms.Dialogs;

namespace SocialMediaPlatform.Reddit.WinForms.Panels
{
    public partial class PostPanel : UserControl
    {
        private readonly UserService _userService;
        private readonly PostService _postService;
        private readonly CommentService _commentService;
        private readonly ReactionService _reactionService;
        private readonly Session _session;
        private readonly MainForm _mainForm;
        private TimelinePostDTO? _currentPost;
        private double _splitRatio = -1;

        public PostPanel(UserService userService, PostService postService, CommentService commentService, ReactionService reactionService, MainForm mainForm)
        {
            _userService = userService;
            _postService = postService;
            _commentService = commentService;
            _reactionService = reactionService;
            _session = Session.GetInstance();
            _mainForm = mainForm;
            InitializeComponent();

            splitContainer.Panel1MinSize = 300;
            splitContainer.Panel2MinSize = 300;

            splitContainer.Resize += SplitContainer_Resize;
            splitContainer.SplitterMoved += (s, e) =>
            {
                if (splitContainer.Width > 0)
                    _splitRatio = (double)splitContainer.SplitterDistance / splitContainer.Width;
            };
        }

        private void SplitContainer_Resize(object sender, EventArgs e)
        {
            if (splitContainer.Width <= 0) return;

            if (_splitRatio < 0)
                _splitRatio = (double)splitContainer.SplitterDistance / splitContainer.Width;

            int newDistance = (int)(splitContainer.Width * _splitRatio);
            newDistance = Math.Max(newDistance, splitContainer.Panel1MinSize);
            newDistance = Math.Min(newDistance, splitContainer.Width - splitContainer.Panel2MinSize);

            splitContainer.SplitterDistance = newDistance;
        }

        // ─── LOAD ─────────────────────────────────────────────────────

        public void Load(PostId postId)
        {
            try
            {
                var posts = _postService.GetFeed();
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

            var counts = _reactionService.GetReactionCount(_currentPost.Id.Value, ReactionTargetType.Post);
            uint upvotes = counts.GetValueOrDefault("Upvote", 0u);
            uint downvotes = counts.GetValueOrDefault("Downvote", 0u);

            upvoteBtn.Text = $"△ {upvotes}";
            downvoteBtn.Text = $"▽ {downvotes}";
        }

        private void upvoteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _reactionService.React(_currentPost!.Id.Value, ReactionTargetType.Post, _session.GetCurrentUser().Id, "Upvote");
                RefreshPostReactions();
            }
            catch (Exception ex) { _mainForm.ShowError(ex.Message); }
        }

        private void downvoteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _reactionService.React(_currentPost!.Id.Value, ReactionTargetType.Post, _session.GetCurrentUser().Id, "Downvote");
                RefreshPostReactions();
            }
            catch (Exception ex) { _mainForm.ShowError(ex.Message); }
        }

        private void editPostBtn_Click(object sender, EventArgs e)
        {
            var dialog = new CreateEditPostDialog(_postService, _session, _currentPost);
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
                    _postService.DeletePost(_currentPost.Id);
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
                var comments = _commentService.GetComments(_currentPost!.Id);
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
            var counts = _reactionService.GetReactionCount(comment.Id.Value, ReactionTargetType.Comment);
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

            upBtn.Click += (s, e) => { _reactionService.React(comment.Id.Value, ReactionTargetType.Comment, _session.GetCurrentUser().Id, "Upvote"); LoadComments(); };
            downBtn.Click += (s, e) => { _reactionService.React(comment.Id.Value, ReactionTargetType.Comment, _session.GetCurrentUser().Id, "Downvote"); LoadComments(); };

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
                _commentService.AddComment(_currentPost!.Id, _session.GetCurrentUser().Id, content);
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
                    _commentService.DeleteComment(commentId);
                    LoadComments();
                }
                catch (Exception ex) { _mainForm.ShowError(ex.Message); }
            }
        }

        // ─── HELPERS ──────────────────────────────────────────────────

        private string ResolveUsername(UserId userId)
        {
            try { return _userService.GetUser(userId).Username; }
            catch { return "u/" + userId.Value; }
        }
    }
}