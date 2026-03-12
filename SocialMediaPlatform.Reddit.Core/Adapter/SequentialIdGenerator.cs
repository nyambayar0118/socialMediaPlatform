using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Reddit.Core.Ports.Output;

namespace SocialMediaPlatform.Reddit.Core.Adapters
{
    /// <summary>
    /// Дараалсан ID үүсгэгч адаптер
    /// </summary>
    public class SequentialIdGenerator : IIdGeneratorPort
    {
        /// <summary>
        /// ID хадгалах репозитори
        /// </summary>
        private readonly ISequentialIdRepoPort _repo;

        /// <summary>
        /// /// Байгуулагч. Репорзиториг dependency injection байдлаар оруулж өгнө.
        /// </summary>
        /// <param name="repo"></param>
        public SequentialIdGenerator(ISequentialIdRepoPort repo)
        {
            _repo = repo;
        }

        /// <summary>Дараагийн хэрэглэгчийн ID үүсгэх</summary>
        public UserId NextUserId()
        {
            var next = _repo.GetLastId(IdEntityType.User) + 1;
            _repo.SaveLastId(IdEntityType.User, next);
            return new UserId { Value = next };
        }

        /// <summary>Дараагийн постын ID үүсгэх</summary>
        public PostId NextPostId()
        {
            var next = _repo.GetLastId(IdEntityType.Post) + 1;
            _repo.SaveLastId(IdEntityType.Post, next);
            return new PostId { Value = next };
        }

        /// <summary>Дараагийн сэтгэгдлийн ID үүсгэх</summary>
        public CommentId NextCommentId()
        {
            var next = _repo.GetLastId(IdEntityType.Comment) + 1;
            _repo.SaveLastId(IdEntityType.Comment, next);
            return new CommentId { Value = next };
        }
    }
}