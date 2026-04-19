using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Repositories;
using SocialMediaPlatform.Reddit.Infrastructure.Tests.Connections;

namespace SocialMediaPlatform.Reddit.Infrastructure.Tests.Persistence.Sqlite
{
    [TestClass]
    public class SequentialIdRepoSqliteTests
    {
        private TestConnection _dbSetup;
        private SequentialIdRepoSqlite _idRepo;

        [TestInitialize]
        public void Setup()
        {
            _dbSetup = new TestConnection();
            _idRepo = new SequentialIdRepoSqlite(_dbSetup.Connection);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbSetup?.Dispose();
        }

        #region GetLastId Tests

        [TestMethod]
        public void GetLastId_NoExistingId_ShouldReturnZero()
        {
            var lastId = _idRepo.GetLastId(IdEntityType.User);

            Assert.AreEqual(0u, lastId);
        }

        [TestMethod]
        public void GetLastId_ExistingId_ShouldReturnCorrectValue()
        {
            _idRepo.SaveLastId(IdEntityType.User, 42);

            var lastId = _idRepo.GetLastId(IdEntityType.User);

            Assert.AreEqual(42u, lastId);
        }

        [TestMethod]
        public void GetLastId_DifferentEntityTypes_ShouldReturnSeparateValues()
        {
            _idRepo.SaveLastId(IdEntityType.User, 10);
            _idRepo.SaveLastId(IdEntityType.Post, 20);
            _idRepo.SaveLastId(IdEntityType.Comment, 30);

            var userLastId = _idRepo.GetLastId(IdEntityType.User);
            var postLastId = _idRepo.GetLastId(IdEntityType.Post);
            var commentLastId = _idRepo.GetLastId(IdEntityType.Comment);

            Assert.AreEqual(10u, userLastId);
            Assert.AreEqual(20u, postLastId);
            Assert.AreEqual(30u, commentLastId);
        }

        #endregion

        #region SaveLastId Tests

        [TestMethod]
        public void SaveLastId_NewId_ShouldSaveSuccessfully()
        {
            _idRepo.SaveLastId(IdEntityType.User, 100);

            var retrievedId = _idRepo.GetLastId(IdEntityType.User);

            Assert.AreEqual(100u, retrievedId);
        }

        [TestMethod]
        public void SaveLastId_UpdateExistingId_ShouldUpdateSuccessfully()
        {
            _idRepo.SaveLastId(IdEntityType.User, 50);
            _idRepo.SaveLastId(IdEntityType.User, 75);

            var retrievedId = _idRepo.GetLastId(IdEntityType.User);

            Assert.AreEqual(75u, retrievedId);
        }

        [TestMethod]
        public void SaveLastId_MaxUintValue_ShouldHandleSuccessfully()
        {
            _idRepo.SaveLastId(IdEntityType.User, uint.MaxValue);

            var retrievedId = _idRepo.GetLastId(IdEntityType.User);

            Assert.AreEqual(uint.MaxValue, retrievedId);
        }

        [TestMethod]
        public void SaveLastId_Zero_ShouldSaveSuccessfully()
        {
            _idRepo.SaveLastId(IdEntityType.User, 0);

            var retrievedId = _idRepo.GetLastId(IdEntityType.User);

            Assert.AreEqual(0u, retrievedId);
        }

        #endregion

        #region Isolation Tests

        [TestMethod]
        public void SaveLastId_DifferentTypes_ShouldNotAffectEachOther()
        {
            _idRepo.SaveLastId(IdEntityType.User, 100);
            _idRepo.SaveLastId(IdEntityType.Post, 200);
            _idRepo.SaveLastId(IdEntityType.Comment, 300);

            _idRepo.SaveLastId(IdEntityType.User, 150);

            var userLastId = _idRepo.GetLastId(IdEntityType.User);
            var postLastId = _idRepo.GetLastId(IdEntityType.Post);
            var commentLastId = _idRepo.GetLastId(IdEntityType.Comment);

            Assert.AreEqual(150u, userLastId);
            Assert.AreEqual(200u, postLastId);
            Assert.AreEqual(300u, commentLastId);
        }

        [TestMethod]
        public void GetLastId_AllEntityTypes_ShouldReturnIndependentValues()
        {
            _idRepo.SaveLastId(IdEntityType.User, 11);
            _idRepo.SaveLastId(IdEntityType.Post, 22);
            _idRepo.SaveLastId(IdEntityType.Comment, 33);

            Assert.AreEqual(11u, _idRepo.GetLastId(IdEntityType.User));
            Assert.AreEqual(22u, _idRepo.GetLastId(IdEntityType.Post));
            Assert.AreEqual(33u, _idRepo.GetLastId(IdEntityType.Comment));
        }

        #endregion

        #region Sequential Behavior Tests

        [TestMethod]
        public void SaveLastId_SequentialIncrement_ShouldTrackProgression()
        {
            _idRepo.SaveLastId(IdEntityType.User, 1);
            Assert.AreEqual(1u, _idRepo.GetLastId(IdEntityType.User));

            _idRepo.SaveLastId(IdEntityType.User, 2);
            Assert.AreEqual(2u, _idRepo.GetLastId(IdEntityType.User));

            _idRepo.SaveLastId(IdEntityType.User, 3);
            Assert.AreEqual(3u, _idRepo.GetLastId(IdEntityType.User));
        }

        [TestMethod]
        public void SaveLastId_NonSequentialValues_ShouldHandleGaps()
        {
            _idRepo.SaveLastId(IdEntityType.User, 10);
            _idRepo.SaveLastId(IdEntityType.User, 50);
            _idRepo.SaveLastId(IdEntityType.User, 100);

            var lastId = _idRepo.GetLastId(IdEntityType.User);

            Assert.AreEqual(100u, lastId);
        }

        #endregion

        #region Persistence Tests

        [TestMethod]
        public void SaveLastId_PersistsAcrossMultipleCalls()
        {
            _idRepo.SaveLastId(IdEntityType.User, 999);

            for (int i = 0; i < 5; i++)
            {
                var id = _idRepo.GetLastId(IdEntityType.User);
                Assert.AreEqual(999u, id);
            }
        }

        #endregion

        #region Edge Cases Tests

        [TestMethod]
        public void SaveLastId_LargeValue_ShouldHandleSuccessfully()
        {
            var largeValue = uint.MaxValue - 1000;
            _idRepo.SaveLastId(IdEntityType.User, largeValue);

            var retrievedId = _idRepo.GetLastId(IdEntityType.User);

            Assert.AreEqual(largeValue, retrievedId);
        }

        [TestMethod]
        public void SaveLastId_OverwriteWithSmallValue_ShouldAllowDecrement()
        {
            _idRepo.SaveLastId(IdEntityType.User, 1000);
            _idRepo.SaveLastId(IdEntityType.User, 500);

            var retrievedId = _idRepo.GetLastId(IdEntityType.User);

            Assert.AreEqual(500u, retrievedId);
        }

        [TestMethod]
        public void SaveLastId_MultipleEntityTypes_AllIndependent()
        {
            _idRepo.SaveLastId(IdEntityType.User, 1);
            _idRepo.SaveLastId(IdEntityType.Post, 2);
            _idRepo.SaveLastId(IdEntityType.Comment, 3);

            var user = _idRepo.GetLastId(IdEntityType.User);
            var post = _idRepo.GetLastId(IdEntityType.Post);
            var comment = _idRepo.GetLastId(IdEntityType.Comment);

            Assert.AreEqual(1u, user);
            Assert.AreEqual(2u, post);
            Assert.AreEqual(3u, comment);
        }

        #endregion
    }
}