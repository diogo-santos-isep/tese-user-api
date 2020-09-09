namespace DAL.Repositories.Interfaces
{
    using Models.Domain.Models;
    using Models.Filters;
    using System.Collections.Generic;

    public interface IUserRepository : IRepository<User>
    {
        long Count(UserFilter filter);
        List<User> Search(UserFilter filter);
    }
}
