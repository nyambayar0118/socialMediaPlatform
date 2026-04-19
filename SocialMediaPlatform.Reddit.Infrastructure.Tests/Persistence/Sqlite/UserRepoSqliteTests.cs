using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Repositories;
using SocialMediaPlatform.Reddit.Infrastructure.Tests.Factories;
using SocialMediaPlatform.Reddit.Infrastructure.Tests.Connections;

namespace SocialMediaPlatform.Reddit.Infrastructure.Tests.Persistence.Sqlite
{
    [TestClass]
    public class UserRepoSqliteTests
    {
        private TestConnection _dbSetup;
        private UserRepoSqlite _userRepo;

        [TestInitialize]
        public void Setup()
        {
            _dbSetup = new TestConnection();
            _userRepo = new UserRepoSqlite(_dbSetup.Connection);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbSetup?.Dispose();
        }

        #region Save Tests

        [TestMethod]
        public void Save_ValidUser_ShouldSaveSuccessfully()
        {
            var user = TestDataFactory.CreateTestUser(id: 1, username: "john_doe");

            _userRepo.Save(user);

            var retrieved = _userRepo.FindById(new UserId { Value = 1 });
            Assert.IsNotNull(retrieved);
            Assert.AreEqual("john_doe", retrieved.Username);
        }

        [TestMethod]
        public void Save_MultipleUsers_ShouldSaveAllSuccessfully()
        {
            var user1 = TestDataFactory.CreateTestUser(id: 1, username: "user1");
            var user2 = TestDataFactory.CreateTestUser(id: 2, username: "user2");
            var user3 = TestDataFactory.CreateTestUser(id: 3, username: "user3");

            _userRepo.Save(user1);
            _userRepo.Save(user2);
            _userRepo.Save(user3);

            var allUsers = _userRepo.FindAll();
            Assert.AreEqual(3, allUsers.Count);
        }

        [TestMethod]
        public void Save_WithProfilePicture_ShouldSaveProfilePath()
        {
            var user = TestDataFactory.CreateTestUser(id: 1);
            user.ProfilePicturePath = "/path/to/picture.jpg";

            _userRepo.Save(user);

            var retrieved = _userRepo.FindById(new UserId { Value = 1 });
            Assert.AreEqual("/path/to/picture.jpg", retrieved.ProfilePicturePath);
        }

        [TestMethod]
        [ExpectedException(typeof(Microsoft.Data.Sqlite.SqliteException))]
        public void Save_DuplicateUsername_ShouldThrowException()
        {
            var user1 = TestDataFactory.CreateTestUser(id: 1, username: "duplicate");
            var user2 = TestDataFactory.CreateTestUser(id: 2, username: "duplicate");

            _userRepo.Save(user1);
            _userRepo.Save(user2);
        }

        #endregion

        #region FindById Tests

        [TestMethod]
        public void FindById_ExistingUser_ShouldReturnUser()
        {
            var originalUser = TestDataFactory.CreateTestUser(id: 1, username: "alice");
            _userRepo.Save(originalUser);

            var retrieved = _userRepo.FindById(new UserId { Value = 1 });

            Assert.IsNotNull(retrieved);
            Assert.AreEqual("alice", retrieved.Username);
            Assert.AreEqual(1u, retrieved.Id.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void FindById_NonExistingUser_ShouldThrowKeyNotFoundException()
        {
            _userRepo.FindById(new UserId { Value = 999 });
        }

        [TestMethod]
        public void FindById_PreservesAllData_ShouldMatchOriginal()
        {
            var originalUser = TestDataFactory.CreateTestUser(id: 5, username: "preserve_test", email: "preserve@test.com");

            _userRepo.Save(originalUser);
            var retrieved = _userRepo.FindById(new UserId { Value = 5 });

            Assert.IsTrue(EntityComparison.UsersAreEqual(originalUser, retrieved));
        }

        #endregion

        #region FindByUsername Tests

        [TestMethod]
        public void FindByUsername_ExistingUsername_ShouldReturnUser()
        {
            var user = TestDataFactory.CreateTestUser(id: 1, username: "testuser");
            _userRepo.Save(user);

            var retrieved = _userRepo.FindByUsername("testuser");

            Assert.IsNotNull(retrieved);
            Assert.AreEqual("testuser", retrieved.Username);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void FindByUsername_NonExistingUsername_ShouldThrowKeyNotFoundException()
        {
            _userRepo.FindByUsername("nonexistent");
        }

        [TestMethod]
        public void FindByUsername_CaseSensitive_ShouldNotMatchDifferentCase()
        {
            var user = TestDataFactory.CreateTestUser(id: 1, username: "TestUser");
            _userRepo.Save(user);

            Assert.ThrowsException<KeyNotFoundException>(() => _userRepo.FindByUsername("testuser"));
        }

        #endregion

        #region Update Tests

        [TestMethod]
        public void Update_ExistingUser_ShouldUpdateSuccessfully()
        {
            var user = TestDataFactory.CreateTestUser(id: 1, username: "original", email: "original@test.com");
            _userRepo.Save(user);

            user.Email = "updated@test.com";
            user.ProfilePicturePath = "/new/path.jpg";
            _userRepo.Update(user);

            var retrieved = _userRepo.FindById(new UserId { Value = 1 });
            Assert.AreEqual("original", retrieved.Username);
            Assert.AreEqual("updated@test.com", retrieved.Email);
            Assert.AreEqual("/new/path.jpg", retrieved.ProfilePicturePath);
        }

        [TestMethod]
        public void Update_UserPassword_ShouldUpdatePasswordOnly()
        {
            var user = TestDataFactory.CreateTestUser(id: 1, username: "bob");
            _userRepo.Save(user);

            user.Password = "newhashedpassword";
            _userRepo.Update(user);

            var retrieved = _userRepo.FindById(new UserId { Value = 1 });
            Assert.AreEqual("newhashedpassword", retrieved.Password);
        }

        [TestMethod]
        public void Update_NonExistingUser_ShouldNotThrowException()
        {
            var user = TestDataFactory.CreateTestUser(id: 999, username: "ghost");
            _userRepo.Update(user);
        }

        #endregion

        #region Delete Tests

        [TestMethod]
        public void Delete_ExistingUser_ShouldDeleteSuccessfully()
        {
            var user = TestDataFactory.CreateTestUser(id: 1, username: "todelete");
            _userRepo.Save(user);

            _userRepo.Delete(new UserId { Value = 1 });

            Assert.ThrowsException<KeyNotFoundException>(() => _userRepo.FindById(new UserId { Value = 1 }));
        }

        [TestMethod]
        public void Delete_NonExistingUser_ShouldNotThrowException()
        {
            _userRepo.Delete(new UserId { Value = 999 });   
        }

        [TestMethod]
        public void Delete_SpecificUser_OthersRemainUnaffected()
        {
            _userRepo.Save(TestDataFactory.CreateTestUser(id: 1, username: "user1"));
            _userRepo.Save(TestDataFactory.CreateTestUser(id: 2, username: "user2"));
            _userRepo.Save(TestDataFactory.CreateTestUser(id: 3, username: "user3"));

            _userRepo.Delete(new UserId { Value = 2 });

            var remaining = _userRepo.FindAll();
            Assert.AreEqual(2, remaining.Count);
            Assert.IsTrue(remaining.Any(u => u.Username == "user1"));
            Assert.IsTrue(remaining.Any(u => u.Username == "user3"));
            Assert.IsFalse(remaining.Any(u => u.Username == "user2"));
        }

        #endregion

        #region FindAll Tests

        [TestMethod]
        public void FindAll_NoUsers_ShouldReturnEmptyList()
        {
            var users = _userRepo.FindAll();

            Assert.AreEqual(0, users.Count);
        }

        [TestMethod]
        public void FindAll_MultipleUsers_ShouldReturnAllUsers()
        {
            _userRepo.Save(TestDataFactory.CreateTestUser(id: 1, username: "user1"));
            _userRepo.Save(TestDataFactory.CreateTestUser(id: 2, username: "user2"));
            _userRepo.Save(TestDataFactory.CreateTestUser(id: 3, username: "user3"));

            var users = _userRepo.FindAll();

            Assert.AreEqual(3, users.Count);
        }

        [TestMethod]
        public void FindAll_ShouldReturnOrderedByCreatedAt()
        {
            var user1 = TestDataFactory.CreateTestUser(id: 1, username: "first");
            var user2 = TestDataFactory.CreateTestUser(id: 2, username: "second");

            _userRepo.Save(user1);
            System.Threading.Thread.Sleep(10);
            _userRepo.Save(user2);

            var users = _userRepo.FindAll();

            Assert.AreEqual(2, users.Count);
            Assert.AreEqual("second", users[0].Username);
            Assert.AreEqual("first", users[1].Username);
        }

        #endregion
    }
}