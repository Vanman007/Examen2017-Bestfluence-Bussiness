using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ExamProject2017.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual BusinessProfile BusinessProfile { get; set; }
        public virtual InfluencerProfile InfluencerProfile { get; set; }
    }
}
