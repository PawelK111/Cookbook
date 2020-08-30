using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CookbookPI.Models;
using CookbookPI.Models.Entities;

namespace CookbookPI.Controllers
{
    public class AdminController : Controller
    {
        private readonly DatabaseContext _context;

        public AdminController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult AdminPanel()
        {
            return View();
        }
        // GET: Admin
        [HttpGet]
        public IActionResult GetUsers()
        {
            var databaseContext = _context.Users.OrderByDescending(x => x.ID_Permission);
            return PartialView(databaseContext.ToList());
        }
        [HttpGet]
        public IActionResult GetRecipesNotAccept()
        {
            var databaseContext = _context.Recipes.Where(u => u.IsAccepted == false);
            return PartialView(databaseContext.ToList());
        }
        [HttpGet]
        public IActionResult GetRecipesAccept()
        {
            var databaseContext = _context.Recipes.Where(u => u.IsAccepted == true);
            return PartialView(databaseContext.ToList());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> UserInfo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.User_Permission)
                .FirstOrDefaultAsync(m => m.ID_User == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewData["ID_Permission"] = new SelectList(_context.User_Permissions, "ID_Permission", "ID_Permission");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_User,ID_Permission,Nickname,Passwrd,Email,DateOfRegistration,isBanned")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdminPanel));
            }
            ViewData["ID_Permission"] = new SelectList(_context.User_Permissions, "ID_Permission", "ID_Permission", users.ID_Permission);
            return View(users);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            ViewData["ID_Permission"] = new SelectList(_context.User_Permissions, "ID_Permission", "ID_Permission", users.ID_Permission);
            return View(users);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_User,ID_Permission,Nickname,Email,isBanned")] Users users)
        {
            if (id != users.ID_User)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.ID_User))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminPanel));
            }
            ViewData["ID_Permission"] = new SelectList(_context.User_Permissions, "ID_Permission", "ID_Permission", users.ID_Permission);
            return View(users);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.User_Permission)
                .FirstOrDefaultAsync(m => m.ID_User == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRecipe(int? id)
        {
            var recipe = _context.Recipes.Find(id);
            _context.Components.FromSql($"DELETE FROM COMPONENTS WHERE ID_RECIPE = {id}");
            _context.Recipes.Remove(recipe);
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminPanel));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.ID_User == id);
        }
    }
}
