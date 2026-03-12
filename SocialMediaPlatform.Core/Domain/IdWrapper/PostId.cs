using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaPlatform.Core.Domain.IdWrapper
{
    /// <summary>
    /// Post-ийн ID дугаарыг ялгахад зориулсан wrapper класс. Энэ нь Post entity-ийн ID-г илэрхийлэх бөгөөд type safety-ийг хангахад тусална.
    /// </summary>
    public class PostId
    {
        /// <summary>
        /// Post-ийн ID дугаарын утга
        /// </summary>
        public required uint Value { get; init; }

        public override string ToString() => Value.ToString();
    }
}
