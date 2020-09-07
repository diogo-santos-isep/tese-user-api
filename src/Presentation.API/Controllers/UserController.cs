namespace Presentation.API.Controllers
{
    using BLL.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Models.Domain.Enums;
    using Models.Domain.Models;
    using Presentation.API.Auth;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _service;

        public UserController(IUserService service)
        {
            this._service = service;
        }

        [HttpGet]
        [ScopeAndRoleAuthorization(Scopes.UserServiceScope, ERole.Admin)]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return this._service.Get();
        }

        [HttpGet("{id}")]
        [ScopeAndRoleAuthorization(Scopes.UserServiceScope, ERole.Admin)]
        public ActionResult<User> Get(string id)
        {
            return this._service.Get(id);
        }

        [HttpPost]
        [ScopeAndRoleAuthorization(Scopes.UserServiceScope, ERole.Admin)]
        public ActionResult<User> Create(User user)
        {
            return this._service.Create(user);
        }

        [HttpPut("{id}")]
        [ScopeAndRoleAuthorization(Scopes.UserServiceScope, ERole.Admin)]
        public ActionResult<User> Update(string id, User user)
        {
            return this._service.Update(id, user);
        }

        [HttpDelete("{id}")]
        [ScopeAndRoleAuthorization(Scopes.UserServiceScope, ERole.Admin)]
        public ActionResult<User> Delete(string id)
        {
            this._service.Delete(id);
            return NoContent();
        }
    }
}
