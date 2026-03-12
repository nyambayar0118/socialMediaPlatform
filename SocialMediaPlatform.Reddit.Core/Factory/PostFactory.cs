using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Posts;

namespace SocialMediaPlatform.Reddit.Core.Factory
{
    /// <summary>
    /// Постын объект үүсгэх factory класс
    /// </summary>
    public class PostFactory
    {
        /// <summary>Шинэ пост объект үүсгэх</summary>
        /// <param name="id">Постын ID дугаар</param>
        /// <param name="authorId">Постыг бичсэн хэрэглэгчийн ID дугаар</param>
        /// <param name="title">Постын гарчиг</param>
        /// <param name="content">Постын агуулга</param>
        /// <returns>Үүсгэгдсэн постын объект</returns>
        public TimelinePost Create(PostId id, UserId authorId, string title, string content)
        {
            return new TimelinePost
            {
                Id = id,
                AuthorId = authorId,
                Title = title,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}