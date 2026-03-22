using SocialMediaPlatform.Reddit.Core.Infrastructure;

namespace SocialMediaPlatform.Reddit.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Dialogs.CreateEditPostDialog(null!));
            //// ─── CONFIG FILE SETUP ────────────────────────────────────
            //if (!File.Exists("config.txt"))
            //{
            //    Directory.CreateDirectory("data");

            //    File.WriteAllLines("config.txt", [
            //        "users|data/users.txt",
            //        "posts|data/posts.txt",
            //        "comments|data/comments.txt",
            //        "reactions|data/reactions.txt",
            //        "ids|data/ids.txt"
            //    ]);

            //    File.WriteAllText("data/users.txt", "");
            //    File.WriteAllText("data/posts.txt", "");
            //    File.WriteAllText("data/comments.txt", "");
            //    File.WriteAllText("data/reactions.txt", "");
            //    File.WriteAllLines("data/ids.txt", [
            //        "User|0",
            //        "Post|0",
            //        "Comment|0"
            //    ]);
            //}

            //// ─── BOOTSTRAP ────────────────────────────────────────────
            //var config = new AppConfig("config.txt");
            //var controller = config.GetController();

            //ApplicationConfiguration.Initialize();
            //Application.Run(new MainForm(controller));
        }
    }
}