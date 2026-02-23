using Microsoft.EntityFrameworkCore;
using PantryPilot.Models;

namespace PantryPilot.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeStep> RecipeSteps { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuRecipe> MenuRecipes { get; set; }
        public DbSet<GroceryList> GroceryLists { get; set; }
        public DbSet<GroceryListItem> GroceryListItems { get; set; }
    }
}