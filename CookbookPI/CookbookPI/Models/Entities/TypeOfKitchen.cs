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
    public class TypeOfKitchen
    {
        [Key]
        public int ID_TypeOfKitchen { get; set; }
        public string Title { get; set; }
    }
}
