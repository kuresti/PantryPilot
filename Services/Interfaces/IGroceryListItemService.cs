using PantryPilot.Models;

namespace PantryPilot.Services.Interfaces;

public interface IGroceryListItemService
{
    Task<GroceryListItem> AddItemAsync(GroceryListItem item);
    Task<List<GroceryListItem>> GetItemsByListIdAsync(int groceryListId);
    Task<bool> UpdateItemAsync(GroceryListItem item);
    Task<bool> RemoveItemAsync(int itemId);
}