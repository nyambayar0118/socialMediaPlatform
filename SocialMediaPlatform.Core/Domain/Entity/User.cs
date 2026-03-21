using System;
using System.Collections.Generic;
using System.Text;
using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Core.Domain.Entity
{
    /// <summary>
    /// Платформ дээрх хэрэглэгч класс
    /// </summary>
    public abstract class User : Entity
    {
        /// <summary>
        /// User-ийн ID дугаар
        /// </summary>
        public required UserId Id { get; init; }
        /// <summary>
        /// Хэрэглэгчийн username. Платформ дээр давхардсан username байхгүй байх ёстой
        /// </summary>
        public required string Username { get; init; }
        /// <summary>
        /// Хэрэглэгчийн email хаяг
        /// </summary>
        public required string Email { get; set; }
        /// <summary>
        /// Хэрэглэгчийн нууц үг
        /// </summary>
        public required string Password { get; set; }
        /// <summary>
        /// Хэрэглэгчийн профайл зурагны файлын зам
        /// </summary>
        public required string ProfilePicturePath { get; set; } = string.Empty;
    }
}
