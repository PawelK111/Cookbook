using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult GetUsers()
        {
            var databaseContext = _context.Users.Include(u => u.User_Permission);
            return PartialView(databaseContext.ToList());
        }
        public IActionResult GetRecipesNotAcc()
        {
            var databaseContext = from c in _context.Recipes where c.IsAccepted == false select c;
            return PartialView(databaseContext.ToList());
        }
        public async Task<IActionResult> GetAllRecipes()
        {
            var databaseContext = _context.Users.Include(u => u.User_Permission);
            return PartialView(await databaseContext.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminPanel));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.ID_User == id);
        }
    }
}
