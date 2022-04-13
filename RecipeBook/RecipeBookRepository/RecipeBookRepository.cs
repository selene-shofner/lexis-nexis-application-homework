using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using RecipeBook.Repository.Entities;

namespace RecipeBook.Repository
{
    public class RecipeBookRepository : IRecipeBookRepository
    {
        private RecipeBookContext Context { get; }

        public RecipeBookRepository(RecipeBookContext context)
        {
            Context = context;
        }

        public IEnumerable<Recipe> GetRecipes(SearchTerms searchTerms)
        {
            var recipes = Context.Recipes
                .Include(recipe => recipe.Ingredients)
                .Where(recipe => searchTerms.TitleSubstring == null || recipe.Title != null && recipe.Title.Contains(searchTerms.TitleSubstring))
                .Where(recipe => searchTerms.DescriptionSubstring == null || recipe.Description != null && recipe.Description.Contains(searchTerms.DescriptionSubstring))
                .Where(recipe => searchTerms.InstructionsSubstring == null || recipe.Instructions != null && recipe.Instructions.Contains(searchTerms.InstructionsSubstring))
                .Where(recipe => searchTerms.TitleSubstring == null || recipe.Title != null && recipe.Title.Contains(searchTerms.TitleSubstring))
                .Where(recipe => searchTerms.TitleSubstring == null || recipe.Title != null && recipe.Title.Contains(searchTerms.TitleSubstring))
                .Where(recipe => recipe.Ingredients.Any(ingredient => searchTerms.IngredientNameSubstring == null || ingredient.Name != null && ingredient.Name.Contains(searchTerms.IngredientNameSubstring)))
                //.Where(recipe => SubstringSearchTermMatches(recipe.Title, searchTerms.TitleSubstring))
                //.Where(recipe => SubstringSearchTermMatches(recipe.Description, searchTerms.DescriptionSubstring))
                //.Where(recipe => SubstringSearchTermMatches(recipe.Instructions, searchTerms.InstructionsSubstring))
                //.Where(recipe => recipe.Ingredients.Any(ingredient => SubstringSearchTermMatches(ingredient.Name, searchTerms.IngredientNameSubstring)))
                ;

            return recipes;
        }

        public static bool SubstringSearchTermMatches(string recipeValue, string substringSearchTerm)
        {
            return string.IsNullOrWhiteSpace(substringSearchTerm) || (!string.IsNullOrWhiteSpace(recipeValue) && recipeValue.Contains(substringSearchTerm));
        }

        //private IQueryable<Recipe> AddSearchTerm(
    }
}
