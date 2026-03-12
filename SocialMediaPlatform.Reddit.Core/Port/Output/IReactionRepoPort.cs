using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Reddit.Core.Ports.Output
{
    /// <summary>
    /// Reaction-ий репозиторийн Output Port интерфейс
    /// </summary>
    public interface IReactionRepoPort
    {
        /// <summary>Reaction хадгалах</summary>
        /// <param name="reaction">Хадгалах Reaction-ий объект</param>
        void Save(Reaction reaction);

        /// <summary>Reaction устгах</summary>
        /// <param name="targetId">Объектын ID дугаар</param>
        /// <param name="authorId">Reaction өгсөн хэрэглэгчийн ID дугаар</param>
        void Delete(uint targetId, UserId authorId);

        /// <summary>Объектын Reaction-ий тоог авах</summary>
        /// <param name="targetId">Объектын ID дугаар</param>
        /// <param name="targetType">Объектын төрөл</param>
        /// <returns>Reaction-ий төрөл ба тоо</returns>
        Dictionary<string, uint> CountByTarget(uint targetId, ReactionTargetType targetType);

        /// <summary>Хэрэглэгч тухайн объектод Reaction өгсөн эсэхийг шалгах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <param name="targetId">Объектын ID дугаар</param>
        /// <param name="targetType">Объектын төрөл</param>
        /// <returns>Reaction өгсөн бол true, үгүй бол false</returns>
        bool ExistsByUserAndTarget(UserId userId, uint targetId, ReactionTargetType targetType);
    }
}