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
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> AddRecipe(List<IFormFile> Photo, [Bind("ID_Recipe, ID_User, Title, ID_TypeOfKitchen, ID_Category, ID_Difficulty, " +
            "ID_NumberOfPeople, ID_TimeOfPrepare, IsAccepted, Description, Instruction")] Recipes recipe)
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

                    ViewBag.Status = true;
                    _context.Add(recipe);
                    _context.SaveChanges();
                    return new EmptyResult();
                }
                catch(Exception ex)
                {
                    ViewBag.ErrorStatus = true;
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
                var id_recipe = (from c in _context.Recipes select c.ID_Recipe).LastOrDefault();
                if (components == null)
                {
                    components = new List<Components>();
                }
                //Loop and insert records.
                foreach (Components x in components)
                {
                    x.ID_Recipe = id_recipe;
                    _context.Components.Add(x);
                }
                int insertedRecords = _context.SaveChanges();
                return Json(insertedRecords);
            }
            catch(Exception ex)
            {
                return Json(ex);
            }
        }
    }
}