using Microsoft.Data.Sqlite;
using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Posts;

namespace SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Mappers
{
    /// <summary>
    /// Постыг SQLite мөр болон domain объект хооронд хөрвүүлэх Mapper
    /// </summary>
    public static class PostMapper
    {
        /// <summary>SQLite мөрийг Post объект болгоно</summary>
        /// <param name="reader">SQLite өгөгдөл уншигч объект</param>
        public static Post ToDomain(SqliteDataReader reader)
        {
            return new TimelinePost
            {
                Id = new PostId { Value = (uint)reader.GetInt64(0) },
                AuthorId = new UserId { Value = (uint)reader.GetInt64(1) },
                Title = reader.GetString(2),
                Content = reader.GetString(3),
                Visibility = Enum.Parse<VisibilityType>(reader.GetString(4)),
                CreatedAt = DateTime.Parse(reader.GetString(5))
            };
        }
    }
}