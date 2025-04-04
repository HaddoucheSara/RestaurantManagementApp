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
            // Créer le builder pour l'application
            var builder = WebApplication.CreateBuilder(args);

            // Configurer les services via ServiceConfiguration
            Application.ServiceConfiguration.ConfigureServices(builder.Services, builder.Configuration);

            // Ajouter des services spécifiques à l'application
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
                app.UseDeveloperExceptionPage(); // Ajout pour la gestion des erreurs en développement
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            // Définir la route par défaut du contrôleur
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Restaurants}/{action=Index}/{id?}");

            // Démarrer l'application
            app.Run();
        }
    }
}
