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
    public class FavoriteRecipes
    {
        [Key]
        public int? ID_FavRecipe { get; set; }
        [ForeignKey("Users")]
        public int? ID_User { get; set; }
        [ForeignKey("Recipes")]
        public int? ID_Recipe { get; set; }
        public Users Users { get; set; }
        public Recipes Recipes { get; set; }
    }
}
