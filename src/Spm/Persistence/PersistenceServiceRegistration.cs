using Application.Services.IEstimateRepository;
using Application.Services.IMb51HistoryRepository;
using Application.Services.IMb51Repository;
using Application.Services.IPlantRepository;
using Application.Services.IRomaniaZm20HistoryRepository;
using Application.Services.IRomaniaZm20Repository;
using Application.Services.ISettingRepository;
using Application.Services.IZm20HistoryRepository;
using Application.Services.IZm20Repository;
using Application.Services.IZs14HistoryRepository;
using Application.Services.IZs14Repository;
using Application.Services.MainReportRepositories;
using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("SpmConnectionString"), b => b.MigrationsAssembly("Persistence")));


            // services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IZm20Repository, Zm20Repository>();
            services.AddScoped<IZm20HistoryRepository, Zm20HistoryRepository>();
            services.AddScoped<IZs14HistoryRepository, Zs14HistoryRepository>();
            services.AddScoped<IZs14Repository, Zs14Repository>();
            services.AddScoped<IMb51Repository, Mb51Repository>();
            services.AddScoped<IMb51HistoryRepository, Mb51HistoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMainReportRepository, MainReportRepository>();
            services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
            services.AddScoped<IMaterialGroupRepository, MaterialGroupRepository>();
            services.AddScoped<IMipRepository, MipRepository>();
            services.AddScoped<IMaterialGroupRepository, MaterialGroupRepository>();
            services.AddScoped<IEstimateRepository, EstimateRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IPlantRepository,PlantRepository>();
            services.AddScoped<IRomaniaZm20Repository,RomaniaZm20Repository>();
            services.AddScoped<IRomaniaZm20HistoryRepository,RomaniaZm20HistoryRepository>();
            services.AddScoped<IOperationClaimRepository,OperationClaimRepository>();


            return services;
        }


    }
}
