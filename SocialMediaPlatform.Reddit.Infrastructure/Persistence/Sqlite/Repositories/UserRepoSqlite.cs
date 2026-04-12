using Microsoft.Data.Sqlite;
using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Users;
using SocialMediaPlatform.Reddit.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Mappers;

namespace SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Repositories
{
    /// <summary>
    /// User service-ийн SQLite Repository-той ажиллах адаптер
    /// </summary>
    public class UserRepoSqlite : IUserRepoPort
    {
        /// <summary>
        /// SQLite холболтын объект
        /// </summary>
        private readonly SqliteConnection _connection;

        /// <summary>
        /// Байгуулагч. SQLite холболтыг dependency injection-р оруулж өгнө.
        /// </summary>
        /// <param name="connection">SQLite холболтын объект</param>
        public UserRepoSqlite(SqliteConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <summary>Шинэ user-г хадгална</summary>
        /// <param name="user">Хадгалах хэрэглэгчийн объект</param>
        public void Save(User user)
        {
            const string insertQuery = @"
                INSERT INTO Users (Id, Username, Email, Password, CreatedAt, ProfilePicturePath)
                VALUES (@Id, @Username, @Email, @Password, @CreatedAt, @ProfilePicturePath)";

            using var command = _connection.CreateCommand();
            command.CommandText = insertQuery;
            command.Parameters.AddWithValue("@Id", user.Id.Value);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@CreatedAt", user.CreatedAt.ToString("O"));
            command.Parameters.AddWithValue("@ProfilePicturePath", user.ProfilePicturePath ?? "");

            command.ExecuteNonQuery();
        }

        /// <summary>ID дугаараар хэрэглэгч хайх</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        public User FindById(UserId userId)
        {
            const string query = @"
                SELECT Id, Username, Email, Password, CreatedAt, ProfilePicturePath
                FROM Users
                WHERE Id = @Id";

            using var command = _connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@Id", userId.Value);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return UserMapper.ToDomain(reader);
            }

            throw new KeyNotFoundException($"User ID {userId.Value} not found");
        }

        /// <summary>Username-аар хэрэглэгч хайх</summary>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        public User FindByUsername(string username)
        {
            const string query = @"
                SELECT Id, Username, Email, Password, CreatedAt, ProfilePicturePath
                FROM Users
                WHERE Username = @Username";

            using var command = _connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@Username", username);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return UserMapper.ToDomain(reader);
            }

            throw new KeyNotFoundException($"Username '{username}' not found");
        }

        /// <summary>Хэрэглэгчийн мэдээлэл шинэчлэх</summary>
        /// <param name="user">Шинэчлэх хэрэглэгчийн объект</param>
        public void Update(User user)
        {
            const string updateQuery = @"
                UPDATE Users
                SET Username = @Username,
                    Email = @Email,
                    Password = @Password,
                    ProfilePicturePath = @ProfilePicturePath
                WHERE Id = @Id";

            using var command = _connection.CreateCommand();
            command.CommandText = updateQuery;
            command.Parameters.AddWithValue("@Id", user.Id.Value);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@ProfilePicturePath", user.ProfilePicturePath ?? "");

            command.ExecuteNonQuery();
        }

        /// <summary>Хэрэглэгч устгах</summary>
        /// <param name="userId">Устгах хэрэглэгчийн ID дугаар</param>
        public void Delete(UserId userId)
        {
            const string deleteQuery = "DELETE FROM Users WHERE Id = @Id";

            using var command = _connection.CreateCommand();
            command.CommandText = deleteQuery;
            command.Parameters.AddWithValue("@Id", userId.Value);

            command.ExecuteNonQuery();
        }

        /// <summary>Бүх хэрэглэгчдийг авах</summary>
        public List<User> FindAll()
        {
            const string query = @"
                SELECT Id, Username, Email, Password, CreatedAt, ProfilePicturePath
                FROM Users
                ORDER BY CreatedAt DESC";

            var users = new List<User>();

            using var command = _connection.CreateCommand();
            command.CommandText = query;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(UserMapper.ToDomain(reader));
            }

            return users;
        }
    }
}