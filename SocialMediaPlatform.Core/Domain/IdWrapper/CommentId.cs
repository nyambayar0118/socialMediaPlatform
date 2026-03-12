using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaPlatform.Core.Domain.IdWrapper
{
    /// <summary>
    /// Comment-ийн ID дугаарыг ялгахад зориулсан wrapper класс. Энэ нь Comment entity-ийн ID-г илэрхийлэх бөгөөд type safety-ийг хангахад тусална.
    /// </summary>
    public class CommentId
    {
        /// <summary>
        /// Comment-ийн ID дугаарын утга
        /// </summary>
        public required uint Value { get; init; }

        public override string ToString() => Value.ToString();
    }
}
