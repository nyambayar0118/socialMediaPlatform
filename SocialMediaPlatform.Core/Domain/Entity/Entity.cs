using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaPlatform.Core.Domain.Entity
{
    /// <summary>
    /// Platform дээрх бүх entity-гийн үндсэн класс
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Entity-г үүсгэсэн UTC огноо
        /// </summary>
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
