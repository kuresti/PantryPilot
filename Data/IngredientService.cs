using Microsoft.EntityFrameworkCore;
using PantryPilot.Models;

namespace PantryPilot.Data
{
    public class IngredientService
    {
        private readonly ApplicationDbContext _context;

        public IngredientService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all ingredients (alphabetical)
        public async Task<List<Ingredient>> GetAllIngredients()
        {
            return await _context.Ingredients
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        // Get ingredient by ID
        public async Task<Ingredient?> GetIngredient(int id)
        {
            return await _context.Ingredients
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        // Create ingredient
        public async Task CreateIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
        }

        // Update ingredient
        public async Task UpdateIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
        }

        // Delete ingredient
        public async Task DeleteIngredient(int id)
        {
            var ingredient = await GetIngredient(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }
        }
    }
}