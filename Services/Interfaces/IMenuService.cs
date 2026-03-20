using PantryPilot.Models;

namespace PantryPilot.Services.Interfaces;

// Created Interface to more loosely couple
// MenuService this creates better abstraction
// and also allows for easier changes as the app
// grows.

public interface IMenuService
{
    Task<Menu> CreateMenuAsync(Menu menu);
    Task<List<Menu>> GetMenusByUserAsync(int userId);
    Task<Menu?> GetMenuByIDAsync(int id);
    Task<bool> UpdateMenuAsync(Menu menu);
    Task<bool> DeleteMenuAsync(int id);
}