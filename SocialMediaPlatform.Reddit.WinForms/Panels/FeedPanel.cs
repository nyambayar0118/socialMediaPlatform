using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.DTOs;
using SocialMediaPlatform.Reddit.Core.Domain.Enum;
using SocialMediaPlatform.Reddit.Core.Infrastructure;
using SocialMediaPlatform.Reddit.WinForms.Dialogs;

namespace SocialMediaPlatform.Reddit.WinForms.Panels
{
    public partial class FeedPanel : UserControl
    {
        private readonly Controller _controller;
        private readonly MainForm _mainForm;
        private List<TimelinePostDTO> _allPosts = new();
        private List<UserDTO> _allUsers = new();
        private double _splitRatio = -1;

        public FeedPanel(Controller controller, MainForm mainForm)
        {
            _controller = controller;
            _mainForm = mainForm;
            InitializeComponent();

            splitContainer.Panel1MinSize = 180;
            splitContainer.Panel2MinSize = 400;

            splitContainer.FixedPanel = FixedPanel.None;

            splitContainer.Resize += SplitContainer_Resize;
        }


        private void SplitContainer_Resize(object sender, EventArgs e)
        {
            if (splitContainer.Width <= 0) return;

            // Capture ratio on first resize
            if (_splitRatio < 0)
                _splitRatio = (double)splitContainer.SplitterDistance / splitContainer.Width;

            int newDistance = (int)(splitContainer.Width * _splitRatio);

            // Clamp to min sizes so it never breaks
            newDistance = Math.Max(newDistance, splitContainer.Panel1MinSize);
            newDistance = Math.Min(newDistance, splitContainer.Width - splitContainer.Panel2MinSize);

            splitContainer.SplitterDistance = newDistance;
        }

        // ─── REFRESH ──────────────────────────────────────────────────

        public new void Refresh()
        {
            LoadUsers();
            LoadPosts();
        }

        // ─── USERS ────────────────────────────────────────────────────

        private void LoadUsers()
        {
            try
            {
                _allUsers = _controller.GetAllUsers();
                RenderUsers(_allUsers);
            }
            catch (Exception ex)
            {
                _mainForm.ShowError("Failed to load users: " + ex.Message);
            }
        }

        private void RenderUsers(List<UserDTO> users)
        {
            userListBox.Items.Clear();
            foreach (var user in users)
                userListBox.Items.Add(user);
        }

        private void userSearchField_TextChanged(object sender, EventArgs e)
        {
            string query = userSearchField.Text.Trim().ToLower();
            var filtered = _allUsers.Where(u => u.Username.ToLower().Contains(query)).ToList();
            RenderUsers(filtered);
        }

        private void userListBox_DoubleClick(object sender, EventArgs e)
        {
            if (userListBox.SelectedItem is UserDTO user)
            {
                var dialog = new UserProfileDialog(user);
                dialog.ShowDialog(_mainForm);
            }
        }

        // ─── POSTS ────────────────────────────────────────────────────

        private void LoadPosts()
        {
            try
            {
                _allPosts = _controller.GetFeed();
                RenderPosts(_allPosts);
            }
            catch (Exception ex)
            {
                _mainForm.ShowError("Failed to load feed: " + ex.Message);
            }
        }

        private void RenderPosts(List<TimelinePostDTO> posts)
        {
            postFeedPanel.Controls.Clear();

            if (posts.Count == 0)
            {
                var empty = new Label
                {
                    Text = "No posts yet. Be the first!",
                    ForeColor = SystemColors.GrayText,
                    AutoSize = true,
                    Margin = new Padding(8, 16, 0, 0)
                };
                postFeedPanel.Controls.Add(empty);
                return;
            }

            foreach (var post in posts)
                postFeedPanel.Controls.Add(BuildPostCard(post));
        }

        private Panel BuildPostCard(TimelinePostDTO post)
        {
            var currentUser = _controller.GetAllUsers()
                .FirstOrDefault(u => u.Id.Value == post.AuthorId.Value);
            string authorName = currentUser?.Username ?? "u/" + post.AuthorId.Value;

            var counts = _controller.GetReactionCount(post.Id.Value, ReactionTargetType.Post);
            uint upvotes = counts.GetValueOrDefault("Upvote", 0u);
            uint downvotes = counts.GetValueOrDefault("Downvote", 0u);

            var session = SocialMediaPlatform.Core.Infrastructure.Session.GetInstance();
            bool hasUpvoted = false;
            bool hasDownvoted = false;

            // ─── CARD ─────────────────────────────────────────────────
            var card = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = postFeedPanel.ClientSize.Width - 20,
                Height = 110,
                Margin = new Padding(0, 0, 0, 6),
                Cursor = Cursors.Hand,
                Tag = post
            };

            // title
            var titleLabel = new Label
            {
                Text = post.Title,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };

            // meta
            var metaLabel = new Label
            {
                Text = $"u/{authorName}  ·  {post.Visibility}",
                ForeColor = SystemColors.GrayText,
                AutoSize = true,
                Location = new Point(10, 34)
            };

            // upvote button
            var upvoteBtn = new Button
            {
                Text = $"△ {upvotes}",
                Location = new Point(10, 70),
                Size = new Size(70, 26),
                Tag = post
            };

            // downvote button
            var downvoteBtn = new Button
            {
                Text = $"▽ {downvotes}",
                Location = new Point(86, 70),
                Size = new Size(70, 26),
                Tag = post
            };

            upvoteBtn.Click += (s, e) => { _controller.React(post.Id.Value, ReactionTargetType.Post, "Upvote"); LoadPosts(); };
            downvoteBtn.Click += (s, e) => { _controller.React(post.Id.Value, ReactionTargetType.Post, "Downvote"); LoadPosts(); };

            // owner actions
            if (session.IsLoggedIn() && post.AuthorId.Value == session.GetCurrentUser().Id.Value)
            {
                var editBtn = new Button
                {
                    Text = "Edit",
                    Size = new Size(60, 26),
                    Location = new Point(card.Width - 140, 70)
                };
                var deleteBtn = new Button
                {
                    Text = "Delete",
                    Size = new Size(60, 26),
                    Location = new Point(card.Width - 74, 70)
                };

                editBtn.Click += (s, e) =>
                {
                    var dialog = new CreateEditPostDialog(_controller, post);
                    dialog.ShowDialog(_mainForm);
                    LoadPosts();
                };
                deleteBtn.Click += (s, e) => HandleDeletePost(post);

                card.Controls.Add(editBtn);
                card.Controls.Add(deleteBtn);
            }

            card.Controls.Add(titleLabel);
            card.Controls.Add(metaLabel);
            card.Controls.Add(upvoteBtn);
            card.Controls.Add(downvoteBtn);

            // click card to open post
            card.Click += (s, e) => _mainForm.ShowPostPanel(post.Id);
            titleLabel.Click += (s, e) => _mainForm.ShowPostPanel(post.Id);
            metaLabel.Click += (s, e) => _mainForm.ShowPostPanel(post.Id);

            return card;
        }

        private void postSearchField_TextChanged(object sender, EventArgs e)
        {
            string query = postSearchField.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(query))
                RenderPosts(_allPosts);
            else
                RenderPosts(_allPosts.Where(p => p.Title.ToLower().Contains(query)).ToList());
        }

        private void newPostButton_Click(object sender, EventArgs e)
        {
            var dialog = new CreateEditPostDialog(_controller);
            dialog.ShowDialog(_mainForm);
            LoadPosts();
        }

        // ─── HELPERS ──────────────────────────────────────────────────

        private void HandleDeletePost(TimelinePostDTO post)
        {
            var result = MessageBox.Show(
                $"Delete \"{post.Title}\"?",
                "Delete post",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (result == DialogResult.Yes)
            {
                try
                {
                    _controller.DeletePost(post.Id);
                    LoadPosts();
                }
                catch (Exception ex)
                {
                    _mainForm.ShowError(ex.Message);
                }
            }
        }
    }
}