// File: PantryPilot/Data/SeedData.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PantryPilot.Models;

namespace PantryPilot.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await context.Database.MigrateAsync();

        var user = await EnsureTestUserAsync(userManager);
        int userId = user.Id;

        // Prevent reseeding for this user
        bool alreadySeeded =
            await context.Recipes.AnyAsync(r => r.UserId == userId) ||
            await context.WeeklyMenus.AnyAsync(wm => wm.UserId == userId);

        if (alreadySeeded)
            return;

        // Ingredients seed data
        var flour = new Ingredient { Name = "Flour", Category = IngredientCategory.Packaged };
        var egg = new Ingredient { Name = "Egg", Category = IngredientCategory.Dairy };
        var milk = new Ingredient { Name = "Milk", Category = IngredientCategory.Dairy };
        var lettuce = new Ingredient { Name = "Lettuce", Category = IngredientCategory.Produce };
        var pasta = new Ingredient { Name = "Pasta", Category = IngredientCategory.Packaged };
        var chicken = new Ingredient { Name = "Chicken", Category = IngredientCategory.Meat };
        var tomato = new Ingredient { Name = "Tomato", Category = IngredientCategory.Produce };
        var cheese = new Ingredient { Name = "Cheese", Category = IngredientCategory.Dairy };

        context.Ingredients.AddRange(flour, egg, milk, lettuce, pasta, chicken, tomato, cheese);

        //Recipes
        var pancakes = new Recipe
        {
            Name = "Seed Pancakes",
            Description = "Simple fluffy pancakes for breakfast",
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
                new RecipeStep { StepNumber = 1, Instruction = "Mix ingredients." },
                new RecipeStep { StepNumber = 2, Instruction = "Pour batter onto skillet." },
                new RecipeStep { StepNumber = 3, Instruction = "Cook until golden on both sides." }
            }
        };

        var salad = new Recipe
        {
            Name = "Seed Salad",
            Description = "A light lunch salad",
            PrepTime = 5,
            CookTime = 0,
            Servings = 1,
            UserId = userId,
            RecipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { Ingredient = lettuce, Quantity = 1m, Unit = "cup" },
                new RecipeIngredient { Ingredient = tomato, Quantity = 1m, Unit = "each" },
                new RecipeIngredient { Ingredient = cheese, Quantity = 0.5m, Unit = "cup" }
            },
            Steps = new List<RecipeStep>
            {
                new RecipeStep { StepNumber = 1, Instruction = "Chop lettuce and tomato." },
                new RecipeStep { StepNumber = 2, Instruction = "Add cheese and toss." },
                new RecipeStep { StepNumber = 3, Instruction = "Serve immediately" }
            }
        };

        var pastaRecipe = new Recipe
        {
            Name = "Seed Pasta",
            Description = "Simple pasta dinner.",
            PrepTime = 12,
            CookTime = 15,
            Servings = 3,
            UserId = userId,
            RecipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { Ingredient = pasta, Quantity = 2m, Unit = "cups" },
                new RecipeIngredient { Ingredient = milk, Quantity = 0.5m, Unit = "cup" }
            },
            Steps = new List<RecipeStep>
            {
                new RecipeStep { StepNumber = 1, Instruction = "Boil pasta" },
                new RecipeStep { StepNumber = 2, Instruction = "Drain and mix with sauce or milk." },
                new RecipeStep { StepNumber = 3, Instruction = "Serve warm." }
            }
        };

        var chickenDinner = new Recipe
        {
            Name = "Seed Chicken Dinner",
            Description = "Simple chicken dinner for testing dinner slot.",
            PrepTime = 15,
            CookTime = 20,
            Servings = 2,
            UserId = userId,
            RecipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { Ingredient = chicken, Quantity = 2m, Unit = "each" },
                new RecipeIngredient { Ingredient = tomato, Quantity = 1m, Unit = "each" },
            },
            Steps = new List<RecipeStep>
            {
                new RecipeStep { StepNumber = 1, Instruction = "Season chicken" },
                new RecipeStep { StepNumber = 2, Instruction = "Cook chicken thoroughly" },
                new RecipeStep { StepNumber = 3, Instruction = "Serve with tomato"}
            }
        };

        var seededRecipes = new List<Recipe>
        {
            pancakes,
            salad,
            pastaRecipe,
            chickenDinner
        };

        context.Recipes.AddRange(seededRecipes);

        // Save here so recipe Ids are generated before WeeklyMenu/MenuDay uses them
        await context.SaveChangesAsync();

        // Menu + join table
        var menu = new Menu
        {
            Name = "Weekly Favorites",
            Date = DateTime.Today,
            UserId = userId,
            MenuRecipes = seededRecipes
                .Select(r => new MenuRecipe { RecipeId = r.Id} )
                .ToList()
          
        };

        context.Menus.Add(menu);

        // Weekly Menu for drag/drop
        var today = DateOnly.FromDateTime(DateTime.Today);

        var weeklyMenu = new WeeklyMenu
        {
            UserId = userId,
            Days = new List<MenuDay>
            {
                new MenuDay
                {
                    Date = today,
                    BreakfastRecipeId = null,
                    LunchRecipeId = null,
                    DinnerRecipeId = null
                },
                new MenuDay
                {
                    Date = today.AddDays(1),
                    BreakfastRecipeId = null,
                    LunchRecipeId = null,
                    DinnerRecipeId = null
                },
                new MenuDay
                {
                    Date = today.AddDays(2),
                    BreakfastRecipeId = null,
                    LunchRecipeId = null,
                    DinnerRecipeId = null,
                }
            }
        };

        context.WeeklyMenus.Add(weeklyMenu);

        // Grocery List generated from menu
        var groceryList = GroceryListGenerator.BuildFromRecipes(
            seededRecipes,
            userId,
            $"Grocery List - {menu.Name}");

        context.GroceryLists.Add(groceryList);

        await context.SaveChangesAsync();
    }

    private static async Task<ApplicationUser> EnsureTestUserAsync(UserManager<ApplicationUser> userManager)
    {
        const string email = "test@pantrypilot.com";
        var user = await userManager.FindByEmailAsync(email);

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
        public static GroceryList BuildFromRecipes(List<Recipe> recipes, int userId, string name)
        {
            var allRecipeIngredients = recipes
                .Where(r => r.RecipeIngredients is not null)
                .SelectMany(r => r.RecipeIngredients!)
                .Where(ri => ri.Ingredient is not null)
                .ToList();

            var items = allRecipeIngredients
                .GroupBy(ri => new { ri.IngredientId, Unit = (ri.Unit ?? string.Empty).Trim() })
                .Select(g => new GroceryListItem
                {
                    IngredientId = g.Key.IngredientId,
                    Ingredient = g.First().Ingredient,
                    Unit = g.Key.Unit,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .OrderBy(i => i.Ingredient!.Name)
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