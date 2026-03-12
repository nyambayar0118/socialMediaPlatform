using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaPlatform.Core.Domain.Entity
{
    /// <summary>
    /// Платформ дээр хэрэглэгчийн бичсэн пост класс
    /// </summary>
    public abstract class Post : Entity
    {
        /// <summary>
        /// Post-ийн ID дугаар
        /// </summary>
        public required PostId Id { get; init; }
        /// <summary>
        /// Post-ийг бичсэн хэрэглэгчийн ID дугаар
        /// </summary>
        public required UserId AuthorId { get; init; }
        /// <summary>
        /// Post-ийн харагдах байдал
        /// </summary>
        public VisibilityType Visibility { get; set; } = VisibilityType.Public;
    }
}
