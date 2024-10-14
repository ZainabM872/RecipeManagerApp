using System.ComponentModel.DataAnnotations;

namespace RecipeManagerApp.Models
{
public class Recipe
{
    public int RecipeId { get; set; } // Primary key

    [Required]
    public string Title { get; set; } = null!; // Add null-forgiving operator

    public string? Cuisine { get; set; } // Make nullable if it can be empty
    public string? MealType { get; set; } // Make nullable if it can be empty
    public string? Ingredients { get; set; } // Make nullable if it can be empty
    public string? Instructions { get; set; } // Make nullable if it can be empty
}

}
