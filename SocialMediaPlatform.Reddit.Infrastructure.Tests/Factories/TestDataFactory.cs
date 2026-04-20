using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Comments;
using SocialMediaPlatform.Reddit.Core.Domain.Posts;
using SocialMediaPlatform.Reddit.Core.Domain.Reactions;
using SocialMediaPlatform.Reddit.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaPlatform.Reddit.Infrastructure.Tests.Factories
{
    /// <summary>
    /// Тестийн явцад ашиглагдах өгөгдлийг үүсгэх үүрэгтэй static үйлдвэрлэгч класс.
    /// </summary>
    public static class TestDataFactory
    {
        /// <summary>
        /// Тестэд ашиглах хэрэглэгчийн объект үүсгэх метод.
        /// </summary>
        /// <param name="id">Хэрэглгэчийн ID дугаар</param>
        /// <param name="username">Хэрэглэгчийн нэр</param>
        /// <param name="email">Хэрэглэгчийн имэйл хаяг</param>
        /// <returns>Үүссэн хэрэглэгчийн объект</returns>
        public static User CreateTestUser(uint id = 1, string username = "testuser", string email = "test@example.com")
        {
            return new NormalUser
            {
                Id = new UserId { Value = id },
                Username = username,
                Email = email,
                Password = "hashedpassword123",
                CreatedAt = DateTime.UtcNow,
                ProfilePicturePath = null
            };
        }

        /// <summary>
        /// Тестэд ашиглагдах post объект үүсгэх метод.
        /// </summary>
        /// <param name="id">Post-ийн ID дугаар</param>
        /// <param name="authorId">Post бичигчийн ID дугаар</param>
        /// <param name="title">Post-ийн гарчиг</param>
        /// <param name="content">Post-ийн агуулга</param>
        /// <returns>Үүссэн post объект</returns>
        public static Post CreateTestPost(uint id = 1, uint authorId = 1, string title = "Test Post", string content = "Test content")
        {
            return new TimelinePost
            {
                Id = new PostId { Value = id },
                AuthorId = new UserId { Value = authorId },
                Title = title,
                Content = content,
                Visibility = VisibilityType.Public,
                CreatedAt = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Тестэд ашиглагдах сэтгэгдэл объект үүсгэх метод.
        /// </summary>
        /// <param name="id">Сэтгэгдлийн ID дугаар</param>
        /// <param name="postId">Сэтгэгдлийг бичсэн Post-ийн ID дугаар</param>
        /// <param name="authorId">Сэтгэгдлийг бичсэн хэрэглэгчийн ID дугаар</param>
        /// <param name="content">Сэтгэгдлийн агуулга</param>
        /// <returns>Үүссэн сэтгэгдэл объект</returns>
        public static Comment CreateTestComment(uint id = 1, uint postId = 1, uint authorId = 1, string content = "Test comment")
        {
            return new MainComment
            {
                Id = new CommentId { Value = id },
                PostId = new PostId { Value = postId },
                AuthorId = new UserId { Value = authorId },
                Content = content,
                CreatedAt = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Тестэд ашиглагдах хариу үйлдлийн Upvote объект үүсгэх метод.
        /// </summary>
        /// <param name="targetId">Хариу үйлдлийг зориулж дарсан зүйлийн ID дугаар</param>
        /// <param name="authorId">Хариу үйлдлийг дарсан хэрэглэгчийн ID дугаар</param>
        /// <param name="targetType">Хариу үйлдлийг зориулж дарсан зүйлийн төрөл</param>
        /// <returns>Үүссэн Upvote объект</returns>
        public static Reaction CreateTestUpvote(uint targetId = 1, uint authorId = 1, ReactionTargetType targetType = ReactionTargetType.Post)
        {
            return new Upvote
            {
                TargetId = targetId,
                TargetType = targetType,
                AuthorId = new UserId { Value = authorId },
                CreatedAt = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Тестэд ашиглагдах хариу үйлдлийн Downvote объект үүсгэх метод.
        /// </summary>
        /// <param name="targetId">Хариу үйлдлийг зориулж дарсан зүйлийн ID дугаар</param>
        /// <param name="authorId">Хариу үйлдлийг дарсан хэрэглэгчийн ID дугаар</param>
        /// <param name="targetType">Хариу үйлдлийг зориулж дарсан зүйлийн төрөл</param>
        /// <returns>Үүссэн Downvote объект</returns>
        public static Reaction CreateTestDownvote(uint targetId = 1, uint authorId = 1, ReactionTargetType targetType = ReactionTargetType.Post)
        {
            return new Downvote
            {
                TargetId = targetId,
                TargetType = targetType,
                AuthorId = new UserId { Value = authorId },
                CreatedAt = DateTime.UtcNow
            };
        }
    }

    /// <summary>
    /// Объектуудыг хооронд нь харьцуулах туслагч класс.
    /// </summary>
    public static class EntityComparison
    {
        /// <summary>
        /// Хэрэглэгч объектуудыг хооронд нь харьцуулах метод. ID, Username, Email, Password зэрэг гол шинж чанаруудыг харьцуулна.
        /// </summary>
        /// <param name="user1">Эхний хэрэглэгч объект</param>
        /// <param name="user2">Хоёр дахь хэрэглэгч объект</param>
        /// <returns>Хоёр объект тэнцүү бол true, үгүй бол false</returns>
        public static bool UsersAreEqual(User? user1, User? user2)
        {
            if (user1 == null && user2 == null) return true;
            if (user1 == null || user2 == null) return false;

            return user1.Id.Value == user2.Id.Value &&
                   user1.Username == user2.Username &&
                   user1.Email == user2.Email &&
                   user1.Password == user2.Password;
        }

        /// <summary>
        /// Post объектуудыг хооронд нь харьцуулах метод. ID, AuthorId, Title, Content, Visibility зэрэг гол шинж чанаруудыг харьцуулна.
        /// </summary>
        /// <param name="post1">Эхний Post объект</param>
        /// <param name="post2">Хоёр дахь Post объект</param>
        /// <returns>Хоёр объект тэнцүү бол true, үгүй бол false</returns>
        public static bool PostsAreEqual(Post? post1, Post? post2)
        {
            if (post1 == null && post2 == null) return true;
            if (post1 == null || post2 == null) return false;

            var p1 = post1 as TimelinePost;
            var p2 = post2 as TimelinePost;

            return post1.Id.Value == post2.Id.Value &&
                   post1.AuthorId.Value == post2.AuthorId.Value &&
                   p1?.Title == p2?.Title &&
                   p1?.Content == p2?.Content &&
                   post1.Visibility == post2.Visibility;
        }

        /// <summary>
        /// Сэтгэгдэл объектуудыг хооронд нь харьцуулах метод. ID, PostId, AuthorId, Content зэрэг гол шинж чанаруудыг харьцуулна.
        /// </summary>
        /// <param name="comment1">Эхний сэтгэгдэл объект</param>
        /// <param name="comment2">Хоёр дахь сэтгэгдэл объект</param>
        /// <returns>Хоёр объект тэнцүү бол true, үгүй бол false</returns>
        public static bool CommentsAreEqual(Comment? comment1, Comment? comment2)
        {
            if (comment1 == null && comment2 == null) return true;
            if (comment1 == null || comment2 == null) return false;

            return comment1.Id.Value == comment2.Id.Value &&
                   comment1.PostId.Value == comment2.PostId.Value &&
                   comment1.AuthorId.Value == comment2.AuthorId.Value &&
                   comment1.Content == comment2.Content;
        }

        /// <summary>
        /// Хариу үйлдлийн объектуудыг хооронд нь харьцуулах метод. TargetId, TargetType, AuthorId зэрэг гол шинж чанаруудыг харьцуулна.
        /// </summary>
        /// <param name="reaction1">Эхний хариу үйлдлийн объект</param>
        /// <param name="reaction2">Хоёр дахь хариу үйлдлийн объект</param>
        /// <returns>Хоёр объект тэнцүү бол true, үгүй бол false</returns>
        public static bool ReactionsAreEqual(Reaction? reaction1, Reaction? reaction2)
        {
            if (reaction1 == null && reaction2 == null) return true;
            if (reaction1 == null || reaction2 == null) return false;

            return reaction1.TargetId == reaction2.TargetId &&
                   reaction1.TargetType == reaction2.TargetType &&
                   reaction1.AuthorId.Value == reaction2.AuthorId.Value &&
                   reaction1.GetType() == reaction2.GetType();
        }
    }
}
