using PantryPilot.Data;
using PantryPilot.Models;
using PantryPilot.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PantryPilot.Services
{
    public class MenuRecipeService : IMenuRecipeService
    {
        private readonly ApplicationDbContext _context;

        public MenuRecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MenuRecipe?> AddRecipeToMenuAsync(int menuId, int recipeId)
        {

            var exists = await _context.MenuRecipes
                .AnyAsync(mr => mr.MenuId == menuId && mr.RecipeId == recipeId);

            if (exists)
            {
                return null;
            }

            var menuRecipe = new MenuRecipe
            {
                MenuId = menuId,
                RecipeId = recipeId
            };

            _context.MenuRecipes.Add(menuRecipe);
            await _context.SaveChangesAsync();

            return menuRecipe;
        }

        public async Task<List<MenuRecipe>> GetRecipesForMenuASync(int menuId)
        {
            return await _context.MenuRecipes
                .Where(mr => mr.MenuId == menuId)
                .Include(mr => mr.Recipe)
                .ToListAsync();
        }

        public async Task<bool> RemoveRecipeFromMenuAsync(int menuRecipeId)
        {
            var menuRecipe = await _context.MenuRecipes.FindAsync(menuRecipeId);
            if (menuRecipe == null)
            {
                return false;
            }

            _context.MenuRecipes.Remove(menuRecipe);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}