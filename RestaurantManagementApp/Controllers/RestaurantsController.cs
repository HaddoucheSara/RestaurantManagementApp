using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;

public class RestaurantsController : Controller
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    // GET: /Restaurants
    public async Task<IActionResult> Index()
    {
        var restaurants = await _restaurantService.GetAllRestaurantsAsync();
        ViewData["Cuisine"] = null;
        return View(restaurants);
    }

    // GET: /Restaurants/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
        if (restaurant == null) return NotFound();
        return View(restaurant);
    }

    // GET: /Restaurants/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Restaurants/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RestaurantCreateDto dto)
    {
        if (ModelState.IsValid)
        {
            await _restaurantService.AddRestaurantAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        return View(dto);
    }

    // GET: /Restaurants/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
        if (restaurant == null) return NotFound();

        var updateDto = new RestaurantUpdateDto
        {
            Id = restaurant.Id,
            Nom = restaurant.Nom,
            Adresse = restaurant.Adresse,
            Cuisine = restaurant.Cuisine,
            Note = (int)restaurant.Note,
            ImagePath = restaurant.ImagePath
        };
        return View(updateDto);
    }

    // POST: /Restaurants/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(RestaurantUpdateDto dto)
    {
        if (ModelState.IsValid)
        {
            await _restaurantService.UpdateRestaurantAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        return View(dto);
    }

    // GET: /Restaurants/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
        if (restaurant == null) return NotFound();
        return View(restaurant);
    }

    // POST: /Restaurants/Delete/5
    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _restaurantService.DeleteRestaurantAsync(id);
        return RedirectToAction(nameof(Index));
    }
    // bonus 
    public async Task<IActionResult> ByCuisine(string cuisine)
    {
        var restaurants = await _restaurantService.GetRestaurantsByCuisineAsync(cuisine);
        ViewData["Cuisine"] = cuisine; // Cuisine filtrée
        return View("Index", restaurants);
    }
}
