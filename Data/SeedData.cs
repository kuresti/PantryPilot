// File: PantryPilot/Data/SeedData.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PantryPilot.Models;
using PantryPilot.Services;

namespace PantryPilot.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        // Migrate the database
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await context.Database.MigrateAsync();

        // Do not reseed if recipes already exist
        if (await context.Recipes.AnyAsync(r => r.UserId == user.Id))
            return;

        // Ensure that the test user exists
        var user = await EnsureTestUserAsync(userManager);
        int userId = user.Id;

        //Ingredients
        // TODO The Grocery Category branch needs to be merged first
        var flour = new Ingredient { Name = "Flour", Category = IngredientCategory.Packaged };
        var egg = new Ingredient { Name = "Egg", Category = IngredientCategory.Dairy };
        var milk = new Ingredient { Name = "Milk", Category = IngredientCategory.Dairy };
        var lettuce = new Ingredient { Name = "Lettuce", Category = IngredientCategory.Produce };
        var pasta = new Ingredient { Name = "Pasta", Category = IngredientCategory.Packaged };

        context.Ingredients.AddRange(flour, egg, milk, lettuce, pasta);

        // Recipes
        var pancakes = new Recipe
        {
            Name = "Seed Pancakes",
            PrepTime = 10,
            CookTime = 5,
            Servings = 2,
            UserId = userId,
            RecipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { Ingredient = flour, Quantity = 1m, Unit = "cup" },
                new RecipeIngredient { Ingredient = egg, Quantity = 2m, Unit = "each" },
                new RecipeIngredient { Ingredient = milk, Quantity = 1m, Unit = "cup" },
            },
            Steps = new List<RecipeStep>
            {
                new RecipeStep { StepNumber = 1, Instruction = "Mix ingredients" },
                new RecipeStep { StepNumber = 2, Instruction = "Cook on skillet" },
            }
        };

        var salad = new Recipe
        {
            Name = "Seed Salad",
            PrepTime = 5,
            CookTime = 0,
            Servings = 1,
            UserId = userId,
            RecipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { Ingredient = lettuce, Quantity = 1m, Unit = "cup" }
            },
        };

        var pastaRecipe = new Recipe
        {
            Name = "Seed Pasta",
            PrepTime = 12,
            CookTime = 15,
            Servings = 3,
            UserId = userId,
            RecipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { Ingredient = pasta, Quantity = 0.5m, Unit = "cup" },

                // Edge-case: duplicate ingredient
                new RecipeIngredient { Ingredient = milk, Quantity = 0.5m, Unit = "cup" },
            },
            Steps = new List<RecipeStep>
            {
                new RecipeStep { StepNumber = 1, Instruction = "Boil pasta" },
                new RecipeStep { StepNumber = 2, Instruction = "Drain and serve" }
            }
        };

        context.Recipes.AddRange(pancakes, salad, pastaRecipe);

        // Menu + join 
        var menu = new Menu
        {
            Name = "Weekly Favorites",
            Date = DateTime.UtcNow.Date,
            UserId = userId,
            MenuRecipes = new List<MenuRecipe>()
        };

        menu.MenuRecipes.Add(new MenuRecipe { Menu = menu, Recipe = pancakes });
        menu.MenuRecipes.Add(new MenuRecipe { Menu = menu, Recipe = salad });
        menu.MenuRecipes.Add(new MenuRecipe { Menu = menu, Recipe = pastaRecipe });

        context.Menus.Add(menu);

        // Grocery list generated from the menu
        var groceryList = GroceryListGenerator.BuildFromMenu(menu, userId, $"Grocery List - {menu.Name}");
        context.GroceryLists.Add(groceryList);

        // Save changes
        await context.SaveChangesAsync();
    }

    private static async Task<ApplicationUser> EnsureTestUserAsync(UserManager<ApplicationUser> userManager)
    {
        const string email = "test@pantrypilot.com";
        var user = await userManager.FindByEmailAsync(email);

        // Check to see if user exists
        if (user != null)
            return user;

        user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        var result = await userManager.CreateAsync(user, "Test123!");

        if (!result.Succeeded)
        {
            var msg = string.Join("; ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
            throw new InvalidOperationException($"Failed to create seed user. {msg}");
        }

        return user;
    }

    private static class GroceryListGenerator
    {
        public static GroceryList BuildFromMenu(Menu menu, int userId, string name)
        {
            var allRecipeIngredients = (menu.MenuRecipes ?? new List<MenuRecipe>())
                .Where(mr => mr.Recipe is not null)
                .SelectMany(mr => mr.Recipe!.RecipeIngredients ?? new List<RecipeIngredient>())
                .Where(ri => ri.Ingredient is not null)
                .ToList();

            var items = allRecipeIngredients
                .GroupBy(ri => new { ri.IngredientId, Unit = (ri.Unit ?? "").Trim() })
                .Select(g => new GroceryListItem
                {
                    IngredientId = g.Key.IngredientId,
                    Ingredient = g.First().Ingredient,
                    Unit = g.Key.Unit,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .OrderBy(i => i.Ingredient.Name)
                .ToList();

            return new GroceryList
            {
                Name = name,
                UserId = userId,
                Items = items
            };
        }
    }
}

        