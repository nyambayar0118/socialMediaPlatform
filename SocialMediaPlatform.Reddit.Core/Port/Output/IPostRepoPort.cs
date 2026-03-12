using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.IdWrapper;

namespace SocialMediaPlatform.Reddit.Core.Ports.Output
{
    /// <summary>
    /// Постын репозиторийн Output Port интерфейс
    /// </summary>
    public interface IPostRepoPort
    {
        /// <summary>Пост хадгалах</summary>
        /// <param name="post">Хадгалах постын объект</param>
        void Save(Post post);

        /// <summary>ID-аар пост хайх</summary>
        /// <param name="postId">Постын ID дугаар</param>
        /// <returns>Олдсон постын объект</returns>
        Post FindById(PostId postId);

        /// <summary>Нийт постын жагсаалт авах</summary>
        /// <returns>Постын объектын жагсаалт</returns>
        List<Post> FindAll();

        /// <summary>Постын мэдээлэл шинэчлэх</summary>
        /// <param name="post">Шинэчлэх постын объект</param>
        void Update(Post post);

        /// <summary>Пост устгах</summary>
        /// <param name="postId">Устгах постын ID дугаар</param>
        void Delete(PostId postId);
    }
}