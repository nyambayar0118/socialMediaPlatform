using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Core.Domain.DTO
{
    /// <summary>
    /// Сэтгэгдэл классын өгөгдлийн дамжуулах объект
    /// </summary>
    /// <param name="Id">Сэтгэгдлийн ID дугаар</param>
    /// <param name="AuthorId">Сэтгэгдлийг бичсэн хэрэглэгчийн ID дугаар</param>
    /// <param name="PostId">Харьяалагдах постын ID дугаар</param>
    /// <param name="Content">Сэтгэгдлийн агуулга</param>
    /// <param name="CreatedAt">Сэтгэгдэл үүссэн огноо, цаг</param>
    public record CommentDTO(
        CommentId Id,
        UserId AuthorId,
        PostId PostId,
        string Content,
        DateTime CreatedAt
    );
}