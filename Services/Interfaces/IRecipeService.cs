using PantryPilot.Models;

namespace PantryPilot.Services.Interfaces;

// Created Interface to more loosely couple
// RecipeService this creates better abstraction
// and also allows for easier changes as the app
// grows.

public interface IRecipeService
{
    Task<List<Recipe>> GetUserRecipes();
    Task<Recipe?> GetRecipe(int id);
    Task<Recipe> CreateRecipe(Recipe recipe);
    Task<bool> UpdateRecipe(Recipe recipe);
    Task<bool> DeleteRecipe(int id);
}