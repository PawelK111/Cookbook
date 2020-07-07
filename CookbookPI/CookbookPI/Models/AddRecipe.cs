using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookbookPI.Models
{
    public class AddRecipe
    {
        public int ID_Recipe { get; set; }
        public int ? ID_User { get; set; }
        public string Title { get; set; }
        public int ID_TypeOfKitchen { get; set; }
        public int ID_Category { get; set; }
        public int ID_Difficulty { get; set; }
        public int ID_NumberOfPeople { get; set; }
        public int ID_TimeOfPrepare { get; set; }
        public byte[] Photo { get; set; }

        public bool IsAccepted = false;
        public string Description { get; set; }
        public string Instruction { get; set; }
    }
}
