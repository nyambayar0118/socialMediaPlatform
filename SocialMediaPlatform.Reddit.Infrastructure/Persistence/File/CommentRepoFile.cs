using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Comments;
using SocialMediaPlatform.Reddit.Core.Ports.Output;

namespace SocialMediaPlatform.Reddit.Infrastructure.Persistence.File
{
    /// <summary>
    /// Сэтгэгдлийн файл дээр суурилсан Repository адаптер
    /// </summary>
    public class CommentRepoFile : ICommentRepoPort
    {
        /// <summary>
        /// Ажиллах файл зам. Файл нь Comment объектыг мөр болгон хадгална (ID|AuthorID|PostID|Content|CreatedAt)
        /// </summary>
        private readonly string _filePath;

        /// <summary>
        /// Байгуулагч. Файл замыг авна. Файл байхгүй бол автоматаар үүсгэнэ.
        /// </summary>
        /// <param name="filePath">Файлын зам</param>
        public CommentRepoFile(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>Сэтгэгдэл хадгалах</summary>
        /// <param name="comment">Хадгалах сэтгэгдэл объект</param>
        public void Save(Comment comment)
        {
            var line = Serialize(comment);
            System.IO.File.AppendAllText(_filePath, line + Environment.NewLine);
        }

        /// <summary>ID-аар сэтгэгдэл хайх</summary>
        /// <param name="commentId">Хайх сэтгэгдлийн ID</param>
        public Comment FindById(CommentId commentId)
        {
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var comment = Deserialize(line);
                if (comment.Id.Value == commentId.Value)
                    return comment;
            }
            throw new KeyNotFoundException($"Comment ID {commentId.Value} not found");
        }

        /// <summary>Постын сэтгэгдлүүдийг авах</summary>
        /// <param name="postId">Постын ID</param>
        public List<Comment> FindByPost(PostId postId)
        {
            return System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(Deserialize)
                .Where(c => c.PostId.Value == postId.Value)
                .Cast<Comment>()
                .ToList();
        }

        /// <summary>Сэтгэгдэл устгах</summary>
        /// <param name="commentId">Устгах сэтгэгдлийн ID</param>
        public void Delete(CommentId commentId)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Where(line =>
                {
                    var parts = line.Split('|');
                    return uint.Parse(parts[0]) != commentId.Value;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Comment объектыг мөр болгох</summary>
        /// <param name="comment">Сериалчлах Comment объект</param>
        private static string Serialize(Comment comment)
        {
            if (comment is MainComment main)
                return $"{main.Id.Value}|{main.AuthorId.Value}|{main.PostId.Value}|{main.Content}|{main.CreatedAt:O}";

            throw new ArgumentException($"Undefined Comment type: {comment.GetType().Name}");
        }

        /// <summary>Мөрийг Comment объект болгох</summary>
        /// <param name="line">Десериалчлах мөр</param>
        private static MainComment Deserialize(string line)
        {
            var parts = line.Split('|');
            return new MainComment
            {
                Id = new CommentId { Value = uint.Parse(parts[0]) },
                AuthorId = new UserId { Value = uint.Parse(parts[1]) },
                PostId = new PostId { Value = uint.Parse(parts[2]) },
                Content = parts[3],
                CreatedAt = DateTime.Parse(parts[4])
            };
        }
    }
}