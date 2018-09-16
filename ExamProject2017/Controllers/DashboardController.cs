using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExamProject2017.Data;
using Microsoft.AspNetCore.Identity;
using ExamProject2017.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace ExamProject2017.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateList()
        {
            var user = await _userManager.GetUserAsync(User);
            var list = new InfluencerList()
            {
                ApplicationUserId = user.Id,
                Name = ""
            };
            await _dbContext.InfluencerLists.AddAsync(list);
            await _dbContext.SaveChangesAsync();
            return Json(list.Id);
        }

        [HttpGet]
        public async Task<JsonResult> GetLists()
        {
            var user = await _userManager.GetUserAsync(User);
            return Json(_dbContext.InfluencerLists.Where(x => x.ApplicationUserId == user.Id));
        }

        [HttpGet]
        public JsonResult GetListObjects(string listId)
        {
            var listObjects = _dbContext.ListObjects.Include(x => x.ApplicationUser).ThenInclude(x => x.InfluencerProfile).Where(x => x.InfluencerListId == listId);
            return Json(listObjects);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteList(string id)
        {
            var list = await _dbContext.InfluencerLists
                .Include(x => x.ListObjects).SingleOrDefaultAsync(x => x.Id == id);
            _dbContext.InfluencerLists.Remove(list);
            await _dbContext.SaveChangesAsync();
            return Json(id);
        }

        [HttpPost]
        public async Task<JsonResult> NameList(string id, string name)
        {
            var list = await _dbContext.InfluencerLists.SingleOrDefaultAsync(x => x.Id == id);
            list.Name = name;
            _dbContext.InfluencerLists.Update(list);
            await _dbContext.SaveChangesAsync();
            return Json("success");
        }

        [HttpGet]
        public async Task<JsonResult> GetAllInfluencers()
        {
            return Json(await _userManager.GetUsersInRoleAsync("Influencer"));
        }


        public async Task<JsonResult> AddToList(string listId, string userId)
        {
            if (_dbContext.ListObjects.Any(x => x.InfluencerListId == listId && x.ApplicationUserId == userId))
                return Json("Error");
            await _dbContext.ListObjects.AddAsync(new ListObject() { ApplicationUserId = userId, InfluencerListId = listId });
            _dbContext.SaveChanges();
            return Json("Success");
        }

        public JsonResult GetAllInfluencere(string id)
        {
            var AllInfluenteres = _userManager.Users;
            return Json(AllInfluenteres);
        }

        public JsonResult Test()
        {
            {
                return Json(new List<int>() { });
            }
        }
    }


}