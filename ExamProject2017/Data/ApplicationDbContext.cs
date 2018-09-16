using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExamProject2017.Models;

namespace ExamProject2017.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<YoutubeData> YoutubeData { get; set; }

        public DbSet<BusinessProfile> BusinessProfile { get; set; }
        public DbSet<InfluencerProfile> InfluencerProfile { get; set; }

        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<InstagramData> InstagramData { get; set; }
        public DbSet<InstagramCity> InstagramCity { get; set; }
        public DbSet<InstagramCountry> InstagramCountry { get; set; }
        public DbSet<InstagramAgeGroup> InstagramAgeGroup { get; set; }

        public DbSet<InfluencerCategory> InfluencerCategory { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<InfluencerPlatform> InfluencerPlatform { get; set; }
        public DbSet<Platform> Platform { get; set; }

        // Dashboard
        public DbSet<InfluencerList> InfluencerLists { get; set; }
        public DbSet<ListObject> ListObjects { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
