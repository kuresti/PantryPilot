// Updated ApplicationDbContext to enable Identity from AspNetCore.Identity


using Microsoft.EntityFrameworkCore;
using PantryPilot.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace PantryPilot.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       
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