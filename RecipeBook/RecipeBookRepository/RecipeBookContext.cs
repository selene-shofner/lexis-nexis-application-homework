using Microsoft.EntityFrameworkCore;
using RecipeBook.Repository.Entities;

namespace RecipeBook.Repository
{
    public class RecipeBookContext : DbContext
    {
        public RecipeBookContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    }
}