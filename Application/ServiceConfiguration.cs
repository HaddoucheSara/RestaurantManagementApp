using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using Infrastructure.Data.Context;
using Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Application
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
           
            services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
         
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IRepository<Restaurant>, RestaurantRepository>();
        }

    }
}