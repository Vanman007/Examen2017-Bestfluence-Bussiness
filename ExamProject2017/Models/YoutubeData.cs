using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ExamProject2017.Data;

namespace ExamProject2017.Models
{
    public class YoutubeData : BaseEntity
    {

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser Influencer { get; set; }

     

        public double Engagement { get; set; }
        public int Views { get; set; }
        public double MaleViews { get; set; }
        public double FemaleViews { get; set; }
        public int Subcribers { get; set; }

        public int Likes { get; set; }
        public int Dislike { get; set; }
        public int Comments { get; set; }
    }
}
