using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace SocialMediaPlatform.Reddit.WinForms.CustomControls
{
    /// <summary>
    /// Хэрэглэгчийн хариу үйлдэл үзүүлэх Custom Control товч
    /// </summary>
    public partial class VoteReactionControl : UserControl
    {
        // Нийт хариу үйлдлийн тоо
        private int _voteCount;
        // Одоогийн хэрэглэгчийн дарсан хариу үйлдэл
        private int _currentUserVote;

        // Товч дээр хулганыг аваачсан эсэхийг хадгалах төлөв
        private bool _hoverUpButton;
        private bool _hoverDownButton;
        
        // Одоо харуулж буй тоон утга
        private float _displayedCount;
        // Хөдөлгөөний хугацааны объект
        private Timer _animationTimer;
        // Шилжиж очих тоон утга
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

        // Custom event
        public event EventHandler<VoteChangedEventArgs> OnVoteChanged;

        /// <summary>
        ///  Байгуулагч функц
        /// </summary>
        public VoteReactionControl()
        {
            // Custom control-ийн хэмжээг тохируулж өгнө
            this.DoubleBuffered = true;
            this.Width = 120;
            this.Height = 40;
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;

            // Харуулах тоон утгын анхны утгыг онооно
            _displayedCount = 0;
            _targetCount = 0;
            _currentUserVote = 0;

            // Хөдөлгөөний timer объектыг initialize хийж, өөрчлөгдөх interval-ийг тохируулна
            _animationTimer = new Timer();
            _animationTimer.Interval = 16;
            _animationTimer.Tick += AnimationTimer_Tick;
        }

        /// <summary>
        /// Custom control-ийг зурах метод
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            // Хэрэгдэхгүй төлөвтэй бол буцаана
            if (!this.Visible)
                return;

            // Grahpic объектыг үүсгээд тохируулна
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Control-ийн хэсгүүдийг агуулах тэгш өнцөгтүүдийг зурна
            Rectangle upRect = new Rectangle(8, 4, 32, 32);
            Rectangle countRect = new Rectangle(48, 4, 24, 32);
            Rectangle downRect = new Rectangle(80, 4, 32, 32);

            // Control-ийн бүрэлдэхүүн хэсгүүдийг зурна
            DrawButton(g, upRect, true);
            DrawCount(g, countRect);
            DrawButton(g, downRect, false);
        }

        /// <summary>
        /// Custom control-ийн Upvote болон Downvote хариу үйлдлийн товчийг зурах метод
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="isUpButton"></param>
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

        /// <summary>
        /// Custom control-ийн хариу үйлдлийн тоог зурах метод
        /// </summary>
        /// <param name="g"></param>
        /// <param name="countRect"></param>
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

        /// <summary>
        /// Custom control дээр хулганыг аваачих event handler метод
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// Custom control дээр хулганаар дарах event handler метод
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// Custom control-оос хулганыг холдуулах event handler метод
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            _hoverUpButton = false;
            _hoverDownButton = false;
            Invalidate();
        }

        /// <summary>
        /// Custom control-ийг хөдөлгөөнжүүлэх метод
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Custom control-ийг зурахаа болих үед ашигласан нөөцийг чөлөөлөх метод
        /// </summary>
        /// <param name="disposing"></param>
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