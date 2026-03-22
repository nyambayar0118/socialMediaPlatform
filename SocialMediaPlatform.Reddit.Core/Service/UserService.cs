using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Enum;
using SocialMediaPlatform.Reddit.Core.Domain.Users;
using SocialMediaPlatform.Reddit.Core.Factory;
using SocialMediaPlatform.Reddit.Core.Ports.Input;
using SocialMediaPlatform.Reddit.Core.Ports.Output;

namespace SocialMediaPlatform.Reddit.Core.Service
{
    /// <summary>
    /// Хэрэглэгчийн үйлдлүүдийн Service
    /// </summary>
    public class UserService : IUserServicePort
    {
        private readonly IUserRepoPort _repo;
        private readonly IIdGeneratorPort _idGenerator;
        private readonly UserFactory _factory;

        public UserService(IUserRepoPort repo, IIdGeneratorPort idGenerator, UserFactory factory)
        {
            _repo = repo;
            _idGenerator = idGenerator;
            _factory = factory;
        }

        /// <summary>Шинэ хэрэглэгч бүртгэх</summary>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <param name="email">Имэйл хаяг</param>
        /// <param name="password">Нууц үг</param>
        /// <returns>Үүсгэгдсэн хэрэглэгчийн DTO</returns>
        public UserDTO Register(string username, string email, string password)
        {
            var id = _idGenerator.NextUserId();
            var user = _factory.Create(UserType.Normal, id, username, email, password, "");
            _repo.Save(user);
            return new UserDTO(user.Id, user.Username, user.Email, user.CreatedAt, user.ProfilePicturePath);
        }

        /// <summary>Хэрэглэгч нэвтрэх</summary>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <param name="password">Нууц үг</param>
        /// <returns>Нэвтэрсэн хэрэглэгчийн DTO</returns>
        /// <exception cref="UnauthorizedAccessException">Нууц үг буруу</exception>
        public UserDTO Login(string username, string password)
        {
            var user = _repo.FindByUsername(username);
            if (user.Password != password)
                throw new UnauthorizedAccessException("Нууц үг буруу байна");
            return new UserDTO(user.Id, user.Username, user.Email, user.CreatedAt, user.ProfilePicturePath);
        }

        /// <summary>Хэрэглэгч гарах</summary>
        public void Logout() { }

        /// <summary>Хэрэглэгчийн мэдээлэл авах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Хэрэглэгчийн DTO</returns>
        public UserDTO GetUser(UserId userId)
        {
            var user = _repo.FindById(userId);
            return new UserDTO(user.Id, user.Username, user.Email, user.CreatedAt, user.ProfilePicturePath);
        }

        /// <summary>Хэрэглэгчийн мэдээлэл засварлах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <param name="username">Шинэ хэрэглэгчийн нэр</param>
        /// <param name="email">Шинэ имэйл хаяг</param>
        /// <returns>Засварласан хэрэглэгчийн DTO</returns>
        public UserDTO EditUser(UserId userId, string username, string email)
        {
            var user = _repo.FindById(userId);
            var updated = new NormalUser
            {
                Id = user.Id,
                Username = user.Username,
                Email = email,
                Password = user.Password,
                CreatedAt = user.CreatedAt,
                ProfilePicturePath = user.ProfilePicturePath
            };
            _repo.Update(updated);
            return new UserDTO(updated.Id, updated.Username, updated.Email, updated.CreatedAt, updated.ProfilePicturePath);
        }

        /// <summary>Хэрэглэгч устгах</summary>
        /// <param name="userId">Устгах хэрэглэгчийн ID дугаар</param>
        public void DeleteUser(UserId userId)
        {
            _repo.Delete(userId);
        }

        List<UserDTO> IUserServicePort.GetAllUsers()
        {
            var users = _repo.FindAll();
            return users.Select(u => new UserDTO(u.Id, u.Username, u.Email, u.CreatedAt, u.ProfilePicturePath)).ToList();
        }
    }
}