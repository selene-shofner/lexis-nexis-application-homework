
using AutoMapper;

using Microsoft.EntityFrameworkCore;

using RecipeBook.Repository.Entities;

namespace RecipeBook.Repository
{
    public class RecipeBookRepository : IRecipeBookRepository
    {
        private RecipeBookContext Context { get; }
        private IMapper Mapper { get; }

        public RecipeBookRepository(RecipeBookContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
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

        private static bool SubstringSearchTermMatches(string recipeValue, string substringSearchTerm)
        {
            return string.IsNullOrWhiteSpace(substringSearchTerm) || (!string.IsNullOrWhiteSpace(recipeValue) && recipeValue.Contains(substringSearchTerm));
        }
        public Recipe? GetRecipe(int id)
        {
            return Context.Recipes
                .Include(recipe => recipe.Ingredients)
                .SingleOrDefault(recipe => recipe.Id == id);
        }

        public Recipe AddRecipe(Recipe newRecipe)
        {
            newRecipe.Id = default;
            foreach (RecipeIngredient ingredient in newRecipe.Ingredients)
            {
                ingredient.Id = default;
                Context.RecipeIngredients.Add(ingredient);
            }

            Context.Recipes.Add(newRecipe);
            Context.SaveChanges();

            return newRecipe;
        }

        public bool DeleteRecipe(int id)
        {
            var recipe = Context.Recipes.SingleOrDefault(recipe => recipe.Id == id);

            if (recipe == null)
            {
                return false;
            }

            Context.Recipes.Remove(recipe);
            Context.SaveChanges();
            return true;
        }

        public bool EditRecipe(int id, Recipe newValues)
        {
            var existingRecipe = Context.Recipes.SingleOrDefault(recipe => recipe.Id == id);

            if (existingRecipe == null)
            {
                return false;
            }

            CopyValues(newValues, existingRecipe);

            IEnumerable<RecipeIngredient> ingredientsToRemove = GetIngredientsMissingFromSecondList(existingRecipe.Ingredients, newValues.Ingredients);

            IEnumerable <RecipeIngredient> ingredientsToAdd = GetIngredientsMissingFromSecondList(newValues.Ingredients, existingRecipe.Ingredients);

            ingredientsToAdd.ToList().ForEach(ingredientToAdd => existingRecipe.Ingredients.Add(ingredientToAdd));

            Context.RecipeIngredients.RemoveRange(ingredientsToRemove);
            Context.RecipeIngredients.AddRange(ingredientsToAdd);

            Context.Entry(existingRecipe).State = EntityState.Modified;
            Context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Copies the values of the recipe, aside from the IDs.
        /// </summary>
        /// <param name="source">The recipe whose fields should be copied</param>
        /// <param name="destination">The recipe into which the data should be copied</param>
        private void CopyValues(Recipe source, Recipe destination)
        {
            Mapper.Map(destination, source);
        }

        private bool DoIngredientsMatch(RecipeIngredient first, RecipeIngredient second)
        {
            return first.QuantityUnit == second.QuantityUnit
                && first.Quantity == second.Quantity
                && first.Name == second.Name;
        }

        private IEnumerable<RecipeIngredient> GetIngredientsMissingFromSecondList(IEnumerable<RecipeIngredient> source, IEnumerable<RecipeIngredient> listToCheck)
        {

            return source.Where(sourceIngredient => !listToCheck
                    .Any(newIngredient => DoIngredientsMatch(sourceIngredient, newIngredient)));

        }
    }
}
