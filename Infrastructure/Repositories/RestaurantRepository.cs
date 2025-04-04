using Domain.DTOs;
using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class RestaurantRepository : IRepository<Restaurant>
    {
        private readonly ApplicationDbContext _context;

        public RestaurantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Restaurant> GetByIdAsync(int id)
        {
            return await _context.Restaurants.FindAsync(id);
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task AddAsync(Restaurant entity)
        {
            await _context.Restaurants.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Restaurant entity)
        {
            _context.Restaurants.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<RestaurantDto>> GetRestaurantsByCuisineAsync(string cuisine)
        {
            return await _context.Restaurants
                .Where(r => r.Cuisine.ToLower() == cuisine.ToLower())
                .Select(r => new RestaurantDto
                {
                    Id = r.Id,
                    Nom = r.Nom,
                    Adresse = r.Adresse,
                    Cuisine = r.Cuisine,
                    Note = r.Note,
                    ImagePath = r.ImagePath
                })
                .ToListAsync();
        }

    }
}
