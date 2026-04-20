using Microsoft.Data.Sqlite;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite;
using System;

namespace SocialMediaPlatform.Reddit.Infrastructure.Tests.Connections
{
    /// <summary>
    /// Тестүүдэд зориулж SQLite өгөгдлийн санг санах ойд үүсгэж, холболтыг удирдах зориулалттай туслах класс.
    /// </summary>
    public class TestConnection : IDisposable
    {
        // Өгөгдлийн сангийн холболтын объект
        private readonly DatabaseConnection _connection;

        // Өгөгдлийн сангийн холболтын property
        public SqliteConnection Connection => _connection.Connection;

        /// <summary>
        /// Холболтыг санах ойд ажиллах горимд үүсгэх байгуулагч.
        /// </summary>
        public TestConnection()
        {
            _connection = new DatabaseConnection("Data Source=:memory:");
        }

        /// <summary>
        /// Бүх хүснэгтийг устгах үйлдэл хийх туслах метод.
        /// </summary>
        public void ClearAllTables()
        {
            const string clearScript = @"
                DELETE FROM Reactions;
                DELETE FROM Comments;
                DELETE FROM Posts;
                DELETE FROM Users;
                DELETE FROM Ids;
            ";

            using var command = Connection.CreateCommand();
            command.CommandText = clearScript;
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Холболтыг хааж, ашигласан нөөцийг чөлөөлөх үйлдлийг дуудах метод.
        /// </summary>
        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}