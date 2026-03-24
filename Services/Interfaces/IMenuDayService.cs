using PantryPilot.Models;

namespace PantryPilot.Services.Interfaces;

public interface IMenuDayService
{
    Task<MenuDay> CreateMenuDayAsync(MenuDay menuDay);
    Task<MenuDay?> GetMenuDayByIdAsync(int id);
    Task<MenuDay?> GetMenuDayByDateAsync(DateOnly date);
    Task<bool> UpdateMenuDayAsync(MenuDay menuDay);
    Task<bool> DeleteMenuDayAsync(int id);
}