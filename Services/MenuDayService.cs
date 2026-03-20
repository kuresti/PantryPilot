using PantryPilot.Data;
using PantryPilot.Models;
using Microsoft.EntityFrameworkCore;
using PantryPilot.Services.Interfaces;

namespace PantryPilot.Services
{
    public class MenuDayService : IMenuDayService
    {
        private readonly ApplicationDbContext _context;

        public MenuDayService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MenuDay> CreateMenuDayAsync(MenuDay menuDay)
        {
            _context.MenuDays.Add(menuDay);
            await _context.SaveChangesAsync();
            return menuDay;
        }

        public async Task<MenuDay?> GetMenuDayByIdAsync(int id)
        {
            return await _context.MenuDays.FirstOrDefaultAsync(md => md.Id ==id);
        }

        public async Task<MenuDay?> GetMenuDayByDateAsync  (DateOnly date)
        {
            return await _context.MenuDays.FirstOrDefaultAsync(md => md.Date == date);
        }

        public async Task<bool> UpdateMenuDayAsync(MenuDay menuDay)
        {
            _context.MenuDays.Update(menuDay);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMenuDayAsync(int id)
        {
            var menuDay = await _context.MenuDays.FindAsync(id);
            if (menuDay == null)
            {
                return false;
            }

            _context.MenuDays.Remove(menuDay);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}