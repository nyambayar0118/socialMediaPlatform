using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Reddit.Core.Ports.Output
{
    /// <summary>
    /// ID үүсгэгчийн Output Port интерфейс
    /// </summary>
    public interface IIdGeneratorPort
    {
        /// <summary>Дараагийн хэрэглэгчийн ID үүсгэх</summary>
        /// <returns>Шинэ UserId</returns>
        UserId NextUserId();

        /// <summary>Дараагийн постын ID үүсгэх</summary>
        /// <returns>Шинэ PostId</returns>
        PostId NextPostId();

        /// <summary>Дараагийн сэтгэгдлийн ID үүсгэх</summary>
        /// <returns>Шинэ CommentId</returns>
        CommentId NextCommentId();
    }
}