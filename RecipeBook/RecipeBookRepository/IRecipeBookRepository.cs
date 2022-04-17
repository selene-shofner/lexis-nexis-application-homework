
using RecipeBook.Repository.Entities;

namespace RecipeBook.Repository
{
    public interface IRecipeBookRepository
    {
        IEnumerable<Recipe> GetRecipes(SearchTerms searchTerms);

#nullable enable
        Recipe? GetRecipe(int id);
#nullable disable

        Recipe AddRecipe(Recipe newRecipe);
        
        bool DeleteRecipe(int id);

        bool EditRecipe(int id, Recipe newValues);

    }
}