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
    public class NumberOfPeople
    {
        [Key]
        public int ID_NumberOfPeople { get; set; }
        public int Amount { get; set; }
    }
}
