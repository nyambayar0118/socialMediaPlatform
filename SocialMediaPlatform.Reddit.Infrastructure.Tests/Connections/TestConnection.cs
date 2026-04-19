using Microsoft.Data.Sqlite;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite;
using System;

namespace SocialMediaPlatform.Reddit.Infrastructure.Tests.Connections
{
    public class TestConnection : IDisposable
    {
        private readonly DatabaseConnection _connection;
        public SqliteConnection Connection => _connection.Connection;

        public TestConnection()
        {
            _connection = new DatabaseConnection("Data Source=:memory:");
        }

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

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}