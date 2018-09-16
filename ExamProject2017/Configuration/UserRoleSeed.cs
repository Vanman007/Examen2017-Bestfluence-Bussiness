using ExamProject2017.Data;
using ExamProject2017.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Configuration
{
    public class UserRoleSeed
    {
        public static void SeedData(ApplicationDbContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                dbContext.Roles.Add(new IdentityRole() { Id = "1", Name = "Admin", NormalizedName = "ADMIN" });
                dbContext.Roles.Add(new IdentityRole() { Id = "2", Name = "Influencer", NormalizedName = "INFLUENCER" });
                dbContext.Roles.Add(new IdentityRole() { Id = "3", Name = "Business", NormalizedName = "BUSINESS" });
            }



            if (!dbContext.Users.Any())
            {
                var userStore = new UserStore<ApplicationUser>(dbContext);
                var userArray = new ApplicationUser[50];
                for (int i = 0; i < 50; i++)
                {
                    userArray[i] = new ApplicationUser()
                    {
                        Id = i.ToString(),
                        Email = "influencer" + i + "@influencer.com",
                        NormalizedEmail = "INFLUENCER" + i + "@INFLUENCER.COM",
                        UserName = "influencer" + i + "@influencer.com",
                        NormalizedUserName = "INFLUENCER" + i + "@INFLUENCER.COM",
                        PhoneNumber = "29636063",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        InfluencerProfile = new InfluencerProfile()
                        {
                            Id = i.ToString(),
                            Address = "Address " + i,
                            AgencyName = "Agency name " + i,
                            BankAccount = "Banknumber " + i,
                            Characteristic = "Funny, Handsome, Sweet",
                            CPR = ((i + i) * 1).ToString(),
                            CVR = ((i + i) * i).ToString(),
                            Firstname = "Firstname " + i,
                            Lastname = "Lastname " + i,
                            PriceFrom = i * i,
                            WorkDescription = "On a scale from 0 to 50 i am " + i
                        }
                    };
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(userArray[i], "test123");
                    userArray[i].PasswordHash = hashed;
                    var result = userStore.CreateAsync(userArray[i]);
                    dbContext.UserRoles.Add(new IdentityUserRole<string>() { RoleId = "2", UserId = userArray[i].Id });
                }
            }

            if (!dbContext.Country.Any())
            {
                var list = new List<Country>()
                {
                    new Country(){Id = "0", Name = "DK"},
                    new Country(){Id = "1", Name = "SE"},
                    new Country(){Id = "2", Name = "NO"},
                    new Country(){Id = "3", Name = "GB"},
                    new Country(){Id = "4", Name = "RU"},
                    new Country(){Id = "5", Name = "US"},
                    new Country(){Id = "6", Name = "PL"},
                };
                dbContext.Country.AddRange(list);
                dbContext.SaveChanges();
            }

            if (!dbContext.Category.Any())
            {
                dbContext.Category.Add(new Models.Category { Id = "0", Name = "Fashion" });
                dbContext.Category.Add(new Models.Category { Id = "1", Name = "Interests" });
                dbContext.Category.Add(new Models.Category { Id = "2", Name = "Entertainment" });
                dbContext.Category.Add(new Models.Category { Id = "3", Name = "Personal" });
                dbContext.Category.Add(new Models.Category { Id = "4", Name = "Lifestyle" });
                dbContext.Category.Add(new Models.Category { Id = "5", Name = "Gaming" });
                dbContext.SaveChanges();
            }

            if (!dbContext.Platform.Any())
            {
                dbContext.Platform.Add(new Models.Platform { Id = "0", Name = "Youtube" });
                dbContext.Platform.Add(new Models.Platform { Id = "1", Name = "Instagram" });
                dbContext.SaveChanges();
            }

            if (!dbContext.InfluencerCategory.Any())
            {
                Random random = new Random();
                for (int i = 0; i < 50; i++)
                {
                    var categories = new InfluencerCategory()
                    {
                        InfluencerProfileId = i.ToString(),
                        CategoryId = random.Next(0, 5) + ""
                    };
                    dbContext.InfluencerCategory.Add(categories);
                    dbContext.SaveChanges();
                }
            }

            if (!dbContext.InfluencerPlatform.Any())
            {
                for (int i = 0; i < 50; i++)
                {
                    dbContext.InfluencerPlatform.Add(new InfluencerPlatform() { InfluencerProfileId = i.ToString(), PlatformId = "1" });
                    dbContext.SaveChanges();
                }
            }

            if (!dbContext.City.Any())
            {
                var list = new List<City>()
                {
                    new City(){ Id = "0", Name = "Frederiksberg"},
                    new City(){ Id = "1", Name = "Roskilde"},
                    new City(){ Id = "2", Name = "København"},
                    new City(){ Id = "3", Name = "Køge"},
                    new City(){ Id = "4", Name = "Hundige"},
                    new City(){ Id = "5", Name = "Vallensbæk"},
                    new City(){ Id = "6", Name = "Solrød"},
                };
                dbContext.City.AddRange(list);
                dbContext.SaveChanges();
            }

            if (!dbContext.InstagramData.Any())
            {
                for (int i = 0; i < 50; i++)
                {
                    var instagramData = new InstagramData()
                    {
                        Id = i.ToString(),
                        ApplicationUserId = i.ToString(),
                        FollowerCount = i * 1000,
                        MediaCount = i * 10,
                        DayImpression = i * 10,
                        WeekImpression = i * 20,
                        MonthImpression = i * 30,
                        DayReach = i * 10,
                        WeekReach = i * 20,
                        MonthReach = i * 30,
                        LastUpdated = DateTime.Now,
                        InstagramCity = new List<InstagramCity>(),
                        InstagramCountry = new List<InstagramCountry>()
                    };

                    Random random = new Random();
                    instagramData.InstagramCity.Add(new InstagramCity() { CityId = "0", Count = random.Next(0, 100000) });
                    instagramData.InstagramCity.Add(new InstagramCity() { CityId = "1", Count = random.Next(0, 100000) });
                    instagramData.InstagramCity.Add(new InstagramCity() { CityId = "2", Count = random.Next(0, 100000) });
                    instagramData.InstagramCity.Add(new InstagramCity() { CityId = "3", Count = random.Next(0, 100000) });
                    instagramData.InstagramCity.Add(new InstagramCity() { CityId = "4", Count = random.Next(0, 100000) });
                    instagramData.InstagramCity.Add(new InstagramCity() { CityId = "5", Count = random.Next(0, 100000) });
                    instagramData.InstagramCity.Add(new InstagramCity() { CityId = "6", Count = random.Next(0, 100000) });

                    instagramData.InstagramCountry.Add(new InstagramCountry() { CountryId = "0", Count = random.Next(0, 10000) });
                    instagramData.InstagramCountry.Add(new InstagramCountry() { CountryId = "1", Count = random.Next(0, 10000) });
                    instagramData.InstagramCountry.Add(new InstagramCountry() { CountryId = "2", Count = random.Next(0, 10000) });
                    instagramData.InstagramCountry.Add(new InstagramCountry() { CountryId = "3", Count = random.Next(0, 10000) });
                    instagramData.InstagramCountry.Add(new InstagramCountry() { CountryId = "4", Count = random.Next(0, 10000) });
                    instagramData.InstagramCountry.Add(new InstagramCountry() { CountryId = "5", Count = random.Next(0, 10000) });
                    instagramData.InstagramCountry.Add(new InstagramCountry() { CountryId = "6", Count = random.Next(0, 10000) });

                    instagramData.InstagramAgeGroup = new InstagramAgeGroup()
                    {
                        Id = i.ToString(),
                        InstagramDataId = i.ToString(),
                        Female13To17 = random.Next(0, 5000),
                        Female18To24 = random.Next(0, 5000),
                        Female25To34 = random.Next(0, 5000),
                        Female35To44 = random.Next(0, 5000),
                        Female45To55 = random.Next(0, 5000),
                        Female55To64 = random.Next(0, 5000),
                        Female65Plus = random.Next(0, 5000),
                        Male13To17 = random.Next(0, 5000),
                        Male18To24 = random.Next(0, 5000),
                        Male25To34 = random.Next(0, 5000),
                        Male35To44 = random.Next(0, 5000),
                        Male45To55 = random.Next(0, 5000),
                        Male55To64 = random.Next(0, 5000),
                        Male65Plus = random.Next(0, 5000),
                    };
                    dbContext.InstagramData.Add(instagramData);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
