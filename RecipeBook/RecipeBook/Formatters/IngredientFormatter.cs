using RecipeBook.Models;

namespace RecipeBook.Helpers
{
    public static class IngredientFormatter
    {
        public static string FormatQuantityUnit(Ingredient model)
        {
            bool omitUnitName = model.QuantityUnit == QuantityUnit.Count;
            string filteredQuantityUnit = omitUnitName ? "" : model.QuantityUnit.ToString("G");

            string pluralitySuffix = "";
            bool pluralize = model.Quantity != 1;
            if (pluralize && !omitUnitName)
            {
                pluralitySuffix = model.QuantityUnit == QuantityUnit.Pinch ? "es" : "s";
            }

            return $"{filteredQuantityUnit}{pluralitySuffix}";
        }
    }
}
