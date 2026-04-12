using Microsoft.Data.Sqlite;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Reddit.Core.Ports.Output;

namespace SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Repositories
{
    /// <summary>
    /// Дараалсан ID үүсгэгчийн SQLite Repository-тай ажиллах адаптер
    /// </summary>
    public class SequentialIdRepoSqlite : ISequentialIdRepoPort
    {
        /// <summary>
        /// SQLite холболтын объект
        /// </summary>
        private readonly SqliteConnection _connection;

        /// <summary>
        /// Байгуулагч. SQLite холболтыг dependency injection-аар авна.
        /// </summary>
        /// <param name="connection">SQLite холболтын объект</param>
        public SequentialIdRepoSqlite(SqliteConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <summary>Сүүлийн ID дугаарыг авна</summary>
        /// <param name="entityType">ID-тай entity-н төрлийг зааж өгөх</param>
        public uint GetLastId(IdEntityType entityType)
        {
            const string query = "SELECT CurrentId FROM Ids WHERE EntityType = @EntityType";

            using var command = _connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@EntityType", entityType.ToString());

            var result = command.ExecuteScalar();
            if (result != null && uint.TryParse(result.ToString(), out var id))
            {
                return id;
            }

            return 0;
        }

        /// <summary>Сүүлийн ID дугаарыг хадгална</summary>
        /// <param name="entityType">ID-тай entity-н төрлийг зааж өгөх</param>
        /// <param name="value">Хадгалах утга</param>
        public void SaveLastId(IdEntityType entityType, uint value)
        {
            const string upsertQuery = @"
                INSERT OR REPLACE INTO Ids (EntityType, CurrentId)
                VALUES (@EntityType, @CurrentId)";

            using var command = _connection.CreateCommand();
            command.CommandText = upsertQuery;
            command.Parameters.AddWithValue("@EntityType", entityType.ToString());
            command.Parameters.AddWithValue("@CurrentId", value);

            command.ExecuteNonQuery();
        }
    }
}