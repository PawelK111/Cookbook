using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookbookPI.Models.Entities;

namespace CookbookPI.Models
{
    public class RecipeInfo
    {
        public Recipes Recipe { get; set; }
        public IQueryable<string> Author { get; set; }
        public IQueryable<string> TypeOfKitchen { get; set; }
        public IQueryable<string> Category { get; set; }
        public IQueryable<string> Difficulty { get; set; }
        public IQueryable<int> NumberOfPeople { get; set; }
        public IQueryable<string> TimeOfPrepare { get; set; }
        public IQueryable<Components> Components { get; set; }

    }
}
