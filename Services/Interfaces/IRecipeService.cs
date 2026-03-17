using PantryPilot.Models;

namespace PantryPilot.Services.Interfaces;

public interface IRecipeService
{
    Task<List<Recipe>> GetUserRecipes();
    Task<Recipe?> GetRecipe(int id);
    Task<Recipe> CreateRecipe(Recipe recipe);
    Task<bool> UpdateRecipe(Recipe recipe);
    Task<bool> DeleteRecipe(int id);
}
