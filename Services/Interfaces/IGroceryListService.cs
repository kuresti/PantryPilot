using PantryPilot.Models;

namespace PantryPilot.Services.Interfaces;

public interface IGroceryListService
{
    Task<GroceryList> CreateGroceryListAsync(GroceryList list);
    Task<List<GroceryList>> GetGroceryListsByUserAsync(int userId);
    Task<GroceryList?> GetGroceryListByIdAsync(int id);
    Task<bool> UpdateGroceryListAsync(GroceryList list);
    Task<bool> DeleteGroceryListAsync(int id);
}