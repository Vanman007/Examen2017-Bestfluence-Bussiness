using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Models.HomeViewModel
{
    public class YoutubeViewModel
    {

        public YoutubeData YoutubeData { get; set; }
       

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
