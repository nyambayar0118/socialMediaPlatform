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
    public static class TestDataFactory
    {
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

    public static class EntityComparison
    {
        public static bool UsersAreEqual(User? user1, User? user2)
        {
            if (user1 == null && user2 == null) return true;
            if (user1 == null || user2 == null) return false;

            return user1.Id.Value == user2.Id.Value &&
                   user1.Username == user2.Username &&
                   user1.Email == user2.Email &&
                   user1.Password == user2.Password;
        }

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

        public static bool CommentsAreEqual(Comment? comment1, Comment? comment2)
        {
            if (comment1 == null && comment2 == null) return true;
            if (comment1 == null || comment2 == null) return false;

            return comment1.Id.Value == comment2.Id.Value &&
                   comment1.PostId.Value == comment2.PostId.Value &&
                   comment1.AuthorId.Value == comment2.AuthorId.Value &&
                   comment1.Content == comment2.Content;
        }

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
