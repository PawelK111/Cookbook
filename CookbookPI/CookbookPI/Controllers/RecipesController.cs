using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CookbookPI.Models;
using CookbookPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace CookbookPI.Controllers
{
    public class RecipesController : Controller
    {
        private readonly DatabaseContext _context;
        public RecipesController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult AddRecipe()
        {
            ViewBag.Status = false;
            try
            {
                ViewBag.Category = (from c in _context.Categories select c).ToList();
                ViewBag.TypeOfKitchen = (from c in _context.TypeOfKitchen select c).ToList();
                ViewBag.Difficulty = (from c in _context.Difficulty select c).ToList();
                ViewBag.NumberOfPeople = (from c in _context.NumberOfPeople select c).ToList();
                ViewBag.TimeOfPrepares = (from c in _context.TimeOfPrepares select c).ToList();
                return View();
            }
            catch
            {
                ViewBag.ErrorStatus = true;
                return View();
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddToFavorite(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lastID = _context.FavoriteRecipes.Select(x => x.ID_Recipe).LastOrDefault();
            if (lastID == 0 || lastID == null)
            {
                lastID = 1;
            }
            FavoriteRecipes FavRecipe = new FavoriteRecipes
            {
                ID_FavRecipe = lastID,
                ID_User = HttpContext.Session.GetInt32("ID_USER"),
                ID_Recipe = id
            };

            _context.FavoriteRecipes.Add(FavRecipe);
            await _context.SaveChangesAsync();
            return View(FavRecipe);
        }

        [HttpGet]
        public async Task<IActionResult> GetByCategory(int? id)
        {
            //ViewBag.User = (from c1 in _context.Users
            //                join c2 in _context.Recipes on c1.ID_User equals c2.ID_User
            //                where c2.ID_Recipe == id
            //                select c1.Nickname).First();
            ViewBag.Category = _context.Categories.Where(x => x.ID_Category == id).Select(x => x.Title).First();
            var databaseContext = _context.Recipes.Where(x => x.ID_Category == id && x.IsAccepted == true);
            return View("ListOfRecipes", await databaseContext.ToListAsync());

        }
        [HttpGet]
        public async Task<IActionResult> GetByCountry(int? id)
        {
            //ViewBag.User = (from c1 in _context.Users
            //                join c2 in _context.Recipes on c1.ID_User equals c2.ID_User
            //                where c2.ID_Recipe == id
            //                select c1.Nickname).First();
            ViewBag.Category = _context.TypeOfKitchen.Where(x => x.ID_TypeOfKitchen == id).Select(x => x.Title).First();
            var databaseContext = _context.Recipes.Where(x => x.ID_TypeOfKitchen == id && x.IsAccepted == true);
            return View("ListOfRecipes", await databaseContext.ToListAsync());
        }


        public async Task<IActionResult> RecipeInfo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            RecipeInfo r = new RecipeInfo();
            r.Recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.ID_Recipe == id);
            if (r.Recipe == null)
            {
                return NotFound();
            }
            r.Author = from c in _context.Users where c.ID_User == r.Recipe.ID_User select c.Nickname;
            r.TypeOfKitchen = from c in _context.TypeOfKitchen where c.ID_TypeOfKitchen == r.Recipe.ID_TypeOfKitchen select c.Title;
            r.Category = from c in _context.Categories where c.ID_Category == r.Recipe.ID_Category select c.Title;
            r.Difficulty = from c in _context.Difficulty where c.ID_Difficulty == r.Recipe.ID_Difficulty select c.Title;
            r.NumberOfPeople = from c in _context.NumberOfPeople where c.ID_NumberOfPeople == r.Recipe.ID_NumberOfPeople select c.Amount;
            r.TimeOfPrepare = from c in _context.TimeOfPrepares where c.ID_TimeOfPrepares == r.Recipe.ID_TimeOfPrepare select c.TimeOfPrepare;
            r.Components = from c in _context.Components where c.ID_Recipe == r.Recipe.ID_Recipe select c;

            return View(r);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecipe(List<IFormFile> Photo, [Bind("ID_Recipe, ID_User, Title, ID_TypeOfKitchen, ID_Category, " +
            "ID_Difficulty, ID_NumberOfPeople, ID_TimeOfPrepare, IsAccepted, Description, Instruction")] Recipes recipe)
        {
            ViewBag.ErrorStatus = false;
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var item in Photo)
                    {
                        if (item.Length > 0)
                            using (var stream = new MemoryStream())
                            {
                                await item.CopyToAsync(stream);
                                recipe.Photo = stream.ToArray();
                            }
                    }
                    recipe.ID_User = HttpContext.Session.GetInt32("ID_USER");
                    var numberRecipes = _context.Users.Where(x => x.ID_User == recipe.ID_User).Select(y => y.NumberOfRecipes).FirstOrDefault() + 1;
                    ViewBag.Status = true;
                    _context.Add(recipe);
                    _context.SaveChanges();
                    return new EmptyResult();
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorStatus = ex;
                    return View("AddRecipe");
                }
            }
            else
                return View(recipe);
        }
        public JsonResult InsertComponents([FromBody]List<Components> components)
        {
            try
            {
                var id_recipe = _context.Recipes.Select(x => x.ID_Recipe).LastOrDefault();
                if (components == null)
                {
                    components = new List<Components>();
                }
                foreach (Components x in components)
                {
                    x.ID_Recipe = id_recipe;
                    _context.Components.Add(x);
                }
                int insertedRecords = _context.SaveChanges();
                return Json(insertedRecords);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View("Search");
        }

        public JsonResult GetSearchingRecipes(string SearchBy, string SearchValue)
        {
            if (SearchBy == "Name")
            {
                var result = _context.Recipes.Where(x => x.Title.StartsWith(SearchValue) && x.IsAccepted == true).ToList();
                return Json(result);
            }
            else if (SearchBy == "User")
            {
                var result = (from c1 in _context.Recipes
                              join c2 in _context.Users on c1.ID_User equals c2.ID_User
                              where c2.Nickname.StartsWith(SearchValue) && c1.IsAccepted == true
                              select c1).ToList();
                return Json(result);
            }
            else if (SearchValue != "")
            {
                var result = (from c1 in _context.Recipes
                              join c2 in _context.Components on c1.ID_Recipe equals c2.ID_Recipe
                              where c2.NameOfComponent.StartsWith(SearchValue) && c1.IsAccepted == true
                              group c1 by c1.Title into groups
                              select groups).ToList();
                return Json(result);
            }
            else
            {
                return Json(null);
            }
        }
    }
}
