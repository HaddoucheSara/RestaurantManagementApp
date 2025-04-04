using Application.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RestaurantManagementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Cr�er le builder pour l'application
            var builder = WebApplication.CreateBuilder(args);

            // Configurer les services via ServiceConfiguration
            Application.ServiceConfiguration.ConfigureServices(builder.Services, builder.Configuration);

            // Ajouter des services sp�cifiques � l'application
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IRestaurantService, RestaurantService>();

            // Construire l'application
            var app = builder.Build();

            // Configurer le pipeline HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage(); // Ajout pour la gestion des erreurs en d�veloppement
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            // D�finir la route par d�faut du contr�leur
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Restaurants}/{action=Index}/{id?}");

            // D�marrer l'application
            app.Run();
        }
    }
}
