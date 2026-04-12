using Microsoft.Data.Sqlite;
using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Reactions;

namespace SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Mappers
{
    /// <summary>
    /// Reaction-ийг SQLite мөр болон domain объект хооронд хөрвүүлэх Mapper
    /// </summary>
    public static class ReactionMapper
    {
        /// <summary>SQLite мөрийг Reaction объект болгоно</summary>
        /// <param name="reader">SQLite өгөгдөл уншигч объект</param>
        public static Reaction ToDomain(SqliteDataReader reader)
        {
            var reactionType = reader.GetString(4);
            var targetId = (uint)reader.GetInt64(0);
            var targetType = Enum.Parse<ReactionTargetType>(reader.GetString(1));
            var authorId = new UserId { Value = (uint)reader.GetInt64(2) };
            var createdAt = DateTime.Parse(reader.GetString(3));

            if (reactionType == "Upvote")
            {
                return new Upvote
                {
                    TargetId = targetId,
                    TargetType = targetType,
                    AuthorId = authorId,
                    CreatedAt = createdAt
                };
            }

            if (reactionType == "Downvote")
            {
                return new Downvote
                {
                    TargetId = targetId,
                    TargetType = targetType,
                    AuthorId = authorId,
                    CreatedAt = createdAt
                };
            }

            throw new ArgumentException($"Undefined Reaction type: {reactionType}");
        }
    }
}