using HabariConnect.Domain.Interfaces;
using HabariConnect.Infrastructure.Data;
using HabariConnect.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HabariConnect.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HabariConnectDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("HabariConnectDatabase")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
        }
    }
}
