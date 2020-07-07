using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using CookbookPI.Models.Entities;

namespace CookbookPI.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            DatabaseContext context = app.ApplicationServices.GetRequiredService<DatabaseContext>();

            if(!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Title = "Przystawki" },
                    new Category { Title = "Zupy" },
                    new Category { Title = "Dania główne" },
                    new Category { Title = "Desery" },
                    new Category { Title = "Napoje" },
                    new Category { Title = "Przekąski" },
                    new Category { Title = "Wegetariańskie" },
                    new Category { Title = "Dietetyczne" }
                    );
                context.SaveChanges();
            }
            if (!context.Difficulty.Any())
            {
                context.Difficulty.AddRange(
                    new Difficulty { Title = "Łatwy" },
                    new Difficulty { Title = "Średni" },
                    new Difficulty { Title = "Trudny" }
                    );
                context.SaveChanges();
            }
            if (!context.NumberOfPeople.Any())
            {
                context.NumberOfPeople.AddRange(
                    new NumberOfPeople { Amount = 1 },
                    new NumberOfPeople { Amount = 2 },
                    new NumberOfPeople { Amount = 3 },
                    new NumberOfPeople { Amount = 4 },
                    new NumberOfPeople { Amount = 5 },
                    new NumberOfPeople { Amount = 6 },
                    new NumberOfPeople { Amount = 7 },
                    new NumberOfPeople { Amount = 8 },
                    new NumberOfPeople { Amount = 9 },
                    new NumberOfPeople { Amount = 10 }
                    );
                context.SaveChanges();
            }
            if (!context.TimeOfPrepares.Any())
            {
                context.TimeOfPrepares.AddRange(
                    new TimeOfPrepares { TimeOfPrepare = "Poniżej 15 minut"},
                    new TimeOfPrepares { TimeOfPrepare = "15 minut" },
                    new TimeOfPrepares { TimeOfPrepare = "30 minut" },
                    new TimeOfPrepares { TimeOfPrepare = "45 minut" },
                    new TimeOfPrepares { TimeOfPrepare = "60 minut" },
                    new TimeOfPrepares { TimeOfPrepare = "90 minut" },
                    new TimeOfPrepares { TimeOfPrepare = "Powyżej 90 minut" }
                    );
                context.SaveChanges();
            }
            if (!context.TypeOfKitchen.Any())
            {
                context.TypeOfKitchen.AddRange(
                    new TypeOfKitchen { Title = "Polska" },
                    new TypeOfKitchen { Title = "Włoska" },
                    new TypeOfKitchen { Title = "Francuska" },
                    new TypeOfKitchen { Title = "Chińska" },
                    new TypeOfKitchen { Title = "Amerykańska" },
                    new TypeOfKitchen { Title = "Brytyjska" },
                    new TypeOfKitchen { Title = "Austriacka" },
                    new TypeOfKitchen { Title = "Hiszpańska" },
                    new TypeOfKitchen { Title = "Szwajcarska" }
                    );
                context.SaveChanges();
            }
        }
    }
}
