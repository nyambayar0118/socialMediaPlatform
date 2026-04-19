using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Repositories;
using SocialMediaPlatform.Reddit.Infrastructure.Tests.Factories;
using SocialMediaPlatform.Reddit.Infrastructure.Tests.Connections;

namespace SocialMediaPlatform.Reddit.Infrastructure.Tests.Persistence.Sqlite
{
    [TestClass]
    public class ReactionRepoSqliteTests
    {
        private TestConnection _dbSetup;
        private ReactionRepoSqlite _reactionRepo;
        private UserRepoSqlite _userRepo;

        [TestInitialize]
        public void Setup()
        {
            _dbSetup = new TestConnection();
            _reactionRepo = new ReactionRepoSqlite(_dbSetup.Connection);
            _userRepo = new UserRepoSqlite(_dbSetup.Connection);

            for (int i = 1; i <= 5; i++)
            {
                var user = TestDataFactory.CreateTestUser(id: (uint)i, username: $"user{i}");
                _userRepo.Save(user);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbSetup?.Dispose();
        }

        #region Save Tests

        [TestMethod]
        public void Save_ValidUpvote_ShouldSaveSuccessfully()
        {
            var upvote = TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1, targetType: ReactionTargetType.Post);

            _reactionRepo.Save(upvote);

            var counts = _reactionRepo.CountByTarget(1, ReactionTargetType.Post);
            Assert.AreEqual(1u, counts["Upvote"]);
        }

        [TestMethod]
        public void Save_ValidDownvote_ShouldSaveSuccessfully()
        {
            var downvote = TestDataFactory.CreateTestDownvote(targetId: 1, authorId: 1, targetType: ReactionTargetType.Post);

            _reactionRepo.Save(downvote);

            var counts = _reactionRepo.CountByTarget(1, ReactionTargetType.Post);
            Assert.AreEqual(1u, counts["Downvote"]);
        }

        [TestMethod]
        public void Save_MultipleReactions_ShouldSaveAll()
        {
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1));
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 2));
            _reactionRepo.Save(TestDataFactory.CreateTestDownvote(targetId: 1, authorId: 3));

            var counts = _reactionRepo.CountByTarget(1, ReactionTargetType.Post);
            Assert.AreEqual(2u, counts["Upvote"]);
            Assert.AreEqual(1u, counts["Downvote"]);
        }

        [TestMethod]
        public void Save_DifferentTargets_ShouldSeparateCorrectly()
        {
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1));
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 2, authorId: 1));

            var counts1 = _reactionRepo.CountByTarget(1, ReactionTargetType.Post);
            var counts2 = _reactionRepo.CountByTarget(2, ReactionTargetType.Post);

            Assert.AreEqual(1u, counts1["Upvote"]);
            Assert.AreEqual(1u, counts2["Upvote"]);
        }

        [TestMethod]
        public void Save_CommentReactions_ShouldCountSeparately()
        {
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1, targetType: ReactionTargetType.Post));
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 2, targetType: ReactionTargetType.Comment));

            var postCounts = _reactionRepo.CountByTarget(1, ReactionTargetType.Post);
            var commentCounts = _reactionRepo.CountByTarget(1, ReactionTargetType.Comment);

            Assert.AreEqual(1u, postCounts["Upvote"]);
            Assert.AreEqual(1u, commentCounts["Upvote"]);
        }

        #endregion

        #region Delete Tests

        [TestMethod]
        public void Delete_ExistingReaction_ShouldDeleteSuccessfully()
        {
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1));

            _reactionRepo.Delete(1, new UserId { Value = 1 });

            var counts = _reactionRepo.CountByTarget(1, ReactionTargetType.Post);
            Assert.IsFalse(counts.ContainsKey("Upvote") || counts["Upvote"] == 0);
        }

        [TestMethod]
        public void Delete_NonExistingReaction_ShouldNotThrowException()
        {
            _reactionRepo.Delete(999, new UserId { Value = 999 });
        }

        [TestMethod]
        public void Delete_SpecificReaction_OthersRemainUnaffected()
        {
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1));
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 2));
            _reactionRepo.Save(TestDataFactory.CreateTestDownvote(targetId: 1, authorId: 3));

            _reactionRepo.Delete(1, new UserId { Value = 1 });

            var counts = _reactionRepo.CountByTarget(1, ReactionTargetType.Post);
            Assert.AreEqual(1u, counts["Upvote"]);
            Assert.AreEqual(1u, counts["Downvote"]);
        }

        #endregion

        #region CountByTarget Tests

        [TestMethod]
        public void CountByTarget_NoReactions_ShouldReturnEmptyDictionary()
        {
            var counts = _reactionRepo.CountByTarget(1, ReactionTargetType.Post);

            Assert.AreEqual(0, counts.Count);
        }

        [TestMethod]
        public void CountByTarget_MixedReactions_ShouldReturnAccurateCounts()
        {
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1));
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 2));
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 3));
            _reactionRepo.Save(TestDataFactory.CreateTestDownvote(targetId: 1, authorId: 4));
            _reactionRepo.Save(TestDataFactory.CreateTestDownvote(targetId: 1, authorId: 5));

            var counts = _reactionRepo.CountByTarget(1, ReactionTargetType.Post);

            Assert.AreEqual(3u, counts["Upvote"]);
            Assert.AreEqual(2u, counts["Downvote"]);
        }

        [TestMethod]
        public void CountByTarget_OnlyUpvotes_ShouldNotIncludeDownvotes()
        {
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1));
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 2));

            var counts = _reactionRepo.CountByTarget(1, ReactionTargetType.Post);

            Assert.AreEqual(2u, counts["Upvote"]);
            Assert.IsFalse(counts.ContainsKey("Downvote"));
        }

        [TestMethod]
        public void CountByTarget_DifferentTargetTypes_ShouldNotMix()
        {
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1, targetType: ReactionTargetType.Post));
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 2, targetType: ReactionTargetType.Comment));

            var postCounts = _reactionRepo.CountByTarget(1, ReactionTargetType.Post);
            var commentCounts = _reactionRepo.CountByTarget(1, ReactionTargetType.Comment);

            Assert.AreEqual(1u, postCounts["Upvote"]);
            Assert.AreEqual(1u, commentCounts["Upvote"]);
        }

        #endregion

        #region ExistsByUserAndTarget Tests

        [TestMethod]
        public void ExistsByUserAndTarget_ExistingReaction_ShouldReturnTrue()
        {
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1));

            var exists = _reactionRepo.ExistsByUserAndTarget(new UserId { Value = 1 }, 1, ReactionTargetType.Post);

            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void ExistsByUserAndTarget_NonExistingReaction_ShouldReturnFalse()
        {
            var exists = _reactionRepo.ExistsByUserAndTarget(new UserId { Value = 1 }, 1, ReactionTargetType.Post);

            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void ExistsByUserAndTarget_DifferentUser_ShouldReturnFalse()
        {
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1));

            var exists = _reactionRepo.ExistsByUserAndTarget(new UserId { Value = 2 }, 1, ReactionTargetType.Post);

            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void ExistsByUserAndTarget_DifferentTarget_ShouldReturnFalse()
        {
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1));

            var exists = _reactionRepo.ExistsByUserAndTarget(new UserId { Value = 1 }, 2, ReactionTargetType.Post);

            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void ExistsByUserAndTarget_DifferentTargetType_ShouldReturnFalse()
        {
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1, targetType: ReactionTargetType.Post));

            var exists = _reactionRepo.ExistsByUserAndTarget(new UserId { Value = 1 }, 1, ReactionTargetType.Comment);

            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void ExistsByUserAndTarget_UserReactedMultipleTimes_ShouldReturnTrue()
        {
            _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: 1));
            _reactionRepo.Delete(1, new UserId { Value = 1 });
            _reactionRepo.Save(TestDataFactory.CreateTestDownvote(targetId: 1, authorId: 1));

            var exists = _reactionRepo.ExistsByUserAndTarget(new UserId { Value = 1 }, 1, ReactionTargetType.Post);

            Assert.IsTrue(exists);
        }

        #endregion

        #region Edge Cases Tests

        [TestMethod]
        public void CountByTarget_LargeNumberOfReactions_ShouldHandleSuccessfully()
        {
            for (int i = 1; i <= 50; i++)
            {
                if (i % 2 == 0)
                    _reactionRepo.Save(TestDataFactory.CreateTestUpvote(targetId: 1, authorId: (uint)i));
                else
                    _reactionRepo.Save(TestDataFactory.CreateTestDownvote(targetId: 1, authorId: (uint)i));
            }

            var counts = _reactionRepo.CountByTarget(1, ReactionTargetType.Post);

            Assert.AreEqual(25u, counts["Upvote"]);
            Assert.AreEqual(25u, counts["Downvote"]);
        }

        #endregion
    }
}