using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CookbookPI.Models
{
    public class SignIn
    {
        public int ID { get; set; }
        public int ID_Permission { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wprowadź nazwę użytkownika!")]
        [Display(Name = "Nazwa użytkownika")]
        public string Nickname { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wprowadź hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Passwrd { get; set; }
    }
}
