namespace PantryPilot.Models
{
    public class MenuRecipe
    {
        public int Id { get; set; }

        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}