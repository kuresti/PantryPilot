using Microsoft.EntityFrameworkCore;
using PantryPilot.Data;
using PantryPilot.Models;
using PantryPilot.Services.Interfaces;

public class RecipeIngredientService : IRecipeIngredientService
{
    private readonly ApplicationDbContext _context;

    public RecipeIngredientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<RecipeIngredient>> GetByRecipeIdAsync(int recipeId)
    {
        return await _context.RecipeIngredients
            .Include(ri => ri.Ingredient)
            .Where(ri => ri.RecipeId == recipeId)
            .OrderBy(ri => ri.Id)
            .ToListAsync();
    }

    public async Task AddAsync(RecipeIngredient recipeIngredient)
    {
        _context.RecipeIngredients.Add(recipeIngredient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(RecipeIngredient recipeIngredient)
    {
        _context.RecipeIngredients.Update(recipeIngredient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);
        if (recipeIngredient != null)
        {
            _context.RecipeIngredients.Remove(recipeIngredient);
            await _context.SaveChangesAsync();
        }
    }
}