using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Reddit.Core.Ports.Output
{
    /// <summary>
    /// Хэрэглэгчийн репозиторийн Output Port интерфейс
    /// </summary>
    public interface IUserRepoPort
    {
        /// <summary>Хэрэглэгч хадгалах</summary>
        /// <param name="user">Хадгалах хэрэглэгчийн объект</param>
        void Save(User user);

        /// <summary>ID-аар хэрэглэгч хайх</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Олдсон хэрэглэгчийн объект</returns>
        User FindById(UserId userId);

        /// <summary>Username-аар хэрэглэгч хайх</summary>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <returns>Олдсон хэрэглэгчийн объект</returns>
        User FindByUsername(string username);

        /// <summary>Хэрэглэгчийн мэдээлэл шинэчлэх</summary>
        /// <param name="user">Шинэчлэх хэрэглэгчийн объект</param>
        void Update(User user);

        /// <summary>Хэрэглэгч устгах</summary>
        /// <param name="userId">Устгах хэрэглэгчийн ID дугаар</param>
        void Delete(UserId userId);
    }
}