using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Reddit.Core.Ports.Output
{
    /// <summary>
    /// Сэтгэгдлийн репозиторийн Output Port интерфейс
    /// </summary>
    public interface ICommentRepoPort
    {
        /// <summary>Сэтгэгдэл хадгалах</summary>
        /// <param name="comment">Хадгалах сэтгэгдлийн объект</param>
        void Save(Comment comment);

        /// <summary>ID-аар сэтгэгдэл хайх</summary>
        /// <param name="commentId">Сэтгэгдлийн ID дугаар</param>
        /// <returns>Олдсон сэтгэгдлийн объект</returns>
        Comment FindById(CommentId commentId);

        /// <summary>Постын сэтгэгдлүүдийг авах</summary>
        /// <param name="postId">Постын ID дугаар</param>
        /// <returns>Сэтгэгдлийн объектын жагсаалт</returns>
        List<Comment> FindByPost(PostId postId);

        /// <summary>Сэтгэгдэл устгах</summary>
        /// <param name="commentId">Устгах сэтгэгдлийн ID дугаар</param>
        void Delete(CommentId commentId);
    }
}