using PantryPilot.Data;
using PantryPilot.Models;
using PantryPilot.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PantryPilot.Services
{
    public class WeeklyMenuService : IWeeklyMenuService
    {
        private readonly ApplicationDbContext _context;

        public WeeklyMenuService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WeeklyMenu> CreateWeeklyMenuAsync(WeeklyMenu weeklyMenu)
        {
            _context.WeeklyMenus.Add(weeklyMenu);
            await _context.SaveChangesAsync();
            return weeklyMenu;
        }

        public async Task<List<WeeklyMenu>> GetAllWeeklyMenusAsync()
        {
            return await _context.WeeklyMenus
                .Include(wm => wm.Days)
                .ToListAsync();
        }

        public async Task<WeeklyMenu?> GetWeeklyMenuByIdAsync(int id)
        {
            return await _context.WeeklyMenus
                .Include(wm => wm.Days)
                .FirstOrDefaultAsync(wm => wm.Id == id);
        }

        public async Task<bool> UpdateWeeklyMenuAsync(WeeklyMenu weeklyMenu)
        {
            _context.WeeklyMenus.Update(weeklyMenu);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWeeklyMenuAsync(int id)
        {
            var weeklyMenu = await _context.WeeklyMenus.FindAsync(id);
            if (weeklyMenu == null)
            {
                return false;
            }

            _context.WeeklyMenus.Remove(weeklyMenu);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<WeeklyMenu?> GetCurrentWeeklyMenuForUserAsync(int userId)
        {
            var weeklyMenu = await _context.WeeklyMenus
                .Include(wm => wm.Days)
                .FirstOrDefaultAsync(wm => wm.UserId == userId);

            if (weeklyMenu != null)
            {
                return weeklyMenu;
            }

            weeklyMenu = new WeeklyMenu
            {
                UserId = userId,
                Days = new List<MenuDay>()
            };

            _context.WeeklyMenus.Add(weeklyMenu);
            await _context.SaveChangesAsync();

            return weeklyMenu;
        }

        public async Task<bool> AssignRecipeToMealAsync(int userId, DateOnly date, MealType mealType, int recipeId)
        {
            var weeklyMenu = await _context.WeeklyMenus
                .Include(wm => wm.Days)
                .FirstOrDefaultAsync(wm => wm.UserId == userId);

            if (weeklyMenu == null)
            {
                return false;
            }

            var menuDay = weeklyMenu.Days.FirstOrDefault(d => d.Date == date);

            if (menuDay == null)
            {
                menuDay = new MenuDay
                {
                    Date = date
                };

                weeklyMenu.Days.Add(menuDay);
            }

            switch (mealType)
            {
                case MealType.Breakfast:
                    menuDay.BreakfastRecipeId = recipeId;
                    break;
                case MealType.Lunch:
                    menuDay.LunchRecipeId = recipeId;
                    break;
                case MealType.Dinner:
                    menuDay.DinnerRecipeId = recipeId;
                    break;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}