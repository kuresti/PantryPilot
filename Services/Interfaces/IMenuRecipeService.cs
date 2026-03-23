using PantryPilot.Models;

namespace PantryPilot.Services.Interfaces;

public interface IMenuRecipeService
{
    Task<MenuRecipe?> AddRecipeToMenuAsync(int menuId, int recipeId);
    Task<List<MenuRecipe>> GetRecipesForMenuASync(int menuId);
    Task<bool> RemoveRecipeFromMenuAsync(int menuRecipeId);
}