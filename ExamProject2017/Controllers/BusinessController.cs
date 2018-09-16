using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ExamProject2017.Models;
using ExamProject2017.Models.BusinessViewModels;
using ExamProject2017.Data;

namespace ExamProject2017.Controllers
{
    [Authorize]
    public class BusinessController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public BusinessController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext; 
        }

        [HttpGet]
        public async Task< IActionResult> Edit()
        {
            // Check if user.BusinessProfile is null.
            // If not null, get all informations
            // If null, do this --> 

            var user = await _userManager.GetUserAsync(User);
            var model = new EditViewModel()
            {
                Email = user.Email,
                Phone = user.PhoneNumber
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            // Check if user.BusinessProfile is null.
            // If not null, get all informations
            // If null, do this --> 

            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                var businessProfile = new BusinessProfile()
                {
                    Id = user.Id,
                    Address = model.Address,
                    BusinessName = model.BusinessName,
                    BusinessType = model.BusinessType,
                    CVR = model.CVR,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Industry = model.Industry,
                    Position = model.Position
                };
                await _dbContext.BusinessProfile.AddAsync(businessProfile);
                await _dbContext.SaveChangesAsync();
                return View(model);
            }
            return View(model); 
        }
    }
}