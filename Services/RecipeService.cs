using Microsoft.EntityFrameworkCore;
using PantryPilot.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace PantryPilot.Services
{
    public class RecipeService
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthenticationStateProvider _authStateProvider;

        public RecipeService(ApplicationDbContext context, AuthenticationStateProvider authStateProvider)
        {
            _context = context;
            _authStateProvider = authStateProvider;
        }

        public async Task<int> GetCurrentUserId()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            var idValue = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(idValue))
            {
                throw new InvalidOperationException("User is not authenticated.");
            }

            return int.Parse(idValue);
        }

        public async Task<List<Recipe>> GetUserRecipes()
        {
            int userId = await GetCurrentUserId();

            return await _context.Recipes
                .Where(r => r.UserId == userId)
                .Include(r => r.RecipeIngredients)
                .Include(r => r.Steps)
                .ToListAsync();
        }

        public async Task<Recipe?> GetRecipe(int id)
        {
            int userId = await GetCurrentUserId();

            return await _context.Recipes
                .Where(r => r.Id == id && r.UserId == userId)
                .Include(r => r.RecipeIngredients)
                .Include(r => r.Steps)
                .FirstOrDefaultAsync();
        }

        public async Task<Recipe> CreateRecipe(Recipe recipe)
        {
            recipe.UserId = await GetCurrentUserId();

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }

        public async Task<bool> UpdateRecipe(Recipe recipe)
        {
            int userId = await GetCurrentUserId();

            if (recipe.UserId != userId)
            {
                return false;
            }

            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteRecipe(int id)
        {
            int userId = await GetCurrentUserId();

            var recipe = await _context.Recipes
                .Where(r => r.Id == id && r.UserId == userId)
                .FirstOrDefaultAsync();

            if (recipe == null)
            {
                return false;
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}