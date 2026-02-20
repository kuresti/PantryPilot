using BlazorApp.Components.Pages;

namespace PantryPilot.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<Recipe> Recipes { get; set; }
        public List<Menu> Menus { get; set; }
        public List<GroceryList> GroceryLists { get; set; }
    }
} 