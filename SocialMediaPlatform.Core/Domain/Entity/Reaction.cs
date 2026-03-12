using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaPlatform.Core.Domain.Entity
{
    /// <summary>
    /// Платформ дээрх хэрэглэгчийн реакцийн класс. Like, Dislike, Love гэх мэт реакциудыг үүнээс удамшуулах байдлаар ашиглана.
    /// </summary>
    public abstract class Reaction : Entity
    {
        /// <summary>
        /// Reaction-г дарсан объектын ID дугаар. Жишээ нь, хэрэв хэрэглэгч пост дээр лайк дарсан бол энэ ID нь тухайн постын ID байх болно.
        /// </summary>
        public required uint TargetId { get; init; }
        /// <summary>
        /// Reaction-г дарсан объектын төрөл. Жишээ нь, хэрэв хэрэглэгч пост дээр лайк дарсан бол энэ TargetType нь Post байх болно.
        /// </summary>
        public required ReactionTargetType TargetType { get; init; }
        /// <summary>
        /// Reaction-г дарсан хэрэглэгчийн ID дугаар
        /// </summary>
        public required UserId AuthorId { get; init; }
    }
}
