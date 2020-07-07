using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CookbookPI.Models.Entities;
using CookbookPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Web;
namespace CookbookPI.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext db;
        public AccountController(DatabaseContext _db)
        {
            db = _db;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUp newUser)
        {
            string message = "";
            bool status = false;
            if (ModelState.IsValid)
            {
                if (db.Users.Any(a => a.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Konto z podanym E-mail już istnieje!");
                    return View();
                }
                if (db.Users.Any(a => a.Nickname == newUser.Nick))
                {
                    ModelState.AddModelError("Nick", "Istnieje konto z podanym nickiem!");
                    return View();
                }
                if(newUser.AcceptRules == false)
                {
                    ModelState.AddModelError("AcceptRules", "Musisz zaakceptować regulamin!");
                    return View();
                }

                newUser.Passwrd = HashCryptPass.HassPass(newUser.Passwrd);
                newUser.ConfirmPasswrd = HashCryptPass.HassPass(newUser.ConfirmPasswrd);
                using (db)
                {
                    Users usr = new Users(1, false, newUser.Nick, newUser.Passwrd, DateTime.Now, newUser.Email);
                    db.Add(usr);
                    db.SaveChanges();
                    status = true;
                }
            }
            else
                message = "Invalid request!";

            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(newUser);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(SignIn user)
        {
            if (ModelState.IsValid)
            {
                user.Passwrd = HashCryptPass.HassPass(user.Passwrd);
                if (db.Users.Any(a => a.Nickname == user.Nickname))
                {
                    if (db.Users.Any(a => a.Passwrd == user.Passwrd))
                    {
                        HttpContext.Session.SetString("nickname", user.Nickname);
                        var id_user = (from x in db.Users where x.Nickname == user.Nickname select x.ID_User).FirstOrDefault();
                        HttpContext.Session.SetInt32("ID_USER", id_user);
                        var permission = (from x in db.Users where x.Nickname == user.Nickname select x.ID_Permission).FirstOrDefault();
                        HttpContext.Session.SetInt32("Permission", permission);
                        return View(user);
                    }
                    else
                    {
                        ModelState.AddModelError("Passwrd", "Podane hasło jest nieprawidłowe!");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("Nick", "Nie istnieje użytkownik z takim nickiem!");
                    return View();
                }

            }
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Route("Profil")]
        public async Task<IActionResult> Profile()
        {
            var user = await db.Users
                .FirstOrDefaultAsync(m => m.ID_User == HttpContext.Session.GetInt32("ID_USER"));
            return View(user);
        }
    }

}