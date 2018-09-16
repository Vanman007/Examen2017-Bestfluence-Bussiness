using ExamProject2017.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Configuration
{
    public static class CategoriesSeed
    {
        public static void SeedData(ApplicationDbContext DbContext)
        {

            if (DbContext.Category.Any())
                return;

            DbContext.Category.Add(new Models.Category { Name = "Fashion" });
            DbContext.Category.Add(new Models.Category { Name = "Interests" });
            DbContext.Category.Add(new Models.Category { Name = "Entertainment" });
            DbContext.Category.Add(new Models.Category { Name = "Personal" });
            DbContext.Category.Add(new Models.Category { Name = "Lifestyle" });
            DbContext.Category.Add(new Models.Category { Name = "Gaming" });
            DbContext.SaveChanges();
        }
    }
}
