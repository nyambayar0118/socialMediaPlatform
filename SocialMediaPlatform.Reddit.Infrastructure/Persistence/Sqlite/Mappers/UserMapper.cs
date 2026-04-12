using Microsoft.Data.Sqlite;
using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Users;
using System.Data;

namespace SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Mappers
{
    /// <summary>
    /// Хэрэглэгчийг SQLite мөр болон domain объект хооронд хөрвүүлэх Mapper класс
    /// </summary>
    public static class UserMapper
    {
        /// <summary>SQLite мөрийг User объект болгох</summary>
        /// <param name="reader">SQLite өгөгдөл уншигч объект</param>
        public static User ToDomain(SqliteDataReader reader)
        {
            return new NormalUser
            {
                Id = new UserId { Value = (uint)reader.GetInt32(0) },
                Username = reader.GetString(1),
                Email = reader.GetString(2),
                Password = reader.GetString(3),
                CreatedAt = DateTime.Parse(reader.GetString(4)),
                ProfilePicturePath = reader.IsDBNull(5) ? "" : reader.GetString(5)
            };
        }
    }
}