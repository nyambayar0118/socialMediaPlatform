namespace SocialMediaPlatform.Reddit.WinForms.Panels
{
    partial class FeedPanel
    {
        private System.ComponentModel.IContainer components = null;

        private SplitContainer splitContainer;
        private Panel userSidePanel;
        private Label usersLabel;
        private TextBox userSearchField;
        private ListBox userListBox;
        private Panel feedSidePanel;
        private Panel feedHeaderPanel;
        private Label feedLabel;
        private TextBox postSearchField;
        private Button newPostButton;
        private FlowLayoutPanel postFeedPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            splitContainer = new SplitContainer();
            userSidePanel = new Panel();
            usersLabel = new Label();
            userSearchField = new TextBox();
            userListBox = new ListBox();
            feedSidePanel = new Panel();
            feedHeaderPanel = new Panel();
            feedLabel = new Label();
            postSearchField = new TextBox();
            newPostButton = new Button();
            postFeedPanel = new FlowLayoutPanel();
            SuspendLayout();

            // ─── SPLIT CONTAINER ───────────────────────────────────────
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.SplitterDistance = 240;
            splitContainer.SplitterWidth = 1;
            splitContainer.FixedPanel = FixedPanel.Panel1;
            splitContainer.Name = "splitContainer";

            // ─── USER SIDE ─────────────────────────────────────────────
            userSidePanel.Dock = DockStyle.Fill;
            userSidePanel.Name = "userSidePanel";

            usersLabel.AutoSize = true;
            usersLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            usersLabel.ForeColor = SystemColors.GrayText;
            usersLabel.Location = new Point(10, 10);
            usersLabel.Name = "usersLabel";
            usersLabel.Text = "USERS";

            userSearchField.Location = new Point(10, 32);
            userSearchField.Name = "userSearchField";
            userSearchField.PlaceholderText = "Search users...";
            userSearchField.Size = new Size(196, 27);
            userSearchField.TabIndex = 0;
            userSearchField.TextChanged += userSearchField_TextChanged;

            userListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            userListBox.Location = new Point(0, 66);
            userListBox.Name = "userListBox";
            userListBox.Size = new Size(240, 500);
            userListBox.BorderStyle = BorderStyle.None;
            userListBox.ItemHeight = 28;
            userListBox.DisplayMember = "Username";
            userListBox.DoubleClick += userListBox_DoubleClick;

            userSidePanel.Controls.Add(usersLabel);
            userSidePanel.Controls.Add(userSearchField);
            userSidePanel.Controls.Add(userListBox);

            splitContainer.Panel1.Controls.Add(userSidePanel);

            // ─── FEED HEADER ───────────────────────────────────────────
            feedHeaderPanel.Dock = DockStyle.Top;
            feedHeaderPanel.Height = 68;
            feedHeaderPanel.Name = "feedHeaderPanel";

            feedLabel.AutoSize = true;
            feedLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            feedLabel.ForeColor = SystemColors.GrayText;
            feedLabel.Location = new Point(12, 8);
            feedLabel.Name = "feedLabel";
            feedLabel.Text = "FEED";

            postSearchField.Anchor = AnchorStyles.None;
            postSearchField.Location = new Point(12, 34);
            postSearchField.Name = "postSearchField";
            postSearchField.PlaceholderText = "Search posts...";
            postSearchField.Size = new Size(480, 27);
            postSearchField.TabIndex = 1;
            postSearchField.TextChanged += postSearchField_TextChanged;

            newPostButton.Anchor = AnchorStyles.None;
            newPostButton.Location = new Point(500, 34);
            newPostButton.Name = "newPostButton";
            newPostButton.Size = new Size(100, 27);
            newPostButton.TabIndex = 2;
            newPostButton.Text = "+ New post";
            newPostButton.Click += newPostButton_Click;

            feedHeaderPanel.Controls.Add(feedLabel);
            feedHeaderPanel.Controls.Add(postSearchField);
            feedHeaderPanel.Controls.Add(newPostButton);

            // ─── POST FEED PANEL ───────────────────────────────────────
            postFeedPanel.AutoScroll = true;
            postFeedPanel.FlowDirection = FlowDirection.TopDown;
            postFeedPanel.WrapContents = false;
            postFeedPanel.Dock = DockStyle.Fill;
            postFeedPanel.Name = "postFeedPanel";
            postFeedPanel.Padding = new Padding(8);

            // ─── FEED SIDE ─────────────────────────────────────────────
            feedSidePanel.Dock = DockStyle.Fill;
            feedSidePanel.Name = "feedSidePanel";

            feedSidePanel.Controls.Add(postFeedPanel);   // Fill — add first
            feedSidePanel.Controls.Add(feedHeaderPanel); // Top  — add second

            splitContainer.Panel2.Controls.Add(feedSidePanel);

            // ─── FEED PANEL ────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "FeedPanel";
            Size = new Size(1100, 640);

            Controls.Add(splitContainer);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}