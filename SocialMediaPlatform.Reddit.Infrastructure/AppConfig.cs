using SocialMediaPlatform.Core.Infrastructure;
using SocialMediaPlatform.Reddit.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Core.Service;
using SocialMediaPlatform.Reddit.Core.Factories;
using SocialMediaPlatform.Reddit.Infrastructure.IdGenerator;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.File;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Repositories;
using SQLitePCL;

namespace SocialMediaPlatform.Reddit.Infrastructure
{
    /// <summary>
    /// Аппликейшний тохиргоо болон dependency-үүдийг удирдах класс
    /// </summary>
    public class AppConfig
    {
        public UserService UserService { get; private set; }
        public PostService PostService { get; private set; }
        public CommentService CommentService { get; private set; }
        public ReactionService ReactionService { get; private set; }
        public Session Session { get; private set; }

        /// <summary>
        /// Файл дээр суурилсан persistence-ээр аппликейшн үүсгэх
        /// </summary>
        /// <param name="configPath">Тохиргооны файлын зам</param>
        /// <exception cref="FileNotFoundException">Тохиргооны файл олдсонгүй</exception>
        public AppConfig(string configPath)
        {
            var config = ReadConfig(configPath);
            InitializeFileMode(config);
        }

        /// <summary>
        /// SQLite дээр суурилсан persistence-ээр аппликейшн үүсгэх
        /// </summary>
        /// <param name="connectionString">SQLite connection string утга</param>
        /// <param name="useSqlite">SQLite ашиглах эсэх</param>
        public AppConfig(string connectionString, bool useSqlite)
        {
            if (!useSqlite)
                throw new ArgumentException("Файл дээр ажиллах горимтой байгуулагчийг ашиглана уу");

            InitializeSqliteMode(connectionString);
        }

        /// <summary>Файл дээр суурилсан repository-аар эхлүүлэх</summary>
        private void InitializeFileMode(Dictionary<string, string> config)
        {
            // Repository-н объектуудыг үүсгэнэ
            var idRepoFile = new SequentialIdRepoFile(config["ids"]);
            var userRepoFile = new UserRepoFile(config["users"]);
            var postRepoFile = new PostRepoFile(config["posts"]);
            var commentRepoFile = new CommentRepoFile(config["comments"]);
            var reactionRepoFile = new ReactionRepoFile(config["reactions"]);

            // Id Generator объект үүсгэнэ
            var idGenerator = new SequentialIdGenerator(idRepoFile);

            // Factory объектуудыг үүсгэнэ
            var userFactory = new UserFactory();
            var postFactory = new PostFactory();

            // Service-үүдийг үүсгэнэ
            UserService = new UserService(userRepoFile, idGenerator, userFactory);
            PostService = new PostService(postRepoFile, idGenerator, postFactory);
            CommentService = new CommentService(commentRepoFile, idGenerator);
            ReactionService = new ReactionService(reactionRepoFile);

            // Session-г үүсгэнэ
            Session = Session.GetInstance();
        }

        /// <summary>SQLite дээр суурилсан repository-аар инициализ хийх</summary>
        private void InitializeSqliteMode(string connectionString)
        {
            var dbConnection = new DatabaseConnection(connectionString);

            // SQLite repositories
            var userRepo = new UserRepoSqlite(dbConnection.Connection);
            var postRepo = new PostRepoSqlite(dbConnection.Connection);
            var commentRepo = new CommentRepoSqlite(dbConnection.Connection);
            var reactionRepo = new ReactionRepoSqlite(dbConnection.Connection);
            var idRepo = new SequentialIdRepoSqlite(dbConnection.Connection);

            // Id Generator объектыг үүсгэнэ
            var idGenerator = new SequentialIdGenerator(idRepo);

            // Factory объектуудыг үүсгэнэ
            var userFactory = new UserFactory();
            var postFactory = new PostFactory();

            // Services объектуудыг үүсгэнэ
            UserService = new UserService(userRepo, idGenerator, userFactory);
            PostService = new PostService(postRepo, idGenerator, postFactory);
            CommentService = new CommentService(commentRepo, idGenerator);
            ReactionService = new ReactionService(reactionRepo);

            // Session-г үүсгэнэ
            Session = Session.GetInstance();
        }

        /// <summary>Тохиргооны файл уншиж key|value хэлбэрт хөрвүүлнэ</summary>
        /// <param name="path">Файлын зам</param>
        /// <returns>Тохиргооны утгуудын dictionary</returns>
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