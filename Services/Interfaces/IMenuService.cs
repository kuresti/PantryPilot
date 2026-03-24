using PantryPilot.Models;

namespace PantryPilot.Services.Interfaces;

public interface IMenuService
{
    Task<Menu> CreateMenuAsync(Menu menu);
    Task<List<Menu>> GetMenusByUserAsync(int userId);
    Task<Menu?> GetMenuByIDAsync(int id);
    Task<bool> UpdateMenuAsync(Menu menu);
    Task<bool> DeleteMenuAsync(int id);
}