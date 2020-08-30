using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.IO;

namespace CookbookPI.Models.Entities
{
    public class Recipes
    {
        [Key]
        public int? ID_Recipe { get; set; }
        [ForeignKey("Users")]
        public int? ID_User { get; set; }
        //  [Required(ErrorMessage = "Wprowadź tytuł przepisu")]
        [DisplayName("Nazwa przepisu")]
        public string Title { get; set; }
        [ForeignKey("TypeOfKitchen")]
        public int ID_TypeOfKitchen { get; set; }
        [ForeignKey("Category")]
        public int ID_Category { get; set; }
        [ForeignKey("Difficulty")]
        public int ID_Difficulty { get; set; }
        [ForeignKey("NumberOfPeople")]
        public int ID_NumberOfPeople { get; set; }
        [ForeignKey("TimeOfPrepares")]
        public int ID_TimeOfPrepare { get; set; }
        public bool IsAccepted { get; set; }
        //  [Required(ErrorMessage = "Wprowadź krótki opis")]
        public string Description { get; set; }
        //   [Required(ErrorMessage = "Wprowadź sposób wykonania przepisu")]
        public string Instruction { get; set; }
        public byte[] Photo { get; set; }
        public Users Users { get; set; }
        public TypeOfKitchen TypeOfKitchen { get; set; }
        public Category Category { get; set; }
        public Difficulty Difficulty { get; set; }
        public NumberOfPeople NumberOfPeople { get; set; }
        public TimeOfPrepares TimeOfPrepares { get; set; }
    }
}
