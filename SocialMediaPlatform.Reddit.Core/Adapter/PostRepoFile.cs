using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Posts;
using SocialMediaPlatform.Reddit.Core.Ports.Output;

namespace SocialMediaPlatform.Reddit.Core.Adapters.File
{
    /// <summary>
    /// Постын файл дээр суурилсан Repository адаптер
    /// </summary>
    public class PostRepoFile : IPostRepoPort
    {
        /// <summary>
        /// Ажиллах файлын зам. Файлд постуудыг мөр болгон хадгална (ID|AuthorID|Visibility|CreatedAt|Title|Content)
        /// </summary>
        private readonly string _filePath;

        /// <summary>
        /// Байгуулагч. Файл замыг авна. Файл байхгүй бол автоматаар үүсгэнэ.
        /// </summary>
        /// <param name="filePath">Файлын зам</param>
        public PostRepoFile(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>Пост хадгалах</summary>
        /// <param name="post">Хадгалах пост объект</param>
        public void Save(Post post)
        {
            var line = Serialize(post);
            System.IO.File.AppendAllText(_filePath, line + Environment.NewLine);
        }

        /// <summary>ID-аар пост хайх</summary>
        /// <param name="postId">Хайх постын ID</param>
        public Post FindById(PostId postId)
        {
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var post = Deserialize(line);
                if (post.Id.Value == postId.Value)
                    return post;
            }
            throw new KeyNotFoundException($"Post ID {postId.Value} not found");
        }

        /// <summary>Нийт постын жагсаалт авах</summary>
        public List<Post> FindAll()
        {
            return System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(Deserialize)
                .Cast<Post>()
                .ToList();
        }

        /// <summary>Постын мэдээлэл шинэчлэх</summary>
        /// <param name="post">Шинэчилсэн пост объект</param>
        public void Update(Post post)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var parts = line.Split('|');
                    return uint.Parse(parts[0]) == post.Id.Value
                        ? Serialize(post)
                        : line;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Пост устгах</summary>
        /// <param name="postId">Устгах постын ID</param>
        public void Delete(PostId postId)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Where(line =>
                {
                    var parts = line.Split('|');
                    return uint.Parse(parts[0]) != postId.Value;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Post объектыг мөр болгох</summary>
        /// <param name="post">Сериалчлах пост объект</param>
        private static string Serialize(Post post)
        {
            if (post is TimelinePost timeline)
                return $"{timeline.Id.Value}|{timeline.AuthorId.Value}|{timeline.Visibility}|{timeline.CreatedAt:O}|{timeline.Title}|{timeline.Content}";

            throw new ArgumentException($"Undefined Post type: {post.GetType().Name}");
        }

        /// <summary>Мөрийг Post объект болгох</summary>
        /// <param name="line">Буцааж объект болгох line</param>
        private static TimelinePost Deserialize(string line)
        {
            var parts = line.Split('|');
            return new TimelinePost
            {
                Id = new PostId { Value = uint.Parse(parts[0]) },
                AuthorId = new UserId { Value = uint.Parse(parts[1]) },
                Visibility = Enum.Parse<VisibilityType>(parts[2]),
                CreatedAt = DateTime.Parse(parts[3]),
                Title = parts[4],
                Content = parts[5]
            };
        }
    }
}