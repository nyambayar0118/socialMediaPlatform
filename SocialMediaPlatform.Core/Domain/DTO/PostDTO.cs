using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Core.Domain.DTO
{
    /// <summary>
    /// Пост классын өгөгдлийн дамжуулах объект
    /// </summary>
    /// <param name="Id">Постын ID дугаар</param>
    /// <param name="AuthorId">Постыг бичсэн хэрэглэгчийн ID дугаар</param>
    /// <param name="Visibility">Постын харагдах байдал</param>
    /// <param name="CreatedAt">Пост үүссэн огноо, цаг</param>
    public record PostDTO(
        PostId Id,
        UserId AuthorId,
        VisibilityType Visibility,
        DateTime CreatedAt
    );
}