using Microsoft.EntityFrameworkCore;
using PantryPilot.Data;
using PantryPilot.Models;
using PantryPilot.Services.Interfaces;

namespace PantryPilot.Services;
public class RecipeStepService : IRecipeStepService
{
    private readonly ApplicationDbContext _context;

    public RecipeStepService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<RecipeStep>> GetByRecipeIdAsync(int recipeId)
    {
        return await _context.RecipeSteps
            .Where(rs => rs.RecipeId == recipeId)
            .OrderBy(rs => rs.StepNumber)
            .ToListAsync();
    }

    public async Task AddAsync(RecipeStep recipeStep)
    {
        _context.RecipeSteps.Add(recipeStep);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(RecipeStep recipeStep)
    {
        _context.RecipeSteps.Update(recipeStep);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var recipeStep = await _context.RecipeSteps.FindAsync(id);
        if (recipeStep != null)
        {
            _context.RecipeSteps.Remove(recipeStep);
            await _context.SaveChangesAsync();
        }
    }
}