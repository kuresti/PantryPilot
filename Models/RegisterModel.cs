using System.ComponentModel.DataAnnotations;

namespace PantryPilot.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(24, ErrorMessage = "Name cannot exceed 24 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare(nameof(Password), ErrorMessage = " Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}