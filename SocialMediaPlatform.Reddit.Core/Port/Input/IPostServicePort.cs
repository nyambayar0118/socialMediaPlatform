using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Reddit.Core.Ports.Input
{
    /// <summary>
    /// Постын үйлдлүүдийн Input Port интерфейс
    /// </summary>
    public interface IPostServicePort
    {
        /// <summary>Шинэ пост үүсгэх</summary>
        /// <param name="authorId">Постыг бичсэн хэрэглэгчийн ID дугаар</param>
        /// <param name="title">Постын гарчиг</param>
        /// <param name="content">Постын агуулга</param>
        /// <returns>Үүсгэгдсэн постын DTO</returns>
        PostDTO CreatePost(UserId authorId, string title, string content);

        /// <summary>Пост засварлах</summary>
        /// <param name="postId">Постын ID дугаар</param>
        /// <param name="title">Шинэ гарчиг</param>
        /// <param name="content">Шинэ агуулга</param>
        /// <returns>Засварласан постын DTO</returns>
        PostDTO EditPost(PostId postId, string title, string content);

        /// <summary>Пост устгах</summary>
        /// <param name="postId">Постын ID дугаар</param>
        void DeletePost(PostId postId);

        /// <summary>Пост авах</summary>
        /// <param name="postId">Постын ID дугаар</param>
        /// <returns>Постын DTO</returns>
        PostDTO GetPost(PostId postId);

        /// <summary>Нийт постын жагсаалт авах</summary>
        /// <returns>Постын DTO жагсаалт</returns>
        List<PostDTO> GetFeed();
    }
}