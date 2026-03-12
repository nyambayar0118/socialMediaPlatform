using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Reddit.Core.Ports.Input
{
    /// <summary>
    /// Reaction үйлдлүүдийн Input Port интерфейс
    /// </summary>
    public interface IReactionServicePort
    {
        /// <summary>Reaction нэмэх</summary>
        /// <param name="targetId">Reaction өгөх объектын ID дугаар</param>
        /// <param name="targetType">Reaction өгөх объектын төрөл</param>
        /// <param name="userId">Reaction өгч буй хэрэглэгчийн ID дугаар</param>
        /// <param name="reactionType">Reaction-ий төрөл</param>
        void React(uint targetId, ReactionTargetType targetType, UserId userId, string reactionType);

        /// <summary>Reaction цуцлах</summary>
        /// <param name="targetId">Reaction цуцлах объектын ID дугаар</param>
        /// <param name="targetType">Reaction цуцлах объектын төрөл</param>
        /// <param name="userId">Reaction цуцлах хэрэглэгчийн ID дугаар</param>
        void Unreact(uint targetId, ReactionTargetType targetType, UserId userId);

        /// <summary>Reaction-ий тоог авах</summary>
        /// <param name="targetId">Объектын ID дугаар</param>
        /// <param name="targetType">Объектын төрөл</param>
        /// <returns>Reaction-ий төрөл ба тоо</returns>
        Dictionary<string, uint> GetReactionCount(uint targetId, ReactionTargetType targetType);

        /// <summary>Хэрэглэгч Reaction өгсөн эсэхийг шалгах</summary>
        /// <param name="targetId">Объектын ID дугаар</param>
        /// <param name="targetType">Объектын төрөл</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Reaction өгсөн бол true, үгүй бол false</returns>
        bool HasReacted(uint targetId, ReactionTargetType targetType, UserId userId);
    }
}