using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Infrastructure;

if (!File.Exists("config.txt"))
{
    Directory.CreateDirectory("data");

    File.WriteAllLines("config.txt", [
        "users|data/users.txt",
        "posts|data/posts.txt",
        "comments|data/comments.txt",
        "reactions|data/reactions.txt",
        "ids|data/ids.txt"
    ]);

    File.WriteAllText("data/users.txt", "");
    File.WriteAllText("data/posts.txt", "");
    File.WriteAllText("data/comments.txt", "");
    File.WriteAllText("data/reactions.txt", "");
    File.WriteAllLines("data/ids.txt", [
        "User|0",
        "Post|0",
        "Comment|0"
    ]);

    Console.WriteLine("Config and data files created.");
}

var config = new AppConfig("config.txt");
var controller = config.GetController();

Console.WriteLine("═══════════════════════════════════");
Console.WriteLine("   SocialMediaPlatform - Reddit");
Console.WriteLine("═══════════════════════════════════");

while (true)
{
    Console.WriteLine("\n── Main Menu ──");
    Console.WriteLine("1. Register");
    Console.WriteLine("2. Login");
    Console.WriteLine("3. Exit");
    Console.Write("Choice: ");

    var choice = Console.ReadLine();

    if (choice == "1") Register(controller);
    else if (choice == "2") Login(controller);
    else if (choice == "3") { Console.WriteLine("Exited."); break; }
    else Console.WriteLine("Invalid choice.");
}

static void Register(Controller controller)
{
    Console.Write("Username: ");
    var username = Console.ReadLine()!;
    Console.Write("Email: ");
    var email = Console.ReadLine()!;
    Console.Write("Password: ");
    var password = Console.ReadLine()!;

    try
    {
        controller.Register(username, email, password);
        Console.WriteLine($"Registration successful. Welcome to Reddit, {username}!");
        LoggedInMenu(controller);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void Login(Controller controller)
{
    Console.Write("Username: ");
    var username = Console.ReadLine()!;
    Console.Write("Password: ");
    var password = Console.ReadLine()!;

    try
    {
        controller.Login(username, password);
        Console.WriteLine($"Logged in. Welcome back, {username}!");
        LoggedInMenu(controller);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void LoggedInMenu(Controller controller)
{
    while (true)
    {
        Console.WriteLine("\n── Feed ──");
        Console.WriteLine("1. Create Post");
        Console.WriteLine("2. View Feed");
        Console.WriteLine("3. View Post & Comments");
        Console.WriteLine("4. React to Post");
        Console.WriteLine("5. Add Comment");
        Console.WriteLine("0. Logout");
        Console.Write("Choice: ");

        var choice = Console.ReadLine();

        if (choice == "1") CreatePost(controller);
        else if (choice == "2") ViewFeed(controller);
        else if (choice == "3") ViewPost(controller);
        else if (choice == "4") ReactToPost(controller);
        else if (choice == "5") AddComment(controller);
        else if (choice == "0") { controller.Logout(); Console.WriteLine("Logged out."); break; }
        else Console.WriteLine("Invalid choice.");
    }
}

static void CreatePost(Controller controller)
{
    Console.Write("Title: ");
    var title = Console.ReadLine()!;
    Console.Write("Content: ");
    var content = Console.ReadLine()!;

    try
    {
        var post = controller.CreatePost(title, content);
        Console.WriteLine($"Post created successfully. ID: {post.Id.Value}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void ViewFeed(Controller controller)
{
    try
    {
        var posts = controller.GetFeed();
        if (posts.Count == 0)
        {
            Console.WriteLine("No posts found.");
            return;
        }
        Console.WriteLine("\n── Feed ──");
        foreach (var post in posts)
            Console.WriteLine($"[{post.Id.Value}] {post.Title} by AuthorId:{post.AuthorId.Value} | {post.CreatedAt:yyyy-MM-dd HH:mm}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void ViewPost(Controller controller)
{
    Console.Write("Post ID: ");
    var input = Console.ReadLine()!;

    try
    {
        var postId = new PostId { Value = uint.Parse(input) };
        var post = controller.GetFeed().FirstOrDefault(p => p.Id.Value == postId.Value);
        if (post == null) { Console.WriteLine("Post not found."); return; }

        Console.WriteLine($"\n── Post [{post.Id.Value}] ──");
        Console.WriteLine($"Title   : {post.Title}");
        Console.WriteLine($"Content : {post.Content}");
        Console.WriteLine($"Posted  : {post.CreatedAt:yyyy-MM-dd HH:mm}");

        var reactions = controller.GetReactionCount(post.Id.Value, ReactionTargetType.Post);
        var upvotes = reactions.GetValueOrDefault("Upvote", 0u);
        var downvotes = reactions.GetValueOrDefault("Downvote", 0u);
        Console.WriteLine($"Votes   : ▲ {upvotes}  ▼ {downvotes}");

        var comments = controller.GetComments(postId);
        Console.WriteLine($"\n── Comments ({comments.Count}) ──");
        if (comments.Count == 0)
            Console.WriteLine("No comments yet.");
        else
            foreach (var comment in comments)
                Console.WriteLine($"[{comment.Id.Value}] AuthorId:{comment.AuthorId.Value} — {comment.Content}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void ReactToPost(Controller controller)
{
    Console.Write("Post ID: ");
    var input = Console.ReadLine()!;
    Console.WriteLine("1. Upvote");
    Console.WriteLine("2. Downvote");
    Console.Write("Choice: ");
    var choice = Console.ReadLine();

    try
    {
        var postId = new PostId { Value = uint.Parse(input) };
        var reactionType = choice == "1" ? "Upvote" : "Downvote";
        controller.React(postId.Value, ReactionTargetType.Post, reactionType);
        Console.WriteLine("Reaction saved.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void AddComment(Controller controller)
{
    Console.Write("Post ID: ");
    var input = Console.ReadLine()!;
    Console.Write("Content: ");
    var content = Console.ReadLine()!;

    try
    {
        var postId = new PostId { Value = uint.Parse(input) };
        var comment = controller.AddComment(postId, content);
        Console.WriteLine($"Comment added successfully. ID: {comment.Id.Value}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}