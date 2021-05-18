using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Split.Domain.Repositories;
using Split.Infrastructure.Persistence;
using Split.Infrastructure.Persistence.Repositories;

namespace Split.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<SplitDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("SplitDB");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });
            services.AddScoped<IDbConnectionProvider>(sp => sp.GetService<SplitDbContext>());
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            return services;
        }
    }
}