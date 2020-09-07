namespace Tests.Integration
{
    using AutoFixture;
    using BLL.Services.Implementations;
    using DAL.Repositories.Implementations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Domain.Models;
    using MongoDB.Bson;
    using System;
    using System.Linq;
    using Tests.Integration.Helpers;

    [TestClass]
    public class UserTests
    {
        private UserService _service;
        private Fixture fixture = new Fixture();

        public UserTests()
        {
            var repo = new UserRepository(DatabaseConnection.Current.Database);
            this._service = new UserService(repo);
        }

        [TestMethod()]
        public void Create_Success()
        {
            var newUser = this.GenerateNewUser();

            newUser = this._service.Create(newUser);
            Assert.IsFalse(String.IsNullOrEmpty(newUser.Id),"Id came back null or empty");

            var savedUser = this._service.Get(newUser.Id);
            Assert.AreEqual(savedUser, newUser, "Users are different");
        }

        [TestMethod()]
        public void CreateAdminIfNotExists_Success()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateAdminIfNotExists_AdminExists_NotCreated()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Delete_Success()
        {
            var newUser = this.GenerateNewUser();

            newUser = this._service.Create(newUser);

            this._service.Delete(newUser.Id);
            var savedUser = this._service.Get(newUser.Id);
            Assert.IsNull(savedUser,"User is not null, therefore still exists");
        }

        [TestMethod()]
        public void GetSingle_Success()
        {
            var newUser = this.GenerateNewUser();

            newUser = this._service.Create(newUser);

            var savedUser = this._service.Get(newUser.Id);
            Assert.IsNotNull(savedUser, "User does not exist");
            Assert.AreEqual(savedUser, newUser, "Users are different");
        }

        [TestMethod()]
        public void GetAll_Success()
        {
            var newUser = this.GenerateNewUser();
            var newUser2 = this.GenerateNewUser();

             this._service.Create(newUser);
             this._service.Create(newUser2);

            var users = this._service.Get();
            Assert.IsTrue(users.Any(t=>t.Id == newUser.Id), $"User1 does not exist");
            Assert.IsTrue(users.Any(t=>t.Id == newUser.Id), $"User2 does not exist");
        }

        [TestMethod()]
        public void Update_Success()
        {
            var newUser = this.GenerateNewUser();

            newUser = this._service.Create(newUser);

            newUser.Name += "wlelele";
            this._service.Update(newUser.Id, newUser);
            var savedUser = this._service.Get(newUser.Id);
            Assert.AreEqual(savedUser.Name, newUser.Name, "Users are different");
        }

        private User GenerateNewUser()
        {
            return this.fixture.Build<User>()
                .Without(t => t.Id)
                .With(t=>t.Department_Id,ObjectId.GenerateNewId().ToString())
                .Create();
        }
    }
}
