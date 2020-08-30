using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace CookbookPI.Models.Entities
{
    public class Users
    {
        [Key]
        public int ID_User { get; set; }
        [ForeignKey("User_Permission")]
        [Display(Name = "Typ konta")]
        public int ID_Permission { get; set; }
        [Display(Name = "Nazwa użytkownika")]
        public string Nickname { get; set; }
        public string Passwrd { get; set; }
        [Display(Name = "Adres E-Mail")]
        public string Email { get; set; }
        [Display(Name = "Data rejestracji")]
        public DateTime DateOfRegistration { get; set; }
        public bool isBanned { get; set; }
        public int NumberOfRecipes { get; set; }
        [Display(Name = "Typ konta")]
        public User_Permission User_Permission { get; set; }

        public Users(int permissions, bool banned, string nickname, string passwrd, DateTime dateofRegistration, string email, int numberRecipes)
        {
            ID_Permission = permissions;
            isBanned = banned;
            Nickname = nickname;
            Passwrd = passwrd;
            DateOfRegistration = dateofRegistration;
            Email = email;
            NumberOfRecipes = numberRecipes;
        }
        public Users()
        {

        }
    }
}
