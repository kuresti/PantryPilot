using PantryPilot.Data;
using PantryPilot.Models;
using PantryPilot.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PantryPilot.Services
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;

        public MenuService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Menu> CreateMenuAsync(Menu menu)
        {
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task<List<Menu>> GetMenusByUserAsync(int userId)
        {
            return await _context.Menus
                .Where(m => m.UserId == userId)
                .Include(m => m.MenuRecipes)
                .ToListAsync();
        }

        public async Task<Menu?> GetMenuByIDAsync(int id)
        {
            return await _context.Menus
                .Include(m => m.MenuRecipes)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> UpdateMenuAsync(Menu menu)
        {
            _context.Menus.Update(menu);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMenuAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return false;
            }
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}