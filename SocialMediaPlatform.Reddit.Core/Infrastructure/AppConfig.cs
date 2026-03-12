using SocialMediaPlatform.Core.Infrastructure;
using SocialMediaPlatform.Reddit.Core.Adapters;
using SocialMediaPlatform.Reddit.Core.Adapters.File;
using SocialMediaPlatform.Reddit.Core.Factory;
using SocialMediaPlatform.Reddit.Core.Service;

namespace SocialMediaPlatform.Reddit.Core.Infrastructure
{
    /// <summary>
    /// Аппликейшний тохиргоо болон dependency-үүдийг удирдах класс
    /// </summary>
    public class AppConfig
    {
        private readonly Controller _controller;

        /// <summary>
        /// Тохиргооны файлаас аппликейшн үүсгэх
        /// </summary>
        /// <param name="configPath">Тохиргооны файлын зам</param>
        /// <exception cref="FileNotFoundException">Тохиргооны файл олдсонгүй</exception>
        public AppConfig(string configPath)
        {
            var config = ReadConfig(configPath);

            // Adapters
            var idRepoFile = new SequentialIdRepoFile(config["ids"]);
            var userRepoFile = new UserRepoFile(config["users"]);
            var postRepoFile = new PostRepoFile(config["posts"]);
            var commentRepoFile = new CommentRepoFile(config["comments"]);
            var reactionRepoFile = new ReactionRepoFile(config["reactions"]);

            // Id Generator
            var idGenerator = new SequentialIdGenerator(idRepoFile);

            // Factories
            var userFactory = new UserFactory();
            var postFactory = new PostFactory();

            // Services
            var userService = new UserService(userRepoFile, idGenerator, userFactory);
            var postService = new PostService(postRepoFile, idGenerator, postFactory);
            var commentService = new CommentService(commentRepoFile, idGenerator);
            var reactionService = new ReactionService(reactionRepoFile);

            // Session
            var session = Session.GetInstance();

            // Controller
            _controller = new Controller(userService, postService, commentService, reactionService, session);
        }

        /// <summary>Controller авах</summary>
        /// <returns>Controller объект</returns>
        public Controller GetController()
        {
            return _controller;
        }

        /// <summary>Тохиргооны файл уншиж key|value хэлбэрт хөрвүүлэх</summary>
        /// <param name="path">Файлын зам</param>
        /// <returns>Тохиргооны утгуудын толь бичиг</returns>
        /// <exception cref="FileNotFoundException">Тохиргооны файл олдсонгүй</exception>
        /// <exception cref="FormatException">Тохиргооны файлын формат буруу</exception>
        private static Dictionary<string, string> ReadConfig(string path)
        {
            if (!System.IO.File.Exists(path))
                throw new FileNotFoundException($"Тохиргооны файл олдсонгүй: {path}");

            var config = new Dictionary<string, string>();

            foreach (var line in System.IO.File.ReadAllLines(path))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split('|');
                if (parts.Length != 2)
                    throw new FormatException($"Тохиргооны файлын формат буруу: '{line}'");
                config[parts[0].Trim()] = parts[1].Trim();
            }

            return config;
        }
    }
}