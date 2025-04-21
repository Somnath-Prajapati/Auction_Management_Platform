using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Persistence.Temp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionManagementSystem.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(IServiceCollection services, IConfiguration configuration)
        {

            // Register your DbContext and other persistence-related services here
            // Example:
            services.AddDbContext<AuctionManagementDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            return services;
        }
    }

}
