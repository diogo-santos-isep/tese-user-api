﻿namespace Presentation.API.Components
{
    using DAL.Repositories.Implementations;
    using DAL.Repositories.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    public static class RepositoryComponents
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
