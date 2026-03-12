using SocialMediaPlatform.Core.Domain.Entity;

namespace SocialMediaPlatform.Reddit.Core.Domain.Posts
{
    /// <summary>
    /// Хэрэглэгчийн өөрийн Timeline дээрх пост класс
    /// </summary>
    public class TimelinePost : Post
    {
        /// <summary>
        /// Постын гарчиг
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// Постын агуулга
        /// </summary>
        public required string Content { get; set; }
    }
}