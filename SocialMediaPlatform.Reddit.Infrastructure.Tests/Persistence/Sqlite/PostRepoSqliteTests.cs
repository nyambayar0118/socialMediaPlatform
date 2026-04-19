using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Repositories;
using SocialMediaPlatform.Reddit.Infrastructure.Tests.Factories;
using SocialMediaPlatform.Reddit.Infrastructure.Tests.Connections;

namespace SocialMediaPlatform.Reddit.Infrastructure.Tests.Persistence.Sqlite
{
    [TestClass]
    public class PostRepoSqliteTests
    {
        private TestConnection _dbSetup;
        private PostRepoSqlite _postRepo;
        private UserRepoSqlite _userRepo;

        [TestInitialize]
        public void Setup()
        {
            _dbSetup = new TestConnection();
            _postRepo = new PostRepoSqlite(_dbSetup.Connection);
            _userRepo = new UserRepoSqlite(_dbSetup.Connection);

            var author = TestDataFactory.CreateTestUser(id: 1, username: "author");
            _userRepo.Save(author);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbSetup?.Dispose();
        }

        #region Save Tests

        [TestMethod]
        public void Save_ValidPost_ShouldSaveSuccessfully()
        {
            var post = TestDataFactory.CreateTestPost(id: 1, authorId: 1, title: "Test Post", content: "Test content");

            _postRepo.Save(post);

            var retrieved = _postRepo.FindById(new PostId { Value = 1 });
            Assert.IsNotNull(retrieved);
            Assert.AreEqual("Test Post", (retrieved as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost)?.Title);
        }

        [TestMethod]
        public void Save_MultiplePostsByDifferentAuthors_ShouldSaveAll()
        {
            var author2 = TestDataFactory.CreateTestUser(id: 2, username: "author2");
            _userRepo.Save(author2);

            var post1 = TestDataFactory.CreateTestPost(id: 1, authorId: 1, title: "Post1");
            var post2 = TestDataFactory.CreateTestPost(id: 2, authorId: 2, title: "Post2");

            _postRepo.Save(post1);
            _postRepo.Save(post2);

            var allPosts = _postRepo.FindAll();
            Assert.AreEqual(2, allPosts.Count);
        }

        [TestMethod]
        public void Save_WithLongContent_ShouldHandleSuccessfully()
        {
            var longContent = new string('a', 10000);
            var post = TestDataFactory.CreateTestPost(id: 1, authorId: 1, content: longContent);

            _postRepo.Save(post);

            var retrieved = _postRepo.FindById(new PostId { Value = 1 });
            Assert.AreEqual(10000, (retrieved as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost)?.Content.Length);
        }

        #endregion

        #region FindById Tests

        [TestMethod]
        public void FindById_ExistingPost_ShouldReturnPost()
        {
            var originalPost = TestDataFactory.CreateTestPost(id: 1, authorId: 1, title: "Find Me");
            _postRepo.Save(originalPost);

            var retrieved = _postRepo.FindById(new PostId { Value = 1 });

            Assert.IsNotNull(retrieved);
            Assert.AreEqual("Find Me", (retrieved as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost)?.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void FindById_NonExistingPost_ShouldThrowKeyNotFoundException()
        {
            _postRepo.FindById(new PostId { Value = 999 });
        }

        [TestMethod]
        public void FindById_PreservesAllData_ShouldMatchOriginal()
        {
            var originalPost = TestDataFactory.CreateTestPost(id: 5, authorId: 1, title: "Preserve Test", content: "Content Test");

            _postRepo.Save(originalPost);
            var retrieved = _postRepo.FindById(new PostId { Value = 5 });

            Assert.IsTrue(EntityComparison.PostsAreEqual(originalPost, retrieved));
        }

        #endregion

        #region FindAll Tests

        [TestMethod]
        public void FindAll_NoPosts_ShouldReturnEmptyList()
        {
            var posts = _postRepo.FindAll();

            Assert.AreEqual(0, posts.Count);
        }

        [TestMethod]
        public void FindAll_MultiplePosts_ShouldReturnAllPosts()
        {
            _postRepo.Save(TestDataFactory.CreateTestPost(id: 1, authorId: 1, title: "Post1"));
            _postRepo.Save(TestDataFactory.CreateTestPost(id: 2, authorId: 1, title: "Post2"));
            _postRepo.Save(TestDataFactory.CreateTestPost(id: 3, authorId: 1, title: "Post3"));

            var posts = _postRepo.FindAll();

            Assert.AreEqual(3, posts.Count);
        }

        [TestMethod]
        public void FindAll_ShouldReturnOrderedByCreatedAtDescending()
        {
            var post1 = TestDataFactory.CreateTestPost(id: 1, authorId: 1, title: "First");
            var post2 = TestDataFactory.CreateTestPost(id: 2, authorId: 1, title: "Second");

            _postRepo.Save(post1);
            System.Threading.Thread.Sleep(10);
            _postRepo.Save(post2);

            var posts = _postRepo.FindAll();

            Assert.AreEqual(2, posts.Count);
            Assert.AreEqual("Second", (posts[0] as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost)?.Title);
            Assert.AreEqual("First", (posts[1] as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost)?.Title);
        }

        #endregion

        #region Update Tests

        [TestMethod]
        public void Update_ExistingPost_ShouldUpdateSuccessfully()
        {
            var post = TestDataFactory.CreateTestPost(id: 1, authorId: 1, title: "Original", content: "Original content");
            _postRepo.Save(post);

            (post as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost).Title = "Updated";
            (post as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost).Content = "Updated content";
            _postRepo.Update(post);

            var retrieved = _postRepo.FindById(new PostId { Value = 1 });
            Assert.AreEqual("Updated", (retrieved as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost)?.Title);
            Assert.AreEqual("Updated content", (retrieved as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost)?.Content);
        }

        [TestMethod]
        public void Update_NonExistingPost_ShouldNotThrowException()
        {
            var post = TestDataFactory.CreateTestPost(id: 999, authorId: 1, title: "Ghost");
            _postRepo.Update(post);
        }

        [TestMethod]
        public void Update_PartialUpdate_ShouldPreserveOtherFields()
        {
            var post = TestDataFactory.CreateTestPost(id: 1, authorId: 1, title: "Original", content: "Content");
            _postRepo.Save(post);

            var retrieved = _postRepo.FindById(new PostId { Value = 1 });
            (retrieved as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost).Title = "New Title";
            _postRepo.Update(retrieved);

            var updated = _postRepo.FindById(new PostId { Value = 1 });
            Assert.AreEqual("New Title", (updated as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost)?.Title);
            Assert.AreEqual("Content", (updated as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost)?.Content);
        }

        #endregion

        #region Delete Tests

        [TestMethod]
        public void Delete_ExistingPost_ShouldDeleteSuccessfully()
        {
            var post = TestDataFactory.CreateTestPost(id: 1, authorId: 1, title: "To Delete");
            _postRepo.Save(post);

            _postRepo.Delete(new PostId { Value = 1 });

            Assert.ThrowsException<KeyNotFoundException>(() => _postRepo.FindById(new PostId { Value = 1 }));
        }

        [TestMethod]
        public void Delete_NonExistingPost_ShouldNotThrowException()
        {
            _postRepo.Delete(new PostId { Value = 999 });
        }

        [TestMethod]
        public void Delete_SpecificPost_OthersRemainUnaffected()
        {
            _postRepo.Save(TestDataFactory.CreateTestPost(id: 1, authorId: 1, title: "Post1"));
            _postRepo.Save(TestDataFactory.CreateTestPost(id: 2, authorId: 1, title: "Post2"));
            _postRepo.Save(TestDataFactory.CreateTestPost(id: 3, authorId: 1, title: "Post3"));

            _postRepo.Delete(new PostId { Value = 2 });

            var remaining = _postRepo.FindAll();
            Assert.AreEqual(2, remaining.Count);
            Assert.IsTrue(remaining.Any(p => (p as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost)?.Title == "Post1"));
            Assert.IsTrue(remaining.Any(p => (p as SocialMediaPlatform.Reddit.Core.Domain.Posts.TimelinePost)?.Title == "Post3"));
        }

        #endregion
    }
}