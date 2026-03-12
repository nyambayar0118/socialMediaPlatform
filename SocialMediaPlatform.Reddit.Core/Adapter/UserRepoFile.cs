using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Users;
using SocialMediaPlatform.Reddit.Core.Ports.Output;

namespace SocialMediaPlatform.Reddit.Core.Adapters.File
{
    /// <summary>
    /// Хэрэглэгчийн файл дээр суурилсан Repository адаптер
    /// </summary>
    public class UserRepoFile : IUserRepoPort
    {
        /// <summary>
        /// Ажиллах файлын зам
        /// </summary>
        private readonly string _filePath;

        /// <summary>
        /// Байгуулагч. Файл замыг авна. Файл байхгүй бол автоматаар үүсгэнэ.
        /// </summary>
        /// <param name="filePath">Файлын зам</param>
        public UserRepoFile(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>Хэрэглэгч хадгалах</summary>
        /// <param name="user">Хадгалах хэрэглэгчийн объект</param>
        public void Save(User user)
        {
            var line = Serialize(user);
            System.IO.File.AppendAllText(_filePath, line + Environment.NewLine);
        }

        /// <summary>ID-аар хэрэглэгч хайх</summary>
        /// <param name="userId">Хайх хэрэглэгчийн ID</param>
        public User FindById(UserId userId)
        {
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var user = Deserialize(line);
                if (user.Id.Value == userId.Value)
                    return user;
            }
            throw new KeyNotFoundException($"User ID {userId.Value} not found");
        }

        /// <summary>Хэрэглэгчийн нэрээр хэрэглэгч хайх</summary>
        /// <param name="username">Хайх хэрэглэгчийн username</param>
        public User FindByUsername(string username)
        {
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var user = Deserialize(line);
                if (user.Username == username)
                    return user;
            }
            throw new KeyNotFoundException($"Username '{username}' not found");
        }

        /// <summary>Хэрэглэгчийн мэдээлэл шинэчлэх</summary>
        /// <param name="user">Шинэчлэх хэрэглэгчийн объект</param>
        public void Update(User user)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var parts = line.Split('|');
                    return uint.Parse(parts[0]) == user.Id.Value
                        ? Serialize(user)
                        : line;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Хэрэглэгч устгах</summary>
        /// <param name="userId">Устгах хэрэглэгчийн ID</param>
        public void Delete(UserId userId)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Where(line =>
                {
                    var parts = line.Split('|');
                    return uint.Parse(parts[0]) != userId.Value;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>User объектыг мөр болгох</summary>
        /// <param name="user">Хэрэглэгчийн объект</param>
        private static string Serialize(User user)
        {
            if (user is NormalUser normal)
                return $"{normal.Id.Value}|{normal.Username}|{normal.Email}|{normal.Password}|{normal.CreatedAt:O}";

            throw new ArgumentException($"Undefined User type: {user.GetType().Name}");
        }

        /// <summary>Мөрийг User объект болгох</summary>
        /// <param name="line">Объект болгох мөр</param>
        private static NormalUser Deserialize(string line)
        {
            var parts = line.Split('|');
            return new NormalUser
            {
                Id = new UserId { Value = uint.Parse(parts[0]) },
                Username = parts[1],
                Email = parts[2],
                Password = parts[3],
                CreatedAt = DateTime.Parse(parts[4])
            };
        }
    }
}