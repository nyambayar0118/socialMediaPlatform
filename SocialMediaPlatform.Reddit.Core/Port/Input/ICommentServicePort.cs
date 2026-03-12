using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Core.Ports.Input
{
    /// <summary>
    /// Comment-ийн үйлдлүүдийн Input Port интерфейс
    /// </summary>
    public interface ICommentServicePort
    {
        /// <summary>Comment нэмэх</summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        /// <param name="authorId">Бичсэн хэрэглэгчийн ID дугаар</param>
        /// <param name="content">Агуулга</param>
        /// <returns>Үүсгэгдсэн comment-ийн DTO</returns>
        CommentDTO AddComment(PostId postId, UserId authorId, string content);

        /// <summary>Comment устгах</summary>
        /// <param name="commentId">Comment-ийн ID дугаар</param>
        void DeleteComment(CommentId commentId);

        /// <summary>Post-ийн comment-уудыг авах</summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        /// <returns>Comment-ийн DTO жагсаалт</returns>
        List<CommentDTO> GetComments(PostId postId);

    }
}