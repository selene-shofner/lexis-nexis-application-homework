namespace RecipeBook.Models
{
    public class RecipeList
    {
        public IEnumerable<Recipe> Recipes { get; set; } = new List<Recipe>();

        public SearchTerms SearchTerms { get; set; } = new SearchTerms();

        public Recipe RecipeBeingAdded { get; set; } = new Recipe();

        public Recipe RecipeBeingEdited { get; set; } = new Recipe();
    }
}
