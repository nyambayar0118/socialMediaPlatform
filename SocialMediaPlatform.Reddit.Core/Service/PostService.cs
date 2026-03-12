using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Posts;
using SocialMediaPlatform.Reddit.Core.Domain.DTOs;
using SocialMediaPlatform.Reddit.Core.Factory;
using SocialMediaPlatform.Reddit.Core.Ports.Input;
using SocialMediaPlatform.Reddit.Core.Ports.Output;

namespace SocialMediaPlatform.Reddit.Core.Service
{
    /// <summary>
    /// Постын үйлдлүүдийн Service
    /// </summary>
    public class PostService : IPostServicePort
    {
        private readonly IPostRepoPort _repo;
        private readonly IIdGeneratorPort _idGenerator;
        private readonly PostFactory _factory;

        public PostService(IPostRepoPort repo, IIdGeneratorPort idGenerator, PostFactory factory)
        {
            _repo = repo;
            _idGenerator = idGenerator;
            _factory = factory;
        }

        /// <summary>Шинэ пост үүсгэх</summary>
        /// <param name="authorId">Постыг бичсэн хэрэглэгчийн ID дугаар</param>
        /// <param name="title">Постын гарчиг</param>
        /// <param name="content">Постын агуулга</param>
        /// <returns>Үүсгэгдсэн постын DTO</returns>
        public TimelinePostDTO CreatePost(UserId authorId, string title, string content)
        {
            var id = _idGenerator.NextPostId();
            var post = _factory.Create(id, authorId, title, content);
            _repo.Save(post);
            return ToDTO(post);
        }

        /// <summary>Пост засварлах</summary>
        /// <param name="postId">Постын ID дугаар</param>
        /// <param name="title">Шинэ гарчиг</param>
        /// <param name="content">Шинэ агуулга</param>
        /// <returns>Засварласан постын DTO</returns>
        public TimelinePostDTO EditPost(PostId postId, string title, string content)
        {
            var post = (TimelinePost)_repo.FindById(postId);
            post.Title = title;
            post.Content = content;
            _repo.Update(post);
            return ToDTO(post);
        }

        /// <summary>Пост устгах</summary>
        /// <param name="postId">Устгах постын ID дугаар</param>
        public void DeletePost(PostId postId)
        {
            _repo.Delete(postId);
        }

        /// <summary>Пост авах</summary>
        /// <param name="postId">Постын ID дугаар</param>
        /// <returns>Постын DTO</returns>
        public TimelinePostDTO GetPost(PostId postId)
        {
            var post = (TimelinePost)_repo.FindById(postId);
            return ToDTO(post);
        }

        /// <summary>Нийт постын жагсаалт авах</summary>
        /// <returns>Постын DTO жагсаалт</returns>
        public List<TimelinePostDTO> GetFeed()
        {
            return _repo.FindAll()
                .Cast<TimelinePost>()
                .Select(ToDTO)
                .ToList();
        }

        /// <summary>TimelinePost объектыг PostDTO болгох</summary>
        private static TimelinePostDTO ToDTO(TimelinePost post)
        {
            return new TimelinePostDTO(post.Id, post.AuthorId, post.Visibility, post.CreatedAt, post.Title, post.Content);
        }
    }
}