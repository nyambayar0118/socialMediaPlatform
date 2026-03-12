using System;
using System.Collections.Generic;
using System.Text;
using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Core.Domain.Entity
{
    /// <summary>
    /// Хэрэглэгчийн бичсэн сэтгэгдэл класс
    /// </summary>
    public abstract class Comment : Entity
    {
        /// <summary>
        /// Сэтгэгдлийн ID дугаар
        /// </summary>
        public required CommentId Id { get; init; }
        /// <summary>
        /// Сэтгэгдлийг бичсэн хэрэглэгчийн ID дугаар
        /// </summary>
        public required UserId AuthorId { get; init; }
        /// <summary>
        /// Сэтгэгдлийн агуулга
        /// </summary>
        public required string Content { get; init; }
    }
}
