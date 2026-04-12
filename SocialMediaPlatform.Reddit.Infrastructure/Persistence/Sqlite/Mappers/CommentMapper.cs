using Microsoft.Data.Sqlite;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Comments;
using SocialMediaPlatform.Core.Domain.Entity;

namespace SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Mappers
{
    /// <summary>
    /// Сэтгэгдлийг SQLite мөр болон domain объект хооронд хөрвүүлэх Mapper
    /// </summary>
    public static class CommentMapper
    {
        /// <summary>SQLite мөрийг Maincomment объект болгоно</summary>
        /// <param name="reader">SQLite өгөгдөл уншигч объект</param>
        public static Comment ToDomain(SqliteDataReader reader)
        {
            return new MainComment
            {
                Id = new CommentId { Value = (uint)reader.GetInt64(0) },
                PostId = new PostId { Value = (uint)reader.GetInt64(1) },
                AuthorId = new UserId { Value = (uint)reader.GetInt64(2) },
                Content = reader.GetString(3),
                CreatedAt = DateTime.Parse(reader.GetString(4))
            };
        }
    }
}
