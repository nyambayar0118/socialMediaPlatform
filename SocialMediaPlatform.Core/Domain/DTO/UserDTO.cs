using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Core.Domain.DTO
{
    /// <summary>
    /// Хэрэглэгч классын өгөгдлийн дамжуулах объект
    /// </summary>
    /// <param name="Id">Хэрэглэгчийн ID дугаар</param>
    /// <param name="Username">Хэрэглэгчийн нэр</param>
    /// <param name="Email">Хэрэглэгчийн имэйл хаяг</param>
    /// <param name="CreatedAt">Хэрэглэгч үүссэн огноо, цаг</param>
    public record UserDTO(
        UserId Id,
        string Username,
        string Email,
        DateTime CreatedAt
    );
}