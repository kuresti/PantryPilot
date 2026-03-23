using PantryPilot.Models;

namespace PantryPilot.Services.Interfaces;

public interface IRecipeStepService
{
    Task<List<RecipeStep>> GetByRecipeIdAsync(int recipeId);
    Task AddAsync(RecipeStep recipeStep);
    Task UpdateAsync(RecipeStep recipeStep);
    Task DeleteAsync(int id);
}