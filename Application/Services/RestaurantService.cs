using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using AutoMapper;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;


namespace Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepository<Restaurant> _restaurantRepository;
        private readonly IMapper _mapper;
       

        public RestaurantService(IRepository<Restaurant> restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
          
        }

        public async Task<RestaurantDto> GetRestaurantByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID");
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null) throw new KeyNotFoundException("Restaurant not found");
            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public async Task<List<RestaurantDto>> GetAllRestaurantsAsync()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            return _mapper.Map<List<RestaurantDto>>(restaurants);
        }

        public async Task AddRestaurantAsync(RestaurantCreateDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);

            if (dto.Image != null)
            {
                // Génère un nom de fichier unique pour l'image
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images"); // Chemin relatif
                Directory.CreateDirectory(imagesFolder); // Crée le dossier s’il n’existe pas
                var filePath = Path.Combine(imagesFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                // Enregistre le chemin relatif de l'image dans la base de données
                restaurant.ImagePath = "/Images/" + fileName;
            }

            await _restaurantRepository.AddAsync(restaurant);
        }

        public async Task UpdateRestaurantAsync(RestaurantUpdateDto dto)
        {
            var existing = await _restaurantRepository.GetByIdAsync(dto.Id);
            if (existing == null) throw new KeyNotFoundException("Restaurant not found");

            existing.Nom = dto.Nom;
            existing.Adresse = dto.Adresse;
            existing.Cuisine = dto.Cuisine;
            existing.Note = dto.Note;

            if (dto.Image != null)
            {
                // Génère un nom de fichier unique pour l'image
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images"); 
                Directory.CreateDirectory(imagesFolder);
                var filePath = Path.Combine(imagesFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                existing.ImagePath = "/Images/" + fileName;
            }

            await _restaurantRepository.UpdateAsync(existing);
        }

        public async Task DeleteRestaurantAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID");
            await _restaurantRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<RestaurantDto>> GetRestaurantsByCuisineAsync(string cuisine)
        {
            return await _restaurantRepository.GetRestaurantsByCuisineAsync(cuisine);
        }
    }
}
