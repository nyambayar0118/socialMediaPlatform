using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Reddit.Core.Domain.DTOs
{
    /// <summary>
    /// TimelinePost классын өгөгдлийн дамжуулах объект
    /// </summary>
    /// <param name="Id">Постын ID дугаар</param>
    /// <param name="AuthorId">Постыг бичсэн хэрэглэгчийн ID дугаар</param>
    /// <param name="Visibility">Постын харагдах байдал</param>
    /// <param name="CreatedAt">Пост үүссэн огноо, цаг</param>
    /// <param name="Title">Постын гарчиг</param>
    /// <param name="Content">Постын агуулга</param>
    public record TimelinePostDTO(
        PostId Id,
        UserId AuthorId,
        VisibilityType Visibility,
        DateTime CreatedAt,
        string Title,
        string Content
    ) : PostDTO(Id, AuthorId, Visibility, CreatedAt);
}