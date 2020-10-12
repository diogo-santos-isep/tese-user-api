namespace BLL.Services.Implementations.Tests
{
    using AutoFixture;
    using DAL.Repositories.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Domain.Models;
    using Models.Filters;
    using Moq;
    using System.Collections.Generic;

    [TestClass()]
    public class UserServiceTests
    {
        private Mock<IUserRepository> repoMock;
        private UserService target;
        private Fixture fixture = new Fixture();
        public UserServiceTests()
        {
            this.repoMock = new Mock<IUserRepository>();
            this.target = new UserService(repoMock.Object);
        }

        [TestMethod()]
        public void Create_Success()
        {
            var newUser = this.GenerateNewUser();

            this.target.Create(newUser);

            this.repoMock.Verify(x => x.Create(newUser), Times.Once, "Create não foi chamado");
        }

        [TestMethod()]
        public void CreateAdminIfNoUserExists_UserExists_DoesNothing()
        {
            this.repoMock.Setup(x => x.Get()).Returns(new List<User>() { this.GenerateNewUser() });
            this.target.CreateAdminIfNoUserExists();

            this.repoMock.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
        }

        [TestMethod()]
        public void CreateAdminIfNoUserExists_NoUserExists_Creates()
        {
            this.repoMock.Setup(x => x.Get()).Returns(new List<User>() {  });
            this.target.CreateAdminIfNoUserExists();

            this.repoMock.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }

        [TestMethod()]
        public void Delete_Success()
        {
            var id = fixture.Create<string>();
            this.target.Delete(id);
            this.repoMock.Verify(x => x.Delete(id), Times.Once, "Delete não foi chamado");
        }

        [TestMethod()]
        public void Get_Success()
        {
            var id = fixture.Create<string>();
            this.target.Get(id);
            this.repoMock.Verify(x => x.Get(id), Times.Once, "Get não foi chamado");
        }

        [TestMethod()]
        public void GetAll_Success()
        {
            this.target.Get();
            this.repoMock.Verify(x => x.Get(), Times.Once, "Get não foi chamado");
        }

        [TestMethod()]
        public void Search_Success()
        {
            var filter = this.fixture.Create<UserFilter>();
            this.target.Search(filter);

            this.repoMock.Verify(x => x.Search(filter), Times.Once, "Search não foi chamado");
            this.repoMock.Verify(x => x.Count(filter), Times.Once, "Count não foi chamado");
        }

        [TestMethod()]
        public void Update_Success()
        {
            var newUser = this.GenerateNewUser();

            newUser = this.target.Update(newUser.Id, newUser);

            this.repoMock.Verify(x => x.Update(newUser.Id, newUser), Times.Once, "Update não foi chamado");
        }
        private User GenerateNewUser()
        {
            return this.fixture.Build<User>()
                .Create();
        }
    }
}