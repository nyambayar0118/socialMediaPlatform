using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Enum;
using SocialMediaPlatform.Reddit.Core.Domain.Users;

namespace SocialMediaPlatform.Reddit.Infrastructure.Factories
{
    /// <summary>
    /// Хэрэглэгчийн объект үүсгэх factory класс
    /// </summary>
    public class UserFactory
    {
        /// <summary>Хэрэглэгчийн төрлөөс хамааран объект үүсгэх</summary>
        /// <param name="type">Хэрэглэгчийн төрөл</param>
        /// <param name="id">Хэрэглэгчийн ID дугаар</param>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <param name="email">Имэйл хаяг</param>
        /// <param name="password">Нууц үг</param>
        /// <param name="profilepicturepath">Профайл зурагны зам</param>
        /// <returns>Үүсгэгдсэн хэрэглэгчийн объект</returns>
        /// <exception cref="ArgumentException">Тодорхойгүй хэрэглэгчийн төрөл</exception>
        public User Create(UserType type, UserId id, string username, string email, string password, string profilepicturepath)
        {
            return type switch
            {
                UserType.Normal => new NormalUser
                {
                    Id = id,
                    Username = username,
                    Email = email,
                    Password = password,
                    CreatedAt = DateTime.UtcNow,
                    ProfilePicturePath = profilepicturepath
                },
                _ => throw new ArgumentException($"Тодорхойгүй хэрэглэгчийн төрөл: {type}")
            };
        }
    }
}