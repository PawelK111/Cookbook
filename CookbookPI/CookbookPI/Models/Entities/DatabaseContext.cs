using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CookbookPI.Models.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<User_Permission> User_Permissions { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Difficulty> Difficulty { get; set; }
        public DbSet<NumberOfPeople> NumberOfPeople { get; set; }
        public DbSet<TimeOfPrepares> TimeOfPrepares { get; set; }
        public DbSet<TypeOfKitchen> TypeOfKitchen { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Components> Components { get; set; }
        public DbSet<FavoriteRecipes> FavoriteRecipes { get; set; }
    }
}
