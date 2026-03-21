using SocialMediaPlatform.Reddit.Core.Infrastructure;

namespace SocialMediaPlatform.Reddit.WinForms
{
    public partial class MainForm : Form
    {
        private readonly Controller _controller;

        public MainForm(Controller controller)
        {
            _controller = controller;
            InitializeComponent();
        }
    }
}
