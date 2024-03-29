﻿namespace Presentation.API.Controllers
{
    using BLL.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Models.Domain.Enums;
    using Models.Domain.Models;
    using Models.DTO.Grids;
    using Models.Filters;
    using Presentation.API.Auth;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        /// <summary>
        /// Performs a Search on Users
        /// | scope: user
        /// | role: admin
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("search")]
        [ScopeAndRoleAuthorization(Scopes.UserServiceScope, ERole.Admin)]
        public ActionResult<UserGrid> Search(UserFilter filter)
        {
            return this._service.Search(filter);
        }

        /// <summary>
        /// Gets a User
        /// | scope: user
        /// | role: admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ScopeAndRoleAuthorization(Scopes.UserServiceScope, ERole.Admin)]
        public ActionResult<User> Get(string id)
        {
            return this._service.Get(id);
        }

        /// <summary>
        /// Gets the logged user using the access token
        /// | scope: user
        /// </summary>
        /// <returns></returns>
        [HttpGet("logged")]
        [ScopeAndRoleAuthorization(Scopes.UserServiceScope)]
        public ActionResult<User> Get()
        {
            var id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value ?? throw new Exception("No user id claim was found on token");
            return this._service.Get(id);
        }

        /// <summary>
        /// Creates a user
        /// | scope: user
        /// | role: admin
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ScopeAndRoleAuthorization(Scopes.UserServiceScope, ERole.Admin)]
        public ActionResult<User> Create(User user)
        {
            return this._service.Create(user);
        }

        /// <summary>
        /// Updates a User
        /// | scope: user
        /// | role: admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ScopeAndRoleAuthorization(Scopes.UserServiceScope, ERole.Admin)]
        public ActionResult<User> Update(string id, User user)
        {
            return this._service.Update(id, user);
        }

        /// <summary>
        /// Deletes a user 
        /// | scope: user
        /// | role: admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ScopeAndRoleAuthorization(Scopes.UserServiceScope, ERole.Admin)]
        public ActionResult<User> Delete(string id)
        {
            this._service.Delete(id);
            return NoContent();
        }
    }
}
