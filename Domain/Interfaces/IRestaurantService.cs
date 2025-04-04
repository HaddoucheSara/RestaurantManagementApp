using Domain.DTOs;


namespace Domain.Interfaces
{
    public interface IRestaurantService
    {
        Task<RestaurantDto> GetRestaurantByIdAsync(int id);
        Task<List<RestaurantDto>> GetAllRestaurantsAsync();
        Task AddRestaurantAsync(RestaurantCreateDto createDto);
        Task UpdateRestaurantAsync(RestaurantUpdateDto updateDto);
        Task DeleteRestaurantAsync(int id);
        Task<IEnumerable<RestaurantDto>> GetRestaurantsByCuisineAsync(string cuisine);
       

    }
}
