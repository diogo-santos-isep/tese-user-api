namespace BLL.Services.Interfaces
{
    using Models.Domain.Models;
    using Models.DTO.Grids;
    using Models.Filters;

    public interface IUserService : IService<User>
    {
        void CreateAdminIfNoUserExists();
        UserGrid Search(UserFilter filter);
    }
}
