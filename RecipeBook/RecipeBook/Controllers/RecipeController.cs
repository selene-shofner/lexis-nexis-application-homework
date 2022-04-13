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
    }
}
