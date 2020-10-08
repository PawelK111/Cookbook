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
        private readonly DatabaseContext context;
        public AccountController(DatabaseContext _context)
        {
            context = _context;
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
                if (context.Users.Any(a => a.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Konto z podanym E-mail już istnieje!");
                    return View();
                }
                if (context.Users.Any(a => a.Nickname == newUser.Nick))
                {
                    ModelState.AddModelError("Nick", "Konto z podaną nazwą użytkownika już istnieje!");
                    return View();
                }
                newUser.Passwrd = HashCryptPass.HassPass(newUser.Passwrd);
                using (context)
                {
                    Users usr = new Users(1, false, newUser.Nick, newUser.Passwrd, DateTime.Now, newUser.Email, 0);
                    context.Add(usr);
                    context.SaveChanges();
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
                if (context.Users.Any(a => a.Nickname == user.Nickname))
                {
                    var currentAccount = context.Users.SingleOrDefault(a => a.Nickname.Equals(user.Nickname));
                    if (HashCryptPass.ValidatePass(user.Passwrd, currentAccount.Passwrd))
                    {
                        HttpContext.Session.SetString("nickname", currentAccount.Nickname);
                        HttpContext.Session.SetInt32("ID_USER", currentAccount.ID_User);
                        HttpContext.Session.SetInt32("ID_Permission", currentAccount.ID_Permission);
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
                    ModelState.AddModelError("Nick", "Nie istnieje użytkownik o takiej nazwie!");
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
        public IActionResult Profile()
        {
            var user = context.Users.Where(m => m.ID_User == HttpContext.Session.GetInt32("ID_USER")).FirstOrDefault();
            return View(user);
        }
    }

}