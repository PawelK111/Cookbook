using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookbookPI.Models
{
    public class SignUp
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Proszę wprowadzić nazwę użytkownika!")]
        [Display(Name = "Podaj nazwę użytkownika")]
        public string Nick { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Proszę wprowadzić hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Podaj hasło")]
        [MinLength(8, ErrorMessage = "Hasło powinno składać się z min. 6 znaków")]
        public string Passwrd { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić potwierdzenie hasła")]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Passwrd", ErrorMessage = "Wprowadzone hasła nie są takie same!")]
        public string ConfirmPasswrd { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Proszę wprowadzić E-mail")]
        [Display(Name = "Podaj adres e-mail")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Musisz zaakceptować regulamin")]
        [Display(Name = "Akceptuję regulamin")]
        public bool AcceptRules { get; set; }
    }
}
