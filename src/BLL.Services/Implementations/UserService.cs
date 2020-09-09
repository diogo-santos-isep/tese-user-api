namespace BLL.Services.Implementations
{
    using BLL.Services.Interfaces;
    using DAL.Repositories.Interfaces;
    using Models.Domain.Models;
    using Models.DTO.Grids;
    using Models.Filters;
    using System;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        private IUserRepository _repo;

        public UserService(IUserRepository _repo)
        {
            this._repo = _repo;
        }
        public User Create(User model)
        {
            return _repo.Create(model);
        }

        public void CreateAdminIfNoUserExists()
        {
            if (Get().Count == 0)
                Create(User.GetAdminDefault());
        }

        public void Delete(string id) => _repo.Delete(id);

        public List<User> Get() => _repo.Get();

        public User Get(string id) => _repo.Get(id);

        public UserGrid Search(UserFilter filter)
        {
            return new UserGrid
            {
                List = _repo.Search(filter),
                Count = Convert.ToInt32(_repo.Count(filter))
            };
        }

        public User Update(string id, User model)
        {
            _repo.Update(id, model);
            return model;
        }
    }
}
