using SocialMediaPlatform.Reddit.Infrastructure;

namespace SocialMediaPlatform.Reddit.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Initialize with file-based persistence (existing)
            //var appConfig = new AppConfig("config.txt");

            // OR initialize with SQLite when ready
            var appConfig = new AppConfig("Data Source=redditforms.db;", useSqlite: true);

            var mainForm = new MainForm(appConfig);
            Application.Run(mainForm);
        }
    }
}