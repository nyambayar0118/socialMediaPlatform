namespace SocialMediaPlatform.Reddit.WinForms.Panels
{
    partial class PostPanel
    {
        private System.ComponentModel.IContainer components = null;

        private Panel topBarPanel;
        private Button backBtn;
        private SplitContainer splitContainer;

        // post detail
        private Panel postDetailPanel;
        private Label titleLabel;
        private Label metaLabel;
        private RichTextBox contentArea;
        private Button upvoteBtn;
        private Button downvoteBtn;
        private Button editPostBtn;
        private Button deletePostBtn;

        // comments
        private Panel commentsSidePanel;
        private Label commentsHeaderLabel;
        private FlowLayoutPanel commentsPanel;

        // comment input
        private Panel commentInputPanel;
        private RichTextBox commentInputArea;
        private Button submitCommentBtn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            topBarPanel = new Panel();
            backBtn = new Button();
            splitContainer = new SplitContainer();
            postDetailPanel = new Panel();
            titleLabel = new Label();
            metaLabel = new Label();
            contentArea = new RichTextBox();
            upvoteBtn = new Button();
            downvoteBtn = new Button();
            editPostBtn = new Button();
            deletePostBtn = new Button();
            commentsSidePanel = new Panel();
            commentsHeaderLabel = new Label();
            commentsPanel = new FlowLayoutPanel();
            commentInputPanel = new Panel();
            commentInputArea = new RichTextBox();
            submitCommentBtn = new Button();
            SuspendLayout();

            // ─── TOP BAR ───────────────────────────────────────────────
            topBarPanel.Dock = DockStyle.Top;
            topBarPanel.Height = 40;
            topBarPanel.Name = "topBarPanel";

            backBtn.FlatStyle = FlatStyle.Flat;
            backBtn.FlatAppearance.BorderSize = 0;
            backBtn.ForeColor = Color.FromArgb(0x18, 0x5F, 0xA5);
            backBtn.Location = new Point(8, 8);
            backBtn.Name = "backBtn";
            backBtn.Size = new Size(80, 26);
            backBtn.Text = "← Back";
            backBtn.Click += backBtn_Click;

            topBarPanel.Controls.Add(backBtn);

            // ─── SPLIT CONTAINER ───────────────────────────────────────
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.SplitterDistance = 520;
            splitContainer.SplitterWidth = 1;
            splitContainer.Name = "splitContainer";

            // ─── POST DETAIL ───────────────────────────────────────────
            postDetailPanel.Dock = DockStyle.Fill;
            postDetailPanel.Name = "postDetailPanel";

            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Name = "titleLabel";
            titleLabel.Text = "";

            metaLabel.AutoSize = true;
            metaLabel.ForeColor = SystemColors.GrayText;
            metaLabel.Location = new Point(20, 50);
            metaLabel.Name = "metaLabel";
            metaLabel.Text = "";

            contentArea.BorderStyle = BorderStyle.None;
            contentArea.Location = new Point(20, 80);
            contentArea.Name = "contentArea";
            contentArea.ReadOnly = true;
            contentArea.ScrollBars = RichTextBoxScrollBars.Vertical;
            contentArea.Size = new Size(480, 200);
            contentArea.BackColor = SystemColors.Control;

            upvoteBtn.Location = new Point(20, 296);
            upvoteBtn.Name = "upvoteBtn";
            upvoteBtn.Size = new Size(70, 26);
            upvoteBtn.Text = "△ 0";
            upvoteBtn.Click += upvoteBtn_Click;

            downvoteBtn.Location = new Point(96, 296);
            downvoteBtn.Name = "downvoteBtn";
            downvoteBtn.Size = new Size(70, 26);
            downvoteBtn.Text = "▽ 0";
            downvoteBtn.Click += downvoteBtn_Click;

            editPostBtn.Location = new Point(20, 332);
            editPostBtn.Name = "editPostBtn";
            editPostBtn.Size = new Size(70, 26);
            editPostBtn.Text = "Edit";
            editPostBtn.Visible = false;
            editPostBtn.Click += editPostBtn_Click;

            deletePostBtn.Location = new Point(96, 332);
            deletePostBtn.Name = "deletePostBtn";
            deletePostBtn.Size = new Size(70, 26);
            deletePostBtn.Text = "Delete";
            deletePostBtn.Visible = false;
            deletePostBtn.Click += deletePostBtn_Click;

            postDetailPanel.Controls.Add(titleLabel);
            postDetailPanel.Controls.Add(metaLabel);
            postDetailPanel.Controls.Add(contentArea);
            postDetailPanel.Controls.Add(upvoteBtn);
            postDetailPanel.Controls.Add(downvoteBtn);
            postDetailPanel.Controls.Add(editPostBtn);
            postDetailPanel.Controls.Add(deletePostBtn);

            splitContainer.Panel1.Controls.Add(postDetailPanel);

            // ─── COMMENTS SIDE ─────────────────────────────────────────
            commentsSidePanel.Dock = DockStyle.Fill;
            commentsSidePanel.Name = "commentsSidePanel";

            commentsHeaderLabel.AutoSize = true;
            commentsHeaderLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            commentsHeaderLabel.ForeColor = SystemColors.GrayText;
            commentsHeaderLabel.Location = new Point(10, 10);
            commentsHeaderLabel.Name = "commentsHeaderLabel";
            commentsHeaderLabel.Text = "COMMENTS";

            commentsPanel.AutoScroll = true;
            commentsPanel.FlowDirection = FlowDirection.TopDown;
            commentsPanel.WrapContents = false;
            commentsPanel.Location = new Point(0, 36);
            commentsPanel.Name = "commentsPanel";
            commentsPanel.Size = new Size(560, 480);

            commentsSidePanel.Controls.Add(commentsHeaderLabel);
            commentsSidePanel.Controls.Add(commentsPanel);

            splitContainer.Panel2.Controls.Add(commentsSidePanel);

            // ─── COMMENT INPUT ─────────────────────────────────────────
            commentInputPanel.Dock = DockStyle.Bottom;
            commentInputPanel.Height = 90;
            commentInputPanel.Name = "commentInputPanel";

            commentInputArea.Location = new Point(10, 10);
            commentInputArea.Name = "commentInputArea";
            commentInputArea.Size = new Size(460, 68);
            commentInputArea.ScrollBars = RichTextBoxScrollBars.Vertical;
            commentInputArea.TabIndex = 0;

            submitCommentBtn.Location = new Point(480, 10);
            submitCommentBtn.Name = "submitCommentBtn";
            submitCommentBtn.Size = new Size(80, 68);
            submitCommentBtn.Text = "Submit";
            submitCommentBtn.TabIndex = 1;
            submitCommentBtn.Click += submitCommentBtn_Click;

            commentInputPanel.Controls.Add(commentInputArea);
            commentInputPanel.Controls.Add(submitCommentBtn);

            // ─── POST PANEL ────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "PostPanel";
            Size = new Size(1100, 640);

            Controls.Add(splitContainer);
            Controls.Add(commentInputPanel);
            Controls.Add(topBarPanel);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}