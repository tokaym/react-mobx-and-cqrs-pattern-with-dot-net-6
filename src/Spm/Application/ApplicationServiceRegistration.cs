using System.Reflection;
using Application.Features.Auths.Rules;
using Application.Features.Users.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using Core.Application.Pipelines.Validation;
using Core.Security.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // services.AddScoped<RoleBusinessRules>();
            services.AddScoped<UserBusinessRules>();
            services.AddScoped<UserOperationClaim>();
            services.AddScoped<OperationClaim>();
            services.AddScoped<AuthBusinessRules>();



            // services.AddScoped<ICacheService, CacheService>();
            // services.AddSingleton<LoggerServiceBase, FileLogger>();
            // services.AddSingleton<IElasticSearch, ElasticSearchManager>();


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));// Add all same type service
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();


            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));



            return services;
        }
    }
}