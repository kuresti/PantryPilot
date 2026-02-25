namespace PantryPilot.Models
{
    public class Recipe
    {
        public int Id { get; set; }  
        public string Name { get; set; }  
        public string Description { get; set; }  
        public int PrepTime { get; set; }  
        public int CookTime { get; set; }  
        public int Servings { get; set; }

        // Foreign Key and navigation to ApplicationUser
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }   // Updated User to ApplicationUser to implement authentication

        // Relationships
        public List<RecipeStep> Steps { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
        public List<MenuRecipe> MenuRecipes { get; set; }
    }
}