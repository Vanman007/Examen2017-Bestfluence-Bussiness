using ExamProject2017.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Configuration
{
    public static class PlatformSeed
    {
        public static void SeedData(ApplicationDbContext DbContext)
        {

            if (DbContext.Platform.Any())
                return;

            DbContext.Platform.Add(new Models.Platform { Name = "Youtube" });
            DbContext.Platform.Add(new Models.Platform { Name = "Instagram"});
            DbContext.SaveChanges();
        }
    }
}
