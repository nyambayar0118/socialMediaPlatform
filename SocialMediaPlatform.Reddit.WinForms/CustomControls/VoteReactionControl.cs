using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace SocialMediaPlatform.Reddit.WinForms.CustomControls
{
    public partial class VoteReactionControl : UserControl
    {
        private int _voteCount;
        private int _currentUserVote;
        private bool _hoverUpButton;
        private bool _hoverDownButton;
        private float _displayedCount;
        private Timer _animationTimer;
        private int _targetCount;

        public int VoteCount
        {
            get => _voteCount;
            set
            {
                if (_voteCount != value)
                {
                    _targetCount = value;
                    _animationTimer?.Start();
                    _voteCount = value;
                    Invalidate();
                }
            }
        }

        public int CurrentUserVote
        {
            get => _currentUserVote;
            set
            {
                if (_currentUserVote != value)
                {
                    _currentUserVote = value;
                    Invalidate();
                }
            }
        }

        public event EventHandler<VoteChangedEventArgs> OnVoteChanged;

        public VoteReactionControl()
        {
            this.DoubleBuffered = true;
            this.Width = 120;
            this.Height = 40;
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;

            _displayedCount = 0;
            _targetCount = 0;
            _currentUserVote = 0;

            _animationTimer = new Timer();
            _animationTimer.Interval = 16;
            _animationTimer.Tick += AnimationTimer_Tick;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (!this.Visible)
                return;

            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle upRect = new Rectangle(8, 4, 32, 32);
            Rectangle countRect = new Rectangle(48, 4, 24, 32);
            Rectangle downRect = new Rectangle(80, 4, 32, 32);

            DrawButton(g, upRect, true);
            DrawCount(g, countRect);
            DrawButton(g, downRect, false);
        }

        private void DrawButton(Graphics g, Rectangle rect, bool isUpButton)
        {
            Color buttonColor;
            Color borderColor;

            if (isUpButton)
            {
                if (_currentUserVote == 1)
                {
                    buttonColor = Color.FromArgb(34, 197, 94);
                    borderColor = Color.FromArgb(34, 197, 94);
                }
                else
                {
                    if (_hoverUpButton)
                        buttonColor = Color.FromArgb(220, 220, 220);
                    else
                        buttonColor = Color.White;

                    borderColor = Color.FromArgb(180, 180, 180);
                }
            }
            else
            {
                if (_currentUserVote == -1)
                {
                    buttonColor = Color.FromArgb(239, 68, 68);
                    borderColor = Color.FromArgb(239, 68, 68);
                }
                else
                {
                    if (_hoverDownButton)
                        buttonColor = Color.FromArgb(220, 220, 220);
                    else
                        buttonColor = Color.White;

                    borderColor = Color.FromArgb(180, 180, 180);
                }
            }

            using (Brush bgBrush = new SolidBrush(buttonColor))
            {
                g.FillRectangle(bgBrush, rect);
            }

            using (Pen borderPen = new Pen(borderColor, 1))
            {
                g.DrawRectangle(borderPen, rect);
            }

            string arrow = isUpButton ? "▲" : "▼";
            Color textColor;

            if (isUpButton && _currentUserVote == 1)
                textColor = Color.White;
            else if (!isUpButton && _currentUserVote == -1)
                textColor = Color.White;
            else
                textColor = Color.FromArgb(51, 51, 51);

            using (Font font = new Font("Arial", 14, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(textColor))
            {
                SizeF textSize = g.MeasureString(arrow, font);
                float x = rect.X + (rect.Width - textSize.Width) / 2;
                float y = rect.Y + (rect.Height - textSize.Height) / 2;

                g.DrawString(arrow, font, textBrush, x, y);
            }
        }

        private void DrawCount(Graphics g, Rectangle countRect)
        {
            Color countColor;

            if (_currentUserVote == 1)
                countColor = Color.FromArgb(34, 197, 94);
            else if (_currentUserVote == -1)
                countColor = Color.FromArgb(239, 68, 68);
            else
                countColor = Color.FromArgb(180, 180, 180);

            string countText = ((int)_displayedCount).ToString();

            using (Font font = new Font("Arial", 13, FontStyle.Bold))
            using (Brush countBrush = new SolidBrush(countColor))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                g.DrawString(countText, font, countBrush,
                    countRect.Left + countRect.Width / 2f,
                    countRect.Top + countRect.Height / 2f,
                    sf);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Rectangle upRect = new Rectangle(8, 4, 32, 32);
            Rectangle downRect = new Rectangle(80, 4, 32, 32);

            bool wasHoverUp = _hoverUpButton;
            bool wasHoverDown = _hoverDownButton;

            _hoverUpButton = upRect.Contains(e.Location);
            _hoverDownButton = downRect.Contains(e.Location);

            if (wasHoverUp != _hoverUpButton || wasHoverDown != _hoverDownButton)
            {
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Rectangle upRect = new Rectangle(8, 4, 32, 32);
            Rectangle downRect = new Rectangle(80, 4, 32, 32);

            if (upRect.Contains(e.Location))
            {
                int newVote = _currentUserVote == 1 ? 0 : 1;
                int newCount = _voteCount + (newVote - _currentUserVote);

                CurrentUserVote = newVote;
                VoteCount = newCount;

                OnVoteChanged?.Invoke(this, new VoteChangedEventArgs
                {
                    VoteType = newVote,
                    NewVoteCount = newCount
                });
            }
            else if (downRect.Contains(e.Location))
            {
                int newVote = _currentUserVote == -1 ? 0 : -1;
                int newCount = _voteCount + (newVote - _currentUserVote);

                CurrentUserVote = newVote;
                VoteCount = newCount;

                OnVoteChanged?.Invoke(this, new VoteChangedEventArgs
                {
                    VoteType = newVote,
                    NewVoteCount = newCount
                });
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            _hoverUpButton = false;
            _hoverDownButton = false;
            Invalidate();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (Math.Abs(_displayedCount - _targetCount) > 0.01f)
            {
                float diff = _targetCount - _displayedCount;
                _displayedCount += diff * 0.1f;

                Invalidate();
            }
            else
            {
                _displayedCount = _targetCount;
                _animationTimer.Stop();
                Invalidate();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animationTimer?.Stop();
                _animationTimer?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}