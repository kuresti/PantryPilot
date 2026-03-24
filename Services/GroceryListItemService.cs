using PantryPilot.Data;
using PantryPilot.Models;
using PantryPilot.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Elfie.Model.Map;

namespace PantryPilot.Services 
{
    public class GroceryListItemService : IGroceryListItemService
    {
        private readonly ApplicationDbContext _context;

        public GroceryListItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GroceryListItem> AddItemAsync(GroceryListItem item)
        {
            _context.GroceryListItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<GroceryListItem>> GetItemsByListIdAsync(int groceryListId)
        {
            return await _context.GroceryListItems
                .Where(i => i.GroceryListId == groceryListId)
                .Include(i => i.Ingredient)
                .ToListAsync();
        }

        public async Task<bool> UpdateItemAsync(GroceryListItem item)
        {
            _context.GroceryListItems.Update(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveItemAsync(int itemId)
        {
            var item = await _context.GroceryListItems.FindAsync(itemId);
            if (item == null)
            {
                return false;
            }

            _context.GroceryListItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}