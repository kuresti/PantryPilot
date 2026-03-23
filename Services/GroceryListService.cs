using PantryPilot.Data;
using PantryPilot.Models;
using Microsoft.EntityFrameworkCore;

namespace PantryPilot.Services
{
    public class GroceryListService
    {
        private readonly ApplicationDbContext _context;

        public GroceryListService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GroceryList> CreateGroceryListAsync(GroceryList list)
        {
            _context.GroceryLists.Add(list);
            await _context.SaveChangesAsync();
            return list;
        }

        public async Task<List<GroceryList>> GetGroceryListsByUserAsync(int userId)
        {
            return await _context.GroceryLists
                .Where(gl => gl.UserId == userId)
                .Include(gl => gl.Items)
                .ToListAsync();
        }

        public async Task<GroceryList?> GetGroceryListByIdAsync(int id)
        {
            return await _context.GroceryLists
                .Include(gl => gl.Items)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(gl => gl.Id == id);
        }

        public async Task<bool> UpdateGroceryListAsync(GroceryList list)
        {
            _context.GroceryLists.Update(list);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteGroceryListAsync(int id)
        {
            var list = await _context.GroceryLists.FindAsync(id);
            if (list == null)
            {
                return false;
            }

            _context.GroceryLists.Remove(list);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}