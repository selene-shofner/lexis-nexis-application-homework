using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RecipeBook.Models;
using RecipeBook.Repository;

namespace RecipeBook.Controllers
{
    public class RecipeController : Controller
    {
        private IRecipeBookRepository Repository { get; }
        private IMapper Mapper { get; }

        private RecipeList RecipeList { get; set; } = new RecipeList();

        public RecipeController(IRecipeBookRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        // GET: RecipeController
        public ActionResult Index()
        {
            RecipeList.SearchTerms.TitleSubstring = "Title";

            var domainSearchTerms = Mapper.Map<Models.SearchTerms, Repository.SearchTerms>(RecipeList.SearchTerms);

            var domainRecipes = Repository.GetRecipes(domainSearchTerms);
            RecipeList.Recipes = domainRecipes.Select(recipe => Mapper.Map<Repository.Entities.Recipe, Models.Recipe>(recipe));
            return View(RecipeList);
        }

        // GET: RecipeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecipeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecipeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RecipeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RecipeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RecipeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RecipeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddStartingDataToDatabase()
        {
            const string HOT_DOG_TITLE = "Boiled Hot Dog";
            Repository.Entities.Recipe existingHotDogRecipe = Repository.GetRecipes(new Repository.SearchTerms { TitleSubstring = HOT_DOG_TITLE })
                .FirstOrDefault();

            if (existingHotDogRecipe == null)
            {
                Recipe newHotDogRecipe = new Recipe
                {
                    Title = HOT_DOG_TITLE,
                    Instructions = @"1. Bring water to boil.
2. Put one or more hot dogs in water, frozen or thawed.
3. Boil for 4-6 minutes if thawed, or 8-10 minutes if frozen.
4. Put hot dog on bun.
5. Season and garnish to taste.",
                    Description = "A hot dog, classic staple of the American connoisseur!",
                    ImageUrl = "https://www.foodservicedirect.com/media/catalog/product/0/0/00635596221593_y7ccny4udy7emuba.jpg?width=1200&height=1200&canvas=1200,1200&quality=75&fit=bounds",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient{ Name="Hot Dog", Quantity = 1, QuantityUnit = QuantityUnit.Count },
                        new Ingredient{ Name="Bun", Quantity = 1, QuantityUnit = QuantityUnit.Count }
                    }
                };

                AddRecipe(newHotDogRecipe);
            }

            const string CHOCOLATE_COVERED_STRAWBERRIES_TITLE = "Chocolate Covered Strawberries";
            Repository.Entities.Recipe existingStrawberriesRecipe = Repository.GetRecipes(new Repository.SearchTerms { TitleSubstring = CHOCOLATE_COVERED_STRAWBERRIES_TITLE })
                .FirstOrDefault();

            if (existingStrawberriesRecipe == null)
            {
                Recipe newStrawberriesRecipe = new Recipe
                {
                    Title = CHOCOLATE_COVERED_STRAWBERRIES_TITLE,
                    Instructions = @"1. Wash and dry the strawberries.
2. Set out parchment paper on which to prepare the strawberries.
3. Melt the chocolate in a large microwave-safe bowl in the microwave
4. Dip each strawberry one by one in the chocolate, and set it out.
5. Allow the chocolate on all of the strawberries to dry.
6. Serve!",
                    Description = "Chocolate covered strawberries, a fondue classic, now with chocolate melted in your microwave!",
                    ImageUrl = "https://hips.hearstapps.com/del.h-cdn.co/assets/18/06/1518114853-delish-chocolate-covered-strawberries-1.jpg",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient{ Name="Chocolate Chips", Quantity=6, QuantityUnit=QuantityUnit.Ounce },
                        new Ingredient{ Name="Strawberries", Quantity=1, QuantityUnit=QuantityUnit.Pound },

                    }
                };

                AddRecipe(newStrawberriesRecipe);
            }

            const string DIJON_MUSHROOM_PORK_CHOPS_TITLE = "Pork Chops In Creamy Dijon Mushroom Sauce";
            Repository.Entities.Recipe existingDijonMushroomPorkChopsRecipe = Repository.GetRecipes(new Repository.SearchTerms { TitleSubstring = DIJON_MUSHROOM_PORK_CHOPS_TITLE })
                .FirstOrDefault();

            if (existingDijonMushroomPorkChopsRecipe == null)
            {
                Recipe newDijonMushroomPorkChopsRecipe = new Recipe
                {
                    Title = DIJON_MUSHROOM_PORK_CHOPS_TITLE,
                    Instructions = @"1. Preheat oven to 450 degrees.  Wash and dry all produce.  Dice potatoes into ½-inch pieces.  Trim and slice mushrooms into ¼-inch-thick pieces.  Halve, peel, and thinly slice half the onion.  Peel and finely chop garlic.  Toss potatoes on a baking sheet with a large drizzle of oil, 1 tsp Tuscan Heat spice, salt, and pepper.  Roast until browned and crispy, 20-25 minutes.
2. Meanwhile, pat pork dry with paper towels, then season all over iwth salt and pepper.  Heat a drizzle of oil in a large pan over medium-high heat.  Add pork and cook until browned and cooked through, 4-5 minutes per side.  Turn off heat; transfer to a cutting board to rest.  Tent with foil to keep warm.
3. Heat a drizzle of oil in pan used for pork over medium-high heat.  Add mushrooms and cook, stirring, until browned and tender, 5-7 minutes.  Add sliced onion, garlic, 1 tsp remaining Tuscan Heat Spice, and another drizzle of oil.  Cook, stirring, until softened, 1-2 minutes.
4. Stir stock concentrate and 1/4 cup water into pan with mushroom mixture.  Simmer until slightly thickened, 2-3 minutes.  Remove pan from heat; stir in sour cream, Dijon, and 1 TBSP butter.  Season generously with pepper.
5. Divie pork and potatoes between plates.  Spoon mushroom sauce over pork.",
                    Description = "Pork Chops in Creamy Dijon Mushroom Sauce, as presented by EveryPlate.  Why EveryPlate crams so much into each 'step' Selene does not know.",
                    ImageUrl = "https://scontent-lga3-2.xx.fbcdn.net/v/t39.30808-6/272677141_5034770209902044_2846705084396814974_n.jpg?_nc_cat=107&ccb=1-5&_nc_sid=9267fe&_nc_ohc=0d9PE-yG238AX9qaPUy&_nc_ht=scontent-lga3-2.xx&oh=00_AT9-69NVVznPnNCwY_0VlTZWuktfChckcTQVdSYU8MS-XQ&oe=625C4071",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient{ Name="Pork Chops", Quantity=1, QuantityUnit=QuantityUnit.Count },
                        new Ingredient{ Name="Button Mushrooms", Quantity=7, QuantityUnit=QuantityUnit.Count },
                        new Ingredient{ Name="Garlic Glove", Quantity=1, QuantityUnit=QuantityUnit.Count },
                        new Ingredient{ Name="Sour Cream", Quantity=2, QuantityUnit=QuantityUnit.Tablespoon },
                        new Ingredient{ Name="Yellow Onion", Quantity=1, QuantityUnit=QuantityUnit.Count },
                        new Ingredient{ Name="Yukon Gold Potatoes", Quantity=5, QuantityUnit=QuantityUnit.Count },
                        new Ingredient{ Name="Chicken Stock Concentrate", Quantity=1, QuantityUnit=QuantityUnit.Cup },
                        new Ingredient{ Name="Tuscan Heat Spice", Quantity=2, QuantityUnit=QuantityUnit.Teaspoon },
                        new Ingredient{ Name="Dijon Mustard", Quantity=2, QuantityUnit=QuantityUnit.Tablespoon },
                        new Ingredient{ Name="Cooking Oil", Quantity=5, QuantityUnit=QuantityUnit.Teaspoon },
                        new Ingredient{ Name="Butter", Quantity=1, QuantityUnit=QuantityUnit.Tablespoon },
                        new Ingredient{ Name="Kosher Salt", Quantity=2, QuantityUnit=QuantityUnit.Tablespoon },
                        new Ingredient{ Name="Black Pepper", Quantity=2, QuantityUnit=QuantityUnit.Tablespoon }

                    }
                };

                AddRecipe(newDijonMushroomPorkChopsRecipe);
            }




            //TODO: Return something better
            return new ObjectResult("Starting data loaded!");
        }

        private Recipe AddRecipe(Recipe recipe)
        {

            var repositoryRecipe = Mapper.Map<Models.Recipe, Repository.Entities.Recipe>(recipe);
            var repositoryResult = Repository.AddRecipe(repositoryRecipe);
            return Mapper.Map<Repository.Entities.Recipe, Models.Recipe>(repositoryResult);

        }
    }
}
