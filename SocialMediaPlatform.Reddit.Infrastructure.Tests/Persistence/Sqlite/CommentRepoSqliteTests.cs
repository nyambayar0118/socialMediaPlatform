using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Repositories;
using SocialMediaPlatform.Reddit.Infrastructure.Tests.Factories;
using SocialMediaPlatform.Reddit.Infrastructure.Tests.Connections;

namespace SocialMediaPlatform.Reddit.Infrastructure.Tests.Persistence.Sqlite
{
    [TestClass]
    public class CommentRepoSqliteTests
    {
        private TestConnection _dbSetup;
        private CommentRepoSqlite _commentRepo;
        private UserRepoSqlite _userRepo;
        private PostRepoSqlite _postRepo;

        [TestInitialize]
        public void Setup()
        {
            _dbSetup = new TestConnection();
            _commentRepo = new CommentRepoSqlite(_dbSetup.Connection);
            _userRepo = new UserRepoSqlite(_dbSetup.Connection);
            _postRepo = new PostRepoSqlite(_dbSetup.Connection);

            var author = TestDataFactory.CreateTestUser(id: 1, username: "author");
            _userRepo.Save(author);

            var post = TestDataFactory.CreateTestPost(id: 1, authorId: 1);
            _postRepo.Save(post);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbSetup?.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullConnection_ShouldThrowArgumentNullException()
        {
            var repo = new CommentRepoSqlite(null);
        }

        #region Save Tests

        [TestMethod]
        public void Save_ValidComment_ShouldSaveSuccessfully()
        {
            var comment = TestDataFactory.CreateTestComment(id: 1, postId: 1, authorId: 1, content: "Great post!");

            _commentRepo.Save(comment);

            var retrieved = _commentRepo.FindById(new CommentId { Value = 1 });
            Assert.IsNotNull(retrieved);
            Assert.AreEqual("Great post!", retrieved.Content);
        }

        [TestMethod]
        public void Save_MultipleComments_ShouldSaveAll()
        {
            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 1, postId: 1, authorId: 1, content: "Comment 1"));
            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 2, postId: 1, authorId: 1, content: "Comment 2"));
            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 3, postId: 1, authorId: 1, content: "Comment 3"));

            var comments = _commentRepo.FindByPost(new PostId { Value = 1 });
            Assert.AreEqual(3, comments.Count);
        }

        [TestMethod]
        public void Save_WithLongContent_ShouldHandleSuccessfully()
        {
            var longContent = new string('a', 5000);
            var comment = TestDataFactory.CreateTestComment(id: 1, postId: 1, authorId: 1, content: longContent);

            _commentRepo.Save(comment);

            var retrieved = _commentRepo.FindById(new CommentId { Value = 1 });
            Assert.AreEqual(5000, retrieved.Content.Length);
        }

        #endregion

        #region FindById Tests

        [TestMethod]
        public void FindById_ExistingComment_ShouldReturnComment()
        {
            var originalComment = TestDataFactory.CreateTestComment(id: 1, postId: 1, authorId: 1, content: "Test comment");
            _commentRepo.Save(originalComment);

            var retrieved = _commentRepo.FindById(new CommentId { Value = 1 });

            Assert.IsNotNull(retrieved);
            Assert.AreEqual("Test comment", retrieved.Content);
        }

        [TestMethod]
        public void FindById_NonExistingComment_ShouldThrowKeyNotFoundException()
        {
            Assert.ThrowsException<KeyNotFoundException>(() =>
                _commentRepo.FindById(new CommentId { Value = 999 }));
        }

        [TestMethod]
        public void FindById_PreservesAllData_ShouldMatchOriginal()
        {
            var originalComment = TestDataFactory.CreateTestComment(id: 5, postId: 1, authorId: 1, content: "Preserve test");

            _commentRepo.Save(originalComment);
            var retrieved = _commentRepo.FindById(new CommentId { Value = 5 });

            Assert.IsTrue(EntityComparison.CommentsAreEqual(originalComment, retrieved));
        }

        #endregion

        #region FindByPost Tests

        [TestMethod]
        public void FindByPost_NoComments_ShouldReturnEmptyList()
        {
            var comments = _commentRepo.FindByPost(new PostId { Value = 1 });

            Assert.AreEqual(0, comments.Count);
        }

        [TestMethod]
        public void FindByPost_MultipleComments_ShouldReturnAllForPost()
        {
            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 1, postId: 1, authorId: 1, content: "Comment1"));
            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 2, postId: 1, authorId: 1, content: "Comment2"));
            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 3, postId: 1, authorId: 1, content: "Comment3"));

            var comments = _commentRepo.FindByPost(new PostId { Value = 1 });

            Assert.AreEqual(3, comments.Count);
        }

        [TestMethod]
        public void FindByPost_OnlyReturnsCommentsForSpecificPost()
        {
            var post2 = TestDataFactory.CreateTestPost(id: 2, authorId: 1);
            _postRepo.Save(post2);

            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 1, postId: 1, authorId: 1, content: "Post1 Comment"));
            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 2, postId: 2, authorId: 1, content: "Post2 Comment"));

            var post1Comments = _commentRepo.FindByPost(new PostId { Value = 1 });
            var post2Comments = _commentRepo.FindByPost(new PostId { Value = 2 });

            Assert.AreEqual(1, post1Comments.Count);
            Assert.AreEqual(1, post2Comments.Count);
            Assert.AreEqual("Post1 Comment", post1Comments[0].Content);
            Assert.AreEqual("Post2 Comment", post2Comments[0].Content);
        }

        [TestMethod]
        public void FindByPost_DifferentPosts_ReturnCorrectComments()
        {
            var post2 = TestDataFactory.CreateTestPost(id: 2, authorId: 1);
            var post3 = TestDataFactory.CreateTestPost(id: 3, authorId: 1);
            _postRepo.Save(post2);
            _postRepo.Save(post3);

            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 1, postId: 1, authorId: 1, content: "C1"));
            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 2, postId: 1, authorId: 1, content: "C2"));
            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 3, postId: 2, authorId: 1, content: "C3"));
            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 4, postId: 3, authorId: 1, content: "C4"));

            var post1Comments = _commentRepo.FindByPost(new PostId { Value = 1 });
            var post2Comments = _commentRepo.FindByPost(new PostId { Value = 2 });
            var post3Comments = _commentRepo.FindByPost(new PostId { Value = 3 });

            Assert.AreEqual(2, post1Comments.Count);
            Assert.AreEqual(1, post2Comments.Count);
            Assert.AreEqual(1, post3Comments.Count);
        }

        #endregion

        #region Delete Tests

        [TestMethod]
        public void Delete_ExistingComment_ShouldDeleteSuccessfully()
        {
            var comment = TestDataFactory.CreateTestComment(id: 1, postId: 1, authorId: 1, content: "To delete");
            _commentRepo.Save(comment);

            _commentRepo.Delete(new CommentId { Value = 1 });

            Assert.ThrowsException<KeyNotFoundException>(() =>
                _commentRepo.FindById(new CommentId { Value = 1 }));
        }

        [TestMethod]
        public void Delete_NonExistingComment_ShouldNotThrowException()
        {
            _commentRepo.Delete(new CommentId { Value = 999 });
        }

        [TestMethod]
        public void Delete_SpecificComment_OthersRemainUnaffected()
        {
            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 1, postId: 1, authorId: 1, content: "Comment1"));
            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 2, postId: 1, authorId: 1, content: "Comment2"));
            _commentRepo.Save(TestDataFactory.CreateTestComment(id: 3, postId: 1, authorId: 1, content: "Comment3"));

            _commentRepo.Delete(new CommentId { Value = 2 });

            var remaining = _commentRepo.FindByPost(new PostId { Value = 1 });
            Assert.AreEqual(2, remaining.Count);
            Assert.IsTrue(remaining.Any(c => c.Content == "Comment1"));
            Assert.IsTrue(remaining.Any(c => c.Content == "Comment3"));
            Assert.IsFalse(remaining.Any(c => c.Content == "Comment2"));
        }

        #endregion

        #region Edge Cases Tests

        [TestMethod]
        public void FindByPost_LargeNumberOfComments_ShouldHandleSuccessfully()
        {
            for (int i = 1; i <= 100; i++)
            {
                _commentRepo.Save(TestDataFactory.CreateTestComment(id: (uint)i, postId: 1, authorId: 1, content: $"Comment{i}"));
            }

            var comments = _commentRepo.FindByPost(new PostId { Value = 1 });

            Assert.AreEqual(100, comments.Count);
        }

        #endregion
    }
}