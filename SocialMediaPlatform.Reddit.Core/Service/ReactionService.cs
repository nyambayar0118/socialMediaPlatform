using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Reactions;
using SocialMediaPlatform.Reddit.Core.Ports.Input;
using SocialMediaPlatform.Reddit.Core.Ports.Output;

namespace SocialMediaPlatform.Reddit.Core.Service
{
    /// <summary>
    /// Reaction-ий үйлдлүүдийн Service
    /// </summary>
    public class ReactionService : IReactionServicePort
    {
        private readonly IReactionRepoPort _repo;

        public ReactionService(IReactionRepoPort repo)
        {
            _repo = repo;
        }

        /// <summary>Reaction нэмэх</summary>
        /// <param name="targetId">Reaction өгөх объектын ID дугаар</param>
        /// <param name="targetType">Reaction өгөх объектын төрөл</param>
        /// <param name="userId">Reaction өгч буй хэрэглэгчийн ID дугаар</param>
        /// <param name="reactionType">Reaction-ий төрөл</param>
        /// <exception cref="ArgumentException">Тодорхойгүй Reaction төрөл</exception>
        public void React(uint targetId, ReactionTargetType targetType, UserId userId, string reactionType)
        {
            if (_repo.ExistsByUserAndTarget(userId, targetId, targetType))
                _repo.Delete(targetId, userId);

            var reaction = reactionType switch
            {
                "Upvote" => (Reaction)(new Upvote
                {
                    TargetId = targetId,
                    TargetType = targetType,
                    AuthorId = userId,
                    CreatedAt = DateTime.UtcNow
                }),
                "Downvote" => new Downvote
                {
                    TargetId = targetId,
                    TargetType = targetType,
                    AuthorId = userId,
                    CreatedAt = DateTime.UtcNow
                },
                _ => throw new ArgumentException($"Undefined Reaction type: {reactionType}")
            };

            _repo.Save(reaction);
        }

        /// <summary>Reaction цуцлах</summary>
        /// <param name="targetId">Reaction цуцлах объектын ID дугаар</param>
        /// <param name="targetType">Reaction цуцлах объектын төрөл</param>
        /// <param name="userId">Reaction цуцлах хэрэглэгчийн ID дугаар</param>
        public void Unreact(uint targetId, ReactionTargetType targetType, UserId userId)
        {
            _repo.Delete(targetId, userId);
        }

        /// <summary>Объектын Reaction-ий тоог авах</summary>
        /// <param name="targetId">Объектын ID дугаар</param>
        /// <param name="targetType">Объектын төрөл</param>
        /// <returns>Reaction-ий төрөл ба тоо</returns>
        public Dictionary<string, uint> GetReactionCount(uint targetId, ReactionTargetType targetType)
        {
            return _repo.CountByTarget(targetId, targetType);
        }

        /// <summary>Хэрэглэгч Reaction өгсөн эсэхийг шалгах</summary>
        /// <param name="targetId">Объектын ID дугаар</param>
        /// <param name="targetType">Объектын төрөл</param>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Reaction өгсөн бол true, үгүй бол false</returns>
        public bool HasReacted(uint targetId, ReactionTargetType targetType, UserId userId)
        {
            return _repo.ExistsByUserAndTarget(userId, targetId, targetType);
        }
    }
}