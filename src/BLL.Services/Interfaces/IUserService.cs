namespace BLL.Services.Interfaces
{
    using Models.Domain.Models;
    public interface IUserService : IService<User>
    {
        void CreateAdminIfNoUserExists();
    }
}
