using PantryPilot.Models;

namespace PantryPilot.Services.Interfaces;

public interface IWeeklyMenuService
{
    Task<WeeklyMenu> CreateWeeklyMenuAsync(WeeklyMenu weeklyMenu);
    Task<List<WeeklyMenu>> GetAllWeeklyMenusAsync();
    Task<WeeklyMenu?> GetWeeklyMenuByIdAsync(int id);
    Task<bool> UpdateWeeklyMenuAsync(WeeklyMenu weeklyMenu);
    Task<bool> DeleteWeeklyMenuAsync(int id);

    Task<WeeklyMenu?> GetCurrentWeeklyMenuForUserAsync(int userId);
    Task<bool> AssignRecipeToMealAsync(int userId, DateOnly date, MealType mealType, int recipeId);
}