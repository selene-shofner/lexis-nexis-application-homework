namespace RecipeBook.Repository
{
    //TODO: Find a better namespace for this
    public class SearchTerms
    {
        public string TitleSubstring { get; set; }
        public string DescriptionSubstring { get; set; }
        public string IngredientNameSubstring { get; set; }
        public string InstructionsSubstring { get; set; }
    }
}
