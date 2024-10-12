using System.ComponentModel.DataAnnotations;

namespace RecipeManagerApp.Models
{
    public class Recipe
    {
        //define the stucture of data stored in the database
        public int RecipeId { get; set; } // Primary key

        [Required]
        public string Title { get; set; } //name of recipe: mandatory field

        public string Cuisine { get; set; } 

        public string MealType { get; set; }

        public string Ingredients { get; set; }

        public string Instructions { get; set; }
    }
}
