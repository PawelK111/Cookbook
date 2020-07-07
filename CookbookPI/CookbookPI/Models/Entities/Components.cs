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
    public class Components
    {
        [Key]
        public int ID_Component { get; set; }
        [ForeignKey("Recipes")]
        public int ? ID_Recipe { get; set; }
        public string NameOfComponent { get; set; }
        public string Unit { get; set; }
        public int Amount { get; set; }
        public Recipes Recipes { get; set; }

    }
}
