using PantryPilot.Models;

namespace PantryPilot.Services.Interfaces;

public interface IRecipeIngredientService
{
    Task<List<RecipeIngredient>> GetByRecipeIdAsync(int recipeId);
    Task AddAsync(RecipeIngredient recipeIngredient);
    Task UpdateAsync(RecipeIngredient recipeIngredient);
    Task DeleteAsync(int id);
}