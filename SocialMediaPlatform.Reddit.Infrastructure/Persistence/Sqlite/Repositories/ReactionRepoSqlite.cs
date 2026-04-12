using Microsoft.Data.Sqlite;
using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Reactions;
using SocialMediaPlatform.Reddit.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Mappers;

namespace SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Repositories
{
    /// <summary>
    /// Reaction service-ийн SQLite Repository-той ажиллах адаптер
    /// </summary>
    public class ReactionRepoSqlite : IReactionRepoPort
    {
        /// <summary>
        /// SQLite холболтын объект
        /// </summary>
        private readonly SqliteConnection _connection;

        /// <summary>
        /// Байгуулагч. SQLite холболтыг dependency injection-аар авна.
        /// </summary>
        /// <param name="connection">SQLite холболтын объект</param>
        public ReactionRepoSqlite(SqliteConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <summary>Reaction хадгална</summary>
        /// <param name="reaction">Хадгалах Reaction-ий объект</param>
        public void Save(Reaction reaction)
        {
            var reactionType = reaction is Upvote ? "Upvote" : "Downvote";

            const string insertQuery = @"
                INSERT INTO Reactions (TargetId, TargetType, AuthorId, ReactionType, CreatedAt)
                VALUES (@TargetId, @TargetType, @AuthorId, @ReactionType, @CreatedAt)";

            using var command = _connection.CreateCommand();
            command.CommandText = insertQuery;
            command.Parameters.AddWithValue("@TargetId", reaction.TargetId);
            command.Parameters.AddWithValue("@TargetType", reaction.TargetType.ToString());
            command.Parameters.AddWithValue("@AuthorId", reaction.AuthorId.Value);
            command.Parameters.AddWithValue("@ReactionType", reactionType);
            command.Parameters.AddWithValue("@CreatedAt", reaction.CreatedAt.ToString("O"));

            command.ExecuteNonQuery();
        }

        /// <summary>Reaction устгана</summary>
        /// <param name="targetId">Объектын ID дугаар</param>
        /// <param name="authorId">Reaction өгсөн хэрэглэгчийн ID дугаар</param>
        public void Delete(uint targetId, UserId authorId)
        {
            const string deleteQuery = @"
                DELETE FROM Reactions
                WHERE TargetId = @TargetId AND AuthorId = @AuthorId";

            using var command = _connection.CreateCommand();
            command.CommandText = deleteQuery;
            command.Parameters.AddWithValue("@TargetId", targetId);
            command.Parameters.AddWithValue("@AuthorId", authorId.Value);

            command.ExecuteNonQuery();
        }

        /// <summary>Объектын Reaction-ий тоог авна</summary>
        /// <param name="targetId">Объектын ID дугаар</param>
        /// <param name="targetType">Объектын төрөл</param>
        public Dictionary<string, uint> CountByTarget(uint targetId, ReactionTargetType targetType)
        {
            const string query = @"
                SELECT ReactionType, COUNT(*) as Count
                FROM Reactions
                WHERE TargetId = @TargetId AND TargetType = @TargetType
                GROUP BY ReactionType";

            var counts = new Dictionary<string, uint>();

            using var command = _connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@TargetId", targetId);
            command.Parameters.AddWithValue("@TargetType", targetType.ToString());

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var reactionType = reader.GetString(0);
                var count = (uint)reader.GetInt64(1);
                counts[reactionType] = count;
            }

            return counts;
        }

        /// <summary>Хэрэглэгч тухайн объектод Reaction өгсөн эсэхийг шалгана</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <param name="targetId">Объектын ID дугаар</param>
        /// <param name="targetType">Объектын төрөл</param>
        public bool ExistsByUserAndTarget(UserId userId, uint targetId, ReactionTargetType targetType)
        {
            const string query = @"
                SELECT COUNT(*) FROM Reactions
                WHERE AuthorId = @AuthorId AND TargetId = @TargetId AND TargetType = @TargetType";

            using var command = _connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@AuthorId", userId.Value);
            command.Parameters.AddWithValue("@TargetId", targetId);
            command.Parameters.AddWithValue("@TargetType", targetType.ToString());

            var result = command.ExecuteScalar();
            return result != null && (long)result > 0;
        }
    }
}