// The ApplicationUser inherits from IdentityUser<int> so that the primary keys in 
// the rest of the schema remain int. Also, IdentityUser gives ApplicationUser the 
// following: UserName, NormalizedEmail, PasswordHash, SecurityStamp, EmailConfirmed,
// LockoutEnabled. Identity also handles: Roles, Claims, Logins, and Tokens.
using Microsoft.AspNetCore.Identity;

namespace PantryPilot.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; } = string.Empty;
        public List<Recipe> Recipes { get; set; } = new();
        public List<Menu> Menus { get; set; } = new();
        public List<GroceryList> GroceryLists { get; set; } = new();
    }
}