using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Core.Infrastructure;
using SocialMediaPlatform.Reddit.Core.Ports.Input;
using SocialMediaPlatform.Reddit.Core.Domain.DTOs;

namespace SocialMediaPlatform.Reddit.Core.Infrastructure
{
    /// <summary>
    /// Платформын үйлдлүүдийг удирдах Controller класс
    /// </summary>
    public class Controller
    {
        private readonly IUserServicePort _userService;
        private readonly IPostServicePort _postService;
        private readonly ICommentServicePort _commentService;
        private readonly IReactionServicePort _reactionService;
        private readonly Session _session;

        public Controller(
            IUserServicePort userService,
            IPostServicePort postService,
            ICommentServicePort commentService,
            IReactionServicePort reactionService,
            Session session)
        {
            _userService = userService;
            _postService = postService;
            _commentService = commentService;
            _reactionService = reactionService;
            _session = session;
        }

        /// <summary>Шинэ хэрэглэгч бүртгэх</summary>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <param name="email">Имэйл хаяг</param>
        /// <param name="password">Нууц үг</param>
        public void Register(string username, string email, string password)
        {
            var user = _userService.Register(username, email, password);
            _session.Login(user);
        }

        /// <summary>Хэрэглэгч нэвтрэх</summary>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <param name="password">Нууц үг</param>
        public void Login(string username, string password)
        {
            var user = _userService.Login(username, password);
            _session.Login(user);
        }

        /// <summary>Хэрэглэгч гарах</summary>
        public void Logout()
        {
            _userService.Logout();
            _session.Logout();
        }

        /// <summary>Шинэ пост үүсгэх</summary>
        /// <param name="title">Постын гарчиг</param>
        /// <param name="content">Постын агуулга</param>
        /// <returns>Үүсгэгдсэн постын DTO</returns>
        public PostDTO CreatePost(string title, string content)
        {
            var currentUser = _session.GetCurrentUser();
            return _postService.CreatePost(currentUser.Id, title, content);
        }

        /// <summary>Пост засварлах</summary>
        /// <param name="postId">Постын ID дугаар</param>
        /// <param name="title">Шинэ гарчиг</param>
        /// <param name="content">Шинэ агуулга</param>
        /// <returns>Засварласан постын DTO</returns>
        public PostDTO EditPost(PostId postId, string title, string content)
        {
            return _postService.EditPost(postId, title, content);
        }

        /// <summary>Пост устгах</summary>
        /// <param name="postId">Устгах постын ID дугаар</param>
        public void DeletePost(PostId postId)
        {
            _postService.DeletePost(postId);
        }

        /// <summary>Нийт постын жагсаалт авах</summary>
        /// <returns>Постын DTO жагсаалт</returns>
        public List<TimelinePostDTO> GetFeed()
        {
            return _postService.GetFeed();
        }

        /// <summary>Сэтгэгдэл нэмэх</summary>
        /// <param name="postId">Харьяалагдах постын ID дугаар</param>
        /// <param name="content">Сэтгэгдлийн агуулга</param>
        /// <returns>Үүсгэгдсэн сэтгэгдлийн DTO</returns>
        public CommentDTO AddComment(PostId postId, string content)
        {
            var currentUser = _session.GetCurrentUser();
            return _commentService.AddComment(postId, currentUser.Id, content);
        }

        /// <summary>Сэтгэгдэл устгах</summary>
        /// <param name="commentId">Устгах сэтгэгдлийн ID дугаар</param>
        public void DeleteComment(CommentId commentId)
        {
            _commentService.DeleteComment(commentId);
        }

        /// <summary>Постын сэтгэгдлүүдийг авах</summary>
        /// <param name="postId">Постын ID дугаар</param>
        /// <returns>Сэтгэгдлийн DTO жагсаалт</returns>
        public List<CommentDTO> GetComments(PostId postId)
        {
            return _commentService.GetComments(postId);
        }

        /// <summary>Reaction нэмэх</summary>
        /// <param name="targetId">Reaction өгөх объектын ID дугаар</param>
        /// <param name="targetType">Reaction өгөх объектын төрөл</param>
        /// <param name="reactionType">Reaction-ий төрөл</param>
        public void React(uint targetId, ReactionTargetType targetType, string reactionType)
        {
            var currentUser = _session.GetCurrentUser();
            _reactionService.React(targetId, targetType, currentUser.Id, reactionType);
        }

        /// <summary>Reaction цуцлах</summary>
        /// <param name="targetId">Reaction цуцлах объектын ID дугаар</param>
        /// <param name="targetType">Reaction цуцлах объектын төрөл</param>
        public void Unreact(uint targetId, ReactionTargetType targetType)
        {
            var currentUser = _session.GetCurrentUser();
            _reactionService.Unreact(targetId, targetType, currentUser.Id);
        }

        /// <summary>Объектын Reaction-ий тоог авах</summary>
        /// <param name="targetId">Объектын ID дугаар</param>
        /// <param name="targetType">Объектын төрөл</param>
        /// <returns>Reaction-ий төрөл ба тоо</returns>
        public Dictionary<string, uint> GetReactionCount(uint targetId, ReactionTargetType targetType)
        {
            return _reactionService.GetReactionCount(targetId, targetType);
        }

        /// <summary>Хэрэглэгчийн мэдээлэл засварлах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <param name="username">Шинэ хэрэглэгчийн нэр</param>
        /// <param name="email">Шинэ имэйл хаяг</param>
        /// <returns>Засварласан хэрэглэгчийн DTO</returns>
        public UserDTO EditUser(UserId userId, string username, string email, string profilePicturePath)
        {
            return _userService.EditUser(userId, username, email, profilePicturePath);
        }

        /// <summary>Хэрэглэгчийн мэдээллийг ID дугаараар авах</summary>
        public UserDTO GetUser(UserId userId)
        {
            return _userService.GetUser(userId);
        }

        /// <summary>Бүх хэрэглэгчдийн жагсаалт авах</summary>
        public List<UserDTO> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }
    }
}