using System.Linq;
using Insurance.Data.Models;

namespace Insurance.Data.Data
{
    public static class DbInitializer
    {
        public static void Initialize(InsuranceContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            var categories = new[]
            {
                new Category{Id = 1, Name = "Electronics"},
                new Category{Id = 2, Name = "Jewelry"},
                new Category{Id = 3, Name = "Furniture"},
                new Category{Id = 4, Name = "Clothing"},
                new Category{Id = 5, Name = "Kitchen"},
                new Category{Id = 6, Name = "Art"},
                new Category{Id = 7, Name = "Miscellaneous"}
            };
            foreach (var c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();
        }
    }
}
