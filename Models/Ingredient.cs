namespace PantryPilot.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<RecipeIngredient> RecipeIngredients { get; set; }
        public List<GroceryListItem> GroceryListItems { get; set; }
    }
}