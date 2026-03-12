using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Reddit.Core.Ports.Input
{
    /// <summary>
    /// Хэрэглэгчийн үйлдлүүдийн Input Port интерфейс
    /// </summary>
    public interface IUserServicePort
    {
        /// <summary>Шинэ хэрэглэгч бүртгэх</summary>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <param name="email">Имэйл хаяг</param>
        /// <param name="password">Нууц үг</param>
        /// <returns>Үүсгэгдсэн хэрэглэгчийн DTO</returns>
        UserDTO Register(string username, string email, string password);

        /// <summary>Хэрэглэгч нэвтрэх</summary>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <param name="password">Нууц үг</param>
        /// <returns>Нэвтэрсэн хэрэглэгчийн DTO</returns>
        UserDTO Login(string username, string password);

        /// <summary>Хэрэглэгч гарах</summary>
        void Logout();

        /// <summary>Хэрэглэгчийн мэдээлэл авах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Хэрэглэгчийн DTO</returns>
        UserDTO GetUser(UserId userId);

        /// <summary>Хэрэглэгчийн мэдээлэл засварлах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <param name="username">Шинэ хэрэглэгчийн нэр</param>
        /// <param name="email">Шинэ имэйл хаяг</param>
        /// <returns>Засварласан хэрэглэгчийн DTO</returns>
        UserDTO EditUser(UserId userId, string username, string email);

        /// <summary>Хэрэглэгч устгах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        void DeleteUser(UserId userId);
    }
}