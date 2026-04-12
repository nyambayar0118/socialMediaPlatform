using Microsoft.Data.Sqlite;

namespace SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite
{
    /// <summary>
    /// SQLite өгөгдлийн сангийн холболтыг удирдах класс
    /// </summary>
    public class DatabaseConnection : IDisposable
    {
        private readonly SqliteConnection _connection;

        public SqliteConnection Connection => _connection;

        public DatabaseConnection(string connectionString)
        {
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
            CreateTables();
        }

        private void CreateTables()
        {
            const string createTablesScript = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY,
                    Username TEXT NOT NULL UNIQUE,
                    Email TEXT NOT NULL,
                    Password TEXT NOT NULL,
                    CreatedAt TEXT NOT NULL,
                    ProfilePicturePath TEXT
                );

                CREATE TABLE IF NOT EXISTS Posts (
                    Id INTEGER PRIMARY KEY,
                    AuthorId INTEGER NOT NULL,
                    Title TEXT NOT NULL,
                    Content TEXT NOT NULL,
                    Visibility TEXT NOT NULL,
                    CreatedAt TEXT NOT NULL,
                    FOREIGN KEY (AuthorId) REFERENCES Users(Id) ON DELETE CASCADE
                );

                CREATE TABLE IF NOT EXISTS Comments (
                    Id INTEGER PRIMARY KEY,
                    PostId INTEGER NOT NULL,
                    AuthorId INTEGER NOT NULL,
                    Content TEXT NOT NULL,
                    CreatedAt TEXT NOT NULL,
                    FOREIGN KEY (PostId) REFERENCES Posts(Id) ON DELETE CASCADE,
                    FOREIGN KEY (AuthorId) REFERENCES Users(Id) ON DELETE CASCADE
                );

                CREATE TABLE IF NOT EXISTS Reactions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    TargetId INTEGER NOT NULL,
                    TargetType TEXT NOT NULL,
                    AuthorId INTEGER NOT NULL,
                    ReactionType TEXT NOT NULL,
                    CreatedAt TEXT NOT NULL,
                    FOREIGN KEY (AuthorId) REFERENCES Users(Id) ON DELETE CASCADE
                );

                CREATE TABLE IF NOT EXISTS Ids (
                    EntityType TEXT PRIMARY KEY,
                    CurrentId INTEGER NOT NULL DEFAULT 0
                );
            ";

            using var command = _connection.CreateCommand();
            command.CommandText = createTablesScript;
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
            _connection.Dispose();
        }
    }
}