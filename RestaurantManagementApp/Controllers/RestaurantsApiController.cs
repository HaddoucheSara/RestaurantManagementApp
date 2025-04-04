using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Application.Services;
using Domain.Interfaces;

namespace RestaurantManagementApp.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantsApiController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsApiController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);  
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if (restaurant == null)
                return NotFound();  

            return Ok(restaurant); 
        }

        // POST: api/Restaurants
        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");  

            await _restaurantService.AddRestaurantAsync(dto);
            return CreatedAtAction(nameof(GetRestaurantById), new { nom = dto.Nom }, dto);  
        }

        // PUT: api/Restaurants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] RestaurantUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");  

            try
            {
                await _restaurantService.UpdateRestaurantAsync(dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();  
            }

            return NoContent(); 
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            try
            {
                await _restaurantService.DeleteRestaurantAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();  
            }

            return NoContent(); 
        }
    }
}
