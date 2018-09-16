using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExamProject2017.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ExamProject2017.Configuration;
using ExamProject2017.Data;
using ExamProject2017.Models.HomeViewModel;
using Microsoft.EntityFrameworkCore;


namespace ExamProject2017.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> UserManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = UserManager;
            _roleManager = roleManager;
            _context = context;
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                //eller return vm
                return RedirectToAction("About");
            }

            //---- add user's role ----
            //var auser = await _userManager.FindByNameAsync("tp_vanman@hotmail.com");
            //await _userManager.AddToRoleAsync(auser, "Admin");    
            //user kommer fra viewmodel når den laves


            return View();
        }

        public IActionResult Pricing()
        {
            return View(); 
        }

        public IActionResult SearchInfluenter(int Id)
        {
            var listCountry = _context.Country.Select(x=>x.Name).ToList();
            var listCities = _context.City.Select(x=>x.Name).ToList();

            var model = new SearchInfluenterViewModel()
            {
                City = listCities,
                Country = listCountry
            };

            return View(model);
        }

        public JsonResult InfluenterSearch(string Contry, string City, int FollowersMax, int FollowersMin, int PriceMax, int PriceMin)
        {
            if(PriceMax < PriceMin)
            {
                return Json("Maximum pris er større end minimum pris!");
            }
            if (FollowersMax < FollowersMin)
            {
                return Json("Maximum follower er større end minimum follower!");
            }

            IQueryable<ApplicationUser> ResultUsers=null;

            var TheCountry = _context.Country.FirstOrDefault(x => x.Name == Contry);
            var TheCity = _context.City.FirstOrDefault(x => x.Name == City);

            if (FollowersMax == 0)
            {
                FollowersMax = 99999999;
            }

            if (PriceMax == 0)
            {
                PriceMax = 99999999;
            }


            //kun land er specificeret
            if (Contry == null && !(City == null)) {
                ResultUsers = _context.InstagramData.
                    Where(x =>
                        x.InstagramCity.Any(y => y.City == TheCity) &&
                        x.FollowerCount <= FollowersMax &&
                        x.FollowerCount >= FollowersMin
                        )
                        .Include(x => x.ApplicationUser)
                        .Select(x => x.ApplicationUser);
            }

            //kun by er specificeret
            if (City == null && !(Contry == null))
            {
                ResultUsers = _context.InstagramData.
                    Where(x =>
                        x.InstagramCountry.Any(y => y.Country == TheCountry) &&
                        x.FollowerCount <= FollowersMax &&
                        x.FollowerCount >= FollowersMin
                        )
                        .Include(x => x.ApplicationUser)
                        .Select(x => x.ApplicationUser);
            }

            //by og land er specificeret
            if (!(City == null) && !(Contry == null))
            {
                ResultUsers = _context.InstagramData.
                    Where(x =>
                        x.InstagramCountry.Any(y => y.Country == TheCountry) &&
                        x.InstagramCity.Any(y => y.City == TheCity) &&
                        x.FollowerCount <= FollowersMax &&
                        x.FollowerCount >= FollowersMin
                        )
                        .Include(x => x.ApplicationUser)
                        .Select(x => x.ApplicationUser);
            }

            //ingen er specificeret
            if (City == null && Contry == null)
            {
                ResultUsers = _context.InstagramData.
                    Where(x =>
                        x.FollowerCount <= FollowersMax &&
                        x.FollowerCount >= FollowersMin
                        )
                        .Include(x => x.ApplicationUser)
                        .Select(x => x.ApplicationUser)
                        .Include(x=>x.InfluencerProfile);
            }

            List<ApplicationUser> Result;
            
            //Pris Filter
            Result = ResultUsers
                .Where(x =>
                    x.InfluencerProfile.PriceFrom <= PriceMax &&
                    x.InfluencerProfile.PriceFrom >= PriceMin).ToList();


            for (int i = 0; i < Result.Count(); i++) {
                Result.ElementAt(i).InfluencerProfile = _context.InfluencerProfile.FirstOrDefault(x => x.Id == Result.ElementAt(i).Id);
            }

            return Json(Result);
        }


        public IActionResult About(int Id)
        {
            ViewData["Message"] = "Your application description page.";

            return View("About");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
