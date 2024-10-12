using Microsoft.EntityFrameworkCore;

namespace RecipeManagerApp.Models
{
    public class RecipeContext : DbContext  //RecipeContext can inherit from DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; } // This line includes your Recipe model
    }
}
