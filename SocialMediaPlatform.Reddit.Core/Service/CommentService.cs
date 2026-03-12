using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Comments;
using SocialMediaPlatform.Reddit.Core.Ports.Input;
using SocialMediaPlatform.Reddit.Core.Ports.Output;

namespace SocialMediaPlatform.Reddit.Core.Service
{
    /// <summary>
    /// Сэтгэгдлийн үйлдлүүдийн Service
    /// </summary>
    public class CommentService : ICommentServicePort
    {
        private readonly ICommentRepoPort _repo;
        private readonly IIdGeneratorPort _idGenerator;

        public CommentService(ICommentRepoPort repo, IIdGeneratorPort idGenerator)
        {
            _repo = repo;
            _idGenerator = idGenerator;
        }

        /// <summary>Шинэ сэтгэгдэл нэмэх</summary>
        /// <param name="postId">Харьяалагдах постын ID дугаар</param>
        /// <param name="authorId">Сэтгэгдэл бичсэн хэрэглэгчийн ID дугаар</param>
        /// <param name="content">Сэтгэгдлийн агуулга</param>
        /// <returns>Үүсгэгдсэн сэтгэгдлийн DTO</returns>
        public CommentDTO AddComment(PostId postId, UserId authorId, string content)
        {
            var id = _idGenerator.NextCommentId();
            var comment = new MainComment
            {
                Id = id,
                AuthorId = authorId,
                PostId = postId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };
            _repo.Save(comment);
            return ToDTO(comment);
        }

        /// <summary>Сэтгэгдэл устгах</summary>
        /// <param name="commentId">Устгах сэтгэгдлийн ID дугаар</param>
        public void DeleteComment(CommentId commentId)
        {
            _repo.Delete(commentId);
        }

        /// <summary>Постын сэтгэгдлүүдийг авах</summary>
        /// <param name="postId">Постын ID дугаар</param>
        /// <returns>Сэтгэгдлийн DTO жагсаалт</returns>
        public List<CommentDTO> GetComments(PostId postId)
        {
            return _repo.FindByPost(postId)
                .Cast<MainComment>()
                .Select(ToDTO)
                .ToList();
        }

        /// <summary>MainComment объектыг CommentDTO болгох</summary>
        private static CommentDTO ToDTO(MainComment comment)
        {
            return new CommentDTO(comment.Id, comment.AuthorId, comment.PostId, comment.Content, comment.CreatedAt);
        }
    }
}