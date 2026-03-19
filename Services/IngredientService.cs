using Microsoft.EntityFrameworkCore;
using PantryPilot.Data;
using PantryPilot.Models;

public class IngredientService
{
    private readonly ApplicationDbContext _context;

    public IngredientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ingredient>> GetAllAsync()
    {
        return await _context.Ingredients
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    public async Task<Ingredient?> GetByIdAsync(int id)
    {
        return await _context.Ingredients.FindAsync(id);
    }

    public async Task AddAsync(Ingredient ingredient)
    {
        ingredient.Name = Normalize(ingredient.Name);

        var exists = await _context.Ingredients
            .AnyAsync(i => i.Name.ToLower() == ingredient.Name.ToLower());

        if (exists)
        {
            return;
        }

        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Ingredient ingredient)
    {
        ingredient.Name = Normalize(ingredient.Name);

        var exists = await _context.Ingredients
            .AnyAsync(i => i.Name.ToLower() == ingredient.Name.ToLower() && i.Id != ingredient.Id);

        if (exists)
        {
            return;
        }

        _context.Ingredients.Update(ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient != null)
        {
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }
    }

    private string Normalize(string name)
    {
        name = name.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            return name;
        }

        return char.ToUpper(name[0]) + name.Substring(1).ToLower();
    }
}