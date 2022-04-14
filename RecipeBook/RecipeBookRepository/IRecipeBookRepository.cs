
using RecipeBook.Repository.Entities;

namespace RecipeBook.Repository
{
    public interface IRecipeBookRepository
    {
        IEnumerable<Recipe> GetRecipes(SearchTerms searchTerms);

        
        Recipe AddRecipe(Recipe newRecipe);
    }
}