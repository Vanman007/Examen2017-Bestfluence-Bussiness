using ExamProject2017.Configuration;
using ExamProject2017.Data;
using ExamProject2017.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ExamProject2017.Models.InfluencerViewModels;
using ExamProject2017.Repository;
using ExamProject2017.Models.HomeViewModel;

namespace ExamProject2017.Controllers
{
    [Authorize]
    public class InfluencerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AppKeyConfig AppConfigs { get; }
        private readonly IRepository<YoutubeData> _YoutubeRepo;
        private readonly RoleManager<IdentityRole> _roleManager;


        public InfluencerController(RoleManager<IdentityRole> roleManager, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IOptions<AppKeyConfig> appkeys, IRepository<YoutubeData> youtubeRepo)
        {
            _YoutubeRepo = youtubeRepo;
            _context = context;
            _userManager = userManager;
            AppConfigs = appkeys.Value;
            _roleManager = roleManager;

        }

        public async Task<IActionResult> Test()
        {
            var user = await _userManager.GetUserAsync(User);
            var YoutubePlatformId = _context.Platform.FirstOrDefault(x => x.Name == "Youtube").Id;
            if (_context.InfluencerPlatform.Any(x => x.InfluencerProfileId == user.Id && x.PlatformId == YoutubePlatformId))
            {
            }
            else
            {
                _context.InfluencerPlatform.Add(new InfluencerPlatform() { PlatformId = YoutubePlatformId, InfluencerProfileId = user.Id });
                _context.SaveChanges();
            }

            return View("Test");
        }

        [HttpPost]
        public async Task<JsonResult> Test(YoutubeViewModel model)
        {


            YoutubeData data = new YoutubeData()
            {
                Views = model.Views,
                FemaleViews = model.FemaleViews,
                Likes = model.Likes,
                Subcribers = model.Subcribers,
                MaleViews = model.MaleViews,
                Comments = model.Comments,
                Dislike = model.Dislike,
                Engagement = model.Engagement,
            };

            _YoutubeRepo.Add(data);

            return Json(true);
        }


        [HttpGet]
        public IActionResult Search(string name)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Watch()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);

            if (_context.InfluencerProfile.Any(x => x.Id == user.Id))
            {
                var influencerProfile = await _context.InfluencerProfile
                    .Include(x => x.InfluencerCategory)
                        .ThenInclude(x => x.Category)
                    .Include(x => x.InfluencerPlatforms)
                        .ThenInclude(x => x.Platform)
                        .SingleOrDefaultAsync(x => x.Id == user.Id);
                var model = new EditViewModel()
                {
                    ProfileIsCreated = true,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Address = influencerProfile.Address,
                    AgencyName = influencerProfile.AgencyName,
                    BankAccount = influencerProfile.BankAccount,
                    Characteristic = influencerProfile.Characteristic,
                    CPR = influencerProfile.CPR,
                    CVR = influencerProfile.CVR,
                    Entertainment = (influencerProfile.InfluencerCategory.Any(x => x.Category.Name == "Entertainment")),
                    Fashion = (influencerProfile.InfluencerCategory.Any(x => x.Category.Name == "Fashion")),
                    Gaming = (influencerProfile.InfluencerCategory.Any(x => x.Category.Name == "Gaming")),
                    Interests = (influencerProfile.InfluencerCategory.Any(x => x.Category.Name == "Interests")),
                    LifeStyle = (influencerProfile.InfluencerCategory.Any(x => x.Category.Name == "Lifestyle")),
                    Personal = (influencerProfile.InfluencerCategory.Any(x => x.Category.Name == "Personal")),
                    Firstname = influencerProfile.Firstname,
                    Lastname = influencerProfile.Lastname,
                    PriceFrom = influencerProfile.PriceFrom,
                    WorkDescription = influencerProfile.WorkDescription
                };
                return View(model);
            }
            return View(new EditViewModel() { ProfileIsCreated = false, Email = user.Email });

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                if (_context.InfluencerProfile.Any(x => x.Id == user.Id))
                {
                    var influencerProfile = await _context.InfluencerProfile.Include(x => x.InfluencerCategory).FirstOrDefaultAsync(x => x.Id == user.Id);
                    influencerProfile.Firstname = model.Firstname;
                    influencerProfile.Lastname = model.Lastname;
                    influencerProfile.PriceFrom = model.PriceFrom;
                    influencerProfile.Address = model.Address;
                    influencerProfile.AgencyName = model.AgencyName;
                    influencerProfile.BankAccount = model.BankAccount;
                    influencerProfile.Characteristic = model.Characteristic;
                    influencerProfile.CPR = model.CPR;
                    influencerProfile.CVR = model.CVR;
                    influencerProfile.WorkDescription = model.WorkDescription;
                    user.PhoneNumber = model.Phone;
                    user.Email = model.Email;
                    await _userManager.UpdateAsync(user);
                    _context.InfluencerProfile.Update(influencerProfile);
                    _context.SaveChanges();
                }
                else
                {
                    var influencerProfile = new InfluencerProfile()
                    {
                        Id = user.Id,
                        Address = model.Address,
                        AgencyName = model.AgencyName,
                        BankAccount = model.BankAccount,
                        Characteristic = model.Characteristic,
                        CPR = model.CPR,
                        CVR = model.CVR,
                        Firstname = model.Firstname,
                        Lastname = model.Lastname,
                        PriceFrom = model.PriceFrom,
                        WorkDescription = model.WorkDescription
                    };
                    user.PhoneNumber = model.Phone;
                    user.Email = model.Email;
                    await _userManager.UpdateAsync(user);
                    await _context.InfluencerProfile.AddAsync(influencerProfile);
                    await _context.SaveChangesAsync();
                }
                var listcategories = _context.InfluencerCategory.Where(x => x.InfluencerProfileId == user.Id);
                foreach (var i in listcategories)
                {
                    _context.InfluencerCategory.Remove(i);
                }
                await _context.SaveChangesAsync();
                if (model.Entertainment) await _context.InfluencerCategory.AddAsync(new InfluencerCategory() { InfluencerProfileId = user.Id, CategoryId = _context.Category.FirstOrDefault(x => x.Name == "Entertainment").Id });
                if (model.Fashion) await _context.InfluencerCategory.AddAsync(new InfluencerCategory() { InfluencerProfileId = user.Id, CategoryId = _context.Category.FirstOrDefault(x => x.Name == "Fashion").Id });
                if (model.Gaming) await _context.InfluencerCategory.AddAsync(new InfluencerCategory() { InfluencerProfileId = user.Id, CategoryId = _context.Category.FirstOrDefault(x => x.Name == "Gaming").Id });
                if (model.Interests) await _context.InfluencerCategory.AddAsync(new InfluencerCategory() { InfluencerProfileId = user.Id, CategoryId = _context.Category.FirstOrDefault(x => x.Name == "Interests").Id });
                if (model.LifeStyle) await _context.InfluencerCategory.AddAsync(new InfluencerCategory() { InfluencerProfileId = user.Id, CategoryId = _context.Category.FirstOrDefault(x => x.Name == "LifeStyle").Id });
                if (model.Personal) await _context.InfluencerCategory.AddAsync(new InfluencerCategory() { InfluencerProfileId = user.Id, CategoryId = _context.Category.FirstOrDefault(x => x.Name == "Personal").Id });
                await _context.SaveChangesAsync();
                model.ProfileIsCreated = true;

                return View(model);
            }
            return View(model);
        }

        public IActionResult Edit2()
        {
            return View();
        }


        [AllowAnonymous]
        public async Task<IActionResult> MediaKit(string Id)
        {
            string UserId = _context.Users.FirstOrDefault(x => x.Id == Id).Id;
            var listKategories = _context.InfluencerCategory.Where(x => x.InfluencerProfileId == UserId).Include(x => x.Category).ToList();
            var listPlatforms = _context.InfluencerPlatform.Where(x => x.InfluencerProfileId == UserId).Include(x => x.Platform).ToList();
            var profile = _context.InfluencerProfile.FirstOrDefault(x => x.Id == UserId);

            var instagramData = await _context.InstagramData.SingleOrDefaultAsync(x => x.ApplicationUserId == UserId); 
            
            var MediaKitVM = new Models.InfluenterViewModels.MediaKitViewModel
            {
                InstagramFollowers = (instagramData == null) ? 0 : instagramData.FollowerCount,
                InstagramPosts = (instagramData == null) ? 0 : instagramData.MediaCount,
                InstagramDayImpression = (instagramData == null) ? 0 : instagramData.DayImpression,
                InstagramDayReach = (instagramData == null) ? 0 : instagramData.DayReach,
                InflucencerProfile = (profile == null) ? new InfluencerProfile() : profile,
                InfluenterPlatform = listPlatforms,
                InfluenterKategori = listKategories,
                ApplicationUserId = UserId
            };
            return View(MediaKitVM);
        }

        [AllowAnonymous]
        public JsonResult InstagramData(string id)
        {
            var GetInstagramData = _context.InstagramData.
                Include(x => x.InstagramAgeGroup).
                Include(x => x.InstagramCity).
                ThenInclude(x => x.City).Include(x => x.InstagramCountry).
                ThenInclude(x => x.Country).
                SingleOrDefault(x => x.ApplicationUserId == id);

            GetInstagramData.InstagramAgeGroup.InstagramData = null;
            GetInstagramData.ApplicationUser = null;
            GetInstagramData.InstagramCity.Select(x => { x.InstagramData = null; return x; }).ToList();
            GetInstagramData.InstagramCountry.Select(x => { x.InstagramData = null; return x; }).ToList();

            return Json(GetInstagramData);
        }

        [AllowAnonymous]
        public JsonResult YoutubeData(string id)
        {
            var GetYoutubeData = _YoutubeRepo.GetAllAsListAsync();

            return Json(GetYoutubeData);
        }

        [AllowAnonymous]
        public async Task<IActionResult> StatisticsInstagramAccounts(string code)
        {
            string UserId = (await _userManager.GetUserAsync(User)).Id;
            if (_context.InstagramData.Any(x => x.ApplicationUserId == UserId))
            {
                await InstagramDataUpdate(code);
            }
            else
            {
                await InstagramDataAdd(code);
            }
            return RedirectToAction("Edit");
        }

        public dynamic[] GetInstagramFacebookData(string code)
        {
            string InstagramBusinessId = "";
            dynamic Access_Token = "";

            dynamic[] AllDatas = new dynamic[3];
            //Authenticate (Get AcessToken)
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://graph.facebook.com/oauth/");
                HttpResponseMessage Access_Token_Response = client.GetAsync("access_token?client_id=" + AppConfigs.AppId + "&client_secret=" +
                    AppConfigs.AppSecret + "&code=" + code + "&redirect_uri=http://localhost:50862/influencer/StatisticsInstagramAccounts").Result;
                Access_Token_Response.EnsureSuccessStatusCode();
                string Access_Token_Result = Access_Token_Response.Content.ReadAsStringAsync().Result;
                Access_Token = JObject.Parse(Access_Token_Result);
                Access_Token = Access_Token.access_token;
            }
            //Get Business Id
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://graph.facebook.com/v2.10/");
                HttpResponseMessage BusinessId_Response = client.GetAsync("me/accounts?fields=instagram_business_account&redirect_uri=http://localhost:50862/influencer/StatisticsInstagramAccounts&access_token=" + Access_Token).Result;
                BusinessId_Response.EnsureSuccessStatusCode();
                string BusinessId_Result = BusinessId_Response.Content.ReadAsStringAsync().Result;
                dynamic Business_Data = JObject.Parse(BusinessId_Result);
                foreach (var i in Business_Data.data)
                {
                    if (i.instagram_business_account != null)
                    {
                        if (InstagramBusinessId != "")
                        {
                            //then he/she has a secondacc so redirect back to page and ask which it is
                        }
                        InstagramBusinessId = (string)i.instagram_business_account.id;
                    }
                }
            }
            //Get Instagram MetaData
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://graph.facebook.com/v2.10/");
                HttpResponseMessage MetaData_Response = client.GetAsync(InstagramBusinessId + "?fields=followers_count,media_count&redirect_uri=http://localhost:50862/influencer/StatisticsInstagramAccounts&access_token=" + Access_Token).Result;
                MetaData_Response.EnsureSuccessStatusCode();
                string MetaData_Result = MetaData_Response.Content.ReadAsStringAsync().Result;
                AllDatas[0] = JObject.Parse(MetaData_Result);
            }
            //Get Instagram Lifetime Data
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://graph.facebook.com/v2.10/");
                HttpResponseMessage LifeTime_Response = client.GetAsync(InstagramBusinessId + "/insights?metric=audience_gender_age,audience_country,audience_city&period=lifetime&redirect_uri=http://localhost:50862/influencer/StatisticsInstagramAccounts&access_token=" + Access_Token).Result;
                LifeTime_Response.EnsureSuccessStatusCode();
                string LifeTime_Result = LifeTime_Response.Content.ReadAsStringAsync().Result;
                AllDatas[1] = JObject.Parse(LifeTime_Result);
            }
            //Get all Instagram day,week and month info
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://graph.facebook.com/v2.10/");
                HttpResponseMessage Reach_Impression_Response = client.GetAsync(InstagramBusinessId + "/insights?metric=impressions,reach&period=day,week,days_28&redirect_uri=http://localhost:50862/influencer/StatisticsInstagramAccounts&access_token=" + Access_Token).Result;
                Reach_Impression_Response.EnsureSuccessStatusCode();
                string Reach_Impression_Result = Reach_Impression_Response.Content.ReadAsStringAsync().Result;
                AllDatas[2] = JObject.Parse(Reach_Impression_Result);
            }
            return AllDatas;
        }

        public async Task InstagramDataAdd(string code)
        {
            var AllDatas = GetInstagramFacebookData(code);
            string UserId = (await _userManager.GetUserAsync(User)).Id;
            InstagramData InstagramDatas = new InstagramData { ApplicationUserId = UserId };
            InstagramDatas.LastUpdated = DateTime.Now;
            List<InstagramCountry> InstagramCountryList = new List<InstagramCountry>();
            List<InstagramCity> InstagramCityList = new List<InstagramCity>();
            InstagramDatas.MediaCount = AllDatas[0].media_count;
            InstagramDatas.FollowerCount = AllDatas[0].followers_count;

            var InstagramAgeGroups = new InstagramAgeGroup()
            {
                Female13To17 = AllDatas[1].data[0].values[0].value["F.13-17"] ?? 0,
                Female18To24 = AllDatas[1].data[0].values[0].value["F.18-24"] ?? 0,
                Female25To34 = AllDatas[1].data[0].values[0].value["F.25-34"] ?? 0,
                Female35To44 = AllDatas[1].data[0].values[0].value["F.35-44"] ?? 0,
                Female45To55 = AllDatas[1].data[0].values[0].value["F.45-54"] ?? 0,
                Female55To64 = AllDatas[1].data[0].values[0].value["F.55-64"] ?? 0,
                Female65Plus = AllDatas[1].data[0].values[0].value["F.65+"] ?? 0,
                Male13To17 = AllDatas[1].data[0].values[0].value["M.13-17"] ?? 0,
                Male18To24 = AllDatas[1].data[0].values[0].value["M.18-24"] ?? 0,
                Male25To34 = AllDatas[1].data[0].values[0].value["M.25-34"] ?? 0,
                Male35To44 = AllDatas[1].data[0].values[0].value["M.35-44"] ?? 0,
                Male45To55 = AllDatas[1].data[0].values[0].value["M.45-54"] ?? 0,
                Male55To64 = AllDatas[1].data[0].values[0].value["M.55-64"] ?? 0,
                Male65Plus = AllDatas[1].data[0].values[0].value["M.65+"] ?? 0
            };
            InstagramDatas.InstagramAgeGroup = InstagramAgeGroups;

            var countries = _context.Country.ToList();
            foreach (var i in AllDatas[1].data[1].values[0].value)
            {
                if (!countries.Any(x => x.Name == i.Name))
                {
                    _context.Country.Add(new Country { Name = i.Name });
                    _context.SaveChanges();
                    countries = _context.Country.ToList();
                }
                var TheCountry = countries.FirstOrDefault(x => x.Name == (string)i.Name);
                var InstaCountry = new InstagramCountry()
                {
                    InstagramData = InstagramDatas,
                    InstagramDataId = InstagramDatas.Id,
                    CountryId = TheCountry.Id,
                    Country = TheCountry,
                    Count = i.Value
                };
                InstagramCountryList.Add(InstaCountry);
            }
            InstagramDatas.InstagramCountry = InstagramCountryList;

            var cities = _context.City.ToList();
            foreach (var i in AllDatas[1].data[2].values[0].value)
            {
                if (!cities.Any(x => x.Name == i.Name))
                {
                    _context.City.Add(new City { Name = i.Name });
                    _context.SaveChanges();
                    cities = _context.City.ToList();
                }
                var TheCity = cities.FirstOrDefault(x => x.Name == (string)i.Name);
                var InstaCity = new InstagramCity()
                {
                    InstagramData = InstagramDatas,
                    InstagramDataId = InstagramDatas.Id,
                    CityId = TheCity.Id,
                    City = TheCity,
                    Count = i.Value
                };
                InstagramCityList.Add(InstaCity);
            }
            InstagramDatas.InstagramCity = InstagramCityList;
            InstagramDatas.DayImpression = AllDatas[2].data[0].values[0].value ?? 0;
            InstagramDatas.WeekImpression = AllDatas[2].data[1].values[0].value ?? 0;
            InstagramDatas.MonthImpression = AllDatas[2].data[2].values[0].value ?? 0;
            InstagramDatas.DayReach = AllDatas[2].data[3].values[0].value ?? 0;
            InstagramDatas.WeekReach = AllDatas[2].data[4].values[0].value ?? 0;
            InstagramDatas.MonthReach = AllDatas[2].data[5].values[0].value ?? 0;
            _context.InstagramData.Add(InstagramDatas);
            _context.SaveChanges();
        }

        public async Task InstagramDataUpdate(string code)
        {
            string UserId = (await _userManager.GetUserAsync(User)).Id;
            InstagramData InstagramDatas = _context.InstagramData.Include(x => x.InstagramAgeGroup).Include(x => x.InstagramCity).ThenInclude(x => x.City).Include(x => x.InstagramCountry).ThenInclude(x => x.Country).SingleOrDefault(x => x.ApplicationUserId == UserId);
            InstagramDatas.LastUpdated = DateTime.Now;
            var AllDatas = GetInstagramFacebookData(code);
            //MetaData
            InstagramDatas.MediaCount = AllDatas[0].followers_count;
            InstagramDatas.FollowerCount = AllDatas[0].media_count;
            //LifeTimeData
            InstagramDatas.InstagramAgeGroup.InstagramDataId = InstagramDatas.Id;
            InstagramDatas.InstagramAgeGroup.InstagramData = InstagramDatas;
            InstagramDatas.InstagramAgeGroup.Female13To17 = AllDatas[1].data[0].values[0].value["F.13-17"] ?? 0;
            InstagramDatas.InstagramAgeGroup.Female18To24 = AllDatas[1].data[0].values[0].value["F.18-24"] ?? 0;
            InstagramDatas.InstagramAgeGroup.Female25To34 = AllDatas[1].data[0].values[0].value["F.25-34"] ?? 0;
            InstagramDatas.InstagramAgeGroup.Female35To44 = AllDatas[1].data[0].values[0].value["F.35-44"] ?? 0;
            InstagramDatas.InstagramAgeGroup.Female45To55 = AllDatas[1].data[0].values[0].value["F.45-54"] ?? 0;
            InstagramDatas.InstagramAgeGroup.Female55To64 = AllDatas[1].data[0].values[0].value["F.55-64"] ?? 0;
            InstagramDatas.InstagramAgeGroup.Female65Plus = AllDatas[1].data[0].values[0].value["F.65+"] ?? 0;
            InstagramDatas.InstagramAgeGroup.Male13To17 = AllDatas[1].data[0].values[0].value["M.13-17"] ?? 0;
            InstagramDatas.InstagramAgeGroup.Male18To24 = AllDatas[1].data[0].values[0].value["M.18-24"] ?? 0;
            InstagramDatas.InstagramAgeGroup.Male25To34 = AllDatas[1].data[0].values[0].value["M.25-34"] ?? 0;
            InstagramDatas.InstagramAgeGroup.Male35To44 = AllDatas[1].data[0].values[0].value["M.35-44"] ?? 0;
            InstagramDatas.InstagramAgeGroup.Male45To55 = AllDatas[1].data[0].values[0].value["M.45-54"] ?? 0;
            InstagramDatas.InstagramAgeGroup.Male55To64 = AllDatas[1].data[0].values[0].value["M.55-64"] ?? 0;
            InstagramDatas.InstagramAgeGroup.Male65Plus = AllDatas[1].data[0].values[0].value["M.65+"] ?? 0;
            //day,week,month
            var countries = _context.Country.ToList();
            foreach (var i in AllDatas[1].data[1].values[0].value)
            {
                if (!countries.Any(x => x.Name == i.Name))
                {
                    _context.Country.Add(new Country { Name = i.Name });
                    _context.SaveChanges();
                    countries = _context.Country.ToList();
                }
                var TheCountry = countries.FirstOrDefault(x => x.Name == (string)i.Name);
                if (InstagramDatas.InstagramCountry.FirstOrDefault(x => x.CountryId == TheCountry.Id) == null)
                {
                    var InstaCountry = new InstagramCountry()
                    {
                        InstagramData = InstagramDatas,
                        InstagramDataId = InstagramDatas.Id,
                        CountryId = TheCountry.Id,
                        Country = TheCountry,
                        Count = i.Value
                    };
                    InstagramDatas.InstagramCountry.Add(InstaCountry);
                }
                else
                {
                    InstagramDatas.InstagramCountry.FirstOrDefault(x => x.CountryId == TheCountry.Id).Count = i.Value;
                }
            }
            var cities = _context.City.ToList();
            foreach (var i in AllDatas[1].data[2].values[0].value)
            {
                if (!cities.Any(x => x.Name == i.Name))
                {
                    _context.City.Add(new City { Name = i.Name });
                    _context.SaveChanges();
                    cities = _context.City.ToList();
                }
                var TheCity = cities.FirstOrDefault(x => x.Name == (string)i.Name);
                if (InstagramDatas.InstagramCity.FirstOrDefault(x => x.CityId == TheCity.Id) == null)
                {
                    var InstaCity = new InstagramCity()
                    {
                        InstagramData = InstagramDatas,
                        InstagramDataId = InstagramDatas.Id,
                        CityId = TheCity.Id,
                        City = TheCity,
                        Count = i.Value
                    };
                    InstagramDatas.InstagramCity.Add(InstaCity);
                }
                else
                {
                    InstagramDatas.InstagramCity.FirstOrDefault(x => x.CityId == TheCity.Id).Count = i.Value;
                }
            }
            InstagramDatas.DayImpression = AllDatas[2].data[0].values[0].value ?? 0;
            InstagramDatas.WeekImpression = AllDatas[2].data[1].values[0].value ?? 0;
            InstagramDatas.MonthImpression = AllDatas[2].data[2].values[0].value ?? 0;
            InstagramDatas.DayReach = AllDatas[2].data[3].values[0].value ?? 0;
            InstagramDatas.WeekReach = AllDatas[2].data[4].values[0].value ?? 0;
            InstagramDatas.MonthReach = AllDatas[2].data[5].values[0].value ?? 0;
            _context.InstagramData.Update(InstagramDatas);
            _context.SaveChanges();


            var InstagramPlatformId = _context.Platform.FirstOrDefault(x => x.Name == "Instagram").Id;
            if (_context.InfluencerPlatform.Any(x => x.InfluencerProfileId == UserId && x.PlatformId == InstagramPlatformId))
            {
            }
            else
            {
                _context.InfluencerPlatform.Add(new InfluencerPlatform() { PlatformId = InstagramPlatformId, InfluencerProfileId = UserId });
                _context.SaveChanges();
            }
        }

    }
}
